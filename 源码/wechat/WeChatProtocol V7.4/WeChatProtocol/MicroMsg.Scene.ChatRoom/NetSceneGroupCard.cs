namespace MicroMsg.Scene.ChatRoom
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;

    public class NetSceneGroupCard : NetSceneBaseEx<GroupCardRequest, GroupCardResponse, GroupCardRequest.Builder>
    {
        private const uint MM_GROUPCARD_ADD = 1;
        private const string TAG = "NetSceneGroupCard";

        public bool doScene(string groupNickName, string groupUserName, List<Contact> contactList)
        {
            if (((contactList == null) || (contactList.Count == 0)) || (string.IsNullOrEmpty(groupNickName) || string.IsNullOrEmpty(groupUserName)))
            {
                Log.d("NetSceneGroupCard", "doScene,invalid input para");
                return false;
            }
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.OpCode = 1;
            base.mBuilder.GroupNickName = groupNickName;
            base.mBuilder.GroupUserName = groupUserName;
            foreach (Contact contact in contactList)
            {
                base.mBuilder.MemberListList.Add(RoomInfo.CreateBuilder().SetUserName(Util.toSKString(contact.strUsrName)).SetNickName(Util.toSKString(contact.strNickName)).Build());
            }
            base.mBuilder.MemberCount = (uint) contactList.Count;
            base.mSessionPack.mConnectMode = 2;
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/addgroupcard";
            base.endBuilder();
            return true;
        }

        protected override void onFailed(GroupCardRequest request, GroupCardResponse response)
        {
            Log.e("NetSceneGroupCard", "send NetSceneGroupCard failed");
            EventCenter.postEvent(EventConst.ON_NETSCENE_CHATROOM_GROUPCARD_ERR, null, null);
        }

        protected override void onSuccess(GroupCardRequest request, GroupCardResponse response)
        {
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneGroupCard", "send NetSceneGroupCard failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_CHATROOM_GROUPCARD_ERR, null, null);
            }
            else if (!ChatRoomMgr.addGroupCardContact(request, response))
            {
                Log.e("NetSceneGroupCard", "send addGroupCardContact failed ");
                EventCenter.postEvent(EventConst.ON_NETSCENE_CHATROOM_GROUPCARD_ERR, null, null);
            }
            else
            {
                EventCenter.postEvent(EventConst.ON_NETSCENE_CHATROOM_GROUPCARD_SUCCESS, null, null);
            }
        }
    }
}

