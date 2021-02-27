namespace MicroMsg.Manager
{
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using MicroMsg.Common.Utils;
    public class WCPayInfoItem
    {
        public int m_c2c_msg_subtype;
        public string m_c2cIconUrl = "";
        public string m_c2cNativeUrl = "";
        public string m_c2cUrl = "";
        public string m_hintText = "";
        public string m_nsFeeDesc = "";
        public string m_nsTranscationID = "";
        public string m_nsTransferID = "";
        public string m_receiverDesc = "";
        public string m_receiverTitle = "";
        public int m_sceneId;
        public string m_sceneText = "";
        public string m_senderDesc = "";
        public string m_senderTitle = "";
        public uint m_uiBeginTransferTime;
        public uint m_uiEffectiveDate;
        public uint m_uiInvalidTime;
        public uint m_uiPaySubType;

        public static WCPayInfoItem fromXElement(XElement nodeappmsg)
        {
            WCPayInfoItem item = new WCPayInfoItem();
            if (nodeappmsg != null)
            {
                XElement element = nodeappmsg.Element("wcpayinfo");
                if (element == null)
                {
                    return item;
                }
                XElement element2 = element.Element("paysubtype");
                if (element2 != null)
                {
                    item.m_uiPaySubType = uint.Parse(element2.Value);
                }
                element2 = element.Element("feedesc");
                if (element2 != null)
                {
                   // item.m_nsFeeDesc = PayService.SafeReplaceCNY(element2.Value);
                }
                element2 = element.Element("transcationid");
                if (element2 != null)
                {
                    item.m_nsTranscationID = element2.Value;
                }
                element2 = element.Element("transferid");
                if (element2 != null)
                {
                    item.m_nsTransferID = element2.Value;
                }
                element2 = element.Element("invalidtime");
                if (element2 != null)
                {
                    item.m_uiInvalidTime = uint.Parse(element2.Value);
                }
                element2 = element.Element("begintransfertime");
                if (element2 != null)
                {
                    item.m_uiBeginTransferTime = uint.Parse(element2.Value);
                }
                element2 = element.Element("effectivedate");
                if (element2 != null)
                {
                    item.m_uiEffectiveDate = uint.Parse(element2.Value);
                }
                element2 = element.Element("url");
                if (element2 != null)
                {
                    item.m_c2cUrl = element2.Value;
                }
                element2 = element.Element("iconurl");
                if (element2 != null)
                {
                    item.m_c2cIconUrl = element2.Value;
                }
                element2 = element.Element("receivertitle");
                if (element2 != null)
                {
                    item.m_receiverTitle = element2.Value;
                }
                element2 = element.Element("sendertitle");
                if (element2 != null)
                {
                    item.m_senderTitle = element2.Value;
                }
                element2 = element.Element("scenetext");
                if (element2 != null)
                {
                    item.m_sceneText = element2.Value;
                }
                element2 = element.Element("senderdes");
                if (element2 != null)
                {
                    item.m_senderDesc = element2.Value;
                }
                element2 = element.Element("receiverdes");
                if (element2 != null)
                {
                    item.m_receiverDesc = element2.Value;
                }
                element2 = element.Element("nativeurl");
                if (element2 != null)
                {
                    item.m_c2cNativeUrl = element2.Value;
                }
                element2 = element.Element("sceneid");
                if (element2 != null)
                {
                    item.m_sceneId = int.Parse(element2.Value);
                }
                element2 = element.Element("innertype");
                if (element2 != null)
                {
                    item.m_c2c_msg_subtype = int.Parse(element2.Value);
                }
            }
            return item;
        }

        public static bool IsWCPayInfoType(AppMsgInfo appMsg)
        {
            if ((appMsg == null) || ((appMsg.type != 0x7d0) && (appMsg.type != 0x7d1)))
            {
                return false;
            }
            return true;
        }

        //public static bool IsWCPayInfoType(ChatMsg chatMsg)
        //{
        //    return (IsWCPayInfoTypePossible(chatMsg.strContent) && IsWCPayInfoType(AppMsgMgr.ParseAppXml(chatMsg.strContent)));
        //}

        public static bool IsWCPayInfoTypePossible(string xml)
        {
            return (!string.IsNullOrEmpty(xml) && xml.Contains("<wcpayinfo>"));
        }

        public Dictionary<string, string> m_c2cNativeUrlQueryDict
        {
            get
            {
                return Util.UrlToDictionay(this.m_c2cNativeUrl);
            }
        }
    }
    public class AppMsgInfo
    {
        public string action;
        public string aeskey;
        public string appid;
        public string attachid;
        public string cdnattachurl;
        public int cdnthumbheight;
        public int cdnthumblength;
        public string cdnthumburl;
        public int cdnthumbwidth;
        public string content;
        public string dataurl;
        public string description;
        public string emoticonmd5;
        public string encryver;
        public string extinfo;
        public string fileext;
        public string fromUserName;
        public string httpthumburl;
        public int itemType;
        public string lowdataurl;
        public string lowurl;
        public MMReaderInfo mmreader;
        public int nChatMsgLocalID;
        public uint nMsgLocalID;
        public int packageFlag;
        public string packageId;
        public WCPayInfoItem payinfoitem;
        public string sdkVer;
        public int showtype;
        public string sourcedisplayname;
        public string sourceusername;
        public string title;
        public int totallength;
        public int type;
        public string url;

        public void GetDataFrom(object item)
        {
            AppMsgInfo info = item as AppMsgInfo;
            if (info != null)
            {
                this.appid = info.appid;
                this.sdkVer = info.sdkVer;
                this.title = info.title;
                this.description = info.description;
                this.action = info.action;
                this.type = info.type;
                this.showtype = info.showtype;
                this.content = info.content;
                this.url = info.url;
                this.lowurl = info.lowurl;
                this.dataurl = info.dataurl;
                this.lowdataurl = info.lowdataurl;
                this.totallength = info.totallength;
                this.attachid = info.attachid;
                this.fileext = info.fileext;
                this.extinfo = info.extinfo;
                this.fromUserName = info.fromUserName;
                this.mmreader = info.mmreader;
                this.nChatMsgLocalID = info.nChatMsgLocalID;
                this.itemType = info.itemType;
                this.cdnattachurl = info.cdnattachurl;
                this.cdnthumburl = info.cdnthumburl;
                this.cdnthumblength = info.cdnthumblength;
                this.cdnthumbheight = info.cdnthumbheight;
                this.cdnthumbwidth = info.cdnthumbwidth;
                this.aeskey = info.aeskey;
                this.encryver = info.encryver;
                this.nMsgLocalID = info.nMsgLocalID;
                this.sourceusername = info.sourceusername;
                this.sourcedisplayname = info.sourcedisplayname;
                this.httpthumburl = info.httpthumburl;
                this.packageId = info.packageId;
                this.packageFlag = info.packageFlag;
            }
        }
    }
}
