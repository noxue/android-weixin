namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Network;
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;

    public class NetSceneNewReg : NetSceneBaseEx<NewRegRequest, NewRegResponse, NewRegRequest.Builder>
    {
        private const string TAG = "NetSceneNewReg";

        public void doScene()
        {
            Log.i("NetSceneNewReg", "qq register (3/3) , qq =" + NetSceneAuth.mAuthUserName + ", reg nick = " + NetSceneGetUserName.mRegNickName + ", rep username = " + NetSceneGetUserName.mRepUserName + ", account  = " + SessionPackMgr.getAccount().getUsername());
            this.doSceneEx(NetSceneGetUserName.mRepUserName, NetSceneAuth.mAuthUserPwdMD5, NetSceneAuth.mAuthUserPwd2MD5, NetSceneGetUserName.mRegNickName, Util.stringToInt(NetSceneAuth.mAuthUserName), "", "", "", "", 0, SessionPackMgr.mAuthTicket2);
        }

        private void doSceneEx(string account, string password, string password2, string nickName, int bindUin, string bindEmail, string bindMobile, string imgSid, string imgCode, int regMode, string ticket)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x20);
            base.mBuilder.UserName = Util.NullAsNil(account);
            base.mBuilder.Pwd = Util.NullAsNil(password);
            base.mBuilder.NickName = Util.NullAsNil(nickName);
            base.mBuilder.BindUin = (uint) bindUin;
            base.mBuilder.BindEmail = Util.NullAsNil(bindEmail);
            base.mBuilder.BindMobile = Util.NullAsNil(bindMobile);
            base.mBuilder.RegMode = (uint) regMode;
            base.mBuilder.SetTicket(Util.NullAsNil(ticket));
            base.mBuilder.Language = AccountMgr.GetCurrentLanguage();
            base.mBuilder.DLSrc = 1;
            base.mSessionPack.mCmdID = 0x20;
            base.mSessionPack.mConnectMode = 2;
            base.endBuilder();
        }

        public void doSceneSetPwdforMobileReg(string rawPwd, string nickName)
        {
            Log.i("NetSceneNewReg", "mobile register (3/3) , set nick and password =" + nickName);
            if ((nickName == null) || (nickName.Length <= 0))
            {
                nickName = "nick";
            }
            this.doSceneEx("", Util.getCutPasswordMD5(rawPwd), "", nickName, 0, "", NetSceneBindOpMobileForReg.mMobileNumber, "", "", 1, NetSceneBindOpMobileForReg.mTicket);
        }

        protected override void onFailed(NewRegRequest request, NewRegResponse response)
        {
            uint regMode = request.RegMode;
            this.postFailedEvent(regMode, -800000);
        }
        //public static void testRegisterWithEmail()
        //{
        //    //EventWatcher eventWatcher = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneReg.onMailAuthRegHandler));
        //    //EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MAILREG_SUCCESS, eventWatcher);
        //    //EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MAILREG_ERR, eventWatcher);
        //    ServiceCenter.sceneNewReg.doSceneEx("hkk009x123", Util.getCutPasswordMD5("test.fu4ku"), Util.getCutPasswordMD5("test.fu4ku"), "baobaotest", 0, "h.k.k@qq.com", "18363118008", "", "", 1, "");
        //}
        protected override void onSuccess(NewRegRequest request, NewRegResponse response)
        {
            int uin = (int) response.Uin;
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            uint regMode = request.RegMode;
            if ((uin == 0) || (ret != RetConst.MM_OK))
            {
                this.postFailedEvent(regMode, (int) ret);
            }
            else
            {
                Log.i("NetSceneNewReg", "new reg success. ");
                AccountMgr.onLogin(response.UserName);
                HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                this.updateUserInfo(request, response);
                switch (regMode)
                {
                    case 0:
                        EventCenter.postEvent(EventConst.ON_NETSCENE_NEWREG_SUCCESS, null, null);
                        break;

                    case 1:
                        EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILEREG_SUCCESS, null, null);
                        break;
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_SUCCESS, null, null);
            }
        }

        private void postFailedEvent(uint newRegMode, int ret)
        {
            Log.e("NetSceneNewReg", "new reg failed, ret " + ret);
            if (newRegMode == 0)
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_NEWREG_ERR, ret, null);
            }
            else if (newRegMode == 1)
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILEREG_ERR, ret, null);
            }
        }

        private void updateUserInfo(NewRegRequest request, NewRegResponse response)
        {
            Account account = AccountMgr.getCurAccount();
            account.bytesSessionkey = Util.StringToByteArray(response.SessionKey);
            account.bytesServerID = SessionPackMgr.getSeverID();
            account.strPwd = request.Pwd;
            account.nUin = response.Uin;
            account.strUsrName = response.UserName;
            account.strBindEmail = response.BindEmail;
            account.strBindMobile = request.BindMobile;
            account.nBindQQ = request.BindUin;
            account.nStatus = response.Status;
            account.strOfficalNickName = response.OfficialNickName;
            account.strOfficalUserName = response.OfficialUserName;
            account.nPushMailStatus = (int) response.PushMailStatus;
            account.strQQMicroBlog = response.QQMicroBlogUserName;
            account.nNewUser = 1;
            account.dbLastSessionKeyTimeStamp = Util.getNowSeconds();
            if (!AccountMgr.updateAccount())
            {
                Log.e("NetSceneNewReg", "add account failed");
            }
        }
    }
}

