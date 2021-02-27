namespace MicroMsg.Scene
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using System;

    public class ContextMgrBase<TContext> where TContext: IContextBase
    {
        protected Queue mContextQueue;
        protected object mQueueLock;
        private EventWatcher mWatcher;
        private const string TAG = "ContextMgrBase";

        public ContextMgrBase()
        {
            this.mContextQueue = new Queue();
            this.mQueueLock = new object();
            if (this.mWatcher == null)
            {
                this.mWatcher = new EventWatcher(this, null, new EventHandlerDelegate(this.HandlerOnLogout));
                EventCenter.registerEventWatcher(EventConst.ON_ACCOUNT_LOGOUT, this.mWatcher);
            }
        }

        public int clearnFinishedContext()
        {
            lock (this.mQueueLock)
            {
                if (this.mContextQueue.isEmpty())
                {
                    return 0;
                }
                int num = 0;
                ListNode node = this.mContextQueue._head._next;
                ListNode node2 = this.mContextQueue._head;
                while (node != null)
                {
                    TContext local = (TContext) node._obj;
                    if ((local == null) || local.needToClean())
                    {
                        node2._next = node._next;
                        this.mContextQueue._size--;
                        num++;
                    }
                    else
                    {
                        node2 = node;
                    }
                    node = node._next;
                }
                this.mContextQueue._tail = node2;
                return num;
            }
        }

        public int countRunningContext()
        {
            lock (this.mQueueLock)
            {
                if (this.mContextQueue.isEmpty())
                {
                    return 0;
                }
                int num = 0;
                for (ListNode node = this.mContextQueue._head._next; node != null; node = node._next)
                {
                    TContext local = (TContext) node._obj;
                    if ((local != null) && local.isRunning())
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public int getCount()
        {
            lock (this.mQueueLock)
            {
                return this.mContextQueue.size();
            }
        }

        public TContext getFirstContextNeedHandle()
        {
            lock (this.mQueueLock)
            {
                if (!this.mContextQueue.isEmpty())
                {
                    for (ListNode node = this.mContextQueue._head._next; node != null; node = node._next)
                    {
                        TContext local = (TContext) node._obj;
                        if ((local != null) && local.needToHandle())
                        {
                            return local;
                        }
                    }
                }
                return default(TContext);
            }
        }

        private void HandlerOnLogout(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            lock (this.mQueueLock)
            {
                Log.d("ContextMgrBase", "clearn task on logout.. " + this.mContextQueue.size());
                this.mContextQueue.reset();
            }
        }

        public void moveToTail(TContext context)
        {
            lock (this.mQueueLock)
            {
                this.mContextQueue.moveToTail(context.GetHashCode());
            }
        }

        public void putToHead(TContext context)
        {
            lock (this.mQueueLock)
            {
                this.mContextQueue.putToHead(context, context.GetHashCode());
            }
        }

        public void putToTail(TContext context)
        {
            lock (this.mQueueLock)
            {
                this.mContextQueue.putToTail(context, context.GetHashCode());
            }
        }

        public void remove(TContext context)
        {
            lock (this.mQueueLock)
            {
                this.mContextQueue.remove(context.GetHashCode());
            }
        }
    }
}

