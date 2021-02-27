using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 微信挂机
{
    public class RSAEncryptData
    {
        static string rsaKey = "D153E8A2B314D2110250A0A550DDACDCD77F5801F3D1CC21CB1B477E4F2DE8697D40F10265D066BE8200876BB7135EDC74CDBC7C4428064E0CDCBE1B6B92D93CEAD69EC27126DEBDE564AAE1519ACA836AA70487346C85931273E3AA9D24A721D0B854A7FCB9DED49EE03A44C189124FBEB8B17BB1DBE47A534637777D33EEC88802CD56D0C7683A796027474FEBF237FA5BF85C044ADC63885A70388CD3696D1F2E466EB6666EC8EFE1F91BC9353F8F0EAC67CC7B3281F819A17501E15D03291A2A189F6A35592130DE2FE5ED8E3ED59F65C488391E2D9557748D4065D00CBEA74EB8CA19867C65B3E57237BAA8BF0C0F79EBFC72E78AC29621C8AD61A2B79B";

        static string rsaKeyNewReg = "D153E8A2B314D2110250A0A550DDACDCD77F5801F3D1CC21CB1B477E4F2DE8697D40F10265D066BE8200876BB7135EDC74CDBC7C4428064E0CDCBE1B6B92D93CEAD69EC27126DEBDE564AAE1519ACA836AA70487346C85931273E3AA9D24A721D0B854A7FCB9DED49EE03A44C189124FBEB8B17BB1DBE47A534637777D33EEC88802CD56D0C7683A796027474FEBF237FA5BF85C044ADC63885A70388CD3696D1F2E466EB6666EC8EFE1F91BC9353F8F0EAC67CC7B3281F819A17501E15D03291A2A189F6A35592130DE2FE5ED8E3ED59F65C488391E2D9557748D4065D00CBEA74EB8CA19867C65B3E57237BAA8BF0C0F79EBFC72E78AC29621C8AD61A2B79B";

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

        public static byte[] RSAEncryptNewReg(byte[] data)
        {
            byte[] rasResult = new byte[0];
            byte[] byteKey = strToToHexByte(rsaKeyNewReg);
            string base64Key = Convert.ToBase64String(byteKey);
            int rsaLen = 256 - 12;
            while (data.Length > 0)
            {
                byte[] one = data.Take(rsaLen).ToArray();
                byte[] oneResult = RSAEncrypt(base64Key, one);
                rasResult = rasResult.Concat(oneResult).ToArray();
                data = data.Skip(one.Length).ToArray();
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
