namespace MicroMsg.Scene.ChatRoom
{
    using System;
    using System.Collections.Generic;

    public class AddChatRoomMemberService
    {
        private const string TAG = "AddChatRoomMemberService";

        public bool doScene(string chatRoomName, List<string> memberList)
        {
            NetSceneAddChatRoomMember member = new NetSceneAddChatRoomMember();
            return member.doScene(chatRoomName, memberList);
        }
    }
}

