namespace MicroMsg.Plugin.Sns.Scene
{
    using micromsg;
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class NetSceneSnsSync : NetSceneBaseEx<SnsSyncRequest, SnsSyncResponse, SnsSyncRequest.Builder>
    {
        private static NetSceneSnsSync _mInstance;
        private bool hasNotify;
        public bool isEnable;
        private const int MAX_CONTINUE_SYNC_COUNT = 10;
        private int mContiuneSyncCount;
        private int mSyncStatus;
        private EventWatcher mWatcher;
        public static  List<string> snsLikeMap = new List<string>();
        private const string TAG = "NetSceneSnsSync";

        public NetSceneSnsSync()
        {
            this.mWatcher = new EventWatcher(this, null, new EventHandlerDelegate(NetSceneSnsSync.HandlerDoScene));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_NOTIFY_SYNC_REQ, this.mWatcher);
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, this.mWatcher);
        }

        private bool canContinue(SnsSyncRequest request, SnsSyncResponse resp)
        {
            if (MD5Core.GetHashString(request.KeyBuf.Buffer.ToByteArray()) == MD5Core.GetHashString(NetSceneNewSync.mBytesSyncKeyBuf))
            {
                //Log.e("NetSceneSnsSync", " request sync key is equal to response sync key");
                return false;
            }
            return ((resp.ContinueFlag & 0x100) != 0);
        }

        private bool continueDoScene()
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            base.mBuilder.Selector = 0x100;
            base.mBuilder.KeyBuf = Util.toSKBuffer(NetSceneNewSync.mBytesSyncKeyBuf);
            base.mSessionPack.mCmdID = 0x66;
            base.endBuilder();
            if (this.mContiuneSyncCount < 10)
            {
                this.mContiuneSyncCount++;
            }
            else
            {
                Log.d("NetSceneSnsSync", "mSyncCount exceed the maximum");
            }
            return true;
        }

        public bool doScene()
        {
            if (!this.isEnable)
            {
                Log.e("NetSceneSnsSync", "sns isEnable is false");
                return false;
            }
            if (this.mSyncStatus == 1)
            {
               // Log.e("NetSceneSnsSync", "is doing snsSync now,return this request");
                return true;
            }
            this.mSyncStatus = 1;
            this.hasNotify = false;
            this.continueDoScene();
            return true;
        }

        private static void HandlerDoScene(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (evtArgs != null)
            {
                Log.i("NetSceneSnsSync", "onHander NetSceneSnsSync mEventID = " + evtArgs.mEventID);
                if (evtArgs.mEventID == EventConst.ON_NETSCENE_SYNC)
                {
                    NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
                    if (mObject == null)
                    {
                        Log.e("NetSceneSnsSync", "NetSceneSyncResult == null");
                    }
                    else if ((mObject.syncStatus == SyncStatus.syncEnd) && (mObject.syncCount == 0))
                    {
                        //Log.d("NetSceneSnsSync", "onHander sync event result.syncStatus = " + mObject.syncStatus);
                        Instance.doScene();
                    }
                    else if ((mObject.syncStatus == SyncStatus.syncEnd))// && mObject.isAppActive
                    {
                        //Log.d("NetSceneSnsSync", "onHander sync event result.syncStatus = " + mObject.syncStatus);
                        Instance.doScene();
                    }
                }
                else
                {
                    int num = 0;
                    if (evtArgs.mObject != null)
                    {
                        num = (int)evtArgs.mObject;
                    }
                    if ((num & 0x100) != 0)
                    {
                        Instance.hasNotify = true;
                        Instance.doScene();
                    }
                }
            }
        }

        public static void Init()
        {
            if (_mInstance == null)
            {
                _mInstance = new NetSceneSnsSync();
                _mInstance.isEnable = true;
            }
        }

        public bool isSyncNow()
        {
            return (this.mSyncStatus == 1);
        }

        protected override void onFailed(SnsSyncRequest request, SnsSyncResponse response)
        {
            PackResult result = base.getLastError();
            this.resetFlag();
            Log.e("NetSceneSnsSync", "doScene: snsSync failed! packResult =" + result);
            this.mSyncStatus = 2;
        }

        protected override void onSuccess(SnsSyncRequest request, SnsSyncResponse response)
        {
            int ret = response.BaseResponse.Ret;
            if (ret != 0)
            {
                Log.e("NetSceneSnsSync", "doScene: snsSync failed!" + ret);
                this.mSyncStatus = 2;
            }
            else
            {
                Log.i("NetSceneSnsSync", string.Concat(new object[] { "doScene: snsSync success, continueFlag = ", response.ContinueFlag, " response.CmdList.Count = ", response.CmdList.Count }));
                List<object> cmdObjectList = NetSceneNewSync.getCmdObjectList(response.CmdList.ListList);
                this.ProcessCmdItem(request, response, cmdObjectList);
            }
        }

        private bool ProcessCmdItem(SnsSyncRequest request, SnsSyncResponse response, List<object> cmdObjectList)
        {
            RespHandler(cmdObjectList);
            if (!this.saveInfo(request, response))
            {
                Log.e("NetSceneSnsSync", "SaveUserInfo failed");
            }
            if (this.canContinue(request, response))
            {
                Log.i("NetSceneSnsSync", "doScene: snsSync NewSyncContinue");
                this.continueDoScene();
                return true;
            }
            Log.w("NetSceneSnsSync", "doScene: snsSync finished");
            this.mSyncStatus = 0;
            this.resetFlag();
            return true;
        }


        private static void processSnsActionGroup(SnsActionGroup cmdSnsActionGp)
        {
            if (cmdSnsActionGp == null)
            {
                Log.e("NetSceneSnsSync", "processSnsActionGroup,invalid object");
            }
            else if (!SnsAsyncMgr.isDeletedObjectID(cmdSnsActionGp.Id))
            {
                SnsMsg msg = new SnsMsg
                {
                    strObjectID = SnsInfo.toStrID(cmdSnsActionGp.Id),
                    strParentID = SnsInfo.toStrID(cmdSnsActionGp.ParentId),
                    strFromUsername = cmdSnsActionGp.CurrentAction.FromUsername,
                    strToUsername = cmdSnsActionGp.CurrentAction.ToUsername,
                    strFromNickname = cmdSnsActionGp.CurrentAction.FromNickname,
                    strToNickname = cmdSnsActionGp.CurrentAction.ToNickname,
                    nType = cmdSnsActionGp.CurrentAction.Type,
                    nSource = cmdSnsActionGp.CurrentAction.Source,
                    nCreateTime = cmdSnsActionGp.CurrentAction.CreateTime,
                    strContent = cmdSnsActionGp.CurrentAction.Content,
                    nCommentId = cmdSnsActionGp.CurrentAction.CommentId,
                    nReplyCommentId = cmdSnsActionGp.CurrentAction.ReplyCommentId
                };
                if (cmdSnsActionGp.ReferAction.CreateTime != 0)
                {
                    SnsMsgDetail detail = new SnsMsgDetail
                    {
                        strFromUsername = cmdSnsActionGp.ReferAction.FromUsername,
                        strToUsername = cmdSnsActionGp.ReferAction.ToUsername,
                        strFromNickname = cmdSnsActionGp.ReferAction.FromNickname,
                        strToNickname = cmdSnsActionGp.ReferAction.ToNickname,
                        nType = cmdSnsActionGp.ReferAction.Type,
                        nSource = cmdSnsActionGp.ReferAction.Source,
                        nCreateTime = cmdSnsActionGp.ReferAction.CreateTime,
                        strContent = cmdSnsActionGp.ReferAction.Content,
                        nCommentId = cmdSnsActionGp.ReferAction.CommentId,
                        nReplyCommentId = cmdSnsActionGp.ReferAction.ReplyCommentId
                    };
                    msg.refer = detail;
                }
                // StorageMgr.snsMsg.updateMsg(msg);
                // SnsInfoMgr.updateComment(cmdSnsActionGp);
            }
        }

        private static void processSnsObject(SnsObject cmdSnsObj)
        {
            if (cmdSnsObj == null)
            {
                Log.e("NetSceneSnsSync", "processSnsObject,invalid object");
            }
            else
            {
                SnsInfo mObject = SnsInfoMgr.toSnsInfo(cmdSnsObj);
                TimeLineObject timeLineObj = SnsInfoMgr.getTimeLine(mObject);
                timeLineObj.strContentDesc = "test";
                timeLineObj.lbs.strPoiName = "test";
                try
                {
                SnsInfoMgr.setTimeLine(mObject, timeLineObj);

                }
                catch (Exception e)
                {

                    Log.e("debug", e.StackTrace);
                }


               // new NetSceneSnsPost().doScene(mObject);
                if (snsLikeMap.Count != 0)
                {
                    if (snsLikeMap.Contains(mObject.strObjectID))
                    {
                        return;

                    }
                    else {
                        snsLikeMap.Add(mObject.strObjectID);
                        snsLikeMap.RemoveAt(0);
                        //自动点赞 自动评论
                        Log.w("NetSceneSnsSync", "收到朋友圈消息: 点赞用户:" + mObject.strNickName + "log mapcount：" + snsLikeMap.Count);


                        try
                        {
                            SnsCommentServiceCenter.snsCommentService.doSendComment(mObject, "", CommentType.MMSNS_COMMENT_LIKE, AddContactScene.MM_ADDSCENE_PF_CONTACT, null, -1);
                            SnsCommentServiceCenter.snsCommentService.doSendComment(mObject, RedisConfig.SnsText, CommentType.MMSNS_COMMENT_TEXT, AddContactScene.MM_ADDSCENE_PF_CONTACT, null, -1);

                        }
                        catch (Exception ex)
                        {

                            Log.w("debug",ex.StackTrace);
                        }
                       
                    }

                }
                else {
                    snsLikeMap.Add(mObject.strObjectID);

                   // SnsCommentServiceCenter.snsCommentService.doSendComment(mObject, "", CommentType.MMSNS_COMMENT_LIKE, AddContactScene.MM_ADDSCENE_PF_CONTACT, null, -1);

                    //Log.w("NetSceneSnsSync", "收到朋友圈消息: 点赞用户:" + mObject.strNickName + "log mapcount：" + snsLikeMap.Count);
                    
                   // SnsCommentServiceCenter.snsCommentService.doSendComment(mObject, RedisConfig.SnsText, CommentType.MMSNS_COMMENT_TEXT, AddContactScene.MM_ADDSCENE_PF_CONTACT, null, -1);



                    try
                    {
                        SnsCommentServiceCenter.snsCommentService.doSendComment(mObject, "", CommentType.MMSNS_COMMENT_LIKE, AddContactScene.MM_ADDSCENE_PF_CONTACT, null, -1);
                        SnsCommentServiceCenter.snsCommentService.doSendComment(mObject, RedisConfig.SnsText, CommentType.MMSNS_COMMENT_TEXT, AddContactScene.MM_ADDSCENE_PF_CONTACT, null, -1);

                    }
                    catch (Exception ex)
                    {

                        Log.w("debug", ex.StackTrace);
                    }
                }
                

            }
        }

        private void resetFlag()
        {
            this.mContiuneSyncCount = 0;
        }

        public static void RespHandler(List<object> cmdObjectList)
        {
            Log.w("NetSceneSnsSync", "parsed cmdlist count =" + cmdObjectList.Count);
            for (int i = 0; i < cmdObjectList.Count; i++)
            {
                object obj2 = cmdObjectList[i];
                if (CmdItemHelper.mapCmdId.ContainsKey(obj2.GetType()))
                {
                    SyncCmdID did = CmdItemHelper.mapCmdId[obj2.GetType()];
                    switch (did)
                    {
                        case SyncCmdID.MM_SNS_SYNCCMD_OBJECT:
                            {
                                processSnsObject(obj2 as SnsObject);
                                continue;
                            }
                        case SyncCmdID.MM_SNS_SYNCCMD_ACTION:
                            {
                                processSnsActionGroup(obj2 as SnsActionGroup);
                                continue;
                            }
                    }
                    Log.d("NetSceneSnsSync", "doCmd: no processing method, cmd id=" + did);
                }
            }
        }

        private bool saveInfo(SnsSyncRequest request, SnsSyncResponse response)
        {
            if ((request == null) || (response == null))
            {
                Log.e("NetSceneSnsSync", "invalid snsSyncReq or snsSyncResp");
                this.mSyncStatus = 2;
                return false;
            }
            NetSceneNewSync.mBytesSyncKeyBuf = NetSceneNewInit.mergeKeyBuf(request.KeyBuf.Buffer.ToByteArray(), response.KeyBuf.Buffer.ToByteArray());
            SyncInfo info = new SyncInfo(NetSceneNewSync.mBytesSyncKeyBuf);
            AccountMgr.saveSyncInfoAsync(info, false);
            return true;
        }

        public static bool enable
        {
            set
            {
                Instance.isEnable = value;
                if (value)
                {
                    if (Instance.hasNotify)
                    {
                        Instance.doScene();
                    }
                }
                else if (Instance.isSyncNow())
                {
                    Instance.cancel();
                    Instance.hasNotify = true;
                }
            }
        }

        public static NetSceneSnsSync Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    _mInstance = new NetSceneSnsSync();
                }
                return _mInstance;
            }
        }
    }
}

