namespace CRYPT
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class DES3
    {
        private static byte[] tenpaykey = new byte[] { 0x3e, 0xca, 0x2f, 0x6f, 250, 0x6d, 0x49, 0x52, 0xab, 0xba, 0xca, 90, 0x7b, 6, 0x7d, 0x23 };

        public static byte[] Des3DecodeECB(byte[] data)
        {
            return Des3DecodeECB(data, tenpaykey, null);
        }

        public static byte[] Des3DecodeECB(byte[] data, byte[] key, byte[] iv = null)
        {
            try
            {
                MemoryStream stream = new MemoryStream(data);
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider {
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                byte[] buffer = new byte[data.Length];
                stream2.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (CryptographicException exception)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", exception.Message);
                return null;
            }
        }

        public static byte[] Des3EncodeECB(byte[] data, byte[] key, byte[] iv)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider {
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                stream2.Write(data, 0, data.Length);
                stream2.FlushFinalBlock();
                byte[] buffer = stream.ToArray();
                stream2.Close();
                stream.Close();
                return buffer;
            }
            catch (CryptographicException exception)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", exception.Message);
                return null;
            }
        }

        public static string Des3EncodeStr(string @in)
        {
            return Des3EncodeECB(Encoding.UTF8.GetBytes(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(@in)).ToString(0x10, 2)), tenpaykey, null).Copy<byte>(0x20).ToString(0x10, 2);
        }
    }
}

