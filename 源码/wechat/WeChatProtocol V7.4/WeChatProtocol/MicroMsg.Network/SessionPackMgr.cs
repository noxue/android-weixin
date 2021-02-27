namespace MicroMsg.Network
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Utils;
    using System;

    public class SessionPackMgr
    {
        private static AccountInfo mAccountInfo = new AccountInfo();
        private static int mAuthStatus = 0;
        public static string mAuthTicket1 = null;
        public static string mAuthTicket2 = null;
        private static byte[] mSessionKey = null;
        private static object mSessionKeyLock = new object();
        private static object mSessionPackLock = new object();
        private static Queue mSessionPackQueue = new Queue();
        private static object mSeverIDLock = new object();

        public static void cancelAllPacket()
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        pack.mCanceled = true;
                    }
                }
            }
        }

        public static void changeSessionPackSeq(int oldSeq, int newSeq)
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    mSessionPackQueue.changeID(oldSeq, newSeq);
                }
            }
        }

        public static void checkPackTimeout()
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    ListNode node = mSessionPackQueue._head._next;
                    double now = Util.getNowMilliseconds();
                    while (node != null)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        if ((pack != null) && (pack.mSendStatus <= 3))
                        {
                            pack.checkPackTimeout(now);
                        }
                        node = node._next;
                    }
                }
            }
        }

        public static void checkSendTimeout()
        {
            SessionPack sessionPack = checkSendTimeoutEx();
            if (sessionPack != null)
            {
                Sender.getInstance().onSendTimeout(sessionPack);
            }
        }

        private static SessionPack checkSendTimeoutEx()
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        if (pack.isSendTimeout())
                        {
                            return pack;
                        }
                    }
                }
                return null;
            }
        }

        public static void cleanAllTimeoutPoint(int connMode)
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        if ((pack.mConnectMode & connMode) != 0)
                        {
                            pack.cancelTimeoutPoint();
                        }
                    }
                }
            }
        }

        public static int clearCompletedOrCancel()
        {
            lock (mSessionPackLock)
            {
                if (mSessionPackQueue.isEmpty())
                {
                    return 0;
                }
                int num = 0;
                ListNode node = mSessionPackQueue._head._next;
                ListNode node2 = mSessionPackQueue._head;
                while (node != null)
                {
                    SessionPack sessionPack = node._obj as SessionPack;
                    if ((sessionPack.mSendStatus >= 5) || sessionPack.mCanceled)
                    {
                        if ((sessionPack.mSendStatus < 5) && sessionPack.mCanceled)
                        {
                            OnCallback.onError(sessionPack, PackResult.BEEN_CANCELLED);
                        }
                        node2._next = node._next;
                        mSessionPackQueue._size--;
                        num++;
                    }
                    else
                    {
                        node2 = node;
                    }
                    node = node._next;
                }
                mSessionPackQueue._tail = node2;
                return num;
            }
        }

        public static void closeAllHttpClientInPacks()
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        if (pack.mHttpClient != null)
                        {
                            pack.mHttpClient.close();
                            pack.mHttpClient = null;
                        }
                    }
                }
            }
        }

        public static AccountInfo getAccount()
        {
            return mAccountInfo;
        }

        public static int getAuthStatus()
        {
            return mAuthStatus;
        }

        public static SessionPack getFirstNotSended()
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        if ((pack.mSendStatus == 0) && !pack.mCanceled)
                        {
                            return pack;
                        }
                    }
                }
                return null;
            }
        }

        public static void getPackEx(Func<SessionPack, bool> predicate)
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack arg = node._obj as SessionPack;
                        if (arg != null)
                        {
                            predicate(arg);
                        }
                    }
                }
            }
        }

        public static void getPackWithHttpClient(Func<SessionPack, bool> predicate)
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack arg = node._obj as SessionPack;
                        if (arg.mHttpClient != null)
                        {
                            predicate(arg);
                        }
                    }
                }
            }
        }

        public static byte[] getSessionKey()
        {
            if (mSessionKey == null)
            {
                return null;
            }
            return getSessionKeyEx();
        }

        public static byte[] getSessionKeyEx()
        {
            lock (mSessionKeyLock)
            {
                byte[] l = new byte[0x24];
                Arrays.fill(l, 0);
                if (mSessionKey != null)
                {
                    Buffer.BlockCopy(mSessionKey, 0, l, 0, 0x24);
                }
                return l;
            }
        }

        public static SessionPack getSessionPackBySeq(int seq)
        {
            lock (mSessionPackLock)
            {
                if (mSessionPackQueue.isEmpty())
                {
                    return null;
                }
                return (mSessionPackQueue.get(seq) as SessionPack);
            }
        }

        public static byte[] getSeverID()
        {
            lock (mSeverIDLock)
            {
                int length = mAccountInfo.getCookie().Length;
                byte[] l = new byte[length];
                Arrays.fill(l, 0);
                Buffer.BlockCopy(mAccountInfo.getCookie(), 0, l, 0, length);
                return l;
            }
        }

        public static bool hasSessionPack()
        {
            lock (mSessionPackLock)
            {
                if (mSessionPackQueue.isEmpty())
                {
                    return false;
                }
                return true;
            }
        }

        public static bool isAuthing()
        {
            return (mAuthStatus == 1);
        }

        public static bool isValidSessionKey()
        {
            lock (mSessionKeyLock)
            {
                return (mSessionKey != null);
            }
        }

        public static void markAllToNotSended(int connMode)
        {
            lock (mSessionPackLock)
            {
                if (!mSessionPackQueue.isEmpty())
                {
                    for (ListNode node = mSessionPackQueue._head._next; node != null; node = node._next)
                    {
                        SessionPack pack = node._obj as SessionPack;
                        if ((pack.mSendStatus <= 3) && ((pack.mConnectMode & connMode) != 0))
                        {
                            pack.mSendStatus = 0;
                        }
                    }
                }
            }
        }

        public static void putToHead(SessionPack sessionPack)
        {
            lock (mSessionPackLock)
            {
                mSessionPackQueue.putToHead(sessionPack, sessionPack.mSeqID);
            }
        }

        public static void putToTail(SessionPack sessionPack)
        {
            lock (mSessionPackLock)
            {
                mSessionPackQueue.putToTail(sessionPack, sessionPack.mSeqID);
            }
        }

        public static void reset()
        {
            lock (mSessionPackLock)
            {
                mSessionPackQueue.reset();
            }
        }
        public static int queueCount()
        {
            lock (mSessionPackLock)
            {
               return mSessionPackQueue.size();
            }
        }
        public static void setAuthStatus(int sts)
        {
            mAuthStatus = sts;
        }

        public static void setSessionKey(byte[] key)
        {
            lock (mSessionKeyLock)
            {
                if (key == null)
                {
                    mSessionKey = null;
                }
                else
                {
                    if (mSessionKey == null)
                    {
                        mSessionKey = new byte[0x24];
                        Arrays.fill(mSessionKey, 0);
                    }
                    int length = key.Length;
                    if (length > 0x24)
                    {
                        length = 0x24;
                    }
                    Buffer.BlockCopy(key, 0, mSessionKey, 0, length);
                }
            }
        }

        public static void setSeverID(byte[] serverID)
        {
            lock (mSeverIDLock)
            {
                byte[] dst = new byte[serverID.Length];
                Buffer.BlockCopy(serverID, 0, dst, 0, serverID.Length);
                mAccountInfo.setCookie(dst);
            }
        }
    }
}

