namespace MicroMsg.Scene.Image
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Storage;
    using System;
    using System.Xml.Linq;
    using MicroMsg.Common.Timer;

    public class DownloadImgService
    {
        private const int MAX_RUNNING = 5;
        private static TimerObject mTimerObject;
        //private static System.Timers.Timer mTimerObject;
        private const string TAG = "DownloadImgService";

        private static void checkReadyContextDispatcher()
        {
            if (mTimerObject == null)
            {
                mTimerObject = TimerService.addTimer(1, new EventHandler(DownloadImgService.onImageDownContextDispatcher), 1, -1);
                mTimerObject.start();
                onImageDownContextDispatcher(null, null);
               // mTimerObject = new System.Timers.Timer();
                //mTimerObject.Interval = 1000;
                //mTimerObject.Elapsed += DownloadImgService.onImageDownContextDispatcher;
                //mTimerObject.Start();
                //onImageDownContextDispatcher(null, null);
            }
        }

        public static int doScene(int imgMsgSvrId, ChatMsg item, string toTalker, int compressType = 0)
        {
           // ChatMsg item = StorageMgr.chatMsg.getBySvrID(toTalker, imgMsgSvrId);
            if (item == null)
            {
                Log.e("DownloadImgService", "Not found chat msg by srvidimg " + imgMsgSvrId);
                return -1;
            }
            if (string.IsNullOrEmpty(item.strClientMsgId))
            {
                item.strClientMsgId = MD5Core.GetHashString(toTalker + imgMsgSvrId + Util.getNowMilliseconds());
                //StorageMgr.chatMsg.modifyMsg(item);
            }
            //MsgTrans trans = StorageMgr.msgImg.getByMsgSvrID(imgMsgSvrId);
            //bool flag = false;
            //if (trans == null)
            //{
            //    trans = new MsgTrans();
            //    flag = true;
            //}
            MsgTrans trans = new MsgTrans();
            trans.nMsgSvrID = imgMsgSvrId;
            trans.nMsgLocalID = item.nMsgLocalID;
            trans.strThumbnail = item.strThumbnail;
            trans.strFromUserName = toTalker;
            trans.strToUserName = AccountMgr.getCurAccount().strUsrName;
            trans.nTransType = 2;
            DownloadImgContext context = DownloadImgContextMgr.getInstance().getContextBySvrid(imgMsgSvrId);
            if (context != null)
            {
                if ((context.mCompressType != 0) || (compressType != 1))
                {
                    Log.e("DownloadImgService", "imgMsgSvrId: " + imgMsgSvrId + "has been in Queue");
                    return -1;
                }
                DownloadImgContextMgr.getInstance().remove(context);
            }
            DownloadImgContext context2 = new DownloadImgContext {
                mImgInfo = trans,
                mCompressType = compressType,
                mChatMsg = item,
                beginTime = (long) Util.getNowMilliseconds()
            };
            CImgMsgContext context3 = parseImageContent(item.strTalker,item.strMsg);
            if (context3 == null)
            {
                Log.e("DownloadImgService", "Parse image msg xml failed: " + item.strContent);
                return -1;
            }
            context2.mImgMsgContent = context3;
            if (compressType == 1)
            {
                context2.mImgInfo.nTotalDataLen = context3.hdlength;
            }
            else
            {
                context2.mImgInfo.nTotalDataLen = context3.length;
            }
            //if (!context2.intLocalDataFile(trans))
            //{
            //    Log.e("DownloadImgService", "intLocalDataFile failed! ");
            //    return -1;
            //}
            context2.mStatus = 0;
            DownloadImgContextMgr.getInstance().putToHead(context2);
            checkReadyContextDispatcher();
            context2.updateChatMsg();
          //  if (flag)
           // {
               // StorageMgr.msgTrans.add(context2.mImgInfo);
           // }
            return 1;
        }

        public static void onImageDownContextDispatcher(object sender, EventArgs e)
        {
            DownloadImgContextMgr.getInstance().clearnFinishedContext();
            if (DownloadImgContextMgr.getInstance().countRunningContext() < MAX_RUNNING)
            {
                DownloadImgContext context = DownloadImgContextMgr.getInstance().getFirstContextNeedHandle();
                if (context == null)
                {
                    if (DownloadImgContextMgr.getInstance().countRunningContext() == 0)
                    {
                        mTimerObject.stop();
                        //mTimerObject.Stop();
                        //mTimerObject.Close();
                        //mTimerObject.Dispose();
                        mTimerObject = null;
                    }
                }
                else
                {
                    Log.i("DownloadImgService", "new  imagedown task  startup, msgid = " + context.mImgInfo.nMsgSvrID);
                    context.startScene();
                }
            }
        }

        public static CImgMsgContext parseImageContent(string strTalker, string xmlContent)
        {
            if (string.IsNullOrEmpty(strTalker))
            {
                return null;
            }
            CImgMsgContext context = new CImgMsgContext();
            if (string.IsNullOrEmpty(xmlContent))
            {
                context.length = 0;
                context.hdlength = 0;
                return context;
            }
            string text = xmlContent;
            if (ContactHelper.isChatRoom(strTalker))
            {
                int index = xmlContent.IndexOf('\n');
                text = xmlContent.Substring(index + 1);
            }
            XElement element = null;
            try
            {
                element = XDocument.Parse(text).Element("msg");
                string name = "img";
                if (element.Element(name) == null)
                {
                    name = "imgmsg";
                }
                XAttribute attribute = element.Element(name).Attribute("length");
                if (attribute != null)
                {
                    context.length = int.Parse(attribute.Value);
                }
                attribute = element.Element(name).Attribute("hdlength");
                if (attribute != null)
                {
                    context.hdlength = int.Parse(attribute.Value);
                }
                attribute = element.Element(name).Attribute("cdnthumblength");
                if (attribute != null)
                {
                    context.thumblength = int.Parse(attribute.Value);
                }
                attribute = element.Element(name).Attribute("encryver");
                if (attribute != null)
                {
                    context.encryVer = int.Parse(attribute.Value);
                }
                attribute = element.Element(name).Attribute("aeskey");
                if (attribute != null)
                {
                    context.aesKey = attribute.Value;
                    context.thumbaesKey = attribute.Value;
                }
                attribute = element.Element(name).Attribute("cdnthumbaeskey");
                if (attribute != null)
                {
                    context.thumbaesKey = attribute.Value;
                }
                attribute = element.Element(name).Attribute("cdnmidimgurl");
                if (attribute != null)
                {
                    context.midUrlKey = attribute.Value;
                }
                attribute = element.Element(name).Attribute("cdnbigimgurl");
                if (attribute != null)
                {
                    context.bigUrlKey = attribute.Value;
                }
                attribute = element.Element(name).Attribute("cdnthumburl");
                if (attribute != null)
                {
                    context.thumbUrlKey = attribute.Value;
                }

                attribute = element.Element(name).Attribute("cdnthumbheight");
                if (attribute != null)
                {
                    context.CDNThumbImgHeight = int.Parse(attribute.Value);
                }

                attribute = element.Element(name).Attribute("cdnthumbwidth");
                if (attribute != null)
                {
                    context.CDNThumbImgWidth =int.Parse( attribute.Value);
                }

            }
            catch (Exception exception)
            {
                Log.e("DownloadImgService", exception.Message);
                context.length = 0;
                context.hdlength = 0;
                return context;
            }
            return context;
        }
    }
}

