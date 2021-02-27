using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRYPT;
using System.Security.Cryptography;
using DotNet4.Utilities;
using ProtoBuf;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

namespace Wchat
{
    public class Util
    {
        [DllImport("CodeDecrypt.dll", EntryPoint = "Zip")]
        private static extern int Zip(byte[] srcByte, int srcLen, byte[] dstByte, int dstLen);
        [DllImport("Common.dll")]
        public static extern int GenerateECKey(int nid, byte[] pub, byte[] pri);

        [DllImport("Common.dll")]
        public static extern int ComputerECCKeyMD5(byte[] pub, int pubLen, byte[] pri, int priLen, byte[] eccKey);


        public static string shortUrl = "http://hkshort.weixin.qq.com";
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        ///<param name="t">
        /// <returns></returns>
        public static string SerializeToString<t>(t T)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<t>(ms, T);

                ProtoBuf.Serializer.Deserialize<t>(ms);

                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }

        public static T Deserialize<T>(byte[] data)
        {
            T t = default(T);
            try
            {
                t = ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(data));
            }
            catch (Exception ex)
            {
                Console.WriteLine("解析成对象出错！");
            }
            return t;


        }

        public static byte[] Serialize<T>(T data)
        {
            MemoryStream memoryStream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(memoryStream, data);
            return memoryStream.ToArray();
        }

        public static byte[] nocompress_rsa(byte[] data)
        {
            RSA rsa = RSA.Create();
            rsa.ImportParameters(new RSAParameters() { Exponent = "010001".ToByteArray(16, 2), Modulus = "D153E8A2B314D2110250A0A550DDACDCD77F5801F3D1CC21CB1B477E4F2DE8697D40F10265D066BE8200876BB7135EDC74CDBC7C4428064E0CDCBE1B6B92D93CEAD69EC27126DEBDE564AAE1519ACA836AA70487346C85931273E3AA9D24A721D0B854A7FCB9DED49EE03A44C189124FBEB8B17BB1DBE47A534637777D33EEC88802CD56D0C7683A796027474FEBF237FA5BF85C044ADC63885A70388CD3696D1F2E466EB6666EC8EFE1F91BC9353F8F0EAC67CC7B3281F819A17501E15D03291A2A189F6A35592130DE2FE5ED8E3ED59F65C488391E2D9557748D4065D00CBEA74EB8CA19867C65B3E57237BAA8BF0C0F79EBFC72E78AC29621C8AD61A2B79B".ToByteArray(16, 2) });

            return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
        }

        public static byte[] HttpPost(byte[] data, string Url_GCI)
        {
            //Console.WriteLine(shortUrl + Url_GCI);
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = shortUrl + Url_GCI,
                Method = "post",
                PostdataByte = data,
                PostDataType = PostDataType.Byte,
                UserAgent = "MicroMessenger Client",
                Accept = "*/*",
                ContentType = "application/octet-stream",
                //sessionid = SEC_SF_edcd630591726845634a339fa1e14168; Domain =.weixin110.qq.com; Path =/; Secure; HttpOnly
                ResultType = ResultType.Byte,
                ProxyIp = "",
            };

            HttpResult ret = http.GetHtml(item);
            // Console.WriteLine(ret.ResultByte.ToString(16, 2));
            return ret.ResultByte == null ? new byte[2] { 1, 2 } : ret.ResultByte;
        }
        /// <summary>
        /// AES解密 先解密后解压
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] uncompress_aes(byte[] data, byte[] key)
        {
            try
            {
                data = AES.AESDecrypt(data, key);
                data = ZipUtils.deCompressBytes(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }
        /// <summary>
        /// 生成62数据
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        public static string SixTwoData(string imei)
        {

            /// string randStr = Time_Stamp().ToString();//随机字符串 
            string hexStr = HexToStr(System.Text.Encoding.Default.GetBytes(imei)).Replace(" ", "");
            string str = "62706c6973743030d4010203040506090a582476657273696f6e58246f626a65637473592461726368697665725424746f7012000186a0a2070855246e756c6c5f1020" + hexStr + "5f100f4e534b657965644172636869766572d10b0c54726f6f74800108111a232d32373a406375787d0000000000000101000000000000000d0000000000000000000000000000007f";
            return str;
        }
        /// <summary>
        /// 获取62数据设备信息
        /// </summary>
        /// <param name="Data">62数据</param>
        /// <returns></returns>
        public static string Get62Key(string Data)
        {
            int FinIndex = Data.IndexOf("6E756C6C5F1020", StringComparison.CurrentCultureIgnoreCase);

            if (FinIndex == -1)
            {
                return "3267b9918eb51294259866a6360432c0";
            }

            int head = FinIndex + "6E756C6C5F1020".Length;
            Console.WriteLine(Data.Substring(head, 64));
            return Encoding.Default.GetString(Data.Substring(head, 64).ToByteArray(16, 2));
        }
        /// <summary>
        /// //AES解密 只解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] nouncompress_aes(byte[] data, byte[] key)
        {
            data = AES.AESDecrypt(data, key);
            //data = ZipUtils.deCompressBytes(data);
            return data;
        }
        public static byte[] compress_rsa(byte[] data)
        {

            using (RSA rsa = RSA.Create())
            {
                byte[] strOut = new byte[] { };
                rsa.ImportParameters(new RSAParameters() { Exponent = "010001".ToByteArray(16, 2), Modulus = "D153E8A2B314D2110250A0A550DDACDCD77F5801F3D1CC21CB1B477E4F2DE8697D40F10265D066BE8200876BB7135EDC74CDBC7C4428064E0CDCBE1B6B92D93CEAD69EC27126DEBDE564AAE1519ACA836AA70487346C85931273E3AA9D24A721D0B854A7FCB9DED49EE03A44C189124FBEB8B17BB1DBE47A534637777D33EEC88802CD56D0C7683A796027474FEBF237FA5BF85C044ADC63885A70388CD3696D1F2E466EB6666EC8EFE1F91BC9353F8F0EAC67CC7B3281F819A17501E15D03291A2A189F6A35592130DE2FE5ED8E3ED59F65C488391E2D9557748D4065D00CBEA74EB8CA19867C65B3E57237BAA8BF0C0F79EBFC72E78AC29621C8AD61A2B79B".ToByteArray(16, 2) });
                var rsa_len = rsa.KeySize;
                rsa_len = rsa_len / 8;
                // data = ZipUtils.compressBytes(data);
                if (data.Length > rsa_len - 12)
                {
                    int blockCnt = (data.Length / (rsa_len - 12)) + (((data.Length % (rsa_len - 12)) == 0) ? 0 : 1);
                    //strOut.resize(blockCnt * rsa_len);

                    for (int i = 0; i < blockCnt; ++i)
                    {
                        int blockSize = rsa_len - 12;
                        if (i == blockCnt - 1) blockSize = data.Length - i * blockSize;
                        var temp = data.Copy(i * (rsa_len - 12), blockSize);
                        strOut = strOut.Concat(rsa.Encrypt(temp, RSAEncryptionPadding.Pkcs1)).ToArray();
                    }
                    return strOut;
                }
                else
                {
                    //strOut.resize(rsa_len);
                    return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                }
            }

        }

        public static byte[] compress_rsa_LOGIN(byte[] data)
        {

            using (RSA rsa = RSA.Create())
            {
                byte[] strOut = new byte[] { };
                rsa.ImportParameters(new RSAParameters() { Exponent = "010001".ToByteArray(16, 2), Modulus = "D153E8A2B314D2110250A0A550DDACDCD77F5801F3D1CC21CB1B477E4F2DE8697D40F10265D066BE8200876BB7135EDC74CDBC7C4428064E0CDCBE1B6B92D93CEAD69EC27126DEBDE564AAE1519ACA836AA70487346C85931273E3AA9D24A721D0B854A7FCB9DED49EE03A44C189124FBEB8B17BB1DBE47A534637777D33EEC88802CD56D0C7683A796027474FEBF237FA5BF85C044ADC63885A70388CD3696D1F2E466EB6666EC8EFE1F91BC9353F8F0EAC67CC7B3281F819A17501E15D03291A2A189F6A35592130DE2FE5ED8E3ED59F65C488391E2D9557748D4065D00CBEA74EB8CA19867C65B3E57237BAA8BF0C0F79EBFC72E78AC29621C8AD61A2B79B".ToByteArray(16, 2) });
                var rsa_len = rsa.KeySize;
                rsa_len = rsa_len / 8;
                data = ZipUtils.compressBytes(data);
                if (data.Length > rsa_len - 12)
                {
                    int blockCnt = (data.Length / (rsa_len - 12)) + (((data.Length % (rsa_len - 12)) == 0) ? 0 : 1);
                    //strOut.resize(blockCnt * rsa_len);

                    for (int i = 0; i < blockCnt; ++i)
                    {
                        int blockSize = rsa_len - 12;
                        if (i == blockCnt - 1) blockSize = data.Length - i * blockSize;
                        var temp = data.Copy(i * (rsa_len - 12), blockSize);
                        strOut = strOut.Concat(rsa.Encrypt(temp, RSAEncryptionPadding.Pkcs1)).ToArray();
                    }
                    return strOut;
                }
                else
                {
                    //strOut.resize(rsa_len);
                    return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                }
            }

        }


        public static byte[] compress_aes(byte[] data, byte[] key)
        {
            data = ZipUtils.compressBytes(data);

            return AES.AESEncrypt(data, key);
        }
        public static byte[] nocompress_aes(byte[] data, byte[] key)
        {
            return AES.AESEncrypt(data, key);
        }

        public static byte[] MD5(byte[] src)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();

            return MD5CSP.ComputeHash(src);
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
        public static byte[] DeflateZip(byte[] srcByte)
        {
            //压缩的时候数据长度 要处理
            byte[] dstByte = new byte[srcByte.Length + 100];
            int len = Zip(srcByte, srcByte.Length, dstByte, dstByte.Length);
            dstByte = dstByte.Take(len).ToArray();

            return dstByte;
        }

        /// <summary>
        /// 反序列JSON
        /// </summary>
        /// <param name="jsonStr">JSON字符串</param>
        /// <returns></returns>
        public static dynamic JsonToObject(string jsonStr)
        {

            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            dynamic modelDy = "";
            try
            {
                modelDy = jsonSerialize.Deserialize<dynamic>(jsonStr); //反序列化
            }
            catch (Exception ex)
            {
                Console.WriteLine("反序列json失败发生错误");
            }
            return modelDy;

        }

        public static string HexToStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("x2");
                }
            }
            return returnStr;
        }


        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return HexToStr(result);
        }

        private static string getip()
        {
            HttpHelper http = new HttpHelper();
            HttpItem Item = new HttpItem()
            {
                URL = "http://www.ifunc.ink/getIp.php",
            };

            HttpResult HttpRet = http.GetHtml(Item);

            return HttpRet.Html;

        }


        public static string EncryptWithMD5(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位

            }
            return strbul.ToString();
        }
    }



}


