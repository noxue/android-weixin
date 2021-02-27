namespace MicroMsg.Manager
{
    using System;
    
    using System.Runtime.Serialization;

    public class ConferenceUrlInfo
    {
        public ConferenceUrlInfo()
        {
        }

        public ConferenceUrlInfo(string t, string v)
        {
            this.url = t;
            this.title = v;
        }


        public string title { get; set; }

    
        public string url { get; set; }
    }
}

