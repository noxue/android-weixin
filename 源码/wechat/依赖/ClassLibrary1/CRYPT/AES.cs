namespace CRYPT
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class AES
    {
        private static UTF8Encoding encoding = new UTF8Encoding();

        public static byte[] AESDecrypt(byte[] data, byte[] password, CipherMode mode = 1)
        {
            byte[] buffer;
            try
            {
                using (Aes aes = new AesManaged())
                {
                    aes.Mode = mode;
                    aes.Key = password;
                    aes.IV = password;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (CryptoStream stream2 = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            stream2.Write(data, 0, data.Length);
                            stream2.FlushFinalBlock();
                        }
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception exception)
            {
                object[] args = new object[] { exception };
                Debug.Print("aes encryption failed ", args);
                buffer = null;
            }
            return buffer;
        }

        private static string AESDoPadWithString(string ogiginalStr, int PadWindth)
        {
            char paddingChar = '0';
            if (ogiginalStr.Length > PadWindth)
            {
                return ogiginalStr.Remove(PadWindth);
            }
            if (ogiginalStr.Length < PadWindth)
            {
                return ogiginalStr.PadRight(PadWindth, paddingChar);
            }
            return ogiginalStr;
        }

        public static byte[] AESEncrypt(byte[] data, byte[] password, CipherMode mode = 1)
        {
            byte[] buffer;
            try
            {
                using (Aes aes = new AesManaged())
                {
                    aes.Mode = mode;
                    aes.Key = password;
                    aes.IV = password;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (CryptoStream stream2 = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            stream2.Write(data, 0, data.Length);
                            stream2.FlushFinalBlock();
                        }
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception exception)
            {
                object[] args = new object[] { exception };
                Debug.Print("aes encryption failed ", args);
                buffer = null;
            }
            return buffer;
        }
    }
}

