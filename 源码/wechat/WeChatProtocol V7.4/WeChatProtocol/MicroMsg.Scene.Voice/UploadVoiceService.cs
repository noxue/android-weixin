namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Scene;
    using System;
    using System.Runtime.InteropServices;

    public class UploadVoiceService
    {
        private const int MAX_COUNT_LOAD = 2;
        private const int MAX_RUNNING = 1000;
       private static TimerObject mTimerObject;
        //private static System.Timers.Timer mTimerObject;// = new System.Timers.Timer();
        //private EventWatcher mWatcherToCleanStatus;
        public const int STATUS_SUCCESS = 0;
        public const int STATUS_VOICE_RECORD_ERROR = 2;
        public const int STATUS_VOICE_TOO_SHORT = 1;
        private const string TAG = "UploadVoiceService";

        public UploadVoiceService()
        {
            //if (this.mWatcherToCleanStatus == null)
            //{
            //    this.mWatcherToCleanStatus = new EventWatcher(this, null, new EventHandlerDelegate(UploadVoiceService.onHandlerToCleanStatus));
            //    EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, this.mWatcherToCleanStatus);
            //}
        }

        private void checkReadyContextDispatcher()
        {
            if (mTimerObject == null)
            {
                Log.i("UploadVoiceService", "upload voice service dispatcher startup. ");
                mTimerObject = new TimerObject();
                mTimerObject = TimerService.addTimer(3, new EventHandler(UploadVoiceService.onVoiceContextDispatcher), 0, -1);
                mTimerObject.start();
                //mTimerObject = new System.Timers.Timer();
                //mTimerObject.Interval = 1000;
                //mTimerObject.Elapsed += UploadVoiceService.onVoiceContextDispatcher;
                //mTimerObject.Start();
            }
        }

        private static void closeDispatcherTimer()
        {
            Log.i("UploadVoiceService", "all ready, close dispatcher timer. ");
            mTimerObject.stop();
            //mTimerObject.Stop();
            //mTimerObject.Close();
            //mTimerObject.Dispose();
            mTimerObject = null;
        }

//        public bool doSceneBegin(string toUserName)
//        {
//            if (UploadVoiceRecorder.isRunning())
//            {
//                Log.e("UploadVoiceService", "scene cannot begin in recording. ");
//                return false;
//            }
//            string hashString = MD5Core.GetHashString(toUserName + Util.getNowMilliseconds());
//            UploadVoiceContext context = UploadVoiceContext.createByClientMsgID(hashString);
//            if (context == null)
//            {
//                Log.e("UploadVoiceService", "create upload voice context failed. ");
//                return false;
//            }
//            Log.i("UploadVoiceService", "cmd to scene begin, toUserName = " + toUserName + " , clientMsgId = " + hashString);
//            context.mUserName = toUserName;
//            context.mStatus = 0;
//            UploadVoiceContextMgr.getInstance().putToHead(context);
//            this.checkReadyContextDispatcher();
//            UploadVoiceRecorder.start(context);
//            return true;
//        }

        public  bool doSceneDirectWithoutRecord(string toUserName,int voiceTimeLength, byte[] buffer ,int EncodeType)
        {
            if (voiceTimeLength > 60)
            {
                voiceTimeLength = 60;
            }
            if (voiceTimeLength < 2)
            {
                voiceTimeLength = 2;
            }
             //voiceTimeLength = buffer.Length;
            voiceTimeLength *= 0x3e8;
            string hashString = MD5Core.GetHashString(toUserName + Util.getNowMilliseconds());
            UploadVoiceContext context = UploadVoiceContext.createByClientMsgID(hashString);
            if (context == null)
            {
                Log.e("UploadVoiceService", "create upload voice context failed. ");
                return false;
            }
            Log.i("UploadVoiceService", "cmd to scene begin, toUserName = " + toUserName + " , clientMsgId = " + hashString);
            //int count = (voiceTimeLength * 7) / 10;
            //byte[] buffer = new byte[count];
            //for (int i = 0; i < count; i++)
            //{
            //    buffer[i] = (byte) (i | 1);
            //}
            context.mCreateTime = (int) (Util.getNowMilliseconds() / 1000.0);
            context.mUserName = toUserName;
            context.EncodeType = EncodeType;//sikl
            context.appendOutputData(buffer, 0, buffer.Length);
            context.mVoiceTimeLength = voiceTimeLength;
            //context.
            context.mEndFlag = 1;
            context.mStatus = 0;
            UploadVoiceContextMgr.getInstance().putToTail(context);
            this.checkReadyContextDispatcher();
            return true;
        }

//        public int doSceneEnd(bool isCancelled = false)
//        {
//            if (!UploadVoiceRecorder.isRunning())
//            {
//                Log.e("UploadVoiceService", "scene cannot end without begin, ignored. ");
//                return 0;
//            }
//            UploadVoiceContext context = UploadVoiceRecorder.getCurrentContext();
//            if (context == null)
//            {
//                Log.e("UploadVoiceService", "scene cannot end with null context, ignored. ");
//                return 0;
//            }
//            context.mIsCancelled = isCancelled;
//            Log.i("UploadVoiceService", "cmd to scene end,  clientMsgId = " + context.mClientMsgId);
//            if (UploadVoiceRecorder.stop() && !context.isInvalidShortVoice())
//            {
//                return 0;
//            }
//            context.mStatus = 4;
//            context.onFinished(3);
//            if (context.isInvalidShortVoice())
//            {
//                Log.e("UploadVoiceService", "scene end with invalid short voice. ");
//                return 1;
//            }
//            Log.e("UploadVoiceService", "scene end with voice record error. ");
//            return 2;
//        }

//        public bool doSceneResume(string clientMsgId)
//        {
//            Log.i("UploadVoiceService", "cmd to scene resume,  clientMsgId = " + clientMsgId);
//            if (UploadVoiceRecorder.isRunning())
//            {
//                Log.e("UploadVoiceService", "scene cannot resume in recording. ");
//                return false;
//            }
//            if (UploadVoiceContextMgr.getInstance().findByClientMsgID(clientMsgId) != null)
//            {
//                Log.e("UploadVoiceService", "already running... " + clientMsgId);
//                return false;
//            }
//            if (!UploadVoiceContextLoader.resumeByClientMsgID(clientMsgId))
//            {
//                return false;
//            }
//            this.checkReadyContextDispatcher();
//            return true;
//        }

        private static void onHandlerToCleanStatus(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
            if (((mObject != null) && (mObject.syncStatus == SyncStatus.syncEnd)) && (mObject.syncCount == 0))
            {
                //UploadVoiceContextLoader.cleanSendingStatus();
                Log.e("UploadVoiceService", "on error, UploadVoiceContextLoader.cleanSendingStatus()");
            }
        }

        private static void onSceneFinished(UploadVoiceContext context)
        {
            if (context.mStatus == 5)
            {
                //UploadVoiceContextLoader.resetIntervalLimit();
                Log.i("UploadVoiceService", "ALL completed, clientmsgid = " + context.mClientMsgId);
            }
            else if (context.mStatus == 4)
            {
                Log.e("UploadVoiceService", "on error, clientmsgid = " + context.mClientMsgId);
            }
            else
            {
                Log.e("UploadVoiceService", "status error, clientmsgid = " + context.mClientMsgId);
            }
        }

        public static void onVoiceContextDispatcher(object sender, EventArgs e)
        {
            UploadVoiceContextMgr.getInstance().clearnFinishedContext();
            if (UploadVoiceContextMgr.getInstance().getCount() <= 0)
            {
                //UploadVoiceContextLoader.checkCleanMsgTrans();
                if (UploadVoiceContextMgr.getInstance().getCount() <= 0)
                {
                    closeDispatcherTimer();
                    return;
                }
            }
            if (UploadVoiceContextMgr.getInstance().countRunningContext() < MAX_RUNNING)
            {
                UploadVoiceContext voiceContext = UploadVoiceContextMgr.getInstance().getFirstContextNeedHandle();
                if (voiceContext != null)
                {
                    Log.i("UploadVoiceService", "new task startup, clientmsgid = " + voiceContext.mClientMsgId);
                    NetSceneUploadVoice voice = new NetSceneUploadVoice();
                    voice.mOnSceneFinished += new onSceneFinishedDelegate(UploadVoiceService.onSceneFinished);
                    voice.doScene(voiceContext);
                }
            }
        }

//        public static void testUploadVoice()
//        {
//            ServiceCenter.sceneUploadVoice.doSceneBegin("halenwp71");
//        }

//        public static void testUploadVoiceStop()
//        {
//            ServiceCenter.sceneUploadVoice.doSceneEnd(false);
//        }
    }
}

