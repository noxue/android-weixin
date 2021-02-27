//namespace MicroMsg.Scene.Image
//{
//    using micromsg;
//    using MicroMsg.Common.Utils;
//    using MicroMsg.Scene;
//    using System;

//    public class NetSceneDownloadCdnImg : DownNetSceneBaseEx<GetMsgImgRequest, GetMsgImgResponse, GetMsgImgRequest.Builder>
//    {
//        public const int DOSCENE_LOADING_ERR = -1;
//        public const int DOSCENE_SUCCESS = 1;
//        public const int DOSCENE_SYS_ERR = 0;
//        public const int MM_MSGIMG_WITH_COMPRESS = 0;
//        public const int MM_MSGIMG_WITHOUT_COMPRESS = 1;
//        private int ONE_PACK_DOWNLOADSIZE = 0x7800;
//        private const string TAG = "NetSceneDlCdnImage";

//        protected override bool doDownScene()
//        {
//            DownloadImgContext mContext = base.mContext as DownloadImgContext;
//            if (mContext == null)
//            {
//                return false;
//            }
//            RTCDNDownloadPara para = new RTCDNDownloadPara {
//                fileFullPath = mContext.mImgInfo.strImagePath,
//                aesKey = mContext.mImgMsgContent.aesKey
//            };
//            if (mContext.mCompressType == 1)
//            {
//                para.fileId = mContext.mImgMsgContent.bigUrlKey;
//                para.filetype = 1;
//            }
//            else
//            {
//                para.fileId = mContext.mImgMsgContent.midUrlKey;
//                para.filetype = 2;
//            }
//            para.fileLength = (uint) mContext.mImgInfo.nTotalDataLen;
//            para.clientMediaID = mContext.mChatMsg.strClientMsgId + para.filetype;
//            mContext.closeFileStream();
//            mContext.mStatus = 2;
//            return true;
//        }

//        public void onDownloadError(string clientMediaID, RTCDNDownloadResult downloadResult)
//        {
//            Log.e("NetSceneDlCdnImage", "CDN: onDownloadError , retCode = " + downloadResult.retCode);
//            this.setDownloadError();
//            DownloadImgContext mContext = base.mContext as DownloadImgContext;
//            if (mContext != null)
//            {
//                string str = string.Concat(new object[] { -1, ",", 2, ",", mContext.beginTime, ",", (long) Util.getNowMilliseconds(), ",", ServiceCenter.reportService.getReportNetworkType(), ",", 3, ",", mContext.mImgInfo.nTotalDataLen }) + "," + downloadResult.transInfo;
//                //ServiceCenter.reportService.addReportItem(0x28b5, str);
//            }
//        }

//        public void onDownloadProgress(string clientMediaID, RTProgressInfo progressInfo)
//        {
//            DownloadImgContext mContext = base.mContext as DownloadImgContext;
//            if (mContext != null)
//            {
//                mContext.mImgInfo.nTotalDataLen = progressInfo.toltalLength;
//                mContext.mImgInfo.nTransDataLen = progressInfo.finishedLength;
//                mContext.updateProgressInfo(0);
//            }
//        }

//        public void onDownloadSuccessed(string clientMediaID, RTCDNDownloadResult downloadResult)
//        {
//            Log.i("NetSceneDlCdnImage", "CDN: onDownloadSuccess , fileLength = " + downloadResult.fileLength);
//            DownloadImgContext mContext = base.mContext as DownloadImgContext;
//            if (mContext != null)
//            {
//                mContext.mImgInfo.nTotalDataLen = (int) downloadResult.fileLength;
//                mContext.mImgInfo.nTransDataLen = (int) downloadResult.fileLength;
//                this.setSceneFinished();
//                string str = string.Concat(new object[] { 0, ",", 2, ",", mContext.beginTime, ",", (long) Util.getNowMilliseconds(), ",", ServiceCenter.reportService.getReportNetworkType(), ",", 3, ",", mContext.mImgInfo.nTotalDataLen }) + "," + downloadResult.transInfo;
//                ServiceCenter.reportService.addReportItem(0x28b5, str);
//            }
//        }

//        protected override void onFailed(GetMsgImgRequest request, GetMsgImgResponse response)
//        {
//        }

//        protected override void onSuccess(GetMsgImgRequest request, GetMsgImgResponse response)
//        {
//        }

//        public void setDownloadError()
//        {
//            DownloadImgContext mContext = base.mContext as DownloadImgContext;
//            if (mContext != null)
//            {
//                mContext.mStatus = 4;
//                mContext.onFinished();
//            }
//        }

//        public void setSceneFinished()
//        {
//            base.mOffsetPos = base.mEndPos;
//            DownloadImgContext mContext = base.mContext as DownloadImgContext;
//            Log.i("NetSceneDlCdnImage", string.Concat(new object[] { "download image block complete... [", base.mStartPos, "---", base.mEndPos, "]" }));
//            mContext.onBlockCompleted(base.mSceneID);
//            if (mContext.isDownloadCompleted())
//            {
//                Log.i("NetSceneDlCdnImage", "all image downlaod completed.");
//                mContext.mStatus = 5;
//                mContext.onFinished();
//            }
//        }
//    }
//}

