namespace MicroMsg.Manager
{
    using System;
    using System.Collections.Generic;

    public class MMReaderItem
    {
        public string cover;
        public string digest;
        public int fileid;
        public int itemshowtype;
        public string longurl;
        public long pub_time;
        public string shorturl;
        public List<MMReaderItemSource> sources;
        public MMReaderItemStyles styles;
        public string title;
        public string tweetid;
        public string url;
    }
}
