namespace MicroMsg.Protocol
{
    using System;

    public class MMPKG_mini_header
    {
        public ushort cert_version;
        public ushort cmd_id;
        public byte compress_algo;
        public uint compress_len;
        public uint compressed_len;
        public ushort device_type;
        public byte encrypt_algo;
        public byte len;
        public int ret;
        public byte[] server_id;
        public byte server_id_len;
        public uint uin;

        public byte[] getHeaderBits()
        {
            return new byte[] { ((byte) ((this.len << 2) + this.compress_algo)), ((byte) ((this.encrypt_algo << 4) + this.server_id_len)) };
        }

        public uint getMaxSize()
        {
            return 0x27;
        }

        public void setHHeaderBits(byte[] srcbuf)
        {
            ushort num = srcbuf[0];
            this.len = (byte) (num >> 2);
            this.compress_algo = (byte) (num & 3);
            ushort num2 = srcbuf[1];
            this.encrypt_algo = (byte) (num2 >> 4);
            this.server_id_len = (byte) (num2 & 15);
        }
    }
}
