namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using System;

    public class UploadVoiceContextMgr : ContextMgrBase<UploadVoiceContext>
    {
        public static UploadVoiceContextMgr gUpVoiceContextMgr;
        private const string TAG = "UploadVoiceContextMgr";

        public UploadVoiceContext findByClientMsgID(string clientMsgID)
        {
            lock (base.mQueueLock)
            {
                if (!base.mContextQueue.isEmpty())
                {
                    for (ListNode node = base.mContextQueue._head._next; node != null; node = node._next)
                    {
                        UploadVoiceContext context = node._obj as UploadVoiceContext;
                        if (context.mClientMsgId.Equals(clientMsgID))
                        {
                            return context;
                        }
                    }
                }
                return null;
            }
        }

        public static UploadVoiceContextMgr getInstance()
        {
            if (gUpVoiceContextMgr == null)
            {
                gUpVoiceContextMgr = new UploadVoiceContextMgr();
            }
            return gUpVoiceContextMgr;
        }
    }
}

