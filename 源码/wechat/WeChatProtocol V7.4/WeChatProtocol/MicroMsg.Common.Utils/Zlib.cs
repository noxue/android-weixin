
namespace WeChat.MicroMsg.Common.Utils
{
    using ComponentAce.Compression.Libs.zlib;
    using System.IO;
    //using ComponentAce.Compression.Libs.zlib;

    public class Zlib
    {
        private const string TAG = "MicroMsg.Zlib";



        public static bool Compress(byte[] inBuf, ref byte[] outBuf)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                ZOutputStream stream2 = new ZOutputStream(stream, -1);
                stream2.Write(inBuf, 0, inBuf.Length);
                stream2.Flush();
                stream.Flush();
                outBuf = stream.ToArray();
                stream.Close();
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public static bool Decompress(byte[] inBuf, int unCompressLen, ref byte[] outBuf)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                ZOutputStream stream2 = new ZOutputStream(stream);
                stream2.Write(inBuf, 0, inBuf.Length);
                stream2.Flush();
                stream.Flush();
                outBuf = stream.ToArray();
                stream.Close();
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}
