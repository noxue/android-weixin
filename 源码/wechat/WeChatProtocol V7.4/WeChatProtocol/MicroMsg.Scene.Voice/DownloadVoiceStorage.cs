namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;

    public class DownloadVoiceStorage
    {
        private const int MIN_CLEAN_INTERVAL = 0x927c0;
        private static double mLastCleanTimeStamp = Util.getNowMilliseconds();
        private const string TAG = "DownloadVoiceDBSync";

        public static void loadDownloadVoiceContextList()
        {
            Log.i("DownloadVoiceDBSync", "delCompleteDownloadVoice result = " + "StorageMgr.msgVoice.delPendingDownloadVoice()");
            //double num = Util.getNowMilliseconds();
            //if (num > (mLastCleanTimeStamp + 600000.0))
            //{
            //    mLastCleanTimeStamp = num;
            //    Log.i("DownloadVoiceDBSync", "delCompleteDownloadVoice result = " + "StorageMgr.msgVoice.delPendingDownloadVoice()");
            //}
            //List<MsgTrans> list = StorageMgr.msgVoice.getPendingDownLoadVoiceList();
            //if ((list == null) || (list.Count == 0))
            //{
            //    Log.i("DownloadVoiceDBSync", "There is no voiceinfo in DB that need to handle");
            //}
            //else
            //{
            //    foreach (MsgTrans trans in list)
            //    {
            //        DownloadVoiceContext context = new DownloadVoiceContext(trans.nMsgSvrID, ChatMsgHelper.getTalker(trans.strToUserName, trans.strFromUserName)) {
            //            mOffset = 0,
            //            mStatus = 0
            //        };
            //        context.Enqueue(trans);
            //        DownloadVoiceContextMgr.getInstance().putToTail(context);
            //    }
            //}
        }

        //public static bool updateDownloadVoiceContext(MsgTrans voiceinfo)
        //{
        //    return StorageMgr.msgVoice.updateTransBySvrID(voiceinfo);
        //}

        public static bool updateVoiceMsgStatus(string talker, int svrID, MsgUIStatus status)
        {
            //ChatMsg item = StorageMgr.chatMsg.getBySvrID(talker, svrID);
            //if (item != null)
            //{
            //    item.nStatus = (int) status;
            //    StorageMgr.chatMsg.modifyMsg(item);
            //}
            Log.i("DownloadVoiceDBSync", "updateVoiceMsgStatus =" + status.ToString());
            return true;
        }
    }
}


