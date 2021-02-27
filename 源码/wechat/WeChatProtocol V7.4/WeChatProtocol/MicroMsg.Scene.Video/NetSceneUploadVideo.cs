namespace MicroMsg.Scene.Video
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using micromsg;

    public class NetSceneUploadVideo : NetSceneBaseEx<UploadVideoRequest, UploadVideoResponse, UploadVideoRequest.Builder>
    {
        private const int MM_VIDEO_CAMERA_FRONT = 1;
        private const int MM_VIDEO_CAMERA_REAR = 2;
        private const int MM_VIDEO_NETWORK_OTHER = 2;
        private const int MM_VIDEO_NETWORK_WIFI = 1;
        public UploadVideoContext mVideoContext;
        private const string TAG = "NetSceneUploadVideo";

        public bool doCancel()
        {
            if (this.mVideoContext != null)
            {
                base.cancel();
                this.mVideoContext.mStatus = 4;
                this.doSceneFinished();
            }
            return true;
        }

        public void doScene(UploadVideoContext videoContext)
        {
            if ((videoContext.mStatus != 0) && (videoContext.mStatus != 4))
            {
                Log.e("NetSceneUploadVideo", "doScene videoContext.mStatus error, status = " + videoContext.mStatus);
            }
            else
            {
                videoContext.mStatus = 1;
                this.mVideoContext = videoContext;
                //this.mVideoContext.mSceneHandle = this;
                this.doSceneEx();
            }
        }

        private void doSceneEx()
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            base.mBuilder.FromUserName = this.mVideoContext.mVideoTrans.strFromUserName;
            base.mBuilder.ToUserName = this.mVideoContext.mVideoTrans.strToUserName;
            base.mBuilder.ClientMsgId = this.mVideoContext.mVideoTrans.strClientMsgId;
            base.mBuilder.PlayLength = (uint) this.mVideoContext.mVideoTrans.nDuration;
            base.mBuilder.VideoTotalLen = (uint) this.mVideoContext.mTotalLength;
            base.mBuilder.ThumbTotalLen = (uint) this.mVideoContext.mThumbTotalLength;
            base.mBuilder.NetworkEnv = 1;
            base.mBuilder.CameraType = 2;
            base.mSessionPack.mCmdID = 0x27;
           // base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/sendsight";
            //base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mNeedCompress = false;
            byte[] inBytes = new byte[0];
            byte[] buffer2 = null;
            buffer2 = this.mVideoContext.getRemainThumbToSend();
            if (buffer2 != null)
            {
                this.mVideoContext.mStatus = 2;
                this.mVideoContext.mSendingLength = buffer2.Length;
                base.mBuilder.ThumbStartPos = (uint) this.mVideoContext.mThumbNetOffset;
                base.mBuilder.ThumbData = Util.toSKBuffer(buffer2);
                base.mBuilder.VideoStartPos = (uint) this.mVideoContext.mNetOffset;
                base.mBuilder.VideoData = Util.toSKBuffer(inBytes);
                base.endBuilder();
            }
            else
            {
                buffer2 = this.mVideoContext.getRemainVideoToSend();
                if (buffer2 != null)
                {
                    this.mVideoContext.mStatus = 3;
                    this.mVideoContext.mSendingLength = buffer2.Length;
                    base.mBuilder.ThumbStartPos = (uint) this.mVideoContext.mThumbNetOffset;
                    base.mBuilder.ThumbData = Util.toSKBuffer(inBytes);
                    base.mBuilder.VideoStartPos = (uint) this.mVideoContext.mNetOffset;
                    base.mBuilder.VideoData = Util.toSKBuffer(buffer2);
                    base.endBuilder();
                }
                else
                {
                    this.mVideoContext.mStatus = 5;
                    this.doSceneFinished();
                }
            }
        }

        public void doSceneFinished()
        {
            if (this.mVideoContext != null)
            {
                this.mVideoContext.updateProgressInfo(0);
                this.mVideoContext.updateContext();
                this.mVideoContext.onFinished();
                //this.mVideoContext.mSceneHandle = null;
                this.mVideoContext = null;
                Log.d("NetSceneUploadVideo", "scene Finished. ");
            }
        }

        protected override void onFailed(UploadVideoRequest request, UploadVideoResponse response)
        {
            if (this.mVideoContext != null)
            {
                this.mVideoContext.mStatus = 4;
                this.doSceneFinished();
            }
        }

        protected override void onSuccess(UploadVideoRequest request, UploadVideoResponse response)
        {
            if (this.mVideoContext != null)
            {
                RetConst ret = (RetConst) response.BaseResponse.Ret;
                if (ret != RetConst.MM_OK)
                {
                    Log.e("NetSceneUploadVideo", "upload video failed, ret = " + ret);
                    this.mVideoContext.mStatus = 4;
                    this.doSceneFinished();
                    return;
                }
                Log.d("NetSceneUploadVideo", string.Concat(new object[] { "upload block success , thumb_startpos = ", response.ThumbStartPos, ", video_startpos = ", response.VideoStartPos, ", thumb_translen = ", this.mVideoContext.mThumbNetOffset, ", video_tranLen =", this.mVideoContext.mNetOffset }));
                if (this.mVideoContext.mStatus == 2)
                {
                    this.mVideoContext.mThumbNetOffset += this.mVideoContext.mSendingLength;
                    goto Label_0119;
                }
                if (this.mVideoContext.mStatus == 3)
                {
                    this.mVideoContext.mNetOffset += this.mVideoContext.mSendingLength;
                    goto Label_0119;
                }
            }
            return;
        Label_0119:
            this.mVideoContext.mVideoTrans.nMsgSvrID = (int) response.MsgId;
            if (this.mVideoContext.isUploadCompleted())
            {
                Log.d("NetSceneUploadVideo", "send Completed. ");
                this.mVideoContext.mStatus = 5;
                this.doSceneFinished();
            }
            else
            {
                this.mVideoContext.updateProgressInfo(0);
                this.doSceneEx();
            }
        }
    }
}

