namespace MicroMsg.Network
{
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System;

    public class Sender
    {
        public static Sender gSender = null;
        private static bool mDisableSender = false;
        private static int mRetryAuthLeft = 3;

        public void checkSender()
        {
            if (mDisableSender)
            {
                mDisableSender = false;
                this.closeSender();
            }
        }

        private bool checkSessionKey(ref SessionPack sessionPack)
        {
            if (sessionPack.mCmdID == 1 || sessionPack.mCmdID == 0xb2)
            {
                AuthPack.updateAccountInfoFromAuth(sessionPack.mRequestObject);

                return true;
            }

            if (sessionPack.mCmdID == 0x20)
            {
                AuthPack.updateAccountInfoFromNewReg(sessionPack.mRequestObject);
                return true;
            }
            if (sessionPack.mCmdID == 0x1f)
            {
                AuthPack.updateAccountInfoFromReg(sessionPack.mRequestObject);
                return true;
            }
            if (sessionPack.mCmdID == 232 || sessionPack.mCmdID == 233)
            {

                return true;
            }
            if (!this.needSessionKey(sessionPack))
            {
                return true;
            }
            if (SessionPackMgr.isValidSessionKey())
            {
                return true;
            }
            this.checkTryAutoAuth();
            return false;
        }

        public bool checkTryAutoAuth()
        {
            if (!SessionPackMgr.getAccount().isValid())
            {
                Log.e("Network", "cannot auto-auth without valid account.");
                this.closeSender();
                return false;
            }
            if (mRetryAuthLeft <= 0)
            {
                if (mRetryAuthLeft == 0)
                {
                    Log.e("Network", " autoAuth no retry left");
                    mRetryAuthLeft = -1;
                }
                return false;
            }
            SessionPackMgr.getPackEx(delegate (SessionPack pack)
            {
                if (pack.mCacheBodyBuffer != null)
                {
                    Log.i("Network", "clearn cache body before autoauth, cmdUri = " + pack.mCmdUri);
                    pack.mCacheBodyBuffer = null;
                }
                return true;
            });
            Log.i("Network", "CMD need autoauth, autoAuthLeft:" + mRetryAuthLeft);
            mRetryAuthLeft--;
            SessionPackMgr.putToHead(AuthPack.makeAutoAuthPack(2));
            return true;
        }

        public void closeSender()
        {
            Log.i("Network", "close sender , clearn all.");
            LongConnector.setEnable(false);
            Connector.close();
            SessionPackMgr.cancelAllPacket();
            SessionPackMgr.getAccount().reset();
            SessionPackMgr.setAuthStatus(0);
        }

        private bool doSendPack(SessionPack sessionPack)
        {
            sessionPack.mSendStatus = 2;
            if (sessionPack.mCmdID == 1)
            {
                Log.i("Network", "auth now...user = " + SessionPackMgr.getAccount().getUsername());
                SessionPackMgr.setAuthStatus(1);
            }

            if (sessionPack.mCmdID == 0xb2)
            {
                Log.i("Network", "auth now...user = " + SessionPackMgr.getAccount().getUsername());
                SessionPackMgr.setAuthStatus(1);
            }
            if (sessionPack.mCmdID == 8)
            {
                sessionPack.mSendStatus = 4;
            }
            else
            {
                sessionPack.setTimeoutPoint();
            }
            if (!Connector.sendPack(sessionPack))
            {
                this.onSendPackFailed(true, sessionPack);
                return false;
            }
            if ((sessionPack.mCmdID == 6) || (sessionPack.mCmdID == 8))
            {
                sessionPack.mSendStatus = 5;
            }
            else
            {
                sessionPack.mSendStatus = 3;
            }
            return true;
        }

        public static Sender getInstance()
        {
            if (gSender == null)
            {
                gSender = new Sender();
            }
            return gSender;
        }

        public bool handler()
        {
            //if (!NetworkDeviceWatcher.isNetworkAvailable())
            //{
            //    return false;
            //}
            SessionPack sessionPack = SessionPackMgr.getFirstNotSended();
            if (sessionPack == null)
            {
                return false;
            }
            if (SessionPackMgr.isAuthing())
            {
                return false;
            }
            if (!Connector.isValidforPack(sessionPack))
            {
                return false;
            }
            if (!this.checkSessionKey(ref sessionPack))
            {
                return false;
            }
            if (!this.preProcessPack(sessionPack))
            {
                Log.e("Network", "Sender: Failed to PreProcess pack.");
                return false;
            }
            if (!this.doSendPack(sessionPack))
            {
                return false;
            }
            return true;
        }

        public void logoutAccount()
        {
            Log.i("Network", "CMD to network , logout account.");
            mDisableSender = true;
        }

        private bool needSessionKey(SessionPack sessionPack)
        {
            if (sessionPack.mCmdID == 0x21)
            {
                return false;
            }
            if (sessionPack.mCmdID == 0x30)
            {
                return false;
            }
            if (sessionPack.mCmdID == 0)
            {
                if (sessionPack.mCmdUri.Equals("/cgi-bin/micromsg-bin/bindopmobileforreg"))
                {
                    return false;
                }
                if (sessionPack.mCmdUri.Equals("/cgi-bin/micromsg-bin/getresetpwdurl"))
                {
                    return false;
                }
            }
            return true;
        }

        public void onSendPackFailed(bool needReconnect, SessionPack sessionPack)
        {
            Log.e("Network", string.Concat(new object[] { "sender:onSendPackFailed, seq =", sessionPack.mSeqID, ", cmd =", sessionPack.mCmdID }));
            SessionPackMgr.cleanAllTimeoutPoint(sessionPack.mConnectMode);
            SessionPackMgr.markAllToNotSended(sessionPack.mConnectMode);
            if (SessionPackMgr.getAuthStatus() == 1)
            {
                SessionPackMgr.setAuthStatus(0);
            }
            Connector.onSendFailed(needReconnect, sessionPack.mConnectMode);
        }

        public void onSendTimeout(SessionPack sessionPack)
        {
            Log.e("Network", string.Concat(new object[] { "sender:onSendTimeout, seq =", sessionPack.mSeqID, ", cmd =", sessionPack.mCmdID }));
            this.onSendPackFailed(true, sessionPack);
        }

        private bool preProcessPack(SessionPack sessionPack)
        {
            if (sessionPack.mRetryLeft <= 0)
            {
                Log.e("Network", "sender: retryLeft =0,cmd =:" + ((CmdConst)sessionPack.mCmdID));
                sessionPack.mSendStatus = 6;
                OnCallback.onError(sessionPack, PackResult.RETRY_LIMIT);
                return false;
            }
            if (sessionPack.mCanceled)
            {
                Log.e("Network", "not send packet been cancelled.)");
                return false;
            }
            if ((sessionPack.mCmdID != 6) && (sessionPack.mRetryLeft != 3))
            {
                int newSeq = SessionPack.getSeqID();
                Log.d("Network", string.Concat(new object[] { "resend pack, change seq ", sessionPack.mSeqID, " to new seq ", newSeq }));
                SessionPackMgr.changeSessionPackSeq(sessionPack.mSeqID, newSeq);
                sessionPack.mSeqID = newSeq;
            }
            sessionPack.mRetryLeft--;
            sessionPack.mSendStatus = 1;
            if (sessionPack.mCacheBodyBuffer == null)
            {
                //   if ((sessionPack.mCmdID == 8) || (sessionPack.mCmdID == 0x19) || (sessionPack.mCmdID == 232 || (sessionPack.mCmdID == 233)))
                //  {
                //sessionPack.mCacheBodyBuffer = sessionPack.requestToByteArray();


                ////Log.e("Network", "mCacheBodyBuffer:"+);
                //if (sessionPack.mCacheBodyBuffer == null)
                //{
                //    Log.e("Network", "newsync check got bytearray failed.");
                //    sessionPack.mSendStatus = 6;
                //    OnCallback.onError(sessionPack, PackResult.PACK_ERROR);
                //    return false;
                //}

                //if (sessionPack.mCmdID == 232 || sessionPack.mCmdID == 233)
                //{
                //    if (!MMPack.EncodePackMini(sessionPack))
                //    //if (!MMPack.EncodePack(sessionPack))
                //    {
                //        Log.e("Network", "sender.encodePack failed,cmd= " + ((CmdConst)sessionPack.mCmdID));
                //        sessionPack.mCacheBodyBuffer = null;
                //        sessionPack.mSendStatus = 6;
                //        OnCallback.onError(sessionPack, PackResult.PACK_ERROR);
                //        return false;
                //    }
                //}

                //return true;
                //  }

                if (!MMPack.EncodePackMini(sessionPack))
                // if (!MMPack.EncodePack(sessionPack))
                {
                    Log.e("Network", "sender.encodePack failed,cmd= " + ((CmdConst)sessionPack.mCmdID));
                    sessionPack.mCacheBodyBuffer = null;
                    sessionPack.mSendStatus = 6;
                    OnCallback.onError(sessionPack, PackResult.PACK_ERROR);
                    return false;
                }
            }
            return true;
        }

        public bool sendAutoAuthPack()
        {
            SessionPack sessionPack = AuthPack.makeAutoAuthPack(2);
            return this.sendPack(sessionPack);
        }

        public bool sendPack(SessionPack sessionPack)
        {
            if (sessionPack == null)
            {
                Log.e("Network", "CMD to Network: null pack");
                return false;
            }
            Log.i("Network", string.Concat(new object[] { "CMD to Network: cmd=", (CmdConst)sessionPack.mCmdID, " seq=", sessionPack.mSeqID }));
            mRetryAuthLeft = 3;
            Connector.onPrepareSend(false);
            LongConnector.setEnable(true);
            sessionPack.timeInCmd = Util.getNowMilliseconds();
            if ((sessionPack.mCmdID == 1) && SessionPackMgr.isAuthing())
            {
                Log.e("Network", "isAuthing, don't send auth request");
                return false;
            }
            if (sessionPack.mCmdID == 1 || sessionPack.mCmdID == 0xb2)
            {
                SessionPackMgr.putToHead(sessionPack);
            }
            else
            {
                SessionPackMgr.putToTail(sessionPack);
            }
            NetHandler.wakeUp();
            return true;
        }
    }
}

