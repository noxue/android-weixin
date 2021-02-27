namespace MicroMsg.Scene.Voice
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Xml.Linq;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using MicroMsg.Plugin.Plugin_Reply;

    public class DownloadVoiceService
    {
        // private static bool isAppActive;
        private static bool isFirstSync;
        private static TimerObject mTimerObject;
        private static EventWatcher mWatcher;
        private const string TAG = "DownloadVoiceService";


        public DownloadVoiceService()
        {
            mWatcher = new EventWatcher(this, null, new EventHandlerDelegate(DownloadVoiceService.HandlerDoScene));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, mWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NET_DEVICE_CONNECTED, mWatcher);
            //EventCenter.registerEventWatcher(EventConst.ON_APP_ACTIVE, mWatcher);
        }

        private static void checkReadyContextDispatcher()
        {
            if (mTimerObject == null)
            {
                Log.d("DownloadVoiceService", "download voice service dispatcher startup. ");
                mTimerObject = TimerService.addTimer(1, new EventHandler(DownloadVoiceService.onVoiceContextDispatcher), 1, -1);
                mTimerObject.start();
            }
        }

        public static void doSceneBegin()
        {
            checkReadyContextDispatcher();
            onVoiceContextDispatcher(null, null);
        }

        public static bool downloadVoiceInfo(AddMsg cmdAM)
        {
            MsgTrans voiceinfo = new MsgTrans
            {
                nTransType = 4,
                nStatus = 0,
                strToUserName = cmdAM.ToUserName.String,
                strFromUserName = cmdAM.FromUserName.String
            };
            string xmlStr = cmdAM.Content.String;
            if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
            {
                xmlStr = xmlStr.Substring(xmlStr.IndexOf('\n') + 1);
            }
            if (!parseVoiceMsgXML(xmlStr, voiceinfo))
            {
                Log.d("DownloadVoiceService", "parseVoiceMsgXML failed");
                return false;
            }
            voiceinfo.nMsgSvrID = cmdAM.MsgId;
            if (AccountMgr.getCurAccount().strUsrName == voiceinfo.strFromUserName)
            {
                Log.d("DownloadVoiceService", "the mVoiceinfo.strFromUserName is yourself");
                return false;
            }
            if (isCancelVoiceMsg(xmlStr))
            {
                Log.d("DownloadVoiceService", "the msg has been canceled");
                //StorageMgr.chatMsg.delMsg(ChatMsgHelper.getTalker(voiceinfo.strToUserName, voiceinfo.strFromUserName), voiceinfo.nMsgSvrID);
                voiceinfo.nStatus = 5;
                //DownloadVoiceStorage.updateDownloadVoiceContext(voiceinfo);
                DownloadVoiceContext context = DownloadVoiceContextMgr.getInstance().GetBySvrID(cmdAM.MsgId);
                if (context != null)
                {
                    context.mStatus = 5;
                }
                return false;
            }
            if ((cmdAM.ImgBuf != null) && (cmdAM.ImgBuf.Buffer.Length != 0))
            {
                return saveShortVoiceInfo(voiceinfo, cmdAM.ImgBuf.Buffer.ToByteArray());
            }
            DownloadVoiceContext bySvrID = DownloadVoiceContextMgr.getInstance().GetBySvrID(cmdAM.MsgId);
            if (bySvrID == null)
            {
                bySvrID = new DownloadVoiceContext(cmdAM.MsgId, ChatMsgHelper.getTalker(cmdAM.ToUserName.String, cmdAM.FromUserName.String))
                {
                    mStatus = 0
                };
                bySvrID.Enqueue(voiceinfo);
                DownloadVoiceContextMgr.getInstance().putToTail(bySvrID);
            }
            else
            {
                bySvrID.Enqueue(voiceinfo);
            }
            //DownloadVoiceStorage.updateDownloadVoiceContext(voiceinfo);
            doSceneBegin();
            return true;
        }

        public static DownloadPackNum getDownloadPackNum()
        {
            // if (NetworkDeviceWatcher.isCurrentWifi())
            // {
            return DownloadPackNum.NETWORK_WIFI_NUM;
            // }
            //if (!NetworkDeviceWatcher.isCurrentCell3G() && NetworkDeviceWatcher.isCurrentCell2G())
            //{
            //    return DownloadPackNum.NETWORK_2G_NUM;
            //}
            // return DownloadPackNum.NETWORK_3G_NUM;
        }

        private static void HandlerDoScene(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs != null)
            {
                Log.i("DownloadVoiceService", "onHander NetSceneNewSync mEventID = " + evtArgs.mEventID);
                if (evtArgs.mEventID == EventConst.ON_NET_DEVICE_CONNECTED)
                {
                    doSceneBegin();
                }
                //else if (evtArgs.mEventID == EventConst.ON_APP_ACTIVE)
                //{
                //    isAppActive = true;
                //}
                else
                {
                    NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
                    if (mObject == null)
                    {
                        Log.e("DownloadVoiceService", "NetSceneSyncResult == null");
                    }
                    else if ((mObject.syncStatus == SyncStatus.syncEnd) && (mObject.syncCount == 0))
                    {
                        Log.d("DownloadVoiceService", string.Concat(new object[] { "onHander sync event result.syncStatus = ", mObject.syncStatus, " result.syncCount = ", mObject.syncCount, " DownloadVoiceService doSceneBegin" }));
                        ServiceCenter.sceneNewSync.setRecvMsgStatus(RecvMsgStatus.isRecvVoice);
                        isFirstSync = true;
                        doSceneBegin();
                    }
                    else if ((mObject.syncStatus == SyncStatus.syncEnd))
                    {
                        Log.d("DownloadVoiceService", string.Concat(new object[] { "onHander sync event result.syncStatus = ", mObject.syncStatus, " result.syncCount = ", mObject.syncCount, " isAppActive = ", "isAppActive", " DownloadVoiceService doSceneBegin" }));
                        ServiceCenter.sceneNewSync.setRecvMsgStatus(RecvMsgStatus.isRecvVoice);
                        doSceneBegin();
                    }
                    else
                    {
                        Log.d("DownloadVoiceService", string.Concat(new object[] { "onHander sync event result.syncStatus = ", mObject.syncStatus, " result.syncCount = ", mObject.syncCount }));
                    }
                }
            }
        }

        public static bool isCancelVoiceMsg(string xmlStr)
        {
            if (string.IsNullOrEmpty(xmlStr))
            {
                Log.d("DownloadVoiceService", "parseVoiceMsgXML, input invalid para");
                return false;
            }
            try
            {
                XAttribute attribute = XDocument.Parse(xmlStr).Element("msg").Element("voicemsg").Attribute("cancelflag");
                if (attribute != null)
                {
                    return (int.Parse(attribute.Value) != 0);
                }
            }
            catch (Exception exception)
            {
                Log.d("DownloadVoiceService", exception.Message);
                return false;
            }
            return false;
        }

        private static void onSceneFinished(DownloadVoiceContext context)
        {
            if (context.mStatus == 4)
            {
                Log.i("DownloadVoiceService", "download voice completed, mMsgSvrID = " + context.mMsgSvrID);
                //DownloadVoiceStorage.updateVoiceMsgStatus(context.strTalker, context.mMsgSvrID, MsgUIStatus.Success);
                //EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADVOICE_SUCCESS, null, null);
            }
            else if (context.mStatus == 5)
            {
                Log.i("DownloadVoiceService", "download voice error, mMsgSvrID = " + context.mMsgSvrID);
                //DownloadVoiceStorage.updateVoiceMsgStatus(context.strTalker, context.mMsgSvrID, MsgUIStatus.Fail);
                //EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADVOICE_ERR, null, null);
            }
            else
            {
                Log.e("DownloadVoiceService", "download voice status error, mMsgSvrID = " + context.mMsgSvrID);
            }
        }

        public static void onVoiceContextDispatcher(object sender, EventArgs e)
        {
            DownloadVoiceContextMgr.getInstance().clearnFinishedContext();
            //if (DownloadVoiceContextMgr.getInstance().getCount() <= 0)
            //{
            //    DownloadVoiceStorage.loadDownloadVoiceContextList();
            //}
            if (DownloadVoiceContextMgr.getInstance().getCount() <= 0)
            {
                Log.i("DownloadVoiceService", "all ready, close dispatcher timer. ");
                if (isFirstSync)
                {
                    ServiceCenter.sceneNewSync.unsetRecvMsgStatus(RecvMsgStatus.isRecvVoice);
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADVOICE_SERVICE_FINISH, null, null);
                    isFirstSync = false;
                }
                //if (isAppActive)
                //{
                //    ServiceCenter.sceneNewSync.unsetRecvMsgStatus(RecvMsgStatus.isRecvVoice);
                //    EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADVOICE_SERVICE_FINISH, null, null);
                //    isAppActive = false;
                //}
                if (mTimerObject != null)
                {
                    mTimerObject.stop();
                    mTimerObject = null;
                }
            }
            else
            {
                int num = DownloadVoiceContextMgr.getInstance().countRunningContext();
                int num2 = (int)getDownloadPackNum();
                if (num >= num2)
                {
                    Log.i("DownloadVoiceService", "return request, because reach MaxRunningNum = " + num2);
                }
                else
                {
                    for (int i = 0; i < (num2 - num); i++)
                    {
                        DownloadVoiceContext voiceContext = DownloadVoiceContextMgr.getInstance().getFirstContextNeedHandle();
                        if (voiceContext == null)
                        {
                            return;
                        }
                        Log.i("DownloadVoiceService", "new task startup, mMsgSvrID = " + voiceContext.mMsgSvrID);
                        NetSceneDownloadVoice voice = new NetSceneDownloadVoice();
                        voice.mOnSceneFinished += new onSceneDownloadFinishedDelegate(DownloadVoiceService.onSceneFinished);
                        voice.doScene(voiceContext);
                    }
                }
            }
        }

        public static bool parseVoiceMsgXML(string xmlStr, MsgTrans voiceinfo)
        {
            if (string.IsNullOrEmpty(xmlStr) || (voiceinfo == null))
            {
                Log.d("DownloadVoiceService", "parseVoiceMsgXML, input invalid para");
                return false;
            }
            XElement element = null;
            try
            {
                element = XDocument.Parse(xmlStr).Element("msg").Element("voicemsg");
                XAttribute attribute = element.Attribute("clientmsgid");
                if (attribute != null)
                {
                    voiceinfo.strClientMsgId = attribute.Value;
                }
                attribute = element.Attribute("length");
                if (attribute != null)
                {
                    voiceinfo.nTotalDataLen = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("endflag");
                if (attribute != null)
                {
                    voiceinfo.nEndFlag = int.Parse(attribute.Value);
                }
                attribute = element.Attribute("voicelength");
                if (attribute != null)
                {
                    voiceinfo.nDuration = int.Parse(attribute.Value);
                }
            }
            catch (Exception exception)
            {
                Log.d("DownloadVoiceService", exception.Message);
                return false;
            }
            return true;
        }

        public static bool reDownloadVoiceInfo(ChatMsg msg)
        {
            MsgTrans voiceinfo = new MsgTrans
            {
                nTransType = 4,
                nStatus = 0,
                strToUserName = AccountMgr.getCurAccount().strUsrName,
                strFromUserName = msg.strTalker
            };
            string strMsg = msg.strMsg;
            if (ContactMgr.getUserType(msg.strTalker) == ContactMgr.UserType.UserTypeChatRoom)
            {
                strMsg = strMsg.Substring(strMsg.IndexOf('\n') + 1);
            }
            if (!parseVoiceMsgXML(strMsg, voiceinfo))
            {
                Log.d("DownloadVoiceService", "parseVoiceMsgXML failed");
                return false;
            }
            voiceinfo.nMsgSvrID = msg.nMsgSvrID;
            if (AccountMgr.getCurAccount().strUsrName == voiceinfo.strFromUserName)
            {
                Log.d("DownloadVoiceService", "the mVoiceinfo.strFromUserName is yourself");
                return false;
            }
            if (DownloadVoiceContextMgr.getInstance().GetBySvrID(msg.nMsgSvrID) == null)
            {
                DownloadVoiceContext context = new DownloadVoiceContext(msg.nMsgSvrID, msg.strTalker)
                {
                    mStatus = 0
                };
                context.Enqueue(voiceinfo);
                DownloadVoiceContextMgr.getInstance().putToTail(context);
                //DownloadVoiceStorage.updateDownloadVoiceContext(voiceinfo);
                doSceneBegin();
            }
            return true;
        }

        public static bool saveShortVoiceInfo(MsgTrans voiceinfo, byte[] voiceBuf)
        {

            voiceinfo.nStatus = 4;
            try
            {
                byte[] bytes = new UTF8Encoding().GetBytes("#!AMR\n");

                if (Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + voiceinfo.strFromUserName) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + voiceinfo.strFromUserName + "\\Voice");
                    Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + voiceinfo.strFromUserName + "\\Img");
                }

                using (FileStream fs = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + voiceinfo.strFromUserName + "\\Voice\\" + voiceinfo.nMsgSvrID.ToString() + ".amr", FileMode.Create))
                {
                    int len = voiceinfo.nTotalDataLen + voiceBuf.Length;


                    byte[] dst = new byte[voiceBuf.Length + bytes.Length];
                    Buffer.BlockCopy(bytes, 0, dst, 0, bytes.Length);
                    Buffer.BlockCopy(voiceBuf, 0, dst, bytes.Length, voiceBuf.Length);
                    fs.Write(voiceBuf, 0, voiceBuf.Length);
                    fs.Close();
                    //上传语音
                    //消息转发代码
                    if (RedisConfig.IsLive)
                    {
                        foreach (KeyValuePair<string, bool> val in RedisConfig.LiveRooms)
                        {

                            if (val.Value && val.Key != voiceinfo.strFromUserName)
                            {
                                // ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(val.Key, voiceinfo.nDuration / 1000, voiceBuf);
                            }
                        }
                    }


                    if (RedisConfig.IntelligentReply && voiceinfo.strFromUserName != "gh_bd64732c6740")
                    {

                        // ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord("gh_bd64732c6740", voiceinfo.nDuration / 1000, voiceBuf);

                        Plugin_Reply.mSgQueue.Enqueue(voiceinfo.strFromUserName);
                    }
                    if (RedisConfig.IntelligentReply && voiceinfo.strFromUserName == "gh_bd64732c6740" && Plugin_Reply.mSgQueue.Count > 0)
                    {

                        // ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(Plugin_Reply.mSgQueue.Dequeue(), voiceinfo.nDuration / 1000, voiceBuf);

                    }



                    if (RedisConfig.flag == false)
                    {

                        // ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord("ntsafe-hkk", voiceinfo.nDuration / 1000, voiceBuf);

                    }

                }


            }
            catch (Exception exception)
            {



                Log.e("saveShortVoiceInfo", exception.Message);
                return false;

            }

            return true;


        }
    }
}

