namespace MicroMsg.Scene
{
    using MicroMsg.Common.Utils;
    using System;
    using Google.ProtocolBuffers;

    public abstract class DownNetSceneBaseEx<TRequest, TResponse, TBuilder> : NetSceneBaseEx<TRequest, TResponse, TBuilder> where TRequest: AbstractMessageLite<TRequest, TBuilder> where TBuilder: AbstractBuilderLite<TRequest, TBuilder>
    {
        protected DownloadContextBase<TRequest, TResponse, TBuilder> mContext;
        public int mEndPos;
        public int mMiniRTT;
        public int mOffsetPos;
        public int mStartPos;
        public bool mUseHttp;
        private const string TAG = "DownNetSceneBaseEx";

        protected DownNetSceneBaseEx()
        {
            this.mMiniRTT = 0x3e8;
        }

        public void doCompleted()
        {
            base.cancel();
            this.mContext = null;
        }

        protected abstract bool doDownScene();
        public bool doScene(DownloadContextBase<TRequest, TResponse, TBuilder> context, int startPos, int offsetPos, int endPos, bool useHttp)
        {
            Log.d("DownNetSceneBaseEx", string.Concat(new object[] { "netscene startup, start = ", startPos, ", offset = ", offsetPos, ", end =", endPos }));
            this.mContext = context;
            this.mStartPos = startPos;
            this.mEndPos = endPos;
            this.mOffsetPos = offsetPos;
            this.mUseHttp = useHttp;
            if (this.mOffsetPos >= this.mEndPos)
            {
                Log.e("DownNetSceneBaseEx", "no need data for download video scene.");
                return false;
            }
            return this.doDownScene();
        }

        public bool isCompleted()
        {
            return (this.mOffsetPos >= this.mEndPos);
        }

        protected override void onSampleSpeed(int RTT)
        {
            if (this.mMiniRTT > RTT)
            {
                this.mMiniRTT = RTT;
            }
        }
    }
}

