using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wchat
{
    public class RSAEncryptData
    {
        static public string rsaKey2 = "";
        
        static string rsaKey = "BD6A54477640F0C0B209DB7747126896B27FB6B219AB9BC9C4CD9661F422E143A75AB2C34EAB88F44719D8D2E0D57CEC9713748BF821EC2014DF97B01CCE262F27CA24F4D89492F99DC8C1A414D0B8E760D815DF53A911D5D807CAF6827084BBE825A49C1BB9369675C4BE435597565B5C4222090235F6A5595003D5D5FA6780EBD51CEAC76D03D8EB9F97B45299719F7C352B2EF32449E0FDD09B562BA0317418B66FC0853EA9F5FFA85EAB8A14E2785C02B0CAC6AFD450EE5A6971C220E72FE6FA4B781235F39D206734C9974127E369E479BF3255FFF8C5FA4B133C642A5656A8E5F176472C5A3FE18D8816E40E58ABC2A4A32BA056EB0B504C86DAE05907";

        static string rsaKeyNewReg = "BD6A54477640F0C0B209DB7747126896B27FB6B219AB9BC9C4CD9661F422E143A75AB2C34EAB88F44719D8D2E0D57CEC9713748BF821EC2014DF97B01CCE262F27CA24F4D89492F99DC8C1A414D0B8E760D815DF53A911D5D807CAF6827084BBE825A49C1BB9369675C4BE435597565B5C4222090235F6A5595003D5D5FA6780EBD51CEAC76D03D8EB9F97B45299719F7C352B2EF32449E0FDD09B562BA0317418B66FC0853EA9F5FFA85EAB8A14E2785C02B0CAC6AFD450EE5A6971C220E72FE6FA4B781235F39D206734C9974127E369E479BF3255FFF8C5FA4B133C642A5656A8E5F176472C5A3FE18D8816E40E58ABC2A4A32BA056EB0B504C86DAE05907";


        public static byte[] RSAEncryptUnlock(byte[] data)
        {
            byte[] rasResult = null;

            int rsaLen = 256 - 12;
            byte[] aaa1 = new byte[rsaLen];
            byte[] aaa2 = new byte[rsaLen];
            byte[] aaa3 = new byte[rsaLen];
            byte[] aaa4 = new byte[data.Length - 3 * rsaLen];
            Array.Copy(data, 0 * rsaLen, aaa1, 0, rsaLen);
            Array.Copy(data, 1 * rsaLen, aaa2, 0, rsaLen);
            Array.Copy(data, 2 * rsaLen, aaa3, 0, rsaLen);
            Array.Copy(data, 3 * rsaLen, aaa4, 0, data.Length - rsaLen * 3);

            //rsa加密
            byte[] byteKey = null;
            if (rsaKey2 == "")
            {
                byteKey = strToToHexByte(rsaKey);
            }
            else
            {
                byteKey = strToToHexByte(rsaKey2);
            }
            string base64Key = Convert.ToBase64String(byteKey);

            byte[] rsa1 = RSAEncrypt(base64Key, aaa1);
            byte[] rsa2 = RSAEncrypt(base64Key, aaa2);
            byte[] rsa3 = RSAEncrypt(base64Key, aaa3);
            byte[] rsa4 = RSAEncrypt(base64Key, aaa4);

            rasResult = rsa1.Concat(rsa2).Concat(rsa3).Concat(rsa4).ToArray();

            return rasResult;
        }


        public static byte[] RSAEncryptCoreData(byte[] data)
        {
            byte[] rasResult = null;

            if (data.Length <= 256)
            {
                //rsa加密
                byte[] byteKey = strToToHexByte(rsaKey);
                string base64Key = Convert.ToBase64String(byteKey);
                rasResult = RSAEncrypt(base64Key, data);
            }
            else
            {
                byte[] aaa1 = data.Take(256 - 12).ToArray();
                byte[] aaa2 = data.Take(data.Length).Skip(256 - 12).ToArray();
                //rsa加密
                byte[] byteKey = strToToHexByte(rsaKey);
                string base64Key = Convert.ToBase64String(byteKey);
                byte[] rsa1 = RSAEncrypt(base64Key, aaa1);

                //rsa加密
                byte[] rsa2 = RSAEncrypt(base64Key, aaa2);
                rasResult = rsa1.Concat(rsa2).ToArray();
            }

            return rasResult;
        }

        static byte[] RSAEncrypt(string publickey, byte[] byt)
        {
            publickey = @"<RSAKeyValue><Modulus>" + publickey + "</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(byt, false);

            return cipherbytes;
        }

        static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
