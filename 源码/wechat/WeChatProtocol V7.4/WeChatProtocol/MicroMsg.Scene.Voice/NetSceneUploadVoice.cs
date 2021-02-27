namespace MicroMsg.Scene.Voice
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;
    using System.Threading;
    using System.Timers;
    using MicroMsg.Common.Algorithm;

    public class NetSceneUploadVoice : NetSceneBaseEx<UploadVoiceRequest, UploadVoiceResponse, UploadVoiceRequest.Builder>
    {
        private TimerObject mTimerObject;
        //private System.Timers.Timer mTimerObject;
        private UploadVoiceContext mVoiceContext;
        private const string TAG = "NetSceneUploadVoice";

        public event onSceneFinishedDelegate mOnSceneFinished;

        public void checkNetScene()
        {
            if (((this.mVoiceContext.mStatus == 1) && !this.mVoiceContext.isSendCompleted()) && !this.mVoiceContext.isInvalidShortVoice())
            {
                byte[] toSend = this.mVoiceContext.getBufferFromHead();
                if (toSend != null)
                {
                    this.mVoiceContext.mStatus = 2;
                    this.doSceneEx(toSend);
                }
            }
        }

        public void checkRecorderSaver()
        {
            if (!this.mVoiceContext.isSaveCompleted())
            {
                this.mVoiceContext.checkRecorderSaver();
            }
        }

        public void dispatchVoiceHandler()
        {
            UploadVoiceContext mVoiceContext = this.mVoiceContext;
            if (mVoiceContext != null)
            {
                if (mVoiceContext.isFinished())
                {
                    Log.e("NetSceneUploadVoice", "completed already, close netscene.. ");
                    this.doSceneFinished(0);
                }
                else if (mVoiceContext.isInvalidShortVoice())// && mVoiceContext.isRecordEnd()
                {
                    mVoiceContext.mStatus = 4;
                    Log.e("NetSceneUploadVoice", "complete with short voice , ignored.. ");
                    this.doSceneFinished(3);
                }
                else if (mVoiceContext.isSaveCompleted() && mVoiceContext.isSendCompleted())
                {
                    mVoiceContext.mStatus = 5;
                    this.doSceneFinished(1);
                }
                else if (mVoiceContext.isSaveCompleted() && (mVoiceContext.mStatus == 4))
                {
                    Log.e("NetSceneUploadVoice", "save completed, but send error. ");
                    this.doSceneFinished(2);
                }
                else
                {
                    this.checkRecorderSaver();
                    this.checkNetScene();
                }
            }
        }

        public void doScene(UploadVoiceContext voiceContext)
        {
            if ((voiceContext.mStatus != 0) && (voiceContext.mStatus != 4))
            {
                Log.e("NetSceneUploadVoice", "doScene status error, status = " + voiceContext.mStatus);
            }
            else
            {
                voiceContext.mStatus = 1;
                this.mVoiceContext = voiceContext;
                this.mVoiceContext.mScene = this;

                if (mTimerObject == null)
                {
                    //this.mTimerObject = new System.Timers.Timer();

                    //this.mTimerObject.Elapsed += new ElapsedEventHandler((o,e) => NetSceneUploadVoice.onTimerHandler(this,new TimerEventArgs(this)));
                    //this.mTimerObject.Interval = 1000;
                        //new System.Timers.ElapsedEventHandler(NetSceneUploadVoice.onTimerHandler);
                    //mTimerObject.Start();
                    this.mTimerObject = TimerService.addTimer(1, new EventHandler(NetSceneUploadVoice.onTimerHandler), 0, -1, new TimerEventArgs(this));
                    this.mTimerObject.start();


                }
            }
        }

        private bool doSceneEx(byte[] toSend)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x13);
            base.mBuilder.FromUserName = AccountMgr.getCurAccount().strUsrName;
            base.mBuilder.ToUserName = this.mVoiceContext.mUserName;
            base.mBuilder.Offset = (uint) this.mVoiceContext.mNetOffset;
            base.mBuilder.Length = (uint) toSend.Length;
            base.mBuilder.ClientMsgId = this.mVoiceContext.mClientMsgId;
            base.mBuilder.MsgId = (uint) this.mVoiceContext.mMsgSrvId;
            base.mBuilder.VoiceLength = (uint) this.mVoiceContext.mVoiceTimeLength;
            base.mBuilder.VoiceFormat = (uint)this.mVoiceContext.EncodeType;
           // base.mBuilder.v
            base.mBuilder.Data = Util.toSKBuffer(toSend);
            uint num = 0;
            if (this.mVoiceContext.isEncodeAmrEnd() && ((this.mVoiceContext.mNetOffset + toSend.Length) >= this.mVoiceContext.mOutputLength))
            {
                num = 1;
            }
            base.mBuilder.EndFlag = num;
            if (this.mVoiceContext.mIsCancelled)
            {
                base.mBuilder.CancelFlag = 1;
                base.mBuilder.Data = Util.toSKBuffer("");
                base.mBuilder.Length = 0;
            }
            base.mSessionPack.mCmdID = 0x13;
            base.endBuilder();
            return true;
        }
        public bool doSceneVoiceEx(string toUsername, int mMsgSrvId, int timeLenth, byte[] toSend)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x13);
            base.mBuilder.FromUserName = AccountMgr.getCurAccount().strUsrName;
            base.mBuilder.ToUserName = toUsername;
            base.mBuilder.Offset = (uint)toSend.Length;
            base.mBuilder.Length = (uint)toSend.Length;
            base.mBuilder.ClientMsgId = MD5Core.GetHashString(toUsername + Util.getNowMilliseconds());
            base.mBuilder.MsgId = (uint)mMsgSrvId;
            base.mBuilder.VoiceLength = (uint)timeLenth;
            base.mBuilder.Data = Util.toSKBuffer(toSend);
            //uint num = 0;
            //if (this.mVoiceContext.isEncodeAmrEnd() && ((this.mVoiceContext.mNetOffset + toSend.Length) >= this.mVoiceContext.mOutputLength))
            //{
            //    num = 1;
            //}
            base.mBuilder.EndFlag = 1;// num;
            //if (this.mVoiceContext.mIsCancelled)
            //{
            //    base.mBuilder.CancelFlag = 1;
            //    base.mBuilder.Data = Util.toSKBuffer("");
            //    base.mBuilder.Length = 0;
            //}
            base.mSessionPack.mCmdID = 0x13;
            base.endBuilder();
            return true;
        }
        public void doSceneFinished(int finishType)
        {
            this.mTimerObject.stop();
            //mTimerObject.Close();
            //mTimerObject.Dispose();
            this.mTimerObject = null;
            if (finishType != 0)
            {
                this.mVoiceContext.onFinished(finishType);
                if (this.mOnSceneFinished != null)
                {
                    this.mOnSceneFinished(this.mVoiceContext);
                }
            }
            this.mVoiceContext.mScene = null;
            this.mVoiceContext = null;
        }

        protected override void onFailed(UploadVoiceRequest request, UploadVoiceResponse response)
        {
            Log.e("NetSceneUploadVoice", "upload Voice Block  failed ");
            if (this.mVoiceContext != null)
            {
                this.mVoiceContext.mStatus = 4;
                this.dispatchVoiceHandler();
            }
        }

        protected override void onSuccess(UploadVoiceRequest request, UploadVoiceResponse response)
        {
            if (this.mVoiceContext != null)
            {
                RetConst ret = (RetConst) response.BaseResponse.Ret;
                if (ret == RetConst.MM_OK)
                {
                    Log.d("NetSceneUploadVoice", "upload Voice Block success ");
                    this.mVoiceContext.mNetOffset += this.mVoiceContext.mSendingLength;
                    this.mVoiceContext.mMsgSrvId = (int) response.MsgId;
                    this.mVoiceContext.mSendingLength = 0;
                    this.mVoiceContext.mStatus = 1;
                }
                else
                {
                    Log.e("NetSceneUploadVoice", "upload Voice Block failed, ret =  " + ret);
                    this.mVoiceContext.mStatus = 4;
                    //if (ret == RetConst.MM_ERR_NEED_QQPWD)
                    //{
                    //    EventCenter.postEvent(EventConst.ON_NETSCENE_UPLOADVOICE_ERR, new UploadVoiceEventArgs(this.mVoiceContext.mChatMsg, ret), null);
                    //}
                }
                this.dispatchVoiceHandler();
            }
        }

        public static void onTimerHandler(object sender, EventArgs e)
        {
            NetSceneUploadVoice voice = TimerEventArgs.getObject(e) as NetSceneUploadVoice;
            if ((voice == null) || (voice.mVoiceContext == null))
            {
                Log.e("NetSceneUploadVoice", "onTimerHandler, invalid timer args");
            }
            else
            {
                voice.mVoiceContext.printfInfo();
                voice.dispatchVoiceHandler();
            }
        }
    }
}

