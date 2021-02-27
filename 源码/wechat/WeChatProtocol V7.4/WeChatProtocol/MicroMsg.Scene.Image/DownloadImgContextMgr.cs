namespace MicroMsg.Scene.Image
{
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using System;

    public class DownloadImgContextMgr : ContextMgrBase<DownloadImgContext>
    {
        public static DownloadImgContextMgr gDownImgContextMgr;
        private const string TAG = "DownloadImgContextMgr";

        public DownloadImgContext getContextBySvrid(int svrID)
        {
            lock (base.mQueueLock)
            {
                if (!base.mContextQueue.isEmpty())
                {
                    for (ListNode node = base.mContextQueue._head._next; node != null; node = node._next)
                    {
                        DownloadImgContext context = (DownloadImgContext) node._obj;
                        if ((context != null) && (context.mImgInfo.nMsgSvrID == svrID))
                        {
                            return context;
                        }
                    }
                }
                return null;
            }
        }

        public static DownloadImgContextMgr getInstance()
        {
            if (gDownImgContextMgr == null)
            {
                gDownImgContextMgr = new DownloadImgContextMgr();
            }
            return gDownImgContextMgr;
        }

        public bool isMsgSvrIDInQueue(int svrID)
        {
            lock (base.mQueueLock)
            {
                if (!base.mContextQueue.isEmpty())
                {
                    for (ListNode node = base.mContextQueue._head._next; node != null; node = node._next)
                    {
                        DownloadImgContext context = (DownloadImgContext) node._obj;
                        if ((context != null) && (context.mImgInfo.nMsgSvrID == svrID))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}

