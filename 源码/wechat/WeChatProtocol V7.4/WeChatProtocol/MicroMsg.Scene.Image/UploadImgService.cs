namespace MicroMsg.Scene.Image
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    public class UploadImgService
    {
        private const int MAX_RUNNING = 5;
        private TimerObject mTimerObject;
        //private  System.Timers.Timer mTimerObject;
        private static UploadImgService mUploadImgSerivce;
        //private EventWatcher mWatcher;
        private const string TAG = "UploadImgService";

        //public void checkInit()
        //{
        //    if (this.mWatcher == null)
        //    {
        //        this.mWatcher = new EventWatcher(null, null, new EventHandlerDelegate(this.onAccountLoginProc));
        //        EventCenter.registerEventWatcher(EventConst.ON_ACCOUNT_LOGIN, this.mWatcher);
        //    }
        //}

        private void checkReadyContextDispatcher()
        {
            if (this.mTimerObject == null)
            {
               this.mTimerObject = TimerService.addTimer(8, new EventHandler(this.onImageContextDispatcher), 1, -1);
               this.mTimerObject.start();
                //onImageDownContextDispatcher(null, null);
                //mTimerObject = new System.Timers.Timer();
                //mTimerObject.Interval = 1000;
                //mTimerObject.Elapsed += this.onImageContextDispatcher;
                //mTimerObject.Start();
            }
        }

        public ChatMsg doScene(string talkerName, string filename, byte[] origStream, int msgType = 3, ParamEx param = null)
        {
            if ((string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(talkerName)) || (origStream == null))
            {
                return null;
            }
            NetSceneUploadImage sceneInstance = new NetSceneUploadImage();
            if (param != null)
            {
                sceneInstance.mIsNeedScale = param.isNeedScale;
            }
            Log.i("UploadImgService", "==============startDoSence,mIsNeedScale = " + sceneInstance.mIsNeedScale);
            return this.startDoSence(talkerName, filename, origStream, sceneInstance, -1, msgType, param);
        }

        public ChatMsg doSceneByTrans(string talkerName, int nImgMsgTranId)
        {
            if ((nImgMsgTranId <= 0) || string.IsNullOrEmpty(talkerName))
            {
                return null;
            }
            NetSceneUploadImage sceneInstance = new NetSceneUploadImage();
            return this.startDoSence(talkerName, null, null, sceneInstance, nImgMsgTranId, 3, null);
        }

        public static UploadImgService getInstance()
        {
            if (mUploadImgSerivce == null)
            {
                mUploadImgSerivce = new UploadImgService();
                //mUploadImgSerivce.checkInit();
            }
            return mUploadImgSerivce;
        }

        //private void onAccountLoginProc(EventWatcher watcher, BaseEventArgs evtArgs)
        //{
        //    List<ChatMsg> msgList = StorageMgr.chatMsg.searhCacheMsg(3, 0, 1);
        //    List<ChatMsg> list2 = StorageMgr.chatMsg.searhCacheMsg(0x27, 0, 1);
        //    if ((list2 != null) && (list2.Count > 0))
        //    {
        //        if (msgList == null)
        //        {
        //            msgList = new List<ChatMsg>();
        //        }
        //        foreach (ChatMsg msg in list2)
        //        {
        //            msgList.Add(msg);
        //        }
        //    }
        //    if ((msgList != null) && (msgList.Count > 0))
        //    {
        //        foreach (ChatMsg msg2 in msgList)
        //        {
        //            msg2.nStatus = 1;
        //        }
        //        //StorageMgr.chatMsg.updateMsgList(msgList);
        //    }
        //}

        private void onImageContextDispatcher(object sender, EventArgs e)
        {
            UpLoadImgContextMgr.getInstance().clearnFinishedContext();
            if (UpLoadImgContextMgr.getInstance().countRunningContext() < MAX_RUNNING)
            {
                UpLoadImgContext imgContext = UpLoadImgContextMgr.getInstance().getFirstContextNeedHandle();
                if (imgContext == null)
                {
                    Log.i("UploadImgService", "No more image need send. ");
                    if (UpLoadImgContextMgr.getInstance().countRunningContext() == 0)
                    {
                        //List<ChatMsg> msgList = StorageMgr.chatMsg.searhCacheMsg(3, 0, 1);
                        //if ((msgList != null) && (msgList.Count > 0))
                        //{
                        //    foreach (ChatMsg msg in msgList)
                        //    {
                        //        msg.nStatus = 1;
                        //    }
                        //   // StorageMgr.chatMsg.updateMsgList(msgList);
                        //}
                        Log.i("UploadImgService", "all ready, close dispatcher timer. ");
                        //this.mTimerObject.stop();
                        mTimerObject.stop();
                        //mTimerObject.Close();
                        //mTimerObject.Dispose();
                        this.mTimerObject = null;
                    }
                }
                else if (((imgContext.imgInfo == null) && (imgContext.imgBUf != null)) && !imgContext.fillContextWithOrigStream())
                {
                    Log.e("UploadImgService", "fillContext fail!");
                    imgContext.mStatus = 5;
                }
                else
                {
                    (imgContext.senceHandle as NetSceneUploadImage).doScene(imgContext);
                }
            }
        }

        private ChatMsg startDoSence(string talkerName, string filename, byte[] origStream, object sceneInstance, int nImgMsgTranId, int msgType = 3, ParamEx param = null)
        {
            if (UpLoadImgContextMgr.getInstance().countRunningContext() >= MAX_RUNNING)
            {
                Log.e("UploadImgService", "send Image scene cannot begin ,running task too much. ");
                return null;
            }
            if ((talkerName == null) || (sceneInstance == null))
            {
                Log.e("UploadImgService", "one of the send Image scene cannot begin, param error.");
                return null;
            }
            if ((origStream != null) && (filename != null))
            {
                UpLoadImgContext context = new UpLoadImgContext {
                    talkerName = talkerName,
                    filename = filename,
                   // origStream = origStream,
                    //imgBUf=origStream,
                    senceHandle = sceneInstance,
                    mMsgType = msgType,
                    mParamEx = param,
                    beginTime = (long) Util.getNowMilliseconds()
                };
                Log.d("UploadImgService", "begin add chatmsg");
                if (context.chatMsgInfo == null)
                {
                    context.chatMsgInfo = new ChatMsg();
                }
                context.imgBUf = new byte[origStream.Length];
                Buffer.BlockCopy(origStream, 0, context.imgBUf,0,origStream.Length);
                context.chatMsgInfo.strTalker = talkerName;
                context.chatMsgInfo.nMsgType = msgType;
                context.chatMsgInfo.nStatus = 0;
                context.chatMsgInfo.nIsSender = 1;
                context.chatMsgInfo.nCreateTime = (long) (Util.getNowMilliseconds() / 1000.0);
                context.chatMsgInfo.strClientMsgId = MD5Core.GetHashString(talkerName + context.GetHashCode() + Util.getNowMilliseconds());
                //StorageMgr.chatMsg.addMsg(context.chatMsgInfo);
                UpLoadImgContextMgr.getInstance().putToHead(context);
                this.checkReadyContextDispatcher();
                return context.chatMsgInfo;
            }
            if (nImgMsgTranId > 0)
            {
                Log.i("UploadImgService", "Resend image nImgMsgTranId = " + nImgMsgTranId);
                UpLoadImgContext context2 = new UpLoadImgContext();
                //context2.loadUploadImgContext(nImgMsgTranId);
                //if ((context2.imgInfo != null) && (context2.mBigImageMemoryStream != null))
                //{
                //    context2.talkerName = talkerName;
                //    context2.senceHandle = sceneInstance;
                //    context2.beginTime = (long) Util.getNowMilliseconds();
                //    UpLoadImgContextMgr.getInstance().putToHead(context2);
                //    this.checkReadyContextDispatcher();
                //    return context2.chatMsgInfo;
                //}
                Log.e("UploadImgService", "one of the send Image scene cannot begin, loadUploadImgContext from db error.");
                return null;
            }
            Log.e("UploadImgService", "one of the send Image scene cannot begin, param error.");
            return null;
        }
    }
}

