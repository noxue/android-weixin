namespace MicroMsg.Scene.ChatRoom
{
    using MicroMsg.Protocol;
    using System;

    public class NetSceneAddChatRoomMemberResult : EventArgs
    {
        public uint maxMemberCount;
        public uint requestMemberCount;
        public RetConst retCode;
        public NetSceneAddChatRoomMember sceneAddChatRoomMember;
    }
}

