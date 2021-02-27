//namespace MicroMsg.Scene.Voice
//{
//    using MicroMsg.Common.Utils;
//    using MicroMsg.Manager;
//    using MicroMsg.Storage;
//    using System;
//    using System.Collections.Generic;

//    public class UploadVoiceContextLoader
//    {
//        private const int MIN_CLEAN_INTERVAL = 0x4e20;
//        private const int MIN_RESUM_INTERVAL = 0x4e20;
//        private static double mLastCleanTimeStamp;
//        private static double mLastLoadTimeStamp;
//        private const string TAG = "UploadVoiceContextLoader";

////        public static void checkCleanMsgTrans()
////        {
////            double num = Util.getNowMilliseconds();
////            if (num < (mLastCleanTimeStamp + 20000.0))
////            {
////                Log.d("UploadVoiceContextLoader", "cannot clean too fast, try later. ");
////            }
////            else
////            {
////                mLastCleanTimeStamp = num;
////                List<MsgTrans> list = StorageMgr.msgVoice.getCleanUploadVoiceList(20, (long) mLastCleanTimeStamp);
////                if ((list != null) && (list.Count != 0))
////                {
////                    foreach (MsgTrans trans in list)
////                    {
////                        if (UploadVoiceContextMgr.getInstance().findByClientMsgID(trans.strClientMsgId) == null)
////                        {
////                            Log.i("UploadVoiceContextLoader", "cleaning ... " + trans.strClientMsgId);
////                            StorageMgr.msgVoice.delByClientMsgID(trans.strClientMsgId);
////                        }
////                    }
////                }
////            }
////        }

//        //public static void checkResumeMsgTrans()
//        //{
//        //    double num = Util.getNowMilliseconds();
//        //    if (num < (mLastLoadTimeStamp + 20000.0))
//        //    {
//        //        Log.d("UploadVoiceContextLoader", "cannot resume too fast, try later.");
//        //    }
//        //    else
//        //    {
//        //        mLastLoadTimeStamp = num;
//        //        List<MsgTrans> list = StorageMgr.msgVoice.getResumeUploadVoiceList(5, (long) mLastLoadTimeStamp);
//        //        if ((list != null) && (list.Count != 0))
//        //        {
//        //            foreach (MsgTrans trans in list)
//        //            {
//        //                Log.i("UploadVoiceContextLoader", "resuming.... " + trans.strClientMsgId);
//        //                if (UploadVoiceContextMgr.getInstance().findByClientMsgID(trans.strClientMsgId) != null)
//        //                {
//        //                    Log.d("UploadVoiceContextLoader", "already running... " + trans.strClientMsgId);
//        //                }
//        //                else
//        //                {
//        //                    UploadVoiceContext context = UploadVoiceContext.createByMsgTrans(trans);
//        //                    if (!context.loadFromMsgTrans())
//        //                    {
//        //                        Log.e("UploadVoiceContextLoader", "Failed to load recorder file,  " + trans.strClientMsgId);
//        //                    }
//        //                    else
//        //                    {
//        //                        Log.d("UploadVoiceContextLoader", "load a recorder file success, put task to tail. ");
//        //                        UploadVoiceContextMgr.getInstance().putToTail(context);
//        //                    }
//        //                }
//        //            }
//        //        }
//        //    }
//        //}

////        public static void cleanSendingStatus()
////        {
////            List<ChatMsg> msgList = StorageMgr.chatMsg.searhCacheMsg(0x22, 0, 1);
////            if ((msgList != null) && (msgList.Count > 0))
////            {
////                foreach (ChatMsg msg in msgList)
////                {
////                    Log.i("UploadVoiceContextLoader", "clean status sending to fail , " + msg.strClientMsgId);
////                    msg.nStatus = 1;
////                }
////                StorageMgr.chatMsg.updateMsgList(msgList);
////            }
////        }

//        public static void resetIntervalLimit()
//        {
//            mLastCleanTimeStamp = 0.0;
//            mLastLoadTimeStamp = 0.0;
//        }

////        public static bool resumeByClientMsgID(string clientMsgID)
////        {
////            MsgTrans msgTrans = StorageMgr.msgVoice.getByClientMsgID(clientMsgID);
////            if (msgTrans == null)
////            {
////                Log.e("UploadVoiceContextLoader", "not found task ,clientmsgid =  " + clientMsgID);
////                return false;
////            }
////            UploadVoiceContext.printInfo(msgTrans);
////            if (!UploadVoiceContext.needResumeFromTrans(msgTrans, 0))
////            {
////                return false;
////            }
////            Log.i("UploadVoiceContextLoader", "resuming.... " + clientMsgID);
////            UploadVoiceContext context = UploadVoiceContext.createByMsgTrans(msgTrans);
////            if (!context.loadFromMsgTrans())
////            {
////                Log.e("UploadVoiceContextLoader", "Failed to load recorder file,  " + msgTrans.strClientMsgId);
////                return false;
////            }
////            Log.d("UploadVoiceContextLoader", "load a recorder file success, put task to head. ");
////            UploadVoiceContextMgr.getInstance().putToHead(context);
////            return true;
////        }
//   }
//}

