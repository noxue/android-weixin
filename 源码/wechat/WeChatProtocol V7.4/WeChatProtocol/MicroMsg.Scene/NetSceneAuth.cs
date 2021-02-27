namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using MicroMsg.Protocol;
    using System;
    using MicroMsg.Manager;
    using MicroMsg.Storage;
    using Common.Timer;

    public class NetSceneAuth : NetSceneBaseEx<AuthRequest, AuthResponse, AuthRequest.Builder>
    {
        public static string mAuthUserName;
        public static string mAuthUserPwd2MD5;
        public static string mAuthUserPwdMD5;
        public static int mIDCUserAuthCount = 0;
        public const int STATUS_ENABLE = 1;
        public const int STATUS_NOTIFY = 2;
        private const string TAG = "NetSceneAuth";

        public  bool doScene(string userName, string pwd)
        {
            if (userName == "facebook@wechat_auth")
            {
                return this.doSceneEx(userName, pwd, "", "", "");
            }
            return this.doSceneEx(userName, Util.getCutPasswordMD5(pwd), Util.getFullPasswordMD5(pwd), "", "");
        }

        private bool doSceneEx(string userName, string pwd, string pwd2, string imgSid, string imgCode)
        {
            base.beginBuilder();
            mAuthUserName = userName;
            mAuthUserPwdMD5 = pwd;
            mAuthUserPwd2MD5 = pwd2;
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(1, 369298705);//369298705
            base.mBuilder.UserName = Util.toSKString(userName);
            base.mBuilder.Pwd = Util.toSKString(pwd);
            base.mBuilder.Pwd2 = Util.NullAsNil(pwd2);
            base.mBuilder.ImgSid = Util.toSKString(imgSid);
            base.mBuilder.ImgCode = Util.toSKString(imgCode);
            base.mBuilder.Channel = 1;// AppInfoHelper.ChannelId;
            base.mBuilder.Language = "zh_CN";//AccountMgr.GetCurrentLanguage();
            base.mBuilder.DeviceBrand = "";//DeviceStatus.get_DeviceManufacturer() ?? "";
            base.mBuilder.DeviceModel = "";//DeviceStatus.get_DeviceName() ?? "";
            base.mBuilder.OSType = Environment.OSVersion.Version.ToString();
            base.mBuilder.TimeZone = Util.getTimeZoneOffsetGwt();
            base.mSessionPack.mCmdID = 1;
            base.endBuilder();
            return true;
        }

        public bool QrcodeLogin(string userName, string pwd)
        {
            base.beginBuilder();
            mAuthUserName = userName;
            mAuthUserPwdMD5 = pwd;
           // mAuthUserPwd2MD5 = pwd2;
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(1, 369298705);//369298705 //1644430665
            base.mBuilder.UserName = Util.toSKString(userName);
            base.mBuilder.Pwd = Util.toSKString(pwd);
           // base.mBuilder.Pwd2 = Util.NullAsNil(pwd2);
            base.mBuilder.ImgSid = Util.toSKString("");
            //base.mBuilder.DeviceType = "Windows 10";
            base.mBuilder.ImgCode = Util.toSKString("");
            base.mBuilder.Channel = 1;// AppInfoHelper.ChannelId;
            base.mBuilder.Language = "zh_CN";//AccountMgr.GetCurrentLanguage();
            base.mBuilder.DeviceBrand = "";//DeviceStatus.get_DeviceManufacturer() ?? "";
            base.mBuilder.DeviceModel = "";//DeviceStatus.get_DeviceName() ?? "";
            base.mBuilder.OSType = Environment.OSVersion.Version.ToString();
            base.mBuilder.TimeZone = Util.getTimeZoneOffsetGwt();
            base.mSessionPack.mCmdID = 1;
            base.endBuilder();
            return true;
        }

        public bool doSceneWithVerify(string userName, string md5Pwd, string md5Pwd2, string imgSid, string imgCode)
        {
            return this.doSceneEx(userName, Util.getCutPasswordMD5(md5Pwd),  Util.getFullPasswordMD5(md5Pwd2), imgSid, imgCode);
        }

        protected override void onFailed(AuthRequest request, AuthResponse response)
        {
            EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_ERR, -800000, null);
        }

        protected override void onSuccess(AuthRequest request, AuthResponse response)
        {
            int uin = (int) response.Uin;
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret == RetConst.MM_ERR_IDC_REDIRECT)
            {
                if (mIDCUserAuthCount < 3)
                {
                    mIDCUserAuthCount++;
                    this.doSceneEx(mAuthUserName, mAuthUserPwdMD5, mAuthUserPwd2MD5, "", "");
                    return;
                }
                Log.e("NetSceneAuth", "Redirect IDC too much, user auth failed!");
            }
            mIDCUserAuthCount = 0;
            if ((uin != 0) && (((ret == RetConst.MM_OK) || (ret == RetConst.MM_ERR_CRITICALUPDATE)) || (ret == RetConst.MM_ERR_RECOMMENDEDUPDATE)))
            {
                Log.d("NetSceneAuth", "auth scene success.");
                AccountMgr.onLogin(response.UserName.String);
                this.updateUserInfo(request, response, SessionPackMgr.getSeverID());
                EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_SUCCESS, response, null);
                ServiceCenter.sceneSendMsgOld.testSendMsg(response.UserName.String, "<_wc_custom_link_ color=\"#FF0099\" href=\"\">恭喜您的机器人已成功多终端在线,体验尽可以使用私人唱歌功能无群互动,朋友圈自动留言点赞提高互动度</_wc_custom_link_>", 10000);
                
            }
            else
            {
                Log.e("NetSceneAuth", "auth scene failed, ret =" + response.BaseResponse.ErrMsg.String);
                //new NetSceneAddSafeDevice().doScene(response.AuthTicket);
                switch (ret)
                {
                    case RetConst.MM_ERR_NEEDREG:
                        EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_NEEDREG, null, null);
                        return;

                    case RetConst.MM_ERR_NEED_VERIFY:
                    {
                        if ((response.ImgBuf == null) || (response.ImgSid == null))
                        {
                            Log.e("NetSceneAuth", "NEED_VERIFY_USER, but ImgSid or ImgBuf is null");
                            EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_ERR, ret, null);
                            return;
                        }
                        VerifyCodeArgs args = new VerifyCodeArgs {
                            mImageSid = response.ImgSid.String,
                            mImageBuf = response.ImgBuf.Buffer.ToByteArray()
                        };
                        //Log.d("NetSceneAuth", "received verify image , sid = " + args.mImageSid);
                        EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_NEEDVERIFY, args, null);
                        return;
                    }
                    case RetConst.MM_ERR_QQ_OK_NEED_MOBILE:
                        EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_ERR, ret, response.BindMobile.String);
                        break;
                }
                if (ret == RetConst.MM_ERR_QQ_OK_NEED_MOBILE)
                {
                    EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_ERR, ret, response.BindMobile.String);
                }
                else
                {
                    EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_ERR, ret, null);
                }
            }
        }

        private bool updateUserInfo(AuthRequest authRequest, AuthResponse authResponse, byte[] serverID)
        {
            Account account = AccountMgr.getCurAccount();
            account.bytesSessionkey = authResponse.SessionKey.ToByteArray();
            account.bytesServerID = serverID;
            if (authRequest.UserName.String == "facebook@wechat_auth")
            {
                account.strPwd = authResponse.Password;
                account.strPwd2 = authResponse.Password;
            }
            else
            {
                account.strPwd = authRequest.Pwd.String;
                account.strPwd2 = authRequest.Pwd2;
            }
            account.nUin = authResponse.Uin;
            account.strUsrName = authResponse.UserName.String;
            account.strNickName = authResponse.NickName.String;
            account.strBindEmail = authResponse.BindEmail.String;
            account.strBindMobile = authResponse.BindMobile.String;
            account.nBindQQ = authResponse.BindUin;
            account.nStatus = authResponse.Status;
            account.strOfficalNickName = authResponse.OfficialNickName.String;
            account.strOfficalUserName = authResponse.OfficialUserName.String;
            account.nPushMailStatus = (int)authResponse.PushMailStatus;
            account.strQQMicroBlog = authResponse.QQMicroBlogUserName.String;
            account.nQQMBlogStatus = (int)authResponse.QQMicroBlogStatus;
            account.dbLastSessionKeyTimeStamp = Util.getNowSeconds();
            account.strAlias = authResponse.Alias;
            account.nPluginFlag = authResponse.PluginFlag;
            account.bytesA2Key = authResponse.A2Key.Buffer.ToByteArray();
            account.strFSURL = authResponse.FSURL;
            account.strAuthTicket = authResponse.AuthTicket;
            account.nSafeDevice = authResponse.SafeDevice;
            //account.nMainAcctType = authResponse.MainAcctType;
            AccountMgr.updateAccount();
            return true;
        }

    }
    public class VerifyCodeArgs
    {
        public byte[] mImageBuf;
        public string mImageSid;
    }
}

