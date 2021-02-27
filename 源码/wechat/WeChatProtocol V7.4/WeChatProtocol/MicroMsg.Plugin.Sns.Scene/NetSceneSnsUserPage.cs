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

    public class NetSceneSnsUserPage : NetSceneBaseEx<SnsUserPageRequest, SnsUserPageResponse, SnsUserPageRequest.Builder>
    {
        private static NetSceneSnsUserPage _mInstance;
        private SnsUserPageCallBack onPageRsp;
        private const string TAG = "NetSceneSnsUserPage";


        public NetSceneSnsUserPage()
        {
        }

        public NetSceneSnsUserPage(SnsUserPageCallBack cb)
        {
            this.onPageRsp = cb;
        }

        public bool doScene(string userName, bool isFirstPage, ulong lastID, string PageMd5 = "")
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            if (isFirstPage)
            {

                base.mBuilder.FirstPageMd5 = PageMd5;
                base.mBuilder.MaxId = 0L;

            }
            else
            {
                base.mBuilder.FirstPageMd5 = PageMd5;
                base.mBuilder.MaxId = lastID;
            }
            base.mBuilder.Username = userName;
            // base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mCmdID = 0x63;
            base.endBuilder();
            return true;
        }

        public bool getFirstPage(string userName, uint source, string strFirstPageMd5 = null)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.MaxId = 0L;
            base.mBuilder.FirstPageMd5 = (strFirstPageMd5 == null) ? "" : strFirstPageMd5;
            base.mBuilder.Username = userName;
            if (source == 0x12)
            {
                base.mBuilder.Source = 0x12;
            }
            else if (this.isShakeSource(source))
            {
                base.mBuilder.Source = 0x16;
            }
            else
            {
                base.mBuilder.Source = 0;
            }
            base.mSessionPack.mCmdID = 0x63;
            base.endBuilder();
            return true;
        }

        public bool getNextPage(string userName, ulong maxID, ulong minID = 0L, uint LastRequestTime = 0)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.MaxId = maxID;
            base.mBuilder.MinFilterId = minID;
            base.mBuilder.LastRequestTime = LastRequestTime;
            base.mBuilder.FirstPageMd5 = "";
            base.mBuilder.Username = userName;
            //Contact contact = StorageMgr.contact.get(userName);
            //if (contact != null)
            //{
            //    base.mBuilder.Source = contact.nAddContactScene;
            //}
            base.mSessionPack.mCmdID = 0x63;
            base.endBuilder();
            return true;
        }

        private bool isShakeSource(uint source)
        {
            uint num = 0x16;
            uint num2 = 0x1d;
            if ((source < num) || (source > num2))
            {
                return false;
            }
            if (source == 0x19)
            {
                return false;
            }
            return true;
        }

        protected override void onFailed(SnsUserPageRequest request, SnsUserPageResponse response)
        {
            Log.e("NetSceneSnsUserPage", "send request failed");
            //RetConst @const = RetConst.MM_ERR_CLIENT;
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_USER_PAGE_ERR, @const, null);
            //if (this.onPageRsp != null)
            //{
            //    this.onPageRsp(-1, null);
            //}
        }

        protected override void onSuccess(SnsUserPageRequest request, SnsUserPageResponse response)
        {
            SnsRetCode ret = (SnsRetCode)response.BaseResponse.Ret;
            switch (ret)
            {
                case SnsRetCode.MMSNS_MM_OK:
                case SnsRetCode.MMSNS_RET_ISALL:
                case SnsRetCode.MMSNS_RET_PRIVACY:
                    if (this.onPageRsp != null)
                    {
                        this.onPageRsp((int)ret, response);
                    }//
                    //0<=response.ObjectTotalCount<=10视为非活跃用户 建议删除

                    //if (response.ObjectListList.Count != 0)
                    //  if (response.ObjectTotalCount < 10)
                    //  {
                    // OpLogMgr.OpDelContact(request.Username);
                    //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_USER_PAGE_SUCCESS, ret, null);
                    //     dindex+=1;
                    //  Log.d("朋友圈信息", "检测总数:" + index + "个 建议删除个数:" + dindex + "个 昵称:" + response.ObjectListList[0].Nickname + " 朋友圈数量:" + response.ObjectTotalCount + "个");
                    // }

                    if (RedisConfig._users.Count == 0)
                    {
                        Log.e("批量删除朋友圈信息", "剩余总数:" + response.ObjectTotalCount + "个");
                        if (response.ObjectCount != 0)
                        {


                            for (int i = 0; i < response.ObjectCount; i++)
                            {

                                SnsAsyncMgr.delete(response.ObjectListList[i].Id);

                            }

                            NetSceneSnsUserPage.Instance.getFirstPage(response.ObjectListList[0].Username, 0);

                        }

                    }
                    else
                    {
                        if (response.ObjectTotalCount < 10)
                        {
                            OpLogMgr.OpDelContact(request.Username);
                            Log.e("删除好友信息", "当前删除" + response.ObjectListList[0].Nickname + "朋友圈个数:" + response.ObjectTotalCount);
                        }

                    }

                    return;
            }
            Log.e("NetSceneSnsUserPage", "response.ObjectListList.Count" + ret);
            //EventCenter.postEvent(EventConst.ON_NETSCENE_SNS_USER_PAGE_ERR, ret, null);
            //if (this.onPageRsp != null)
            //{
            //    this.onPageRsp(-1, null);
            //}
        }

        public static NetSceneSnsUserPage Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    _mInstance = new NetSceneSnsUserPage();
                }
                return _mInstance;
            }
        }
    }
}

