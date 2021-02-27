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
    

    public class NetSceneAddChatRoomMember : NetSceneBaseEx<AddChatRoomMemberRequest, AddChatRoomMemberResponse, AddChatRoomMemberRequest.Builder>
    {
        private const string TAG = "NetSceneAddChatRoomMember";

        public NetSceneAddChatRoomMember()
        {
            this.invalidUsernameList = new List<string>();
            this.memeberBlackList = new List<string>();
            this.notExistUserList = new List<string>();
            this.verifyUserList = new List<string>();
        }

        public bool doScene(string chatRoomName, List<string> memberList)
        {
            if (((memberList == null) || (memberList.Count == 0)) || string.IsNullOrEmpty(chatRoomName))
            {
                Log.d("NetSceneAddChatRoomMember", "doScene,invalid input para");
                return false;
            }
            Log.i("NetSceneAddChatRoomMember", "doscene begin");
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x24, 369298705);
            base.mBuilder.ChatRoomName = Util.toSKString(chatRoomName);
            foreach (string str in memberList)
            {
                base.mBuilder.MemberListList.Add(MemberReq.CreateBuilder().SetMemberName(Util.toSKString(str)).Build());
            }
            base.mBuilder.MemberCount = (uint) memberList.Count;
            base.mSessionPack.mCmdID = 0x24;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(AddChatRoomMemberRequest request, AddChatRoomMemberResponse response)
        {
            Log.e("NetSceneAddChatRoomMember", "send AddChatRoomMemberRequest failed");
            NetSceneAddChatRoomMemberResult result = new NetSceneAddChatRoomMemberResult {
                retCode = RetConst.MM_ERR_CLIENT,
                sceneAddChatRoomMember = this
            };
            EventCenter.postEvent(EventConst.ON_NETSCENE_ADD_CHATROOM_MEMBER_ERR, result, null);
        }

        protected override void onSuccess(AddChatRoomMemberRequest request, AddChatRoomMemberResponse response)
        {
            Log.i("NetSceneAddChatRoomMember", "doscene respone sucess");
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            NetSceneAddChatRoomMemberResult result = new NetSceneAddChatRoomMemberResult {
                retCode = (RetConst) response.BaseResponse.Ret,
                sceneAddChatRoomMember = this
            };
            if (result.retCode == RetConst.MM_ERR_MEMBER_TOOMUCH)
            {
                Log.d("NetSceneAddChatRoomMember", "send AddChatRoomMemberRequest failed ret =" + ret);
                string str = response.BaseResponse.ErrMsg.String;
                try
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    if (strArray.Length == 3)
                    {
                        result.requestMemberCount = Convert.ToUInt32(strArray[1]);
                        result.maxMemberCount = Convert.ToUInt32(strArray[2]);
                    }
                    else
                    {
                        Log.e("NetSceneAddChatRoomMember", "get maxMemberCount from errmsg failed, maxMemberCount not exist in errmsg");
                    }
                }
                catch (Exception exception)
                {
                    Log.e("NetSceneAddChatRoomMember", "get maxMemberCount from errmsg failed" + exception);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_ADD_CHATROOM_MEMBER_ERR, result, null);
            }
            else if (result.retCode != RetConst.MM_OK)
            {
                Log.e("NetSceneAddChatRoomMember", "send AddChatRoomMemberRequest failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_ADD_CHATROOM_MEMBER_ERR, result, null);
            }
            else
            {
                result.retCode = ChatRoomMgr.checkMemberStatusFromMemberList(this.invalidUsernameList, this.memeberBlackList, this.notExistUserList, this.verifyUserList, response.MemberListList);
                if (result.retCode != RetConst.MM_OK)
                {
                    Log.e("NetSceneAddChatRoomMember", "send checkMemberStatusFromMemberList failed ret =" + result.retCode);
                    EventCenter.postEvent(EventConst.ON_NETSCENE_ADD_CHATROOM_MEMBER_ERR, result, null);
                }
                else if (!ChatRoomMgr.addChatroomMember(request.ChatRoomName.String, response, this.verifyUserList))
                {
                    Log.e("NetSceneAddChatRoomMember", "addChatroomMember : fail");
                    EventCenter.postEvent(EventConst.ON_NETSCENE_ADD_CHATROOM_MEMBER_ERR, result, null);
                }
                else
                {
                    Log.i("NetSceneAddChatRoomMember", "doscene post sucess");
                    EventCenter.postEvent(EventConst.ON_NETSCENE_ADD_CHATROOM_MEMBER_SUCCESS, result, null);
                }
            }
        }

        public List<string> invalidUsernameList { get; set; }

        public List<string> memeberBlackList { get; set; }

        public List<string> notExistUserList { get; set; }

        public List<string> verifyUserList { get; set; }
    }
}

