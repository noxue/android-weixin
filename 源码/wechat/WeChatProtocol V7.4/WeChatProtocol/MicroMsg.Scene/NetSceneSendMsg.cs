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
    using System.Xml.Linq;
    using System.IO;

    //public class NetSceneSendMsg : NetSceneBaseEx<SendMsgRequest, SendMsgResponse, SendMsgRequest.Builder>
    public class NetSceneSendMsg : NetSceneBaseEx<SendMsgRequestNew, SendMsgResponseNew, SendMsgRequestNew.Builder>
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
        public static Dictionary<uint, ChatMsg> sendingHash = new Dictionary<uint, ChatMsg>();
        private const string TAG = "NetSceneSendMsg";

        public NetSceneSendMsg()
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

        public ChatMsg buildChatMsg(string talkerName, string msgContent, int msgType = 1, List<string> atUserList = null)
        {

            return new ChatMsg { strMsg = msgContent, strTalker = talkerName, nMsgType = msgType, nStatus = 0, nCreateTime = ((long)Util.getNowMilliseconds()) / 0x3e8L, nIsSender = 1, strClientMsgId = MD5Core.GetHashString(string.Concat(new object[] { this.id_random.Next(), msgContent, Util.getNowMilliseconds() })), msgSource = GetMsgSource(atUserList) };
        }

        private string GetMsgSource(List<string> atUserList)
        {
            if ((atUserList == null) || (atUserList.Count == 0))
            {
                return "";
            }
            XElement element = new XElement("msgsource");
            string str = atUserList[0];
            for (int i = 1; i < atUserList.Count; i++)
            {
                str = str + "," + atUserList[i];
            }
            XElement content = new XElement("atuserlist");
            content.SetValue(str);
            element.Add(content);
            StringWriter textWriter = new StringWriter();
            element.Save(textWriter);
            return textWriter.GetStringBuilder().ToString();
        }
        public static void checkInit()
        {
            if (mWatcherToCleanStatus == null)
            {
                mWatcherToCleanStatus = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneSendMsg.onHandlerToCleanStatus));
                EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, mWatcherToCleanStatus);
            }
        }

        private bool doScene(List<ChatMsg> chatMsgList, int emojiType = 1)
        {
            return new NetSceneSendMsg().doSceneEx(chatMsgList, emojiType);
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
            if (chatMsgList.Count > 500)
            {
                count = 500;
                int num2 = chatMsgList.Count - this.mListSendedLen;
                count = (num2 > 500) ? 500 : num2;
            }
            base.beginBuilder();
            for (int i = this.mListSendedLen; i < (this.mListSendedLen + count); i++)
            {
                ChatMsg local1 = chatMsgList[i];
                MicroMsgRequestNew.Builder builder = MicroMsgRequestNew.CreateBuilder();
                builder.SetCreateTime((uint)chatMsgList[i].nCreateTime);
                if (chatMsgList[i].strMsg == null)
                {
                    chatMsgList[i].strMsg = "";
                }
                builder.SetContent(chatMsgList[i].strMsg);
                if (chatMsgList[i].strClientMsgId == null)
                {
                    chatMsgList[i].strClientMsgId = "";
                }
                builder.SetClientMsgId((uint)chatMsgList[i].strClientMsgId.GetHashCode());
                // builder.SetFromUserName(Util.toSKString(AccountMgr.curUserName));
                builder.SetToUserName(Util.toSKString(chatMsgList[i].strTalker));
                builder.SetMsgSource(Util.nullAsNil(chatMsgList[i].msgSource));

                builder.SetType((uint)chatMsgList[i].nMsgType);
                //builder.SetEmojiFlag((uint) emojiType);
                //MicroMsgRequest item = builder.Build();
                MicroMsgRequestNew item = builder.Build();

                base.mBuilder.ListList.Add(item);
            }
            base.mBuilder.Count = (uint)base.mBuilder.ListList.Count;
            base.mSessionPack.mCmdID = 237;
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
            uint hashCode = (uint)chatMsgInfo.strClientMsgId.GetHashCode();
            if (sendingHash.ContainsKey(hashCode))
            {
                return false;
            }
            sendingHash.Add(hashCode, chatMsgInfo);
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
            if (this.isMsgValid(chatMsgInfo) && !sendingHash.ContainsKey((uint)chatMsgInfo.strClientMsgId.GetHashCode()))
            {
                sendingHash.Add((uint)chatMsgInfo.strClientMsgId.GetHashCode(), chatMsgInfo);
                chatMsgInfo.nStatus = 0;
                //if (StorageMgr.chatMsg.addMsg(chatMsgInfo))
                //{
                this.autoReSendMsg(chatMsgInfo, emojiType);
                //}
            }
        }

        private void doSendMsgFailProc(SendMsgRequestNew request, SendMsgResponseNew response)
        {
            List<ChatMsg> msgList = new List<ChatMsg>();
            if ((response != null) && (response.Count > 0))
            {
                for (int i = 0; i < response.Count; i++)
                {
                    uint clientMsgId = response.GetList(i).ClientMsgId;
                    if (clientMsgId != 0 && sendingHash.ContainsKey(clientMsgId))
                    {
                        ChatMsg item = sendingHash[clientMsgId];
                        sendingHash.Remove(clientMsgId);
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
                    uint str2 = request.GetList(j).ClientMsgId;
                    if (str2 != 0 && sendingHash.ContainsKey(str2))
                    {
                        ChatMsg msg2 = sendingHash[str2];
                        sendingHash.Remove(str2);
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

        protected override void onFailed(SendMsgRequestNew request, SendMsgResponseNew response)
        {
            if ((base.getLastError() == PackResult.PACK_TIMEOUT) && (mMsgRetryTimes < 1))
            {
                Log.e("NetSceneSendMsg", "doScene: send message timeout! retry doscene mMsgRetryTimes = " + mMsgRetryTimes);
                if ((request != null) && (request.Count > 0))
                {
                    for (int i = 0; i < request.Count; i++)
                    {
                        uint clientMsgId = request.GetList(i).ClientMsgId;
                        if (clientMsgId != 0 && sendingHash.ContainsKey(clientMsgId))
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

        protected override void onSuccess(SendMsgRequestNew request, SendMsgResponseNew response)
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
                    MicroMsgResponseNew list = response.GetList(i);
                    if (list != null)
                    {
                        if (list.Ret != 0)
                        {
                            Log.e("NetSceneSendMsg", "error send msg fail ret =  " + list.Ret);
                            uint clientMsgId = list.ClientMsgId;
                            if (clientMsgId != 0 && sendingHash.ContainsKey(clientMsgId))
                            {
                                ChatMsg item = sendingHash[clientMsgId];
                                if (item != null)
                                {
                                    sendingHash.Remove(clientMsgId);
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
                            uint str2 = list.ClientMsgId;
                            if (str2 != 0 && sendingHash.ContainsKey(str2))
                            {
                                ChatMsg msg2 = sendingHash[str2];
                                sendingHash.Remove(str2);
                                if (msg2 != null)
                                {
                                    msg2.nStatus = 2;
                                    msg2.nMsgSvrID = (int)list.MsgId;
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
                if (((this.mSendingList != null) && (this.mListSendedLen < this.mSendingList.Count)) && (this.mSendingList.Count > 500))
                {
                    this.doSceneEx(this.mSendingList, 1);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_SENDMSG_SUCCESS, null, null);
            }
        }

        public void sendMsgList(List<ChatMsg> msgList)
        {
            //foreach (ChatMsg msg in msgList)
            //{
            //    if (!sendingHash.ContainsKey(msg.strClientMsgId))
            //    {
            //        sendingHash.Add(msg.strClientMsgId, msg);
            //    }
            //}
            this.doScene(msgList, 1);
        }

        public void testSendMsg(string usrName, string strToSend, int msgType = 1, string atUser = "")
        {
            if (((usrName != null) && (strToSend != null)))
            {
                List<ChatMsg> chatMsgList = new List<ChatMsg>();
                Random random = new Random();
                ChatMsg item = this.buildChatMsg(usrName, strToSend, msgType, new List<string> { atUser });
                chatMsgList.Add(item);
                item.strClientMsgId = item.strClientMsgId + random.Next();
                this.doScene(chatMsgList, 1);
            }
        }
    }
}

