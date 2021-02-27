namespace MicroMsg.Scene.ChatRoom
{
    using System;
    using System.Collections.Generic;


    public class GroupCardService
    {
        private const string TAG = "GroupCardService";

        public bool doScene(string groupNickName, string groupUserName, List<MicroMsg.Storage.Contact> contactList)
        {
            NetSceneGroupCard card = new NetSceneGroupCard();
            return card.doScene(groupNickName, groupUserName, contactList);
        }
    }
}

