namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;

    public class DownloadVoiceContext : IContextBase
    {
        public const int MAX_DOWNLOAD_PACK_SIZE = 0x10000;
        public int mMsgSvrID;
        public int mOffset;
        public int mStatus;
        private Queue<MsgTrans> mVoiceMsgQueue;
        public const int STATUS_BLOCK_COMPLETE = 3;
        public const int STATUS_BLOCK_RECVING = 2;
        public const int STATUS_COMPLETE = 4;
        public const int STATUS_ERROR = 5;
        public const int STATUS_READY = 0;
        public const int STATUS_RECVING = 1;
        public string strTalker;
        private const string TAG = "DownloadVoiceContext";

        public DownloadVoiceContext(int MsgSvrID, string talker)
        {
            this.mMsgSvrID = MsgSvrID;
            this.strTalker = talker;
            this.mVoiceMsgQueue = new Queue<MsgTrans>();
        }

        public MsgTrans Dequeue()
        {
            if (this.mVoiceMsgQueue.Count == 0)
            {
                return null;
            }
            return this.mVoiceMsgQueue.Dequeue();
        }

        public void Enqueue(MsgTrans voiceinfo)
        {
            this.mVoiceMsgQueue.Enqueue(voiceinfo);
        }

        public MsgTrans GetCurrentVoiceBlock()
        {
            if (this.mVoiceMsgQueue.Count == 0)
            {
                return null;
            }
            return this.mVoiceMsgQueue.Peek();
        }

        public bool isBlockRunning()
        {
            return (this.mStatus == 2);
        }

        public bool isRunning()
        {
            if (((this.mStatus != 1) && (this.mStatus != 2)) && (this.mStatus != 3))
            {
                return false;
            }
            return true;
        }

        public bool needToClean()
        {
            if ((this.mStatus != 4) && (this.mStatus != 5))
            {
                return false;
            }
            return true;
        }

        public bool needToHandle()
        {
            return (this.mStatus == 0);
        }

        public bool UpdateQueue(MsgTrans voiceinfo)
        {
            if (voiceinfo == null)
            {
                Log.d("DownloadVoiceContext", "invalid voiceinfo");
                return false;
            }
            bool flag = false;
            foreach (MsgTrans trans in this.mVoiceMsgQueue)
            {
                if ((trans.nMsgSvrID == voiceinfo.nMsgSvrID) && (trans.strClientMsgId == voiceinfo.strClientMsgId))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                this.mVoiceMsgQueue.Enqueue(voiceinfo);
            }
            return true;
        }
    }
}

