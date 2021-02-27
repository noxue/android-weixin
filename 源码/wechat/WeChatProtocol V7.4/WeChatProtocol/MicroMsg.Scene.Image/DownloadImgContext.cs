namespace MicroMsg.Scene.Image
{
    
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;

    using System.IO.IsolatedStorage;
    using micromsg;
    using MicroMsg.Common.Algorithm;

    public class DownloadImgContext : DownloadContextBase<GetMsgImgRequest, GetMsgImgResponse, GetMsgImgRequest.Builder>
    {
        public long beginTime;
        public ChatMsg mChatMsg;
        public int mCompressType;
        public MsgTrans mImgInfo;
        public CImgMsgContext mImgMsgContent;
        public int mStatus;
        public const int STATUS_COMPLETE = 5;
        public const int STATUS_ERROR = 4;
        public const int STATUS_INIT = 0;
        public const int STATUS_LOADING = 2;
        public const int STATUS_READY = 1;
        private const string TAG = "DownloadImgContext";
        public string talkerName = "";

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

        protected override DownNetSceneBaseEx<GetMsgImgRequest, GetMsgImgResponse, GetMsgImgRequest.Builder> createDownSceneInstance()
        {
            return new NetSceneDownloadImg();
        }

        public string getProgressString(int byteLen)
        {
            string str = ((byteLen * 1.0) / 1024.0).ToString();
            int index = str.IndexOf(".", 0);
            if (index == -1)
            {
                return str;
            }
            return str.Substring(0, index + 2);
        }

        public bool isDownloadCompleted()
        {
            return ((this.mImgInfo.nTransDataLen >= this.mImgInfo.nTotalDataLen) && (this.mImgInfo.nTotalDataLen != 0));
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

        public void onFinished()
        {
            this.updateProgressInfo(0);
            this.updateDownloadImgContext();
            this.updateChatMsg();
            base.closeAllScene();
        }

        protected override string onPrepareDataFilePath(IsolatedStorageFile currentIsolatedStorage)
        {
            if (!currentIsolatedStorage.DirectoryExists(StorageIO.getImagePath()))
            {
                currentIsolatedStorage.CreateDirectory(StorageIO.getImagePath());
            }
            string hashString = MD5Core.GetHashString(this.mImgInfo.strThumbnail + Util.getNowMilliseconds());
            return (StorageIO.getImagePath() + "/" + hashString + "_recv.jpg");
        }

        public void startScene()
        {
            base.BLOCK_MIN_SIZE = 0x10000;
            this.mStatus = 1;
            if (this.mImgInfo.nTransDataLen >= this.mImgInfo.nTotalDataLen)
            {
                this.mStatus = 5;
                this.onFinished();
            }
            else if (!base.startSceneWithRange(this.mImgInfo.nTransDataLen, this.mImgInfo.nTotalDataLen, false))
            {
                this.mStatus = 4;
                this.onFinished();
            }
            else
            {
                this.mStatus = 2;
            }
        }

        public void updateChatMsg()
        {
            if (this.mChatMsg != null)
            {
                if (this.mStatus == 5)
                {
                    if (this.mCompressType == 1)
                    {
                        StorageIO.deleteFile(this.mChatMsg.strPath);
                    }
                    this.mChatMsg.strPath = this.mImgInfo.strImagePath;
                }
                this.mChatMsg.nStatus = convertStatus(this.mStatus);
                if ((this.mStatus == 5) && (this.mCompressType == 1))
                {
                    //this.mChatMsg.strContent = ChatMsgMgr.rePackImageLenXml(this.mChatMsg, this.mImgInfo.nTotalDataLen);
                }
               // StorageMgr.chatMsg.modifyMsg(this.mChatMsg);
            }
        }

        public bool updateDownloadImgContext()
        {
            if (this.mImgInfo == null)
            {
                return false;
            }
            if (this.mStatus == 5)
            {
                //StorageMgr.msgImg.delByMsgSrvID(this.mImgInfo.nMsgSvrID);
                base.updateBlockInfo(true, this.mImgInfo.nTotalDataLen);
            }
            else
            {
                this.mImgInfo.nStatus = this.mStatus;
                this.mImgInfo.nLastModifyTime = (uint) (Util.getNowMilliseconds() / 1000.0);
               // StorageMgr.msgImg.update(this.mImgInfo);
                base.updateBlockInfo(false, this.mImgInfo.nTotalDataLen);
            }
            return true;
        }

        public void updateProgressInfo(int partLen)
        {
            if (this.mStatus == 5)
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADIMG_COMPLETE, new ParamReturn(100, this.mImgInfo.nMsgLocalID, this.mImgInfo.strFromUserName, this.mImgInfo.strImagePath), null);
            }
            else if (this.mStatus == 4)
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADIMG_FAIL, new ParamReturn(-1, this.mImgInfo.nMsgLocalID, this.mImgInfo.strFromUserName), null);
            }
            else
            {
                string strProgress = this.getProgressString(this.mImgInfo.nTransDataLen);
                EventCenter.postEvent(EventConst.ON_NETSCENE_DOWNLOADIMG_PROGRESS, new ParamImgDownProgress(strProgress, this.mImgInfo.nMsgLocalID, this.mImgInfo.strFromUserName), null);
            }
        }
    }
}

