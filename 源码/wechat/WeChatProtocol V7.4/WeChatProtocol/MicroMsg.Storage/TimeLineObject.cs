namespace MicroMsg.Storage
{
    using MicroMsg.Common.Utils;
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class TimeLineObject : TloXmlItem
    {
        public TloAppInfo appInfo;
        public ContentObject content;
        public TloLocation lbs;
        public uint nCreateTime;
        public ulong nID;
        public int nPrivate;
        public string strContentDesc = string.Empty;
        public string strNickName = string.Empty;
        public string strPublicUserName = string.Empty;
        public string strUsername = string.Empty;
        private const string TAG = "TimeLineObject";

        public static void dumpXmlData(byte[] bytesXmldata)
        {
        }

        public static bool empty(XElement e)
        {
            if (!e.IsEmpty && !string.IsNullOrEmpty(e.Value))
            {
                return false;
            }
            return !e.HasAttributes;
        }

        private static XDocument loadXml(byte[] bytesXmlData)
        {
            if ((bytesXmlData != null) && (bytesXmlData.Length > 0))
            {
                string strInXml = "";
                using (MemoryStream stream = new MemoryStream(bytesXmlData))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        strInXml = reader.ReadToEnd();
                    }
                }
                strInXml = Util.preParaXml(strInXml);
                if (!string.IsNullOrEmpty(strInXml))
                {
                    return XDocument.Parse(strInXml);
                }
            }
            return null;
        }

        public bool Parse(byte[] bytesXmlData)
        {
            if (bytesXmlData != null)
            {
                try
                {
                    XDocument document = loadXml(bytesXmlData);
                    if (document == null)
                    {
                        return false;
                    }
                    if (document.Root == null)
                    {
                        return false;
                    }
                    if (document.Root.Name != "TimelineObject")
                    {
                        Log.e("TimeLineObject", "error object name=" + document.Root.Name);
                        return false;
                    }
                    foreach (XElement element in document.Root.Elements())
                    {
                        if (!empty(element))
                        {
                            if (element.Name == "id")
                            {
                                this.nID = (ulong)element;
                            }
                            else if (element.Name == "username")
                            {
                                this.strUsername = (string)element;
                            }
                            else if (element.Name == "createTime")
                            {
                                this.nCreateTime = (uint)element;
                            }
                            else if (element.Name == "private")
                            {
                                this.nPrivate = (int)element;
                            }
                            else if (element.Name == "contentDesc")
                            {
                                this.strContentDesc = (string)element;
                            }
                            else if (element.Name == "ContentObject")
                            {
                                this.content = new ContentObject(element);
                            }
                            else if (element.Name == "location")
                            {
                                this.lbs = new TloLocation(element);
                            }
                            else if (element.Name == "appInfo")
                            {
                                if (!TloXmlItem.IsEmpty(element))
                                {
                                    this.appInfo = new TloAppInfo(element);
                                }
                            }
                            else if (element.Name == "publicUserName")
                            {
                                this.strPublicUserName = (string)element;
                            }
                            else
                            {
                                bool flag1 = element.Name == "sourceUserName";
                            }
                        }
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Log.e("TimeLineObject", "xml parse error=" + exception);
                }
            }
            return false;
        }

        public static TimeLineObject ParseFrom(byte[] bytesXmlData)
        {
            TimeLineObject obj2 = new TimeLineObject();
            if (obj2.Parse(bytesXmlData))
            {
                return obj2;
            }
            return null;
        }

        public XElement toXElement()
        {
            XElement element = new XElement("TimelineObject");
            element.Add(new XElement("id", this.nID));
            element.Add(new XElement("username", this.strUsername));
            element.Add(new XElement("createTime", this.nCreateTime));
            element.Add(new XElement("private", this.nPrivate));
            if (this.appInfo != null)
            {
                element.Add(this.appInfo.toXElement());
            }
            element.Add(new XElement("contentDesc", this.strContentDesc));
            if (this.content != null)
            {
                element.Add(this.content.toXElement());
            }
            if (this.lbs != null)
            {
                element.Add(this.lbs.toXElement());
            }
            element.Add(new XElement("sourceUserName", this.strPublicUserName));
            return element;
        }

        public byte[] toXmlData()
        {
            MemoryStream stream = new MemoryStream();
            this.toXElement().Save(stream);
            return stream.ToArray();
        }
    }
}
