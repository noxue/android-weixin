namespace MicroMsg.Scene.Image
{
    using System;

    public class CImgMsgContext
    {
        public string aesKey = "";
        public string bigUrlKey = "";
        public int encryVer;
        public int hdlength;
        public int CDNThumbImgHeight;
        public int CDNThumbImgWidth;
        public int length;
        public string midUrlKey = "";
        public string thumbaesKey = "";
        public int thumblength;
        public string thumbUrlKey = "";

        public bool isCdnImgMsg()
        {
            if (this.aesKey.Length < 0x10)
            {
                return false;
            }
            return true;
        }

        public bool isCdnImgMsgWithThumb()
        {
            if (!this.isCdnImgMsg())
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.thumbUrlKey))
            {
                return false;
            }
            return true;
        }
    }
}
