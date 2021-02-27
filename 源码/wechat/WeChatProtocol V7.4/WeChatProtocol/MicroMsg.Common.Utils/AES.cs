
namespace WeChat.MicroMsg.Common.Utils
{
    using System;
    using System.Security.Cryptography;

    public class AES
    {
        private const string TAG = "aes";

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            if ((data == null) || (key == null))
            {
                return null;
            }
            byte[] buffer2 = new byte[0x10];
            int num = (key.Length > 0x10) ? 0x10 : key.Length;
            Buffer.BlockCopy(key, 0, buffer2, 0, num);
            using (AesManaged managed = new AesManaged())
            {
                //managed.set_KeySize(0x80);
                managed.KeySize = 128;
                managed.BlockSize = 128;
                // managed.set_BlockSize(0x80);
                managed.Key = buffer2;
                managed.IV = buffer2;
               // managed.Mode = CipherMode.CBC;
               // managed.Padding = PaddingMode.PKCS7;
                //managed.set_Key(buffer2);
                // managed.set_IV(buffer2);
                return managed.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if ((data == null) || (key == null))
            {
                return null;
            }
            byte[] buffer2 = new byte[0x10];
            int num = (key.Length > 0x10) ? 0x10 : key.Length;
            Buffer.BlockCopy(key, 0, buffer2, 0, num);
            using (AesManaged managed = new AesManaged())
            {
                // managed.get_LegalKeySizes();

                //managed.set_KeySize(0x80);
                //managed.set_BlockSize(0x80);
                //managed.set_Key(buffer2);
                //managed.set_IV(buffer2);

                managed.KeySize = 0x80;
                managed.BlockSize = 0x80;
                managed.Key = buffer2;
                managed.IV = buffer2;
                return managed.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}

