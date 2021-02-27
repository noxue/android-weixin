namespace MicroMsg.Network
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    public class LongConnector
    {
        private const int CONN_CONNECT_FAIL_MAX = 3;
        private const int CONN_RETRY_MAX = 3;
        private const int CONN_SEND_FAIL_MAX = 3;
        private static bool mAvailableConnect = true;
        private static int mConnFailedCount = 0;
        private static bool mEnableConnect = false;
        public static double mFirstKeepAlivePointAfterConnected = 0.0;
        public static double mLastConnectedTimestamp = 0.0;
        public static double mLastConnTimestamp = 0.0;
        private static double mLastUnAvailableTimestamp = 0.0;
        private static double mNextHelloTimestamp = 0.0;
        private static int mReconnectLeft = 3;
        private static int mSendFailedCount = 0;
        public static SocketClient mSocketClient = null;

        public static bool checkKeepAlive()
        {
            if (mNextHelloTimestamp == 0.0)
            {
                return false;
            }
            if (SessionPack.getNow() < mNextHelloTimestamp)
            {
                return false;
            }
            if (!SessionPackMgr.getAccount().isValid() || !SessionPackMgr.isValidSessionKey())
            {
                stopKeepAlivePoint();
                return false;
            }
            if (mFirstKeepAlivePointAfterConnected == mNextHelloTimestamp)
            {
                EventCenter.postEvent(EventConst.ON_NET_MM_NET_NOOP, null, null);
            }
            else
            {
                EventCenter.postEvent(EventConst.ON_NET_MM_NET_NOOP, null, null);
            }
            restartKeepAlivePoint(0xea60);
            return true;
        }

        public static bool checkReady()
        {
            if (mEnableConnect)
            {
                //if (!NetworkDeviceWatcher.isNetworkAvailable())
                //{
                //    return false;
                //}
                if (!isAvailable())
                {
                    if (Util.getNowMilliseconds() < (mLastUnAvailableTimestamp + 600000.0))
                    {
                        return false;
                    }
                    Log.e("Network", "network device is available, but longconn is not available, try longconn per 10mintues...");
                    setAvailable(true);
                }
                if (checkReadyEx())
                {
                    checkKeepAlive();
                    return true;
                }
            }
            return false;
        }

        private static bool checkReadyEx()
        {
            if (isValid())
            {
                if ((mLastConnectedTimestamp != 0.0) && (SessionPack.getNow() > (mLastConnectedTimestamp + 30000.0)))
                {
                    EventCenter.postEvent(EventConst.ON_NET_LONGCONN_KEEPALIVE, null, null);
                    mLastConnectedTimestamp = 0.0;
                }
                return true;
            }
            if (mReconnectLeft <= 0)
            {
                if (mReconnectLeft == 0)
                {
                    Log.e("Network", "no chance to try connect.");
                    mReconnectLeft = -1;
                }
                return false;
            }
            double num = SessionPack.getNow();
            double num2 = mLastConnTimestamp + getSleepTime(mReconnectLeft);
            if ((mReconnectLeft == 3) || (num >= num2))
            {
                mLastConnTimestamp = num;
                mReconnectLeft--;
                Connector.setConnectorStatus(ConntectorStatus.Connecting);
                mSocketClient = new SocketClient();
                HostService.HostInfo info = HostService.mLongConnHosts.getAvailableHost();
                SocketError error = mSocketClient.Connect(info.mHost, info.mPort);
                if (error == ((SocketError) ((int) SocketError.Success)))
                {
                    Log.i("Network", "connect success.");
                    mLastConnectedTimestamp = num;
                    //NetworkDeviceWatcher.onConnected();
                    mReconnectLeft = 2;
                    mFirstKeepAlivePointAfterConnected = restartKeepAlivePoint(0xbb8);
                    mConnFailedCount = 0;
                    Connector.setConnectorStatus(ConntectorStatus.Connected);
                    return true;
                }
                if (error == ((SocketError) ((int) SocketError.HostNotFound)))
                {
                    HostService.setDNSFailed();
                }
                Log.e("Network", "connect error:" + error.ToString());
                close();
                mConnFailedCount++;
                if (mConnFailedCount >= 3)
                {
                    Log.i("Network", "longconnect is not available as connect failed(3/3).");
                    setAvailable(false);
                    mConnFailedCount = 0;
                }
                HostService.mLongConnHosts.onCurrentHostFailed();
                Connector.setConnectorStatus(ConntectorStatus.Disconnect);
            }
            return false;
        }

        public static void close()
        {
            if (mSocketClient != null)
            {
                mSocketClient.Close();
                mSocketClient = null;
                Connector.setConnectorStatus(ConntectorStatus.Disconnect);
            }
            //NetworkDeviceWatcher.onConnnectClosed();
            stopKeepAlivePoint();
        }

        public static Socket getRawSocket()
        {
            if (!isValid())
            {
                return null;
            }
            return mSocketClient.mSocket;
        }

        private static double getSleepTime(int reconLeft)
        {
            switch (reconLeft)
            {
                case 1:
                    return 25000.0;

                case 2:
                    return 15000.0;

                case 3:
                    return 0.0;
            }
            return 40000.0;
        }

        public static bool isAvailable()
        {
            return mAvailableConnect;
        }

        public static bool isValid()
        {
            if (mSocketClient == null)
            {
                return false;
            }
            return mSocketClient.isConnect();
        }

        public static void onPrepare()
        {
            mReconnectLeft = 3;
        }

        private static bool onReadSocket(byte[] recvBuffer, int recvCount, ref byte[] outBody, ref int outSeq, ref int outCmd)
        {
            NetPack.Response response = new NetPack.Response();
            if (!response.unserializeFromBuffer(recvBuffer, recvCount))
            {
                Log.e("Network", "longconn. onReadSocket:error in unserializeFromBuffer ");
                return false;
            }
            restartKeepAlivePoint(0xea60);
            int seq = response.getHead().seq;
            int opCmd = response.getHead().opCmd;
            response.getLength();
            outSeq = seq;
            outCmd = opCmd;
            outBody = new byte[response.getBody().Length];
            Buffer.BlockCopy(response.getBody(), 0, outBody, 0, response.getBody().Length);
            return true;
        }

        public static void onSendFailed(bool needReconnect)
        {
            close();
            mSendFailedCount++;
            if (mSendFailedCount >= 3)
            {
                mSendFailedCount = 0;
                setAvailable(false);
                Log.i("Network", "longconnect is not available as send failed(3/3).");
            }
            HostService.mLongConnHosts.onCurrentHostFailed();
            if (!needReconnect)
            {
                mReconnectLeft = 0;
            }
        }

        public static bool receive(List<Connector.RawPackBody> listPack)
        {
            byte[] outBuf = null;
            int offset = 0;
            int count = 0;
            if (!receiveFromSocket(ref outBuf, ref offset, ref count))
            {
                return false;
            }
            byte[] outBody = null;
            int outSeq = 0;
            int outCmd = 0;
            if (!onReadSocket(outBuf, count, ref outBody, ref outSeq, ref outCmd))
            {
                return false;
            }
            Connector.RawPackBody item = new Connector.RawPackBody {
                body = outBody,
                cmd = outCmd,
                seq = outSeq
            };
            //string aa = Util.byteToHexStr(outBuf);
            listPack.Add(item);
            return true;
        }

        private static bool receiveFromSocket(ref byte[] outBuf, ref int offset, ref int count)
        {
            if (!isValid())
            {
                return false;
            }
            SocketError error = mSocketClient.Receive(ref outBuf, ref offset, ref count);
            if (error != ((SocketError) ((int) SocketError.Success)))
            {
                Log.e("Network", "longconn.Socket error in receive: " + error.ToString());
            }
            if ((error == ((SocketError) ((int) SocketError.ConnectionAborted))) || (error == ((SocketError) ((int) SocketError.ConnectionReset))))
            {
                Log.e("Network", "longconn.Socket closeed " );
                close();
                return false;
            }
            if ((error != ((SocketError) ((int) SocketError.Success))) || (count == 0))
            {
                return false;
            }
            mSendFailedCount = 0;
            setAvailable(true);
            HostService.mLongConnHosts.onCurrentHostSuccess();
            return true;
        }

        public static double restartKeepAlivePoint(int interval)
        {
            mNextHelloTimestamp = SessionPack.getNow() + interval;
            return mNextHelloTimestamp;
        }

        public static void sendHelloPack()
        {
            Log.i("Network", "send hello pack... ");
            SessionPack sessionPack = new SessionPack {
                mCmdID = 6,
                mCacheBodyBuffer = new byte[0],
                mRetryLeft = 1
            };
            SessionPackMgr.putToTail(sessionPack);
        }

        public static bool sendPack(SessionPack sessionPack)
        {
            if (mSocketClient == null)
            {
                return false;
            }
            NetPack.Request request = new NetPack.Request(sessionPack.mSeqID, sessionPack.mCmdID, sessionPack.mCacheBodyBuffer);

           // NetPack.Request request = new NetPack.Request(1, 701, Util.HexStringToByte("bf8e7f1603181100000000000000000000000000000000000000bd05b007b00787010d0000012b0000024c000002004b8116d636cdcf7c1eac04e1427a78c784acc68ed332c2475ae778918ef43170db0d46c3c7f7bacf1c14f2385438cb8e066083dd24d355d93cea6bf159ea20a309be99a125f26f8e8726504e8616533924faebd1ef985bf05876ac59513b1eca83696d87b1f7519754176fbf12e9f51e2e913469884f48419003e747416c7985d12abfad19b15bc3048322aea2ad801a2f8390262aab19a73bcd34199e0993c1659bad1d4859cb137892c179d536823e4418a4e8a06835a143b0416dda924e14f91a90567db32c0731f94844515779080789092025cdfbe12565a306b546c3daa2996e1670aa65456292c09d29f541cd2fe2fb8d9ad155f8b1824fad1037e7d058a1d36a69a8236f0230400316e98e314916fb197571e349a058a1d0ba7f36285a5aae4b54d4af84cd4579bfe1ceaa10a0d55875be281a56f98cb8fb59a51d58ad39d20f8513a8a14728409e9113cca6a90be4074b2f35000b45df07dc5d13fdc53a10793feb45f1ac1cbb56c3b744fe8dc7f1d8f2decd8886d324ddee3d58e4e5510042e4b319b93deae768cdd8d7f39fef00656e1f665e80e281ffe4f424a8d466309824a8479edc67693f8e769e5c97d7778c63a95507d60a901c1890fa9a1c6f5d7cb4fda44f454e81b3fc60c7c7582e2f888cde6f41a8c657566f8dbcdee4e97ba6f8681448e3c93308837cd5c6597e20d03757f5a24a5ff6bc4032566c14c753cef9f0b17e70fb1f96e75423b325badf3fdfff4c9d554b4ed29ff21c7545150190aa44b2a60c74634081c17f9a3ce36d299b7b04dc0c09f85ca71b61be02baee13841992b284ca5cd8fe0e6608d0158fcdb3b60706cef28350fd5998fddd19b9b4ccb661c7f4f5a9c80906dee68cb303da3519b5a1b588a6bc47af60cdf6f38cf28bbb1d0572bd23d6250fc15a64c39aabf50463b2e9d9b3d3059bff1466f137ffb62ebee8d29fe85f38d923b17f0c259e0dbb75c1a61f57f3e9006d6a9ae8159398c1883ab421174ebe646cce7952a65d09c1285288bb6132efc997846b6879a47448785eddac3e7ad705b020325b5faa19daeab8544e0b1a149b657a5f533d6ba31164c3a0203d6414496b098e77011602368b585527c7badc5f27df367bcc043718d3e613b5a0cdbe90eccb271f3d7bfc2393575df718c40b2d30263558a18edda94d12e50c770920223fe0b54c7b43354f052104db5364d1d825035d5da36a015b86fdf26301d0d214d717d14c9ff499a26e4e31976ff5c0b28c841ea0a42762638f245c12774c0f59b8a659f0c1a425aa1fbecfeb0648afc020fbf078a268a50c894b9674f6de085cfc2e"));

            byte[] outBuf = null;
            request.serializeToBuffer(ref outBuf);
            //FlowControl.onSend(sessionPack.mCmdID, sessionPack.mCmdUri, (uint) outBuf.Length);
            SocketError error = mSocketClient.Send(outBuf);
           // string a = Util.byteToHexStr(outBuf);
            sessionPack.timeInSent = Util.getNowMilliseconds();
            if (error != ((SocketError) ((int) SocketError.Success)))
            {
                Log.e("Network", "Socket Send error : " + error.ToString());
                if ((error == ((SocketError) ((int) SocketError.ConnectionAborted))) || (error == ((SocketError) ((int) SocketError.ConnectionReset))))
                {
                    close();
                }
                return false;
            }
            restartKeepAlivePoint(0xea60);
            return true;
        }

        public static void setAvailable(bool available)
        {
            if (mAvailableConnect != available)
            {
                mAvailableConnect = available;
                if (!available)
                {
                    mLastUnAvailableTimestamp = Util.getNowMilliseconds();
                }
            }
        }

        public static void setEnable(bool enable)
        {
            mEnableConnect = enable;
        }

        public static void stopKeepAlivePoint()
        {
            mNextHelloTimestamp = 0.0;
        }
    }
}

