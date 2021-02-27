namespace MicroMsg.Protocol
{
    using System;

    public class TLVUtil
    {
        public static int byte2int(byte[] b, int offSet, int len, bool isBigHead)
        {
            if (len == 4)
            {
                if (isBigHead)
                {
                    return ((((b[offSet] << 0x18) | ((b[offSet + 1] & 0xff) << 0x10)) | ((b[offSet + 2] & 0xff) << 8)) | (b[offSet + 3] & 0xff));
                }
                return ((((b[offSet + 3] << 0x18) | ((b[offSet + 2] & 0xff) << 0x10)) | ((b[offSet + 1] & 0xff) << 8)) | (b[offSet] & 0xff));
            }
            if (isBigHead)
            {
                return ((b[offSet] << 8) | (b[offSet + 1] & 0xff));
            }
            return ((b[offSet + 1] << 8) | (b[offSet] & 0xff));
        }

        public static int DecodeVByte32(ref int apuValue, byte[] apcBuffer, int off)
        {
            int num3;
            int num = 0;
            int num2 = 0;
            int num4 = 0;
            byte num5 = apcBuffer[off + num++];
            while ((num5 & 0xff) >= 0x80)
            {
                num3 = num5 & 0x7f;
                num4 += num3 << num2;
                num2 += 7;
                num5 = apcBuffer[off + num++];
            }
            num3 = num5;
            num4 += num3 << num2;
            apuValue = num4;
            return num;
        }

        public static int EncodeVByte32(int auValue, byte[] apcBuffer, int off)
        {
            int num = 0;
            while (auValue >= 0x80)
            {
                apcBuffer[off + num++] = (byte) (0x80 + (auValue & 0x7f));
                auValue = auValue >> 7;
            }
            apcBuffer[off + num++] = (byte) auValue;
            return num;
        }

        public static void fillByteArray(byte[] array, byte val)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[0] = val;
            }
        }

        public static byte[] getCheckSum(byte[] apcBuf, int offset, int aiSize)
        {
            byte[] buffer = new byte[2];
            if ((apcBuf == null) || (aiSize == 0))
            {
                buffer[0] = 0xff;
                buffer[1] = 0xff;
                return buffer;
            }
            ushort num = 0;
            for (int i = 0; i < aiSize; i++)
            {
                num = (ushort) (num + ((ushort) ((sbyte) apcBuf[offset + i])));
            }
            uint num3 = num;
            num3 = ~num3;
            num = (ushort) (num3 & 0xffff);
            buffer[0] = (byte) (num & 0xff);
            buffer[1] = (byte) ((num >> 8) & 0xff);
            return buffer;
        }

        public static byte[] int2byte(int x, int len, bool isBigHead)
        {
            byte[] buffer = null;
            if (len == 4)
            {
                buffer = new byte[4];
                if (isBigHead)
                {
                    buffer[0] = (byte) ((x >> 0x18) & 0xff);
                    buffer[1] = (byte) ((x >> 0x10) & 0xff);
                    buffer[2] = (byte) ((x >> 8) & 0xff);
                    buffer[3] = (byte) (x & 0xff);
                    return buffer;
                }
                buffer[3] = (byte) ((x >> 0x18) & 0xff);
                buffer[2] = (byte) ((x >> 0x10) & 0xff);
                buffer[1] = (byte) ((x >> 8) & 0xff);
                buffer[0] = (byte) (x & 0xff);
                return buffer;
            }
            buffer = new byte[2];
            if (isBigHead)
            {
                buffer[0] = (byte) (x & 0xff);
                buffer[1] = (byte) ((x >> 8) & 0xff);
                return buffer;
            }
            buffer[1] = (byte) (x & 0xff);
            buffer[0] = (byte) ((x >> 8) & 0xff);
            return buffer;
        }

        public static bool IsLittleEndianMechine()
        {
            return BitConverter.IsLittleEndian;
        }

        public static byte[] ToLittleEndian(short w)
        {
            return new byte[] { ((byte) (w & 0xff)), ((byte) ((w >> 8) & 0xff)) };
        }

        public static byte[] ToLittleEndian(int w)
        {
            byte[] buffer = new byte[] { (byte) (w & 0xff), (byte) ((w >> 8) & 0xff00) };
            buffer[2] = (byte) ((w >> 0x10) & 0xff);
            buffer[3] = (byte) ((w >> 0x18) & 0xff00);
            return buffer;
        }
    }
}

