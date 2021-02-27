namespace MicroMsg.Plugin.Sns.Scene
{
    using micromsg;
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class NetSceneSnsComment : NetSceneBaseEx<SnsCommentRequest, SnsCommentResponse, SnsCommentRequest.Builder>
    {


        private SnsCommentNeedSend mCommentContent;
        public static Dictionary<string, SnsCommentNeedSend> sendingMap;
        private const string TAG = "NetSceneSnsComment";

        public static SnsCommentNeedSend creatSnsCommentNeedSend(SnsInfo snsObj, string strContent, CommentType type, AddContactScene source, CommentArg replyInfo = null, int referCommetID = -1)
        {
            SnsCommentNeedSend send = new SnsCommentNeedSend {
                objectID = snsObj.nObjectID,
                parentID = 0L,
                referComment = null,
                strClientID = MD5Core.GetHashString(Util.getNowMilliseconds().ToString()),
                currentComment = new SnsComment()
            };
            send.currentComment.strContent = strContent;
            send.currentComment.strNickName = snsObj.strNickName;
            send.currentComment.strUserName = snsObj.strUserName;
            if (replyInfo != null)
            {
                send.currentComment.strNickName = replyInfo.strNickName;
                send.currentComment.strUserName = replyInfo.strUserName;
                send.currentComment.nReplyCommentId = replyInfo.commentID;
            }
            send.currentComment.nType = (uint) type;
            send.currentComment.nCreateTime = (uint) Util.getNowSeconds();
            send.currentComment.nSource = (uint) source;
            if (referCommetID >= 0)
            {
                foreach (SnsComment comment in snsObj.commentList.list)
                {
                    if (comment.nCommentId == referCommetID)
                    {
                        send.referComment = new SnsComment();
                        send.referComment.strContent = comment.strContent;
                        send.referComment.strNickName = comment.strNickName;
                        send.referComment.strUserName = comment.strUserName;
                        send.refFromUserName = snsObj.strUserName;
                        send.refFromNickName = snsObj.strNickName;
                        send.referComment.nType = comment.nType;
                        send.referComment.nCreateTime = comment.nCreateTime;
                        send.referComment.nSource = comment.nSource;
                        send.referComment.nReplyCommentId = comment.nReplyCommentId;
                    }
                }
            }
            return send;
        }

        public bool doScene(SnsCommentNeedSend comment)
        {
            if ((comment == null) || (comment.currentComment == null))
            {
                return false;
            }
            if ((sendingMap != null) && sendingMap.ContainsKey(comment.strClientID))
            {
                return false;
            }
            this.mCommentContent = comment;
            Account account = AccountMgr.getCurAccount();
            SnsActionGroup.Builder builder = new SnsActionGroup.Builder {
                Id = comment.objectID,
                ParentId = comment.parentID
            };
            builder.CurrentAction = new SnsAction.Builder { ToUsername = comment.currentComment.strUserName, ToNickname = comment.currentComment.strNickName, FromUsername = account.strUsrName, FromNickname = account.strNickName, Type = comment.currentComment.nType, Source = comment.currentComment.nSource, CreateTime = comment.currentComment.nCreateTime, Content = comment.currentComment.strContent, CommentId = comment.currentComment.nCommentId, ReplyCommentId = comment.currentComment.nReplyCommentId }.Build();
            if (comment.referComment != null)
            {
                builder.ReferAction = new SnsAction.Builder { ToUsername = comment.referComment.strUserName, ToNickname = comment.referComment.strNickName, FromUsername = account.strUsrName, FromNickname = account.strNickName, Type = comment.referComment.nType, Source = comment.referComment.nSource, CreateTime = comment.referComment.nCreateTime, Content = comment.referComment.strContent, CommentId = comment.referComment.nCommentId, ReplyCommentId = comment.referComment.nReplyCommentId }.Build();
            }
            if (sendingMap == null)
            {
                sendingMap = new Dictionary<string, SnsCommentNeedSend>();
            }
            sendingMap.Add(comment.strClientID, comment);
            return this.doSceneEx(builder.Build(), comment.strClientID);
        }

        public void doSceneEnd(string cliendID, bool isSuccess)
        {
            if (sendingMap != null)
            {
                sendingMap.Remove(cliendID);
            }
             //SnsCommentNeedSend send = null;
          //  SnsCommentNeedSendMap data = ConfigMgr.get<SnsCommentNeedSendMap>();
            //if (((data != null) && (data.map != null)) && (data.map.Count > 0))
            //{
            //    if (isSuccess)
            //    {
            //        data.map.Remove(cliendID);
            //    }
            //    else
            //    {
            //        SnsCommentNeedSend send = null;
            //        data.map.TryGetValue(cliendID, out send);
            //        if (send != null)
            //        {
            //            send.nRetryTimes++;
            //        }
            //    }
               // ConfigMgr.write<SnsCommentNeedSendMap>(data);
           // }
        }

        private bool doSceneEx(SnsActionGroup content, string clientID)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.Action = content;
            base.mBuilder.ClientId = clientID;
            base.mSessionPack.mCmdID = 100;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(SnsCommentRequest request, SnsCommentResponse response)
        {
            Log.e("NetSceneSnsComment", "send request failed");
            //this.doSceneEnd(request.ClientId, false);
        }

        protected override void onSuccess(SnsCommentRequest request, SnsCommentResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneSnsComment", "send request failed ret =" + ret.ToString());
                switch (ret)
                {
                    case RetConst.MMSNS_RET_SPAM:
                    case RetConst.MMSNS_RET_BAN:
                    case RetConst.MMSNS_RET_PRIVACY:
                    case RetConst.MMSNS_RET_COMMENT_HAVE_LIKE:
                    case RetConst.MMSNS_RET_COMMENT_NOT_ALLOW:
                    case RetConst.MMSNS_RET_CLIENTID_EXIST:
                    case RetConst.MMSNS_RET_COMMENT_PRIVACY:
                        //this.doSceneEnd(request.ClientId, true);
                       // EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_COMMENT_ERR, ret, null);
                        return;
                }
               // this.doSceneEnd(request.ClientId, false);
            }
            else
            {
                //SnsInfoMgr.update(response.SnsObject);
               // this.doSceneEnd(request.ClientId, true);
            }
        }
    }
}

