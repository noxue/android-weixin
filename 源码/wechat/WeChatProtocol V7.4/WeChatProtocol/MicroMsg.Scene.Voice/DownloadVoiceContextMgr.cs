namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using System;

    public class DownloadVoiceContextMgr : ContextMgrBase<DownloadVoiceContext>
    {
        public static DownloadVoiceContextMgr gDownloadVoiceContextMgr;
        private const string TAG = "DownloadVoiceContextMgr";

        public DownloadVoiceContext GetBySvrID(int svrID)
        {
            lock (base.mQueueLock)
            {
                if (!base.mContextQueue.isEmpty())
                {
                    for (ListNode node = base.mContextQueue._head._next; node != null; node = node._next)
                    {
                        DownloadVoiceContext context = (DownloadVoiceContext) node._obj;
                        if ((context != null) && (context.mMsgSvrID == svrID))
                        {
                            return context;
                        }
                    }
                }
                return null;
            }
        }

        public static DownloadVoiceContextMgr getInstance()
        {
            if (gDownloadVoiceContextMgr == null)
            {
                gDownloadVoiceContextMgr = new DownloadVoiceContextMgr();
            }
            return gDownloadVoiceContextMgr;
        }
    }
}

