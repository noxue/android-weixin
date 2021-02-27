using micromsg;
using MicroMsg.Common.Event;
using MicroMsg.Common.Utils;
using MicroMsg.Manager;
using MicroMsg.Network;
using MicroMsg.Protocol;
using MicroMsg.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroMsg.Scene
{
    public class NetSceneNewAuth : NetSceneBaseEx<NewAuthRequest, NewAuthResponse, NewAuthRequest.Builder>
    {
        public static string mAuthUserName;

        public static string mAuthUserPwdMD5;
        public static int mIDCUserAuthCount = 0;
        public const int STATUS_ENABLE = 1;
        public const int STATUS_NOTIFY = 2;
        public bool doScene(string userName, string pwd)
        {
            try
            {
                base.beginBuilder();
                mAuthUserName = userName;
                mAuthUserPwdMD5 = pwd;

                base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(1, 1644430665);//369492260  1644430665

                //string a = Util.byteToHexStr(base.mBuilder.BaseRequest.ToByteArray());
                base.mBuilder.UserName = Util.toSKString(userName);
                base.mBuilder.Pwd = Util.toSKString(pwd);
                base.mBuilder.Pwd2 = Util.NullAsNil("");
                base.mBuilder.ImgSid = Util.toSKString("");
                base.mBuilder.ImgCode = Util.toSKString("");


                //base.mBuilder.ClientSeqID = "WP7" + ((long)Util.getNowMilliseconds());
              //  base.mBuilder.SoftType = "<softtype><k3>9.3.2</k3><k9>iPad</k9><k10>2</k10><k19>AC638664-9598-41F1-ADDB-5791FE4ADB2D</k19><k20>9697B844-9368-4154-A6F7-5A4623B06D27</k20><k21>405-5G</k21><k22>(null)</k22><k24>74:44:1:8d:60:27</k24><k33>\\345\\276\\256\\344\\277\\241</k33><k47>1</k47><k50>0</k50><k51>com.luoming</k51><k54>iPad4,4</k54></softtype>";
                //base.mBuilder.DeviceType = "iPad";
                //base.mBuilder.IPhoneVer = "iPad4,4";

                base.mBuilder.Channel = 0;// AppInfoHelper.ChannelId;
                base.mBuilder.InputType = 2;
                base.mBuilder.TimeStamp = Convert.ToUInt32(Util.getNowSeconds().ToString().Substring(0, 10));
                base.mBuilder.BuiltinIPSeq = 0;
                base.mBuilder.WTLoginReqBuff= Util.toSKBuffer("");
                base.mBuilder.BundleID = "com.tencent.xin";
                base.mBuilder.DeviceName = "Apple Watch";
                base.mBuilder.DeviceType = ConstantsProtocol.DEVICE_TYPE;

                base.mBuilder.Language = "zh_CN";//AccountMgr.GetCurrentLanguage();
                base.mBuilder.DeviceBrand = "Apple";//DeviceStatus.get_DeviceManufacturer() ?? "";
                base.mBuilder.RealCountry = "CN";//DeviceStatus.get_DeviceManufacturer() ?? "";
                base.mBuilder.DeviceModel = "";//DeviceStatus.get_DeviceName() ?? "";
                base.mBuilder.RandomEncryKey = Util.toSKBuffer(new byte[] { 104, 84, 125, 199, 142, 226, 48, 218, 83, 195, 3, 84, 3, 123, 208, 162 });
                base.mBuilder.OSType = "8.10.12393.0";
               // base.mBuilder.IMEI = "4eff1bfe75a6cb3216004eefb8d78d84";
                base.mBuilder.KSid = Util.toSKBuffer("");
                base.mBuilder.TimeZone ="8.00";
               // base.mBuilder.ClientSeqID = "4eff1bfe75a6cb3216004eefb8d78d84-" + Util.getNowSeconds().ToString().Substring(0, 10);
               // base.mBuilder.AdSource = "AC638664-9598-41F1-ADDB-5791FE4ADB2D";
                base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/newauth";
                base.mSessionPack.mCmdID = 0xb2;
         
                base.endBuilder();

            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }
        protected override void onFailed(NewAuthRequest request, NewAuthResponse response)
        {
            //EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_ERR, -800000, null);
        }

        protected override void onSuccess(NewAuthRequest request, NewAuthResponse response)
        {
            int uin = (int)response.Uin;
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if (ret == RetConst.MM_ERR_IDC_REDIRECT)
            {
                if (mIDCUserAuthCount < 3)
                {
                    mIDCUserAuthCount++;
                    this.doScene(mAuthUserName, mAuthUserPwdMD5);
                    return;
                }
                Log.e("NetSceneAuth", "Redirect IDC too much, user auth failed!");
            }
            mIDCUserAuthCount = 0;
            if ((uin != 0) && (((ret == RetConst.MM_OK) || (ret == RetConst.MM_ERR_CRITICALUPDATE)) || (ret == RetConst.MM_ERR_RECOMMENDEDUPDATE)))
            {
                Log.d("NetSceneAuth", "auth scene success. SessionKey"+Util.byteToHexStr(response.SessionKey.ToByteArray()));              
                AccountMgr.onLogin(response.UserName.String);
                SessionPackMgr.getAccount().SessionKey = response.SessionKey.ToByteArray();
                this.updateUserInfo(request, response, SessionPackMgr.getSeverID());
                EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_SUCCESS, response, null);
                

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
                            VerifyCodeArgs args = new VerifyCodeArgs
                            {
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
        private bool updateUserInfo(NewAuthRequest authRequest, NewAuthResponse authResponse, byte[] serverID)
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
}
