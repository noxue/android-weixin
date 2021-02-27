namespace MicroMsg.Manager
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class SnsInfoMgr
    {

        private static List<ulong> local_send_ids = new List<ulong>();
        private const ulong MIN_SENDING_OBJECT_ID = 18446744073709549615L;
        private static List<ulong> private_ids = new List<ulong>();
        private const string TAG = "SnsInfoMgr";

        static SnsInfoMgr()
        {
            //EventCenter.registerEventHandler(EventConst.ON_STORAGE_SNSINFO_ADD, new EventHandlerDelegate(SnsInfoMgr.track_private_snsObject));
            //EventCenter.registerEventHandler(EventConst.ON_STORAGE_SNSINFO_MODIFY, new EventHandlerDelegate(SnsInfoMgr.track_private_snsObject));
        }



        public static bool deleteComment(ulong objectID, int delCommentId, bool bChangeNow = true)
        {

            SnsAsyncMgr.delComment(objectID, delCommentId, bChangeNow ? 0 : 1);
            return true;
        }

        public static bool deleteSnsObject(ulong objectID, bool bChangeNow = true)
        {
            SnsAsyncMgr.delete(objectID, bChangeNow ? 0 : 1);
            return true;
        }


        public static void dumpSnsObjectList(IList<SnsObject> sojList)
        {
            using (IEnumerator<SnsObject> enumerator = sojList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    SnsObject current = enumerator.Current;
                }
            }
        }



        public static int getCheckSum(SnsObject obj)
        {
            return Util.CheckSum(obj.ToByteArray());
        }




        public static TimeLineObject getTimeLine(SnsInfo sns)
        {
            return sns.getTimeLine();
        }



        public static bool isSendingObjectID(ulong objID)
        {
            return (objID >= 18446744073709549615L);
        }



        //public static bool setPrivate(ulong objectID, bool isPrivate, bool bChangeNow = true)
        //{
        //    if (bChangeNow)
        //    {
        //        SnsInfo item = get(objectID);
        //        if (item == null)
        //        {
        //            return false;
        //        }
        //        item.setPrivacy(isPrivate);
        //       // StorageMgr.snsInfo.modify(item);
        //    }
        //    SnsAsyncMgr.setPrivacy(objectID, isPrivate, bChangeNow ? 0 : 1);
        //    return true;
        //}

        public static void setTimeLine(SnsInfo sns, TimeLineObject tl)
        {
            sns.setTimeLine(tl);
        }

        public static SnsComment toComment(SnsActionGroup act)
        {
            if (act == null)
            {
                return null;
            }
            return new SnsComment { strUserName = act.CurrentAction.FromUsername, strNickName = act.CurrentAction.FromNickname, nType = act.CurrentAction.Type, nSource = act.CurrentAction.Source, nCreateTime = act.CurrentAction.CreateTime, strContent = act.CurrentAction.Content, nCommentId = act.CurrentAction.CommentId, nReplyCommentId = act.CurrentAction.ReplyCommentId };
        }

        private static SnsComment toSnsComment(SnsCommentInfo ci)
        {
            return new SnsComment { strUserName = ci.Username, strNickName = ci.Nickname, nSource = ci.Source, nType = ci.Type, strContent = ci.Content, nCreateTime = ci.CreateTime, nCommentId = ci.CommentId, nReplyCommentId = ci.ReplyCommentId, strReplyUsername = ci.ReplyUsername };
        }

        private static SnsCommentList toSnsCommentList(IList<SnsCommentInfo> ciList)
        {
            SnsCommentList list = new SnsCommentList();
            foreach (SnsCommentInfo info in ciList)
            {
                list.list.Add(toSnsComment(info));
            }
            return list;
        }

        public static SnsInfo toSnsInfo(SnsObject obj)
        {
            SnsInfo sns = new SnsInfo
            {
                nCheckSum = getCheckSum(obj),
                strObjectID = SnsInfo.toStrID(obj.Id),
                strUserName = obj.Username,
                strNickName = obj.Nickname,
                nCreateTime = obj.CreateTime,
                bytesObjectDesc = obj.ObjectDesc.Buffer.ToByteArray(),
                nLikeFlag = (int)obj.LikeFlag,
                nLikeCount = (int)obj.LikeCount,
                likeList = toSnsCommentList(obj.LikeUserListList),
                nCommentCount = (int)obj.CommentCount,
                commentList = toSnsCommentList(obj.CommentUserListList),
                nWithUserCount = (int)obj.WithUserCount,
                withList = toSnsCommentList(obj.WithUserListList),
                nExtFlag = obj.ExtFlag,
                nStatus = 0,
                nBlackListCount = obj.BlackListCount,
                BlackList = toSnsUserNameList(obj.BlackListList),
                nGroupUserCount = obj.GroupListCount,
                GroupUserList = toSnsUserNameList(obj.GroupUserList),
                nIsRichText = (obj.IsNotRichText == 0) ? 1 : 0
            };
            TimeLineObject obj2 = getTimeLine(sns);
            if ((obj2 != null) && (obj2.content != null))
            {
                sns.nObjectStyle = obj2.content.nStyle;
            }
            return sns;
        }


        public static SnsUserNameList toSnsUserNameList(IList<SKBuiltinString_t> iList)
        {
            SnsUserNameList list = new SnsUserNameList();
            if ((iList != null) && (iList.Count >= 1))
            {
                foreach (SKBuiltinString_t _t in iList)
                {
                    list.list.Add(_t.String);
                }
            }
            return list;
        }


        private static void track_private_snsObject(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (local_send_ids.Count > 0)
            {
                List<SnsInfo> list = evtArgs.getListObject<SnsInfo>();
                if ((list != null) && (list.Count > 0))
                {
                    foreach (SnsInfo info in list)
                    {
                        if (local_send_ids.Contains(info.nObjectID))
                        {
                            TimeLineObject obj2 = getTimeLine(info);
                            if (obj2 != null)
                            {
                                if (obj2.nPrivate == 1)
                                {
                                    if (!private_ids.Contains(info.nObjectID))
                                    {
                                        private_ids.Add(info.nObjectID);
                                       // SnsPageMgr.notifyPrivateObjChanged();
                                    }
                                }
                                else if (private_ids.Contains(info.nObjectID))
                                {
                                    private_ids.Remove(info.nObjectID);
                                }
                            }
                        }
                    }
                }
            }
        }


    }
}

