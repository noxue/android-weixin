namespace MicroMsg.Plugin.Sns.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;

    public class NetSceneSnsDetail : NetSceneBaseEx<SnsObjectDetailRequest, SnsObjectDetailResponse, SnsObjectDetailRequest.Builder>
    {
        private ulong mObjectID;
        private const string TAG = "NetSceneSnsComment";

        public bool doScene(ulong objectID)
        {
            this.mObjectID = objectID;
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.Id = objectID;
            base.mSessionPack.mCmdID = 0x65;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(SnsObjectDetailRequest request, SnsObjectDetailResponse response)
        {
            Log.e("NetSceneSnsComment", "send request failed");
        }

        protected override void onSuccess(SnsObjectDetailRequest request, SnsObjectDetailResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneSnsComment", "send request failed ret =" + ret);
                if (RetConst.MM_ERR_ARG == ret)
                {
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_GETDETAIL_ERR, this.mObjectID, null);
                }
            }
            else
            {
                SnsInfoMgr.toSnsInfo(response.Object);



            }
        }
    }
}

