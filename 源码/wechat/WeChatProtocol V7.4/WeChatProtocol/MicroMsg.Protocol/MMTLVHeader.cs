namespace MicroMsg.Protocol
{
    using System;

    public class MMTLVHeader
    {
        public uint CertVersion;
        public ushort CmdId;
        public short CompressAlogrithm;
        public uint CompressedLen;
        public uint CompressLen;
        public short CompressVersion;
        public short CryptAlgorithm;
        public byte[] DeviceId;
        public ushort Dummy1;
        public ushort Dummy2;
        public int Ret;
        public byte[] ServerId;
        public uint Uin;

        public MMTLVHeader()
        {
            this.Reset();
        }

        ~MMTLVHeader()
        {
            this.Reset();
        }

        private void Reset()
        {
            this.Ret = 0;
            this.Uin = 0;
            this.CmdId = 0;
            this.Dummy1 = 0;
            this.ServerId = null;
            this.DeviceId = null;
            this.CompressVersion = 0;
            this.CompressAlogrithm = 0;
            this.CryptAlgorithm = 0;
            this.Dummy2 = 0;
            this.CompressLen = 0;
            this.CompressedLen = 0;
            this.CertVersion = 0;
        }
    }
}
