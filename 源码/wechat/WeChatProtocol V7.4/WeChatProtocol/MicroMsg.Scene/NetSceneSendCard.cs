namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using System;
    using System.Runtime.InteropServices;
    //Œﬁ”√
    public class NetSceneSendCard : NetSceneBaseEx<SendCardRequest, SendCardResponse, SendCardRequest.Builder>
    {
        private const string TAG = "NetSceneSendCard";
        private const uint USERINFO_QRCODE_FRIST_IN = 0x10402;
        private const uint USERINFO_QRCODE_NOW_STYLE = 0x10401;

        public bool doScene(string strContent, SendCardType type, uint style, string strContentEx = "")
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.UserName = AccountMgr.getCurAccount().strUsrName;
            base.mBuilder.Content = strContent;
            base.mBuilder.SendCardBitFlag = (uint) type;
            base.mBuilder.Style = style;
            if (type == SendCardType.MM_SENDCARD_READER)
            {
                base.mBuilder.ContentEx = strContentEx;
            }
            base.mSessionPack.mCmdID = 0x2a;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(SendCardRequest request, SendCardResponse response)
        {
            Log.e("NetSceneSendCard", "send SendCardRequest failed");
            RetConst @const = RetConst.MM_ERR_CLIENT;
            EventCenter.postEvent(EventConst.ON_NETSCENE_SEND_CARD_ERR, @const, null);
        }

        protected override void onSuccess(SendCardRequest request, SendCardResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneSendCard", "send SendCardRequest failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_SEND_CARD_ERR, ret, null);
            }
            else
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_SEND_CARD_SUCCESS, ret, null);
            }
        }
    }
}

