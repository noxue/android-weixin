namespace MicroMsg.Scene.Image
{
    using MicroMsg.Scene;
    using System;

    public class UpLoadImgContextMgr : ContextMgrBase<UpLoadImgContext>
    {
        public static UpLoadImgContextMgr gUpImgContextMgr;
        private const string TAG = "UpLoadImgContextMgr";

        public static UpLoadImgContextMgr getInstance()
        {
            if (gUpImgContextMgr == null)
            {
                gUpImgContextMgr = new UpLoadImgContextMgr();
            }
            return gUpImgContextMgr;
        }
    }
}

