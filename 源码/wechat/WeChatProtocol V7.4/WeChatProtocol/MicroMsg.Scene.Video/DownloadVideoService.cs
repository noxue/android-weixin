namespace MicroMsg.Scene.Video
{
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
    using System.IO;

    public class DownloadVideoService
    {
        private const int MAX_RUNNING = 5;
        private static TimerObject mTimerObject;
        //private EventWatcher mWatcherToCleanStatus;
        private const string TAG = "DownloadVideoService";

        public DownloadVideoService()
        {
            //this.mWatcherToCleanStatus = new EventWatcher(this, null, new EventHandlerDelegate(DownloadVideoService.onHandlerToCleanStatus));
            //EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, this.mWatcherToCleanStatus);
        }

        private void checkReadyContextDispatcher()
        {
            if (mTimerObject == null)
            {
                Log.d("DownloadVideoService", "Download video service dispatcher startup. ");
                mTimerObject = TimerService.addTimer(2, new EventHandler(this.onVideoDownContextDispatcher), 0, -1);
                mTimerObject.start();
                this.onVideoDownContextDispatcher(null, null);
            }
        }

        //public static void cleanSendingStatus()
        //{
        //    List<ChatMsg> collection = StorageMgr.chatMsg.searhCacheMsg(0x2b, 0, 0);
        //    List<ChatMsg> list2 = StorageMgr.chatMsg.searhCacheMsg(0x3e, 0, 0);
        //    List<ChatMsg> list3 = new List<ChatMsg>();
        //    if ((collection != null) && (collection.Count > 0))
        //    {
        //        list3.AddRange(collection);
        //    }
        //    if ((list2 != null) && (list2.Count > 0))
        //    {
        //        list3.AddRange(list2);
        //    }
        //    if (list3.Count != 0)
        //    {
        //        foreach (ChatMsg msg in list3)
        //        {
        //            Log.i("DownloadVideoService", string.Concat(new object[] { "Failed to clean status-downing , ", msg.strClientMsgId, " nMsgType = ", msg.nMsgType }));
        //            msg.nStatus = 1;
        //        }
        //        StorageMgr.chatMsg.updateMsgList(collection);
        //    }
        //}

        public bool doCancelScene(int msgSvrId, bool isThumb = false)
        {
            DownloadVideoContext context = DownloadVideoContextMgr.getInstance().findBySrvMsgID(msgSvrId, isThumb);
            if (context == null)
            {
                return false;
            }
            if ((context.mStatus == 5) || (context.mStatus == 4))
            {
                return false;
            }
            context.mStatus = 4;
            context.doCancelAll();
            return true;
        }

        private bool doSceneEx(int msgSvrId, string talker, bool isThumb,ChatMsg msg)
        {
            MsgTrans trans;
            if (msgSvrId == 0)
            {
                Log.e("DownloadVideoService", "Not found the chatmsg , invalid msgid =  " + msgSvrId);
                return false;
            }
            DownloadVideoContext context = DownloadVideoContextMgr.getInstance().findBySrvMsgID(msgSvrId, isThumb);
            if ((context != null) && !context.needToClean())
            {
                Log.e("DownloadVideoService", "already downloading video  by msgid =  " + msgSvrId);
                return false;
            }
           // ChatMsg msg = StorageMgr.chatMsg.getBySvrID(talker, msgSvrId);
            if (msg == null)
            {
                Log.e("DownloadVideoService", "Not found the chatmsg by msgid =  " + msgSvrId);
                return false;
            }

            Log.d("DownloadVideoService", "begin downLoad video thumb...msgid = " + msgSvrId);
            trans = new MsgTrans();
            trans.nTransType = 6;
            trans.nMsgSvrID = msgSvrId;
            trans.nMsgLocalID = msg.nMsgLocalID;
            trans.strThumbnail = msg.strThumbnail;
            trans.strFromUserName = talker;
            trans.strToUserName = AccountMgr.getCurAccount().strUsrName;
            DownloadVideoContext contextInfo = new DownloadVideoContext {
                mVideoInfo = trans,
                mChatMsg = msg,
                mIsThumbMode = isThumb
            };
            if (!parseVideoMsgXML(msg.strMsg, trans, contextInfo))
            {
                Log.e("DownloadVideoService", "parseVideoMsgXML failed!  ");
                return false;
            }

            if (Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\ReplyRes\\Video\\") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\ReplyRes\\Video");

            }
            //using (FileStream sw = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\ReplyRes\\Video\\" + msgSvrId.ToString() + ".xml", FileMode.CreateNew))
            //{
            //    byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg.strMsg);
            //    sw.Write(myByte, 0, myByte.Length);
            //    sw.Close();
            //}

            new NetSceneUploadCdnVideo().doSceneToCGI(talker, trans, contextInfo, msg.nMsgType);
            return true;

            contextInfo.updateProgressInfo(0);
            contextInfo.updateChatMsg();
            DownloadVideoContextMgr.getInstance().putToHead(contextInfo);
            this.checkReadyContextDispatcher();
            return true;
        }

        public bool doSceneForThumb(int msgSvrId, string talker,ChatMsg msg)
        {
            return this.doSceneEx(msgSvrId, talker, true, msg);
        }

        public bool doSence(int msgSvrId, string talker, ChatMsg msg)
        {
            return this.doSceneEx(msgSvrId, talker, false, msg);
        }

        private static void onHandlerToCleanStatus(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
            if (((mObject != null) && (mObject.syncStatus == SyncStatus.syncEnd)) && (mObject.syncCount == 0))
            {
                //cleanSendingStatus();
            }
        }

        public void onVideoDownContextDispatcher(object sender, EventArgs e)
        {
            DownloadVideoContextMgr.getInstance().clearnFinishedContext();
            if (DownloadVideoContextMgr.getInstance().getCount() <= 0)
            {
                Log.d("DownloadVideoService", "all ready, close dispatcher timer. ");
                mTimerObject.stop();
                mTimerObject = null;
            }
            else if (DownloadVideoContextMgr.getInstance().countRunningContext() < MAX_RUNNING)
            {
                DownloadVideoContext context = DownloadVideoContextMgr.getInstance().getFirstContextNeedHandle();
                if (context != null)
                {
                    Log.i("DownloadVideoService", "new task startup, msgid = " + context.mVideoInfo.nMsgSvrID);
                    context.startScene();
                }
            }
        }

        public static bool parseVideoMsgXML(string xmlStr, MsgTrans videoinfo, DownloadVideoContext contextInfo = null)
        {
            if (string.IsNullOrEmpty(xmlStr) || (videoinfo == null))
            {
                Log.d("DownloadVideoService", "failed to parse msg xml, input invalid para");
                return false;
            }
            xmlStr = Util.preParaXml(xmlStr);
            XElement element = null;
            try
            {
                element = XDocument.Parse(xmlStr).Element("msg").Element("videomsg");
                XAttribute attribute = element.Attribute("clientmsgid");
                if (attribute != null)
                {
                    videoinfo.strClientMsgId = attribute.Value;
                }
                attribute = element.Attribute("length");
                if (attribute != null)
                {
                    videoinfo.nTotalDataLen = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("playlength");
                if (attribute != null)
                {
                    videoinfo.nDuration = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("fromusername");
                if (attribute != null)
                {
                    videoinfo.strFromUserName = attribute.Value;
                }
                if (contextInfo != null)
                {
                    attribute = element.Attribute("aeskey");
                    if (attribute != null)
                    {
                        contextInfo.mCdnAesKey = attribute.Value;
                        contextInfo.mCdnThumbAesKey = attribute.Value;
                    }
                    attribute = element.Attribute("cdnthumbaeskey");
                    if (attribute != null)
                    {
                        contextInfo.mCdnThumbAesKey = attribute.Value;
                    }
                    attribute = element.Attribute("cdnvideourl");
                    if (attribute != null)
                    {
                        contextInfo.mCdnVideoUrl = attribute.Value;
                    }
                    attribute = element.Attribute("cdnthumburl");
                    if (attribute != null)
                    {
                        contextInfo.mCdnThumbUrl = attribute.Value;
                    }
                    attribute = element.Attribute("cdnthumblength");
                    if (attribute != null)
                    {
                        contextInfo.mCdnThumbLength = int.Parse(attribute.Value);
                    }
                    attribute = element.Attribute("cdnthumbheight");
                    if (attribute != null)
                    {
                        contextInfo.mCdnThumbHeight = int.Parse(attribute.Value);
                    }
                    attribute = element.Attribute("cdnthumbwidth");
                    if (attribute != null)
                    {
                        contextInfo.mCdnThumbWidth = int.Parse(attribute.Value);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.d("DownloadVideoService", exception.Message);
                return false;
            }




            return true;
        }
    }
}

