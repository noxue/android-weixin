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

    public class NetSceneReg : NetSceneBaseEx<RegRequest, RegResponse, RegRequest.Builder>
    {
        private const string TAG = "NetSceneReg";

        public void doScene(string account, string rawPsw, string bindEmail)
        {
            this.doScene(account, rawPsw, "", 0, bindEmail, "", "", "");
        }

        public void doScene(string account, string rawPsw, string nickName, int bindUin, string bindEmail, string bindMobile, string imgSid, string imgCode)
        {
            this.doScene(account, Util.getCutPasswordMD5(rawPsw), Util.getFullPasswordMD5(rawPsw), nickName, bindUin, bindEmail, bindMobile, imgSid, imgCode);
        }

        private void doScene(string account, string password, string password2, string nickName, int bindUin, string bindEmail, string bindMobile, string imgSid, string imgCode)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x1f);
            base.mBuilder.UserName = Util.toSKString(account);
            base.mBuilder.Pwd = Util.toSKString(password);
            base.mBuilder.NickName = Util.toSKString(nickName);
            base.mBuilder.BindUin = (uint) bindUin;
            base.mBuilder.BindEmail = Util.toSKString(bindEmail);
            base.mBuilder.BindMobile = Util.toSKString(bindMobile);
            base.mBuilder.Ticket = Util.toSKString("");
            base.mBuilder.ImgSid = Util.toSKString(imgSid);
            base.mBuilder.ImgCode = Util.toSKString(imgCode);
            base.mBuilder.Pwd2 = password2;
            base.mBuilder.Language = AccountMgr.GetCurrentLanguage();
            base.mBuilder.DLSrc = 1;
            base.mSessionPack.mCmdID = 0x1f;
            base.endBuilder();
        }

        protected override void onFailed(RegRequest request, RegResponse response)
        {
            EventCenter.postEvent(EventConst.ON_NETSCENE_MAILREG_ERR, -800000, null);
        }

        private static void onMailAuthRegHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs.mEventID == EventConst.ON_NETSCENE_MAILREG_SUCCESS)
            {
                Log.d("MAILREG", "(1/2)onMailReg: need Fill NickName");
                Log.d("MAILREG", "(2/2)onMailReg Success");
            }
            else if (evtArgs.mEventID == EventConst.ON_NETSCENE_MAILREG_ERR)
            {
                Log.d("MAILREG", "(2/2)onMailReg error, ret = ");
            }
        }

        private static void onMobileRegHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs.mEventID == EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS)
            {
                Log.d("QQREG", "(1/3)onMobileRegHandler");
                string verifyCode = "3535";
                ServiceCenter.sceneBindOpMobileForReg.doSceneVerify(verifyCode);
            }
            else if (evtArgs.mEventID == EventConst.ON_NETSCENE_MOBILEREG_VERIFY_SUCCESS)
            {
                Log.d("QQREG", "(2/3)onMobileRegHandler");
                ServiceCenter.sceneNewReg.doSceneSetPwdforMobileReg("mm@1234", "halen");
            }
            else if (evtArgs.mEventID == EventConst.ON_NETSCENE_MOBILEREG_SUCCESS)
            {
                Log.d("QQREG", "(3/3)onMobileRegHandler");
            }
        }

        private static void onQQAuthRegHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs.mEventID == EventConst.ON_NETSCENE_AUTH_NEEDREG)
            {
                Log.d("QQREG", "(1/3)onQQAuthNeedFillUserName");
                //ServiceCenter.sceneGetUserName.doScene("halenhuang");
            }
            else if (evtArgs.mEventID == EventConst.ON_NETSCENE_FILLUSERNAME_SUCCESS)
            {
                Log.d("QQREG", "(2/3)onQQAuthNeedReg");
                ServiceCenter.sceneNewReg.doScene();
            }
            else if (evtArgs.mEventID == EventConst.ON_NETSCENE_NEWREG_SUCCESS)
            {
                Log.d("QQREG", "(3/3)onQQAuthSuccess");
            }
        }

        protected override void onSuccess(RegRequest request, RegResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret == RetConst.MM_OK)
            {
                Log.d("NetSceneReg", string.Concat(new object[] { "success. account=", request.UserName, " mail=", request.BindEmail }));
                AccountMgr.onLogin(request.UserName.String);
                HostService.updateAuthBuiltinIP(response.BuiltinIPList);
                this.updateUserInfo(request, response);
                EventCenter.postEvent(EventConst.ON_NETSCENE_MAILREG_SUCCESS, null, null);
                EventCenter.postEvent(EventConst.ON_NETSCENE_AUTH_SUCCESS, null, null);
            }
            else
            {
                Log.d("NetSceneReg", "failed.");
                EventCenter.postEvent(EventConst.ON_NETSCENE_MAILREG_ERR, ret, null);
            }
        }

        public static void testRegisterWithEmail()
        {
            EventWatcher eventWatcher = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneReg.onMailAuthRegHandler));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MAILREG_SUCCESS, eventWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MAILREG_ERR, eventWatcher);
            ServiceCenter.sceneReg.doScene("hkk009x123", "test.fu4ku", "h.k.k@qq.com");
        }

        public static void testRegisterWithMobile()
        {
            EventWatcher eventWatcher = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneReg.onMobileRegHandler));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS, eventWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_VERIFY_SUCCESS, eventWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_MOBILEREG_SUCCESS, eventWatcher);
            ServiceCenter.sceneBindOpMobileForReg.doScene("18923885064");
        }

        public static void testRegisterWithQQ()
        {
            EventWatcher eventWatcher = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneReg.onQQAuthRegHandler));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_NEEDREG, eventWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_FILLUSERNAME_SUCCESS, eventWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_NEWREG_SUCCESS, eventWatcher);
            ServiceCenter.sceneAuth.doScene("31814349", "fu4ku.");
        }

        private void updateUserInfo(RegRequest request, RegResponse response)
        {
            Account account = AccountMgr.getCurAccount();
            account.bytesSessionkey = response.SessionKey.ToByteArray();
            account.bytesServerID = SessionPackMgr.getSeverID();
            account.strPwd = request.Pwd.String;
            account.nUin = response.Uin;
            account.strUsrName = request.UserName.String;
            account.strBindEmail = request.BindEmail.String;
            account.strBindMobile = request.BindMobile.String;
            account.nBindQQ = request.BindUin;
            account.strOfficalNickName = response.OfficialNickName.String;
            account.strOfficalUserName = response.OfficialUserName.String;
            account.nPushMailStatus = (int) response.PushMailStatus;
            account.strQQMicroBlog = response.QQMicroBlogUserName.String;
            account.nNewUser = 1;
            account.dbLastSessionKeyTimeStamp = Util.getNowSeconds();
            if (!AccountMgr.updateAccount())
            {
                Log.e("NetSceneReg", "add account failed");
            }
        }
    }
}

