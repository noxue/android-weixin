namespace MicroMsg.Network
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Receiver
    {
        public static Receiver gReceiver;
    

        public static Receiver getInstance()
        {
            if (gReceiver == null)
            {
                gReceiver = new Receiver();
            }
            return gReceiver;
        }

        public bool handler()
        {
            //if (!NetworkDeviceWatcher.isNetworkAvailable())
            //{
            //    return false;
            //}
            List<Connector.RawPackBody> list = Connector.receiveBodyList();
            if ((list == null) || (list.Count <= 0))
            {
                return false;
            }
            foreach (Connector.RawPackBody body in list)
            {
                this.onReceivedResponsePack(body.body, body.seq, body.cmd, body.cmdUri);
            }
            return true;
        }

        private void onReceivedNotify(int seq, int cmd, byte[] data)
        {
            Log.w("Network", string.Concat(new object[] { "notify received, cmd=", (CmdConst)cmd, ", datalen=", data.Length }));
            int num3 = cmd;
            if (num3 <= 0x18)
            {
                switch (num3)
                {
                    case 5:
                    case 0x18:
                        if ((data != null) && (data.Length >= 4))
                        {
                            int offset = 0;
                            int num2 = Util.readInt(data, ref offset);
              
                            EventCenter.postEvent(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, num2, null);
                     
                            return;
                        }
                        EventCenter.postEvent(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, null, null);
                        return;

                    case 6:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        return;

                    case 7:
                        EventCenter.postEvent(EventConst.SYS_MSG_EXIT, null, null);
                        return;

                    case 12:
                        Connector.close();
                        return;
                }
            }
            else
            {
                switch (num3)
                {
                    case 0x3d:
                        // EventCenter.postEvent(EventConst.ON_VOIP_NOTIFYSYNC, data, null);
                        break;
                    case 318:
                        // EventCenter.postEvent(EventConst.ON_VOIP_NOTIFYSYNC, data, null);
                        Log.w("Network", string.Concat(new object[] { "encode notify received, cmd=", (CmdConst)cmd, ", data", Util.byteToHexStr(data) }));
                        decodeNotifyData(data);
                        break;
                    case 0x48:
                        ServerBulletin.onRecvBulletin(data);
                        return;

                    default:
                        if (num3 == 120)
                        {
                            //EventCenter.postEvent(EventConst.ON_VOIP_NOTIFYINVITE, data, null);
                        }
                        return;
                }
            }
        }

        private void decodeNotifyData(byte[] encryptData)
        {
            uint encryptAlgo = Util.ReadProtoInt(encryptData, 1);
            uint compressAlgo = Util.ReadProtoInt(encryptData, 4);
            byte[] cipherText = Util.ReadProtoRawData(encryptData, 8);
            if (encryptAlgo == 5)
            {
                int Salt = (int)Util.ReadProtoInt(encryptData, 3);
                byte[] sencryptSalt = TLVUtil.int2byte(Salt, 4, false);
                byte[] sessionKey = SessionPackMgr.getAccount().SessionKey;
                byte[] decodeKey = new byte[20];

                Buffer.BlockCopy(sessionKey, 0, decodeKey, 0, 16);
                Buffer.BlockCopy(sencryptSalt, 0, decodeKey, 16, sencryptSalt.Length);

                byte[] decodeAesKey = MD5Core.GetHash(decodeKey);
                // Log.w("Network", string.Concat(new object[] { "encode notify salt ", Util.byteToHexStr(sencryptSalt), "SessionKey ", Util.byteToHexStr(sessionKey), "decode Key ", Util.byteToHexStr(decodeAesKey) }));
                byte[] decryptedData = Util.AESDecrypt(cipherText, decodeAesKey);

                if (compressAlgo == 1)
                {

                    Zlib.Decompress(decryptedData, decryptedData.Length, ref decryptedData);
                }

                Log.w("Network", string.Concat(new object[] { "decode notify result ", Util.byteToHexStr(decryptedData) }));

                cipherText = Util.ReadProtoRawData(decryptedData, 1);
                cipherText = Util.ReadProtoRawData(cipherText, 1);
                string ChatRoomId = Encoding.UTF8.GetString(cipherText);
                //  uint newMsgid = Util.ReadProtoInt(decryptedData, 2);

                uint newMsgSeq = Util.ReadProtoInt(decryptedData, 3);
                cipherText = Util.ReadProtoRawData(decryptedData, 6);
                cipherText = Util.ReadProtoRawData(cipherText, 1);
                string Contact = Encoding.UTF8.GetString(cipherText);

                uint msgType = Util.ReadProtoInt(decryptedData, 8);


                Log.w("Network", string.Concat(new object[] { "decode notify data ", " ChatRoomId ", ChatRoomId, " newMsgSeq ", newMsgSeq, "MsgType ", msgType, " Contact ", Contact }));


                new NetSceneGetChatRoomMsg().doScene(ChatRoomId, newMsgSeq);




            }

        }

        private void onReceivedResponsePack(byte[] body, int seq, int cmd, string cmdUri)
        {
            Log.i("Network", string.Concat(new object[] { "receive a packet , cmd = ", (CmdConst)cmd, ",seq = ", seq, " , len = ", body.Length }));
            SessionPack sessionPack = SessionPackMgr.getSessionPackBySeq(seq);
            if (sessionPack == null)
            {
                //FlowControl.onReceive(cmd, "", (uint) body.Length);
                this.onReceivedNotify(seq, cmd, body);
            }
            else
            {
                sessionPack.timeInRecvEnd = Util.getNowMilliseconds();
                sessionPack.cancelTimeoutPoint();
                sessionPack.mSendStatus = 4;
                if (sessionPack.mCmdID == 6)
                {
                    sessionPack.mSendStatus = 5;
                }
                else if (sessionPack.mCmdID == 0x19)
                {
                    sessionPack.mSendStatus = 5;
                    sessionPack.mResponseBuffer = body;
                    OnCallback.onSuccess(sessionPack);
                }
                else
                {
                    if (sessionPack.mCmdID == 1 || sessionPack.mCmdID == 0xb2)
                    {
                        SessionPackMgr.setAuthStatus(0);
                    }
                    if (sessionPack.mCanceled)
                    {
                        Log.i("Network", "receive a packet been cancelled.");
                    }
                    else
                    {
                        byte[] key = sessionPack.getSessionKey(false);
                        if (key == null)
                        {
                            Log.e("Network", "recv a packet without valid key, resend later.");
                            sessionPack.mCacheBodyBuffer = null;
                            sessionPack.mSendStatus = 0;
                        }
                        else
                        {
                            byte[] serverId = null;
                            byte[] decryptedData = null;
                            int ret = 0;
                            if (!MMPack.DecodePack(ref decryptedData, body, key, ref serverId, ref ret))
                            {
                                Log.e("Network", string.Concat(new object[] { "unpack error! cmd=", (CmdConst)cmd, " seq=", seq }));
                                Connector.close();
                                if (ret == -13)
                                {
                                    Log.e("Network", "session timeout");
                                    SessionPackMgr.setSessionKey(null);
                                }
                                if (sessionPack.mRetryLeft > 0)
                                {
                                    sessionPack.mCacheBodyBuffer = null;
                                    sessionPack.cancelTimeoutPoint();
                                    sessionPack.mSendStatus = 0;
                                }
                                else
                                {
                                    sessionPack.mSendStatus = 6;
                                    OnCallback.onError(sessionPack, PackResult.UNPACK_ERROR);
                                }
                            }
                            else
                            {
                                sessionPack.mSendStatus = 5;
                                sessionPack.mResponseBuffer = decryptedData;
                               // string a = Util.byteToHexStr(decryptedData);
                                object responseObject = null;
                                if (!AuthPack.preProcessAuthPack(sessionPack, ref responseObject))
                                {
                                    sessionPack.mSendStatus = 6;
                                    OnCallback.onAllError(PackResult.AUTH_ERROR);
                                    Connector.onSendFailed(false, 3);
                                }
                                else
                                {
                                    this.updateCookie(sessionPack.mCmdID, ret, serverId);
                                    if (responseObject != null)
                                    {
                                        sessionPack.mResponseObject = responseObject;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            sessionPack.mResponseObject = sessionPack.onResponseParser();
                                        }
                                        catch (Exception exception)
                                        {
                                            Log.e("Network", "onResponseParser failed," + exception.ToString());
                                            OnCallback.onError(sessionPack, PackResult.PARSER_ERROR);
                                            return;
                                        }
                                        if (sessionPack.mResponseObject == null)
                                        {
                                            Log.e("Network", "onResponseParser failed");
                                            OnCallback.onError(sessionPack, PackResult.PARSER_ERROR);
                                            return;
                                        }
                                    }
                                    OnCallback.onSuccess(sessionPack);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void updateCookie(int cmdID, int ret, byte[] cookie)
        {
            if (cmdID != 8)
            {
                bool flag = false;
                if (ret == 0)
                {
                    flag = true;
                }
                if ((cmdID == 1) && ((ret == -16) || (ret == -17)))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (cookie != null)
                    {
                        SessionPackMgr.setSeverID(cookie);
                    }
                    else
                    {
                        Log.e("Network", "updateCookie: invalid cookie");
                    }
                }
            }
        }
    }
}

