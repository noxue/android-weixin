namespace MicroMsg.Storage
{
    using System;

    public class SnsComment
    {
        public int nCommentId;
        public uint nCreateTime;
        public int nReplyCommentId;
        public uint nSource;
        public uint nType;
        public string strContent;
        public string strNickName;
        public string strReplyUsername;
        public string strUserName;
    }
}

