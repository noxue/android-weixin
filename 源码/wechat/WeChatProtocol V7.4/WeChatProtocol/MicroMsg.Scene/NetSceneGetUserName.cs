namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using MicroMsg.Protocol;
    using System;

    public class NetSceneGetUserName : NetSceneBaseEx<GetUserNameRequest, GetUserNameResponse, GetUserNameRequest.Builder>
    {
        public static string mRegNickName;
        public static string mRepUserName;
        private const string TAG = "NetSceneGetUserName";

        public void doScene(string nickName)
        {
            base.beginBuilder();
            mRegNickName = nickName;
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x21);
            base.mBuilder.Pwd = Util.NullAsNil(NetSceneAuth.mAuthUserPwdMD5);
            base.mBuilder.NickName = Util.NullAsNil(nickName);
            base.mBuilder.BindUin = (uint) Util.stringToInt(NetSceneAuth.mAuthUserName);
            base.mBuilder.Ticket = Util.NullAsNil(SessionPackMgr.mAuthTicket1);
            SessionPackMgr.getAccount().setNickname(nickName);
            base.mSessionPack.mCmdID = 0x21;
            base.endBuilder();
        }

        protected override void onFailed(GetUserNameRequest request, GetUserNameResponse response)
        {
            EventCenter.postEvent(EventConst.ON_NETSCENE_FILLUSERNAME_ERR, -800000, null);
        }

        protected override void onSuccess(GetUserNameRequest request, GetUserNameResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_FILLUSERNAME_ERR, ret, null);
            }
            else
            {
                mRepUserName = response.UserName;
                EventCenter.postEvent(EventConst.ON_NETSCENE_FILLUSERNAME_SUCCESS, null, null);
            }
        }
    }
}

