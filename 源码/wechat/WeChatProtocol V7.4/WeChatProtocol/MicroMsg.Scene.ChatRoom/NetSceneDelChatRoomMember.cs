namespace MicroMsg.Scene.ChatRoom
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using System;
    using System.Collections.Generic;

    public class NetSceneDelChatRoomMember : NetSceneBaseEx<DelChatRoomMemberRequest, DelChatRoomMemberResponse, DelChatRoomMemberRequest.Builder>
    {
        private const string TAG = "NetSceneDelChatRoomMember";

        public bool doScene(string chatRoomName, List<string> memberList)
        {
            if (((memberList == null) || (memberList.Count == 0)) || string.IsNullOrEmpty(chatRoomName))
            {
                Log.d("NetSceneDelChatRoomMember", "doScene,invalid input para");
                return false;
            }
            Log.i("NetSceneDelChatRoomMember", "doscene begin");
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.ChatRoomName = chatRoomName;
            foreach (string str in memberList)
            {
                base.mBuilder.MemberListList.Add(DelMemberReq.CreateBuilder().SetMemberName(Util.toSKString(str)).Build());
            }
            base.mBuilder.MemberCount = (uint) memberList.Count;
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/delchatroommember";
            base.mSessionPack.mConnectMode = 2;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(DelChatRoomMemberRequest request, DelChatRoomMemberResponse response)
        {
            Log.e("NetSceneDelChatRoomMember", "send AddChatRoomMemberRequest failed");
            NetSceneDelChatRoomMemberResult result = new NetSceneDelChatRoomMemberResult {
                retCode = RetConst.MM_ERR_CLIENT,
                sceneDelChatRoomMember = this
            };
            EventCenter.postEvent(EventConst.ON_NETSCENE_DEL_CHATROOM_MEMBER_ERR, result, null);
        }

        protected override void onSuccess(DelChatRoomMemberRequest request, DelChatRoomMemberResponse response)
        {
            Log.i("NetSceneDelChatRoomMember", "doscene respone sucess");
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            NetSceneDelChatRoomMemberResult result = new NetSceneDelChatRoomMemberResult {
                retCode = (RetConst) response.BaseResponse.Ret,
                sceneDelChatRoomMember = this,
                nameList = new List<string>()
            };
            for (int i = 0; i < response.MemberListList.Count; i++)
            {
                result.nameList.Add(response.MemberListList[i].MemberName.String);
            }
            if (result.retCode != RetConst.MM_OK)
            {
                Log.e("NetSceneDelChatRoomMember", "send AddChatRoomMemberRequest failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_DEL_CHATROOM_MEMBER_ERR, result, null);
            }
            else if (!ChatRoomMgr.delChatroomMember(request.ChatRoomName, response))
            {
                Log.e("NetSceneDelChatRoomMember", "delChatroomMember : fail");
                EventCenter.postEvent(EventConst.ON_NETSCENE_DEL_CHATROOM_MEMBER_ERR, result, null);
            }
            else
            {
                Log.i("NetSceneDelChatRoomMember", "doscene post sucess");
                EventCenter.postEvent(EventConst.ON_NETSCENE_DEL_CHATROOM_MEMBER_SUCCESS, result, null);
            }
        }
    }
}

