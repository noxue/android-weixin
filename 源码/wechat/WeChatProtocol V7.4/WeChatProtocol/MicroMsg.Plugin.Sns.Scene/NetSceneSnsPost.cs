namespace MicroMsg.Plugin.Sns.Scene
{
    using MicroMsg.Common.Algorithm;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;

    using micromsg;

    public class NetSceneSnsPost : NetSceneBaseEx<SnsPostRequest, SnsPostResponse, SnsPostRequest.Builder>
    {
        //public onSceneFinishedDelegate mOnSceneFinished;
        //private SnsPostContext mPostContext;
        private const string TAG = "NetSceneSnsPost";

        //public bool doScene(SnsPostContext context)
        //{
        //    this.mPostContext = context;
        //    base.beginBuilder();
        //    base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
        //    Util.ByteArrayToString(context.localSnsObject.bytesObjectDesc);
        //    base.mBuilder.ObjectDesc = Util.toSKBuffer(context.localSnsObject.bytesObjectDesc);
        //    if (((context.localSnsObject.withList != null) && (context.localSnsObject.withList.list != null)) && (context.localSnsObject.withList.list.Count > 0))
        //    {
        //        base.mBuilder.WithUserListNum = (uint) context.localSnsObject.withList.list.Count;
        //        foreach (SnsComment comment in context.localSnsObject.withList.list)
        //        {
        //            base.mBuilder.WithUserListList.Add(Util.toSKString(comment.strUserName));
        //        }
        //    }
        //    base.mBuilder.Privacy = (uint) context.privacyFlag;
        //    base.mBuilder.SyncFlag = context.sync2WeiboFlag;
        //    if (context.mSnsTrans == null)
        //    {
        //        if (context.objectType != 2)
        //        {
        //            return false;
        //        }
        //        Log.d("NetSceneSnsPost", "context.objectType = " + context.objectType.ToString());
        //        base.mBuilder.ClientId = MD5Core.GetHashString(Util.nullAsNil(context.strContent) + Util.getNowMilliseconds());
        //    }
        //    else
        //    {
        //        base.mBuilder.ClientId = Util.nullAsNil(context.mSnsTrans.strClientID);
        //    }
        //    base.mBuilder.PostBGImgType = (uint) context.bgImgType;
        //    if ((context.localSnsObject.BlackList != null) && (context.localSnsObject.BlackList.list.Count > 0))
        //    {
        //        foreach (string str in context.localSnsObject.BlackList.list)
        //        {
        //            base.mBuilder.BlackListList.Add(Util.toSKString(str));
        //        }
        //        base.mBuilder.BlackListNum = (uint) context.localSnsObject.BlackList.list.Count;
        //    }
        //    if ((context.localSnsObject.GroupUserList != null) && (context.localSnsObject.GroupUserList.list.Count > 0))
        //    {
        //        foreach (string str2 in context.localSnsObject.GroupUserList.list)
        //        {
        //            base.mBuilder.GroupUserList.Add(Util.toSKString(str2));
        //        }
        //        base.mBuilder.GroupUserNum = (uint) context.localSnsObject.GroupUserList.list.Count;
        //    }
        //    base.mSessionPack.mCmdID = 0x61;
        //    base.endBuilder();
        //    context.mSnsObjectStatus = 2;
        //    return true;
        //}

        public bool doScene(SnsInfo snsObj)
        {
            if ((snsObj == null) || (snsObj.bytesObjectDesc == null))
            {
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            //Util.ByteArrayToString(snsObj.bytesObjectDesc);
            base.mBuilder.ObjectDesc = Util.toSKBuffer(snsObj.bytesObjectDesc);
            if (((snsObj.withList != null) && (snsObj.withList.list != null)) && (snsObj.withList.list.Count > 0))
            {
                base.mBuilder.WithUserListNum = (uint) snsObj.withList.list.Count;
                foreach (SnsComment comment in snsObj.withList.list)
                {
                    base.mBuilder.WithUserListList.Add(Util.toSKString(comment.strUserName));
                }
            }
            base.mBuilder.Privacy = 0;
            base.mBuilder.SyncFlag = 0;
            base.mBuilder.ClientId = MD5Core.GetHashString(Util.getNowMilliseconds() + snsObj.bytesObjectDesc.ToString());
            base.mBuilder.PostBGImgType = 1;
            if ((snsObj.BlackList != null) && (snsObj.BlackList.list.Count > 0))
            {
                foreach (string str in snsObj.BlackList.list)
                {
                    base.mBuilder.BlackListList.Add(Util.toSKString(str));
                }
                base.mBuilder.BlackListNum = (uint) snsObj.BlackList.list.Count;
            }
            if ((snsObj.GroupUserList != null) && (snsObj.GroupUserList.list.Count > 0))
            {
                foreach (string str2 in snsObj.GroupUserList.list)
                {
                    base.mBuilder.GroupUserList.Add(Util.toSKString(str2));
                }
                base.mBuilder.GroupUserNum = (uint) snsObj.GroupUserList.list.Count;
            }
            base.mSessionPack.mCmdID = 0x61;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(SnsPostRequest request, SnsPostResponse response)
        {
            Log.e("NetSceneSnsPost", "send request failed");
            //RetConst @const = RetConst.MM_ERR_CLIENT;
            //if ((((this.mPostContext != null) && (this.mPostContext.localSnsObject != null)) && ((StorageMgr.snsInfo != null) && (this.mPostContext.mSnsTrans != null))) && (StorageMgr.snsTrans != null))
            //{
            //    this.mPostContext.localSnsObject.nStatus = 4;
            //   // StorageMgr.snsInfo.update(this.mPostContext.localSnsObject);
            //    this.mPostContext.mSnsObjectStatus = 4;
            //    this.mPostContext.mSnsTrans.nObjectstatus = this.mPostContext.mSnsObjectStatus;
            //    //StorageMgr.snsTrans.update(this.mPostContext.mSnsTrans);
            //}
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_POST_ERR, @const, null);
        }

        protected override void onSuccess(SnsPostRequest request, SnsPostResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneSnsPost", "send request failed ret =" + ret);

               // EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_POST_ERR, ret, null);
            }

            else
            {
                SnsInfo item = SnsInfoMgr.toSnsInfo(response.SnsObject);

                Log.d("NetSceneSnsPost", "post success bytesObjectDesc = " + Util.ByteArrayToString(item.bytesObjectDesc));

            }
        }
    }
}
