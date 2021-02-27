namespace MicroMsg.Scene.Video
{

    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using micromsg;
    using MicroMsg.Storage;

    public class NetSceneUploadCdnVideo : NetSceneBaseEx<UploadVideoRequest, UploadVideoResponse, UploadVideoRequest.Builder>
    {
       // private const int INTERVAL = 200;
        //private string mCdnComReportInfo = "";
        //private long mLastProgress;
        //private const int MM_VIDEO_CAMERA_FRONT = 1;
        //private const int MM_VIDEO_CAMERA_REAR = 2;
        //private const int MM_VIDEO_NETWORK_OTHER = 2;
        //private const int MM_VIDEO_NETWORK_WIFI = 1;
       // public UploadVideoContext mVideoContext;
        private const string TAG = "NetSceneUploadVideo";

        //public bool doCancel()
        //{
        //    if (this.mVideoContext != null)
        //    {
        //        CDNComService.Instance.cancelUploadMedia(this.mVideoContext.mVideoTrans.strClientMsgId);
        //        base.cancel();
        //        this.mVideoContext.mStatus = 4;
        //        this.doSceneFinished();
        //    }
        //    return true;
        //}

        //private void doReportInfo(int retCode)
        //{
        //    int num = this.mVideoContext.mVideoTrans.nTotalDataLen + this.mVideoContext.mThumbTotalLength;
        //    string str = string.Concat(new object[] { retCode, ",", 1, ",", (long) this.mVideoContext.startTimestamp, ",", (long) Util.getNowMilliseconds(), ",", ReportService.getReportNetworkType(), ",", CdnMediaType.MediaType_VIDEO, ",", num, ",", this.mCdnComReportInfo });
        //    NetSceneKVReport.addReportItem(0x28b5, str);
        //}

        //public void doScene(UploadVideoContext videoContext)
        //{
        //    if ((videoContext.mStatus != 0) && (videoContext.mStatus != 4))
        //    {
        //        Log.e("NetSceneUploadVideo", "doScene videoContext.mStatus error, status = " + videoContext.mStatus);
        //    }
        //    else
        //    {
        //        videoContext.mStatus = 1;
        //        this.mVideoContext = videoContext;
        //        this.mVideoContext.mSceneHandle = this;
        //        this.doSceneToCDN();
        //    }
        //}

        //public void doSceneFinished()
        //{
        //    if (this.mVideoContext != null)
        //    {
        //        this.mVideoContext.updateProgressInfo(0);
        //        this.mVideoContext.updateContext();
        //        this.mVideoContext.onFinished();
        //        this.mVideoContext.mSceneHandle = null;
        //        this.mVideoContext = null;
        //        Log.d("NetSceneUploadVideo", "scene Finished. ");
        //    }
        //}

        //private int doSceneToCDN()
        //{
        //    RTCDNUploadPara para = new RTCDNUploadPara {
        //        fileFullPath = this.mVideoContext.mVideoTrans.strImagePath,
        //        filetype = 4,
        //        toUser = this.mVideoContext.mVideoTrans.strToUserName,
        //        clientMediaID = this.mVideoContext.mChatMsg.strClientMsgId,
        //        arg = 0,
        //        hasThumb = true,
        //        thumbfileFullPath = this.mVideoContext.mVideoTrans.strThumbnail
        //    };
        //    this.mVideoContext.doCloseFile();
        //    para.needStorage = false;
        //    para.isStreamMedia = false;
        //    para.priority = 2;
        //    if (this.mVideoContext.mChatMsg.nMsgType == 0x3e)
        //    {
        //        para.smallVideoFlag = 1;
        //    }
        //    if (this.mVideoContext.mOrigChatMsg != null)
        //    {
        //        Log.d("NetSceneUploadVideo", "forward video msg , mOrigChatMsg = " + this.mVideoContext.mOrigChatMsg.strMsg);
        //        VideoDetailInfo info = UploadVideoService.parseVideoMsgXML(this.mVideoContext.mOrigChatMsg.strMsg);
        //        if (((info != null) && !string.IsNullOrEmpty(info.mCdnAesKey)) && !string.IsNullOrEmpty(info.mCdnVideoUrl))
        //        {
        //            para.fileid = info.mCdnVideoUrl;
        //            para.aeskey = info.mCdnAesKey;
        //            para.fileLength = (uint) info.nTotalDataLen;
        //        }
        //        else
        //        {
        //            Log.d("NetSceneUploadVideo", "Parse video msg xml failed: " + this.mVideoContext.mOrigChatMsg.strMsg);
        //        }
        //    }
        //    return CDNComService.Instance.startupUploadMedia(para, this);
        //}

        public void doSceneToCGI(string toUsername,MsgTrans videoinfo, DownloadVideoContext contextInfo, int nMsgType)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.FromUserName = videoinfo.strToUserName;
            base.mBuilder.ToUserName = toUsername;
            base.mBuilder.ClientMsgId = videoinfo.nMsgSvrID.ToString();
            base.mBuilder.PlayLength =(uint)videoinfo.nDuration;
            base.mBuilder.NetworkEnv = 1;
            base.mBuilder.CameraType = 2;
            base.mSessionPack.mCmdID = 0x27;
            base.mSessionPack.mNeedCompress = false;
            base.mBuilder.VideoTotalLen = (uint)videoinfo.nTotalDataLen;
            base.mBuilder.VideoStartPos = 0;
            base.mBuilder.VideoData = Util.toSKBuffer(new byte[0]);
            base.mBuilder.ThumbTotalLen = (uint)contextInfo.mCdnThumbLength;
            base.mBuilder.ThumbStartPos = 0;
            base.mBuilder.ThumbData = Util.toSKBuffer(new byte[0]);
            base.mBuilder.CDNVideoUrl = contextInfo.mCdnVideoUrl;
            base.mBuilder.AESKey = contextInfo.mCdnAesKey;
            base.mBuilder.EncryVer = 1;
            base.mBuilder.CDNThumbUrl = contextInfo.mCdnThumbUrl;
            base.mBuilder.CDNThumbImgSize = contextInfo.mCdnThumbLength;
            base.mBuilder.CDNThumbImgWidth = 0x84;
            base.mBuilder.CDNThumbImgHeight = 0x63;
            base.mBuilder.CDNThumbAESKey = contextInfo.mCdnThumbAesKey;
            if (nMsgType == 0x3e)//小视频
            {
                base.mBuilder.FuncFlag = 3;
                base.mBuilder.VideoMd5 = Util.nullAsNil("");
            }
            base.endBuilder();
        }

        protected override void onFailed(UploadVideoRequest request, UploadVideoResponse response)
        {
            RetConst ret = (RetConst)response.BaseResponse.Ret;
        }

        protected override void onSuccess(UploadVideoRequest request, UploadVideoResponse response)
        {

                RetConst ret = (RetConst) response.BaseResponse.Ret;

                if (ret != RetConst.MM_OK)
                {
                    Log.e("NetSceneUploadVideo", "upload video failed, ret = " + ret);
                }
                Log.w("视频上传", "upload video Success！");
            
        }
    }
}

