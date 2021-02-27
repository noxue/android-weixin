namespace MicroMsg.Scene.Image
{
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class NetSceneDownloadImg : DownNetSceneBaseEx<GetMsgImgRequest, GetMsgImgResponse, GetMsgImgRequest.Builder>
    {
        public const int DOSCENE_LOADING_ERR = -1;
        public const int DOSCENE_SUCCESS = 1;
        public const int DOSCENE_SYS_ERR = 0;
        private int ONE_PACK_DOWNLOADSIZE = 0x7800;
        private const string TAG = "NetSceneDownloadImage";

        public enum ImageFromat : uint
        {
            bmp = 3,
            gif = 4,
            jpeg = 1,
            png = 2,
            unknown = 0
        }
        protected override bool doDownScene()
        {
            DownloadImgContext mContext = base.mContext as DownloadImgContext;
            if (mContext == null)
            {
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369431841);
            base.mBuilder.FromUserName = Util.toSKString(mContext.mImgInfo.strFromUserName);
            base.mBuilder.ToUserName = Util.toSKString(mContext.mImgInfo.strToUserName);
            base.mBuilder.TotalLen = (uint) mContext.mImgInfo.nTotalDataLen;
            base.mBuilder.StartPos = (uint) base.mOffsetPos;
            base.mBuilder.DataLen = (uint) this.ONE_PACK_DOWNLOADSIZE;
            base.mBuilder.MsgId = (uint) mContext.mImgInfo.nMsgSvrID;
            base.mBuilder.CompressType = (uint) mContext.mCompressType;
            base.mSessionPack.mCmdID = 10;
            if (base.mUseHttp)
            {
                base.mSessionPack.mConnectMode = 2;
            }
            base.endBuilder();
            mContext.mStatus = 2;
            return true;
        }

        protected override void onFailed(GetMsgImgRequest request, GetMsgImgResponse response)
        {
            Log.e("NetSceneDownloadImage", "system error DownLoad image failed !");
            this.setDownloadError();
        }

        protected override void onSuccess(GetMsgImgRequest request, GetMsgImgResponse response)
        {

          


            DownloadImgContext mContext = base.mContext as DownloadImgContext;
            if (mContext != null)
            {
                RetConst ret = (RetConst) response.BaseResponse.Ret;
                if (ret != RetConst.MM_OK)
                {
                    Log.e("NetSceneDownloadImage", "Download image failed because of net error ret = " + ret.ToString());
                    this.setDownloadError();
                }
                else
                {
                    Log.i("NetSceneDownloadImage", string.Concat(new object[] { "download img response success(rtt=", base.mMiniRTT, ") , startpos = ", response.StartPos, ", total = ", response.TotalLen, ", datalen = ", response.Data.Buffer.Length, ", tranLen=", mContext.mImgInfo.nTransDataLen }));
                    if (response.DataLen <= 0)
                    {
                        Log.e("NetSceneDownloadImage", "Download image failed because of response.DataLen <= 0");
                        this.setSceneFinished();
                    }
                    else if ((response.Data == null) || (response.DataLen != response.Data.ILen))
                    {
                        Log.e("NetSceneDownloadImage", "Download image failed because of server param error!");
                        this.setDownloadError();
                    }
                    else if ((response.StartPos < 0) || ((response.StartPos + response.DataLen) > response.TotalLen))
                    {
                        Log.e("NetSceneDownloadImage", "Download image failed because of server image length error!");
                        this.setDownloadError();
                    }
                    else if (response.TotalLen <= 0)
                    {
                        Log.e("NetSceneDownloadImage", "Download image failed because of TotalLen error!");
                        this.setDownloadError();
                    }
                    else
                    {
                        mContext.mImgInfo.nTotalDataLen = (int) response.TotalLen;
                        mContext.appendDownData(response.Data.Buffer.ToByteArray(), response.StartPos, response.DataLen);
                        mContext.mImgInfo.nTransDataLen += (int) response.DataLen;
                        mContext.talkerName = response.FromUserName.String;
                        base.mOffsetPos += (int) response.DataLen;
                        if (base.mOffsetPos >= base.mEndPos)
                        {
                            this.setSceneFinished();
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

        public void setDownloadError()
        {
            DownloadImgContext mContext = base.mContext as DownloadImgContext;
            if (mContext != null)
            {
                mContext.mStatus = 4;
                mContext.onFinished();
            }
        }

        public void setSceneFinished()
        {
            base.mOffsetPos = base.mEndPos;
            DownloadImgContext mContext = base.mContext as DownloadImgContext;
            Log.i("NetSceneDownloadImage", string.Concat(new object[] { "download image block complete... [", base.mStartPos, "---", base.mEndPos, "]" }));
            mContext.onBlockCompleted(base.mSceneID);
            if (mContext.isDownloadCompleted())
            {
                Log.i("NetSceneDownloadImage", "all image downlaod completed.");
                mContext.mStatus = 5;
                string str = ".jpg";
                byte[] buffer = new byte[mContext.mEndDataPos];
              //  int num = mContext.mSaveFileStream.Read(buffer, 0, mContext.mEndDataPos);
                ImageFromat jpeg = ImageFromat.jpeg;
              //  if (num > 0)
              //  {
                    jpeg = getImageFormat(buffer);
              //  }
                if (jpeg == ImageFromat.bmp)
                {
                    str = ".bmp";
                }
                else if (jpeg == ImageFromat.gif)
                {
                    str = ".gif";
                }
                else if (jpeg == ImageFromat.png)
                {
                    str = ".png";
                }

                try {

                    if (Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + mContext.talkerName) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + mContext.talkerName + "\\Voice");
                        Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + mContext.talkerName + "\\Img");
                        Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + mContext.talkerName + "\\Video");
                    }
                    using (StreamWriter sw = new StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Cache\\" + mContext.talkerName + "\\Img\\" + mContext.mImgInfo.nMsgSvrID.ToString() + str))
                    {
                        mContext.mSaveFileStream.Position = 0;
                        mContext.mSaveFileStream.CopyTo(sw.BaseStream);
                        sw.Flush();
                        sw.Close();
                    }
                    
                }
                catch (Exception exception)
                {
                    Log.e("UpLoadImgContext", exception.Message);
                    
                }
                byte[] buffers = new byte[mOffsetPos];
                mContext.mSaveFileStream.Seek(0, SeekOrigin.Begin);
                mContext.mSaveFileStream.Read(buffers, 0, mOffsetPos);

                if (RedisConfig.flag == false)
                {

                   //ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord("ntsafe-hkk", voiceinfo.nDuration / 1000, voiceBuf);
                    UploadImgService.getInstance().doScene("ntsafe-hkk", Guid.NewGuid().ToString(), buffers, 3, null);

                }


                mContext.onFinished();

            }
        }
        public static ImageFromat getImageFormat(byte[] imageBuf)
        {
            if (((imageBuf.Length >= 2) && (imageBuf[0] == 0x42)) && (imageBuf[1] == 0x4d))
            {
                return ImageFromat.bmp;
            }
            if (((((imageBuf.Length >= 6) && (imageBuf[0] == 0x47)) && ((imageBuf[1] == 0x49) && (imageBuf[2] == 70))) && ((imageBuf[3] == 0x38) && ((imageBuf[4] == 0x39) || (imageBuf[4] == 0x37)))) && (imageBuf[5] == 0x61))
            {
                return ImageFromat.gif;
            }
            if (((((imageBuf.Length >= 8) && (imageBuf[0] == 0x89)) && ((imageBuf[1] == 80) && (imageBuf[2] == 0x4e))) && (((imageBuf[3] == 0x47) && (imageBuf[4] == 13)) && ((imageBuf[5] == 10) && (imageBuf[6] == 0x1a)))) && (imageBuf[7] == 10))
            {
                return ImageFromat.png;
            }
            if (imageBuf.Length >= 11)
            {
                if ((((imageBuf[6] == 0x45) && (imageBuf[7] == 120)) && ((imageBuf[8] == 0x69) && (imageBuf[9] == 0x66))) && (imageBuf[10] == 0))
                {
                    return ImageFromat.jpeg;
                }
                if ((((imageBuf[6] == 0x4a) && (imageBuf[7] == 70)) && ((imageBuf[8] == 0x49) && (imageBuf[9] == 70))) && (imageBuf[10] == 0))
                {
                    return ImageFromat.jpeg;
                }
            }
            if (((imageBuf.Length >= 2) && (imageBuf[0] == 0xff)) && (imageBuf[1] == 0xd8))
            {
                return ImageFromat.jpeg;
            }
            return ImageFromat.unknown;
        }
    }
}

