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
    using MicroMsg.Common.Algorithm;

    public class NetSceneUploadCDNImage : NetSceneBaseEx<UploadMsgImgRequest, UploadMsgImgResponse, UploadMsgImgRequest.Builder>
    {


        private const string TAG = "NetSceneUploadCdnImage";



        public void doSceneToCGI(string msgid, CImgMsgContext imgContext, string toUsername)
        {





            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.FromUserName = Util.toSKString(AccountMgr.curUserName);
            base.mBuilder.ToUserName = Util.toSKString(toUsername);
            base.mBuilder.ClientImgId = Util.toSKString(msgid);
            base.mBuilder.MsgType = 3;
            base.mBuilder.TotalLen = (uint)imgContext.length;
            base.mBuilder.StartPos = 0;
            base.mBuilder.DataLen = (uint)imgContext.length;
            base.mBuilder.Data = Util.toSKBuffer(new byte[0]);
            base.mBuilder.EncryVer = 1;
            base.mBuilder.AESKey = imgContext.aesKey;
            base.mBuilder.CDNThumbAESKey = imgContext.aesKey;

            base.mBuilder.CompressType = 0;
            base.mBuilder.CDNBigImgSize = 0;
            base.mBuilder.CDNBigImgUrl = "";

            if (imgContext.hdlength != 0)
            {
                base.mBuilder.CDNMidImgSize = imgContext.hdlength;
            }
            else
            {

                base.mBuilder.CDNMidImgSize = imgContext.length;
            }

            base.mBuilder.CDNMidImgUrl = imgContext.midUrlKey;
            base.mBuilder.CDNThumbImgSize = imgContext.thumblength;
            base.mBuilder.CDNThumbImgUrl = imgContext.thumbUrlKey;

            base.mBuilder.CDNThumbImgWidth = imgContext.CDNThumbImgWidth;
            base.mBuilder.CDNThumbImgHeight = imgContext.CDNThumbImgHeight;
            base.mSessionPack.mCmdID = 9;
            base.mSessionPack.mNeedCompress = false;
            base.endBuilder();

        }


        protected override void onFailed(UploadMsgImgRequest request, UploadMsgImgResponse response)
        {
            Log.e("NetSceneUploadCdnImage", "Send image failed because of system error");

        }

        protected override void onSuccess(UploadMsgImgRequest request, UploadMsgImgResponse response)
        {
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            //string aa = Util.byteToHexStr(request.ToByteArray());
            switch (ret)
            {
                case RetConst.MM_OK:


                    Log.i("NetSceneUploadCdnImage", "send Complete!");

                    return;

                case RetConst.MM_ERR_NEED_QQPWD:
                    Log.i("NetSceneUploadCdnImage", "send QQ IMG ERR_NEED_QQPWD");
                    return;
            }
            Log.e("NetSceneUploadCdnImage", "Send image failed because of net error ret = " + ret.ToString());

        }

    }
}

