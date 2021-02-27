namespace MicroMsg.Scene.Image
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.IO;

    public class NetSceneUploadImage : NetSceneBaseEx<UploadMsgImgRequest, UploadMsgImgResponse, UploadMsgImgRequest.Builder>
    {
        public bool mIsNeedScale = true;
        public const int MM_MSGIMG_WITH_COMPRESS = 0;
        public const int MM_MSGIMG_WITHOUT_COMPRESS = 1;
        public UpLoadImgContext mUpImgContext;
        private int mUpLoadedSize = 0x2000;
        private const string TAG = "NetSceneUploadImage";

        public NetSceneUploadImage()
        {
            this.mUpLoadedSize = 0x10000;
        }

        public void doScene(UpLoadImgContext imgContext)
        {
            if ((imgContext.mStatus != 0) && (imgContext.mStatus != 4))
            {
                Log.e("NetSceneUploadImage", "doScene imgContext.mStatus error, status = " + imgContext.mStatus);
            }
            else
            {
                imgContext.mStatus = 1;
                this.mUpImgContext = imgContext;
                if (((this.mUpImgContext == null) || (this.mUpImgContext.imgInfo == null)) || (this.mUpImgContext.mBigImageMemoryStream == null))
                {
                    this.mUpImgContext.mStatus = 4;
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
                    this.doSceneFinished();
                }
                else if (((this.mUpImgContext.imgInfo.nTotalDataLen - this.mUpImgContext.imgInfo.nTransDataLen) == 0) || (this.mUpImgContext.imgInfo.nTotalDataLen == 0))
                {
                    Log.e("NetSceneUploadImage", "mUpImgContext send dataLen = 0 mUpImgContext.imgInfo.nMsgTransID = " + this.mUpImgContext.imgInfo.nMsgTransID);
                    this.mUpImgContext.mStatus = 4;
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName, this.mUpImgContext.imgInfo.strImagePath), null);
                    this.doSceneFinished();
                }
                else
                {
                    Log.i("NetSceneUploadImage", "file size = " + this.mUpImgContext.imgInfo.nTotalDataLen + " byte");
                    Account account = AccountMgr.getCurAccount();
                    this.mUpImgContext.imgInfo.strFromUserName = account.strUsrName;
                    int nProgress = (this.mUpImgContext.imgInfo.nTransDataLen * 100) / this.mUpImgContext.imgInfo.nTotalDataLen;
                    if (nProgress <= 5)
                    {
                        nProgress = 5;
                    }
                    Log.i("NetSceneUploadImage", "Progress is: " + nProgress + "%");
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_PROGRESS, new ParamReturn(nProgress, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
                    this.doSceneEx();
                }
            }
        }

        private void doSceneEx()
        {
            int count = this.mUpImgContext.imgInfo.nTotalDataLen - this.mUpImgContext.imgInfo.nTransDataLen;
            if (count > this.mUpLoadedSize)
            {
                count = this.mUpLoadedSize;
            }
            if ((string.IsNullOrEmpty(this.mUpImgContext.imgInfo.strFromUserName) || string.IsNullOrEmpty(this.mUpImgContext.imgInfo.strToUserName)) || string.IsNullOrEmpty(this.mUpImgContext.imgInfo.strClientMsgId))
            {
                Log.e("NetSceneUploadImage", "Param error doScene fail");
                this.mUpImgContext.mStatus = 4;
                EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgTransID, this.mUpImgContext.talkerName), null);
                this.doSceneFinished();
            }
            else
            {
                byte[] buffer = new byte[count];
                this.mUpImgContext.mBigImageMemoryStream.Seek((long)this.mUpImgContext.imgInfo.nTransDataLen, SeekOrigin.Begin);
                this.mUpImgContext.mBigImageMemoryStream.Read(buffer, 0, count);
                if (buffer == null)
                {
                    Log.e("NetSceneUploadImage", "read buffer From Stream fail");
                    this.mUpImgContext.mStatus = 4;
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
                    this.doSceneFinished();
                }
                else
                {
                    base.beginBuilder();
                    base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
                    base.mBuilder.FromUserName = Util.toSKString(this.mUpImgContext.imgInfo.strFromUserName);
                    base.mBuilder.ToUserName = Util.toSKString(this.mUpImgContext.imgInfo.strToUserName);
                    base.mBuilder.ClientImgId = Util.toSKString(this.mUpImgContext.imgInfo.strClientMsgId);
                    base.mBuilder.TotalLen = (uint)this.mUpImgContext.imgInfo.nTotalDataLen;
                    base.mBuilder.StartPos = (uint)this.mUpImgContext.imgInfo.nTransDataLen;
                    if (!this.mIsNeedScale)
                    {
                        base.mBuilder.CompressType = 1;
                    }
                    else
                    {
                        base.mBuilder.CompressType = 0;
                    }
                    base.mBuilder.DataLen = (uint)count;
                    base.mBuilder.Data = Util.toSKBuffer(buffer);
                    base.mBuilder.MsgType = (uint)this.mUpImgContext.mMsgType;
                    base.mSessionPack.mCmdID = 9;
                    base.mSessionPack.mNeedCompress = false;
                    base.mSessionPack.mConnectMode = 1;
                    base.endBuilder();
                    this.mUpImgContext.mStatus = 2;
                }
            }
        }

        public void doSceneFinished()
        {
            this.mUpImgContext.doFinished();
        }

        protected override void onFailed(UploadMsgImgRequest request, UploadMsgImgResponse response)
        {
            Log.e("NetSceneUploadImage", "Send image failed because of system error");
            this.mUpImgContext.mStatus = 4;
            EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
            this.doSceneFinished();
        }

        protected override void onSuccess(UploadMsgImgRequest request, UploadMsgImgResponse response)
        {

           // string aa = Util.byteToHexStr(request.ToByteArray());
           // string bb = Util.byteToHexStr(response.ToByteArray());
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            switch (ret)
            {
                case RetConst.MM_OK:
                    if (((response.StartPos >= 0) && (this.mUpImgContext.imgInfo.nTotalDataLen > 0)) && (((response.StartPos <= this.mUpImgContext.imgInfo.nTotalDataLen) && (response.StartPos >= this.mUpImgContext.imgInfo.nTransDataLen)) && (!this.mUpImgContext.isUploadCompleted() || (response.DataLen > 0))))
                    {
                        if (response.MsgId > 0)
                        {
                            this.mUpImgContext.imgInfo.nMsgSvrID = (int)response.MsgId;
                            this.mUpImgContext.chatMsgInfo.nMsgSvrID = (int)response.MsgId;
                        }
                        this.mUpImgContext.imgInfo.nTransDataLen = (int)response.StartPos;
                        this.mUpImgContext.updateProgressInfo();
                        if (this.mUpImgContext.isUploadCompleted())
                        {
                            this.mUpImgContext.mStatus = 5;
                            Log.i("NetSceneUploadImage", "send Complete!");
                            EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_COMPLETE, new ParamReturn(100, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
                            this.doSceneFinished();
                        }
                        else
                        {
                            this.doSceneEx();
                        }
                        return;
                    }
                    Log.e("NetSceneUploadImage", "Send image failed because of response error");
                    this.mUpImgContext.mStatus = 4;
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
                    this.doSceneFinished();
                    return;

                case RetConst.MM_ERR_SYS:
                    {
                        int num = this.mUpImgContext.imgInfo.nTotalDataLen - this.mUpImgContext.imgInfo.nTransDataLen;
                        if (num <= this.mUpLoadedSize)
                        {
                            this.mUpImgContext.mStatus = 5;
                            this.mUpImgContext.imgInfo.nTransDataLen = this.mUpImgContext.imgInfo.nTotalDataLen;
                            Log.i("NetSceneUploadImage", "Resend Complete!");
                            EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_COMPLETE, new ParamReturn(100, this.mUpImgContext.imgInfo.nMsgTransID, this.mUpImgContext.talkerName), null);
                            this.mUpImgContext.mStatus = 4;
                            this.doSceneFinished();
                            return;
                        }
                        break;
                    }
                case RetConst.MM_ERR_NEED_QQPWD:
                    Log.i("NetSceneUploadImage", "send QQ IMG ERR_NEED_QQPWD");
                    this.mUpImgContext.mStatus = 4;
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(this.mUpImgContext.imgInfo.nMsgTransID, RetConst.MM_ERR_NEED_QQPWD), null);
                    this.doSceneFinished();
                    return;
            }
            Log.e("NetSceneUploadImage", "Send image failed because of net error ret = " + ret.ToString());
            this.mUpImgContext.mStatus = 4;
            // EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_FAIL, new ParamReturn(-1, this.mUpImgContext.imgInfo.nMsgLocalID, this.mUpImgContext.talkerName), null);
            this.doSceneFinished();
        }
    }
}

