namespace MicroMsg.Storage
{
    using MicroMsg.Common.Utils;
    using System;
    using System.Xml.Linq;

    public class TloAppInfo : TloXmlItem
    {
        public int nVersion;
        public string strAppName;
        public string strFromUrl;
        public string strID;
        public string strInstallUrl;
        public int nisForceUpdate;

        public TloAppInfo()
        {
        }

        public TloAppInfo(XElement xml)
        {
            this.parse(xml);
        }

        public bool parse(XElement xml)
        {
            foreach (XElement element in xml.Elements())
            {
                if (!element.IsEmpty || element.HasAttributes)
                {
                    if (element.Name == "id")
                    {
                        this.strID = (string) element;
                    }
                    else if (element.Name == "version")
                    {
                        if ((string)element == "") {
                            this.nVersion = 0;

                        } else {
                            this.nVersion = (int)element;
                        }
                        
                    }
                    else if (element.Name == "appName")
                    {
                        this.strAppName = (string) element;
                    }
                    else if (element.Name == "installUrl")
                    {
                        this.strInstallUrl = (string) element;
                    }
                    else if (element.Name == "fromUrl")
                    {
                        this.strFromUrl = (string) element;
                    }
                    else if (element.Name == "isForceUpdate")
                    {
                        this.nisForceUpdate = (int)element;
                    }
                    else
                    {
                        Log.e("TimeLineObject", "unknown AppInfo tag=" + element.Name);
                    }
                }
            }
            return true;
        }

        public XElement toXElement()
        {
            XElement element = new XElement("appInfo");
            element.Add(new XElement("id", this.strID));
            element.Add(new XElement("version", this.nVersion));
            element.Add(new XElement("appName", this.strAppName));
            element.Add(new XElement("installUrl", this.strInstallUrl));
            element.Add(new XElement("fromUrl", this.strFromUrl));
            element.Add(new XElement("isForceUpdate", this.nisForceUpdate));
            return element;
        }
    }
}

