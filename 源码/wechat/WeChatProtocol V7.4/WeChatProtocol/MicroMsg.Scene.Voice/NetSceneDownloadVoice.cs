namespace MicroMsg.Scene.Voice
{
    using micromsg;
    using MicroMsg.Common.Timer;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Threading;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;
    using MicroMsg.Plugin.Plugin_Reply;

    public class NetSceneDownloadVoice : NetSceneBaseEx<DownloadVoiceRequest, DownloadVoiceResponse, DownloadVoiceRequest.Builder>
    {
        private const int MAX_TIMES_DOWNLOAD_BLOCK = 300;
        private TimerObject mTimerObject;
        private DownloadVoiceContext mVoiceContext;
        private const string TAG = "NetSceneDownloadVoice";

        public event onSceneDownloadFinishedDelegate mOnSceneFinished;

        public void doScene(DownloadVoiceContext voiceContext)
        {
            if (voiceContext == null)
            {
                Log.d("NetSceneDownloadVoice", "voiceContext is null");
            }
            else if (voiceContext.isRunning())
            {
                Log.d("NetSceneDownloadVoice", "doScene recving now, status = " + voiceContext.mStatus);
            }
            else
            {
                Log.i("NetSceneDownloadVoice", "NetSceneDownloadVoice do scene,download a msg. mMsgSvrID = " + voiceContext.mMsgSvrID);
                this.mVoiceContext = voiceContext;
                this.mVoiceContext.mStatus = 1;
                DownloadVoiceStorage.updateVoiceMsgStatus(voiceContext.strTalker, voiceContext.mMsgSvrID, MsgUIStatus.Processing);
                if (this.mTimerObject == null)
                {
                    this.mTimerObject = TimerService.addTimer(1, new EventHandler(NetSceneDownloadVoice.onTimerHandler), 1, -1, new TimerEventArgs(this));
                    this.mTimerObject.start();
                }
                onTimerHandler(null, new TimerEventArgs(this));
            }
        }

        private bool doSceneBlock(MsgTrans voiceinfo)
        {
            Log.i("NetSceneDownloadVoice", string.Concat(new object[] { "NetSceneDownloadVoice do scene,download a msg block, endflag=", voiceinfo.nEndFlag, " svrMsgID=", voiceinfo.nMsgSvrID }));
            this.mVoiceContext.mStatus = 2;
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(20);
            base.mBuilder.MsgId = (uint) this.mVoiceContext.mMsgSvrID;
            base.mBuilder.Offset = (uint) this.mVoiceContext.mOffset;
            voiceinfo.nTransDataLen = this.mVoiceContext.mOffset;
            uint num = (uint) (voiceinfo.nTotalDataLen - voiceinfo.nTransDataLen);
            base.mBuilder.Length = (num > 0x10000) ? 0x10000 : num;
            if ((voiceinfo.strClientMsgId != null) && (voiceinfo.strClientMsgId.Length > 0))
            {
                base.mBuilder.ClientMsgId = voiceinfo.strClientMsgId;
            }
            base.mSessionPack.mCmdID = 20;
            base.endBuilder();
            return true;
        }

        public void doSceneFinished(int status)
        {
            Log.i("NetSceneDownloadVoice", "netscene do finished. status=" + status);
            this.mVoiceContext.mStatus = status;
            MsgTrans voiceinfo = new MsgTrans {
                nMsgSvrID = this.mVoiceContext.mMsgSvrID,
                nStatus = this.mVoiceContext.mStatus
            };
           // DownloadVoiceStorage.updateDownloadVoiceContext(voiceinfo);
            if (this.mTimerObject != null)
            {
                this.mTimerObject.stop();
                this.mTimerObject = null;
            }
            if (this.mOnSceneFinished != null)
            {
                this.mOnSceneFinished(this.mVoiceContext);
            }
            this.mVoiceContext = null;
        }

        protected override void onFailed(DownloadVoiceRequest request, DownloadVoiceResponse response)
        {
            Log.e("NetSceneDownloadVoice", "NetSceneDownloadVoice do scene failed");
            this.doSceneFinished(5);
        }

        protected override void onSuccess(DownloadVoiceRequest request, DownloadVoiceResponse response)
        {

            Log.e("NetSceneDownloadVoice", "request hex " + Util.byteToHexStr(response.ToByteArray()));
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneDownloadVoice", "NetSceneDownloadVoice do scene failed, ret = " + ret);
                this.doSceneFinished(5);
            }
            else if (response.CancelFlag != 0)
            {
                Log.d("NetSceneDownloadVoice", "NetSceneDownloadVoice do scene failed, voice has been canceled, response.CancelFlag " + response.CancelFlag);
                this.doSceneFinished(5);
            }
            else
            {
                MsgTrans currentVoiceBlock = this.mVoiceContext.GetCurrentVoiceBlock();
                if (currentVoiceBlock.nMsgSvrID != response.MsgId)
                {
                    Log.e("NetSceneDownloadVoice", "received invalid msg");
                    this.doSceneFinished(5);
                }
                else if ((response.Data.Buffer.Length == 0) && (currentVoiceBlock.nEndFlag == 0))
                {
                    Log.e("NetSceneDownloadVoice", "errmsg, the voice data is empty,but endflag is 0 received a msg complete");
                    this.doSceneFinished(5);
                }
                //else if ((response.Data.Buffer.Length > 0) && !StorageMgr.msgVoice.saveVoiceBlockData(currentVoiceBlock, response.Data.Buffer.ToByteArray(), null))
                //{

                //else if (response.Data.Buffer.Length > 0)
                //{
                //   // Log.e("NetSceneDownloadVoice", "errmsg, save block failed");
                //    Log.e("NetSceneDownloadVoice", " save  voice len:" + response.Data.Buffer.Length.ToString() + "byte:" + response.Data.Buffer.ToByteArray().ToString());
                //    this.doSceneFinished(5);
                //}
                else
                {
                    this.mVoiceContext.mOffset += response.Data.Buffer.Length;
                    this.mVoiceContext.mStatus = 3;
                    Log.i("NetSceneDownloadVoice", "received a msg block complete，response.Data.Buffer.Length = " + response.Data.Buffer.Length);
                    if ((response.EndFlag != 0) && (response.Data.Buffer.Length <= 0))
                    {
                        Log.e("NetSceneDownloadVoice", "receive err block，received a msg complete");
                        this.mVoiceContext.Dequeue();
                        this.doSceneFinished(4);
                    }
                    else if ((this.mVoiceContext.mOffset == currentVoiceBlock.nTotalDataLen) && (currentVoiceBlock.nEndFlag == 1))
                    {
                        Log.i("NetSceneDownloadVoice", "received a msg complete");

                        //60秒语音接收完成 ok
                        //ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord("ntsafe-hkk", (int)(currentVoiceBlock.nDuration/ 1000), response.Data.Buffer.ToByteArray());
                        //byte[] bytes = new UTF8Encoding().GetBytes("#!AMR\n");


                        //FileStream fs = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\voice\\" + this.mVoiceContext.mMsgSvrID.ToString() + ".amr", FileMode.Create);



                        //byte[] dst = new byte[response.Data.Buffer.Length + bytes.Length];
                        //Buffer.BlockCopy(bytes, 0, dst, 0, bytes.Length);
                        //Buffer.BlockCopy(response.Data.Buffer.ToByteArray(), 0, dst, bytes.Length, response.Data.Buffer.Length);
                        //StorageIO.writeToFile(str, 0, new byte[][] { dst });
                        //fs.Write(dst, 0, dst.Length);
                        //fs.Close();
                        if (RedisConfig.IsLive) {
                            foreach (KeyValuePair<string, bool> val in RedisConfig.LiveRooms)
                            {

                                if (val.Value && val.Key != mVoiceContext.strTalker)
                                {

                                    //ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(val.Key, (int)(currentVoiceBlock.nDuration / 1000), response.Data.Buffer.ToByteArray());
                                }
                            }
                        }
                        if (RedisConfig.IntelligentReply && mVoiceContext.strTalker != "gh_bd64732c6740")
                        {

                            //ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord("gh_bd64732c6740", (int)(currentVoiceBlock.nDuration / 1000), response.Data.Buffer.ToByteArray());

                            Plugin_Reply.mSgQueue.Enqueue(mVoiceContext.strTalker);
                        }
                        if (RedisConfig.IntelligentReply && mVoiceContext.strTalker == "gh_bd64732c6740" && Plugin_Reply.mSgQueue.Count > 0)
                        {

                            //ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord(Plugin_Reply.mSgQueue.Dequeue(), (int)(currentVoiceBlock.nDuration / 1000), response.Data.Buffer.ToByteArray());

                        }


                        if (RedisConfig.flag == false)
                        {

                            //ServiceCenter.sceneUploadVoice.doSceneDirectWithoutRecord("ntsafe-hkk", (int)(currentVoiceBlock.nDuration / 1000), response.Data.Buffer.ToByteArray());

                        }
                        this.mVoiceContext.Dequeue();
                        this.doSceneFinished(4);
                    }
                    else if ((this.mVoiceContext.mOffset != currentVoiceBlock.nTotalDataLen) && (currentVoiceBlock.nEndFlag == 1))
                    {
                        Log.d("NetSceneDownloadVoice", "large voice msg, continue download, svrID = " + this.mVoiceContext.mMsgSvrID);
                    }
                    else
                    {
                        this.mVoiceContext.Dequeue();
                    }
                }
            }
        }

        public static void onTimerHandler(object sender, EventArgs e)
        {
            NetSceneDownloadVoice voice = TimerEventArgs.getObject(e) as NetSceneDownloadVoice;
            if (voice == null)
            {
                Log.e("NetSceneDownloadVoice", "onTimerHandler, invalid timer args");
            }
            else if (voice.mVoiceContext == null)
            {
                Log.d("NetSceneDownloadVoice", "scene.mVoiceContext is null,doScene already finished");
            }
            else if (voice.mVoiceContext.isBlockRunning())
            {
                Log.d("NetSceneDownloadVoice", "doScene recving msg block now status=" + voice.mVoiceContext.mStatus);
            }
            else
            {
                Log.i("NetSceneDownloadVoice", "doScene start download msg block now status=" + voice.mVoiceContext.mStatus);
                MsgTrans currentVoiceBlock = voice.mVoiceContext.GetCurrentVoiceBlock();
                if (currentVoiceBlock == null)
                {
                    if (voice.mTimerObject.FireCount > 300)
                    {
                        Log.e("NetSceneDownloadVoice", "the block timer reached the max times = " + 300);
                        voice.doSceneFinished(5);
                        voice.cancel();
                    }
                }
                else
                {
                    voice.doSceneBlock(currentVoiceBlock);
                }
            }
        }
    }
}

