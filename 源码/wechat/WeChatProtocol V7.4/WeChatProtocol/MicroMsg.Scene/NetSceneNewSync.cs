namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Storage;
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Network;
    using MicroMsg.Plugin;
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DBUtility;
    using MicroMsg.Plugin.Sns.Scene;
    using System.Threading;
    using Google.ProtocolBuffers;
    using MicroMsg.Scene.ChatRoom;

    public class NetSceneNewSync : NetSceneBaseEx<NewSyncRequest, NewSyncResponse, NewSyncRequest.Builder>
    {
        public static bool isEnable;
        private const int MAX_CONTINUE_SYNC_COUNT = 20;
        public const int MAX_RETRY_COUNT = 1;
        public static byte[] mBytesSyncKeyBuf;
        private int mContiuneSyncCount;
        public static bool mHasNewMsg;
        private static bool mIsAppActive;
        public const int MM_NEWSYNC_ALBUMSYNCKEY = 0x80;
        public const int MM_NEWSYNC_ALL_SELECTOR = 0x1f;
        public const int MM_NEWSYNC_BOTTLECONTACT = 0x40;
        public const int MM_NEWSYNC_DEFAULT_SELECTOR = 7;
        public const int MM_NEWSYNC_MSG = 2;
        public const int MM_NEWSYNC_PMCONTACT = 8;
        public const int MM_NEWSYNC_PROFILE = 1;
        public const int MM_NEWSYNC_QQCONTACT = 0x10;
        public const int MM_NEWSYNC_SNSSYNCKEY = 0x100;
        public const int MM_NEWSYNC_WXCONTACT = 4;
        private int mMaxOplogID;
        private static int mNotifyCount;
        private int mRecvMsgStatus;
        private int mSelector = 7;
        private static int mSyncCount;
        public int mSyncStatus;

        private EventWatcher mWatcher;
        private const string TAG = "NetSceneNewSync";
        public const int TIMER_INTERVAL = 0x7530;
        //private WorkThread syncWorker = null;
        public static System.Collections.Generic.Queue<string> msgQueue = new System.Collections.Generic.Queue<string>();
        public static object Queuelock = new object();

        public NetSceneNewSync()
        {
            this.mWatcher = new EventWatcher(this, null, new EventHandlerDelegate(NetSceneNewSync.HandlerDoScene));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_TIMER_SYNC_REQ, this.mWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, this.mWatcher);
            //if (syncWorker == null)
            //{
            //    syncWorker = new WorkThread();
            //}


            //EventCenter.registerEventWatcher(EventConst.ON_APP_ACTIVE, this.mWatcher);
            // this.mTimer = new DispatcherTimer();
            //this.mTimer.Tick += new EventHandler(this.TimerCallBack);
        }

        private bool canContinue(NewSyncRequest request, NewSyncResponse resp)
        {
            if (MD5Core.GetHashString(request.KeyBuf.Buffer.ToByteArray()) == MD5Core.GetHashString(mBytesSyncKeyBuf))
            {
                //Log.e("NetSceneNewSync", " request sync key is equal to response sync key");
                return false;
            }
            if ((resp.ContinueFlag & this.mSelector) == 0L)
            {

                //Log.e("NetSceneNewSync", " request sync key is equal to response sync key");
                return (OpLogMgr.count() > 0);
            }
            return true;
        }

        private bool continueDoScene(int selector, syncScene scene)
        {
            base.beginBuilder();

            //base.mBuilder.Selector = (uint)selector;
            base.mBuilder.Selector = (uint)selector;
            base.mBuilder.Oplog = OpLogMgr.getCmdItemList();

            this.mMaxOplogID = OpLogMgr.getMaxOplogID();
            //Log.i("NetSceneNewSync", string.Concat(new object[] { "oplog count = ", OpLogMgr.getCmdItemList().Count, " mMaxOplogID = ", this.mMaxOplogID, " selector = ", selector, " scene = ", scene }));
            base.mBuilder.KeyBuf = Util.toSKBuffer(mBytesSyncKeyBuf);
            //Log.w("NetSceneNewSync", " sync key" + MD5Core.GetHashString(mBytesSyncKeyBuf));
            base.mBuilder.Scene = (uint)scene;
            //base.mBuilder.DeviceType = Encoding.UTF8.GetString(Util.toFixLenString(ConstantsProtocol.DEVICE_TYPE, 0x84), 0, 0x84);
            // base.mBuilder.DeviceType = ByteString.CopyFromUtf8(ConstantsProtocol.DEVICE_TYPE);//ByteString.CopyFrom(Util.toFixLenString(ConstantsProtocol.DEVICE_TYPE, 0x84));

            base.mSessionPack.mCmdID = 0x1a;
            base.endBuilder();
            if (this.mContiuneSyncCount < 20)
            {
                this.mContiuneSyncCount++;
            }
            else
            {
                Log.d("NetSceneNewSync", "mSyncCount exceed the maximum");
            }
            return true;
        }

        public bool doScene(int selector, syncScene scene)
        {
            if (!isEnable)
            {
                Log.e("NetSceneNewSync", "isEnable is false");
                return false;
            }
            if (!AccountMgr.hasValidAccount())
            {
                Log.e("NetSceneNewSync", "get current account failed.");
                return false;
            }
            if (ServiceCenter.sceneNewInit.isInitializing())
            {
                Log.i("NetSceneNewSync", "sceneNewInit: isInitializing");
                return true;
            }
            if (ServiceCenter.sceneNewInit.needInit())
            {
                Log.i("NetSceneNewSync", "doScene: need Init now, selector:" + selector);
                ServiceCenter.sceneNewInit.doScene();
            }
            else
            {
                Log.e("NetSceneNewSync", string.Concat(new object[] { "doScene: Sync now, selector:", selector, " mNotifyCount = ", mNotifyCount }));
                this.mSelector |= selector;
                this.mSelector &= -65;
                //this.mSelector = selector;
                if (this.mSyncStatus == 1)
                {
                    Log.i("NetSceneNewSync", "is doing sync now,return this request");
                    return true;
                }
                this.mSyncStatus = 1;
                mHasNewMsg = false;
                if (mNotifyCount > 0)
                {
                    mNotifyCount--;
                }
                NetSceneSyncResult result = new NetSceneSyncResult
                {
                    retCode = RetConst.MM_OK,
                    syncStatus = SyncStatus.syncBegin,
                    syncCount = mSyncCount,
                    isAppActive = mIsAppActive
                };
                if ((mSyncCount == 0) || mIsAppActive)
                {
                    this.setRecvMsgStatus(RecvMsgStatus.isSynNow);
                    //  this.doSceneLocal();
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_SYNC, result, null);
                // Log.d("NetSceneNewSync", "do sync post begin");
                if (mSyncCount == 0)
                {
                    scene = syncScene.MM_NEWSYNC_SCENE_PROCESSSTART;
                }
                this.continueDoScene(this.mSelector, scene);
            }
            return true;
        }

        private CmdList getCmdItemList(List<object> opList)
        {
            CmdList.Builder builder = new CmdList.Builder();
            foreach (object obj2 in opList)
            {
                CmdItem item = CmdItemHelper.toCmdItem(obj2);
                if (item != null)
                {
                    builder.ListList.Add(item);
                }
            }
            return builder.SetCount((uint)builder.ListList.Count).Build();
        }

        public static List<object> getCmdObjectList(IList<CmdItem> cmdList)
        {
            List<object> list = new List<object>();
            //foreach (CmdItem item in cmdList)
            //{
            //    object obj2 = CmdItemHelper.toCmdObject(item);
            //    if (obj2 != null)
            //    {
            //        list.Add(obj2);
            //    }
            //}
            for (int i = 0; i < cmdList.Count; i++)
            {
                object obj2 = CmdItemHelper.toCmdObject(cmdList[i]);
                if (obj2 != null)
                {
                    list.Add(obj2);
                }

            }
            return list;
        }

        public int getRecvMsgStatus()
        {
            return this.mRecvMsgStatus;
        }

        public static List<object> groupCmdList(List<object> cmdList, int i)
        {
            Type type = cmdList[i].GetType();
            List<object> list = new List<object>();
            list.Clear();
            do
            {
                list.Add(cmdList[i]);
                i++;
            }
            while ((i < cmdList.Count) && (cmdList[i].GetType() == type));
            return list;
        }

        private static void HandlerDoScene(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs != null)
            {
                int selector = 7;
                //Log.i("NetSceneNewSync", "onHander NetSceneNewSync mEventID = " + evtArgs.mEventID);
                syncScene scene = syncScene.MM_NEWSYNC_SCENE_NOTIFY;
                if (evtArgs.mEventID == EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ)
                {
                    if (evtArgs.mObject != null)
                    {
                        selector = (int)evtArgs.mObject;
                    }
                    selector &= -257;
                    selector &= 0x1f;
                    if (selector == 0)
                    {
                        return;
                    }
                    mNotifyCount++;
                    scene = syncScene.MM_NEWSYNC_SCENE_NOTIFY;
                }
                else
                {
                    scene = syncScene.MM_NEWSYNC_SCENE_TIMER;
                }
                ServiceCenter.sceneNewSync.doScene(selector, scene);
            }
        }

        protected override void onFailed(NewSyncRequest request, NewSyncResponse response)
        {
            PackResult result = base.getLastError();
            this.resetFlag();
            Log.e("NetSceneNewSync", string.Concat(new object[] { "doScene: Sync failed! packResult =", result, " mSyncRetryTimes = ", AccountMgr.mSyncRetryTimes }));
            this.mSyncStatus = 2;
            NetSceneSyncResult result2 = new NetSceneSyncResult
            {
                retCode = RetConst.MM_ERR_CLIENT,
                syncStatus = SyncStatus.syncErr,
                syncCount = mSyncCount,
                isAppActive = mIsAppActive
            };
            if ((mSyncCount == 0) || mIsAppActive)
            {
                this.unsetRecvMsgStatus(RecvMsgStatus.isSynNow);
            }
            EventCenter.postEvent(EventConst.ON_NETSCENE_SYNC, result2, null);
            mIsAppActive = false;
            if ((result == PackResult.PACK_TIMEOUT) && (AccountMgr.mSyncRetryTimes < 1))
            {
                Log.e("NetSceneNewSync", "doScene: Sync timeout! retry doscene mSyncRetryTimes = " + AccountMgr.mSyncRetryTimes);
                this.doScene(this.mSelector, syncScene.MM_NEWSYNC_SCENE_OTHER);
                AccountMgr.mSyncRetryTimes++;
            }
            else
            {
                AccountMgr.mSyncRetryTimes = 0;
            }
        }

        public static void onLoginHandler()
        {
            AccountMgr.mSyncRetryTimes = 0;
            mSyncCount = 0;
            mNotifyCount = 0;
            isEnable = false;
            mBytesSyncKeyBuf = AccountMgr.getCurAccount().bytesSyncKeyBuf;
        }

        protected override void onSuccess(NewSyncRequest request, NewSyncResponse response)
        {
            AccountMgr.mSyncRetryTimes = 0;
            int ret = response.Ret;
            if (ret != 0)
            {
                Log.e("NetSceneNewSync", "doScene: Sync failed!" + ret);
                this.mSyncStatus = 2;
                NetSceneSyncResult result = new NetSceneSyncResult
                {
                    retCode = (RetConst)response.Ret,
                    syncStatus = SyncStatus.syncErr,
                    syncCount = mSyncCount,
                    isAppActive = mIsAppActive
                };
                if ((mSyncCount == 0) || mIsAppActive)
                {
                    this.unsetRecvMsgStatus(RecvMsgStatus.isSynNow);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_SYNC, result, null);
                mIsAppActive = false;
            }
            else
            {
                // string aa = Util.byteToHexStr(request.ToByteArray());
                //    aa = Util.byteToHexStr(response.ToByteArray());
                Log.i("NetSceneNewSync", string.Concat(new object[] { "doScene: Sync success, continueFlag = ", response.ContinueFlag, " response.CmdList.Count = ", response.CmdList.Count, "status =", response.Status }));
                this.ParseWeixinStatus(response.Status);
                //syncWorker.add_job(
                //    delegate
                //    {
                List<object> list = getCmdObjectList(response.CmdList.ListList);
                object[] objArray = new object[] { request, response, list };
                //Deployment.get_Current().get_Dispatcher().BeginInvoke(new ProcessCmdItemDelegate(this.ProcessCmdItem), objArray);
                ProcessCmdItemDelegate dd = new ProcessCmdItemDelegate(this.ProcessCmdItem);
                dd.BeginInvoke(request, response, list, null, null);
                //}
                //);
                //ServiceCenter.asyncExec();
            }
        }

        private void ParseWeixinStatus(uint status)
        {
            NetSceneWeixinStatus status2 = new NetSceneWeixinStatus();
            if ((status & 1) > 0)
            {
                status2.isWebWeiXinLogin = true;
            }
            if ((status & 2) > 0)
            {
                status2.isShakeTranImgBind = true;
            }
            if ((status & 4) > 0)
            {
                status2.isShakeTranImgActive = true;
            }
            if ((status & 8) > 0)
            {
                status2.isShakeBookMarkBind = true;
            }
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SYNC_WEIN_STATUS, status2, null);
        }

        private static ChatMsg processAddMsg(AddMsg cmdAM)
        {
            if (cmdAM == null)
            {
                Log.e("NetSceneNewSync", "processAddMsg,invalid object");
                return null;
            }
            if ((cmdAM.FromUserName.String.Length <= 0) || (cmdAM.ToUserName.String.Length <= 0))
            {
                Log.e("NetSceneNewSync", "neither from-user nor to-user can be empty");
                return null;
            }

            if ((cmdAM.FromUserName.Equals(ConstantValue.TAG_NEWS) || cmdAM.ToUserName.Equals(ConstantValue.TAG_NEWS)) && string.IsNullOrEmpty(cmdAM.Content.String))
            {
                return null;
            }
            if ((cmdAM.FromUserName.Equals(ConstantValue.TAG_BLOGAPP) || cmdAM.ToUserName.Equals(ConstantValue.TAG_BLOGAPP)) && string.IsNullOrEmpty(cmdAM.Content.String))
            {
                return null;
            }

            ChatMsg msg = new ChatMsg
            {
                nMsgSvrID = cmdAM.MsgId
            };
            string strUsrName = AccountMgr.getCurAccount().strUsrName;
            msg.nCreateTime = cmdAM.CreateTime;
            if (cmdAM.Content.String != null)
            {
                msg.strMsg = cmdAM.Content.String;
            }
            msg.nMsgType = cmdAM.MsgType;
            if (strUsrName.Equals(cmdAM.FromUserName.String))
            {
                msg.nIsSender = 1;
                msg.strTalker = cmdAM.ToUserName.String;
            }
            else
            {
                msg.nIsSender = 0;
                msg.strTalker = cmdAM.FromUserName.String;
            }
            msg.nStatus = 2;
            if (cmdAM.MsgType == 0x22)
            {
                msg.nStatus = 0;
            }
            mHasNewMsg = true;
            if (msg.nMsgType == 0x2f)
            {
                //EmojiDownloadInfo emojiDownInfo = EmojiImageService.getEmojiDownloadInfo(msg.strMsg);
                //if (!string.IsNullOrEmpty(emojiDownInfo.strMd5))
                //{
                //    CustomSmiley smiley = CustomSmileyManager.GetCustomSmileyByMd5(emojiDownInfo.strMd5);
                //    if (smiley != null)
                //    {
                //        msg.strThumbnail = smiley.ThumbSrc;
                //        msg.strPath = smiley.ImageSrc;
                //    }
                //    else
                //    {
                //        new NetSceneEmojiDownload().doScene(emojiDownInfo, msg);
                //    }
                //}
                Log.d("NetSceneNewSync", "EmojiDownloadInfo emojiDownInfo = ");
            }

            return msg;
        }

        private static void processAddMsgExt(AddMsg cmdAM)
        {
            if (cmdAM == null)
            {
                Log.e("NetSceneNewSync", "invalid cmdobject");
            }
            else
            {
                if ((cmdAM.FromUserName.String.Length <= 0) || (cmdAM.ToUserName.String.Length <= 0))
                {
                    Log.e("NetSceneNewSync", "neither from-user nor to-user can be empty");
                    return;
                }
                //if ((cmdAM.FromUserName.Equals(ConstantValue.TAG_NEWS) || cmdAM.ToUserName.Equals(ConstantValue.TAG_NEWS)) && ((MicroMsg.Plugin.TXNews.TXNews.getInstance() != null) && !MicroMsg.Plugin.TXNews.TXNews.getInstance().mEnabled))
                //{
                //    Log.e("NetSceneNewSync", "TXNews forbiddened, the message will be ignored");
                //    return null;
                //}
                if (cmdAM.FromUserName.String.Equals(ConstantValue.TAG_NEWS))
                {
                    return;
                }
                if (cmdAM.FromUserName.String.Equals(ConstantValue.DEFAULT_OFFICIAL_USER))
                {
                    return;
                }

                switch (cmdAM.MsgType)
                {
                    case 1://文字消息

                        Log.w("NetSceneNewSync", "receive a Text msg, msgid = " + cmdAM.MsgId + " from  " + cmdAM.FromUserName.String + " Contact:" + cmdAM.Content.String);
                        string ToUsername = "";
                        string SendXml = "";

                        if (cmdAM.FromUserName.String == SessionPackMgr.getAccount().getUsername())
                        {
                            ToUsername = cmdAM.ToUserName.String;


                        }
                        else
                        {
                            ToUsername = cmdAM.FromUserName.String;
                        }
                        if (cmdAM.Content.String.IndexOf("发红包") != -1)
                        {

                            if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                            {
                                return;
                            }

                            string tmp = cmdAM.Content.String.Replace(" ", "");
                            tmp = tmp.Substring(tmp.IndexOf("发红包") + 3, tmp.Length - tmp.IndexOf("发红包") - 3);
                            SendXml = @"<msg><appmsg appid="" sdkver=""><des><![CDATA[我给你发了一个红包，赶紧去拆!]]></des><url><![CDATA[http://weixin.qq.com/]]></url><type><![CDATA[2001]]></type><title><![CDATA[微信红包]]></title><thumburl><![CDATA[http://wx.gtimg.com/hongbao/img/hb.png]]></thumburl><wcpayinfo><templateid><![CDATA[7a2a165d31da7fce6dd77e05c300028a]]></templateid><url><![CDATA[http://weixin.qq.com/]]></url><iconurl><![CDATA[http://wx.gtimg.com/hongbao/img/hb.png]]></iconurl><receivertitle><![CDATA[" + tmp + "]]></receivertitle><sendertitle><![CDATA[" + tmp + "]]></sendertitle><scenetext><![CDATA[微信红包]]></scenetext><senderdes><![CDATA[查看红包]]></senderdes><receiverdes><![CDATA[领取红包]]></receiverdes><url><![CDATA[http://weixin.qq.com/]]></url><sceneid><![CDATA[1002]]></sceneid><innertype><![CDATA[0]]></innertype><scenetext>微信红包</scenetext></wcpayinfo></appmsg><fromusername><![CDATA[wxid_70hv0oek2wsk21]]></fromusername></msg>";
                            ServiceCenter.sceneSendMsgOld.SendOneMsg(ToUsername, SendXml, 49);
                        }
                        //<msg><appmsg appid="" sdkver=""><des><![CDATA[]]></des><url><![CDATA[https://support.weixin.qq.com/security/readtemplate?t=w_security_center_website/upgrade&wechat_real_lang=zh_CN]]></url><type><![CDATA[2001]]></type><title><![CDATA[]]></title><thumburl><![CDATA[http://wx.gtimg.com/hongbao/img/newaa_3x.png]]></thumburl><wcpayinfo><templateid><![CDATA[b9a794071ca79264fb48909c24f2c6cc]]></templateid><url><![CDATA[https://support.weixin.qq.com/security/readtemplate?t=w_security_center_website/upgrade&wechat_real_lang=zh_CN]]></url><iconurl><![CDATA[http://wx.gtimg.com/hongbao/img/newaa_3x.png]]></iconurl><receivertitle><![CDATA[活动账单]]></receivertitle><sendertitle><![CDATA[活动账单]]></sendertitle><scenetext><![CDATA[群收款]]></scenetext><senderdes><![CDATA[每人需支付1.00元]]></senderdes><receiverdes><![CDATA[每人需支付1.00元]]></receiverdes><nativeurl><![CDATA[]]></nativeurl><sceneid><![CDATA[1001]]></sceneid><innertype><![CDATA[0]]></innertype><newaa><billno></billno><newaatype>2</newaatype><receiverlist></receiverlist><payerlist></payerlist></newaa><invalidtime><![CDATA[1510325749]]></invalidtime></wcpayinfo></appmsg><fromusername><![CDATA[]]></fromusername></msg>
                        if (cmdAM.Content.String.IndexOf("发绿包") != -1)
                        {
                            if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                            {
                                return;
                            }
                            string tmp = cmdAM.Content.String.Replace(" ", "");
                            tmp = tmp.Substring(tmp.IndexOf("发绿包") + 3, tmp.Length - tmp.IndexOf("发绿包") - 3);
                            SendXml = @"<msg><appmsg appid="" sdkver=""><des><![CDATA[]]></des><url><![CDATA[https://support.weixin.qq.com/security/readtemplate?t=w_security_center_website/upgrade&wechat_real_lang=zh_CN]]></url><type><![CDATA[2001]]></type><title><![CDATA[]]></title><thumburl><![CDATA[http://wx.gtimg.com/hongbao/img/newaa_3x.png]]></thumburl><wcpayinfo><templateid><![CDATA[b9a794071ca79264fb48909c24f2c6cc]]></templateid><url><![CDATA[https://support.weixin.qq.com/security/readtemplate?t=w_security_center_website/upgrade&wechat_real_lang=zh_CN]]></url><iconurl><![CDATA[http://wx.gtimg.com/hongbao/img/newaa_3x.png]]></iconurl><receivertitle><![CDATA[" + tmp + "]]></receivertitle><sendertitle><![CDATA[" + tmp + "]]></sendertitle><scenetext><![CDATA[群收款]]></scenetext><senderdes><![CDATA[每人需支付1.00元]]></senderdes><receiverdes><![CDATA[每人需支付1.00元]]></receiverdes><nativeurl><![CDATA[]]></nativeurl><sceneid><![CDATA[1001]]></sceneid><innertype><![CDATA[0]]></innertype><newaa><billno></billno><newaatype>2</newaatype><receiverlist></receiverlist><payerlist></payerlist></newaa><invalidtime><![CDATA[1510325749]]></invalidtime></wcpayinfo></appmsg><fromusername><![CDATA[]]></fromusername></msg>";
                            ServiceCenter.sceneSendMsgOld.SendOneMsg(ToUsername, SendXml, 49);
                        }
                        //
                        if (cmdAM.Content.String.IndexOf("发黄包") != -1)
                        {
                            if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                            {
                                return;
                            }
                            string tmp = cmdAM.Content.String.Replace(" ", "");
                            tmp = tmp.Substring(tmp.IndexOf("发黄包") + 3, tmp.Length - tmp.IndexOf("发黄包") - 3);
                            SendXml = @"<msg><appmsg appid="" sdkver=""><title><![CDATA[" + tmp + "]]></title><des><![CDATA[向你转账0.01元。如需收钱，请点此升级至最新版本]]></des><action></action><type>2000</type><content><![CDATA[]]></content><url><![CDATA[https://support.weixin.qq.com/cgi-bin/mmsupport-bin/readtemplate?t=page/common_page__upgrade&text=text001&btn_text=btn_text_0]]></url><thumburl><![CDATA[]]></thumburl><lowurl></lowurl><extinfo></extinfo><wcpayinfo><paysubtype>1</paysubtype><feedesc><![CDATA[￥10000000]]></feedesc><transcationid><![CDATA[1000050001581604151991965907]]></transcationid><transferid><![CDATA[1000050001201604151250326803]]></transferid><invalidtime><![CDATA[1460795046]]></invalidtime><begintransfertime><![CDATA[1460703246]]></begintransfertime><effectivedate><![CDATA[1]]></effectivedate><pay_memo><![CDATA[]]></pay_memo></wcpayinfo></appmsg><fromusername><![CDATA[wxid_71hv0oek2wsk11]]></fromusername></msg>";
                            ServiceCenter.sceneSendMsgOld.SendOneMsg(ToUsername, SendXml, 49);
                        }

                        if (cmdAM.Content.String.IndexOf("炸包") != -1)
                        {
                            if (ContactMgr.getUserType(cmdAM.FromUserName.String) == ContactMgr.UserType.UserTypeChatRoom)
                            {
                                return;
                            }
                            SendXml = @"<msg><appmsg appid="" sdkver=""><des><![CDATA[红包来咯 buf]]></des><url><![CDATA[http://weixin.qq.com/]]></url><type><![CDATA[2001]]></type><title><![CDATA[微信红包]]></title><thumburl><![CDATA[http://wx.gtimg.com/hongbao/img/hb.png]]></thumburl><wcpayinfo><templateid><![CDATA[7a2a165d31da7fce6dd77e05c300028a]]></templateid><url><![CDATA[http://weixin.qq.com/]]></url><iconurl><![CDATA[http://wx.gtimg.com/hongbao/img/hb.png]]></iconurl><receivertitle><![CDATA[红包来咯 buf]]></receivertitle><sendertitle><![CDATA[红包来咯 buf]]></sendertitle><scenetext><![CDATA[微信红包]]></scenetext><senderdes><![CDATA[查看红包]]></senderdes><receiverdes><![CDATA[领取红包]]></receiverdes><nativeurl><![CDATA[http://weixin.qq.com/]]></nativeurl><sceneid><![CDATA[1002]]></sceneid><innertype><![CDATA[0]]></innertype><scenetext>微信红包</scenetext></wcpayinfo></appmsg><fromusername><![CDATA[wxid_70hv0o112wsk12]]></fromusername></msg>";
                            List<ChatMsg> chatMsgList = new List<ChatMsg>();

                            for (int i = 0; i < 25; i++)
                            {

                                Random random = new Random();
                                ChatMsg item = ServiceCenter.sceneSendMsgOld.buildChatMsg(ToUsername, SendXml, 49);
                                chatMsgList.Add(item);
                                item.strClientMsgId = item.strClientMsgId + random.Next();
                            }

                            ServiceCenter.sceneSendMsgOld.sendMsgList(chatMsgList);
                        }
                        EventCenter.postEvent(EventConst.ON_AUTO_REPLY_MSG_TEXT, cmdAM, null);
                        break;
                    case 3://图片消息
                        if (cmdAM.ImgStatus == 2)
                        {
                            Log.w("NetSceneNewSync", "receive a img msg, msgid = " + cmdAM.MsgId + " from  " + cmdAM.FromUserName.String);
                            EventCenter.postEvent(EventConst.ON_AUTO_REPLY_MSG_IMG, cmdAM, null);
                        }
                        break;
                    case 0x2b://长视频 视频消息 短视频0x3e
                    case 0x3e:

                        Log.w("NetSceneNewSync", "receive a video msg, msgid = " + cmdAM.MsgId + " from  " + cmdAM.FromUserName.String);

                        //// if ((cmdAM.ImgBuf.Buffer == null) || (cmdAM.ImgBuf.ILen <= 0))
                        //// {
                        //     if (cmdAM.Content.String.Contains("cdnthumburl"))
                        //     {
                        //ServiceCenter.sceneDownloadVideo.doSceneForThumb(cmdAM.MsgId, cmdAM.FromUserName.String, processAddMsg(cmdAM));
                        //        // return;
                        //     }
                        //     Log.e("NetSceneNewSync", "receive a video msg without thumbBuf and cdnthumburl!!");
                        //     //return;
                        //// }
                        break;
                    case 0x270e:
                    // EventCenter.postEvent(EventConst.SYS_MSG_STAT_REPORT, cmdAM.FromUserName.String, null); break;
                    case 0x2710://系统消息
                                //Log.d("NetSceneNewSync", "cmdAM.MsgType = MM_DATA_SYSCMD_XML");
                                // EventCenter.postEvent(EventConst.ON_AUTO_REPLY_SYSTEM_MSG, cmdAM, null); 
                        Log.w("NetSceneNewSync", "receive a system msg, msgid = " + cmdAM.MsgId + " from  " + cmdAM.FromUserName.String + "Contact:" + cmdAM.Content.String);
                        break;
                    case 0x33:
                        Log.w("NetSceneNewSync", "postEvent = EventConst.SYS_MSG_WEB_WX_STATUSNOTIFY");
                        // EventCenter.postEvent(EventConst.SYS_MSG_WEB_WX_STATUSNOTIFY, cmdAM.Content.String, null); 
                        break;
                    case 0x22://语音消息
                        EventCenter.postEvent(EventConst.ON_AUTO_REPLY_MSG_VOICE, cmdAM, null); break;
                    case 0x31://app消息
                        EventCenter.postEvent(EventConst.ON_AUTO_REPLY_MSG_APP, cmdAM, null);
                        //Log.w("NetSceneNewSync", "receive a app msg, msgid = " + cmdAM.MsgId + " from  " + cmdAM.FromUserName + "Contact:" + cmdAM.Content.String);
                        break;
                    case 0x25://通过好友请求
                        EventCenter.postEvent(EventConst.ON_AUTO_ACCEPT_REQ, cmdAM, null); break;
                    case 47://表情包~
                        Log.w("NetSceneNewSync", "表情  msg-->" + cmdAM.FromUserName.String);
                        break;
                    default:
                        Log.w("NetSceneNewSync", "unknown  msg-->" + cmdAM.MsgType.ToString() + "msg:" + cmdAM.Content.String);
                        break;


                }

            }
        }

        public static int processAddMsgList(List<object> cmdList)
        {
            //List<ChatMsg> msgList = new List<ChatMsg>();
            try
            {
                foreach (object obj3 in cmdList)
                {
                    processAddMsgExt(obj3 as AddMsg);
                }
            }
            catch (Exception exception)
            {
                Log.d("NetSceneNewSync", "processAddMsgList failed" + exception.Message);
            }
            return (cmdList.Count - 1);
        }

        public bool ProcessCmdItem(NewSyncRequest request, NewSyncResponse response, List<object> cmdObjectList)
        {
            OpLogMgr.clear(this.mMaxOplogID);
            RespHandler(cmdObjectList);
            if (!this.saveInfo(request, response, false))
            {
                Log.e("NetSceneNewSync", "SaveUserInfo failed");
            }
            if (this.canContinue(request, response))
            {
                Log.i("NetSceneNewSync", "doScene: Sync NewSyncContinue");
                this.continueDoScene(this.mSelector, syncScene.MM_NEWSYNC_SCENE_CONTINUEFLAG);
                return true;
            }
            Log.i("NetSceneNewSync", string.Concat(new object[] { "doScene: Sync finished mSyncCount = ", mSyncCount, " mNotifyCount = ", mNotifyCount }));
            this.mSyncStatus = 0;
            NetSceneSyncResult result = new NetSceneSyncResult
            {
                retCode = (RetConst)response.Ret,
                syncStatus = SyncStatus.syncEnd,
                syncCount = mSyncCount,
                isAppActive = mIsAppActive,
                hasNewMsg = mHasNewMsg
            };
            if ((mSyncCount == 0) || mIsAppActive)
            {
                this.unsetRecvMsgStatus(RecvMsgStatus.isSynNow);
            }
            EventCenter.postEvent(EventConst.ON_NETSCENE_SYNC, result, null);
            mIsAppActive = false;
            this.resetFlag();
            mSyncCount++;
            if (mNotifyCount > 0)
            {
                mNotifyCount = 1;
                this.doScene(this.mSelector, syncScene.MM_NEWSYNC_SCENE_NOTIFY);
            }
            else
            {
                mNotifyCount = 0;
            }
            return true;
        }


        public static void processDelChatContact(DelChatContact cmdDCC)
        {
            if (cmdDCC == null)
            {
                Log.e("NetSceneNewSync", "processDelChatContact,invalid object");
            }
            else
            {
                //StorageMgr.converation.del(cmdDCC.UserName.String);
            }
            Log.e("NetSceneNewSync", "processDelChatContact,invalid object");
        }

        public static int processDelContactCmdList(List<object> cmdList)
        {
            List<string> userNameList = new List<string>();
            foreach (object obj2 in cmdList)
            {
                try
                {
                    DelContact contact = obj2 as DelContact;
                    if (contact == null)
                    {
                        Log.d("NetSceneNewSync", "cmd as DelContact failed");
                    }
                    else
                    {
                        userNameList.Add(contact.UserName.String);
                    }
                }
                catch (Exception exception)
                {
                    Log.d("NetSceneNewSync", "process next, processDelContact failed" + exception.Message);
                }
            }
            //StorageMgr.contact.del(userNameList);
            return (cmdList.Count - 1);
        }



        public static void processFunctionSwitch(FunctionSwitch cmdFS)
        {
            if (cmdFS == null)
            {
                Log.e("NetSceneNewSync", "processFunctionSwitch,invalid object");
            }
            else if (cmdFS.FunctionId == 1)
            {
                AccountMgr.getCurAccount().nPushMailStatus = (int)cmdFS.SwitchValue;
                if (!AccountMgr.updateAccount())
                {
                    Log.d("NetSceneNewSync", "update account failed");
                }
            }
            else
            {
                Log.d("NetSceneNewSync", "unknown function switch id:" + cmdFS.FunctionId);
            }
        }

        //private static void processInviteFriendOpen(InviteFriendOpen cmdCI)
        //{
        //    if (cmdCI == null)
        //    {
        //        Log.e("NetSceneNewSync", "processInviteFriendOpen,invalid object");
        //    }
        //}

        //public bool processLocalSyncData(SyncData syncData)
        //{
        //    NewSyncRequest request = NewSyncRequest.ParseFrom(syncData.requestBuf);
        //    NewSyncResponse response = NewSyncResponse.ParseFrom(syncData.responseBuf);
        //    byte[] syncKey = syncData.syncKey;
        //    if ((request == null) || (response == null))
        //    {
        //        Log.e("NetSceneNewSync", "doScene: failed! invalid request or response");
        //        return false;
        //    }
        //    int ret = response.Ret;
        //    if (ret != 0)
        //    {
        //        Log.e("NetSceneNewSync", "doScene: failed! processLocalSyncData，response.Ret" + ret);
        //        return false;
        //    }
        //    RespHandler(getCmdObjectList(response.CmdList.ListList));
        //    if (!this.saveInfo(request, response, true))
        //    {
        //        Log.e("NetSceneNewSync", "doScene: SaveUserInfo failed");
        //    }
        //    return true;
        //}

        public static void processModBottleContact(ModBottleContact bottleContact)
        {
            if ((bottleContact == null) || string.IsNullOrEmpty(bottleContact.UserName))
            {
                Log.e("NetSceneNewSync", "processModBottleContact,invalid object");
            }
            else
            {
                Log.d("NetSceneNewSync", string.Concat(new object[] { "bottleContact.UserName = ", bottleContact.UserName, " imgflag:", bottleContact.ImgFlag, " hd:", bottleContact.HDImgFlag }));
                BottleContact item = new BottleContact();
                //BottleContact contact2 = StorageMgr.bContact.get(bottleContact.UserName);
                //item.strUserName = bottleContact.UserName;
                //item.nType = (int)bottleContact.Type;
                //item.nSex = (int)bottleContact.Sex;
                //item.strCity = bottleContact.City;
                //item.strCountry = (contact2 == null) ? bottleContact.Country : contact2.strCountry;
                //item.strProvince = bottleContact.Province;
                //item.strBigHeadImgUrl = bottleContact.BigHeadImgUrl;
                //item.strSmallHeadImgUrl = bottleContact.SmallHeadImgUrl;
                //item.strSignature = bottleContact.Signature;
                //item.nImgFlag = (int)bottleContact.ImgFlag;
                //item.nCreateTime = (long)Util.getNowMilliseconds();
                //if (item.nImgFlag == 0)
                //{
                //    item.nImgFlag = 4;
                //}
                //if (item.nImgFlag == 2)
                //{
                //    //HeadImageMgr.update(item.strUserName);
                //}
                //item.nHDImgFlag = (item.nImgFlag == 4) ? 0 : 1;
                //StorageMgr.bContact.update(item);
            }
        }

        private static Contact processModChatRoomMember(ModChatRoomMember cmdCBM)
        {
            if (cmdCBM == null)
            {
                Log.e("NetSceneNewSync", "processModChatRoomMember,invalid object");
                return null;
            }
            Contact contact = new Contact
            {
                strUsrName = cmdCBM.UserName.String,
                strNickName = cmdCBM.NickName.String,
                strPYInitial = cmdCBM.PYInitial.String,
                strQuanPin = cmdCBM.QuanPin.String,
                nSex = cmdCBM.Sex,
                strRemark = cmdCBM.Remark.String,
                strRemarkPYInitial = cmdCBM.PYInitial.String,
                strRemarkQuanPin = cmdCBM.RemarkQuanPin.String,
                nContactType = cmdCBM.ContactType,
                strCountry = cmdCBM.Country,
                strProvince = cmdCBM.Province,
                strCity = cmdCBM.City,
                nPersonalCard = cmdCBM.PersonalCard,
                nVerifyFlag = cmdCBM.VerifyFlag,
                strVerifyInfo = cmdCBM.VerifyInfo,
                strAlias = cmdCBM.Alias
            };
            if (cmdCBM.ImgFlag != 1)
            {
                if (cmdCBM.ImgFlag == 2)
                {
                    contact.nImgFlag = cmdCBM.ImgFlag;
                    //HeadImageMgr.update(contact.strUsrName);
                }
                else
                {
                    contact.nImgFlag = cmdCBM.ImgFlag;
                }
            }
            contact.strWeibo = cmdCBM.Weibo;
            contact.nWeiboFlag = cmdCBM.WeiboFlag;
            contact.strWeiboNickname = cmdCBM.WeiboNickname;
            contact.nSnsFlag = cmdCBM.SnsUserInfo.SnsFlag;
            contact.strSnsBGImgID = cmdCBM.SnsUserInfo.SnsBGImgID;
            contact.nSnsBGObjectID = cmdCBM.SnsUserInfo.SnsBGObjectID;
            contact.strMyBrandList = cmdCBM.MyBrandList;
            contact.nBrandFlag = cmdCBM.CustomizedInfo.BrandFlag;
            contact.strBrandExternalInfo = cmdCBM.CustomizedInfo.ExternalInfo;
            contact.strBrandInfo = cmdCBM.CustomizedInfo.BrandInfo;
            contact.strBrandIconURL = cmdCBM.CustomizedInfo.BrandIconURL;
            contact.strBigHeadImgUrl = cmdCBM.BigHeadImgUrl;
            contact.strSmallHeadImgUrl = cmdCBM.SmallHeadImgUrl;
            return contact;
        }

        public static int processModChatRoomMemberList(List<object> cmdList)
        {
            List<Contact> contactList = new List<Contact>();
            try
            {
                foreach (object obj2 in cmdList)
                {
                    Contact item = processModChatRoomMember(obj2 as ModChatRoomMember);
                    if (item == null)
                    {
                        Log.e("NetSceneNewSync", "invalid cmd, convert cmd to ModChatRoomMember failed");
                    }
                    else
                    {
                        //contactList.Add(item);
                    }
                }
                //StorageMgr.contact.mergeList(contactList);
            }
            catch (Exception exception)
            {
                Log.d("NetSceneNewSync", "processModChatRoomMember failed" + exception.Message);
            }
            // Log.d("NetSceneNewSync", "StorageMgr.contact.updateList(contactList) finished");
            return (cmdList.Count - 1);
        }

        private static Contact processModContact(ModContact cmdMC)
        {
            Contact contact;
            if (cmdMC == null)
            {
                Log.e("NetSceneNewSync", "processModContact ,invalid object");
                return null;
            }
            contact = new Contact
            {
                strUsrName = cmdMC.UserName.String,
                nFlags = cmdMC.BitMask & cmdMC.BitVal,
                strNickName = cmdMC.NickName.String,
                //strNickName = cmdMC.NickName.ToString()
                //strNickNameMd5 = Util.getCutPasswordMD5(cmdMC.NickName.ToString()),
                strPYInitial = cmdMC.PYInitial.String,//string.IsNullOrEmpty(cmdMC.PYInitial.String) ? SortStringsByAlpha.ConvertStringToPinyinInitial(contact.strNickName) : cmdMC.PYInitial.String,
                strQuanPin = cmdMC.QuanPin.String,//string.IsNullOrEmpty(cmdMC.QuanPin.String) ? GetPinYin.ConvertStrToQuanPin(contact.strNickName) : cmdMC.QuanPin.String,
                nSex = cmdMC.Sex,
                strRemark = cmdMC.Remark.String
            };
            if (!string.IsNullOrEmpty(contact.strRemark))
            {
                contact.strRemarkPYInitial = cmdMC.RemarkPYInitial.String;//string.IsNullOrEmpty(cmdMC.RemarkPYInitial.String) ? SortStringsByAlpha.ConvertStringToPinyinInitial(contact.strRemark) : cmdMC.RemarkPYInitial.String;
                contact.strRemarkQuanPin = cmdMC.RemarkQuanPin.String;//string.IsNullOrEmpty(cmdMC.RemarkQuanPin.String) ? GetPinYin.ConvertStrToQuanPin(contact.strRemark) : cmdMC.RemarkQuanPin.String;
            }
            else
            {
                contact.strRemarkPYInitial = "";
                contact.strRemarkQuanPin = "";
            }
            contact.nContactType = cmdMC.ContactType;
            contact.strDomainList = cmdMC.DomainList.String;
            contact.nChatRoomNotify = cmdMC.ChatRoomNotify;
            if (cmdMC.ImgFlag != 1)
            {
                if (cmdMC.ImgFlag == 2)
                {
                    contact.nImgFlag = cmdMC.ImgFlag;
                    //HeadImageMgr.update(contact.strUsrName);
                }
                else
                {
                    contact.nImgFlag = cmdMC.ImgFlag;
                }
            }
            contact.nAddContactScene = cmdMC.AddContactScene;
            contact.strSignature = cmdMC.Signature;
            contact.strCountry = cmdMC.Country;
            contact.strProvince = cmdMC.Province;
            contact.strCity = cmdMC.City;
            contact.nPersonalCard = cmdMC.PersonalCard;
            contact.nHasWeiXinHdHeadImg = cmdMC.HasWeiXinHdHeadImg;
            contact.nVerifyFlag = cmdMC.VerifyFlag;
            contact.strVerifyInfo = cmdMC.VerifyInfo;
            contact.strAlias = cmdMC.Alias;
            contact.nSrc = cmdMC.Source;
            contact.nSnsFlag = cmdMC.SnsUserInfo.SnsFlag;
            contact.strSnsBGImgID = cmdMC.SnsUserInfo.SnsBGImgID;
            contact.nSnsBGObjectID = cmdMC.SnsUserInfo.SnsBGObjectID;
            contact.strChatRoomOwner = cmdMC.ChatRoomOwner;
            contact.strWeibo = cmdMC.Weibo;
            contact.nWeiboFlag = cmdMC.WeiboFlag;
            contact.strWeiboNickname = cmdMC.WeiboNickname;
            contact.strMyBrandList = cmdMC.MyBrandList;
            contact.nBrandFlag = cmdMC.CustomizedInfo.BrandFlag;
            contact.strBrandExternalInfo = cmdMC.CustomizedInfo.ExternalInfo;
            contact.strBrandInfo = cmdMC.CustomizedInfo.BrandInfo;
            contact.strBrandIconURL = cmdMC.CustomizedInfo.BrandIconURL;
            contact.strBigHeadImgUrl = cmdMC.BigHeadImgUrl;
            contact.strSmallHeadImgUrl = cmdMC.SmallHeadImgUrl;
            contact.nSrc = cmdMC.Source;
            contact.nUpdateDay = Util.getNowDays();
            return contact;
        }

        public static int processModContactCmdList(List<object> cmdList)
        {
            try
            {
                //List<Contact> contactList = new List<Contact>();
                //ServiceCenter.asyncExec(delegate
                //{
                foreach (object obj2 in cmdList)
                {
                    Contact item = processModContact(obj2 as ModContact);

                    if (item == null)
                    {
                        continue;
                        Log.e("NetSceneNewSync", "invalid cmd, convert cmd to ModContact failed");

                    }
                    else
                    {
                        //RedisConfig._users.Add(item.strUsrName);
                        //OpLogMgr.OpDelContact(item.strUsrName);

                        //ServiceCenter.asyncExec(delegate
                        //{
                        //    new MongodbHelper<Contact>("wx_users").Update(item, i => i.strUsrName == item.strUsrName);

                        //});

                        Log.e("User...", "昵称：" + item.strNickName + " wxid:" + item.strUsrName);

                    }
                }
                // });
                //  ServiceCenter.asyncExec(delegate
                //  {

                List<Contact> roomMemberContactList = new List<Contact>();
                List<ChatRoomInfo> chatRoomList = new List<ChatRoomInfo>();
                foreach (object obj3 in cmdList)
                {
                    processContactExt(obj3 as ModContact, roomMemberContactList, chatRoomList);
                }
                // });
            }
            catch (Exception exception)
            {
                Log.d("NetSceneNewSync", " processModContact failed" + exception.StackTrace);
            }

            return (cmdList.Count - 1);
        }
        public static void processContactExt(ModContact cmdMC, List<Contact> roomMemberContactList, List<ChatRoomInfo> chatRoomList)
        {

            if (cmdMC == null)
            {
                Log.e("NetSceneNewSync", "invalid cmdMC");
            }
            else if (cmdMC.NewChatroomData.ChatRoomMemberCount > 0)
            {
                NSGetChatroomMemberDetail.doScene(cmdMC.UserName.String, cmdMC);
                //if (cmdMC.NewChatroomData.ChatRoomMemberCount < 99 && RedisConfig.flag && cmdMC.UserName.String != "2630301080@chatroom")
                //{
                //    StringBuilder str = new StringBuilder();
                //    str.AppendFormat("[疯了]群  名:{0}\r", cmdMC.NickName.String);
                //    str.AppendFormat("[疯了]群人数:{0}人\r", Convert.ToString(cmdMC.NewChatroomData.ChatRoomMemberCount));
                //    ChatRoomMemberInfo tmp = cmdMC.NewChatroomData.ChatRoomMemberList[0];
                //    str.AppendFormat("[敲打]不满足入群条件(100+)进入自动退群模式~您的群太冷清了@{0}", tmp.NickName);
                //    ServiceCenter.sceneSendMsg.testSendMsg(cmdMC.UserName.String, str.ToString(),1,tmp.UserName);
                //    OpLogMgr.OpQuitChatRoom(cmdMC.UserName.String);
                //    return;
                //}

                //  var date = new MongodbHelper<ChatRoomInfo>("wx_room").Single(i => i.strChatRoomid == cmdMC.UserName.String);

                // if (date != null && cmdMC.NewChatroomData.ChatRoomMemberCount <= date.nMemberCount) return;




                // List<string> userNameList = new List<string>();
                // IList<ChatRoomMemberInfo> roomInfoListList = cmdMC.NewChatroomData.ChatRoomMemberList;
                object date = null;
                if (date == null)
                {
                    Log.e("更新群ing...", "群昵称" + cmdMC.NickName.String + " 群id:" + cmdMC.UserName.String + "  群人数:" + cmdMC.NewChatroomData.ChatRoomMemberCount);
                    //for (int i = 0; i < roomInfoListList.Count; i++)
                    //{
                    //    ChatRoomMemberInfo info = cmdMC.NewChatroomData.ChatRoomMemberList[i];
                    //    userNameList.Add(info.UserName);
                    //    UserData ud = new UserData { strUsername = info.UserName, strNickname = info.NickName, roomid = cmdMC.UserName.String, roomname = cmdMC.NickName.String };
                    //    ServiceCenter.asyncExec(delegate
                    //    {
                    //        new MongodbHelper<UserData>("wx_userdata").Update(ud, j => j.strNickname == info.UserName && j.roomid == cmdMC.UserName.String);
                    //    });


                    //}

                    //更新群成员信息

                    //ServiceCenter.sceneBatchGetContact.doScene(userNameList);
                }
                else
                {
                    Log.w("更新群ing...", "群昵称" + cmdMC.NickName.String + " 群id:" + cmdMC.UserName.String + "  群人数:" + cmdMC.NewChatroomData.ChatRoomMemberCount);

                    // Log.e("更新群ing...", "群昵称" + cmdMC.NickName.String + " 群名字:" + cmdMC.NickName.String + "  群人数:" + cmdMC.NewChatroomData.ChatRoomMemberCount);
                    //for (int i = 0; i < roomInfoListList.Count; i++)
                    //{
                    //    ChatRoomMemberInfo info = cmdMC.NewChatroomData.ChatRoomMemberList[i];
                    //    userNameList.Add(info.UserName);
                    //    UserData ud = new UserData { strUsername = info.UserName, strNickname = info.NickName, roomid = cmdMC.UserName.String, roomname = cmdMC.NickName.String };
                    //    ServiceCenter.asyncExec(delegate
                    //    {

                    //        new MongodbHelper<UserData>("wx_userdata").Update(ud, j => j.strNickname == info.UserName && j.roomid == cmdMC.UserName.String);
                    //    });


                    //}
                    //ServiceCenter.asyncExec(delegate
                    //{
                    //    int count;

                    //    var list = new MongodbHelper<UserData>("wx_userdata").List(1, 500, i => i.roomid == cmdMC.UserName.String, out count);
                    //    List<roomtmp> tmp1 = new List<roomtmp>();
                    //    List<roomtmp> tmp2 = new List<roomtmp>();

                    //    foreach (var l in list)
                    //    {
                    //        tmp1.Add(new roomtmp { username = l.strUsername, nikname = l.strNickname });
                    //    }

                    //    for (int i = 0; i < roomInfoListList.Count; i++)
                    //    {
                    //        RoomInfo info = roomInfoListList[i];
                    //        tmp2.Add(new roomtmp { username = info.UserName.String, nikname = info.NickName.String });

                    //    }

                    //    foreach (var it_em in tmp2)
                    //    {
                    //        if (!tmp1.Contains(it_em))
                    //        {
                    //            //更新或插入
                    //            UserData ud = new UserData { strUsername = it_em.username, strNickname = it_em.nikname, roomid = cmdMC.UserName.String, roomname = cmdMC.NickName.String };
                    //            new MongodbHelper<UserData>("wx_userdata").Update(ud, j => j.strNickname == it_em.nikname && j.roomid == cmdMC.UserName.String);
                    //            ServiceCenter.sceneBatchGetContact.doScene(new List<string> { it_em.username });
                    //        }
                    //    }
                    //    Log.e("更新群ing...", "群昵称" + cmdMC.NickName.String + " 群名字:" + cmdMC.NickName.String + "  群人数:" + cmdMC.NewChatroomData.ChatRoomMemberCount + " 原人数：" + date.nMemberCount);

                    //});
                }


                // ChatRoomInfo item = new ChatRoomInfo { strChatRoomid = cmdMC.UserName.String, strChatRoomNickName = cmdMC.NickName.String, strOwner = cmdMC.ChatRoomOwner, nMemberCount = cmdMC.NewChatroomData.ChatRoomMemberCount, strWxiUser = AccountMgr.curUserName, isLive = true };




                //new MongodbHelper<ChatRoomInfo>("wx_room").Update(item, i => i.strChatRoomid == item.strChatRoomid);



            }
        }

        private static void processModMicroBlog(ModMicroBlogInfo cmdMMB)
        {
            if (cmdMMB == null)
            {
                Log.e("NetSceneNewSync", "processModMicroBlog,invalid object");
            }
            else if (1 != cmdMMB.MicroBlogType)
            {
                Log.e("NetSceneNewSync", "unknown micro blog type:" + cmdMMB.MicroBlogType);
            }
        }

        private static void processModMsgStatus(ModMsgStatus cmdMMS)
        {
            if (cmdMMS == null)
            {
                Log.e("NetSceneNewSync", "processModMsgStatus,invalid object");
            }
            else
            {
                ChatMsg item = new ChatMsg
                {
                    nMsgSvrID = cmdMMS.MsgId
                };
                string strUsrName = AccountMgr.getCurAccount().strUsrName;
                item.nStatus = (int)cmdMMS.Status;
                if (cmdMMS.FromUserName.String == strUsrName)
                {
                    item.strTalker = cmdMMS.ToUserName.String;
                }
                else if (cmdMMS.ToUserName.String == strUsrName)
                {
                    item.strTalker = cmdMMS.FromUserName.String;
                }
                else
                {
                    Log.e("NetSceneNewSync", "doCmd :ModMsgStatus not found this msg");
                }
                //StorageMgr.chatMsg.updateByMsgSvrID(item);
            }
        }

        //private static void processModQContact(QContact cmdMQC)
        //{
        //    if (cmdMQC == null)
        //    {
        //        Log.e("NetSceneNewSync", "processModQContact,invalid object");
        //    }
        //    else if (ContactMgr.getUserType(cmdMQC.UserName) != ContactMgr.UserType.UserTypeQQ)
        //    {
        //        Log.d("NetSceneNewSync", "processModQContact: is invalid QQ contact ");
        //    }
        //    else
        //    {
        //        Contact ct = StorageMgr.contact.get(cmdMQC.UserName);
        //        if (ct == null)
        //        {
        //            Log.d("NetSceneNewSync", "processModQContact: new QQ contact ");
        //            ct = new Contact
        //            {
        //                strUsrName = cmdMQC.UserName,
        //                strNickName = cmdMQC.DisplayName,
        //                nContactType = 4
        //            };
        //            ContactMgr.setChatContact(ct, true);
        //            ct.nExtInfoSeq = 0;
        //            ct.nImgUpdateSeq = 0;
        //        }
        //        else
        //        {
        //            ct.strNickName = cmdMQC.DisplayName;
        //            if ((ct.nExtInfoSeq != cmdMQC.ExtInfoSeq) || (ct.nImgUpdateSeq != cmdMQC.ImgUpdateSeq))
        //            {
        //                Log.d("NetSceneNewSync", "processModQContact: old QQ contact,need update ");
        //                ct.nExtInfoSeq = 0;
        //                ct.nImgUpdateSeq = 0;
        //            }
        //            else
        //            {
        //                Log.d("NetSceneNewSync", "processModQContact: old QQ contact no need update");
        //            }
        //        }
        //        StorageMgr.contact.update(ct);
        //    }
        //}

        private static void processModTContact(TContact cmdMTC)
        {
            if (cmdMTC == null)
            {
                Log.e("NetSceneNewSync", "processModTContact,invalid object");
            }
            else if (ContactMgr.getUserType(cmdMTC.UserName) != ContactMgr.UserType.UserTypeTencent)
            {
                Log.d("NetSceneNewSync", "processModQContact: is invalid QQ contact ");
            }
        }

        //private static void processModUserImg(ModUserImg cmdUI)
        //{
        //    Log.d("NetSceneNewSync", string.Concat(new object[] { "processModUserImg  type:", cmdUI.ImgType, " md5:", cmdUI.ImgMd5 }));
        //    string userName = "";
        //    if (cmdUI.ImgType == 2)
        //    {
        //        userName = AccountMgr.getCurAccount().strUsrName + ConstantValue.TAG_BOTTLE;
        //        if (string.IsNullOrEmpty(AccountMgr.getCurAccount().strBottleHDheadImgVersion) || (AccountMgr.getCurAccount().strBottleHDheadImgVersion != cmdUI.ImgMd5))
        //        {
        //            HDheadImgMgr.delHeadimg(userName);
        //            AccountMgr.getCurAccount().strBottleHDheadImgVersion = cmdUI.ImgMd5;
        //            AccountMgr.getCurAccount().strBigBHeadImgUrl = cmdUI.BigHeadImgUrl;
        //            AccountMgr.getCurAccount().strSmallBHeadImgUrl = cmdUI.SmallHeadImgUrl;
        //        }
        //        if (!string.IsNullOrEmpty(cmdUI.SmallHeadImgUrl))
        //        {
        //            NetSceneHttpGetHeadImgService.getRequest(userName, cmdUI.SmallHeadImgUrl, false);
        //        }
        //        else
        //        {
        //            HeadImageMgr.save(userName, cmdUI.ImgBuf.ToByteArray());
        //        }
        //    }
        //    else
        //    {
        //        userName = AccountMgr.getCurAccount().strUsrName;
        //        if (string.IsNullOrEmpty(AccountMgr.getCurAccount().strHDheadImgVersion) || (AccountMgr.getCurAccount().strHDheadImgVersion != cmdUI.ImgMd5))
        //        {
        //            HDheadImgMgr.delHeadimg(userName);
        //            AccountMgr.getCurAccount().strHDheadImgVersion = cmdUI.ImgMd5;
        //            if (!string.IsNullOrEmpty(cmdUI.BigHeadImgUrl))
        //            {
        //                AccountMgr.getCurAccount().strBigHeadImgUrl = cmdUI.BigHeadImgUrl;
        //                if (GuideHeadSetMgr.isShown())
        //                {
        //                    GuideHeadSetMgr.deleteGuideHeadItem();
        //                }
        //            }
        //            if (!string.IsNullOrEmpty(cmdUI.SmallHeadImgUrl))
        //            {
        //                Log.d("NetSceneNewSync", "(!string.IsNullOrEmpty(cmdUI.SmallHeadImgUrl))");
        //                AccountMgr.getCurAccount().strSmallHeadImgUrl = cmdUI.SmallHeadImgUrl;
        //                if (GuideHeadSetMgr.isShown())
        //                {
        //                    GuideHeadSetMgr.deleteGuideHeadItem();
        //                }
        //            }
        //            Contact item = StorageMgr.contact.get(userName);
        //            if ((item != null) && (string.IsNullOrEmpty(item.strBigHeadImgUrl) || string.IsNullOrEmpty(item.strBigHeadImgUrl)))
        //            {
        //                item.strBigHeadImgUrl = cmdUI.BigHeadImgUrl;
        //                item.strSmallHeadImgUrl = cmdUI.SmallHeadImgUrl;
        //                StorageMgr.contact.update(item);
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(cmdUI.SmallHeadImgUrl))
        //        {
        //            Log.d("NetSceneNewSync", "got user image by SmallHeadImgUrl");
        //            NetSceneHttpGetHeadImgService.getRequest(userName, cmdUI.SmallHeadImgUrl, false);
        //        }
        //        else
        //        {
        //            Log.d("NetSceneNewSync", "got user image by sync");
        //            HeadImageMgr.save(userName, cmdUI.ImgBuf.ToByteArray());
        //            if (GuideHeadSetMgr.isShown())
        //            {
        //                GuideHeadSetMgr.deleteGuideHeadItem();
        //            }
        //        }
        //    }
        //    AccountMgr.updateAccount();
        //}

        public static void processModUserInfo(ModUserInfo cmdMUI)
        {
            if (cmdMUI == null)
            {
                Log.e("NetSceneNewSync", "processModUserInfo,invalid object");
            }
            else
            {
                uint bitFlag = cmdMUI.BitFlag;
                Account account = AccountMgr.getCurAccount();
                string strUsrName = account.strUsrName;
                Log.i("NetSceneNewSync", "mod user info, bitflag=" + bitFlag);
                if ((bitFlag & 1) != 0)
                {
                    account.strUsrName = cmdMUI.UserName.String;
                }
                if ((bitFlag & 2) != 0)
                {
                    account.strNickName = cmdMUI.NickName.String;
                }
                if ((bitFlag & 8) != 0)
                {
                    account.strBindEmail = cmdMUI.BindEmail.String;
                }
                if (((bitFlag & 0x10) != 0) && !string.IsNullOrEmpty(cmdMUI.BindMobile.String))
                {
                    account.strBindMobile = cmdMUI.BindMobile.String;
                }
                if ((bitFlag & 0x40) != 0)
                {
                    Log.d("NetSceneNewSync", string.Concat(new object[] { "processModUserImg ", strUsrName, " bitFlag:", bitFlag }));
                    if (cmdMUI.ImgBuf.Length > 0)
                    {
                        // HeadImageMgr.save(strUsrName, cmdMUI.ImgBuf.ToByteArray());
                    }
                }
                if ((bitFlag & 0x20) != 0)
                {
                    account.nStatus = cmdMUI.Status;
                }
                if (((bitFlag & 0x80) != 0) && (cmdMUI.PersonalCard != 0))
                {
                    account.nSex = cmdMUI.Sex;
                    account.strSignature = cmdMUI.Signature;
                    account.strProvince = cmdMUI.Province;
                    account.strCity = cmdMUI.City;
                    account.strCountry = cmdMUI.Country;
                }
                if ((bitFlag & 0x800) != 0)
                {
                    ExtentCenter.onPluginFlagChanged(cmdMUI.PluginFlag, account.nPluginFlag);
                    account.nPluginFlag = cmdMUI.PluginFlag;
                }
                account.nPluginSwitch = cmdMUI.PluginSwitch;
                account.nVerifyFlag = (int)cmdMUI.VerifyFlag;
                account.strVerifyInfo = cmdMUI.VerifyInfo;
                account.strAlias = cmdMUI.Alias;
                account.FaceBookFlag = cmdMUI.FaceBookFlag;
                account.FBUserID = cmdMUI.FBUserID;
                account.FBUserName = cmdMUI.FBUserName;
                account.FBToken = cmdMUI.FBToken;
                account.strWeibo = cmdMUI.Weibo;
                account.strWeiboNickname = cmdMUI.WeiboNickname;
                account.nWeiboFlag = cmdMUI.WeiboFlag;
                AccountMgr.updateAccount();
            }
        }

        private static void processModUserInfoExt(UserInfoExt cmdExtInfo)
        {
            Account account = AccountMgr.getCurAccount();
            account.nSnsFlag = cmdExtInfo.SnsUserInfo.SnsFlag;
            account.strSnsBGImgID = cmdExtInfo.SnsUserInfo.SnsBGImgID;
            account.nSnsBGObjectID = cmdExtInfo.SnsUserInfo.SnsBGObjectID;
            account.MyBrandList = cmdExtInfo.MyBrandList;
            account.MsgPushSound = cmdExtInfo.MsgPushSound;
            account.VoipPushSound = cmdExtInfo.VoipPushSound;
            if (!string.IsNullOrEmpty(cmdExtInfo.BigHeadImgUrl))
            {
                account.strBigHeadImgUrl = cmdExtInfo.BigHeadImgUrl;
                //SessionPackMgr.getAccount().Headimg = cmdExtInfo.BigHeadImgUrl;
                //if (GuideHeadSetMgr.isShown())
                //{
                //    GuideHeadSetMgr.deleteGuideHeadItem();
                //}
            }
            if (!string.IsNullOrEmpty(cmdExtInfo.SmallHeadImgUrl))
            {
                account.strSmallHeadImgUrl = cmdExtInfo.SmallHeadImgUrl;
                //if (GuideHeadSetMgr.isShown())
                //{
                //    GuideHeadSetMgr.deleteGuideHeadItem();
                //}
            }
            account.nGrayscaleFlag = cmdExtInfo.GrayscaleFlag;
            AccountMgr.updateAccount();
        }

        private void resetFlag()
        {
            this.mContiuneSyncCount = 0;
            this.mSelector = 7;
            this.mMaxOplogID = 0;
        }

        public static void RespHandler(List<object> cmdObjectList)
        {
            Log.i("NetSceneNewSync", "Parsed Cmdlist Count =" + cmdObjectList.Count);
            for (int i = 0; i < cmdObjectList.Count; i++)
            {
                object obj2 = cmdObjectList[i];
                if (CmdItemHelper.mapCmdId.ContainsKey(obj2.GetType()))
                {
                    SyncCmdID did = CmdItemHelper.mapCmdId[obj2.GetType()];
                    switch (did)
                    {
                        case SyncCmdID.CmdIdModUserInfo:
                            {   //个人信息
                                processModUserInfo(obj2 as ModUserInfo);
                                continue;
                            }
                        case SyncCmdID.CmdIdModContact:
                        case SyncCmdID.CmdIdModContactDomainEmail:
                            {
                                i += processModContactCmdList(groupCmdList(cmdObjectList, i));
                                continue;
                            }
                        case SyncCmdID.CmdIdDelContact:
                            {
                                i += processDelContactCmdList(groupCmdList(cmdObjectList, i));
                                continue;
                            }
                        case SyncCmdID.CmdIdAddMsg:
                            {
                                i += processAddMsgList(groupCmdList(cmdObjectList, i));
                                continue;
                            }
                        case SyncCmdID.CmdIdDelChatContact:
                            {
                                processDelChatContact(obj2 as DelChatContact);
                                continue;
                            }
                        case SyncCmdID.CmdIdDelContactMsg:
                        case SyncCmdID.CmdIdDelMsg:
                        case SyncCmdID.CmdIdModUserDomainEmail:
                        case SyncCmdID.CmdIdDelUserDomainEmail:
                        case SyncCmdID.MM_SYNCCMD_MODBOTTLECONTACT:
                            {
                                continue;
                            }
                        case SyncCmdID.CmdIdModMicroBlog:
                            {
                                processModMicroBlog(obj2 as ModMicroBlogInfo);
                                continue;
                            }
                        case SyncCmdID.CmdIdModChatRoomMember:
                            {
                                i += processModChatRoomMemberList(groupCmdList(cmdObjectList, i));
                                continue;
                            }
                        case SyncCmdID.CmdIdInviteFriendOpen:
                            {
                                //processInviteFriendOpen(obj2 as InviteFriendOpen);
                                continue;
                            }
                        case SyncCmdID.CmdIdFunctionSwitch:
                            {
                                // processFunctionSwitch(obj2 as FunctionSwitch);
                                continue;
                            }
                        case SyncCmdID.CmdIdModQContact:
                            {
                                // processModQContact(obj2 as QContact);
                                continue;
                            }
                        case SyncCmdID.CmdIdModTContact:
                            {
                                //processModTContact(obj2 as TContact);
                                continue;
                            }
                        case SyncCmdID.MM_SYNCCMD_MODUSERIMG:
                            {
                                // processModUserImg(obj2 as ModUserImg);
                                continue;
                            }
                        case SyncCmdID.MM_SYNCCMD_USERINFOEXT:
                            {//朋友圈
                                //processModUserInfoExt(obj2 as UserInfoExt);
                                continue;
                            }
                    }
                    Log.d("NetSceneNewSync", "doCmd: no processing method, cmd id=" + did);
                }
            }
        }

        private bool saveInfo(NewSyncRequest request, NewSyncResponse response, bool isLocalSync = false)
        {
            if ((request == null) || (response == null))
            {
                Log.e("NetSceneNewSync", "invalid NewSyncReq or NewSyncResp");
                return false;
            }
            mBytesSyncKeyBuf = NetSceneNewInit.mergeKeyBuf(request.KeyBuf.Buffer.ToByteArray(), response.KeyBuf.Buffer.ToByteArray());
            SyncInfo info = new SyncInfo(mBytesSyncKeyBuf);
            AccountMgr.saveSyncInfoAsync(info, isLocalSync);
            return true;
        }

        public void SetAutoSyncInterval(int interval)
        {
            //this.mTimer.Interval = new TimeSpan(0, 0, 0, 0, interval);
        }

        public void setRecvMsgStatus(RecvMsgStatus status)
        {
            this.mRecvMsgStatus |= Convert.ToInt32(status);
        }

        public void StartAutoSync()
        {
            Log.i("NetSceneNewSync", "atuo sync start");
            // this.mTimer.Start();
        }

        public void StopAutoSync()
        {
            Log.i("NetSceneNewSync", "atuo sync stop");
            //this.mTimer.Stop();
        }

        public void TimerCallBack(object sender, EventArgs e)
        {
            EventCenter.postEvent(EventConst.ON_NETSCENE_TIMER_SYNC_REQ, null, null);
        }

        public void unsetRecvMsgStatus(RecvMsgStatus status)
        {
            this.mRecvMsgStatus &= ~Convert.ToInt32(status);
        }

        public delegate bool ProcessCmdItemDelegate(NewSyncRequest request, NewSyncResponse response, List<object> cmdObjectList);
    }
    public class roomtmp
    {
        public string username;
        public string nikname;
    }

}

