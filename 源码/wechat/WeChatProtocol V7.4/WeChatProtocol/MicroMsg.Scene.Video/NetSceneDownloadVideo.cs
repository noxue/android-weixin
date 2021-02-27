namespace MicroMsg.Scene.Video
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using micromsg;
    using System.IO;

    public class NetSceneDownloadVideo : DownNetSceneBaseEx<DownloadVideoRequest, DownloadVideoResponse, DownloadVideoRequest.Builder>
    {
        private const int MM_VIDEO_NETWORK_OTHER = 2;
        private const int MM_VIDEO_NETWORK_WIFI = 1;
        private const string TAG = "NetSceneDwVideoEx";

        protected override bool doDownScene()
        {
            DownloadVideoContext mContext = base.mContext as DownloadVideoContext;
            if (mContext == null)
            {
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.TotalLen = (uint) mContext.mVideoInfo.nTotalDataLen;
            base.mBuilder.StartPos = (uint) base.mOffsetPos;
            base.mBuilder.NetworkEnv = 1;
            base.mBuilder.MsgId = (uint) mContext.mVideoInfo.nMsgSvrID;
            base.mSessionPack.mCmdID = 40;
            if (base.mUseHttp)
            {
                base.mSessionPack.mConnectMode = 2;
            }
            base.endBuilder();
            return true;
        }

        protected override void onFailed(DownloadVideoRequest request, DownloadVideoResponse response)
        {
            DownloadVideoContext mContext = base.mContext as DownloadVideoContext;
            if ((mContext == null) || !mContext.isRunning())
            {
                Log.e("NetSceneDwVideoEx", "scene is finished, ignored onFailed. ");
            }
            else
            {
                mContext.mStatus = 4;
                mContext.onFinishedEx();
            }
        }

        protected override void onSuccess(DownloadVideoRequest request, DownloadVideoResponse response)
        {
            DownloadVideoContext mContext = base.mContext as DownloadVideoContext;
            if ((mContext == null) || !mContext.isRunning())
            {
                Log.e("NetSceneDwVideoEx", "scene is finished, ignored response. ");
            }
            else
            {
                RetConst ret = (RetConst) response.BaseResponse.Ret;
                if (ret != RetConst.MM_OK)
                {
                    Log.e("NetSceneDwVideoEx", "download video failed, ret = " + ret);
                    mContext.mStatus = 4;
                    mContext.onFinishedEx();
                }
                else
                {
                    Log.d("NetSceneDwVideoEx", string.Concat(new object[] { "download response success(rtt=", base.mMiniRTT, ") , startpos = ", response.StartPos, ", total = ", response.TotalLen, ", datalen = ", response.Data.Buffer.Length, ", tranLen=", mContext.mVideoInfo.nTransDataLen }));
                    if ((response.StartPos < 0) || ((response.StartPos + response.Data.Buffer.Length) > response.TotalLen))
                    {
                        Log.e("NetSceneDwVideoEx", "download failed ");
                        mContext.mStatus = 4;
                        mContext.onFinishedEx();
                    }
                    else
                    {
                        mContext.mVideoInfo.nTotalDataLen = (int) response.TotalLen;
                        mContext.appendDownData(response.Data.Buffer.ToByteArray(), response.StartPos, (uint) response.Data.Buffer.Length);
                        mContext.mVideoInfo.nTransDataLen += response.Data.Buffer.Length;
                        base.mOffsetPos += response.Data.Buffer.Length;
                        if (base.mOffsetPos >= base.mEndPos)
                        {
                            Log.d("NetSceneDwVideoEx", string.Concat(new object[] { "download video block complete... [", base.mStartPos, "---", base.mEndPos, "]" }));
                            mContext.onBlockCompleted(base.mSceneID);
                            if (mContext.isDownloadCompleted())
                            {
                                Log.d("NetSceneDwVideoEx", "all videos downlaod completed.");
                                mContext.mStatus = 5;
                                mContext.onFinishedEx();
                            }
                        }
                        else
                        {
                            mContext.updateProgressInfo(0);
                            this.doDownScene();
                        }
                    }
                }
            }
        }
    }
}

