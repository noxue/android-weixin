namespace MicroMsg.Protocol
{
    using System;

    public class TLVTypeTransform
    {
        public static int byteArrayHLToInt(byte[] b)
        {
            return (((((b[0] & 0xff) << 0x18) | ((b[1] & 0xff) << 0x10)) | ((b[2] & 0xff) << 8)) | (b[3] & 0xff));
        }

        public static long byteArrayHLToLong(byte[] b)
        {
            return (((((((((b[0] & 0xffL) << 0x38) | ((b[1] & 0xffL) << 0x30)) | ((b[2] & 0xffL) << 40)) | ((b[3] & 0xffL) << 0x20)) | ((b[4] & 0xffL) << 0x18)) | ((b[5] & 0xffL) << 0x10)) | ((b[6] & 0xffL) << 8)) | (b[7] & 0xffL));
        }

        public static int byteArrayLHToInt(byte[] b)
        {
            return (((((b[3] & 0xff) << 0x18) | ((b[2] & 0xff) << 0x10)) | ((b[1] & 0xff) << 8)) | (b[0] & 0xff));
        }

        public static long byteArrayLHToLong(byte[] b)
        {
            return (((((((((b[7] & 0xffL) << 0x38) | ((b[6] & 0xffL) << 0x30)) | ((b[5] & 0xffL) << 40)) | ((b[4] & 0xffL) << 0x20)) | ((b[3] & 0xffL) << 0x18)) | ((b[2] & 0xffL) << 0x10)) | ((b[1] & 0xffL) << 8)) | (b[0] & 0xffL));
        }

        public static byte[] intToByteArrayHL(int n)
        {
            return reverseByteArray(intToByteArrayLH(n));
        }

        public static byte[] intToByteArrayLH(int n)
        {
            byte[] buffer = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                int num2 = i * 8;
                buffer[i] = (byte) ((n >> num2) & 0xff);
            }
            return buffer;
        }

        public static byte[] longToByteArrayHL(long n)
        {
            return reverseByteArray(longToByteArrayLH(n));
        }

        public static byte[] longToByteArrayLH(long n)
        {
            byte[] buffer = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                int num2 = i * 8;
                buffer[i] = (byte) (n >> num2);
            }
            return buffer;
        }

        private static byte[] reverseByteArray(byte[] b)
        {
            int length = b.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = b[(length - 1) - i];
            }
            return buffer;
        }

        public static int reverseInt(int n)
        {
            return byteArrayHLToInt(intToByteArrayLH(n));
        }

        public static long reverseLong(long n)
        {
            return byteArrayHLToLong(longToByteArrayLH(n));
        }
    }
}

