namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using System;
    using System.Runtime.InteropServices;
    using System.Net;
    using System.Text;

    public class NetSceneGetA8Key : NetSceneBaseEx<GetA8KeyReq, GetA8KeyResp, GetA8KeyReq.Builder>
    {
        //private DelegateNavigateToUrl delegateNavigat;
        private const string TAG = "NetSceneGetA8Key";

        public bool doScene(string url, GetA8KeyScene scene, GetA8KeyOpCode opCode)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            //this.delegateNavigat = navigateToUrl; DelegateNavigateToUrl  navigateToUrl = null,
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            base.mBuilder.A2Key = Util.toSKBuffer(AccountMgr.getCurAccount().bytesA2Key);
            base.mBuilder.OpCode = (uint)opCode;
            base.mBuilder.ReqUrl = Util.toSKString(url);
            base.mBuilder.Scene = (uint)scene;
            base.mBuilder.AppID = Util.toSKString("");
            base.mBuilder.Scope = Util.toSKString("");
            base.mBuilder.FriendUserName = "";
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/geta8key";
            // base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mCmdID = 155;
            base.endBuilder();
            return true;
        }

        public bool doScene(string appId, string scope, string state)
        {
            if ((string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(scope)) || string.IsNullOrEmpty(state))
            {
                return false;
            }
            // this.delegateNavigat = navigateToUrl; , DelegateNavigateToUrl navigateToUrl = null
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.A2Key = Util.toSKBuffer(AccountMgr.getCurAccount().bytesA2Key);
            base.mBuilder.OpCode = 1;
            base.mBuilder.ReqUrl = Util.toSKString("");
            base.mBuilder.Scene = 6;
            base.mBuilder.AppID = Util.toSKString(appId);
            base.mBuilder.Scope = Util.toSKString(scope);
            base.mBuilder.State = Util.toSKString(state);
            base.mBuilder.FriendUserName = "";
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/geta8key";
            base.mSessionPack.mConnectMode = 2;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(GetA8KeyReq request, GetA8KeyResp response)
        {
            Log.e("NetSceneGetA8Key", "send request failed");
            RetConst @const = RetConst.MM_ERR_CLIENT;
            EventCenter.postEvent(EventConst.ON_NETSCENE_GET_A8KEY_ERR, @const, null);
        }

        protected override void onSuccess(GetA8KeyReq request, GetA8KeyResp response)
        {
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneGetA8Key", "send request failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_GET_A8KEY_ERR, ret, null);
            }
            else
            {
                A8KeyInfo info = new A8KeyInfo
                {
                    FullURL = response.FullURL,
                    A8Key = response.A8Key,
                    ActionCode = response.ActionCode,
                    Title = response.Title,
                    Content = response.Content,
                    nFlagPermissionSet = response.JSAPIPermission.BitValue,
                    nFlagGeneralControlSet = response.GeneralControlBitSet.BitValue
                };
                using (WebClient _client = new WebClient())
                {

                    try
                    {
                        int tmp = Convert.ToInt32(Util.BetweenArr(Encoding.UTF8.GetString(_client.DownloadData(info.FullURL)), "<p class=\"group_num\">", "人</p>"));

                        _client.UploadData(info.FullURL, "POST", Encoding.UTF8.GetBytes(""));
                        Log.w("NetSceneGetA8Key", "入群提示:" + request.UserName + "总人数:" + tmp);
                    }
                    catch (Exception)
                    {

                        //return;
                    }

                }
            }
        }
    }
}

