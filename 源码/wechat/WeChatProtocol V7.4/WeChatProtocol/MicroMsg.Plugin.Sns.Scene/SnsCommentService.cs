namespace MicroMsg.Plugin.Sns.Scene
{//点赞类
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class SnsCommentService
    {
        public const int MAX_RETRY_TIMES = 20;
        private const int MAX_RUNNING = 100;
        //private static EventWatcher mWatcher;
        private const string TAG = "SnsCommentService";

        public SnsCommentService()
        {
            // mWatcher = new EventWatcher(this, null, new EventHandlerDelegate(this.HandlerDoScene));
            //EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SYNC, mWatcher);
        }

        public static bool delCommentFromXml(SnsCommentNeedSend comment)
        {
            return true;
        }

        public bool doSendComment(SnsInfo snsObj, string strContent, CommentType type, AddContactScene source, CommentArg replyInfo = null, int refCommentID = -1)
        {
            if (snsObj == null)
            {
                return false;
            }
            SnsComment item = new SnsComment
            {
                strContent = strContent,
                strNickName = AccountMgr.getCurAccount().strNickName,
                strUserName = AccountMgr.getCurAccount().strUsrName,
                nType = (uint)type,
                nCreateTime = (uint)Util.getNowSeconds(),
                nSource = (uint)source

            };
            if (replyInfo != null)
            {
                item.nReplyCommentId = replyInfo.commentID;
                item.strReplyUsername = replyInfo.strUserName;
            }

            if ((type == CommentType.MMSNS_COMMENT_LIKE) || (type == CommentType.MMSNS_COMMENT_STRANGER_LIKE))
            {
                snsObj.likeList.list.Add(item);
                snsObj.likeList.list = snsObj.likeList.list;
                snsObj.likeList = snsObj.likeList;
                snsObj.nLikeFlag = 1;
                snsObj.nLikeCount++;
            }
            else
            {

                snsObj.commentList.list.Add(item);
                snsObj.commentList.list = snsObj.commentList.list;
                snsObj.commentList = snsObj.commentList;
                snsObj.nCommentCount++;
            }
            //if (StorageMgr.snsInfo.getByObjectID(snsObj.strObjectID) != null)
            //{
            //    StorageMgr.snsInfo.updateByObjectID(snsObj);
            //}
            SnsCommentNeedSend comment = NetSceneSnsComment.creatSnsCommentNeedSend(snsObj, strContent, type, source, replyInfo, refCommentID);

            new NetSceneSnsComment().doScene(comment);

            return true;
        }



        private void HandlerDoScene(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
            if (mObject == null)
            {
                Log.e("SnsCommentService", "NetSceneSyncResult == null");
            }
            else if (mObject.syncStatus == SyncStatus.syncEnd)
            {
                sendFailedComment();
            }
        }

        public static void sendFailedComment()
        {
            //SnsCommentNeedSendMap data = ConfigMgr.get<SnsCommentNeedSendMap>();
            //if (((data != null) && (data.map != null)) && (data.map.Count > 0))
            //{
            //    Dictionary<string, SnsCommentNeedSend>.Enumerator enumerator = data.map.GetEnumerator();
            //    while (enumerator.MoveNext())
            //    {
            //        KeyValuePair<string, SnsCommentNeedSend> current = enumerator.Current;
            //        SnsCommentNeedSend comment = current.Value;
            //        if (comment != null)
            //        {
            //            if (comment.nRetryTimes >= 20)
            //            {
            //                data.map.Remove(comment.strClientID);
            //                if (data.map.Count > 0)
            //                {
            //                    continue;
            //                }
            //                break;
            //            }
            //            new NetSceneSnsComment().doScene(comment);
            //        }
            //    }
            //   // ConfigMgr.write<SnsCommentNeedSendMap>(data);
            //}
        }

    }
}

