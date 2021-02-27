namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Network;
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class NetSceneSendMsgOld : NetSceneBaseEx<SendMsgRequest, SendMsgResponse, SendMsgRequest.Builder>
    {
        private Random id_random = new Random();
        public const int MAX_RETRY_COUNT = 1;
        private const int MAX_SEND_SIZE = 500;
        private int mListSendedLen;
        public const int MM_MSG_EMOJI_ART = 4;
        public const int MM_MSG_EMOJI_EMOJI = 2;
        public const int MM_MSG_EMOJI_QQ = 1;
        public static int mMsgRetryTimes = 0;
        private List<ChatMsg> mSendingList;
        private static EventWatcher mWatcherToCleanStatus = null;
        public static Dictionary<string, ChatMsg> sendingHash = new Dictionary<string, ChatMsg>();
        private const string TAG = "NetSceneSendMsg";

        public NetSceneSendMsgOld()
        {
            checkInit();
        }

        private void autoReSendMsg(ChatMsg msg, int emojiType)
        {
            List<ChatMsg> chatMsgList = new List<ChatMsg> {
                msg
            };
            this.doScene(chatMsgList, emojiType);
        }

        public ChatMsg buildChatMsg(string talkerName, string msgContent, int msgType = 1)
        {
           // ChatMsg msg;
            return new ChatMsg { strMsg = msgContent, strTalker = talkerName, nMsgType = msgType, nStatus = 0, nCreateTime = ((long)Util.getNowMilliseconds()) / 0x3e8L, nIsSender = 1, strClientMsgId = MD5Core.GetHashString(string.Concat(new object[] { talkerName, this.id_random.Next(), msgContent, Util.getNowMilliseconds() })) };
        }

        public static void checkInit()
        {
            if (mWatcherToCleanStatus == null)
            {
                mWatcherToCleanStatus = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneSendMsgOld.onHandlerToCleanStatus));
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, mWatcherToCleanStatus);
            }
        }

        private bool doScene(List<ChatMsg> chatMsgList, int emojiType = 1)
        {
            return new NetSceneSendMsgOld().doSceneEx(chatMsgList, emojiType);
        }

        private bool doSceneEx(List<ChatMsg> chatMsgList, int emojiType = 1)
        {
            if ((chatMsgList == null) || (chatMsgList.Count <= 0))
            {
                Log.e("NetSceneSendMsg", "sendMsg list is null");
                return false;
            }
            this.mSendingList = chatMsgList;
            int count = chatMsgList.Count;
            if (chatMsgList.Count > 10)
            {
                count = 10;
                int num2 = chatMsgList.Count - this.mListSendedLen;
                count = (num2 > 10) ? 10 : num2;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(2);
            for (int i = this.mListSendedLen; i < (this.mListSendedLen + count); i++)
            {
                ChatMsg local1 = chatMsgList[i];
                MicroMsgRequest.Builder builder = MicroMsgRequest.CreateBuilder();
                builder.SetCreateTime((uint) chatMsgList[i].nCreateTime);
                if (chatMsgList[i].strMsg == null)
                {
                    chatMsgList[i].strMsg = "";
                }
                builder.SetContent(chatMsgList[i].strMsg);
                if (chatMsgList[i].strClientMsgId == null)
                {
                    chatMsgList[i].strClientMsgId = "";
                }
                builder.SetClientMsgId(chatMsgList[i].strClientMsgId);
                builder.SetFromUserName(Util.toSKString(AccountMgr.curUserName));
                builder.SetToUserName(Util.toSKString(chatMsgList[i].strTalker));
                //builder.SetToUserName(Util.toSKString("ntsafe-hkk"));

                builder.SetType((uint) chatMsgList[i].nMsgType);
                builder.SetEmojiFlag((uint) emojiType);
                MicroMsgRequest item = builder.Build();
                base.mBuilder.ListList.Add(item);
            }
            base.mBuilder.Count = (uint) base.mBuilder.ListList.Count;
            base.mSessionPack.mCmdID = 2;
            base.endBuilder();
            this.mListSendedLen += count;
            return true;
        }

        public bool doSceneResendMsg(ChatMsg chatMsgInfo)
        {
            if (!this.isMsgValid(chatMsgInfo))
            {
                return false;
            }
            if (sendingHash.ContainsKey(chatMsgInfo.strClientMsgId))
            {
                return false;
            }
            sendingHash.Add(chatMsgInfo.strClientMsgId, chatMsgInfo);
            if (chatMsgInfo.nStatus != 0)
            {
                chatMsgInfo.nStatus = 0;
                //StorageMgr.chatMsg.updateMsg(chatMsgInfo);
            }
            List<ChatMsg> chatMsgList = new List<ChatMsg> {
                chatMsgInfo
            };
            return this.doScene(chatMsgList, 1);
        }

        public void doSendMsg(ChatMsg chatMsgInfo, int emojiType = 1)
        {
            if (this.isMsgValid(chatMsgInfo) && !sendingHash.ContainsKey(chatMsgInfo.strClientMsgId))
            {
                sendingHash.Add(chatMsgInfo.strClientMsgId, chatMsgInfo);
                chatMsgInfo.nStatus = 0;
                //if (StorageMgr.chatMsg.addMsg(chatMsgInfo))
                //{
                    this.autoReSendMsg(chatMsgInfo, emojiType);
                //}
            }
        }

        private void doSendMsgFailProc(SendMsgRequest request, SendMsgResponse response)
        {
            List<ChatMsg> msgList = new List<ChatMsg>();
            if ((response != null) && (response.Count > 0))
            {
                for (int i = 0; i < response.Count; i++)
                {
                    string clientMsgId = response.GetList(i).ClientMsgId;
                    if (!string.IsNullOrEmpty(clientMsgId) && sendingHash.ContainsKey(clientMsgId))
                    {
                        ChatMsg item = sendingHash[clientMsgId];
                        sendingHash.Remove(item.strClientMsgId);
                        if (item != null)
                        {
                            item.nStatus = 1;
                            msgList.Add(item);
                        }
                    }
                }
            }
            else if ((request != null) && (request.Count > 0))
            {
                for (int j = 0; j < request.Count; j++)
                {
                    string str2 = request.GetList(j).ClientMsgId;
                    if (!string.IsNullOrEmpty(str2) && sendingHash.ContainsKey(str2))
                    {
                        ChatMsg msg2 = sendingHash[str2];
                        sendingHash.Remove(msg2.strClientMsgId);
                        if (msg2 != null)
                        {
                            msg2.nStatus = 1;
                            msgList.Add(msg2);
                        }
                    }
                }
            }
            //StorageMgr.chatMsg.updateMsgList(msgList);
            msgList = null;
        }

        public string getContactCardContent(Contact con)
        {
            if (con == null)
            {
                return null;
            }
            return string.Concat(new object[] { 
                "<msg username=\"", con.strUsrName, "\" nickname=\"", con.strNickName.htmlString(), "\" fullpy=\"", con.strQuanPin, "\" shortpy=\"", con.strPYInitial, "\" imagestatus=\"", con.nImgFlag, "\" scene=\"17\" province=\"", con.strProvince, "\" city=\"", con.strCity, "\" sign=\"", con.strSignature.htmlString(), 
                "\" percard=\"", con.nPersonalCard, "\" sex=\"", con.nSex, "\" alias=\"", con.strAlias.htmlString(), "\"  certflag=\"", con.nVerifyFlag, "\"  certinfo=\"", con.strVerifyInfo.htmlString(), "\"  brandHomeUrl=\"", con.strBrandExternalInfo.htmlString(), "\"  brandIconUrl=\"", con.strBrandIconURL.htmlString(), "\" />"
             });
        }

        public string getMapMsgContent(double latitudeParam, double longitudeParam, int scale, string address)
        {
            return string.Concat(new object[] { "<msg><location x=\"", latitudeParam, "\" y=\"", longitudeParam, "\" scale=\"", scale, "\" label=\"", address.htmlString(), "\" maptype=\"roadmap\"/></msg>" });
        }

        private bool isMsgValid(ChatMsg chatMsgInfo)
        {
            return ((((chatMsgInfo != null) && !string.IsNullOrEmpty(chatMsgInfo.strTalker)) && (!string.IsNullOrEmpty(chatMsgInfo.strMsg) && !string.IsNullOrEmpty(chatMsgInfo.strClientMsgId))) && (((chatMsgInfo.nMsgType == 1) || (chatMsgInfo.nMsgType == 0x24)) || ((chatMsgInfo.nMsgType == 0x2a) || (chatMsgInfo.nMsgType == 0x30))));
        }

        protected override void onFailed(SendMsgRequest request, SendMsgResponse response)
        {
            if ((base.getLastError() == PackResult.PACK_TIMEOUT) && (mMsgRetryTimes < 1))
            {
                Log.e("NetSceneSendMsg", "doScene: send message timeout! retry doscene mMsgRetryTimes = " + mMsgRetryTimes);
                if ((request != null) && (request.Count > 0))
                {
                    for (int i = 0; i < request.Count; i++)
                    {
                        string clientMsgId = request.GetList(i).ClientMsgId;
                        if (!string.IsNullOrEmpty(clientMsgId) && sendingHash.ContainsKey(clientMsgId))
                        {
                            ChatMsg msg = sendingHash[clientMsgId];
                            if (msg != null)
                            {
                                List<ChatMsg> chatMsgList = new List<ChatMsg> {
                                    msg
                                };
                                this.doScene(chatMsgList, 1);
                            }
                        }
                    }
                }
                mMsgRetryTimes++;
            }
            else
            {
                Log.e("NetSceneSendMsg", "send msg fail because of net error");
                mMsgRetryTimes = 0;
                this.doSendMsgFailProc(request, null);
            }
        }

        private static void onHandlerToCleanStatus(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
            if (mObject != null)
            {
                SyncStatus syncStatus = mObject.syncStatus;
            }
            //if (((mObject != null) && (mObject.syncStatus == SyncStatus.syncEnd)) && (mObject.syncCount == 0))
            //{
            //    List<ChatMsg> msgList = StorageMgr.chatMsg.searhCacheMsg(1, 0, 1);
            //    List<ChatMsg> list2 = StorageMgr.chatMsg.searhCacheMsg(0x24, 0, 1);
            //    List<ChatMsg> list3 = StorageMgr.chatMsg.searhCacheMsg(0x30, 0, 1);
            //    if (msgList == null)
            //    {
            //        msgList = new List<ChatMsg>();
            //    }
            //    if ((list2 != null) && (list2.Count > 0))
            //    {
            //        foreach (ChatMsg msg in list2)
            //        {
            //            msgList.Add(msg);
            //        }
            //    }
            //    if ((list3 != null) && (list3.Count > 0))
            //    {
            //        foreach (ChatMsg msg2 in list3)
            //        {
            //            msgList.Add(msg2);
            //        }
            //    }
            //    if ((msgList != null) && (msgList.Count > 0))
            //    {
            //        ServiceCenter.sceneSendMsg.sendCachMsg(msgList);
            //        if (mWatcherToCleanStatus != null)
            //        {
            //            EventCenter.removeEventWatcher(EventConst.ON_NETSCENE_SYNC, mWatcherToCleanStatus);
            //            mWatcherToCleanStatus = null;
            //        }
            //    }
            //}
        }

        protected override void onSuccess(SendMsgRequest request, SendMsgResponse response)
        {
            mMsgRetryTimes = 0;
            if (response.BaseResponse.Ret != 0)
            {
                Log.e("NetSceneSendMsg", "send msg fail because of error ret = " + response.BaseResponse.Ret.ToString());
                this.doSendMsgFailProc(request, response);
            }
            else
            {
                List<ChatMsg> msgList = new List<ChatMsg>();
                for (int i = 0; i < response.Count; i++)
                {
                    MicroMsgResponse list = response.GetList(i);
                    if (list != null)
                    {
                        if (list.Ret != 0)
                        {
                            Log.e("NetSceneSendMsg", "error send msg fail ret =  " + list.Ret);
                            string clientMsgId = list.ClientMsgId;
                            if (!string.IsNullOrEmpty(clientMsgId) && sendingHash.ContainsKey(clientMsgId))
                            {
                                ChatMsg item = sendingHash[clientMsgId];
                                if (item != null)
                                {
                                    sendingHash.Remove(item.strClientMsgId);
                                    item.nStatus = 1;
                                   // StorageMgr.chatMsg.updateMsg(item);
                                    if (list.Ret == -49)
                                    {
                                        EventCenter.postEvent(EventConst.ON_NETSCENE_SENDMSG_FAIL, new MsgResultRet(item, RetConst.MM_ERR_NEED_QQPWD), null);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string str2 = list.ClientMsgId;
                            if (!string.IsNullOrEmpty(str2) && sendingHash.ContainsKey(str2))
                            {
                                ChatMsg msg2 = sendingHash[str2];
                                sendingHash.Remove(msg2.strClientMsgId);
                                if (msg2 != null)
                                {
                                    msg2.nStatus = 2;
                                    msg2.nMsgSvrID = (int) list.MsgId;
                                    if (list.CreateTime > 0)
                                    {
                                        msg2.nCreateTime = list.CreateTime;
                                    }
                                    msgList.Add(msg2);
                                }
                            }
                        }
                    }
                }
                //StorageMgr.chatMsg.updateMsgList(msgList);
                if (((this.mSendingList != null) && (this.mListSendedLen < this.mSendingList.Count)) && (this.mSendingList.Count > 10))
                {
                    this.doSceneEx(this.mSendingList, 1);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_SENDMSG_SUCCESS, null, null);
            }
        }

        public void sendMsgList(List<ChatMsg> msgList)
        {
            foreach (ChatMsg msg in msgList)
            {
                if (!sendingHash.ContainsKey(msg.strClientMsgId))
                {
                    sendingHash.Add(msg.strClientMsgId, msg);
                }
            }
            this.doScene(msgList, 1);
        }

        public  void testSendMsg(string usrName, string strToSend,int msgType=10000)
        {
            if (((usrName != null) && (strToSend != null)))
            {
                List<ChatMsg> chatMsgList = new List<ChatMsg>();
                Random random = new Random();
                ChatMsg item = this.buildChatMsg(usrName, strToSend, msgType);
                chatMsgList.Add(item);
                item.strClientMsgId = item.strClientMsgId + random.Next();
                this.doScene(chatMsgList, 1);
            }
        }

        public void SendOneMsg(string usrName, string strToSend, int msgType = 10000)
        {
            if (((usrName != null) && (strToSend != null)))
            {
                List<ChatMsg> chatMsgList = new List<ChatMsg>();
                Random random = new Random();
                ChatMsg item = this.buildChatMsg(usrName, strToSend, msgType);
                chatMsgList.Add(item);
                item.strClientMsgId = item.strClientMsgId + random.Next();
                this.doScene(chatMsgList, 1);
            }
        }
    }
}

