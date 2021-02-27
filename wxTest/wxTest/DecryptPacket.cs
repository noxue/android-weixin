using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 微信挂机
{
    public class DecryptPacket
    {
        public static byte[] DecryptReceivedPacket(byte[] receivedPacket, byte[] key)
        {
            byte[] decByte = null;
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = key;
                ICryptoTransform ict = aes.CreateDecryptor();

                decByte = ict.TransformFinalBlock(receivedPacket, 0, receivedPacket.Length);
                aes.Clear();
            }
            catch (Exception ex)
            {
            }

            return decByte;
        }

        public static byte[] AESEncryptorData(byte[] data, byte[] key)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = key.Take(16).ToArray();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.IV = key.Take(16).ToArray();
            ICryptoTransform ict = aes.CreateEncryptor();
            byte[] decByte = null;
            decByte = ict.TransformFinalBlock(data, 0, data.Length);
            aes.Clear();

            return decByte;
        }
    }
}
