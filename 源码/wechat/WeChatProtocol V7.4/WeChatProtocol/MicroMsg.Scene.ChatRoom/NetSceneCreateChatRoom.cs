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
    

    public class NetSceneCreateChatRoom : NetSceneBaseEx<CreateChatRoomRequest, CreateChatRoomResponse, CreateChatRoomRequest.Builder>
    {
        private const string TAG = "NetSceneCreateChatRoom";

        public NetSceneCreateChatRoom()
        {
            this.invalidUsernameList = new List<string>();
            this.memeberBlackList = new List<string>();
            this.notExistUserList = new List<string>();
            this.verifyUserList = new List<string>();
            this.chatRoomName = "";
        }

        public bool doScene(string topic, List<string> memberList)
        {
            if (((memberList == null) || (memberList.Count == 0)) || string.IsNullOrEmpty(topic))
            {
                Log.d("NetSceneCreateChatRoom", "doScene,invalid input para");
                return false;
            }
            Log.i("NetSceneCreateChatRoom", "doscene begin");
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0x25);
            base.mBuilder.Topic = Util.toSKString(topic);
            foreach (string str in memberList)
            {
                base.mBuilder.MemberListList.Add(MemberReq.CreateBuilder().SetMemberName(Util.toSKString(str)).Build());
            }
            base.mBuilder.MemberCount = (uint) memberList.Count;
            base.mSessionPack.mCmdID = 0x25;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(CreateChatRoomRequest request, CreateChatRoomResponse response)
        {
            Log.e("NetSceneCreateChatRoom", "send CreateChatRoomRequest failed");
            NetSceneCreateChatRoomResult result = new NetSceneCreateChatRoomResult {
                retCode = RetConst.MM_ERR_CLIENT,
                sceneCreateChatRoom = this
            };
            EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_ERR, result, null);
        }

        protected override void onSuccess(CreateChatRoomRequest request, CreateChatRoomResponse response)
        {
            Log.i("NetSceneCreateChatRoom", "doscene respone sucess");
            NetSceneCreateChatRoomResult result = new NetSceneCreateChatRoomResult();
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            result.retCode = ret;
            result.sceneCreateChatRoom = this;
            if (result.retCode == RetConst.MM_ERR_MEMBER_TOOMUCH)
            {
                Log.d("NetSceneCreateChatRoom", "send NetSceneCreateChatRoom failed ret =" + ret);
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
                        Log.e("NetSceneCreateChatRoom", "get maxMemberCount from errmsg failed, maxMemberCount not exist in errmsg");
                    }
                }
                catch (Exception exception)
                {
                    Log.e("NetSceneCreateChatRoom", "get maxMemberCount from errmsg failed" + exception);
                }
                EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_ERR, result, null);
            }
            else if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneCreateChatRoom", "send NetSceneCreateChatRoom failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_ERR, result, null);
            }
            else
            {
                this.chatRoomName = response.ChatRoomName.String;
                result.retCode = ChatRoomMgr.checkMemberStatusFromMemberList(this.invalidUsernameList, this.memeberBlackList, this.notExistUserList, this.verifyUserList, response.MemberListList);
                if (result.retCode != RetConst.MM_OK)
                {
                    Log.e("NetSceneCreateChatRoom", "send checkMemberStatusFromMemberList failed ret =" + result.retCode);
                    EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_ERR, result, null);
                }
                else if (string.IsNullOrWhiteSpace(this.chatRoomName) || (response.MemberListList.Count == this.verifyUserList.Count))
                {
                    Log.e("NetSceneCreateChatRoom", string.Concat(new object[] { "createChatroom failed ret =", result.retCode, " verifyUserList.Count = ", this.verifyUserList.Count }));
                    EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_ERR, result, null);
                }
                else if (!ChatRoomMgr.createChatroom(response, this.verifyUserList))
                {
                    Log.e("NetSceneCreateChatRoom", "createChatroom : fail");
                    EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_ERR, result, null);
                }
                else
                {
                    Log.i("NetSceneCreateChatRoom", "doscene post sucess");
                    EventCenter.postEvent(EventConst.ON_NETSCENE_CREATE_CHATROOM_SUCCESS, result, null);
                }
            }
        }

        public string chatRoomName { get; set; }

        public List<string> invalidUsernameList { get; set; }

        public List<string> memeberBlackList { get; set; }

        public List<string> notExistUserList { get; set; }

        public List<string> verifyUserList { get; set; }
    }
}

