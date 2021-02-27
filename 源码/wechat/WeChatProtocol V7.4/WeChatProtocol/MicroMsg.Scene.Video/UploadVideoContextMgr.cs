namespace MicroMsg.Scene.Video
{
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using System;

    public class UploadVideoContextMgr : ContextMgrBase<UploadVideoContext>
    {
        public static UploadVideoContextMgr gContextMgr;

        public UploadVideoContext findByClientMsgID(string clientMsgID)
        {
            lock (base.mQueueLock)
            {
                if (!base.mContextQueue.isEmpty())
                {
                    for (ListNode node = base.mContextQueue._head._next; node != null; node = node._next)
                    {
                        UploadVideoContext context = node._obj as UploadVideoContext;
                        if ((context != null) && context.mVideoTrans.strClientMsgId.Equals(clientMsgID))
                        {
                            return context;
                        }
                    }
                }
                return null;
            }
        }

        public static UploadVideoContextMgr getInstance()
        {
            if (gContextMgr == null)
            {
                gContextMgr = new UploadVideoContextMgr();
            }
            return gContextMgr;
        }
    }
}

