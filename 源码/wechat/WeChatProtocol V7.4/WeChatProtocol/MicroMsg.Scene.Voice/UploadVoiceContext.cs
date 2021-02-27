namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;



    public class UploadVoiceContext : IContextBase, IRecorderContext
    {
        private MsgTrans _mMsgTrans;
        public const int FINISHED_ERROR = 2;
        public const int FINISHED_NULL = 0;
        public const int FINISHED_SHORTVOICE = 3;
        public const int FINISHED_SUCCESS = 1;
        private int MAX_SEND_BYTE_PER_PACK = 61400; //60kb 61400
        public ChatMsg mChatMsg;
        private int mFinishedType;
        private const int MIN_SAVE_BLOCK = 0xfa00;
        private const int MIN_SEND_BYTE_PER_PACK = 0x1f40;
        public bool mIsCancelled;
        private byte[] mOutputBuffer = new byte[0x10000];
        private object mOutputBufLock = new object();
        public NetSceneUploadVoice mScene;
        public int mSendingLength;
        private UploadVoiceProgress mVoiceProgress = new UploadVoiceProgress();
        public const int STATUS_COMPLETE = 5;
        public const int STATUS_ERROR = 4;
        public const int STATUS_INIT = 0;
        public const int STATUS_READY = 1;
        public const int STATUS_SENDING = 2;
        private const string TAG = "UploadVoiceContext";
        public int EncodeType;
        private UploadVoiceContext()
        {
        }

        public bool appendOutputData(byte[] buffer, int offset, int count)
        {
            lock (this.mOutputBufLock)
            {
                if (count > 0)
                {
                    if ((this.mOutputLength + count) > this.mOutputBuffer.Length)
                    {
                        // int length = this.mOutputBuffer.Length;
                        int length = count;

                        byte[] mOutputBuffer = this.mOutputBuffer;
                        //if (length >= 0x20000)
                        //{
                        //    Log.e("UploadVoiceContext", "recorder voice buffer overflow,stop recorded. ");
                        //    return false;
                        //}
                        this.mOutputBuffer = new byte[length * 2];
                       // Buffer.BlockCopy(mOutputBuffer, 0, this.mOutputBuffer, 0, length);
                    }
                    Buffer.BlockCopy(buffer, offset, this.mOutputBuffer, this.mOutputLength, count);
                    this.mOutputLength += count;
                }
                return true;
            }
        }

        public bool checkAddVoiceChatMsg()
        {
            if ((this.mChatMsg != null) || this.mIsCancelled)
            {
                return false;
            }
            this.mChatMsg = new ChatMsg();
            this.mChatMsg.strMsg = "";
            this.mChatMsg.strTalker = this._mMsgTrans.strToUserName;
            this.mChatMsg.nMsgType = 0x22;
            this.mChatMsg.nStatus = this.convertStatus(this.mStatus);
            this.mChatMsg.nCreateTime = this.mCreateTime;
            this.mChatMsg.nIsSender = 1;
            this.mChatMsg.strClientMsgId = this.mClientMsgId;
            this.updateChatMsgContent();
            //if (StorageMgr.chatMsg.addMsg(this.mChatMsg))
            //{
            //    this._mMsgTrans.nMsgLocalID = this.mChatMsg.nMsgLocalID;
            //    this.updateToVoiceTransMsg();
            //}
            //else
            //{
            //    this.mChatMsg = null;
            //    Log.e("UploadVoiceContext", "add chat-msg failed.");
            //}
            //Log.e("UploadVoiceContext", "add chat-msg failed. return true");
            return true;
        }

        public void checkRecorderSaver()
        {
            int count = this.mOutputLength - this.mTotalLength;
            if (!this.isInvalidShortVoice())
            {
                this.checkAddVoiceChatMsg();
                if ((count >= 0xfa00) && this.isEncodeAmrEnd())
                {
                    byte[] dst = new byte[count];
                    Buffer.BlockCopy(this.mOutputBuffer, this.mTotalLength, dst, 0, count);
                    // StorageMgr.msgVoice.saveVoiceBlockData(this._mMsgTrans, dst, this.mChatMsg);
                    Log.i("UploadVoiceContext", string.Concat(new object[] { "saved to file, offset = ", this.mTotalLength, ", size = ", count }));
                    this.mTotalLength += count;
                }
                if (this.mTotalLength == 0)
                {
                    this.mTotalLength += count;
                }
            }
        }

        private int convertStatus(int status)
        {
            MsgUIStatus processing = MsgUIStatus.Processing;
            if (status == 5)
            {
                processing = MsgUIStatus.Success;
            }
            else if (status == 4)
            {
                processing = MsgUIStatus.Fail;
            }
            return (int)processing;
        }

        public static UploadVoiceContext createByClientMsgID(string clientMsgID)
        {
            // MsgTrans msgTrans = StorageMgr.msgVoice.getByClientMsgID(clientMsgID);
            // if (msgTrans == null)
            //  {
            MsgTrans msgTrans = new MsgTrans
            {
                nTransType = 3,
                strClientMsgId = clientMsgID
            };
            return createByMsgTrans(msgTrans);
            //  }
            // return createByMsgTrans(msgTrans);
        }

        public static UploadVoiceContext createByMsgTrans(MsgTrans msgTrans)
        {
            return new UploadVoiceContext { _mMsgTrans = msgTrans };
        }

        public byte[] getBufferFromHead()
        {
            lock (this.mOutputBufLock)
            {
                int count = MAX_SEND_BYTE_PER_PACK;
                int num2 = this.mOutputLength - this.mNetOffset;
                if (count > num2)
                {
                    count = num2;
                }
                if ((count < 0x1f40) && !this.isEncodeAmrEnd())
                {
                    return null;
                }
                if (count <= 0)
                {
                    return null;
                }
                byte[] dst = new byte[count];
                Buffer.BlockCopy(this.mOutputBuffer, this.mNetOffset, dst, 0, count);
                this.mSendingLength = count;
                return dst;
            }
        }

        public bool isEncodeAmrEnd()
        {
            return (this.mEndFlag == 1);
        }


        public bool isFinished()
        {
            return (this.mFinishedType != 0);
        }

        public bool isInvalidShortVoice()
        {
            return (this.mVoiceTimeLength <= 0x3e8);
        }

        public bool isNeedData()
        {
            return !this.isFinished();
        }

        public bool isRecordEnd()
        {
            //return (this.mEndFlag == 1);
            return true; ;

        }

        public bool isRunning()
        {
            if (this.mStatus == 0)
            {
                return false;
            }
            return (this.mFinishedType == 0);
        }

        public bool isSaveCompleted()
        {
            return isSaveCompleted(this._mMsgTrans);
        }

        public static bool isSaveCompleted(MsgTrans msgTrans)
        {
            return ((msgTrans.nTotalDataLen == msgTrans.nRecordLength) && (msgTrans.nEndFlag == 1));
        }

        public bool isSendCompleted()
        {
            return isSendCompleted(this._mMsgTrans);
        }

        public static bool isSendCompleted(MsgTrans msgTrans)
        {
            return ((msgTrans.nTransDataLen == msgTrans.nRecordLength) && (msgTrans.nEndFlag == 1));
        }

        public bool loadFromMsgTrans()
        {
            //byte[] buffer = StorageMgr.msgVoice.loadVoiceFileData(this._mMsgTrans);
            //if (buffer == null)
            //{
            //    Log.e("UploadVoiceContext", "load from msgtrasn failed , no data found ");
            //    this.mStatus = 5;
            //    StorageMgr.msgVoice.updateTransByClientMsgId(this._mMsgTrans);
            //    return false;
            //}
            //this.mChatMsg = StorageMgr.chatMsg.getMsg(this._mMsgTrans.nMsgLocalID);
            //if (this.mChatMsg == null)
            //{
            //    Log.e("UploadVoiceContext", "failed to load chat msg by clientmsgid , ignored task. ");
            //    this.mStatus = 5;
            //    StorageMgr.msgVoice.updateTransByClientMsgId(this._mMsgTrans);
            //    return false;
            //}
            //Log.d("UploadVoiceContext", "load upvoice from msgtrans success. ");
            //this.mOutputBuffer = buffer;
            //this.mSendingLength = 0;
            //this.mStatus = 0;
            //this.updateToVoiceChatMsg();
            return true;
        }

        public static bool needCleanFromTrans(MsgTrans msgTrans, uint now)
        {
            return !needResumeFromTrans(msgTrans, now);
        }

        public static bool needResumeFromTrans(MsgTrans msgTrans, uint now)
        {
            if (msgTrans.nStatus == 5)
            {
                Log.d("UploadVoiceContext", "comleted task, ignored.");
                return false;
            }
            if (!isSaveCompleted(msgTrans))
            {
                Log.d("UploadVoiceContext", "not saved task, ignored.");
                return false;
            }
            return true;
        }

        public bool needToClean()
        {
            return (this.mFinishedType != 0);
        }

        public bool needToHandle()
        {
            return (this.mStatus == 0);
        }

        public bool onAppendOutputData(byte[] buffer, int offset, int count)
        {
            return this.appendOutputData(buffer, offset, count);
        }

        public void onFinished(int finishType)
        {
            this.mFinishedType = finishType;
            this.updateToVoiceTransMsg();
            this.updateToVoiceChatMsg();
        }

        public void onStartup(int creatTime, object args)
        {
            this.mCreateTime = creatTime;
        }

        public void onStop(byte[] voiceBuffer, int dataLength, int timeLength)
        {
            this.mEndFlag = 1;
            if (this.mScene != null)
            {
                this.mScene.dispatchVoiceHandler();
            }
            if (this.mIsCancelled)
            {
                Log.d("UploadVoiceContext", "on stop with cancelled..");
                if (this.mChatMsg != null)
                {
                    // StorageMgr.chatMsg.delMsg(this.mChatMsg);
                }
                if (this._mMsgTrans != null)
                {
                    // StorageMgr.msgVoice.del(this._mMsgTrans);
                }
            }
        }

        public void onVoiceTimeChanged(int timeLength)
        {
            this.mVoiceTimeLength = timeLength;
        }

        public void printfInfo()
        {
        }

        public static void printInfo(MsgTrans msgTrans)
        {
            Log.d("UploadVoiceContext", string.Concat(new object[] { "task info: savedlen =", msgTrans.nTotalDataLen, ", recordedlen=", msgTrans.nRecordLength, ", sentlen = ", msgTrans.nTransDataLen, ", voicelen = ", msgTrans.nDuration, ", endflag = ", msgTrans.nEndFlag }));
        }

        private void updateChatMsgContent()
        {
            this.mChatMsg.strMsg = string.Concat(new object[] { "<msg><voicemsg  endflag=\"", this.mEndFlag, "\" length=\"", this.mTotalLength, "\" voicelength=\"", this.mVoiceTimeLength, "\" clientmsgid=\"", this.mClientMsgId, "\" fromusername=\"", this.mUserName, "\" downcount=\"", this.mNetOffset, "\" /></msg>" });
        }

        public void updateToVoiceChatMsg()
        {
            if ((this.mChatMsg != null) && !this.mIsCancelled)
            {
                Log.d("UploadVoiceContext", "update  voice-chat msg....timelength = " + this.mVoiceTimeLength);
                this.mChatMsg.nStatus = this.convertStatus(this.mStatus);
                this.mChatMsg.nMsgSvrID = this.mMsgSrvId;
                this.updateChatMsgContent();
                //StorageMgr.chatMsg.modifyMsg(this.mChatMsg);
            }
        }

        private void updateToVoiceTransMsg()
        {
            if ((!this.isInvalidShortVoice() && (this._mMsgTrans != null)) && !this.mIsCancelled)
            {
                //StorageMgr.msgVoice.updateTransByClientMsgId(this._mMsgTrans);
            }
        }

        public string mClientMsgId
        {
            get
            {
                return this._mMsgTrans.strClientMsgId;
            }
        }

        public int mCreateTime
        {
            get
            {
                return (int)this._mMsgTrans.nCreateTime;
            }
            set
            {
                this._mMsgTrans.nCreateTime = (long)((ulong)value);
            }
        }

        public int mEndFlag
        {
            get
            {
                return this._mMsgTrans.nEndFlag;
            }
            set
            {
                this._mMsgTrans.nEndFlag = value;
                this.updateToVoiceTransMsg();
                this.updateToVoiceChatMsg();
            }
        }

        public int mMsgSrvId
        {
            get
            {
                return this._mMsgTrans.nMsgSvrID;
            }
            set
            {
                this._mMsgTrans.nMsgSvrID = value;
            }
        }

        public int mNetOffset
        {
            get
            {
                return this._mMsgTrans.nTransDataLen;
            }
            set
            {
                this._mMsgTrans.nTransDataLen = value;
            }
        }

        public int mOutputLength
        {
            get
            {
                return this._mMsgTrans.nRecordLength;
            }
            set
            {
                this._mMsgTrans.nRecordLength = value;
            }
        }

        public int mStatus
        {
            get
            {
                return this._mMsgTrans.nStatus;
            }
            set
            {
                this._mMsgTrans.nStatus = value;
            }
        }

        public int mTotalLength
        {
            get
            {
                return this._mMsgTrans.nTotalDataLen;
            }
            set
            {
                this._mMsgTrans.nTotalDataLen = value;
            }
        }

        public string mUserName
        {
            get
            {
                return this._mMsgTrans.strToUserName;
            }
            set
            {
                this._mMsgTrans.strToUserName = value;
            }
        }

        public int mVoiceTimeLength
        {
            get
            {
                return this._mMsgTrans.nDuration;
            }
            set
            {
                if (value > this._mMsgTrans.nDuration)
                {
                    this._mMsgTrans.nDuration = value;
                }
            }
        }
    }
}

