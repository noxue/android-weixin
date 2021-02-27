namespace MicroMsg.Scene.ChatRoom
{
    using MicroMsg.Protocol;
    using System;

    public class NetSceneCreateChatRoomResult : EventArgs
    {
        public uint maxMemberCount;
        public uint requestMemberCount;
        public RetConst retCode;
        public NetSceneCreateChatRoom sceneCreateChatRoom;
    }
}

