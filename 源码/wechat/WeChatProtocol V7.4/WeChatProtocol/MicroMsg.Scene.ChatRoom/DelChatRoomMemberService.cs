namespace MicroMsg.Scene.ChatRoom
{
    using System;
    using System.Collections.Generic;

    public class DelChatRoomMemberService
    {
        private const string TAG = "DelChatRoomMemberService";

        public bool doScene(string chatRoomName, List<string> memberList)
        {
            NetSceneDelChatRoomMember member = new NetSceneDelChatRoomMember();
            return member.doScene(chatRoomName, memberList);
        }
    }
}

