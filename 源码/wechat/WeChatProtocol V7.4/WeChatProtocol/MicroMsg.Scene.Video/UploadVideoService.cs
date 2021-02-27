namespace MicroMsg.Scene.Video
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

    public class UploadVideoService
    {
        private const int MAX_RUNNING = 5;
        private static TimerObject mTimerObject;
        //private EventWatcher mWatcherToCleanStatus;
        private const string TAG = "UploadVideoService";

        public UploadVideoService()
        {
            //this.mWatcherToCleanStatus = new EventWatcher(this, null, new EventHandlerDelegate(UploadVideoService.onHandlerToCleanStatus));
            //EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, this.mWatcherToCleanStatus);
        }

        private static void checkReadyContextDispatcher()
        {
            if (mTimerObject == null)
            {
                Log.d("UploadVideoService", "upload video service dispatcher startup. ");
                mTimerObject = TimerService.addTimer(2, new EventHandler(UploadVideoService.onVideoContextDispatcher), 0, -1);
                mTimerObject.start();
                onVideoContextDispatcher(null, null);
            }
        }

        public static void cleanSendingStatus()
        {
            //List<ChatMsg> collection = StorageMgr.chatMsg.searhCacheMsg(0x2b, 0, 1);
            //List<ChatMsg> list2 = StorageMgr.chatMsg.searhCacheMsg(0x3e, 0, 1);
            //List<ChatMsg> list3 = new List<ChatMsg>();
            //if ((collection != null) && (collection.Count > 0))
            //{
            //    list3.AddRange(collection);
            //}
            //if ((list2 != null) && (list2.Count > 0))
            //{
            //    list3.AddRange(list2);
            //}
            //if (list3.Count != 0)
            //{
            //    foreach (ChatMsg msg in list3)
            //    {
            //        Log.i("UploadVideoService", string.Concat(new object[] { "Failed to clean status-sending , ", msg.strClientMsgId, " nMsgType = ", msg.nMsgType }));
            //        msg.nStatus = 1;
            //    }
            //    StorageMgr.chatMsg.updateMsgList(collection);
            //}
        }

        private static void closeDispatcherTimer()
        {
            if (mTimerObject != null)
            {
                mTimerObject.stop();
                mTimerObject = null;
                Log.d("UploadVideoService", "all ready, close dispatcher timer. null ");

            }
            Log.d("UploadVideoService", "all ready, close dispatcher timer. count " + UploadVideoContextMgr.getInstance().getCount());

        }

        public bool doCancelScene(string clientMsgId)
        {
            UploadVideoContext context = UploadVideoContextMgr.getInstance().findByClientMsgID(clientMsgId);
            if (context == null)
            {
                Log.e("UploadVideoService", "not found clientMsgId= " + clientMsgId);
                return false;
            }
            if ((context.mStatus == 5) || (context.mStatus == 4))
            {
                Log.e("UploadVideoService", "wait to clean , clientMsgId= " + clientMsgId);
                return false;
            }
            //if (context.mSceneHandle == null)
            //{
            //    Log.e("UploadVideoService", "task not runing, clientMsgId= " + clientMsgId);
            //    context.mStatus = 4;
            //    context.updateContext();
            //    context.onFinished();
            //    return false;
            //}
            //context.mSceneHandle.doCancel();
            return true;
        }

        public bool doSceneResume(ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            string strClientMsgId = msg.strClientMsgId;
            Log.i("UploadVideoService", "cmd to scene resume,  clientMsgId = " + strClientMsgId);
            if (UploadVideoContextMgr.getInstance().findByClientMsgID(strClientMsgId) != null)
            {
                Log.e("UploadVideoService", "already running,clean first. " + strClientMsgId);
                UploadVideoContextMgr.getInstance().clearnFinishedContext();
                if (UploadVideoContextMgr.getInstance().findByClientMsgID(strClientMsgId) != null)
                {
                    Log.e("UploadVideoService", "already running, ignored! clientMsgId = " + strClientMsgId);
                    return false;
                }
            }
            //if (!resumeByClientMsgID(msg))
            //{
            //    return false;
            //}
            checkReadyContextDispatcher();
            return true;
        }

        public bool doSence(string toUserName, string videoFile, string thumbFile, int playLength, bool isForward = false, ChatMsg origChatMsg = null)
        {
            string hashString = MD5Core.GetHashString(toUserName + Util.getNowMilliseconds());
            UploadVideoContext context = UploadVideoContext.createByClientMsgID(hashString);
            if (context == null)
            {
                Log.e("UploadVideoService", "create upload video context failed. ");
                return false;
            }
            Log.i("UploadVideoService", "cmd to scene begin, toUserName = " + toUserName + " , clientMsgId = " + hashString);
            context.mVideoTrans.strToUserName = toUserName;
            context.mVideoTrans.strFromUserName = AccountMgr.getCurAccount().strUsrName;
            context.mVideoTrans.strThumbnail = thumbFile;
            context.mVideoTrans.strImagePath = videoFile;
            context.mVideoTrans.nDuration = playLength;
            context.mStatus = 0;
            if (isForward)
            {
                //context.preProcessVideoFile(thumbFile, videoFile);
                context.mOrigChatMsg = origChatMsg;
            }
            if (!context.initThumbMemStream())
            {
                Log.e("UploadVideoService", "failed to load thumb file. ");
                return false;
            }
            if (!context.initVideoFileStream())
            {
                Log.e("UploadVideoService", "failed to load video file. ");
                return false;
            }
            context.addVoiceChatMsg(true);
            UploadVideoContextMgr.getInstance().putToHead(context);
            checkReadyContextDispatcher();
            return true;
        }

        public bool doSenceShortVideo(string toUserName, string videoFile, string thumbFile, int playLength, bool isForward = false, ChatMsg origChatMsg = null)
        {
            Log.i("UploadVideoService", "begin up short video, videoFile = " + videoFile);
            string hashString = MD5Core.GetHashString(toUserName + Util.getNowMilliseconds());
            UploadVideoContext context = UploadVideoContext.createByClientMsgID(hashString);
            if (context == null)
            {
                Log.e("UploadVideoService", "create upload short video context failed. ");
                return false;
            }
            Log.i("UploadVideoService", "cmd to scene begin, toUserName = " + toUserName + " , clientMsgId = " + hashString);
            context.mVideoTrans.strToUserName = toUserName;
            context.mVideoTrans.strFromUserName = AccountMgr.getCurAccount().strUsrName;
            context.mVideoTrans.strThumbnail = thumbFile;
            context.mVideoTrans.strImagePath = videoFile;
            context.mVideoTrans.nDuration = playLength;
            context.mStatus = 0;
            if (isForward)
            {
                //context.preProcessSightFile(thumbFile, videoFile);
                context.mOrigChatMsg = origChatMsg;
            }
            if (!context.initThumbMemStream())
            {
                Log.e("UploadVideoService", "failed to load thumb file. ");
                return false;
            }
            if (!context.initVideoFileStream())
            {
                Log.e("UploadVideoService", "failed to load video file. ");
                return false;
            }
            context.addVoiceChatMsg(true);
            UploadVideoContextMgr.getInstance().putToHead(context);
            checkReadyContextDispatcher();
            return true;
        }

        public UploadVideoContext.ProgressInfo getProgress(string clientMsgId)
        {
            //UploadVideoContext context = UploadVideoContextMgr.getInstance().findByClientMsgID(clientMsgId);
            //if (context != null)
            //{
            //    return context.mProgressInfo;
            //}
            //MsgTrans trans = StorageMgr.msgVideo.getByClientMsgID(clientMsgId);
            //if (trans != null)
            //{
            //    UploadVideoContext.ProgressInfo info = new UploadVideoContext.ProgressInfo {
            //        totalLength = trans.nTotalDataLen + trans.nRecordLength,
            //        sentLength = trans.nTransDataLen + trans.nEndFlag,
            //        clientMsgId = trans.strClientMsgId,
            //        status = UploadVideoContext.convertStatus(trans.nStatus)
            //    };
            //    info.printInfo();
            //    return info;
            //}
            return null;
        }

        private static void onHandlerToCleanStatus(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
            if (((mObject != null) && (mObject.syncStatus == SyncStatus.syncEnd)) && (mObject.syncCount == 0))
            {
                cleanSendingStatus();
            }
        }

        public static void onVideoContextDispatcher(object sender, EventArgs e)
        {
            UploadVideoContextMgr.getInstance().clearnFinishedContext();
            if (UploadVideoContextMgr.getInstance().getCount() <= 0)
            {
                closeDispatcherTimer();
            }
            else if (UploadVideoContextMgr.getInstance().countRunningContext() < MAX_RUNNING)
            {
                UploadVideoContext videoContext = UploadVideoContextMgr.getInstance().getFirstContextNeedHandle();
                if (videoContext != null)
                {
                    //Log.i("UploadVideoService", "new task startup, clientmsgid = " + videoContext.mVideoTrans.strClientMsgId);
                    //if (videoContext.mChatMsg.nMsgType == 0x3e)
                    //{
                    //    //new NetSceneUploadCdnVideo().doScene(videoContext);
                    //}
                    //else if (CDNComService.Instance.isUseCdnComSendVideo && CDNComService.Instance.isCdnSupportToUser(videoContext.mVideoTrans.strToUserName))
                    //{
                    //   // new NetSceneUploadCdnVideo().doScene(videoContext);
                    //}
                    //else
                    //{
                    new NetSceneUploadVideo().doScene(videoContext);
                    // }
                }
            }
        }

        public static VideoDetailInfo parseVideoMsgXML(string xmlStr)
        {
            if (string.IsNullOrEmpty(xmlStr))
            {
                Log.d("UploadVideoService", "failed to parse msg xml, input invalid para");
                return null;
            }
            xmlStr = Util.preParaXml(xmlStr);
            VideoDetailInfo info = new VideoDetailInfo();
            XElement element = null;
            try
            {
                element = XDocument.Parse(xmlStr).Element("msg").Element("videomsg");
                XAttribute attribute = element.Attribute("clientmsgid");
                if (attribute != null)
                {
                    info.strClientMsgId = attribute.Value;
                }
                attribute = element.Attribute("length");
                if (attribute != null)
                {
                    info.nTotalDataLen = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("playlength");
                if (attribute != null)
                {
                    info.nDuration = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("fromusername");
                if (attribute != null)
                {
                    info.strFromUserName = attribute.Value;
                }
                attribute = element.Attribute("aeskey");
                if (attribute != null)
                {
                    info.mCdnAesKey = attribute.Value;
                    info.mCdnThumbAesKey = attribute.Value;
                }
                attribute = element.Attribute("cdnthumbaeskey");
                if (attribute != null)
                {
                    info.mCdnThumbAesKey = attribute.Value;
                }
                attribute = element.Attribute("cdnvideourl");
                if (attribute != null)
                {
                    info.mCdnVideoUrl = attribute.Value;
                }
                attribute = element.Attribute("cdnthumburl");
                if (attribute != null)
                {
                    info.mCdnThumbUrl = attribute.Value;
                }
                attribute = element.Attribute("cdnthumblength");
                if (attribute != null)
                {
                    info.mCdnThumbLength = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("cdnthumbheight");
                if (attribute != null)
                {
                    info.mCdnThumbHeight = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("cdnthumbwidth");
                if (attribute != null)
                {
                    info.mCdnThumbWidth = int.Parse(attribute.Value);
                }
            }
            catch (Exception exception)
            {
                Log.d("UploadVideoService", exception.Message);
                return null;
            }
            return info;
        }

        //public static bool resumeByClientMsgID(ChatMsg msg)
        //{
        //    string strClientMsgId = msg.strClientMsgId;
        //    MsgTrans videoinfo = StorageMgr.msgVideo.getByClientMsgID(strClientMsgId);
        //    if (videoinfo == null)
        //    {
        //        Log.d("UploadVideoService", "not found task ,clientmsgid =  " + strClientMsgId);
        //        videoinfo = new MsgTrans {
        //            nTransType = 5,
        //            nMsgLocalID = msg.nMsgLocalID,
        //            strClientMsgId = strClientMsgId,
        //            strToUserName = msg.strTalker,
        //            strFromUserName = AccountMgr.strUsrName,
        //            strThumbnail = msg.strThumbnail,
        //            strImagePath = msg.strPath
        //        };
        //        DownloadVideoService.parseVideoMsgXML(msg.strMsg, videoinfo, null);
        //        videoinfo.nRecordLength = (int) StorageIO.fileLength(videoinfo.strThumbnail);
        //    }
        //    if (videoinfo.nStatus == 5)
        //    {
        //        return false;
        //    }
        //    Log.i("UploadVideoService", "resuming.... " + strClientMsgId);
        //    UploadVideoContext context = UploadVideoContext.createByMsgTrans(videoinfo);
        //    if (!context.loadFromMsgTrans())
        //    {
        //        Log.e("UploadVideoService", "Failed to load video data,  " + videoinfo.strClientMsgId);
        //        return false;
        //    }
        //    Log.d("UploadVideoService", "load video file success, put task to head. ");
        //    UploadVideoContextMgr.getInstance().putToHead(context);
        //    return true;
        //}

        public void test()
        {
            byte[] buffer = new byte[0x100000];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(i + 1);
            }
            //StorageIO.writeToFile("testVideo.mp4", 0, new byte[][] { buffer });
            byte[] buffer2 = new byte[0x6000];
            //StorageIO.writeToFile("testThumb.jpg", 0, new byte[][] { buffer2 });
            ServiceCenter.sceneUploadVideo.doSence("ntsafe-hkk", "3.mp4", "1.jpg", 3, false, null);
        }

        public void testSendShortVideo()
        {
            string videoFile = "storage/mmteam943346/video/cbfda3a7-ab92-43f4-bb62-d9611efc489e.mp4";
            string thumbFile = "storage/mmteam943346/video/98877090-b576-4d26-9a7f-f4ec52c845bf.jpg";
            ServiceCenter.sceneUploadVideo.doSenceShortVideo("terrycjxu", videoFile, thumbFile, 3, false, null);
        }
    }
}

