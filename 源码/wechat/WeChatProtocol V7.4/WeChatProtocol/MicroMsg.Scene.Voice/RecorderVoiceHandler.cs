//namespace MicroMsg.Scene.Voice
//{
//    using MicroMsg.Common.Audio.Amr;
//    using MicroMsg.Common.Timer;
//    using MicroMsg.Common.Utils;
//    //using Microsoft.Phone.Info;
//    using System;
//    using System.Collections.Generic;

//    public class RecorderVoiceHandler
//    {
//        public static AMREncodeFloat[] mAmrEncoderList = null;
//        private int mBlockSeq;
//        public bool mIsEnd;
//        private List<VoiceBlockInfo> mListBlock;
//        private int mOutputTimeLength;
//        public IRecorderContext mRecorderContext;
//        private TimerObject mTimerObject;
//        public static WorkThread[] mWorkerList = null;
//        public static int mWorkerNum = 4;
//        private const string TAG = "RecorderVoiceHandler";

//        public static void asyncExec(int seq, Action act)
//        {
//            if (mWorkerList == null)
//            {
//                if (isLowMemDevice())
//                {
//                    mWorkerNum = 2;
//                }
//                mWorkerList = new WorkThread[mWorkerNum];
//                mAmrEncoderList = new AMREncodeFloat[mWorkerNum];
//                for (int i = 0; i < mWorkerNum; i++)
//                {
//                    mWorkerList[i] = new WorkThread();
//                    mAmrEncoderList[i] = new AMREncodeFloat();
//                }
//            }
//            mWorkerList[seq % mWorkerNum].add_job(act);
//        }

//        private void encodeAmrFromBuffer(int seq, VoiceBlockInfo blockInfo)
//        {
//            asyncExec(seq, delegate {
//                int dstOffset = 0;
//                byte[] amr = new byte[0xa28];
//                byte[] dst = new byte[0x1000];
//                int num2 = blockInfo.mInPcmData.Length / 640;
//                AMREncodeFloat num3 = mAmrEncoderList[seq % mWorkerNum];
//                for (int j = 0; j < num2; j++)
//                {
//                    int amrlen = 0;
//                    num3.pcm16K2AmrFast(blockInfo.mInPcmData, j * 640, amr, ref amrlen);
//                    if ((amrlen > 0) && ((dstOffset + amrlen) < dst.Length))
//                    {
//                        Buffer.BlockCopy(amr, 0, dst, dstOffset, amrlen);
//                        dstOffset += amrlen;
//                    }
//                }
//                blockInfo.mOutAmrData = new byte[dstOffset];
//                Buffer.BlockCopy(dst, 0, blockInfo.mOutAmrData, 0, dstOffset);
//                blockInfo.mIsEncoded = true;
//            });
//        }

//        private void finishedVoiceHandler()
//        {
//            byte[] buffer = new byte[0x10000];
//            int dataLength = this.makeFullOutput(buffer);
//            this.mRecorderContext.onStop(buffer, dataLength, this.mOutputTimeLength);
//            if (this.mTimerObject != null)
//            {
//                this.mTimerObject.stop();
//                this.mTimerObject = null;
//            }
//            if (this.mListBlock != null)
//            {
//                this.mListBlock.Clear();
//                this.mListBlock = null;
//            }
//        }

//        private static bool isLowMemDevice()
//        {
//            //try
//            //{
//            //    long num = (long) DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit");
//            //    return (num < 0x5a00000L);
//            //}
//            //catch (ArgumentOutOfRangeException)
//            //{
//            //    return false;
//           // }
//            Log.d("RecorderVoiceHandler", "isLowMemDevice->return ture");
//            return true;
//        }

//        private int makeFullOutput(byte[] buffer)
//        {
//            int count = this.mListBlock.Count;
//            int dstOffset = 0;
//            for (int i = 0; i < count; i++)
//            {
//                if (this.mListBlock[i].mOutAmrData == null)
//                {
//                    return dstOffset;
//                }
//                if ((dstOffset + this.mListBlock[i].mOutAmrData.Length) < buffer.Length)
//                {
//                    Buffer.BlockCopy(this.mListBlock[i].mOutAmrData, 0, buffer, dstOffset, this.mListBlock[i].mOutAmrData.Length);
//                    dstOffset += this.mListBlock[i].mOutAmrData.Length;
//                }
//            }
//            return dstOffset;
//        }

//        private void onCheckVoiceEncodeDispatcher(object sender, EventArgs e)
//        {
//            int count = this.mListBlock.Count;
//            int num2 = 0;
//            for (int i = 0; i < count; i++)
//            {
//                if (this.mListBlock[i].mIsSent)
//                {
//                    num2++;
//                }
//                else
//                {
//                    if (!this.mListBlock[i].mIsEncoded)
//                    {
//                        break;
//                    }
//                    this.mRecorderContext.onAppendOutputData(this.mListBlock[i].mOutAmrData, 0, this.mListBlock[i].mOutAmrData.Length);
//                    this.mListBlock[i].mIsSent = true;
//                    num2++;
//                }
//            }
//            if (this.mIsEnd && (num2 == count))
//            {
//                this.finishedVoiceHandler();
//            }
//        }

//        public void pushPcmBlock(int timeLen, byte[] pcmbuffer, int count)
//        {
//            if ((this.mRecorderContext == null) || !this.mRecorderContext.isNeedData())
//            {
//                Log.e("RecorderVoiceHandler", "context is finished.");
//            }
//            else
//            {
//                this.mRecorderContext.onVoiceTimeChanged(timeLen);
//                VoiceBlockInfo item = new VoiceBlockInfo {
//                    mInPcmData = new byte[count]
//                };
//                Buffer.BlockCopy(pcmbuffer, 0, item.mInPcmData, 0, count);
//                if (this.mListBlock == null)
//                {
//                    this.mListBlock = new List<VoiceBlockInfo>();
//                }
//                this.mListBlock.Add(item);
//                if (this.mTimerObject == null)
//                {
//                    this.mTimerObject = TimerService.addTimer(1, new EventHandler(this.onCheckVoiceEncodeDispatcher));
//                    this.mTimerObject.start();
//                }
//                this.mBlockSeq++;
//                this.encodeAmrFromBuffer(this.mBlockSeq, item);
//            }
//        }

//        public void updataVoiceTime(int len)
//        {
//            this.mOutputTimeLength = len;
//            if (this.mRecorderContext != null)
//            {
//                this.mRecorderContext.onVoiceTimeChanged(this.mOutputTimeLength);
//            }
//        }

//        public class VoiceBlockInfo
//        {
//            public byte[] mInPcmData;
//            public bool mIsEncoded;
//            public bool mIsSent;
//            public byte[] mOutAmrData;
//        }
//    }
//}

