namespace MicroMsg.Storage
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using MongoDB.Attributes;

    public class UserData
    {
        public string strUsername;
        public string strNickname;
        public string roomid;
        public string roomname;
        public int textcount = 0;
        public int voicecount = 0;
        public int imgcount = 0;
        public int appcount = 0;
        public int invite = 0;
        public int signdays = 0;
        public int signdaybyday = 0;
        public int signtime = 1462407000;
        public int gamecoin = 0;
        public int gamecharm = 0;
        public int gameluckymoney = 0;
        public double money = 0.0;
        public int bank = 0;
        public int regtime = 1462407000;
        public int locktime = 1462407000;

    }
    public class ChatRoomInfo
    {



        [MongoAlias("_id")]
        public string strChatRoomid { get; set; }

        public string strChatRoomNickName;

        public string strOwner;

        public int nMemberCount;

        public bool isLive;
        /// <summary>
        /// ËùÊô»úÆ÷ÈËid
        /// </summary>
        public string strWxiUser;
    }
}

