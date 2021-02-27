namespace MicroMsg.Network
{
    using MicroMsg.Common.Utils;
    using System;

    public class NetPack
    {
        public class Header
        {
            public short headLen = 0x10;
            public int opCmd;
            public int seq;
            public int totalLen;
            public short version = 1;

            public Header(int seq, int dataLen, int opCmd)
            {
                this.totalLen = dataLen + this.headLen;
                this.opCmd = opCmd;
                this.seq = seq;
            }
        }

        public class Request
        {
            public byte[] body;
            public NetPack.Header head;

            public Request(int seq, int cmd, byte[] data)
            {
                this.head = new NetPack.Header(seq, data.Length, cmd);
                this.body = data;
            }

            public byte[] getBody()
            {
                return this.body;
            }

            public NetPack.Header getHead()
            {
                return this.head;
            }

            public int getLength()
            {
                return this.head.totalLen;
            }

            public int serializeToBuffer(ref byte[] outBuf)
            {
                outBuf = new byte[this.head.totalLen];
                int offset = 0;
                Util.writeInt(this.head.totalLen, ref outBuf, ref offset);
                Util.writeShort(this.head.headLen, ref outBuf, ref offset);
                Util.writeShort(this.head.version, ref outBuf, ref offset);
                Util.writeInt(this.head.opCmd, ref outBuf, ref offset);
                Util.writeInt(this.head.seq, ref outBuf, ref offset);
                Util.writeBuffer(this.body, ref outBuf, ref offset);
                return this.head.totalLen;
            }
        }

        public class Response
        {
            public byte[] body;
            public NetPack.Header head;

            public Response()
            {
                this.head = new NetPack.Header(0, 0, 0);
                this.body = new byte[0];
            }

            public Response(NetPack.Request req)
            {
                this.head = new NetPack.Header(req.head.seq, 0, req.head.opCmd);
                this.body = new byte[0];
            }

            public byte[] getBody()
            {
                return this.body;
            }

            public NetPack.Header getHead()
            {
                return this.head;
            }

            public int getLength()
            {
                return this.head.totalLen;
            }

            public bool unserializeFromBuffer(byte[] buffer, int count)
            {
                int offset = 0;
                this.head.totalLen = Util.readInt(buffer, ref offset);
                this.head.headLen = Util.readShort(buffer, ref offset);
                this.head.version = Util.readShort(buffer, ref offset);
                if ((this.head.headLen != 0x10) || (this.head.version != 1))
                {
                    Log.e("Network", string.Concat(new object[] { "unserialize invalid header, length=", this.head.headLen, ", version=", this.head.version }));
                }
                if (this.head.totalLen > count)
                {
                    Log.e("Network", string.Concat(new object[] { "unserialize invalid data-size, head.total=", this.head.headLen, ", recv-data=", count }));
                    return false;
                }
                this.head.opCmd = Util.readInt(buffer, ref offset);
                this.head.seq = Util.readInt(buffer, ref offset);
                int num2 = this.head.totalLen - this.head.headLen;
                this.body = new byte[num2];
                Util.readBuffer(buffer, ref offset, ref this.body, num2);
                return true;
            }
        }
    }
}

