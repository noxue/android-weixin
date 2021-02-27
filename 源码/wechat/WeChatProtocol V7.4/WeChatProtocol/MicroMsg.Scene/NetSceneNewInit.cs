namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;

    using System.Text;
    using MicroMsg.Plugin.Sns.Scene;
    using System.Threading;
    using Common.Timer;

    public class NetSceneNewInit : NetSceneBaseEx<NewInitRequest, NewInitResponse, NewInitRequest.Builder>
    {
        public const int MAX_INIT_COUNT = 100;
        public const int MAX_RETRY_COUNT = 3;
        private static List<CmdItem> mCmdList = new List<CmdItem>();
        private int mInitCount;
        private static byte[] mInitCurrentSynckey = null;
        private static byte[] mInitMaxSynckey = null;
        private static int mInitStatus = 0;
        private uint mProgress;
        private int mScene = 3;
        private int mSelector;
        private EventWatcher mWatcher;
        private const string TAG = "NetSceneNewInit";

        public NetSceneNewInit()
        {
            this.mWatcher = new EventWatcher(this, null, new EventHandlerDelegate(NetSceneNewInit.onEventHandler));
            EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_AUTH_SUCCESS, this.mWatcher);
        }

        private bool canContinue(NewInitRequest request, NewInitResponse response)
        {

            if (MD5Core.GetHashString(request.CurrentSynckey.Buffer.ToByteArray()) == MD5Core.GetHashString(response.CurrentSynckey.Buffer.ToByteArray()))
            {
                Log.e("NetSceneNewInit", " request sync key is equal to response sync key");
                return false;
            }
            return ((response.ContinueFlag & this.mSelector) != 0L);
        }

        private bool continueDoScene()
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(this.mScene);
            base.mBuilder.UserName = AccountMgr.getCurAccount().strUsrName;
           // string aa = Util.byteToHexStr();
           // base.mBuilder.CurrentSynckey = Util.toSKBuffer(mInitCurrentSynckey);
            base.mBuilder.CurrentSynckey = Util.toSKBuffer(mInitCurrentSynckey);

            base.mBuilder.MaxSynckey = Util.toSKBuffer(mInitMaxSynckey);
            base.mSessionPack.mCmdID = 0x1b;
            base.endBuilder();
            if (this.mInitCount < 100)
            {
                this.mInitCount++;
            }
            else
            {
                Log.d("NetSceneNewInit", "mInitCount exceed the maximum");
            }
            this.mProgress = 10;
            return true;
        }

        public bool doScene()
        {
            Log.i("NetSceneNewInit", "newInit doscene begin");
            EventCenter.postEvent(EventConst.ON_NETSCENE_PROCESS_NEWINIT_BEGIN, null, null);
            mInitStatus = 2;
            this.mSelector = 7;
            if ((AccountMgr.getCurAccount().nNewUser != 0) && ((mInitCurrentSynckey == null) || (mInitCurrentSynckey.Length == 0)))
            {
                this.mScene = 7;
            }
            else if ((mInitCurrentSynckey == null) || (mInitCurrentSynckey.Length == 0))
            {
                this.mScene = 3;
            }
            else
            {
                this.mScene = 4;
            }
            return this.continueDoScene();
        }

        public bool isInitDone()
        {
            return !this.needInit();
        }

        public bool isInitializing()
        {
            if (mInitStatus == 2)
            {
                //Log.d("NetSceneNewInit", "mInitStatus =" + mInitStatus + ",do not need init");
                return true;
            }
            return false;
        }
        public static byte[] mergeKeyBuf(byte[] oldbuf, byte[] newbuf)
        {
            if ((oldbuf == null) || (oldbuf.Length == 0))
            {
                return newbuf;
            }
            if ((newbuf == null) || (newbuf.Length == 0))
            {
                return oldbuf;
            }
            SyncKey key = null;
            try
            {
                key = SyncKey.ParseFrom(oldbuf);
            }
            catch (Exception exception)
            {
                Log.e("NetSceneNewInit", "synckey oldbuf is invalid" + exception.Message);
                return newbuf;
            }
            SyncKey key2 = null;
            try
            {
                key2 = SyncKey.ParseFrom(newbuf);
            }
            catch (Exception exception2)
            {
                Log.e("NetSceneNewInit", "synckey newbuf is invalid" + exception2.Message);
                return oldbuf;
            }
            IList<KeyVal> keyList = key.KeyList;
            IList<KeyVal> list2 = key2.KeyList;
            Dictionary<uint, uint> dictionary = new Dictionary<uint, uint>();
            for (int i = 0; i < keyList.Count; i++)
            {
                if (!dictionary.ContainsKey(keyList[i].Key))
                {
                    dictionary.Add(keyList[i].Key, keyList[i].Val);
                }
            }
            for (int j = 0; j < list2.Count; j++)
            {
                if (dictionary.ContainsKey(list2[j].Key))
                {
                    dictionary[list2[j].Key] = list2[j].Val;
                }
                else
                {
                    dictionary.Add(list2[j].Key, list2[j].Val);
                }
            }
            SyncKey defaultInstance = SyncKey.DefaultInstance;
            SyncKey.Builder builder = new SyncKey.Builder().SetKeyNum((uint)dictionary.Count);
            foreach (KeyValuePair<uint, uint> pair in dictionary)
            {
                KeyVal.Builder introduced17 = KeyVal.CreateBuilder().SetKey(pair.Key);
                KeyVal item = introduced17.SetVal(pair.Value).Build();
                builder.KeyList.Add(item);
            }
            return builder.Build().ToByteArray();
        }


        public bool needInit()
        {
            if ((NetSceneNewSync.mBytesSyncKeyBuf != null) && (mInitStatus == 1))
            {
                // mInitStatus = ;
                return false;
            }
            return true;
        }

        private static void onEventHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            Log.d("NetSceneNewInit", "receive ON_NETSCENE_AUTH_SUCCESS");
        }

        protected override void onFailed(NewInitRequest request, NewInitResponse response)
        {
            Log.e("NetSceneNewInit", "newInit doscene NewInit failed! network err");
            EventCenter.postEvent(EventConst.ON_NETSCENE_PROCESS_NEWINIT_ERR, null, null);
            mInitStatus = 0;
            this.resetFlag();
        }

        public static void onLoginHandler()
        {
            AccountMgr.mInitRetryTimes = 0;
            if (AccountMgr.getCurAccount().nInitStatus != 1)
            {
                mInitStatus = 0;
            }
            else
            {
                mInitStatus = 1;
            }
            mInitCurrentSynckey = AccountMgr.getCurAccount().bytesCurSyncKey;
            mInitMaxSynckey = AccountMgr.getCurAccount().bytesMaxSyncKey;
        }

        protected override void onSuccess(NewInitRequest request, NewInitResponse response)
        {
            Log.i("NetSceneNewInit", string.Concat(new object[] { "newInit doscene onSuccess!initresp.selectBitmap:", response.SelectBitmap, " response.ContinueFlag=", response.ContinueFlag, " response.CmdListList.Count =", response.CmdListList.Count }));
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneNewInit", "deal With NewSyncResponse: NewInit failed! ret = " + ret);
                mInitStatus = 0;
                this.resetFlag();
                //EventCenter.postEvent(EventConst.ON_NETSCENE_PROCESS_NEWINIT_ERR, null, null);
            }
            else
            {
                mCmdList.AddRange(response.CmdListList);
                this.ProcessCmdItem(request, response);
                //List<object> cmdObjectList = NetSceneNewSync.getCmdObjectList(mCmdList);
                this.ProcessCmdItemByGroup(NetSceneNewSync.getCmdObjectList(mCmdList));
                mCmdList.Clear();
            }
        }

        private bool ProcessCmdItem(NewInitRequest request, NewInitResponse response)
        {
            if (!this.saveUserInfo(request, response))
            {
                Log.d("NetSceneNewInit", "SaveUserInfo failed");
            }
            if (this.canContinue(request, response))
            {
                Log.i("NetSceneNewInit", "newInit doscene continue");
                this.mProgress = response.Ratio;
                this.continueDoScene();
                return true;
            }
            Log.i("NetSceneNewInit", string.Concat(new object[] { "newInit doscene finished! init end, initresp.selectBitmap:", response.SelectBitmap, " response.ContinueFlag=", response.ContinueFlag }));
            this.mProgress = 70;
            //List<object> cmdObjectList = NetSceneNewSync.getCmdObjectList(mCmdList);
            //this.ProcessCmdItemByGroup(cmdObjectList);
            this.mProgress = 90;
            this.resetFlag();
            mInitStatus = 1;
            SyncInfo info = new SyncInfo(mInitCurrentSynckey, mInitMaxSynckey, NetSceneNewSync.mBytesSyncKeyBuf, mInitStatus);
            AccountMgr.saveSyncInfoAsync(info, false);
            Log.i("NetSceneNewInit", "newInit doscene save accinfo(include bytesSyncKeyBuf) success");
            if (this.needInit())
            {
                AccountMgr.mInitRetryTimes++;
                if (AccountMgr.mInitRetryTimes > 3)
                {
                    Log.e("NetSceneNewInit", "newInit doscene NewInit failed! mInitRetryTimes reach MAX_RETRY_COUNT=3");
                    mInitStatus = 0;
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_PROCESS_NEWINIT_ERR, null, null);
                    return false;
                }
                Log.d("NetSceneNewInit", "newInit doscene err,retry newinit. mInitRetryTimes = " + AccountMgr.mInitRetryTimes);
            }
            else
            {
                Log.d("NetSceneNewInit", "newInit doscene success");
                this.mProgress = 100;
                EventCenter.postEvent(EventConst.ON_NETSCENE_PROCESS_NEWINIT_FINISH, null, null);
                
            // TimerService.addTimer(300, new EventHandler(Util.PostOnlineRequest), 0, -1).start();
          

            }
            ServiceCenter.sceneNewSync.doScene(2, syncScene.MM_NEWSYNC_SCENE_AFTERINIT);
            return true;
        }

        private void ProcessCmdItemByGroup(List<object> cmdObjectList)
        {
            List<object> list = new List<object>();
            List<object> list2 = new List<object>();
            List<object> list3 = new List<object>();
            List<object> list4 = new List<object>();
            Log.i("NetSceneNewInit", "newInit doscene ,ProcessCmdItemByGroup");
            foreach (object obj2 in cmdObjectList)
            {
                if (obj2.GetType() == typeof(ModContact))
                {
                    list.Add(obj2);
                }
                else if (obj2.GetType() == typeof(AddMsg))
                {
                    list2.Add(obj2);
                }
                else if (obj2.GetType() == typeof(ModChatRoomMember))
                {
                    list3.Add(obj2);
                }
                else
                {
                    list4.Add(obj2);
                }
            }
            if (list.Count > 0)
                NetSceneNewSync.RespHandler(list);
            if (list2.Count > 0)
                NetSceneNewSync.RespHandler(list2);
            if (list3.Count > 0)
                NetSceneNewSync.RespHandler(list3);
            if (list4.Count > 0)
                NetSceneNewSync.RespHandler(list4);
        }

        private void resetFlag()
        {
            mCmdList.Clear();
            this.mInitCount = 0;
            this.mProgress = 0;
        }

        public bool saveUserInfo(NewInitRequest request, NewInitResponse response)
        {
            if ((request == null) || (response == null))
            {
                Log.d("NetSceneNewInit", "invalid NewInitReq or NewInitResp");
                return false;
            }
            mInitCurrentSynckey = response.CurrentSynckey.Buffer.ToByteArray();
            mInitMaxSynckey = response.MaxSynckey.Buffer.ToByteArray();
            NetSceneNewSync.mBytesSyncKeyBuf = mergeKeyBuf(NetSceneNewSync.mBytesSyncKeyBuf, response.CurrentSynckey.Buffer.ToByteArray());
            AccountMgr.getCurAccount().nNewUser = 0;
            AccountMgr.updateAccount();
            return true;
        }

        public uint progress
        {
            get
            {
                return this.mProgress;
            }
        }

        public delegate bool ProcessCmdItemDelegate(NewInitRequest request, NewInitResponse response, List<object> cmdObjectList);
    }
}

