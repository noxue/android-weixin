namespace MicroMsg.Scene.ChatRoom
{

    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System.Collections.Generic;
    using micromsg;

    internal class NSInviteChatRoomMember : NetSceneBaseEx<InviteChatRoomMemberRequest, InviteChatRoomMemberResponse, InviteChatRoomMemberRequest.Builder>
    {
        private const string TAG = "NSInviteChatRoomMember";

        public bool doScene(string chatroomName, List<string> usernameList)
        {
            Log.i("NSInviteChatRoomMember", "doscene begin");
            if (string.IsNullOrEmpty(chatroomName) || (usernameList == null))
            {
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.SetChatRoomName(Util.toSKString(chatroomName));
            foreach (string str in usernameList)
            {
                base.mBuilder.MemberListList.Add(MemberReq.CreateBuilder().SetMemberName(Util.toSKString(str)).Build());
            }
            base.mBuilder.SetMemberCount((uint) usernameList.Count);
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/invitechatroommember";
            base.mSessionPack.mConnectMode = 2;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(InviteChatRoomMemberRequest request, InviteChatRoomMemberResponse response)
        {
            Log.e("NSInviteChatRoomMember", "onfailed ,ret =" + RetConst.MM_ERR_CLIENT);
            //base.postEventToUI(EventConst.ON_INVITE_CHATROOMMEMBER, false, null, 0);
        }

        protected override void onSuccess(InviteChatRoomMemberRequest request, InviteChatRoomMemberResponse response)
        {
            Log.d("NSInviteChatRoomMember", "response ret = " + response.BaseResponse.Ret);
            //base.postEventToUI(EventConst.ON_INVITE_CHATROOMMEMBER, true, response, 0);
        }
    }
}

