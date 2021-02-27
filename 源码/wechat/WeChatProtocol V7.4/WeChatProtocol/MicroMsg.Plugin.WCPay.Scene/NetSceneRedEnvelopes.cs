namespace MicroMsg.Plugin.WCPay.Scene
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Plugin.WCPay;
    using MicroMsg.Plugin.WCRedEnvelopes.Scene;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using micromsg;

    public class NetSceneRedEnvelopes : NetSceneBaseEx<HongBaoReq, HongBaoRes, HongBaoReq.Builder>
    {
        public NetSceneRedEnvelopesCallback FailedCallback;
        private string mCmdUri;
        private string mTenpayFunName;
        public object Tag;
        private const string TAG = "NetSceneRedEnvelopes";
        //businesshongbao
        private NetSceneRedEnvelopes()
        {
        }

        public NetSceneRedEnvelopes(string funid, string cmdUri = "/cgi-bin/mmpay-bin/hongbao")
        {
            //this.mTenpayFunName = funid;
            //this.mTenpayFunSeq = PayEventService.touchLastFuncSeq(funid);
            this.mCmdUri = cmdUri;
        }

        public NetSceneRedEnvelopes(string funid, NetSceneRedEnvelopesCallback callback, string cmdUri = "hongbao")
        {
            this.mTenpayFunName = funid;
            //this.mTenpayFunSeq = "";//PayEventService.touchLastFuncSeq(funid);
            this.mCmdUri = "/cgi-bin/mmpay-bin/hongbao";
        }

        public void doScene(Dictionary<string, object> queryDic, int cmdID, int outputType)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            base.mBuilder.CgiCmd = (uint)cmdID;
            base.mBuilder.OutPutType = (uint)outputType;
            base.mBuilder.ReqText = Util.packQueryQuest(queryDic, true);
            //Log.w("NetSceneRedEnvelopes", string.Concat(new object[] { "hongbao request sent,  funid = ", this.mTenpayFunName, ", reqText.Len = ", base.mBuilder.ReqText.ILen }));
            base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mCmdUri = "/cgi-bin/mmpay-bin/hongbao";
            base.endBuilder();
        }

        public void doScene(string str)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.CgiCmd = 4;
            base.mBuilder.OutPutType = 1;
            base.mBuilder.ReqText = Util.toSKBuffer(str);// Util.packQueryQuest(queryDic, true);
            Log.i("NetSceneRedEnvelopes", string.Concat(new object[] { "hongbao request sent,  Encrpt = ", str, ", reqText.Len = ", base.mBuilder.ReqText.ILen }));
            base.mSessionPack.mConnectMode = 1;
            base.mSessionPack.mCmdUri = "/cgi-bin/mmpay-bin/businesshongbao";
            base.endBuilder();
        }
        protected override void onFailed(HongBaoReq request, HongBaoRes response)
        {
            //if (this.mTenpayFunSeq != PayEventService.getLastFuncSeq(this.mTenpayFunName))
            //{
            //    Log.e("NetSceneRedEnvelopes", "onfailed, funid = " + this.mTenpayFunName + ", ignored for different seq.");
            //}
            //else
            //{
            Log.e("NetSceneRedEnvelopes", string.Concat(new object[] { "onfailed, funid = ", this.mTenpayFunName, ", ret =", RetConst.MM_ERR_CLIENT }));
            //if (this.FailedCallback != null)
            //{
            //    this.FailedCallback(-20001, strings.WCPay_UnKnown_Error, this.Tag);
            //}
            //else
            //{
            //    PayContext.instance.mPayMoneyOnCallback.onFailedinRedEnvelopes(this.mTenpayFunName, -20001, strings.WCPay_UnKnown_Error, this.Tag);
            //}
            //  }
        }

        protected override void onSuccess(HongBaoReq request, HongBaoRes response)
        {
            string jo = Util.nullAsNil(response.RetText.Buffer.ToStringUtf8());
            Log.w("LuckMoney", string.Concat(new object[] { "hongbao response success" ,"\n", jo }));

            if (response.BaseResponse.Ret != 0)
            {
                Log.e("NetSceneRedEnvelopes", string.Concat(new object[] { "tenpay response error, funid = ", this.mTenpayFunName, ", response.ret = ", response.BaseResponse.Ret }));
                Log.e("NetSceneRedEnvelopes", string.Concat(new object[] { "wechat retText = ", jo, ",platRet = ", response.PlatRet, ", platMsg = ", response.PlatMsg }));
                //string platMsg = jo;
                //if (string.IsNullOrEmpty(platMsg))
                //{
                //    platMsg = response.PlatMsg;
                //}
                //if (this.FailedCallback != null)
                //{
                //    this.FailedCallback(-20001, "", this.Tag);
                //}
                //else
                //{
                //    PayContext.instance.mPayMoneyOnCallback.onFailedinRedEnvelopes(this.mTenpayFunName, response.BaseResponse.Ret, platMsg, this.Tag);
                //}
            }
            else
            {
                int length = jo.Length;
                if (length > 0x40)
                {
                    length = 0x40;
                }
                Log.i("NetSceneRedEnvelopes", "TenPay response success, funid = " + this.mTenpayFunName + ", retText =  " + jo.Substring(0, length) + " ...");
                Dictionary<string, object> responseDic = PayUtils.deserializeToDictionaryEx(jo);
                int errorType = response.ErrorType;
                string errorMsg = response.ErrorMsg;
                if ((responseDic == null) || (responseDic.Count <= 0))
                {
                    Log.e("NetSceneRedEnvelopes", "no member in responseDic !!!!!!!!please line to tenpay!!!!!");
                    responseDic = new Dictionary<string, object>();
                }
                responseDic.Add("wx_error_type", errorType);
                responseDic.Add("wx_error_msg", errorMsg);
                this.onTenPayResponse(response.CgiCmdid, responseDic, request);
            }
        }

        private void onTenPayResponse(int cmdid, Dictionary<string, object> responseDic, HongBaoReq tenPayRequest)
        {
            Log.i("NetSceneRedEnvelopes", "onTenPayResponse, cmdid = " + cmdid);
            int num = PayUtils.getSafeInt(responseDic, "retcode");
            uint num2 = PayUtils.getSafeUInt(responseDic, "wx_error_type");
            string str = PayUtils.getSafeValue(responseDic, "wx_error_msg");
            Log.i("NetSceneRedEnvelopes", string.Concat(new object[] { "onResponse: retCode = ", num, ",wx_error_type = ", num2, ",wxErrorMsg= ", str }));
            if (num != 0)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = PayUtils.getSafeValue(responseDic, "retmsg");
                }
                if (this.FailedCallback != null)
                {
                    this.FailedCallback((int)num2, str, this.Tag);
                }
                else
                {
                    //PayContext.instance.mPayMoneyOnCallback.onFailedinRedEnvelopes(this.mTenpayFunName, (int) num2, str, this.Tag);
                }
            }
            else if (this.mCmdUri == "/cgi-bin/mmpay-bin/hongbao")
            {
                switch (cmdid)
                {
                    case 0:
                        //RedEnvelopesQueryUserInfo.onResponse(responseDic, tenPayRequest);
                        return;

                    case 1:
                        // RedEnvelopesPayRequest.onResponse(responseDic, tenPayRequest);
                        return;

                    case 2:
                        // RedEnvelopesSendAppMsg.onResponse(responseDic, tenPayRequest);
                        return;

                    case 3:
                        //RedEnvelopesQueryRequest.onResponse(responseDic, tenPayRequest);
                        return;

                    case 4:
                        Log.w("LuckMoney", string.Concat(new object[] { "hongbao response success" }));
                        RedEnvelopesOpen.onResponse(responseDic, tenPayRequest);
                        return;

                    case 5:
                        // RedEnvelopesQueryDetail.onResponse(responseDic, tenPayRequest);
                        return;

                    case 6:
                        //  RedEnvelopesQueryList.onResponse(this, responseDic, tenPayRequest);
                        return;

                    case 7:
                        return;

                    case 8:
                        //RedEnvelopesThanks.onResponse(responseDic, tenPayRequest);
                        return;
                }
            }
            else if (this.mCmdUri == "businesshongbao")
            {
                switch (cmdid)
                {
                    case 0:
                        //RedEnvelopesBizQueryRequest.onResponse(responseDic, tenPayRequest);
                        return;

                    case 1:
                        //RedEnvelopesBizOpen.onResponse(responseDic, tenPayRequest);
                        return;

                    case 2:
                        // RedEnvelopesBizCheckAuthRequest.onResponse(responseDic, tenPayRequest);
                        break;

                    default:
                        return;
                }
            }
        }
    }



}

