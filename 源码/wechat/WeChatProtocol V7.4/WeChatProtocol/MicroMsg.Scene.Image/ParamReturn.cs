namespace MicroMsg.Scene.Image
{
    using MicroMsg.Protocol;
    using System;

    public class ParamReturn
    {
        public int mImageLocalId;
        public int mImgMsgTranID;
        public int mProgress;
        public RetConst mRetCode;
        public string mStrImgPath;
        public string mToTalker;

        public ParamReturn(int imgMsgTranID, RetConst retCode)
        {
            this.mToTalker = "";
            this.mRetCode = RetConst.MM_ERR_SYS;
            this.mImgMsgTranID = imgMsgTranID;
            this.mRetCode = retCode;
        }

        public ParamReturn(int nProgress, int imageLocalId, string toTalker)
        {
            this.mToTalker = "";
            this.mRetCode = RetConst.MM_ERR_SYS;
            this.mProgress = nProgress;
            this.mImageLocalId = imageLocalId;
            this.mToTalker = toTalker;
        }

        public ParamReturn(int nProgress, int imageLocalId, string toTalker, string strImgPath)
        {
            this.mToTalker = "";
            this.mRetCode = RetConst.MM_ERR_SYS;
            this.mProgress = nProgress;
            this.mImageLocalId = imageLocalId;
            this.mToTalker = toTalker;
            this.mStrImgPath = strImgPath;
        }
    }
}

