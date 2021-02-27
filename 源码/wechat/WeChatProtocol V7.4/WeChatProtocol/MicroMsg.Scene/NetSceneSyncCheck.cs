namespace MicroMsg.Scene
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Network;


    public class NetSceneSyncCheck
    {
        private static EventWatcher mEventWatcher = null;
        private const string TAG = "NetSceneSyncCheck";

        private NetSceneSyncCheck()
        {
        }

        private void doScene()
        {
            SessionPack sessionPack = new SessionPack
            {
                mCmdID = 0x19,
                mRequestObject = new object()
            };
            sessionPack.mProcRequestToByteArray += new RequestToByteArrayDelegate(this.requestToByteArray);
            sessionPack.mCompleted += new SessionPackCompletedDelegate(this.onCompleted);
            Sender.getInstance().sendPack(sessionPack);
        }

        public static void initSyncCheck()
        {
            if (mEventWatcher == null)
            {
                EventWatcher eventWatcher = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneSyncCheck.onEventHandler));
                EventCenter.registerEventWatcher(EventConst.ON_NET_MM_NET_NOOP, eventWatcher);
            }
        }

        private void onCompleted(object sender, PackEventArgs e)
        {
            SessionPack pack = sender as SessionPack;
            byte[] mResponseBuffer = pack.mResponseBuffer;
            if (e.isSuccess() && (mResponseBuffer != null))
            {
                int offset = 0;
                int num2 = Util.readInt(mResponseBuffer, ref offset);
                Log.d("NetSceneSyncCheck", "new sync check success, continueFlag =  " + num2);
                if ((num2 & 7) != 0)
                {
                    //
                    EventCenter.postEvent(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, num2 & 7, null);
                }
                else
                {
                    ServiceCenter.sceneNewSync.doScene(num2 & 7, syncScene.MM_NEWSYNC_SCENE_TIMER);
                }
                //EventCenter.postEvent(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, null, null);

            }
            else
            {
                Log.e("NetSceneSyncCheck", "new sync check failed. ");
            }
        }

        private static void onEventHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs.mEventID == EventConst.ON_NET_MM_NET_NOOP)
            {
                new NetSceneSyncCheck().doScene();
            }
        }

        private byte[] requestToByteArray(object obj)
        {
            int nUin = (int)AccountMgr.getCurAccount().nUin;
            byte[] mBytesSyncKeyBuf = NetSceneNewSync.mBytesSyncKeyBuf;
            if ((nUin == 0) || (mBytesSyncKeyBuf == null))
            {
                Log.e("NetSceneSyncCheck", "new sync make pack body failed. ");
                return null;
            }
            int length = mBytesSyncKeyBuf.Length;
            int num3 = (((nUin >> 13) & 0x7ffff) | (length << 0x13)) ^ 0x5601f281;
            int num4 = (((length >> 13) & 0x7ffff) | (nUin << 0x13)) ^ 0x5601f281;
            int offset = 0;
            byte[] buf = new byte[8 + length];
            Util.writeInt(num3, ref buf, ref offset);
            Util.writeInt(num4, ref buf, ref offset);
            Util.writeBuffer(mBytesSyncKeyBuf, ref buf, ref offset);
            return buf;
        }
    }
}

