namespace MicroMsg.Manager
{
    using MicroMsg.Storage;
    using System;


    public class SnsOpLog
    {
        public int nDelCommentId;
        public ulong nObjectID;
        public int nOpType;
        public int nRetryTimes;
        public int nUpdateSnsObject;

        public SnsOpLog(ulong objId, int opType, int updateSnsObj, int comId)
        {
            this.nObjectID = objId;
            this.nOpType = opType;
            this.nUpdateSnsObject = updateSnsObj;
            this.nDelCommentId = comId;
        }
    }
}

