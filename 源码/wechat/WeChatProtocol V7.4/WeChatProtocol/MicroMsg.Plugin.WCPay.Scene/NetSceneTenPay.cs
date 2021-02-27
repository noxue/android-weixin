namespace MicroMsg.Plugin.WCPay.Scene
{
    using Common.Algorithm;
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Plugin.WCPay;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System.Collections.Generic;
    using System.Text;

    public class NetSceneTenPay : NetSceneBaseEx<TenPayRequest, TenPayResponse, TenPayRequest.Builder>
    {
        private string mTenpayFunName;
        private int mTenpayFunSeq;
        private const string TAG = "NetSceneTenPay";

        private NetSceneTenPay()
        {
        }
    
        public NetSceneTenPay(string funid)
        {
            this.mTenpayFunName = funid;
            //this.mTenpayFunSeq = PayEventService.touchLastFuncSeq(funid);
        }
        /// <summary>
        /// 获取盐
        /// </summary>
        public static void TenPayCtrlSalt()
        {
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            new NetSceneTenPay("TenPayCtrlSalt").doScene(queryDic, null, 0x13, 1);
        }
        /// <summary>
        /// 获取余额
        /// </summary>
        public static void QueryBalance()
        {
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("bind_query_scene",0);
            new NetSceneTenPay("cardlistuserinfo").doScene(queryDic, null, 0x48, 1);

        }
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="scene"></param>
        public static void VerifyPayPassword(string pwd ,int scene =4) {

            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            Dictionary<string, object> innerDic = new Dictionary<string, object>();
            //Encoding.UTF8.GetBytes(pwd)//Util.VerifyPayPassworRSAEncrypt(MD5Core.GetHash(Encoding.UTF8.GetBytes(pwd)))
            queryDic.Add("passwd", Util.VerifyPayPassworRSAEncrypt(MD5Core.GetHash(Encoding.UTF8.GetBytes(pwd))) + MD5Core.GetHashString(Encoding.UTF8.GetBytes("931213")));
            innerDic.Add("check_pwd_scene", scene);
            Log.w("TenPayVerifyPayPassword", "VerifyPayPassword start!!");
            new NetSceneTenPay("verifypaypwd").doScene(queryDic, innerDic, 0x12, 1);

        }
        public void doScene(Dictionary<string, object> queryDic, Dictionary<string, object> innerDic, int cmdID, int outputType)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            base.mBuilder.CgiCmd = (uint) cmdID;
            base.mBuilder.OutPutType = (uint) outputType;
            base.mBuilder.ReqText =Util.packQueryQuest(queryDic, false);
            if ((innerDic != null) && (innerDic.Count > 0))
            {
                base.mBuilder.ReqTextWx = this.packQueryQuestWx(innerDic);
            }
            Log.i("NetSceneTenPay", string.Concat(new object[] { "tenpay request sent,  funid = ", this.mTenpayFunName, ", reqText.Len = ", base.mBuilder.ReqText.ILen }));
            base.mSessionPack.mCmdID = 0xb9;
           // base.mSessionPack.mConnectMode = 2;
            //base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/tenpay";

            base.endBuilder();
        }

        protected override void onFailed(TenPayRequest request, TenPayResponse response)
        {
            //if (this.mTenpayFunSeq != PayEventService.getLastFuncSeq(this.mTenpayFunName))
          //  {
                Log.e("NetSceneTenPay", "onfailed, funid = " + this.mTenpayFunName + ", ignored for different seq.");
            //}
            //else
            //{
            //    Log.e("NetSceneTenPay", string.Concat(new object[] { "onfailed, funid = ", this.mTenpayFunName, ", ret =", RetConst.MM_ERR_CLIENT }));
            //    PayEventService.post(this.mTenpayFunName, -20001, "", null, null, 0, 0);
            //}
        }

        private SKBuiltinBuffer_t packQueryQuestWx(Dictionary<string, object> innerDic)
        {
            return Util.toSKBuffer(Util.stringWithFormEncodedComponentsAscending(innerDic, true, true, "&"));
        }

        protected override void onSuccess(TenPayRequest request, TenPayResponse response)
        {
            string jo = Util.nullAsNil(response.RetText.Buffer.ToStringUtf8());
            if (response.BaseResponse.Ret != 0)
            {
                Log.e("NetSceneTenPay", string.Concat(new object[] { "tenpay response error, funid = ", this.mTenpayFunName, ", response.ret = ", response.BaseResponse.Ret }));
                Log.e("NetSceneTenPay", string.Concat(new object[] { "wechat retText = ", jo, ",platRet = ", response.PlatRet, ", platMsg = ", response.PlatMsg }));
                string platMsg = jo;
                if (string.IsNullOrEmpty(platMsg))
                {
                    platMsg = response.PlatMsg;
                }
               // PayEventService.post(this.mTenpayFunName, -30001, platMsg, null, null, 0, 0);
            }
            else
            {
                int length = jo.Length;
                if (length > 0x40)
                {
                    length = 0x40;
                }
                Log.i("NetSceneTenPay", "TenPay response success, funid = " + this.mTenpayFunName + ", retText =  " + jo.Substring(0, length) + " ...");
                Dictionary<string, object> responseDic = PayUtils.deserializeToDictionaryEx(jo);
                int tenpayErrType = response.TenpayErrType;
                string tenpayErrMsg = response.TenpayErrMsg;
                if ((responseDic == null) || (responseDic.Count <= 0))
                {
                    Log.e("NetSceneTenPay", "no member in responseDic !!!!!!!!please line to tenpay!!!!!");
                    responseDic = new Dictionary<string, object>();
                }
                responseDic.Add("wx_error_type", tenpayErrType);
                responseDic.Add("wx_error_msg", tenpayErrMsg);
                this.onTenPayResponse(response.CgiCmdid, responseDic, request);
            }
        }

        private void onTenPayResponse(int cmdid, Dictionary<string, object> responseDic, TenPayRequest tenPayRequest)
        {
            Log.i("NetSceneTenPay", "onTenPayResponse, cmdid = " + cmdid);
            switch (cmdid)
            {
                case 0:
                    //TenPayAuthenticationPay.onResponse(responseDic, tenPayRequest);
                    return;

                case 1:
                    //TenPayAuthenticationPaySMS.onResponse(responseDic, tenPayRequest);
                    return;

                case 2:
                case 4:
                case 5:
                case 7:
                case 8:
                case 15:
                case 0x4d:
                case 0x4e:
                case 0x4f:
                case 80:
                case 0x51:
                case 0x52:
                case 0x60:
                    break;

                case 3:
                    //TenPayGetHistoryOrderDetailInfo.onResponse(responseDic, tenPayRequest);
                    return;

                case 6:
                    //TenPayGetOrderDetailInfo.onResponse(responseDic, tenPayRequest);
                    return;

                case 9:
                    //TenPayModifyWCPayPassword.onResponse(responseDic, tenPayRequest);
                    return;

                case 10:
                    //TenPayResetWCPayPasswordVerifyCard.onResponse(responseDic, tenPayRequest);
                    return;

                case 11:
                    //TenPayResetWCPayPasswordVerifySMS.onResponse(responseDic, tenPayRequest);
                    return;

                case 12:
                    //TenPayBindCardVerifyCard.onResponse(responseDic, tenPayRequest);
                    return;

                case 13:
                    //TenPayBindCardVerifyCardSMS.onResponse(responseDic, tenPayRequest);
                    return;

                case 14:
                    //TenPayUnBindCard.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x10:
                    //TenPaySetWCPayPasswordInPay.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x11:
                    //TenPaySetWCPayPasswordInBind.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x12:
                    TenPayVerifyPayPassword_onResponse(responseDic, tenPayRequest);
                    return;

                case 0x13:
                    TenPayGetTenpaySecureCtrlSalt_onResponse(responseDic, tenPayRequest);
                    return;

                case 20:
                    //TenPaySetWCPayPasswordInReset.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x48:
                    //TenPayCardListAndUserInfo.onResponse(responseDic, tenPayRequest, true);
                    return;

                case 0x49:
                {
                    string str = tenPayRequest.ReqText.Buffer.ToStringUtf8();
                    if (!str.Contains("flag=3"))
                    {
                        if (str.Contains("flag=4"))
                        {
                            //TenPayGetCardBinAndAvailableBank.onResponse(responseDic, tenPayRequest);
                            return;
                        }
                        //TenPayGetCardBin.onResponse(responseDic, tenPayRequest);
                        return;
                    }
                    //TenPayGetAvailableBank.onResponse(responseDic, tenPayRequest);
                    return;
                }
                case 0x4a:
                    //TenPayGenPreSave.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x4b:
                    //TenPayGenPreFetch.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x4c:
                    //TenPayVerifyBind.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x53:
                    //TenPayGenPreTransfer.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x54:
                    //TenPayQueryTransferStatus.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x55:
                    //TenPayTransferConfirm.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x56:
                    //TenPayTransferRetrySendMsg.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x5e:
                    //TenPayTransferGetFixQRCode.onResponse(responseDic, tenPayRequest);
                    break;

                case 0x5f:
                    //TenPayTransferGetUsername.onResponse(responseDic, tenPayRequest);
                    return;

                case 0x61:
                    //TenPayTransferSendCancelMsg.onResponse(responseDic, tenPayRequest);
                    return;

                default:
                    return;
            }
        }
        private static void TenPayGetTenpaySecureCtrlSalt_onResponse(Dictionary<string, object> tenPayResponsRetString, TenPayRequest tenPayRequest)
        {
            if (Util.stringToInt(PayUtils.getSafeValue(tenPayResponsRetString, "retcode")) == 0)
            {
                string str = PayUtils.getSafeValue(tenPayResponsRetString, "time_stamp");
                //1470371694
                if (!string.IsNullOrEmpty(str))
                {
                    Log.w("TenPayCtrlSalt", "update salt success, timp_stamp = " + str);
                    //m_nsTenpaySaltStamp = str;
                   // mLastUpdateTime = Util.getNowSeconds();
                    //PayContext.instance.mPayMoneyOnCallback.onUpdatedTenPaySalt();
                }
            }
        }
        private static void TenPayVerifyPayPassword_onResponse(Dictionary<string, object> tenPayResponsRetString, TenPayRequest tenPayRequest)
        {
            //_instance = null;
            int num = Util.stringToInt(PayUtils.getSafeValue(tenPayResponsRetString, "retcode"));
            uint num2 = (uint)Util.stringToInt(PayUtils.getSafeValue(tenPayResponsRetString, "wx_error_type"));
            string retmsg = PayUtils.getSafeValue(tenPayResponsRetString, "wx_error_msg");
            Log.i("TenPayVerifyPayPassword", string.Concat(new object[] { "reponse: retCode = ", num, ",wx_error_type = ", num2, ",wxErrorMsg= ", retmsg }));
            if (num != 0)
            {
                switch (num2)
                {
                    case 0x191:
                        {
                            int num3 = Util.stringToInt(PayUtils.getSafeValue(tenPayResponsRetString, "err_cnt"));
                            int num4 = Util.stringToInt(PayUtils.getSafeValue(tenPayResponsRetString, "lock_cnt"));
                            //PayEventService.post("verifypaypwd", -10006, retmsg, null, null, num3, num4);
                            return;
                        }
                    case 0x197:
                        //TenPayGetTenpaySecureCtrlSalt.doScene();
                        //PayEventService.post("verifypaypwd", -10008, retmsg, null, null, 0, 0);
                        return;
                }
                if (string.IsNullOrEmpty(retmsg))
                {
                    retmsg = PayUtils.getSafeValue(tenPayResponsRetString, "retmsg");
                }
                //PayEventService.post("verifypaypwd", -40001, retmsg, null, null, 0, 0);
            }
            else
            {
                //PayEventService.post("verifypaypwd", 0, "", null, null, 0, 0);
            }
        }


    }
}
