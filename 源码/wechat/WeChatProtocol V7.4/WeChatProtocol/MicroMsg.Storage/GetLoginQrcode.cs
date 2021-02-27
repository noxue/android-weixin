using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatProtocol.MicroMsg.Storage
{
    public class GetLoginQrcode
    {

        private byte[] _imgBuf;
        private byte[] notifyKey;
        private uint checkTime;
        private string uuid;
        private uint expiredTime;

        public byte[] ImgBuf { get => _imgBuf; set => _imgBuf = value; }
        public uint CheckTime { get => checkTime; set => checkTime = value; }
        public string Uuid { get => uuid; set => uuid = value; }
        public uint ExpiredTime { get => expiredTime; set => expiredTime = value; }
        public byte[] NotifyKey { get => notifyKey; set => notifyKey = value; }
    }
}
