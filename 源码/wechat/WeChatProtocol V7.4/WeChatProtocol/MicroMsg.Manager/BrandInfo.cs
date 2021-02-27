namespace MicroMsg.Manager
{
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;


    public class BrandInfo
    {
        public BrandInfo()
        {
        }

        public BrandInfo(List<ConferenceUrlInfo> urls)
        {
            this.urls = urls;
        }


        public List<ConferenceUrlInfo> urls { get; set; }
    }
}

