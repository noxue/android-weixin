namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;
    using System.Runtime.InteropServices;

    public class NetSceneBindOpMobileForReg : NetSceneBaseEx<BindOpMobileRequest, BindOpMobileResponse, BindOpMobileRequest.Builder>
    {
        public static int DIALFLAG_NO = 0;
        public static int DIALFLAG_YES = 1;
        public static int mIDCMobileRegCount = 0;
        public static string mMobileNumber;
        public static string mTicket;
        public static string mUserName;
        public const int OPCODE_BINDMOBILE_REG_CHECK = 6;
        public const int OPCODE_BINDMOBILE_REG_CHECKED = 7;
        public const int OPCODE_BINDMOBILE_REG_READY = 5;
        private const string TAG = "NetSceneBindOpMobileForReg";


        public void doScene(string mobile)
        {
            Log.i("NetSceneBindOpMobileForReg", "mobile register (1/3), set mobile =" + mobile);
            mMobileNumber = mobile;
            this.doSceneEx(mobile, "", 5, DIALFLAG_NO, "", "");
        }

        public void doSceneBindSafeDevice(string strNum, string strUsrName)
        {
            if (string.IsNullOrEmpty(strNum) || string.IsNullOrEmpty(strUsrName))
            {
                Log.i("NetSceneBindOpMobileForReg", "verify BindSafeDevice fail did not bind mobile = null");
            }
            else
            {
                mMobileNumber = strNum;
                mUserName = strUsrName;
                this.doSceneEx(strNum, "", 10, DIALFLAG_NO, "", strUsrName);
            }
        }

        public void doSceneDialForVerifyCode(string lang)
        {
            
            Log.i("NetSceneBindOpMobileForReg", "mobile register (2/3) ,dial for verify code, lang =" + lang);
            this.doSceneEx(mMobileNumber, "", 5, DIALFLAG_YES, lang, "");
        }

        private void doSceneEx(string mobile, string verifyCode, int opCode, int dialFlag, string dialLang, string strUserName = "")
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            base.mBuilder.UserName = strUserName;
            base.mBuilder.Mobile = mobile;
            base.mBuilder.Opcode = opCode;
            base.mBuilder.Verifycode = verifyCode;
            base.mBuilder.DialFlag = dialFlag;
            base.mBuilder.DialLang = dialLang;
            base.mBuilder.AuthTicket = "";
            if (!string.IsNullOrEmpty(AccountMgr.getCurAccount().strAuthTicket))
            {
                base.mBuilder.AuthTicket = AccountMgr.getCurAccount().strAuthTicket;
            }
            base.mBuilder.ForceReg = 0;
            base.mBuilder.SafeDeviceName = "";//SafeDeviceService.getdeviceName();
            base.mBuilder.SafeDeviceType = "";//SafeDeviceService.getdeviceType();
            base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/bindopmobileforreg";
            base.endBuilder();
        }

        public void doSceneVerify(string verifyCode)
        {
            Log.i("NetSceneBindOpMobileForReg", "mobile register (2/3) , set verify =" + verifyCode);
            this.doSceneEx(mMobileNumber, verifyCode, 6, DIALFLAG_NO, "", "");
        }

        public void doSceneVerifyForBindSafeDevice(string verifyCode)
        {
            Log.i("NetSceneBindOpMobileForReg", "bind mobile  verify (2/2), mobile =" + mMobileNumber);
            this.doSceneEx(mMobileNumber, verifyCode, 11, DIALFLAG_NO, "", mUserName);
        }

        public void getDialForSafeDevVerifyCode(string lang)
        {
            Log.i("NetSceneBindOpMobileForReg", "mobile register SafeDev, dial for verify code, lang =" + lang);
            this.doSceneEx(mMobileNumber, "", 10, DIALFLAG_YES, lang, "");
        }

        protected override void onFailed(BindOpMobileRequest request, BindOpMobileResponse response)
        {
            int opcode = request.Opcode;
            this.postFailedEvent(request, opcode, -800000);
        }

        protected override void onSuccess(BindOpMobileRequest request, BindOpMobileResponse response)
        {
            int opcode = request.Opcode;
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret == RetConst.MM_ERR_IDC_REDIRECT)
            {
                if (mIDCMobileRegCount < 3)
                {
                    mIDCMobileRegCount++;
                    this.doSceneEx(request.Mobile, request.Verifycode, request.Opcode, request.DialFlag, request.DialLang, "");
                    return;
                }
                Log.e("NetSceneBindOpMobileForReg", "Redirect IDC too much, bindmobile failed!");
            }
            mIDCMobileRegCount = 0;
            if (ret != RetConst.MM_OK)
            {
                if (ret == RetConst.MM_ERR_FREQ_LIMITED)
                {
                    Log.e("NetSceneBindOpMobileForReg", string.Concat(new object[] { "error in opCode =", opcode, "  ret =²Ù×÷Æµ·±ÉÔºóÖØÊÔ", ret }));
                }
                Log.e("NetSceneBindOpMobileForReg", string.Concat(new object[] { "error in opCode =", opcode, "  ret =", ret }));
               // this.postFailedEvent(request, opcode, (int) ret);
            }
            else
            {
                mTicket = response.Ticket;
                Log.i("NetSceneBindOpMobileForReg", string.Concat(new object[] { "success. opCode =", opcode, "  ticket = ", mTicket }));
                switch (opcode)
                {
                    case 6:
                        Log.i("NetSceneBindOpMobileForReg", mMobileNumber + "verify success");
                        EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILEREG_VERIFY_SUCCESS, null, null);
                        break;

                    case 10:
                    {
                        Account account = AccountMgr.getCurAccount();
                        account.strAuthTicket = response.AuthTicket;
                        account.nMainAcctType = response.MainAcctType;
                        account.nSafeDevice = response.SafeDevice;
                        Log.d("NetSceneBindOpMobileForReg", "VERIFY_READY MainAcct = " + response.MainAcct);
                        AccountMgr.updateAccount();
                        //SafeDeviceService.saveSafeDeviceList(response.SafeDeviceList);
                        EventCenter.postEvent(EventConst.ON_NETSCENE_BINDMOBILE_NEEDVERIFY, null, null);
                        break;
                    }
                    case 11:
                    {
                        Account account2 = AccountMgr.getCurAccount();
                        account2.strAuthTicket = response.AuthTicket;
                        account2.nMainAcctType = response.MainAcctType;
                        account2.nSafeDevice = response.SafeDevice;
                        Log.d("NetSceneBindOpMobileForReg", "VERIFY_CHECK MainAcct = " + response.MainAcct);
                        AccountMgr.updateAccount();
                        //SafeDeviceService.saveSafeDeviceList(response.SafeDeviceList);
                        AccountMgr.SetOpenSafeDevice(true);
                        EventCenter.postEvent(EventConst.ON_NETSCENE_SAFEDEVICE_VERIFY_SUCCESS, null, null);
                        break;
                    }
                    case 5:
                        if (request.DialFlag == DIALFLAG_YES)
                        {
                            Log.i("NetSceneBindOpMobileForReg", "get voice verify code success,phone =" + mMobileNumber);
                            EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILE_VOICEDIAL_SUCCESS, null, null);
                            return;
                        }
                        Log.i("NetSceneBindOpMobileForReg", "set phone success,phone =" + mMobileNumber);
                        EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_SUCCESS, null, null);
                        break;
                }
                if ((response.SafeDeviceList != null) && (response.SafeDeviceList.Count > 0))
                {
                    Log.i("NetSceneBindOpMobileForReg", "add Local device to safeDeviceList");
                   // SafeDeviceService.saveSafeDeviceList(response.SafeDeviceList);
                }
            }
        }

        private void postFailedEvent(BindOpMobileRequest request, int opCode, int ret)
        {
            if ((opCode == 5) || (opCode == 10))
            {
                if ((request != null) && (request.DialFlag == DIALFLAG_YES))
                {
                    Log.i("NetSceneBindOpMobileForReg", "get voice verify code failed,phone =" + mMobileNumber);
                    EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILE_VOICEDIAL_ERR, ret, null);
                }
                else
                {
                    EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILEREG_SETPHONE_ERR, ret, null);
                }
            }
            else if ((opCode == 6) || (opCode == 11))
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_MOBILEREG_VERIFY_ERR, ret, null);
            }
        }
    }
}

