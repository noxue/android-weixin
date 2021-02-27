namespace MicroMsg.Network
{
    using MicroMsg.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;

    public class ShortConnector
    {
        private const int ERROR_COUNT_MAX = 3;
        private static int mFailedCount;

        public static bool checkReady()
        {
            return true;
        }

        public static void close()
        {
            SessionPackMgr.closeAllHttpClientInPacks();
        }

        public static bool isAvailable()
        {
            return (mFailedCount == 0);
        }

        public static bool isValid()
        {
            return true;
        }

        public static void onPrepare()
        {
        }

        public static void onSendFailed(bool needReconnect)
        {
            mFailedCount++;
            if (mFailedCount > 3)
            {
                LongConnector.setAvailable(true);
            }
            close();
            HostService.mShortConnHosts.onCurrentHostFailed();
        }

        public static bool receiveList(List<Connector.RawPackBody> listPack)
        {
            SessionPackMgr.getPackWithHttpClient(delegate (SessionPack sessionPack) {
                int seq = 0;
                byte[] outBuf = null;
                string cmdUri = null;
                SocketError error = sessionPack.mHttpClient.receive(ref outBuf, ref seq, ref cmdUri);
                if (error == ((SocketError) ((int) SocketError.NoData)))
                {
                    return false;
                }
                if (error == ((SocketError) ((int) SocketError.SocketError)))
                {
                    sessionPack.mHttpClient.close();
                    sessionPack.mHttpClient = null;
                    return false;
                }
                if (((outBuf == null) || (outBuf.Length < 2)) )
                {
                    Log.e("Network", "received invalid reponse body, seq = " + seq);
                    sessionPack.mHttpClient.close();
                    sessionPack.mHttpClient = null;
                    return false;
                }
                Connector.RawPackBody item = new Connector.RawPackBody {
                    body = outBuf,
                    cmd = sessionPack.mCmdID,
                    seq = seq
                };
                listPack.Add(item);
                mFailedCount = 0;
                sessionPack.mHttpClient.close();
                sessionPack.mHttpClient = null;
                HostService.mShortConnHosts.onCurrentHostSuccess();
                return true;
            });
            return true;
        }

        public static bool sendPack(SessionPack sessionPack)
        {
            HostService.HostInfo info = HostService.mShortConnHosts.getAvailableHost();
            string cmdUri = string.Concat(new object[] { "http://", info.mHost, ":", info.mPort, sessionPack.mCmdUri });
            string mHost = info.mHost;
            HttpClient client = new HttpClient(cmdUri, sessionPack.mSeqID, mHost);
            sessionPack.mHttpClient = client;
            //FlowControl.onSend(sessionPack.mCmdID, sessionPack.mCmdUri, (uint) sessionPack.mCacheBodyBuffer.Length);

            //if (sessionPack.mCmdUri == "/cgi-bin/micromsg-bin/getchatroommemberdetail") {

               


            //    byte[] data = new byte[0];
            //    using (WebClient _client = new WebClient())
            //    {
                 
            //        data = _client.UploadData(cmdUri, "POST", sessionPack.mCacheBodyBuffer);

            //        Log.e("Network", "  send body = " + Util.byteToHexStr(sessionPack.mCacheBodyBuffer) + " revce length \n" + data.Length+" data \n"+Util.byteToHexStr(data)+" \nKey "+ Util.byteToHexStr( SessionPackMgr.getSessionKeyEx()));
            //    }
            //    // GetChatroomMemberDetailResponse response = GetChatroomMemberDetailResponse.ParseFrom(data);


            //}
            bool flag = client.send(sessionPack.mCacheBodyBuffer, sessionPack.mCacheBodyBuffer.Length, null, sessionPack.mSeqID);
            sessionPack.timeInSent = Util.getNowMilliseconds();
            return flag;
        }
    }
}

