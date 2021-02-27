using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace aliyun
{
    public class DeflateCompression
    {
        [DllImport("CodeDecrypt.dll")]
        private static extern int Zip(byte[] srcByte, int srcLen, byte[] dstByte, int dstLen);

        [DllImport("CodeDecrypt.dll")]
        private static extern int UnZip(byte[] srcByte, int srcLen, byte[] dstByte, int dstLen);

        public static byte[] DeflateZip(byte[] srcByte)
        {
            //压缩的时候数据长度 要处理
            byte[] dstByte = new byte[srcByte.Length + 100];
            int len = Zip(srcByte, srcByte.Length, dstByte, dstByte.Length);
            dstByte = dstByte.Take(len).ToArray();

            return dstByte;
        }

        public static byte[] DeflateUnZip(byte[] srcByte)
        {
            byte[] dstByte = new byte[204800];
            int len = UnZip(srcByte, srcByte.Length, dstByte, dstByte.Length);
            dstByte = dstByte.Take(len).ToArray();

            return dstByte;
        }

        public static byte[] DeflateLBSUnzip(byte[] srcByte)
        {
            byte[] dstByte = new byte[256000];
            int len = UnZip(srcByte, srcByte.Length, dstByte, dstByte.Length);
            dstByte = dstByte.Take(len).ToArray();

            return dstByte;
        }
    }
}
