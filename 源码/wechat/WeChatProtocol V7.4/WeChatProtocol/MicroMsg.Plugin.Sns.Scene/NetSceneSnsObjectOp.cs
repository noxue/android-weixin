namespace MicroMsg.Plugin.Sns.Scene
{
    using micromsg;
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;
    using System.Collections.Generic;

    public class NetSceneSnsObjectOp : NetSceneBaseEx<SnsObjectOpRequest, SnsObjectOpResponse, SnsObjectOpRequest.Builder>
    {
        private static SKBuiltinBuffer_t emptyCommentID = Util.toSKBuffer("");
        private NetSceneSnsObjectOpCallBack onOpRsp;
        private const string TAG = "NetSceneSnsObjectOp";

        public NetSceneSnsObjectOp()
        {
        }

        public NetSceneSnsObjectOp(NetSceneSnsObjectOpCallBack cb)
        {
            this.onOpRsp = cb;
        }

        public static string createOpID()
        {
            return MD5Core.GetHashString(AccountMgr.getCurAccount().strUsrName + Util.getNowMilliseconds());
        }

        public bool doScene(List<SnsOpLog> opList)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 0x64000031);
            foreach (SnsOpLog log in opList)
            {
                SnsObjectOp.Builder builder = SnsObjectOp.CreateBuilder();
                builder.Id = log.nObjectID;
                builder.OpType = (uint) log.nOpType;
                builder.Ext = (log.nDelCommentId == 0) ? emptyCommentID : Util.toSKBuffer(SnsObjectOpDeleteComment.CreateBuilder().SetCommentId(log.nDelCommentId).Build().ToByteArray());
                base.mBuilder.OpListList.Add(builder.Build());
            }
            base.mBuilder.OpCount = (uint) base.mBuilder.OpListList.Count;
            base.mSessionPack.mCmdID = 0x68;
            return base.endBuilder();
        }

        protected override void onFailed(SnsObjectOpRequest request, SnsObjectOpResponse response)
        {
            Log.e("NetSceneSnsObjectOp", "send request failed");
            if (this.onOpRsp != null)
            {
                this.onOpRsp(null);
            }
            EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_OPLOG_ERR, request.OpListList[0].OpType, null);
        }

        protected override void onSuccess(SnsObjectOpRequest request, SnsObjectOpResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneSnsObjectOp", "send request failed ret =" + ret);
                if (this.onOpRsp != null)
                {
                    this.onOpRsp(null);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_OPLOG_ERR, request.OpListList[0].OpType, null);
            }
            else
            {
                if (this.onOpRsp != null)
                {
                    this.onOpRsp(response.OpRetListList);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_OPLOG_SUCCESS, request.OpListList[0].OpType, null);
            }
        }
    }
}

