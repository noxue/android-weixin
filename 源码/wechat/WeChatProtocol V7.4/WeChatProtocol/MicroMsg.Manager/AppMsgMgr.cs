namespace MicroMsg.Manager
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using MicroMsg.Scene;
    //using MicroMsg.Storage;
    //using MicroMsg.UI.UserContrl;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Xml.Linq;
    using MicroMsg.Storage;

    public class AppMsgMgr
    {
        public const string TAG = "AppMsgMgr";

        public static string addXmlNode(string fromStrXml, List<MsgXmlNode> nodeList)
        {
            if ((string.IsNullOrEmpty(fromStrXml) || (nodeList == null)) || (nodeList.Count <= 0))
            {
                return null;
            }
            try
            {
                XDocument document = XDocument.Parse(fromStrXml);
                foreach (MsgXmlNode node in nodeList)
                {
                    foreach (XElement element in document.Descendants(node.parentName))
                    {
                        if (element.Element(node.Name) == null)
                        {
                            element.SetElementValue(node.Name, node.Value);
                        }
                    }
                }
                return document.ToString();
            }
            catch (Exception exception)
            {
                Log.e("AppMsgMgr", exception.Message);
                return null;
            }
        }

        public static AppMusicInfo getMusicInfo(ChatMsg msg)
        {
            if (msg == null)
            {
                return null;
            }
            AppMsgInfo info = ParseAppXml(msg.strMsg);
            if (info == null)
            {
                return null;
            }
            if (info.type != 3)
            {
                return null;
            }
            if (string.IsNullOrEmpty(info.url) && string.IsNullOrEmpty(info.lowurl))
            {
                Log.e("AppMsgMgr", "err lowurl = " + info.lowurl + " url" + info.url);
                return null;
            }
            string url = string.IsNullOrEmpty(info.lowurl) ? info.url : info.lowurl;
            if (string.IsNullOrEmpty(info.url))
            {
                url = info.url;
            }
            int index = url.IndexOf("wechat_music_url=");
            if (index < 0)
            {
                index = url.IndexOf("p=");
                if (index < 0)
                {
                    Log.e("AppMsgMgr", "invalid sourceurl = " + url);
                    return null;
                }
                index += "p=".Length;
            }
            else
            {
                index += "wechat_music_url=".Length;
            }
            string str2 = url.Substring(index);
            if (string.IsNullOrEmpty(str2))
            {
                Log.e("AppMsgMgr", "invalid bcdstring = " + str2);
                return null;
            }
            AppMusicInfo objectFromJson = (AppMusicInfo)Util.GetObjectFromJson(Util.BcdToStr(str2), typeof(AppMusicInfo));
            if (objectFromJson == null)
            {
                return null;
            }
            return objectFromJson;
        }

        public static string getXmlNodeString(string fromStrXml, string nodeName)
        {
            if (string.IsNullOrEmpty(fromStrXml) || string.IsNullOrEmpty(nodeName))
            {
                return null;
            }
            try
            {
                foreach (XElement element in XDocument.Parse(fromStrXml).Descendants(nodeName))
                {
                    return element.ToString();
                }
                return null;
            }
            catch (Exception exception)
            {
                Log.e("AppMsgMgr", exception.Message);
                return null;
            }
        }

        public static bool isFileSupport(string fileName)
        {
            return true;
        }

        public static bool IsMultyArticleMsg(AppMsgInfo info)
        {
            return (((info != null) && (info.mmreader != null)) && (info.mmreader.count > 1));
        }

        public static bool isPictureFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            fileName = fileName.Trim().ToLower();
            if (!fileName.EndsWith(".png"))
            {
                return fileName.EndsWith(".jpg");
            }
            return true;
        }

        public static bool isTxtFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            fileName = fileName.Trim().ToLower();
            if ((!fileName.EndsWith(".txt") && !fileName.EndsWith(".log")) && !fileName.EndsWith(".xml"))
            {
                return fileName.EndsWith(".xsl");
            }
            return true;
        }

        public static string modifyXmlNode(string fromStrXml, List<MsgXmlNode> nodeList)
        {
            if ((string.IsNullOrEmpty(fromStrXml) || (nodeList == null)) || (nodeList.Count <= 0))
            {
                return null;
            }
            try
            {
                XDocument document = XDocument.Parse(fromStrXml);
                foreach (MsgXmlNode node in nodeList)
                {
                    foreach (XElement element in document.Descendants(node.Name))
                    {
                        element.Value = node.Value;
                    }
                }
                return document.ToString();
            }
            catch (Exception exception)
            {
                Log.e("AppMsgMgr", exception.Message);
                return null;
            }
        }

        public static AppMsgInfo ParseAppXml(string strInXml)
        {
            string str = Util.preParaXml(strInXml);
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            try
            {
                XElement element = null;
                element = XDocument.Parse(str).Element("msg");
                if (element == null)
                {
                    return null;
                }
                AppMsgInfo info = new AppMsgInfo();
                XElement element2 = element.Element("fromusername");
                if (element2 != null)
                {
                    info.fromUserName = element2.Value;
                }
                XElement nodeappmsg = element.Element("appmsg");
                if (nodeappmsg != null)
                {
                    XAttribute attribute = nodeappmsg.Attribute("appid");
                    if (attribute != null)
                    {
                        info.appid = attribute.Value;
                    }
                    attribute = nodeappmsg.Attribute("sdkver");
                    if (attribute != null)
                    {
                        info.sdkVer = attribute.Value;
                    }
                    XElement element4 = nodeappmsg;
                    if (element4 != null)
                    {
                        XElement element5 = element4.Element("title");
                        if (element5 != null)
                        {
                            info.title = element5.Value;
                        }
                        XElement element6 = element4.Element("des");
                        if (element6 != null)
                        {
                            info.description = element6.Value;
                        }
                        XElement element7 = element4.Element("action");
                        if (element7 != null)
                        {
                            info.action = element7.Value;
                        }
                        XElement element8 = element4.Element("type");
                        if (element8 != null)
                        {
                            info.type = int.Parse(element8.Value);
                        }
                        XElement element9 = element4.Element("showtype");
                        if (element9 != null)
                        {
                            info.showtype = int.Parse(element9.Value);
                        }
                        XElement element10 = element4.Element("content");
                        if (element10 != null)
                        {
                            info.content = element10.Value;
                        }
                        XElement element11 = element4.Element("url");
                        if (element11 != null)
                        {
                            info.url = element11.Value;
                            //if (!string.IsNullOrEmpty(info.url) && EmotionStoreMgr.IsEmotionDetailUrl(info.url))
                            //{
                            //    info.packageId = EmotionStoreMgr.GetPackageIdFromUri(info.url);
                            //}
                        }
                        XElement element12 = element4.Element("lowurl");
                        if (element12 != null)
                        {
                            info.lowurl = element12.Value;
                        }
                        XElement element13 = element4.Element("dataurl");
                        if (element13 != null)
                        {
                            info.dataurl = element13.Value;
                        }
                        XElement element14 = element4.Element("lowdataurl");
                        if (element14 != null)
                        {
                            info.lowdataurl = element14.Value;
                        }
                        XElement element15 = element4.Element("thumburl");
                        if (element15 != null)
                        {
                            info.httpthumburl = element15.Value;
                        }
                        XElement element16 = element4.Element("appattach");
                        if (element16 != null)
                        {
                            XElement element17 = element16.Element("totallen");
                            if (element17 != null)
                            {
                                info.totallength = int.Parse(element17.Value);
                            }
                            XElement element18 = element16.Element("attachid");
                            if (element18 != null)
                            {
                                info.attachid = element18.Value;
                            }
                            XElement element19 = element16.Element("emoticonmd5");
                            if (element19 != null)
                            {
                                info.emoticonmd5 = element19.Value;
                            }
                            XElement element20 = element16.Element("fileext");
                            if (element20 != null)
                            {
                                info.fileext = element20.Value;
                            }
                            XElement element21 = element16.Element("cdnattachurl");
                            if (element21 != null)
                            {
                                info.cdnattachurl = element21.Value;
                            }
                            XElement element22 = element16.Element("cdnthumburl");
                            if (element22 != null)
                            {
                                info.cdnthumburl = element22.Value;
                            }
                            XElement element23 = element16.Element("cdnthumblength");
                            if (element23 != null && element23.Value != "")
                            {
                                info.cdnthumblength = int.Parse(element23.Value);
                            }
                            XElement element24 = element16.Element("cdnthumbheight");
                            if (element24 != null && element24.Value != "")
                            {
                                info.cdnthumbheight = int.Parse(element24.Value);
                            }
                            XElement element25 = element16.Element("cdnthumbwidth");
                            if (element25 != null && element25.Value != "")
                            {
                                info.cdnthumbwidth = int.Parse(element25.Value);
                            }
                            XElement element26 = element16.Element("aeskey");
                            if (element26 != null)
                            {
                                info.aeskey = element26.Value;
                            }
                            XElement element27 = element16.Element("encryver");
                            if (element27 != null)
                            {
                                info.encryver = element27.Value;
                            }
                        }
                        XElement element28 = element4.Element("extinfo");
                        if (element28 != null)
                        {
                            info.extinfo = element28.Value;
                        }
                        XElement element29 = element4.Element("sourceusername");
                        if (element29 != null)
                        {
                            info.sourceusername = element29.Value;
                        }
                        XElement element30 = element4.Element("sourcedisplayname");
                        if (element30 != null)
                        {
                            info.sourcedisplayname = element30.Value;
                        }
                        XElement element31 = element4.Element("emoticonshared");
                        if (element31 != null)
                        {
                            XElement element32 = element31.Element("packageid");
                            if (element32 != null)
                            {
                                info.packageId = element32.Value;
                            }
                            XElement element33 = element31.Element("packageflag");
                            if (element33 != null && element33.Value != "")
                            {
                                info.packageFlag = int.Parse(element33.Value);
                            }
                        }
                        XElement element34 = element4.Element("mmreader");
                        MMReaderInfo info2 = null;
                        if (element34 != null)
                        {
                            element34 = element34.Element("category");
                            if (element34 != null)
                            {
                                info2 = new MMReaderInfo();
                                XAttribute attribute2 = element34.Attribute("type");
                                if (attribute2 != null && !string.IsNullOrEmpty(element34.Value))
                                {
                                    info2.type = int.Parse(attribute2.Value);
                                }
                                attribute2 = element34.Attribute("count");
                                if (attribute2 != null && !string.IsNullOrEmpty(attribute2.Value))
                                {
                                    info2.count = int.Parse(attribute2.Value);
                                }
                                XElement element35 = element34.Element("name");
                                if (element35 != null)
                                {
                                    info2.name = element35.Value;
                                }
                                XElement element36 = element34.Element("topnew");
                                if (element36 != null)
                                {
                                    MMReaderTopNew new2 = new MMReaderTopNew();
                                    XElement element37 = element36.Element("cover");
                                    if (element37 != null)
                                    {
                                        new2.cover = element37.Value;
                                    }
                                    element37 = element36.Element("width");
                                    if ((element37 != null) && !string.IsNullOrEmpty(element37.Value))
                                    {
                                        new2.width = int.Parse(element37.Value);
                                    }
                                    element37 = element36.Element("height");
                                    if ((element37 != null) && !string.IsNullOrEmpty(element37.Value))
                                    {
                                        new2.height = int.Parse(element37.Value);
                                    }
                                    info2.topnew = new2;
                                }
                                IEnumerable<XElement> enumerable = element34.Elements("item");
                                if (enumerable != null)
                                {
                                    List<MMReaderItem> list = new List<MMReaderItem>();
                                    foreach (XElement element38 in enumerable)
                                    {
                                        MMReaderItem item = new MMReaderItem();
                                        if (element38 != null)
                                        {
                                            XElement element39 = element38.Element("title");
                                            if (element39 != null)
                                            {
                                                item.title = element39.Value;
                                            }
                                            XElement element40 = element38.Element("digest");
                                            if (element40 != null)
                                            {
                                                item.digest = element40.Value;
                                            }
                                            XElement element41 = element38.Element("url");
                                            if (element41 != null)
                                            {
                                                item.url = element41.Value;
                                            }
                                            XElement element42 = element38.Element("shorturl");
                                            if (element42 != null)
                                            {
                                                item.shorturl = element42.Value;
                                            }
                                            XElement element43 = element38.Element("longurl");
                                            if (element43 != null)
                                            {
                                                item.longurl = element43.Value;
                                            }
                                            XElement element44 = element38.Element("pub_time");
                                            if (element44 != null)
                                            {
                                                item.pub_time = long.Parse(element44.Value);
                                            }
                                            XElement element45 = element38.Element("cover");
                                            if (element45 != null)
                                            {
                                                item.cover = element45.Value;
                                            }
                                            XElement element46 = element38.Element("tweetid");
                                            if (element46 != null)
                                            {
                                                item.tweetid = element46.Value;
                                            }
                                            XElement element47 = element38.Element("itemshowtype");
                 
                                            if (element47 != null && !string.IsNullOrEmpty(element47.Value))
                                            {
                                                //int.TryParse(element47.Value, out item.itemshowtype);
                                                item.itemshowtype = int.Parse(element47.Value);

                                            }
                                            XElement element48 = element38.Element("sources");
                                            if ((element48 != null) && (element48.Elements("source") != null))
                                            {
                                                List<MMReaderItemSource> list2 = new List<MMReaderItemSource>();
                                                foreach (XElement element49 in enumerable)
                                                {
                                                    MMReaderItemSource source = new MMReaderItemSource();
                                                    XElement element50 = element49.Element("name");
                                                    if (element50 != null)
                                                    {
                                                        source.name = element50.Value;
                                                    }
                                                    XElement element51 = element49.Element("icon");
                                                    if (element51 != null)
                                                    {
                                                        source.icon = element51.Value;
                                                    }
                                                    list2.Add(source);
                                                }
                                                if (list2.Count != 0)
                                                {
                                                    item.sources = list2;
                                                }
                                            }
                                            XElement element52 = element38.Element("styles");
                                            if (element52 != null)
                                            {
                                                item.styles = new MMReaderItemStyles();
                                                XElement element53 = element52.Element("topColor");
                                                if (element53 != null)
                                                {
                                                    item.styles.topColor = element53.Value;
                                                }
                                                IEnumerable<XElement> enumerable3 = element52.Elements("style");
                                                if (enumerable3 != null)
                                                {
                                                    List<MMReaderItemStyleItem> list3 = new List<MMReaderItemStyleItem>();
                                                    foreach (XElement element54 in enumerable3)
                                                    {
                                                        MMReaderItemStyleItem item2 = new MMReaderItemStyleItem();
                                                        XElement element55 = element54.Element("range");
                                                        if (element55 != null)
                                                        {
                                                            item2.range = element55.Value;
                                                        }
                                                        XElement element56 = element54.Element("font");
                                                        if (element56 != null)
                                                        {
                                                            item2.font = element56.Value;
                                                        }
                                                        XElement element57 = element54.Element("color");
                                                        if (element57 != null)
                                                        {
                                                            item2.color = element57.Value;
                                                        }
                                                        list3.Add(item2);
                                                    }
                                                    if (list3.Count != 0)
                                                    {
                                                        item.styles.styleList = list3;
                                                    }
                                                }
                                                IEnumerable<XElement> enumerable4 = element52.Elements("line");
                                                if (enumerable4 != null)
                                                {
                                                    List<MMReaderItemLineItem> list4 = new List<MMReaderItemLineItem>();
                                                    foreach (XElement element58 in enumerable4)
                                                    {
                                                        MMReaderItemLineItem item3 = new MMReaderItemLineItem();
                                                        XElement element59 = element58.Element("offset");
                                                        if (element59 != null)
                                                        {
                                                            int.TryParse(element59.Value, out item3.offset);
                                                        }
                                                        XElement element60 = element58.Element("font");
                                                        if (element60 != null)
                                                        {
                                                            item3.font = element60.Value;
                                                        }
                                                        XElement element61 = element58.Element("color");
                                                        if (element61 != null)
                                                        {
                                                            item3.color = element61.Value;
                                                        }
                                                        XElement element62 = element58.Element("chars");
                                                        if (element62 != null)
                                                        {
                                                            item3.chars = element62.Value;
                                                        }
                                                        list4.Add(item3);
                                                    }
                                                    if (list4.Count != 0)
                                                    {
                                                        item.styles.lineList = list4;
                                                    }
                                                }
                                            }
                                            list.Add(item);
                                        }
                                    }
                                    if (list.Count != 0)
                                    {
                                        info2.items = list;
                                    }
                                }
                            }
                        }
                        info.mmreader = info2;
                    }
                }
                if ((info.type == 0x7d0) || (info.type == 0x7d1))
                {
                    info.payinfoitem = WCPayInfoItem.fromXElement(nodeappmsg);
                }
                return info;
            }
            catch (Exception exception)
            {
                Log.e("AppMsgMgr", exception.ToString() + str);
                return null;
            }
        }


        //public static MMReaderInfo parseMMReaderXml(string strInXml)
        //{
        //    string str = Util.preParaXml(strInXml);
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return null;
        //    }
        //    try
        //    {
        //        XElement element = null;
        //        element = XDocument.Parse(str).Element("mmreader");
        //        MMReaderInfo info = null;
        //        if (element != null)
        //        {
        //            element = element.Element("category");
        //            if (element != null)
        //            {
        //                info = new MMReaderInfo();
        //                XAttribute attribute = element.Attribute("type");
        //                if (attribute != null)
        //                {
        //                    info.type = int.Parse(attribute.Value);
        //                }
        //                attribute = element.Attribute("count");
        //                if (attribute != null)
        //                {
        //                    info.count = int.Parse(attribute.Value);
        //                }
        //                XElement element2 = element.Element("name");
        //                if (element2 != null)
        //                {
        //                    info.name = element2.Value;
        //                }
        //                XElement element3 = element.Element("topnew");
        //                if (element3 != null)
        //                {
        //                    MMReaderTopNew new2 = new MMReaderTopNew();
        //                    XElement element4 = element3.Element("cover");
        //                    if (element4 != null)
        //                    {
        //                        new2.cover = element4.Value;
        //                        int index = new2.cover.IndexOf("|", 0);
        //                        if (index > 0)
        //                        {
        //                            new2.cover = new2.cover.Substring(0, index);
        //                        }
        //                    }
        //                    element4 = element3.Element("width");
        //                    if (element4 != null)
        //                    {
        //                        new2.width = int.Parse(element4.Value);
        //                    }
        //                    element4 = element3.Element("height");
        //                    if (element4 != null)
        //                    {
        //                        new2.height = int.Parse(element4.Value);
        //                    }
        //                    info.topnew = new2;
        //                }
        //                IEnumerable<XElement> collection = element.Elements("newitem");
        //                ObservableCollection<XElement> observables = null;
        //                if (collection != null)
        //                {
        //                    observables = collection.ToObservableCollection<XElement>();
        //                    if (observables.Count <= 0)
        //                    {
        //                        collection = element.Elements("item");
        //                        if (collection != null)
        //                        {
        //                            observables = collection.ToObservableCollection<XElement>();
        //                        }
        //                    }
        //                }
        //                if (observables != null)
        //                {
        //                    List<MMReaderItem> list = new List<MMReaderItem>();
        //                    foreach (XElement element5 in observables)
        //                    {
        //                        MMReaderItem item = new MMReaderItem();
        //                        if (element5 != null)
        //                        {
        //                            XElement element6 = element5.Element("title");
        //                            if (element6 != null)
        //                            {
        //                                item.title = element6.Value;
        //                            }
        //                            XElement element7 = element5.Element("digest");
        //                            if (element7 != null)
        //                            {
        //                                item.digest = element7.Value;
        //                            }
        //                            XElement element8 = element5.Element("url");
        //                            if (element8 != null)
        //                            {
        //                                item.url = element8.Value;
        //                            }
        //                            XElement element9 = element5.Element("shorturl");
        //                            if (element9 != null)
        //                            {
        //                                item.shorturl = element9.Value;
        //                            }
        //                            XElement element10 = element5.Element("longurl");
        //                            if (element10 != null)
        //                            {
        //                                item.longurl = element10.Value;
        //                            }
        //                            XElement element11 = element5.Element("pub_time");
        //                            if (element11 != null)
        //                            {
        //                                item.pub_time = long.Parse(element11.Value);
        //                            }
        //                            XElement element12 = element5.Element("cover");
        //                            if (element12 != null)
        //                            {
        //                                item.cover = element12.Value;
        //                                int length = item.cover.IndexOf("|", 0);
        //                                if (length > 0)
        //                                {
        //                                    item.cover = item.cover.Substring(0, length);
        //                                }
        //                            }
        //                            XElement element13 = element5.Element("tweetid");
        //                            if (element13 != null)
        //                            {
        //                                item.tweetid = element13.Value;
        //                            }
        //                            XElement element14 = element5.Element("sources");
        //                            if ((element14 != null) && (element14.Elements("source") != null))
        //                            {
        //                                List<MMReaderItemSource> list2 = new List<MMReaderItemSource>();
        //                                foreach (XElement element15 in collection)
        //                                {
        //                                    MMReaderItemSource source = new MMReaderItemSource();
        //                                    XElement element16 = element15.Element("name");
        //                                    if (element16 != null)
        //                                    {
        //                                        source.name = element16.Value;
        //                                    }
        //                                    XElement element17 = element15.Element("icon");
        //                                    if (element17 != null)
        //                                    {
        //                                        source.icon = element17.Value;
        //                                    }
        //                                    list2.Add(source);
        //                                }
        //                                if (list2.Count != 0)
        //                                {
        //                                    item.sources = list2;
        //                                }
        //                            }
        //                            list.Add(item);
        //                        }
        //                    }
        //                    if (list.Count != 0)
        //                    {
        //                        info.items = list;
        //                    }
        //                }
        //            }
        //        }
        //        return info;
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("AppMsgMgr", exception.Message);
        //        return null;
        //    }
        //}

        //public static ShakeTrans parseShakeTransXml(string strXml)
        //{
        //    if (string.IsNullOrEmpty(strXml))
        //    {
        //        return null;
        //    }
        //    AppMsgInfo info = parseAppXml(strXml);
        //    if (info == null)
        //    {
        //        return null;
        //    }
        //    if (info.showtype != 2)
        //    {
        //        return null;
        //    }
        //    ShakeTrans trans = new ShakeTrans();
        //    try
        //    {
        //        XElement element = null;
        //        element = XDocument.Parse(strXml).Element("msg");
        //        if (element == null)
        //        {
        //            return null;
        //        }
        //        XElement element2 = element.Element("ShakePageResult");
        //        if (element2 == null)
        //        {
        //            return null;
        //        }
        //        XElement element3 = element2.Element("title");
        //        if (element3 != null)
        //        {
        //            trans.strTitle = element3.Value;
        //        }
        //        XElement element4 = element2.Element("url");
        //        if (element4 != null)
        //        {
        //            trans.strPageUrl = element4.Value;
        //        }
        //        IEnumerable<XElement> enumerable = element2.Elements("imagelist").Elements<XElement>();
        //        if (enumerable != null)
        //        {
        //            List<ShakeImage> list = new List<ShakeImage>();
        //            foreach (XElement element5 in enumerable)
        //            {
        //                ShakeImage item = new ShakeImage();
        //                if (element5 != null)
        //                {
        //                    XElement element6 = element5.Element("thumburl");
        //                    if (element6 != null)
        //                    {
        //                        item.strThumbUrl = element6.Value;
        //                    }
        //                    XElement element7 = element5.Element("imgurl");
        //                    if (element7 != null)
        //                    {
        //                        item.strImgUrl = element7.Value;
        //                    }
        //                    XElement element8 = element5.Element("weburl");
        //                    if (element8 != null)
        //                    {
        //                        item.strWebUrl = element8.Value;
        //                    }
        //                }
        //                list.Add(item);
        //            }
        //            trans.imageList = list;
        //        }
        //        return trans;
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("AppMsgMgr", exception.ToString() + strXml);
        //        return null;
        //    }
        //}

        //public static void SendThirdGif(string toUser, AppInfo appInfo, string thumbSrc, string imageSrc)
        //{
        //    if ((!string.IsNullOrEmpty(toUser) && (appInfo != null)) && (!string.IsNullOrEmpty(thumbSrc) && !string.IsNullOrEmpty(imageSrc)))
        //    {
        //        string destinationFileName = null;
        //        string str2 = null;
        //        long content = 0L;
        //        string hashString = "";
        //        try
        //        {
        //            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
        //            {
        //                if (!file.DirectoryExists(StorageIO.getThumbnailPath()))
        //                {
        //                    file.CreateDirectory(StorageIO.getThumbnailPath());
        //                }
        //                if (!file.DirectoryExists(StorageIO.getAttachmentPath()))
        //                {
        //                    file.CreateDirectory(StorageIO.getAttachmentPath());
        //                }
        //                string str4 = thumbSrc.Substring(thumbSrc.IndexOf("."));
        //                destinationFileName = StorageIO.getThumbnailPath() + "/" + MD5Core.GetHashString(thumbSrc + Util.getNowMilliseconds()) + str4;
        //                file.CopyFile(thumbSrc, destinationFileName);
        //                str4 = imageSrc.Substring(imageSrc.IndexOf("."));
        //                str2 = StorageIO.getAttachmentPath() + "/" + MD5Core.GetHashString(imageSrc + Util.getNowMilliseconds()) + str4;
        //                file.CopyFile(imageSrc, str2);
        //            }
        //            byte[] input = StorageIO.readFromFile(str2);
        //            content = input.Length;
        //            hashString = MD5Core.GetHashString(input);
        //            input = null;
        //        }
        //        catch (Exception exception)
        //        {
        //            destinationFileName = null;
        //            str2 = null;
        //            Log.e("AppMsgMgr", exception.Message);
        //            return;
        //        }
        //        XElement element = new XElement("msg");
        //        XElement element2 = new XElement("appmsg");
        //        element2.Add(new XAttribute("appid", appInfo.AppID));
        //        element2.Add(new XAttribute("sdkver", 0));
        //        element2.Add(new XElement("title", ""));
        //        element2.Add(new XElement("des", ""));
        //        element2.Add(new XElement("action", ""));
        //        element2.Add(new XElement("type", 8));
        //        element2.Add(new XElement("showtype", 0));
        //        element2.Add(new XElement("content", ""));
        //        element2.Add(new XElement("url", ""));
        //        element2.Add(new XElement("lowurl", ""));
        //        XElement element3 = new XElement("appattach");
        //        element3.Add(new XElement("totallen", content));
        //        element3.Add(new XElement("attachid", ""));
        //        element3.Add(new XElement("emoticonmd5", hashString));
        //        element3.Add(new XElement("fileext", "pic"));
        //        element2.Add(element3);
        //        element2.Add(new XElement("extinfo", ""));
        //        element2.Add(new XElement("sourceusername", ""));
        //        element2.Add(new XElement("sourcedisplayname", ""));
        //        element2.Add(new XElement("commenturl", ""));
        //        element.Add(element2);
        //        element.Add(new XElement("fromusername", AccountMgr.curUserName));
        //        element.Add(new XElement("scene", 0));
        //        element.Add(new XElement("commenturl", ""));
        //        XElement element4 = new XElement("appinfo");
        //        element4.Add(new XElement("version", appInfo.AppVersion));
        //        element4.Add(new XElement("appname", appInfo.AppName));
        //        element.Add(element4);
        //        StringWriter textWriter = new StringWriter();
        //        element.Save(textWriter);
        //        string xmlStrMsg = textWriter.GetStringBuilder().ToString();
        //        ChatMsg sendmsg = SendAppMsgService.bulidAppMsg(AccountMgr.curUserName, toUser, xmlStrMsg, destinationFileName, str2);
        //        if (sendmsg != null)
        //        {
        //            SendAppMsgService.uploadAttachAndSendAppMsg(sendmsg, AppMsgSouce.MM_APPMSG_WEIXIN);
        //        }
        //    }
        //}

        //public static string updateXmlNode(string fromStrXml, List<MsgXmlNode> nodeList)
        //{
        //    if ((string.IsNullOrEmpty(fromStrXml) || (nodeList == null)) || (nodeList.Count <= 0))
        //    {
        //        return null;
        //    }
        //    try
        //    {
        //        XDocument document = XDocument.Parse(fromStrXml);
        //        foreach (MsgXmlNode node in nodeList)
        //        {
        //            foreach (XElement element in document.Descendants(node.parentName))
        //            {
        //                element.SetElementValue(node.Name, node.Value);
        //            }
        //        }
        //        return document.ToString();
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("AppMsgMgr", exception.Message);
        //        return null;
        //    }
        //}
    }

    public class MMReaderItemLineItem
    {
        public string chars;
        public string color;
        public string font;
        public int offset;
    }
    public class MMReaderItemStyles
    {
        public List<MMReaderItemLineItem> lineList;
        public List<MMReaderItemStyleItem> styleList;
        public string topColor;
    }
    public class MMReaderItemStyleItem
    {
        public string color;
        public string font;
        public string range;
    }
}

