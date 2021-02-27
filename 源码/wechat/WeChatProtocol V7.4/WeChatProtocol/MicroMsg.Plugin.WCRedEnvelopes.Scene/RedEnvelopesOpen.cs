namespace MicroMsg.Plugin.WCRedEnvelopes.Scene
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Plugin.WCPay;
    using MicroMsg.Plugin.WCPay.Scene;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using micromsg;

    public class RedEnvelopesOpen
    {
        private static NetSceneRedEnvelopes _instance;
        public const string funid = "RedEnvelopesOpen";
        public static Action<WCRedEnvelopesDetailInfo> Callback { get; set; }
        private const string TAG = "RedEnvelopesOpen";

        public static void doCancel()
        {
            if (_instance != null)
            {
                _instance.FailedCallback = null;
                _instance.cancel();
                _instance = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType">1</param>
        /// <param name="channelId">1</param>
        /// <param name="sendId">id</param>
        /// <param name="headImg">Í·Ïñ</param>
        /// <param name="nickName">êÇ³Æ</param>
        /// <param name="nativeUrl">wxpay</param>
        /// <param name="sessionUserName">·¢ºì°üusername</param>
        /// <param name="failedCallback"></param>
        public static void doScene(int msgType, int channelId, string sendId, string headImg, string nickName, string nativeUrl, string sessionUserName, NetSceneRedEnvelopesCallback failedCallback = null)
        {
            if (_instance != null)
            {
                _instance.FailedCallback = null;
                _instance.cancel();
                _instance = null;
            }
            //if (Callback != null)
            //{
            //    Callback = null;
            //    //Callback = 
            //}
            doSceneEx(msgType, channelId, sendId, headImg, nickName, nativeUrl, sessionUserName, failedCallback);
        }

        private static void doSceneEx(int msgType, int channelId, string sendId, string headImg, string nickName, string nativeUrl, string sessionUserName, NetSceneRedEnvelopesCallback failedCallback)
        {
            _instance = new NetSceneRedEnvelopes("RedEnvelopesOpen");
            _instance.FailedCallback = failedCallback;
            Log.i("RedEnvelopesOpen", "doScene");
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("msgType", msgType);
            queryDic.Add("channelId", channelId);
            queryDic.Add("sendId", sendId);
            queryDic.Add("headImg", headImg);
            queryDic.Add("nickName", nickName);
            queryDic.Add("nativeUrl", nativeUrl);
            queryDic.Add("sessionUserName", sessionUserName);
            _instance.doScene(queryDic, 4, 1);
        }

        public static void onResponse(Dictionary<string, object> tenPayResponsRetString, HongBaoReq tenPayRequest)
        {
            _instance = null;
            WCRedEnvelopesDetailInfo info = new WCRedEnvelopesDetailInfo
            {
                m_enWCRedEnvelopesType = (ENWCRedEnvelopesType)PayUtils.getSafeInt(tenPayResponsRetString, "hbType"),
                m_enWCRedEnvelopesStatus = (ENWCRedEnvelopesStatus)PayUtils.getSafeInt(tenPayResponsRetString, "hbStatus"),
                m_enWCRedEnvelopesUserReceiveStatus = (ENWCRedEnvelopesUserReceiveStatus)PayUtils.getSafeInt(tenPayResponsRetString, "receiveStatus"),
                m_nsStatusMessage = PayUtils.getSafeValue(tenPayResponsRetString, "statusMess"),
                m_nsGameMessage = PayUtils.getSafeValue(tenPayResponsRetString, "gameMess"),
                m_bIsSender = PayUtils.getSafeInt(tenPayResponsRetString, "isSender") > 0,
                m_nsWishing = PayUtils.getSafeValue(tenPayResponsRetString, "wishing"),
                m_nsSendNickName = PayUtils.getSafeValue(tenPayResponsRetString, "sendNick"),
                m_nsSendHeadImg = PayUtils.getSafeValue(tenPayResponsRetString, "sendHeadImg"),
                m_nsSendId = PayUtils.getSafeValue(tenPayResponsRetString, "sendId"),
                m_nsAdMessage = PayUtils.getSafeValue(tenPayResponsRetString, "adMessage"),
                m_nsAdUrl = PayUtils.getSafeValue(tenPayResponsRetString, "adUrl"),
                m_lAmount = PayUtils.getSafeLong(tenPayResponsRetString, "amount"),
                m_lRecNum = PayUtils.getSafeLong(tenPayResponsRetString, "recNum"),
                m_lRecAmount = PayUtils.getSafeLong(tenPayResponsRetString, "recAmount"),
                m_lTotalNum = PayUtils.getSafeLong(tenPayResponsRetString, "totalNum"),
                m_lTotalAmount = PayUtils.getSafeLong(tenPayResponsRetString, "totalAmount"),
                m_nsReceiveId = PayUtils.getSafeValue(tenPayResponsRetString, "receiveId"),
                m_bHasWriteThanks = PayUtils.getSafeInt(tenPayResponsRetString, "hasWriteAnswer") > 0,
                m_bAllowJumpToBalance = PayUtils.getSafeInt(tenPayResponsRetString, "jumpChange") > 0,
                m_nsJumpBalanceDesc = PayUtils.getSafeValue(tenPayResponsRetString, "changeWording"),
                m_nsHeadTitle = PayUtils.getSafeValue(tenPayResponsRetString, "headTitle"),
                m_bHasMoreReceiver = PayUtils.getSafeInt(tenPayResponsRetString, "isContinue") > 0,
                m_bCanShareHB = PayUtils.getSafeInt(tenPayResponsRetString, "canShare") > 0,
                m_nsWaterMarkUrl = PayUtils.getSafeValue(tenPayResponsRetString, "watermark"),
                m_nsExternMessage = PayUtils.getSafeValue(tenPayResponsRetString, "externMess")
            };
            List<WCRedEnvelopesReceiverInfo> observables = new List<WCRedEnvelopesReceiverInfo>();
            foreach (Dictionary<string, object> dictionary in PayUtils.getSafeArray(tenPayResponsRetString, "record"))
            {
                WCRedEnvelopesReceiverInfo receiverInfoFromDictionary = RedEnvelopesUtils.GetReceiverInfoFromDictionary(dictionary, info.m_nsReceiveId);
                if (receiverInfoFromDictionary != null)
                {
                    observables.Add(receiverInfoFromDictionary);
                }
            }
            info.m_arrReceiveList = observables;
            if (Callback != null)
            {
                Callback(info);
            }
            //PayEventService.post("RedEnvelopesOpen", 0, "", info, null, 0, 0);
        }

        public static void ReceiverQueryRedEnvelopes(string channelId, string sendId, string nativeUrl, NetSceneRedEnvelopesCallback failedCallback = null)
        {
            _instance = new NetSceneRedEnvelopes("RedEnvelopesQueryRequest", "hongbao");
            if (failedCallback != null)
            {
                _instance.FailedCallback = failedCallback;
            }
            Log.i("RedEnvelopesQueryRequest", "doScene");
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("msgType", 1);
            queryDic.Add("channelId", channelId);
            queryDic.Add("sendId", sendId);
            queryDic.Add("nativeUrl", nativeUrl);
            // queryDic.Add("offset",1);
            //queryDic.Add("limit", 11);


            queryDic.Add("inWay", 0);
            _instance.doScene(queryDic, 5, 1);
        }

    }
}

