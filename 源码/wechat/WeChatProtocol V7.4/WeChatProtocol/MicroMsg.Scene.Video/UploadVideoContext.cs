namespace MicroMsg.Scene.Video
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;

    public class UploadVideoContext : IContextBase
    {
        //public RTCDNUploadResult mCdnRet;
        public ChatMsg mChatMsg;
        private int mLastRatio;
        public ChatMsg mOrigChatMsg;
        public ProgressInfo mProgressInfo = new ProgressInfo();
        //public INetSceneControl mSceneHandle;
        public int mSendingLength;
        private MemoryStream mThumbMemStream;
        private MemoryStream mVideoFileStream;
        public MsgTrans mVideoTrans;
        public double startTimestamp = Util.getNowMilliseconds();
        public const int STATUS_COMPLETE = 5;
        public const int STATUS_ERROR = 4;
        public const int STATUS_INIT = 0;
        public const int STATUS_READY = 1;
        public const int STATUS_SENDING = 3;
        public const int STATUS_THUMBSENDING = 2;
        private const string TAG = "UploadVideoContext";
        public static int UPLOAD_BLOCKSIZE = 0xf000;//500kb 0x7d000//0xf000

        private UploadVideoContext()
        {
        }

        public bool addVoiceChatMsg(bool isShortVideo)
        {
            if (this.mChatMsg != null)
            {
                return false;
            }
            this.mChatMsg = new ChatMsg();
            this.mChatMsg.strMsg = "";
            this.mChatMsg.strTalker = this.mVideoTrans.strToUserName;
            if (isShortVideo)
            {
                this.mChatMsg.nMsgType = 0x3e;
            }
            else
            {
                this.mChatMsg.nMsgType = 0x2b;
            }
            this.mChatMsg.nStatus = convertStatus(this.mStatus);
            this.mChatMsg.nCreateTime = (long) (Util.getNowMilliseconds() / 1000.0);
            this.mChatMsg.nIsSender = 1;
            this.mChatMsg.strClientMsgId = this.mVideoTrans.strClientMsgId;
            this.mChatMsg.strThumbnail = this.mVideoTrans.strThumbnail;
            this.mChatMsg.strPath = this.mVideoTrans.strImagePath;
            this.updateChatMsgContent();
            //StorageMgr.chatMsg.addMsg(this.mChatMsg);
            this.mVideoTrans.nMsgLocalID = this.mChatMsg.nMsgLocalID;
            this.updateToVideoTransMsg();
            return true;
        }

        public static int convertStatus(int status)
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
            return (int) processing;
        }

        public static UploadVideoContext createByClientMsgID(string clientMsgID)
        {
            MsgTrans trans = new MsgTrans {
                nTransType = 5,
                strClientMsgId = clientMsgID
            };
            return new UploadVideoContext { mVideoTrans = trans };
        }

        public static UploadVideoContext createByMsgTrans(MsgTrans msgTrans)
        {
            return new UploadVideoContext { mVideoTrans = msgTrans };
        }

        public void doCloseFile()
        {
            if (this.mThumbMemStream != null)
            {
                this.mThumbMemStream.Close();
                this.mThumbMemStream.Dispose();
                this.mThumbMemStream = null;
            }
            if (this.mVideoFileStream != null)
            {
                this.mVideoFileStream.Close();
                this.mVideoFileStream.Dispose();
                this.mVideoFileStream = null;
            }
        }

        public byte[] getRemainThumbToSend()
        {
            if (!this.initThumbMemStream())
            {
                return null;
            }
            if (this.mThumbNetOffset < 0)
            {
                return null;
            }
            if (this.mThumbTotalLength <= 0)
            {
                return null;
            }
            if (this.mThumbNetOffset >= this.mThumbTotalLength)
            {
                return null;
            }
            int count = UPLOAD_BLOCKSIZE;
            if ((this.mThumbTotalLength - this.mThumbNetOffset) < count)
            {
                count = this.mThumbTotalLength - this.mThumbNetOffset;
            }
            byte[] buffer = new byte[count];
            this.mThumbMemStream.Seek((long) this.mThumbNetOffset, SeekOrigin.Begin);
            if (this.mThumbMemStream.Read(buffer, 0, count) != count)
            {
                return null;
            }
            return buffer;
        }

        public byte[] getRemainVideoToSend()
        {
            if (!this.initVideoFileStream())
            {
                return null;
            }
            if (this.mNetOffset < 0)
            {
                return null;
            }
            if (this.mTotalLength <= 0)
            {
                return null;
            }
            if (this.mNetOffset >= this.mTotalLength)
            {
                return null;
            }
            int count = UPLOAD_BLOCKSIZE;
            if ((this.mTotalLength - this.mNetOffset) < count)
            {
                count = this.mTotalLength - this.mNetOffset;
            }
            byte[] buffer = new byte[count];
            this.mVideoFileStream.Seek((long) this.mNetOffset, SeekOrigin.Begin);
            if (this.mVideoFileStream.Read(buffer, 0, count) != count)
            {
                return null;
            }
            return buffer;
        }

        public bool initThumbMemStream()
        {
   
            if (this.mThumbMemStream != null)
            {
                return true;
            }
            try
            {

                using (FileStream fsRead = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\ReplyRes\\Video\\" + this.mVideoTrans.strThumbnail, FileMode.Open))

                {

                    this.mThumbTotalLength = (int)fsRead.Length;
                    this.mThumbMemStream = new MemoryStream();

                    fsRead.CopyTo(this.mThumbMemStream, this.mThumbTotalLength);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.e("UploadVideoContext", exception.Message);
            }
            return false;
        }

        public bool initVideoFileStream()
        {
            if (this.mVideoFileStream != null)
            {
                return true;
            }
            try
            {
                using (FileStream fsRead = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\ReplyRes\\Video\\" + this.mVideoTrans.strImagePath, FileMode.Open))
                {
                    this.mVideoFileStream = new MemoryStream();
                    fsRead.CopyTo(this.mVideoFileStream, (int)fsRead.Length);
                    if (this.mVideoFileStream == null)
                    {
                        return false;
                    }
                    this.mTotalLength = (int) this.mVideoFileStream.Length;
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.e("UploadVideoContext", exception.Message);
            }
            return false;
        }

        public bool isRunning()
        {
            if (((this.mStatus != 1) && (this.mStatus != 3)) && (this.mStatus != 2))
            {
                return false;
            }
            return true;
        }

        public bool isUploadCompleted()
        {
            return ((this.mThumbNetOffset == this.mThumbTotalLength) && (this.mNetOffset == this.mTotalLength));
        }

        //public bool loadFromMsgTrans()
        //{
        //    if (!this.initThumbMemStream())
        //    {
        //        Log.e("UploadVideoContext", "load from thumb failed , no data found ");
        //        this.mStatus = 5;
        //        this.updateContext();
        //        return false;
        //    }
        //    if (!this.initVideoFileStream())
        //    {
        //        Log.e("UploadVideoContext", "load from video failed , no data found ");
        //        this.mStatus = 5;
        //        this.updateContext();
        //        return false;
        //    }
        //    this.mChatMsg = StorageMgr.chatMsg.getMsg(this.mVideoTrans.nMsgLocalID);
        //    if (this.mChatMsg == null)
        //    {
        //        Log.e("UploadVideoContext", "failed to load chat msg by clientmsgid , ignored task. ");
        //        this.mStatus = 5;
        //        this.updateContext();
        //        return false;
        //    }
        //    Log.e("UploadVideoContext", "load video from msgtrans success. ");
        //    this.mStatus = 0;
        //    this.updateToVoiceChatMsg();
        //    return true;
        //}

        public bool needToClean()
        {
            return ((this.mStatus == 5) || (this.mStatus == 4));
        }

        public bool needToHandle()
        {
            return (this.mStatus == 0);
        }

        public void onFinished()
        {
            this.doCloseFile();
            this.updateToVoiceChatMsg();
            Log.d("UploadVideoContext", string.Concat(new object[] { "[*]upload  finished(status = ", this.mStatus, ") , run time = ", Util.getNowMilliseconds() - this.startTimestamp, "ms, rate = ", (int) (((double) (this.mVideoTrans.nTotalDataLen * 8)) / (Util.getNowMilliseconds() - this.startTimestamp)), "Kbps\n" }));
        }

        //public void preProcessSightFile(string thumbFile, string videoFile)
        //{
        //    try
        //    {
        //        using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
        //        {
        //            string hashString = MD5Core.GetHashString(thumbFile + Util.getNowMilliseconds());
        //            string destinationFileName = StorageIO.getSightChatPath() + hashString + ".jpg";
        //            file.CopyFile(thumbFile, destinationFileName);
        //            this.mVideoTrans.strThumbnail = destinationFileName;
        //            hashString = MD5Core.GetHashString(videoFile + Util.getNowMilliseconds());
        //            destinationFileName = StorageIO.getSightChatPath() + hashString + ".mp4";
        //            file.CopyFile(videoFile, destinationFileName);
        //            this.mVideoTrans.strImagePath = destinationFileName;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("UploadVideoContext", exception.Message);
        //    }
        //}

        //public void preProcessVideoFile(string thumbFile, string videoFile)
        //{
        //    try
        //    {
        //        using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
        //        {
        //            if (!file.DirectoryExists(StorageIO.getVideoPath()))
        //            {
        //                file.CreateDirectory(StorageIO.getVideoPath());
        //            }
        //            string hashString = MD5Core.GetHashString(thumbFile + Util.getNowMilliseconds());
        //            string destinationFileName = StorageIO.getVideoPath() + "/upvideothumb" + hashString + ".jpg";
        //            file.CopyFile(thumbFile, destinationFileName);
        //            this.mVideoTrans.strThumbnail = destinationFileName;
        //            hashString = MD5Core.GetHashString(videoFile + Util.getNowMilliseconds());
        //            destinationFileName = StorageIO.getVideoPath() + "/uploadvideo" + hashString + ".mp4";
        //            file.CopyFile(videoFile, destinationFileName);
        //            this.mVideoTrans.strImagePath = destinationFileName;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("UploadVideoContext", exception.Message);
        //    }
        //}

        private void updateChatMsgContent()
        {
            //if (this.mCdnRet == null)
           // {
                this.mChatMsg.strMsg = string.Concat(new object[] { "<msg><videomsg  playlength=\"", this.mVideoTrans.nDuration, "\" length=\"", this.mTotalLength, "\" clientmsgid=\"", this.mVideoTrans.strClientMsgId, "\" /></msg>" });
            //}
            //else
            //{
            //    string strUsrName = "";
            //    if (this.mChatMsg.IsSender())
            //    {
            //        strUsrName = AccountMgr.strUsrName;
            //    }
            //    else
            //    {
            //        strUsrName = this.mChatMsg.strTalker;
            //    }
            //    this.mChatMsg.strMsg = string.Concat(new object[] { 
            //        " <msg><videomsg clientmsgid=\"", this.mVideoTrans.strClientMsgId, "\" aeskey=\"", this.mCdnRet.aesKey, "\" cdnthumbaeskey=\"", this.mCdnRet.aesKey, "\" cdnvideourl=\"", this.mCdnRet.fileId, "\" cdnthumburl=\"", this.mCdnRet.fileId, "\" cdnthumblength=\"", this.mThumbTotalLength, "\" playlength=\"", this.mVideoTrans.nDuration, "\" length=\"", this.mTotalLength, 
            //        "\" fromusername=\"", strUsrName, "\" type=\"", this.mChatMsg.nMsgType, "\" /></msg>"
            //     });
           // }
        }

        public bool updateContext()
        {
            if (this.mStatus == 5)
            {
                Log.d("UploadVideoContext", "delete  upload video context, All completed.");
                //StorageMgr.msgVideo.delByClientMsgID(this.mVideoTrans.strClientMsgId);
            }
            else
            {
                this.updateToVideoTransMsg();
            }
            return true;
        }

        public void updateProgressInfo(int partLen)
        {
            if ((this.mTotalLength + this.mThumbTotalLength) != 0)
            {
                this.mProgressInfo.totalLength = this.mTotalLength + this.mThumbTotalLength;
                this.mProgressInfo.sentLength = (this.mNetOffset + this.mThumbNetOffset) + partLen;
                this.mProgressInfo.clientMsgId = this.mVideoTrans.strClientMsgId;
                this.mProgressInfo.status = convertStatus(this.mStatus);
                int num = (this.mProgressInfo.sentLength * 100) / this.mProgressInfo.totalLength;
                if ((num > (this.mLastRatio + 10)) || (num >= 100))
                {
                    this.mLastRatio = num;
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_UPLOADVIDEO_PROGRESS, this.mProgressInfo, null);
                }
            }
        }

        public void updateToVideoTransMsg()
        {
            Log.d("UploadVideoContext", "update to trans storage..");
            if (this.mVideoTrans.nMsgTransID == 0)
            {
                //StorageMgr.msgVideo.add(this.mVideoTrans);
            }
            else
            {
                //StorageMgr.msgVideo.update(this.mVideoTrans);
            }
        }

        public void updateToVoiceChatMsg()
        {
            this.mChatMsg.nStatus = convertStatus(this.mStatus);
            this.mChatMsg.nMsgSvrID = this.mVideoTrans.nMsgSvrID;
            this.updateChatMsgContent();
           // StorageMgr.chatMsg.modifyMsg(this.mChatMsg);
        }

        public int mNetOffset
        {
            get
            {
                return this.mVideoTrans.nTransDataLen;
            }
            set
            {
                this.mVideoTrans.nTransDataLen = value;
            }
        }

        public int mStatus
        {
            get
            {
                return this.mVideoTrans.nStatus;
            }
            set
            {
                this.mVideoTrans.nStatus = value;
            }
        }

        public int mThumbNetOffset
        {
            get
            {
                return this.mVideoTrans.nEndFlag;
            }
            set
            {
                this.mVideoTrans.nEndFlag = value;
            }
        }

        public int mThumbTotalLength
        {
            get
            {
                return this.mVideoTrans.nRecordLength;
            }
            set
            {
                this.mVideoTrans.nRecordLength = value;
            }
        }

        public int mTotalLength
        {
            get
            {
                return this.mVideoTrans.nTotalDataLen;
            }
            set
            {
                this.mVideoTrans.nTotalDataLen = value;
            }
        }

        public class ProgressInfo
        {
            public string clientMsgId;
            public int sentLength;
            public int status;
            public int totalLength;

            public void printInfo()
            {
                Log.d("UploadVideoContext", string.Concat(new object[] { " progress: totalLen = ", this.totalLength, ",sentLen =", this.sentLength, ", status = ", (MsgUIStatus) this.status, ", clientmsgid = ", this.clientMsgId }));
            }
        }
    }
}

