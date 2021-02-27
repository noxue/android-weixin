namespace MicroMsg.Plugin.WCRedEnvelopes.Scene
{
    using MicroMsg.Manager;
    using MicroMsg.Plugin.WCPay;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;

    public class RedEnvelopesUtils
    {
        public static string ConvertMoneyToString(long amount)
        {
            double num = ((double) amount) / 100.0;
            return num.ToString("f2");
        }

        //public static string GetAppMsgContent(ChatMsg msg, AppMsgInfo msgInfo)
        //{
        //    if ((msg == null) || (msgInfo.payinfoitem == null))
        //    {
        //        return string.Format("[{0}]", strings.WCRedEnvelopes_WelcomePage_Title);
        //    }
        //    string str = string.Format("[{0}]", strings.WCRedEnvelopes_WelcomePage_Title);
        //    if (msg.IsSender())
        //    {
        //        return (str + msgInfo.payinfoitem.m_senderTitle);
        //    }
        //    return (str + msgInfo.payinfoitem.m_receiverTitle);
        //}

        public static WCRedEnvelopesAtomicInfo GetAtomicInfoFromDictionary(Dictionary<string, object> dict)
        {
            if (dict != null)
            {
                return new WCRedEnvelopesAtomicInfo { m_bEnable = PayUtils.getSafeInt(dict, "enable") > 0, m_nsAtomicUrl = PayUtils.getSafeValue(dict, "fissionUrl"), m_nsAtomicTitle = PayUtils.getSafeValue(dict, "fissionContent"), m_nsSessionKey = PayUtils.getSafeLong(dict, "sessionKey") };
            }
            return null;
        }

        public static WCRedEnvelopesOperationInfo GetOperationsInfoFromDictionary(Dictionary<string, object> dict)
        {
            if (dict != null)
            {
                return new WCRedEnvelopesOperationInfo { m_bOpEnable = PayUtils.getSafeInt(dict, "enable") > 0, m_nsOpName = PayUtils.getSafeValue(dict, "name"), m_nsOpType = PayUtils.getSafeValue(dict, "type"), m_nsOpContent = PayUtils.getSafeValue(dict, "content"), m_nsOssKey = PayUtils.getSafeLong(dict, "ossKey") };
            }
            return null;
        }

        public static WCRedEnvelopesReceiverInfo GetReceiverInfoFromDictionary(Dictionary<string, object> dict, string receiveId)
        {
            if (dict == null)
            {
                return null;
            }
            WCRedEnvelopesReceiverInfo info = new WCRedEnvelopesReceiverInfo {
                m_nsReceiverName = PayUtils.getSafeValue(dict, "receiveName"),
                m_nsReceiverHeadImg = PayUtils.getSafeValue(dict, "receiveHeadImg"),
                m_lReceiveAmount = PayUtils.getSafeLong(dict, "receiveAmount"),
                m_uiReceiveTime = PayUtils.getSafeLong(dict, "receiveTime"),
                m_nsWishing = PayUtils.getSafeValue(dict, "answer"),
                m_nsReceiverGameTips = PayUtils.getSafeValue(dict, "gameTips"),
                m_nsReceiveId = PayUtils.getSafeValue(dict, "receiveId"),
                m_nsReceiverUserName = PayUtils.getSafeValue(dict, "userName")
            };
            if (!string.IsNullOrEmpty(receiveId))
            {
                info.m_bIsMySelf = receiveId.Equals(info.m_nsReceiveId);
                return info;
            }
            info.m_bIsMySelf = false;
            return info;
        }

        public enum ENWCRedEnvelopesKind : uint
        {
            ENWCRedEnvelopesTypeDefaultNormalKind = 1,
            ENWCRedEnvelopesTypeEnterpriceseKind = 2,
            ENWCRedEnvelopesTypeSpringKind = 3
        }

        public enum ENWCRedEnvelopesType : uint
        {
            ENWCRedEnvelopesTypeDefaultGroupType = 1,
            ENWCRedEnvelopesTypeDefaultNormalType = 0
        }
    }
}

