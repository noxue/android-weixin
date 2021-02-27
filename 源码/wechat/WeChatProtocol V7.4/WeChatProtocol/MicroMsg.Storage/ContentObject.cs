namespace MicroMsg.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class ContentObject : TloXmlItem
    {
        public List<TloMedia> mediaList;
        public int nStyle;
        public string strContentUrl;
        public string strDescription;
        public string strTitle;
        private const string TAG = "TimeLineObject";

        public ContentObject()
        {
            this.strTitle = string.Empty;
            this.strDescription = string.Empty;
            this.strContentUrl = string.Empty;
        }

        public ContentObject(XElement xml)
        {
            this.strTitle = string.Empty;
            this.strDescription = string.Empty;
            this.strContentUrl = string.Empty;
            this.parse(xml);
        }

        public override void dump(ref string strInfo)
        {
            base.dump(ref strInfo);
            if (this.mediaList != null)
            {
                foreach (TloMedia media in this.mediaList)
                {
                    media.dump(ref strInfo);
                }
            }
        }

        public bool parse(XElement xml)
        {
            foreach (XElement element in xml.Elements())
            {
                if (!TimeLineObject.empty(element))
                {
                    if (element.Name == "contentStyle")
                    {
                        this.nStyle = (int)element;
                    }
                    else if (element.Name == "title")
                    {
                        this.strTitle = (string)element;
                    }
                    else if (element.Name == "description")
                    {
                        this.strDescription = (string)element;
                    }
                    else if (element.Name == "mediaList")
                    {
                        this.parseMediaList(element);
                    }
                    else if (element.Name == "contentUrl")
                    {
                        this.strContentUrl = (string)element;
                    }
                }
            }
            return true;
        }

        public bool parseMediaList(XElement xml)
        {
            foreach (XElement element in xml.Elements())
            {
                if ((!element.IsEmpty && !string.IsNullOrEmpty(element.Value)) && (element.Name == "media"))
                {
                    if (this.mediaList == null)
                    {
                        this.mediaList = new List<TloMedia>();
                    }
                    this.mediaList.Add(new TloMedia(element));
                }
            }
            return true;
        }

        public XElement toXElement()
        {
            XElement element = new XElement("ContentObject");
            element.Add(new XElement("contentStyle", this.nStyle));
            if (this.strTitle != null)
            {
                element.Add(new XElement("title", this.strTitle));
            }
            if (this.strDescription != null)
            {
                element.Add(new XElement("description", this.strDescription));
            }
            if (this.strContentUrl != null)
            {
                element.Add(new XElement("contentUrl", this.strContentUrl));
            }
            if (this.mediaList != null)
            {
                XElement content = new XElement("mediaList");
                foreach (TloMedia media in this.mediaList)
                {
                    content.Add(media.toXElement());
                }
                element.Add(content);
            }
            return element;
        }
    }
}
