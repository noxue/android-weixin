namespace MicroMsg.Scene.ChatRoom
{
    using System;
    using System.Collections.Generic;

    public class CreateChatRoomService
    {
        private const string TAG = "CreateChatRoomService";

        public bool doScene(string topic, List<string> memberList)
        {
            NetSceneCreateChatRoom room = new NetSceneCreateChatRoom();
            return room.doScene(topic, memberList);
        }
    }
}

