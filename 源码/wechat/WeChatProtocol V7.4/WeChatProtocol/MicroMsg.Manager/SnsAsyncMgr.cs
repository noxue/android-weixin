namespace MicroMsg.Manager
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Plugin.Sns.Scene;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class SnsAsyncMgr
    {
        private static List<SnsOpLog> curOpList = new List<SnsOpLog>();
        private const int max_error_retry_times = 10;
        private static SnsAsyncQueue queue;
        private static NetSceneSnsObjectOp sceneOp;

        private static SnsOpLog addSnsOp(ulong snsObjectID, int opType, int updateSnsObj = 0, int delCommentID = 0)
        {
            SnsOpLog item = (from o in queue.OpList
                where ((o.nObjectID == snsObjectID) && (o.nOpType == opType)) && (o.nDelCommentId == delCommentID)
                select o).FirstOrDefault<SnsOpLog>();
            if (item == null)
            {
                item = new SnsOpLog(snsObjectID, opType, updateSnsObj, delCommentID);
                queue.OpList.Add(item);
                if ((opType == 1) && !queue.delList.Contains(snsObjectID))
                {
                    queue.delList.Add(snsObjectID);
                }
               // ConfigMgr.delayWrite<SnsAsyncQueue>(queue);
            }
            return item;
        }

        public static void ClearAll()
        {
            queue.delList.Clear();
           // ConfigMgr.delayWrite<SnsAsyncQueue>(queue);
        }

        public static void delComment(ulong snsObjectID, int commentID, int updateSnsObj = 0)
        {
            addSnsOp(snsObjectID, 4, updateSnsObj, commentID);
            trySendSnsOpLog();
        }

        public static void delete(ulong snsObjectID, int updateSnsObj = 0)
        {
            addSnsOp(snsObjectID, 1, updateSnsObj, 0);
            trySendSnsOpLog();
        }

        public static List<ulong> getDeleteIDS()
        {
            return queue.delList;
        }

        public static List<ulong> getDeletingIDS()
        {
            return (from op in queue.OpList
                where op.nOpType == 1
                select op.nObjectID).ToList<ulong>();
        }

        public static void init()
        {
            AccountMgr.registerLoginNotify(new onAccountLoginCallback(SnsAsyncMgr.onLogin));
            EventCenter.registerEventHandler(EventConst.ON_NETSCENE_SYNC, new EventHandlerDelegate(SnsAsyncMgr.onSyncEnd));
        }

        public static bool isDeletedObjectID(ulong ojbectID)
        {
            return queue.delList.Contains(ojbectID);
        }

        private static void onLogin()
        {
            if (sceneOp != null)
            {
                sceneOp.cancel();
            }
            sceneOp = new NetSceneSnsObjectOp(new NetSceneSnsObjectOpCallBack(SnsAsyncMgr.onSendSnsOpLogRsp));
            curOpList.Clear();
            queue = null;//ConfigMgr.read<SnsAsyncQueue>();
            if (queue == null)
            {
                queue = new SnsAsyncQueue();
            }
        }

        private static void onSendSnsOpLogRsp(IList<int> resultList)
        {
            Func<SnsOpLog, int, bool> predicate = null;
            if (resultList == null)
            {
                curOpList.Clear();
            }
            else if ((curOpList.Count <= 0) || ((resultList.Count > 0) && (resultList.Count != curOpList.Count)))
            {
                //DebugEx.debugBreak();
                curOpList.Clear();
            }
            else
            {
                if (resultList.Count > 0)
                {
                    if (predicate == null)
                    {
                        predicate = (op, index) => resultList[index] != 0;
                    }
                    List<SnsOpLog> second = curOpList.Where<SnsOpLog>(predicate).ToList<SnsOpLog>();
                    if (second.Count > 0)
                    {
                        foreach (SnsOpLog log in second)
                        {
                            log.nRetryTimes++;
                            if (log.nRetryTimes > 10)
                            {
                                queue.OpList.Remove(log);
                            }
                        }
                        List<SnsOpLog> opList = curOpList.Except<SnsOpLog>(second).ToList<SnsOpLog>();
                        updateSnsObject(opList);
                        queue.OpList = queue.OpList.Except<SnsOpLog>(opList).ToList<SnsOpLog>();
                       // ConfigMgr.delayWrite<SnsAsyncQueue>(queue);
                        curOpList.Clear();
                        return;
                    }
                }
                updateSnsObject(curOpList);
                queue.OpList = queue.OpList.Except<SnsOpLog>(curOpList).ToList<SnsOpLog>();
                //ConfigMgr.delayWrite<SnsAsyncQueue>(queue);
                curOpList.Clear();
                trySendSnsOpLog();
            }
        }

        private static void onSyncEnd(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            NetSceneSyncResult mObject = evtArgs.mObject as NetSceneSyncResult;
            if ((mObject != null) && ((mObject.syncStatus == SyncStatus.syncEnd) && mObject.isAppActive))
            {
                trySendSnsOpLog();
            }
        }

        public static void removeDeleteIDS(List<ulong> ids)
        {
            queue.delList = queue.delList.Except<ulong>(ids).ToList<ulong>();
            //ConfigMgr.delayWrite<SnsAsyncQueue>(queue);
        }

        public static void setLikeFlag(ulong snsObjectID, bool bLike, int updateSnsObj = 0)
        {
            //if (!bLike)
           // {
                addSnsOp(snsObjectID, 5, updateSnsObj, 0);
                trySendSnsOpLog();
           // }
        }

        public static void setPrivacy(ulong snsObjectID, bool bPrivacy, int updateSnsObj = 0)
        {
            addSnsOp(snsObjectID, bPrivacy ? 2 : 3, updateSnsObj, 0);
            trySendSnsOpLog();
        }

        private static void trySendSnsOpLog()
        {
            if ((curOpList.Count <= 0) && (queue.OpList.Count > 0))
            {
                curOpList.AddRange(queue.OpList);
                sceneOp.doScene(curOpList);
            }
        }

        private static void updateSnsObject(List<SnsOpLog> opList)
        {
            foreach (SnsOpLog log in opList)
            {
                if (log.nUpdateSnsObject == 0)
                {
                    continue;
                }
                if (log.nOpType == 1)
                {
                    //SnsInfoMgr.del(log.nObjectID);
                    //SnsMsgMgr.deleteMsg(log.nObjectID);
                    continue;
                }
                Log.e("updateSnsObject", "SnsInfo");
                //SnsInfo item = SnsInfoMgr.get(log.nObjectID);
                //if (item != null)
                //{
                //    switch (log.nOpType)
                //    {
                //        case 2:
                //            item.setPrivacy(true);
                //            break;

                //        case 3:
                //            item.setPrivacy(false);
                //            break;

                //        case 4:
                //            item.deleteSnsComment(log.nDelCommentId);
                //            break;

                //        case 5:
                //            item.setLikeFlag(false, null);
                //            break;
                //    }
                //    StorageMgr.snsInfo.modify(item);
                //}
            }
        }
    }
}

