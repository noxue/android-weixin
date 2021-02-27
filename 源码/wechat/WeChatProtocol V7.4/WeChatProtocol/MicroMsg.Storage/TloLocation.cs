namespace MicroMsg.Storage
{
    using System;
    using System.Xml.Linq;

    public class TloLocation : TloXmlItem
    {
        public float fLatitude;
        public float fLongitude;
        public string strPoiScale;
        public string strCity;
        public string strCountry;
        public string strName;
        public string strPoiName;
        public string strProvince;

        public TloLocation()
        {
            this.strCountry = string.Empty;
            this.strCity = string.Empty;
            this.strProvince = string.Empty;
            this.strName = string.Empty;
            this.strPoiName = string.Empty;
            this.strPoiScale = string.Empty;

        }

        public TloLocation(XElement xml)
        {
            this.strCountry = string.Empty;
            this.strCity = string.Empty;
            this.strProvince = string.Empty;
            this.strName = string.Empty;
            this.strPoiName = string.Empty;
            this.strPoiScale = string.Empty;
            this.parse(xml);
        }

        public bool parse(XElement xml)
        {
            foreach (XAttribute attribute in xml.Attributes())
            {
                if (attribute.Name == "longitude")
                {
                    this.fLongitude = (float)attribute;
                }
                else if (attribute.Name == "latitude")
                {
                    this.fLatitude = (float)attribute;
                }
                else if (attribute.Name == "country")
                {
                    this.strCountry = (string)attribute;
                }
                else if (attribute.Name == "city")
                {
                    this.strCity = (string)attribute;
                }
                else if (attribute.Name == "province")
                {
                    this.strProvince = (string)attribute;
                }
                else if (attribute.Name == "poiScale")
                {
                    this.strPoiScale = (string)attribute;
                }
                else if (attribute.Name == "poiName")
                {
                    this.strPoiName = (string)attribute;
                }
            }
            return true;
        }

        public XElement toXElement()
        {
            XElement element = new XElement("location");
            element.Add(new XAttribute("longitude", this.fLongitude));
            element.Add(new XAttribute("latitude", this.fLatitude));
            if (!string.IsNullOrEmpty(this.strCountry))
            {
                element.Add(new XAttribute("country", this.strCountry));
            }
            if (!string.IsNullOrEmpty(this.strCity))
            {
                element.Add(new XAttribute("city", this.strCity));
            }
            if (!string.IsNullOrEmpty(this.strProvince))
            {
                element.Add(new XAttribute("province", this.strProvince));
            }
            if (!string.IsNullOrEmpty(this.strPoiName))
            {
                element.Add(new XAttribute("poiName", this.strPoiName));
            }
            element.Add(new XAttribute("poiScale", this.strPoiScale));
            //element.Add(new XAttribute("poiClassifyId", "UgcPoiEx_28505809636"));
            //element.Add(new XAttribute("poiClassifyType", "2"));
            //element.Add(new XAttribute("poiClickableStatus", "0"));
            return element;
        }
    }
}
