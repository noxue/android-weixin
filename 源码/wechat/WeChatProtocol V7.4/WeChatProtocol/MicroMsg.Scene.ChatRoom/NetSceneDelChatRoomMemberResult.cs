namespace MicroMsg.Scene.ChatRoom
{
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;

    public class NetSceneDelChatRoomMemberResult : EventArgs
    {
        public List<string> nameList;
        public RetConst retCode;
        public NetSceneDelChatRoomMember sceneDelChatRoomMember;
    }
}

