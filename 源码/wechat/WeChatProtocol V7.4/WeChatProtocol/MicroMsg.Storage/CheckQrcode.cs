using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatProtocol.MicroMsg.Storage
{
    class CheckQrcode
    {

        private uint _Status;
        private string _Username;
        private string _Password;
        private string _HeadImgUrl;
        private string _Nickname;
        private uint _ExpiredTime;
        private string _Uuid;

        public uint Status { get => _Status; set => _Status = value; }
        public string Username { get => _Username; set => _Username = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string HeadImgUrl { get => _HeadImgUrl; set => _HeadImgUrl = value; }
        public string Nickname { get => _Nickname; set => _Nickname = value; }
        public uint ExpiredTime { get => _ExpiredTime; set => _ExpiredTime = value; }
        public string Uuid { get => _Uuid; set => _Uuid = value; }
    }
}
