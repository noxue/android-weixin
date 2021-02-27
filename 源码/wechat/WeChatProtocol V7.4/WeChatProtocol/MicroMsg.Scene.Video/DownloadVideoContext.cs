namespace MicroMsg.Scene.Video
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System.IO.IsolatedStorage;
   

    public class DownloadVideoContext : DownloadContextBase<DownloadVideoRequest, DownloadVideoResponse, DownloadVideoRequest.Builder>
    {
        public string mCdnAesKey;
        public string mCdnThumbAesKey;
        public int mCdnThumbHeight;
        public int mCdnThumbLength;
        public string mCdnThumbUrl;
        public int mCdnThumbWidth;
        public string mCdnVideoUrl;
        public ChatMsg mChatMsg;
        public bool mIsThumbMode;
        public ProgressInfo mProgressInfo = new ProgressInfo();
        public int mStatus;
        public MsgTrans mVideoInfo;
        public double startTimestamp = Util.getNowMilliseconds();
        public const int STATUS_COMPLETE = 5;
        public const int STATUS_ERROR = 4;
        public const int STATUS_INIT = 0;
        public const int STATUS_LOADING = 2;
        public const int STATUS_READY = 1;
        private const string TAG = "DownloadVideoContext";

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

        protected override DownNetSceneBaseEx<DownloadVideoRequest, DownloadVideoResponse, DownloadVideoRequest.Builder> createDownSceneInstance()
        {
            //if (!string.IsNullOrEmpty(this.mCdnAesKey))
            //{
            //    return new NetSceneDownloadCdnVideo();
            //}
            return new NetSceneDownloadVideo();
        }

        public void doCancelAll()
        {
            this.updateContext();
            this.updateChatMsg();
            base.closeAllScene();
        }

        public bool isDownloadCompleted()
        {
            return ((this.mVideoInfo.nTransDataLen == this.mVideoInfo.nTotalDataLen) && (this.mVideoInfo.nTotalDataLen != 0));
        }

        public override bool isRunning()
        {
            if ((this.mStatus != 1) && (this.mStatus != 2))
            {
                return false;
            }
            return true;
        }

        public override bool needToClean()
        {
            return ((this.mStatus == 5) || (this.mStatus == 4));
        }

        public override bool needToHandle()
        {
            return (this.mStatus == 0);
        }

        public void onFinishedEx()
        {
            this.updateProgressInfo(0);
            this.updateContext();
            this.updateChatMsg();
            base.closeAllScene();
        }

        protected override string onPrepareDataFilePath(IsolatedStorageFile currentIsolatedStorage)
        {
            if (this.mIsThumbMode)
            {
                return string.Concat(new object[] { StorageIO.getThumbnailPath(), "/", this.mVideoInfo.nMsgSvrID, ".jpg" });
            }
            if (!currentIsolatedStorage.DirectoryExists(StorageIO.getVideoPath()))
            {
                currentIsolatedStorage.CreateDirectory(StorageIO.getVideoPath());
            }
            return string.Concat(new object[] { StorageIO.getVideoPath(), "/downvideo", this.mVideoInfo.nMsgSvrID, ".mp4" });
        }

        public void startScene()
        {
            this.mStatus = 1;
            if (this.mVideoInfo.nTransDataLen >= this.mVideoInfo.nTotalDataLen)
            {
                this.mStatus = 5;
                this.onFinishedEx();
            }
            else
            {
                bool flag = false;
                if (!string.IsNullOrEmpty(this.mCdnAesKey))
                {
                    flag = base.startSceneWithRange(this.mVideoInfo.nTransDataLen, this.mVideoInfo.nTotalDataLen, true);
                }
                else
                {
                    flag = base.startSceneWithRange(this.mVideoInfo.nTransDataLen, this.mVideoInfo.nTotalDataLen, false);
                }
                if (!flag)
                {
                    this.mStatus = 4;
                    this.onFinishedEx();
                }
                else
                {
                    this.mStatus = 2;
                }
            }
        }

        public void updateChatMsg()
        {
            if (this.mChatMsg != null)
            {
                if (this.mIsThumbMode)
                {
                    this.mChatMsg.nStatus = 3;
                    if (this.mStatus == 5)
                    {
                        this.mChatMsg.strThumbnail = this.mVideoInfo.strImagePath;
                    }
                    //StorageMgr.chatMsg.modifyMsg(this.mChatMsg);
                }
                else
                {
                    this.mChatMsg.strPath = this.mVideoInfo.strImagePath;
                    this.mChatMsg.nStatus = convertStatus(this.mStatus);
                    //StorageMgr.chatMsg.modifyMsg(this.mChatMsg);
                    Log.d("DownloadVideoContext", "update video chatmsg ,status = " + ((MsgUIStatus) this.mChatMsg.nStatus));
                }
            }
        }

        public bool updateContext()
        {
            if (this.mVideoInfo == null)
            {
                return false;
            }
            if (!this.mIsThumbMode)
            {
                if (this.mStatus == 5)
                {
                    Log.d("DownloadVideoContext", string.Concat(new object[] { "update download video context , delete msgtrans = ", this.mVideoInfo.nMsgTransID, ", msgid = ", this.mVideoInfo.nMsgSvrID }));
                   // StorageMgr.msgVideo.delByMsgSvrID(this.mVideoInfo.nMsgSvrID);
                    base.updateBlockInfo(true, this.mVideoInfo.nTotalDataLen);
                }
                else
                {
                    Log.d("DownloadVideoContext", "update download video context now, status=" + this.mStatus);
                    this.mVideoInfo.nStatus = this.mStatus;
                    this.mVideoInfo.nLastModifyTime = (uint) (Util.getNowMilliseconds() / 1000.0);
                    //StorageMgr.msgVideo.update(this.mVideoInfo);
                    base.updateBlockInfo(false, this.mVideoInfo.nTotalDataLen);
                }
            }
            return true;
        }

        public void updateProgressInfo(int partLen)
        {
            if (!this.mIsThumbMode && (this.mVideoInfo.nTotalDataLen != 0))
            {
                this.mProgressInfo.totalLength = this.mVideoInfo.nTotalDataLen;
                this.mProgressInfo.gotLength = this.mVideoInfo.nTransDataLen + partLen;
                this.mProgressInfo.msgSvrId = this.mVideoInfo.nMsgSvrID;
                this.mProgressInfo.status = convertStatus(this.mStatus);
                this.mProgressInfo.printInfo();
                //EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADVIDEO_PROGRESS, this.mProgressInfo, null);
            }
        }

        public class ProgressInfo
        {
            public int gotLength;
            public int msgSvrId;
            public int status;
            public int totalLength;

            public bool isCompleted()
            {
                return (this.gotLength >= this.totalLength);
            }

            public void printInfo()
            {
                Log.d("DownloadVideoContext", string.Concat(new object[] { " progress: totalLen = ", this.totalLength, ",gotLen =", this.gotLength, ", status = ", (MsgUIStatus) this.status, ", msgid = ", this.msgSvrId }));
            }
        }
    }
}

