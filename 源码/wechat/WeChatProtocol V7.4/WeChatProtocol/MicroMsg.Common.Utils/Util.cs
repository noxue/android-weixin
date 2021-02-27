namespace MicroMsg.Common.Utils
{
    using Google.ProtocolBuffers;
    using MicroMsg.Common.Algorithm;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using micromsg;
    using System.Runtime.Serialization.Json;
    using System.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Security.Cryptography;
    using System.Reflection;
    using System.Net;
    using Network;
    using System.Xml.Serialization;
    using MicroMsg.Protocol;
    using System.Text.RegularExpressions;

    //using System.Collections.Generic;

    public static class Util
    {
        private static UTF8Encoding encoding = new UTF8Encoding();

        // public static byte[] gDeviceID;//= HexStringToByte("937fd6cfac5140684af356940f0caf48");
        public static string getDeviceUniqueId()
        {
            string gDeviceID = null;
            if (gDeviceID == null)
            {

                string str = null;

                if (str == null)
                {
                    str = "6684E64BCE9745031";
                }
                int length = str.Length;
                if (length > 14)
                {
                    gDeviceID = "Windows" + str.Substring(0, 9);
                }
                else
                {
                    gDeviceID = "Windows" + str + "wphone1234567890".Substring(0, 14 - length);
                }
            }
            return gDeviceID;
        }
        public static byte[] StringToByteArray(string str)
        {
            return encoding.GetBytes(NullAsNil(str));
        }
        public static string nullAsNil(string str)
        {
            if (str == null)
            {
                return "";
            }
            return str;
        }

        public static string NullAsNil(string str)
        {
            if (str != null)
            {
                return str;
            }
            return "";
        }

        public static byte[] NullAsNil(byte[] buf)
        {
            if (buf != null)
            {
                return buf;
            }
            return new byte[0];
        }

        public static int GetRandom(int a, int b)
        {
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return ran.Next(a, b + 1);
        }
        public static int readInt(byte[] buf, ref int offset)
        {
            int num = buf[offset];
            int num2 = buf[offset + 1];
            int num3 = buf[offset + 2];
            int num4 = buf[offset + 3];
            offset += 4;
            return ((((num << 0x18) + (num2 << 0x10)) + (num3 << 8)) + num4);
        }
        public static long stringToLong(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0L;
            }
            long num = 0L;
            try
            {
                num = long.Parse(str, NumberStyles.Integer);
            }
            catch (Exception exception)
            {
                Log.d("util", "stringToInt error:" + exception.ToString());
            }
            return num;
        }

        public static string bcd2Str(byte[] bytes)
        {
            StringBuilder temp = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
            {
                temp.Append((byte)((bytes[i] & 0xf0) >> 4));
                temp.Append((byte)(bytes[i] & 0x0f));
            }
            return temp.ToString().Substring(0, 1).Equals("0") ? temp.ToString().Substring(1) : temp.ToString();
        }

        public static byte[] HexStringToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
            {
                hexString = hexString + " ";
            }
            byte[] buffer = new byte[hexString.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 0x10);
            }
            return buffer;
        }

        public static SKBuiltinBuffer_t packQueryQuest(Dictionary<string, object> queryDic, bool needLog = false)
        {
            string str = stringWithFormEncodedComponentsAscending(queryDic, true, true, "&");

            if (string.IsNullOrEmpty(str))
            {
                str = "WCPaySign=";
            }
            else
            {
                //string str2 = "";//TenpayUtil.Get3DesSignData(str);
                string str2 = WCPaySignDES3Encode(str, "6BA3DAAA443A2BBB6311D7932B25F626");
                //str = str + "&WCPaySign=" + str2;
            }
            return Util.toSKBuffer(str);
        }


        public static string VerifyPayPassworRSAEncrypt(byte[] plainText)
        {
            //string encryptText = string.Empty;
            byte[] encryptBuf = new byte[256];

            //Utils.RSA.RSAEncrypt(out encryptBuf, plainText, "3943597CEFF2372F48089A062900AB30C9A0933ABA38B783D83FCAE7170C15D145F67B5C506985DF2C77FEB196EA43B55CD30D1EAFD8FA34694B72B952FF6B9349964413470F0369AF39E7EB6E09B692F3044E3ECD2E7BE333D4779FF8536F0236CB3D40E94798FA7216115B29C7488421F6A6626A77D42D84DAC4A304E35D82");
            //bcd2Str(encryptBuf);
            return byteToHexStr(encryptBuf);

        }
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2") + " ";
                }
            }
            return returnStr;
        }
        public static string WCPaySignDES3Encode(string data, string key)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            //DES.KeySize = 16;
            DES.Mode = CipherMode.CBC;
            DES.Padding = PaddingMode.Zeros;
            Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
            object obj = t.GetField("Encrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);
            MethodInfo mi = DES.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
            byte[] keys = str2Bcd(key);
            ICryptoTransform desCrypt = (ICryptoTransform)mi.Invoke(DES, new object[] { keys, CipherMode.CBC, null, 0, obj });
            byte[] Buffer = Encoding.UTF8.GetBytes(data);
            byte[] result = desCrypt.TransformFinalBlock(Buffer, 0, Buffer.Length);

            return bcd2Str(result);
        }
        public static byte[] str2Bcd(string asc)
        {
            int len = asc.Length;
            int mod = len % 2;

            if (mod != 0)
            {
                asc = "0" + asc;
                len = asc.Length;
            }

            byte[] abt = new byte[len];
            if (len >= 2)
            {
                len = len / 2;
            }

            byte[] bbt = new byte[len];
            abt = System.Text.Encoding.UTF8.GetBytes(asc);
            int j, k;

            for (int p = 0; p < asc.Length / 2; p++)
            {
                if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9'))
                {
                    j = abt[2 * p] - '0';
                }
                else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z'))
                {
                    j = abt[2 * p] - 'a' + 0x0a;
                }
                else
                {
                    j = abt[2 * p] - 'A' + 0x0a;
                }

                if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9'))
                {
                    k = abt[2 * p + 1] - '0';
                }
                else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z'))
                {
                    k = abt[2 * p + 1] - 'a' + 0x0a;
                }
                else
                {
                    k = abt[2 * p + 1] - 'A' + 0x0a;
                }

                int a = (j << 4) + k;
                byte b = (byte)a;
                bbt[p] = b;
            }
            return bbt;
        }
        public static Tuple<string, IEnumerable<KeyValuePair<string, string>>> UrlToData(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            url = url.Trim();
            string[] source = url.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
            if (source.Length == 1)
            {
                return new Tuple<string, IEnumerable<KeyValuePair<string, string>>>(url, null);
            }
            string str = source[0];
            return new Tuple<string, IEnumerable<KeyValuePair<string, string>>>(str, source.Skip<string>(1).Select<string, KeyValuePair<string, string>>(delegate (string s)
            {
                int length = s.IndexOf('=');
                return new KeyValuePair<string, string>(Uri.UnescapeDataString(s.Substring(0, length)), Uri.UnescapeDataString(s.Substring(length + 1)));
            }).ToList<KeyValuePair<string, string>>());
        }

        public static Dictionary<string, string> UrlToDictionay(string url)
        {
            try
            {
                IEnumerable<KeyValuePair<string, string>> enumerable = UrlToData(url).Item2;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                if (enumerable != null)
                {
                    foreach (KeyValuePair<string, string> pair in enumerable)
                    {
                        if (!dictionary.ContainsKey(pair.Key))
                        {
                            dictionary.Add(pair.Key, pair.Value);
                        }
                    }
                }
                return dictionary;
            }
            catch
            {
                Log.e("util", "UrlToDictionay Error:" + url);
                return new Dictionary<string, string>();
            }
        }

        public static string stringWithFormEncodedComponentsAscending(Dictionary<string, object> queryPairs, bool ascending, bool skipempty, string sep)
        {
            List<string> list;
            if (ascending)
            {
                list = (from m in queryPairs
                        select m.Key into b
                        orderby b
                        select b).ToList<string>();
            }
            else
            {
                list = (from m in queryPairs
                        select m.Key into b
                        orderby b descending
                        select b).ToList<string>();
            }
            string str = null;
            foreach (string str2 in list)
            {
                string str3;
                if (queryPairs[str2] == null)
                {
                    str3 = string.Empty;
                }
                else if (queryPairs[str2] is Enum)
                {
                    str3 = ((int)queryPairs[str2]).ToString();
                }
                else
                {
                    str3 = HttpUtility.UrlEncode(queryPairs[str2].ToString());
                }
                if (!skipempty || !string.IsNullOrEmpty(str3))
                {
                    if (str == null)
                    {
                        str = str2 + "=" + str3;
                    }
                    else
                    {
                        str = str + sep + str2 + "=" + str3;
                    }
                }
            }
            return str;
        }





        public static int stringToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            int num = 0;
            try
            {
                num = (int)long.Parse(str, NumberStyles.Integer);
            }
            catch (Exception exception)
            {
                Log.d("util", "stringToInt error:" + exception.ToString());
            }
            return num;
        }

        public static int writeBuffer(byte[] buf, ref byte[] destbuf, ref int offset)
        {
            Buffer.BlockCopy(buf, 0, destbuf, offset, buf.Length);
            offset += buf.Length;
            return offset;
        }

        public static int writeByte(byte value, ref byte[] buf, ref int offset)
        {
            buf[offset] = value;
            offset++;
            return offset;
        }

        public static int writeInt(int value, ref byte[] buf, ref int offset)
        {
            buf[offset] = (byte)((value >> 0x18) & 0xff);
            buf[offset + 1] = (byte)((value >> 0x10) & 0xff);
            buf[offset + 2] = (byte)((value >> 8) & 0xff);
            buf[offset + 3] = (byte)(value & 0xff);
            offset += 4;
            return offset;
        }

        public static int writeShort(short value, ref byte[] buf, ref int offset)
        {
            buf[offset] = (byte)((value >> 8) & 0xff);
            buf[offset + 1] = (byte)(value & 0xff);
            offset += 2;
            return offset;
        }

        public static short readShort(byte[] buf, ref int offset)
        {
            int num = buf[offset];
            int num2 = buf[offset + 1];
            offset += 2;
            return (short)((num << 8) + num2);
        }
        public static int readBuffer(byte[] srcbuf, ref int offset, ref byte[] destbuf, int count)
        {
            Buffer.BlockCopy(srcbuf, offset, destbuf, 0, count);
            offset += count;
            return count;
        }
        public static SKBuiltinBuffer_t toSKBuffer(string inStr)
        {
            SKBuiltinBuffer_t defaultInstance = SKBuiltinBuffer_t.DefaultInstance;
            SKBuiltinBuffer_t.Builder builder = new SKBuiltinBuffer_t.Builder();
            if (string.IsNullOrEmpty(inStr))
            {
                builder.ILen = 0;
            }
            else
            {
                builder.Buffer = ByteString.CopyFromUtf8(inStr);
                builder.ILen = (uint)builder.Buffer.Length;
            }
            return builder.Build();
        }

        public static SKBuiltinBuffer_t toSKBuffer(byte[] inBytes)
        {
            SKBuiltinBuffer_t defaultInstance = SKBuiltinBuffer_t.DefaultInstance;
            SKBuiltinBuffer_t.Builder builder = new SKBuiltinBuffer_t.Builder();
            if (inBytes == null)
            {
                builder.ILen = 0;
            }
            else
            {
                builder.Buffer = ByteString.CopyFrom(inBytes);
                builder.ILen = (uint)builder.Buffer.Length;
            }
            return builder.Build();
        }

        public static SKBuiltinBuffer_t toSKBuffer(byte[] inBytes, int offset, int count)
        {
            SKBuiltinBuffer_t defaultInstance = SKBuiltinBuffer_t.DefaultInstance;
            SKBuiltinBuffer_t.Builder builder = new SKBuiltinBuffer_t.Builder();
            if (inBytes == null)
            {
                builder.ILen = 0;
            }
            else
            {
                builder.Buffer = ByteString.CopyFrom(inBytes, offset, count);
                builder.ILen = (uint)builder.Buffer.Length;
            }
            return builder.Build();
        }

        public static SKBuiltinString_t toSKString(string inStr)
        {
            SKBuiltinString_t defaultInstance = SKBuiltinString_t.DefaultInstance;
            SKBuiltinString_t.Builder builder = SKBuiltinString_t.CreateBuilder();
            builder.String = (inStr == null) ? "" : inStr;
            return builder.Build();
        }
        public static int getNowDays()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = new DateTime(0x7b2, 1, 1);
            return (int)time.Subtract(time2).TotalDays;
        }
        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }
        public static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static double getNowMilliseconds()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = new DateTime(0x7b2, 1, 1);
            return time.Subtract(time2).TotalMilliseconds + GetRandom(1, 100);
        }

        public static double getNowSeconds()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = new DateTime(0x7b2, 1, 1);
            return time.Subtract(time2).TotalSeconds;
        }
        public static byte[] toFixLenString(string str, int len)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] l = new byte[len];
            //Arrays.fill(l, 0);
            for (int i = 0; (i < bytes.Length) && (i < l.Length); i++)
            {
                l[i] = bytes[i];
            }
            return l;
        }
        public static string getTimeZoneOffsetGwt()
        {
            DateTime now = DateTime.Now;
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan span = now.Subtract(utcNow);
            if (span.Hours > 0)
            {
                span = span.Add(new TimeSpan(0, 30, 0));
            }
            return (span.Hours.ToString());
        }
        public static string getFullPasswordMD5(string psw)
        {
            return MD5Core.GetHashString(psw, Encoding.UTF8);
        }
        public static string ByteArrayToString(byte[] buf)
        {
            buf = NullAsNil(buf);
            return encoding.GetString(buf, 0, buf.Length);
        }
        public static string getCutPasswordMD5(string rawPsw)
        {
            if (rawPsw == null)
            {
                rawPsw = "";
            }
            if (rawPsw.Length <= 0x10)
            {
                return getFullPasswordMD5(rawPsw);
            }
            return getFullPasswordMD5(rawPsw.Substring(0, 0x10));
        }

        public static byte[] byteArrayClone(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            byte[] destinationArray = new byte[buffer.Length];
            Array.Copy(buffer, destinationArray, buffer.Length);
            return destinationArray;
        }
        public static int CheckSum(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            int num = 0;
            foreach (char ch in str)
            {
                num += ch;
            }
            return num;
        }

        public static int CheckSum(byte[] data)
        {
            if (data == null)
            {
                return 0;
            }
            int num = 0;
            foreach (byte num2 in data)
            {
                num += num2;
            }
            return num;
        }
        public static object GetObjectFromJson(string strJson, Type type)
        {
            if (strJson == null)
            {
                return null;
            }
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(strJson)))
                {
                    return serializer.ReadObject(stream);
                }
            }
            catch
            {
                Log.d("Util", "Json parse failed");
                return null;
            }
        }
        public static string htmlString(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static byte[] AESDecrypt(byte[] encryptedData, byte[] key)
        {
            try
            {


                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = key; // Encoding.UTF8.GetBytes(AesKey);
                rijndaelCipher.IV = key;// Encoding.UTF8.GetBytes(AesIV);
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return plainText;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public static string BcdToStr(string strBcd)
        {
            int length = strBcd.Length;
            if ((length % 2) != 0)
            {
                return null;
            }
            char[] chArray2 = strBcd.ToCharArray();
            char[] chArray = new char[length / 2];
            int index = 0;
            for (int i = 0; index < strBcd.Length; i++)
            {
                int num3 = (chArray2[index] > '9') ? (chArray2[index] - '7') : (chArray2[index] - '0');
                int num4 = (chArray2[index + 1] > '9') ? (chArray2[index + 1] - '7') : (chArray2[index + 1] - '0');
                chArray[i] = (char)((num3 << 4) + num4);
                index += 2;
            }
            return new string(chArray);
        }
        public static string preParaXml(string strInXml)
        {
            if (string.IsNullOrEmpty(strInXml))
            {
                return null;
            }
            string str = strInXml;
            byte[] src = StringToByteArray(strInXml);
            if ((((src != null) && (src.Length >= 4)) && ((src[0] == 0xef) && (src[1] == 0xbb))) && (src[2] == 0xbf))
            {
                byte[] dst = new byte[src.Length - 3];
                Buffer.BlockCopy(src, 3, dst, 0, src.Length - 3);
                str = ByteArrayToString(dst);
            }
            if (!str.StartsWith("<"))
            {
                int index = str.IndexOf("<", 0);
                if (index < 0)
                {
                    return null;
                }
                str = str.Substring(index);
            }
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return str;
        }
        public static string BetweenArr(string str, string leftstr, string rightstr)
        {
            //    string tmp = "";
            //    int leftIndex = str.IndexOf(leftstr);//左文本起始位置  
            //    int leftlength = leftstr.Length;//左文本长度  
            //    int rightIndex = 0;
            //    string temp = "";
            //    while (leftIndex != -1)
            //    {
            //        rightIndex = str.IndexOf(rightstr, leftIndex + leftlength);
            //        if (rightIndex == -1)
            //        {
            //            break;
            //        }
            //        temp = str.Substring(leftIndex + leftlength, rightIndex - leftIndex - leftlength);
            //        tmp = tmp + temp;
            //        leftIndex = str.IndexOf(leftstr, rightIndex + 1);
            //    }
            //    return tmp;
            string temp = "";
            try
            {
                int i = str.IndexOf(leftstr) + leftstr.Length;
                 temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return temp;
        }
        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
        }
        public static void PostOnlineRequest(object sender, EventArgs e)
        {
            string ret = string.Empty;
            try
            {
                string a = "{\"HeadImgurl\":\"" + RedisConfig.headimg + "\",\"Nickname\":\"" + SessionPackMgr.getAccount().Nickname + "\",\"Username\":\"" + SessionPackMgr.getAccount().getUsername() + "\",\"Status\":" + getNowSeconds().ToString().Substring(0, 10) + "}";
                byte[] byteArray = Encoding.UTF8.GetBytes(a); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri("http://127.0.0.1:8080/api"));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            //return ret;
        }

        public static int RSAEncrypt(out byte[] encryptText, byte[] plainText, string KEY_N, string KEY_E)
        {



            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                //rsa.  =
                RSAParameters rsaParameters = new RSAParameters()
                {
                    Exponent = HexStringToByte(KEY_E),  // FromHex(exponent), // new byte[] {01, 00, 01}
                    Modulus = HexStringToByte(KEY_N)// FromHex(modulus),   // new byte[] {A3, A6, ...}
                };

                rsa.ImportParameters(rsaParameters);
                // string plainTexts = Util.byteToHexStr(plainText);
                //string tmp = Util.byteToHexStr(rsa.Encrypt(plainText, false));
                //encryptText = Util.HexStringToByte(tmp);
                encryptText = rsa.Encrypt(plainText, false);
            }

            return 0;

        }

        public static T Deserialize<T>(T t, string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                XmlSerializer xz = new XmlSerializer(t.GetType());
                return (T)xz.Deserialize(sr);
            }
        }
        #region 序列化  
        /// <summary>  
        /// 序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="obj">对象</param>  
        /// <returns></returns>  
        public static string Serializer<T>(T t)
        {
            MemoryStream Stream = new MemoryStream();
            // XmlSerializer xml = new XmlSerializer(type);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer slz = new XmlSerializer(t.GetType());
            try
            {
                //序列化对象  
                //  xml.Serialize(Stream, obj);
                slz.Serialize(Stream, t, ns);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion

        public static byte[] RSAEncryptBlock(byte[] plainText, string KEY_N, string KEY_E, int blockSize = 2048)
        {



            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                //rsa.  =
                RSAParameters rsaParameters = new RSAParameters()
                {
                    Exponent = HexStringToByte(KEY_E), // FromHex(exponent), // new byte[] {01, 00, 01}
                    Modulus = HexStringToByte(KEY_N)// FromHex(modulus),   // new byte[] {A3, A6, ...}
                };

                rsa.ImportParameters(rsaParameters);


                int bufferSize = (blockSize / 8) - 11;
                byte[] buffer = new byte[bufferSize];

                // string plainTexts = Util.byteToHexStr(plainText);
                //string tmp = Util.byteToHexStr(rsa.Encrypt(plainText, false));
                //encryptText = Util.HexStringToByte(tmp);
                //encryptText = rsa.Encrypt(plainText, false);
                using (MemoryStream input = new MemoryStream(plainText))
                using (MemoryStream ouput = new MemoryStream())
                {
                    while (true)
                    {
                        int readLine = input.Read(buffer, 0, bufferSize);
                        if (readLine <= 0)
                        {
                            break;
                        }
                        byte[] temp = new byte[readLine];
                        Array.Copy(buffer, 0, temp, 0, readLine);
                        byte[] encrypt = rsa.Encrypt(temp, false);
                        ouput.Write(encrypt, 0, encrypt.Length);
                    }
                    return ouput.ToArray();
                }
            }

            return null;


        }


        public static byte[] ReadProtoRawData(byte[] buffer, int tags)
        {
            uint tag = 0;
            string fieldName = "";
            CodedInputStream input = CodedInputStream.CreateInstance(buffer);
            while (input.ReadTag(out tag, out fieldName))
            {

                int Field = WireFormat.GetTagFieldNumber(tag);
                WireFormat.WireType WireType = WireFormat.GetTagWireType(tag);
                switch (WireType)
                {
                    case WireFormat.WireType.LengthDelimited:
                        ByteString tmps = ByteString.Empty;

                        input.ReadBytes(ref tmps);
                        if (tags == tag >> 3)
                        {
                            return tmps.ToByteArray();
                        }
                        break;
                    case WireFormat.WireType.Fixed32:
                        break;
                    case WireFormat.WireType.Fixed64:
                        break;
                    case WireFormat.WireType.Varint:
                        int val = 0;
                        input.ReadInt32(ref val);
                        break;

                }

            }
            return null;

        }
        public static void WriteLog(string strLog)
        {
            string sFilePath = ConstantsProtocol.CurrentDirectory;
            //+ DateTime.Now.ToString("dd")
            string sFileName = "二次登陆日志" + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
            sw.Close();
            fs.Close();
        }
        public static void ReadProtoRawDataS(List<object> list, byte[] buffer, int tags)
        {
            uint tag = 0;
            string fieldName = "";
            CodedInputStream input = CodedInputStream.CreateInstance(buffer);
            while (input.ReadTag(out tag, out fieldName))
            {

                int Field = WireFormat.GetTagFieldNumber(tag);
                WireFormat.WireType WireType = WireFormat.GetTagWireType(tag);
                switch (WireType)
                {
                    case WireFormat.WireType.LengthDelimited:
                        ByteString tmps = ByteString.Empty;

                        input.ReadBytes(ref tmps);
                        if (tags == tag >> 3)
                        {
                            //return tmps.ToByteArray();
                            list.Add(tmps.ToByteArray());
                        }
                        break;
                    case WireFormat.WireType.Fixed32:
                        break;
                    case WireFormat.WireType.Fixed64:
                        break;
                    case WireFormat.WireType.Varint:
                        int val = 0;
                        input.ReadInt32(ref val);
                        break;

                }

            }

        }


        public static uint ReadProtoInt(byte[] buffer, int tags)
        {
            uint tag = 0;
            string fieldName = "";
            CodedInputStream input = CodedInputStream.CreateInstance(buffer);
            while (input.ReadTag(out tag, out fieldName))
            {


                int Field = WireFormat.GetTagFieldNumber(tag);

                WireFormat.WireType WireType = WireFormat.GetTagWireType(tag);


                switch (WireType)
                {
                    case WireFormat.WireType.LengthDelimited:
                        ByteString tmps = ByteString.Empty;

                        input.ReadBytes(ref tmps);

                        break;
                    case WireFormat.WireType.Fixed32:
                        break;
                    case WireFormat.WireType.Fixed64:
                        break;
                    case WireFormat.WireType.Varint:
                        uint val = 0;
                        input.ReadUInt32(ref val);

                        if (tags == tag >> 3)
                        {
                            return val;
                        }
                        break;

                }

            }
            return 0;

        }
    }



}

