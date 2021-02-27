namespace MicroMsg.Plugin.Sns.Scene
{
    using MicroMsg.Storage;
    using System;

   
    public class SnsCommentNeedSend
    {
        public SnsComment currentComment;
        public int nRetryTimes;
        public ulong objectID;
        public ulong parentID;
        public SnsComment referComment;
        public string refFromNickName;
        public string refFromUserName;
        public string strClientID;
    }
}

