namespace MicroMsg.Network
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using System;
    using System.Collections.Generic;

    public class Connector
    {
        public const int CONN_MODE_ALL = 3;
        public const int CONN_MODE_HTTP = 2;
        public const int CONN_MODE_NULL = 0;
        public const int CONN_MODE_TCP = 1;
        private static ConntectorStatus mConnectorStatus = ConntectorStatus.Disconnect;
        public static double mLastActiveTimestamp = 0.0;
        public static double mLastShortKeepAliveTimestamp = 0.0;
        private static List<RawPackBody> mListRecvPack = new List<RawPackBody>();

        private static void checkConnectStatus()
        {
            //if (!NetworkDeviceWatcher.isNetworkAvailable())
            //{
            //    setConnectorStatus(ConntectorStatus.Disconnect);
            //}
            //else 
            if (!LongConnector.isAvailable() || (mConnectorStatus == ConntectorStatus.Disconnect))
            {
                if (ShortConnector.isAvailable())
                {
                    setConnectorStatus(ConntectorStatus.Connected);
                }
                else
                {
                    setConnectorStatus(ConntectorStatus.Disconnect);
                }
            }
        }

        private static void checkNeedKeepAliveInShortConn(double interval)
        {//NetworkDeviceWatcher.isNetworkAvailable() && 
            if ((!LongConnector.isAvailable()) && ShortConnector.isValid())
            {
                double num = Util.getNowMilliseconds();
                if (mLastShortKeepAliveTimestamp == 0.0)
                {
                    mLastShortKeepAliveTimestamp = num;
                }
                if (num >= (mLastShortKeepAliveTimestamp + interval))
                {
                    Log.i("Network", "need keep alive in short connnector mode, post event to sync.");
                    EventCenter.postEvent(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, null, null);
                    mLastShortKeepAliveTimestamp = num;
                }
            }
        }

        public static void checkReady()
        {
            checkConnectStatus();
            checkNeedKeepAliveInShortConn(60000.0);
            LongConnector.checkReady();
            ShortConnector.checkReady();
        }

        public static void close()
        {
            Log.i("Network", "close connector. ");
            LongConnector.close();
            ShortConnector.close();
            onPrepareSend(false);
        }

        public static ConntectorStatus getConnectorStatus()
        {
            return mConnectorStatus;
        }

        public static bool isValidforPack(SessionPack sessionPack)
        {
            if (!LongConnector.isAvailable())
            {
                sessionPack.mConnectMode = 2;
                sessionPack.mCacheBodyBuffer = null;
            }
            int mConnectMode = sessionPack.mConnectMode;
            if (mConnectMode == 1)
            {
                return LongConnector.isValid();
            }
            if (mConnectMode != 2)
            {
                return false;
            }
            if (string.IsNullOrEmpty(sessionPack.mCmdUri))
            {
                Log.e("Network", "Cannot send pack via http without uri,  will been cancelled. seq= " + sessionPack.mSeqID);
                sessionPack.mCanceled = true;
                return false;
            }
            return ShortConnector.isValid();
        }

        public static void onPrepareSend(bool needCheckAlive)
        {
            NetHandler.startup();
            LongConnector.onPrepare();
            ShortConnector.onPrepare();
            if (needCheckAlive)
            {
                checkNeedKeepAliveInShortConn(0.0);
            }
        }

        public static void onSendFailed(bool needReconnect, int connMode)
        {
            if ((connMode & 1) != 0)
            {
                LongConnector.onSendFailed(needReconnect);
            }
            if ((connMode & 2) != 0)
            {
                ShortConnector.onSendFailed(needReconnect);
            }
        }

        public static void printInfo()
        {
            //NetworkDeviceWatcher.printfInfo();
            //Log.i("Network", string.Concat(new object[] { "connector status = ", mConnectorStatus, ", network available = ", NetworkDeviceWatcher.isNetworkAvailable(), ", longconn available = ", LongConnector.isAvailable() }));
        }

        public static List<RawPackBody> receiveBodyList()
        {
            mListRecvPack.Clear();
            LongConnector.receive(mListRecvPack);
            ShortConnector.receiveList(mListRecvPack);
            if (mListRecvPack.Count > 0)
            {
                mLastActiveTimestamp = SessionPack.getNow();
            }
            return mListRecvPack;
        }

        public static bool sendPack(SessionPack sessionPack)
        {
            mLastActiveTimestamp = SessionPack.getNow();
            switch (sessionPack.mConnectMode)
            {
                case 1:
                    return LongConnector.sendPack(sessionPack);

                case 2:
                    return ShortConnector.sendPack(sessionPack);
            }
            return true;
        }

        public static void setConnectorStatus(ConntectorStatus status)
        {
            if (status != mConnectorStatus)
            {
                mConnectorStatus = status;
                if (status == ConntectorStatus.Connected)
                {
                    EventCenter.postEvent(EventConst.ON_NET_MM_NET_CONNECTED, null, null);
                }
                else if (status == ConntectorStatus.Connecting)
                {
                    EventCenter.postEvent(EventConst.ON_NET_MM_NET_CONNECTING, null, null);
                }
                else if (status == ConntectorStatus.Disconnect)
                {
                    EventCenter.postEvent(EventConst.ON_NET_MM_NET_DISCONNECT, null, null);
                }
            }
        }

        public class ConnectorContext
        {
            public int mCmd;
            public string mCmdUri;
            public int mConnectMode = 1;
            public HttpClient mHttpClient;
            public int mSeq;
        }

        public class RawPackBody
        {
            public byte[] body;
            public int cmd;
            public string cmdUri;
            public int seq;
        }
    }
}

