namespace MicroMsg.Plugin.Sns.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Runtime.InteropServices;

    public class NetSceneSnsTimeLine : NetSceneBaseEx<SnsTimeLineRequest, SnsTimeLineResponse, SnsTimeLineRequest.Builder>
    {
        private static NetSceneSnsTimeLine _mInstance;
        //private SnsTimeLineCallBack onTimeLineRsp;
        private const string TAG = "NetSceneSnsTimeLine";

        public NetSceneSnsTimeLine()
        {
        }

        //public NetSceneSnsTimeLine(SnsTimeLineCallBack cb)
        //{
        //    this.onTimeLineRsp = cb;
        //}

        public bool doScene(bool isFirstPage, ulong lastID)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            if (isFirstPage)
            {
                //SnsPage page = StorageMgr.snsPage.get(AccountMgr.strUsrName);
                //if (page == null)
                //{
                //    base.mBuilder.FirstPageMd5 = "";
                //    base.mBuilder.MaxId = 0L;
                //}
                //else
                //{
                //    base.mBuilder.FirstPageMd5 = page.strFirstPageMd5;
                //    base.mBuilder.MaxId = 0L;
                //}
            }
            else
            {
                base.mBuilder.FirstPageMd5 = "";
                base.mBuilder.MaxId = lastID;
            }
            base.mBuilder.MinFilterId = 0L;
            base.mBuilder.LastRequestTime = 0;
            base.mSessionPack.mCmdID = 0x62;
            base.endBuilder();
            return true;
        }

        public bool getFirstPage(string strFirstPageMd5 = null)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.FirstPageMd5 = (strFirstPageMd5 == null) ? "" : strFirstPageMd5;
            base.mBuilder.MaxId = 0L;
            base.mBuilder.MinFilterId = 0L;
            base.mBuilder.LastRequestTime = 0;
            base.mSessionPack.mCmdID = 0x62;
            base.endBuilder();
            return true;
        }

        public bool getNextPage(ulong maxID, ulong minID = 0L, uint LastRequestTime = 0)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.MaxId = maxID;
            base.mBuilder.MinFilterId = minID;
            base.mBuilder.LastRequestTime = LastRequestTime;
            base.mBuilder.FirstPageMd5 = "";
            base.mSessionPack.mCmdID = 0x62;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(SnsTimeLineRequest request, SnsTimeLineResponse response)
        {
            Log.e("NetSceneSnsTimeLine", "send request failed");
            //if (this.onTimeLineRsp != null)
            //{
            //    this.onTimeLineRsp(-1, null);
            //}
            //RetConst @const = RetConst.MM_ERR_CLIENT;
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_TIME_LINE_ERR, @const, null);
        }

        protected override void onSuccess(SnsTimeLineRequest request, SnsTimeLineResponse response)
        {
            SnsRetCode ret = (SnsRetCode) response.BaseResponse.Ret;
            switch (ret)
            {
                case SnsRetCode.MMSNS_MM_OK:
                case SnsRetCode.MMSNS_RET_ISALL:
                    //if (this.onTimeLineRsp != null)
                    //{
                    //    this.onTimeLineRsp((int) ret, response);
                    //}
                    if (response.ObjectListList.Count != 0)
                    {
                        //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_TIME_LINE_SUCCESS, ret, null);
                    }
                    return;
            }
            Log.e("NetSceneSnsTimeLine", "send request failed ret =" + ret);
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_TIME_LINE_ERR, ret, null);
            //if (this.onTimeLineRsp != null)
            //{
            //    this.onTimeLineRsp(-1, null);
            //}
        }

        public static NetSceneSnsTimeLine Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    _mInstance = new NetSceneSnsTimeLine();
                }
                return _mInstance;
            }
        }
    }
}

