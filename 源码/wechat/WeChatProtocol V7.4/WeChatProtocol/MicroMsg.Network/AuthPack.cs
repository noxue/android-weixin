namespace MicroMsg.Network
{
    using Google.ProtocolBuffers;
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    // using MicroMsg.Manager;
    using MicroMsg.Protocol;
    //using MicroMsg.Storage;
    //using Microsoft.Phone.Info;
    using System;
    using MicroMsg.Manager;
    using MicroMsg.Storage;

    public class AuthPack
    {
        public static int mIDCAutoAuthCount = 0;
        public static string mKickMessage = "";

        private static byte[] authRequestToByteArray(object obj)
        {
            NewAuthRequest request = obj as NewAuthRequest;
            Console.WriteLine(Util.byteToHexStr(request.ToByteArray()));
            return request.ToByteArray();
        }
        //¶þ´ÎµÇÂ½
        private static NewAuthRequest makeAuthRequest(int scene)
        {
            if (!SessionPackMgr.getAccount().isValid())
            {
                return null;
            }
            BaseRequest defaultInstance = BaseRequest.DefaultInstance;
            BaseRequest.Builder builder = BaseRequest.CreateBuilder();
            //builder.SessionKey = ByteString.CopyFrom(SessionPackMgr.getSessionKeyEx());
            builder.SessionKey = ByteString.CopyFrom(SessionPackMgr.getAccount().SessionKey);
            builder.Uin = (uint)SessionPackMgr.getAccount().getUin();
            builder.DeviceID = ByteString.CopyFromUtf8(Util.getDeviceUniqueId());
            builder.ClientVersion = (int)ConstantsProtocol.CLIENT_MIN_VERSION;
            builder.DeviceType = ByteString.CopyFromUtf8(ConstantsProtocol.DEVICE_TYPE);
            builder.Scene = (uint)scene;
            defaultInstance = builder.Build();
          
            NewAuthRequest.Builder builder2 = NewAuthRequest.CreateBuilder();
            builder2.BaseRequest = defaultInstance;
            builder2.UserName = Util.toSKString(SessionPackMgr.getAccount().getUsername());
            builder2.Pwd = Util.toSKString(SessionPackMgr.getAccount().getPassword());
           // builder2.Pwd2 = Util.NullAsNil("");
            builder2.ImgSid = Util.toSKString("");
            builder2.ImgCode = Util.toSKString("");
            builder2.Channel = 0;// AppInfoHelper.ChannelId;
            builder2.InputType = 0;
            builder2.TimeStamp = Convert.ToUInt32(Util.getNowSeconds().ToString().Substring(0, 10));
            builder2.BuiltinIPSeq = 0;
            //builder2.WTLoginReqBuff = Util.toSKBuffer("");
            //builder2.BundleID = "com.tencent.xin";
            builder2.DeviceName = "Apple Watch";

            builder2.DeviceType = ConstantsProtocol.DEVICE_TYPE;
            builder2.AutoAuthTicket = SessionPackMgr.getAccount().AutoAuthKey;
            builder2.Language = "zh_CN";//AccountMgr.GetCurrentLanguage();
            builder2.DeviceBrand = "Apple";//DeviceStatus.get_DeviceManufacturer() ?? "";
            builder2.RealCountry = "CN";//DeviceStatus.get_DeviceManufacturer() ?? "";
            builder2.DeviceModel = "";//DeviceStatus.get_DeviceName() ?? "";
            builder2.RandomEncryKey = Util.toSKBuffer(new byte[] { 104, 84, 125, 199, 142, 226, 48, 218, 83, 195, 3, 84, 3, 123, 208, 162 });
            builder2.OSType = "8.10.12393.0";
            builder2.TimeZone = "8.00";

            Log.i("Network", "Old AutoAuthTicket = " + SessionPackMgr.getAccount().AutoAuthKey);
            Util.WriteLog("Old AutoAuthTicket = " + SessionPackMgr.getAccount().AutoAuthKey);
            return builder2.Build();
        }

        public static SessionPack makeAutoAuthPack(int scene)
        {
            NewAuthRequest request = makeAuthRequest(scene);
            if (request == null)
            {
                return null;
            }
            SessionPack pack = new SessionPack
            {
                mCmdID = 0xb2,
                mRequestObject = request,
                mCmdUri = "/cgi-bin/micromsg-bin/newauth",
                mNeedAutoAuth = true
            };
            pack.mProcRequestToByteArray += new RequestToByteArrayDelegate(AuthPack.authRequestToByteArray);
            pack.mCompleted += new SessionPackCompletedDelegate(AuthPack.onAutoAuthCompleted);
            return pack;
        }

        private static void onAutoAuthCompleted(object sender, PackEventArgs e)
        {
            if (!e.isSuccess())
            {
                Log.e("Network", "auto auth failed .");
            }
            else
            {
                SessionPack pack = sender as SessionPack;
                NewAuthResponse mResponseObject = (NewAuthResponse)pack.mResponseObject;
                NewAuthRequest mRequestObject = (NewAuthRequest)pack.mRequestObject;
                RetConst ret = (RetConst)mResponseObject.BaseResponse.Ret;
                if (ret == RetConst.MM_ERR_IDC_REDIRECT)
                {
                    if (mIDCAutoAuthCount < 3)
                    {
                        mIDCAutoAuthCount++;
                        SessionPackMgr.putToHead(makeAutoAuthPack(2));
                        return;
                    }
                    Log.e("Network", "Redirect IDC too much, auto auth failed!");
                }
                mIDCAutoAuthCount = 0;
                switch (ret)
                {
                    case RetConst.MM_OK:
                    case RetConst.MM_ERR_CRITICALUPDATE:
                    case RetConst.MM_ERR_RECOMMENDEDUPDATE:
                        {
                            Log.i("Network", "auto auth success. ");
                            Log.i("Network", "New AutoAuthTicket = " + SessionPackMgr.getAccount().AutoAuthKey);
                            Util.WriteLog("New AutoAuthTicket = " + SessionPackMgr.getAccount().AutoAuthKey);
                            // Account account = AccountMgr.getCurAccount();
                            // account.bytesSessionkey = mResponseObject.SessionKey.ToByteArray();
                            // account.nUin = mResponseObject.Uin;
                            //  account.dbLastSessionKeyTimeStamp = Util.getNowSeconds();
                            //  account.bytesA2Key = mResponseObject.A2Key.Buffer.ToByteArray();
                            SessionPackMgr.getAccount().SessionKey = mResponseObject.SessionKey.ToByteArray();
                            AccountMgr.updateAccount();
                            EventCenter.postEvent(EventConst.ON_NETSCENE_AUTOAUTH_SUCCESS, mResponseObject, null);
                            return;
                        }
                }
                if (ret == RetConst.MM_ERR_NEED_VERIFY)
                {
                    if ((mResponseObject.ImgBuf == null) || (mResponseObject.ImgSid == null))
                    {
                        Log.e("Network", "NEED_VERIFY_USER, but ImgSid or ImgBuf is null");
                        EventCenter.postEvent(EventConst.ON_NETSCENE_AUTOAUTH_ERR, ret, null);
                    }
                    else
                    {
                        //VerifyCodeArgs args = new VerifyCodeArgs {
                        //    mImageSid = mResponseObject.ImgSid.String,
                        //    mImageBuf = mResponseObject.ImgBuf.Buffer.ToByteArray()
                        //};
                        Log.e("Network", "auto auth failed, need verify,  sid = " + "args.mImageSid");
                        //EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_NEEDVERIFY, args, null);
                    }
                }
                else if (ret == RetConst.MM_ERR_QQ_OK_NEED_MOBILE)
                {
                    Log.i("Network", "autoAuth Need Mobile veryfy ret = " + ret.ToString());
                    EventCenter.postEvent(EventConst.ON_NETSCENE_AUTOAUTH_ERR, ret, mResponseObject.BindMobile.String);
                }
                else
                {
                    Log.e("Network", "auto auth failed, result = " + mResponseObject.BaseResponse.ErrMsg.String);
                    EventCenter.postEvent(EventConst.ON_NETSCENE_AUTOAUTH_ERR, ret, null);
                }
            }
        }

        public static object onParserAuthPack(SessionPack sessionPack)
        {
            AuthResponse response = AuthResponse.ParseFrom(sessionPack.mResponseBuffer);
            if (response == null)
            {
                Log.e("Network", "auth parse failed. ");
                return null;
            }
            int uin = (int)response.Uin;
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            Log.d("Network", "auth parsed success. ");
            if ((uin != 0) && (((ret == RetConst.MM_OK) || (ret == RetConst.MM_ERR_CRITICALUPDATE)) || (ret == RetConst.MM_ERR_RECOMMENDEDUPDATE)))
            {
                Log.i("Network", "auth PASS, uin= " + uin);
                AuthRequest mRequestObject = sessionPack.mRequestObject as AuthRequest;
                if ((mRequestObject != null) && (mRequestObject.UserName.String == "facebook@wechat_auth"))
                {
                    SessionPackMgr.getAccount().setAuthInfo(response.UserName.String, response.Password, response.Password);
                }
                HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                SessionPackMgr.setSessionKey(response.SessionKey.ToByteArray());
               
                SessionPackMgr.getAccount().setUin(uin);
                SessionPackMgr.getAccount().Nickname = response.NickName.String;
                //SessionPackMgr.getAccount().Headimg=response.WTLoginRspBuff
                SessionPackMgr.setAuthStatus(2);
                return response;
            }
            Log.e("Network", "auth Failed,ret = " + ret);
            switch (ret)
            {
                case RetConst.MM_ERR_NEEDREG:
                    Log.i("Network", "auth result: need register");
                    SessionPackMgr.mAuthTicket1 = response.Ticket;
                    Connector.close();
                    return response;

                case RetConst.MM_ERR_IDC_REDIRECT:
                    Log.i("Network", "Need to redirect IDC for auth ...");
                    HostService.updateAuthIDCHost(response.NewHostList);
                    HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                    Connector.close();
                    return response;

                case RetConst.MM_ERR_QQ_OK_NEED_MOBILE:
                    SessionPackMgr.setSessionKey(null);
                    return response;

                default:
                    SessionPackMgr.setSessionKey(null);
                    SessionPackMgr.getAccount().reset();
                    switch (ret)
                    {
                        case RetConst.MM_ERR_RECOMMENDEDUPDATE:
                            Log.e("Network", "Auth Failed: MM_ERR_RECOMMENDEDUPDATE ");
                            goto Label_022B;

                        case RetConst.MM_ERR_CRITICALUPDATE:
                            Log.e("Network", "Auth Failed: MM_ERR_CRITICALUPDATE ");
                            goto Label_022B;

                        case RetConst.MM_ERR_AUTH_ANOTHERPLACE:
                            Log.e("Network", "Auth Failed: MM_ERR_AUTH_ANOTHERPLACE ");
                            mKickMessage = response.KickResponse;
                            Sender.getInstance().closeSender();
                            goto Label_022B;

                        case RetConst.MM_ERR_NEED_VERIFY:
                            Log.e("Network", "Auth Failed: MM_ERR_NEED_VERIFY ");
                            goto Label_022B;

                        case RetConst.MM_ERR_NOUSER:
                            Log.e("Network", "Auth Failed: MM_ERR_NOUSER ");
                            goto Label_022B;

                        case RetConst.MM_ERR_PASSWORD:
                            Log.e("Network", "Auth Failed: MM_ERR_PASSWORD ");
                            goto Label_022B;
                    }
                    break;
            }
            Label_022B:
            Connector.close();
            return response;
        }
        public static object onParserNewAuthPack(SessionPack sessionPack)
        {
            NewAuthResponse response = NewAuthResponse.ParseFrom(sessionPack.mResponseBuffer);
            if (response == null)
            {
                Log.e("Network", "auth parse failed. ");
                return null;
            }
            int uin = (int)response.Uin;
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            Log.d("Network", "auth parsed success. ");
            if ((uin != 0) && (((ret == RetConst.MM_OK) || (ret == RetConst.MM_ERR_CRITICALUPDATE)) || (ret == RetConst.MM_ERR_RECOMMENDEDUPDATE)))
            {
                Log.i("Network", "auth PASS, uin= " + uin);
            
                HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                SessionPackMgr.setSessionKey(response.SessionKey.ToByteArray());
                SessionPackMgr.getAccount().AutoAuthKey = response.AutoAuthTicket;
                SessionPackMgr.getAccount().setUin(uin);
                SessionPackMgr.getAccount().Nickname = response.NickName.String;
                //SessionPackMgr.getAccount().Headimg=response.WTLoginRspBuff
                SessionPackMgr.setAuthStatus(2);
                return response;
            }
            Log.e("Network", "auth Failed,ret = " + ret);
            switch (ret)
            {
                case RetConst.MM_ERR_NEEDREG:
                    Log.i("Network", "auth result: need register");
                    SessionPackMgr.mAuthTicket1 = response.Ticket;
                    Connector.close();
                    return response;

                case RetConst.MM_ERR_IDC_REDIRECT:
                    Log.i("Network", "Need to redirect IDC for auth ...");
                    HostService.updateAuthIDCHost(response.NewHostList);
                    HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                    Connector.close();
                    return response;

                case RetConst.MM_ERR_QQ_OK_NEED_MOBILE:
                    SessionPackMgr.setSessionKey(null);
                    return response;

                default:
                    SessionPackMgr.setSessionKey(null);
                    SessionPackMgr.getAccount().reset();
                    switch (ret)
                    {
                        case RetConst.MM_ERR_RECOMMENDEDUPDATE:
                            Log.e("Network", "Auth Failed: MM_ERR_RECOMMENDEDUPDATE ");
                            goto Label_022B;

                        case RetConst.MM_ERR_CRITICALUPDATE:
                            Log.e("Network", "Auth Failed: MM_ERR_CRITICALUPDATE ");
                            goto Label_022B;

                        case RetConst.MM_ERR_AUTH_ANOTHERPLACE:
                            Log.e("Network", "Auth Failed: MM_ERR_AUTH_ANOTHERPLACE ");
                            mKickMessage = response.KickResponse;
                            Sender.getInstance().closeSender();
                            goto Label_022B;

                        case RetConst.MM_ERR_NEED_VERIFY:
                            Log.e("Network", "Auth Failed: MM_ERR_NEED_VERIFY ");
                            goto Label_022B;

                        case RetConst.MM_ERR_NOUSER:
                            Log.e("Network", "Auth Failed: MM_ERR_NOUSER ");
                            goto Label_022B;

                        case RetConst.MM_ERR_PASSWORD:
                            Log.e("Network", "Auth Failed: MM_ERR_PASSWORD ");
                            goto Label_022B;
                    }
                    break;
            }
            Label_022B:
            Connector.close();
            return response;
        }
        public static object onParserBindOpMobileForReg(SessionPack sessionPack)
        {
            BindOpMobileResponse response = BindOpMobileResponse.ParseFrom(sessionPack.mResponseBuffer);
            if (response == null)
            {
                Log.e("Network", "BindOpMobileResponse parse failed. ");
                return null;
            }
            if (response.BaseResponse.Ret == -301)
            {
                Log.i("Network", "Need to redirect IDC for BindOpMobileForReg...");
                HostService.updateAuthIDCHost(response.NewHostList);
                HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                Connector.close();
            }
            return response;
        }

        public static object onParserGetUserNamePack(SessionPack sessionPack)
        {
            GetUserNameResponse response = GetUserNameResponse.ParseFrom(sessionPack.mResponseBuffer);
            if (response == null)
            {
                Log.e("Network", "getusername parse failed. ");
                return null;
            }
            Connector.close();
            Log.d("Network", "getUserName parsed success. ");
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("Network", "NetSceneGetUserName: failed. ret = " + ret);
                return response;
            }
            Log.i("Network", "NetSceneGetUserName: success.name = " + response.UserName);
            SessionPackMgr.getAccount().setUsername(response.UserName);
            SessionPackMgr.mAuthTicket2 = response.Ticket;
            return response;
        }

        public static object onParserNewRegPack(SessionPack sessionPack)
        {
            NewRegResponse response = NewRegResponse.ParseFrom(sessionPack.mResponseBuffer);
            if (response == null)
            {
                Log.e("Network", "new reg parse failed. ");
                return null;
            }
            int uin = (int)response.Uin;
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            Log.d("Network", string.Concat(new object[] { "new reg parsed success. uin= ", uin, "ret =", ret }));
            NewRegRequest mRequestObject = sessionPack.mRequestObject as NewRegRequest;
            uint regMode = mRequestObject.RegMode;
            if ((uin != 0) && (ret == RetConst.MM_OK))
            {
                Log.i("Network", "new reg PASS");
                SessionPackMgr.setSessionKey(Util.StringToByteArray(response.SessionKey));
                SessionPackMgr.getAccount().setUin(uin);
                SessionPackMgr.setAuthStatus(2);
                return response;
            }
            Log.e("Network", "new reg failed, ret = " + ret);
            SessionPackMgr.setSessionKey(null);
            SessionPackMgr.getAccount().reset();
            return response;
        }

        public static object onParserRegPack(SessionPack sessionPack)
        {
            RegResponse response = RegResponse.ParseFrom(sessionPack.mResponseBuffer);
            if (response == null)
            {
                Log.e("Network", "register parse failed. ");
                return null;
            }
            Log.d("Network", "register parsed success. ");
            int uin = (int)response.Uin;
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if ((uin != 0) && (ret == RetConst.MM_OK))
            {
                Log.i("Network", "register PASS");
                SessionPackMgr.setSessionKey(response.SessionKey.ToByteArray());
                SessionPackMgr.getAccount().setUin(uin);
                SessionPackMgr.setAuthStatus(2);
                return response;
            }
            Log.e("Network", "register Failed, ret = " + ret);
            SessionPackMgr.setSessionKey(null);
            SessionPackMgr.getAccount().reset();
            return response;
        }

        public static bool preProcessAuthPack(SessionPack sessionPack, ref object responseObject)
        {
            string str;
            switch (sessionPack.mCmdID)
            {
                case 0x1f:
                    responseObject = onParserRegPack(sessionPack);
                    return (responseObject != null);

                case 0x20:
                    responseObject = onParserNewRegPack(sessionPack);
                    return (responseObject != null);

                case 0x21:
                    responseObject = onParserGetUserNamePack(sessionPack);
                    return (responseObject != null);

                case 1:
                    responseObject = onParserAuthPack(sessionPack);
                    return (responseObject != null);
                case 0xb2:
                    responseObject = onParserNewAuthPack(sessionPack);
                    return (responseObject != null);

                case 0xb9:
                    TenPayResponse response = TenPayResponse.ParseFrom(sessionPack.mResponseBuffer);
                    responseObject = response;
                    return (responseObject != null);
            }
            if (((str = sessionPack.mCmdUri) != null) && (str == "/cgi-bin/micromsg-bin/bindopmobileforreg"))
            {
                responseObject = onParserBindOpMobileForReg(sessionPack);
                return (responseObject != null);
            }
            return true;
        }

        public static void updateAccountInfoFromAuth(object request)
        {

            switch (request.GetType().Name)
            {
                case "AuthRequest":
                    AuthRequest request2 = request as AuthRequest;
                    SessionPackMgr.getAccount().setAuthInfo(request2.UserName.String, request2.Pwd.String, request2.Pwd2);
                    break;
                case "NewAuthRequest":
                    NewAuthRequest request3 = request as NewAuthRequest;
                    SessionPackMgr.getAccount().setAuthInfo(request3.UserName.String, request3.Pwd.String, request3.Pwd2);

                    break;
            }


            
            
        }

        public static void updateAccountInfoFromNewReg(object request)
        {
            NewRegRequest request2 = request as NewRegRequest;
            SessionPackMgr.getAccount().setAuthInfo(request2.UserName, request2.Pwd, "");
        }

        public static void updateAccountInfoFromReg(object request)
        {
            RegRequest request2 = request as RegRequest;
            SessionPackMgr.getAccount().setAuthInfo(request2.UserName.String, request2.Pwd.String, request2.Pwd2);
        }
    }
}

