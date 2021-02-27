namespace CRYPT
{
    using System;
    using System.IO;
    using zlib;

    public static class ZipUtils
    {
        public static byte[] compressBytes(byte[] sourceByte)
        {
            MemoryStream sourceStream = new MemoryStream(sourceByte);
            Stream stream2 = compressStream(sourceStream);
            byte[] buffer = new byte[stream2.Length];
            stream2.Position = 0L;
            stream2.Read(buffer, 0, buffer.Length);
            stream2.Close();
            sourceStream.Close();
            return buffer;
        }

        public static Stream compressStream(Stream sourceStream)
        {
            MemoryStream stream = new MemoryStream();
            ZOutputStream output = new ZOutputStream(stream, -1);
            CopyStream(sourceStream, output);
            output.finish();
            return stream;
        }

        public static void CopyStream(Stream input, Stream output)
        {
            int num;
            byte[] buffer = new byte[0x7d0];
            while ((num = input.Read(buffer, 0, 0x7d0)) > 0)
            {
                output.Write(buffer, 0, num);
            }
            output.Flush();
        }

        public static byte[] deCompressBytes(byte[] sourceByte)
        {
            MemoryStream sourceStream = new MemoryStream(sourceByte);
            Stream stream2 = deCompressStream(sourceStream);
            byte[] buffer = new byte[stream2.Length];
            stream2.Position = 0L;
            stream2.Read(buffer, 0, buffer.Length);
            stream2.Close();
            sourceStream.Close();
            return buffer;
        }

        public static Stream deCompressStream(Stream sourceStream)
        {
            MemoryStream stream = new MemoryStream();
            ZOutputStream output = new ZOutputStream(stream);
            CopyStream(sourceStream, output);
            output.finish();
            return stream;
        }
    }
}

