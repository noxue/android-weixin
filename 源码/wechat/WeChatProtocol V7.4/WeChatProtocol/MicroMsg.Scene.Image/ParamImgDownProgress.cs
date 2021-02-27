namespace MicroMsg.Scene.Image
{
    using System;

    public class ParamImgDownProgress
    {
        public int mImageLocalId;
        public string mstrProgress = "0";
        public string mToTalker = "";

        public ParamImgDownProgress(string strProgress, int imageLocalId, string toTalker)
        {
            this.mstrProgress = strProgress;
            this.mImageLocalId = imageLocalId;
            this.mToTalker = toTalker;
        }
    }
}

