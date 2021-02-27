namespace MicroMsg.Scene.Video
{
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using System;

    public class DownloadVideoContextMgr : ContextMgrBase<DownloadVideoContext>
    {
        public static DownloadVideoContextMgr gDownVideoContextMgr;
        private const string TAG = "DownloadVideoContextMgr";

        public DownloadVideoContext findBySrvMsgID(int srvMsgID, bool isThumbMode)
        {
            lock (base.mQueueLock)
            {
                if (!base.mContextQueue.isEmpty())
                {
                    for (ListNode node = base.mContextQueue._head._next; node != null; node = node._next)
                    {
                        DownloadVideoContext context = node._obj as DownloadVideoContext;
                        if (((context != null) && (context.mIsThumbMode == isThumbMode)) && (context.mVideoInfo.nMsgSvrID == srvMsgID))
                        {
                            return context;
                        }
                    }
                }
                return null;
            }
        }

        public static DownloadVideoContextMgr getInstance()
        {
            if (gDownVideoContextMgr == null)
            {
                gDownVideoContextMgr = new DownloadVideoContextMgr();
            }
            return gDownVideoContextMgr;
        }
    }
}

