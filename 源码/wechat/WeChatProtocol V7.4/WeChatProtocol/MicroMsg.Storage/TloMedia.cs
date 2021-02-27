namespace MicroMsg.Storage
{

    using System;
    using System.Xml.Linq;

    public class TloMedia : TloXmlItem
    {
        //public Picture mPicture;
        public int nAngle;
        public float nHeight;
        public ulong nID;
        public int nPrivate;
        public int nThumbType;
        public float nTotalSize;
        public int nType;
        public int nUrlType;
        public float nWidth;
        public string strDescription;
        public string strThumb;
        public string strTitle;
        public string strUrl;
        private const string TAG = "TimeLineObject";

        public TloMedia()
        {
            this.strTitle = string.Empty;
            this.strDescription = string.Empty;
            this.strUrl = string.Empty;
            this.strThumb = string.Empty;
        }

        public TloMedia(XElement xml)
        {
            this.strTitle = string.Empty;
            this.strDescription = string.Empty;
            this.strUrl = string.Empty;
            this.strThumb = string.Empty;
            this.parse(xml);
        }

        public void copyFrom(TloMedia other)
        {
            if ((this != other) && (other != null))
            {
                this.nID = other.nID;
                this.nType = other.nType;
                this.strTitle = other.strTitle;
                this.strDescription = other.strDescription;
                this.nPrivate = other.nPrivate;
                this.nUrlType = other.nUrlType;
                this.strUrl = other.strUrl;
                this.nThumbType = other.nThumbType;
                this.strThumb = other.strThumb;
                this.nWidth = other.nWidth;
                this.nHeight = other.nHeight;
                this.nTotalSize = other.nTotalSize;
                //this.mPicture = other.mPicture;
                this.nAngle = other.nAngle;
            }
        }

        public bool parse(XElement xml)
        {
            foreach (XElement element in xml.Elements())
            {
                if (!TimeLineObject.empty(element))
                {
                    if (element.Name == "id")
                    {
                        this.nID = (ulong)element;
                    }
                    else if (element.Name == "type")
                    {
                        this.nType = (int)element;
                    }
                    else if (element.Name == "title")
                    {
                        this.strTitle = (string)element;
                    }
                    else if (element.Name == "description")
                    {
                        this.strDescription = (string)element;
                    }
                    else if (element.Name == "private")
                    {
                        this.nPrivate = (int)element;
                    }
                    else if (element.Name == "url")
                    {
                        this.strUrl = (string)element;
                        this.nUrlType = (int)element.Attribute("type");
                    }
                    else if (element.Name == "thumb")
                    {
                        this.strThumb = (string)element;
                        this.nThumbType = (int)element.Attribute("type");
                    }
                    else if (element.Name == "size")
                    {
                        foreach (XAttribute attribute in element.Attributes())
                        {
                            if (attribute.Name == "width")
                            {
                                this.nWidth = (float)element.Attribute("width");
                            }
                            else if (attribute.Name == "height")
                            {
                                this.nHeight = (float)element.Attribute("height");
                            }
                            else if (attribute.Name == "nTotalSize")
                            {
                                this.nTotalSize = (float)element.Attribute("totalSize");
                            }
                        }
                    }
                }
            }
            return true;
        }

        public XElement toXElement()
        {
            XElement element = new XElement("media");
            element.Add(new XElement("id", this.nID));
            element.Add(new XElement("type", this.nType));
            if (this.strTitle != null)
            {
                element.Add(new XElement("title", this.strTitle));
            }
            if (this.strDescription != null)
            {
                element.Add(new XElement("description", this.strDescription));
            }
            element.Add(new XElement("private", this.nPrivate));
            element.Add(new XElement("url", new object[] { new XAttribute("type", this.nUrlType), this.strUrl }));
            element.Add(new XElement("thumb", new object[] { new XAttribute("type", this.nThumbType), this.strThumb }));
            element.Add(new XElement("size", new object[] { new XAttribute("width", this.nWidth), new XAttribute("height", this.nHeight), new XAttribute("totalSize", this.nTotalSize) }));
            return element;
        }
    }
}
