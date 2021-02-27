using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 微信挂机
{
    class Fun
    {
        static Random r = new Random();
        public userInfo deviceInfo()
        {
            userInfo info = new userInfo();
            info.imei = GenIEMI(info.imei);//生成imei
            info.deviceID = GenDeviceID();
            return info;
        }

        public static string Encrypt3DES(string strString)
        {
            byte[] key = { 0x3E, 0xCA, 0x2F, 0x6F, 0xFA, 0x6D, 0x49, 0x52, 0xAB, 0xBA, 0xCA, 0x5A, 0x7B, 0x06, 0x7D, 0x23};
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = key;
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = System.Text.Encoding.ASCII.GetBytes(strString);

            return toHexStr(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        internal static string GenMac(string p)
        {
            if (p.Length > 14)
            {
                return p;
            }
            Random rd = new Random();
            string[] s = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            StringBuilder sb = new StringBuilder(p);
            sb.Append(s[rd.Next(s.Length)]);
            sb.Append(s[rd.Next(s.Length)]);
            sb.Append(":");
            sb.Append(s[rd.Next(s.Length)]);
            sb.Append(s[rd.Next(s.Length)]);
            sb.Append(":");
            sb.Append(s[rd.Next(s.Length)]);
            sb.Append(s[rd.Next(s.Length)]);

            return sb.ToString();
        }

        internal static string GenSerial(string p, int len)
        {
            Random rd = new Random();
            string[] s = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            StringBuilder sb = new StringBuilder(p);
            for (int n = 0; n < len; ++n)
            {
                sb.Append(s[rd.Next(s.Length)]);
            }

            return sb.ToString();
        }

        public static string toHexStr(byte[] data)
        {
            string abc = "0123456789abcdef";
            StringBuilder sb = new StringBuilder();
            int high, low;
            foreach (byte item in data)
            {
                high = item / 16;
                low = item % 16;
                sb.Append(abc[high]).Append(abc[low]);
            }
            return sb.ToString();
        }

        public static byte[] toBin(string data)
        {
            byte[] result = new byte[data.Length / 2];
            int sum = 0;
            for (int n = 0; n < data.Length; n = n + 2)
            {
                if (data[n] >= '0' && data[n] <= '9')
                {
                    sum = data[n] - '0';
                }
                else
                {
                    sum = data[n] - 'a' + 10;
                }
                sum *= 16;

                if (data[n + 1] >= '0' && data[n + 1] <= '9')
                {
                    sum += data[n + 1] - '0';
                }
                else
                {
                    sum += data[n + 1] - 'a' + 10;
                }

                result[n / 2] = (byte)sum;
            }
            return result;
        }

        public static byte[] MD5(byte[] src)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
            
            return MD5CSP.ComputeHash(src);
        }

        public static string CumputeMD5(string msg)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(msg, "MD5").ToLower();
        }

        public static string TimeMd5()
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString(), "MD5").ToLower();
        }

        public static ImgSX imgsx(string imgPath)
        {
            Bitmap bmp = new Bitmap(imgPath);
            ImgSX sx = new ImgSX();
            sx.Height = bmp.Height;
            sx.Width = bmp.Width;

            return sx;
        }

        public static string GenIEMI(string srcIMEI)
        {
            byte[] values = new byte[15];

            int n;
            for (n = 0; n < srcIMEI.Length; n++)
            {
                values[n] = (byte)(Convert.ToInt32(srcIMEI[n]) - 0x30);
            }
            for (; n < 14; n++)
            {
                values[n] = (byte)r.Next(10);
            }

            int sum = 0;
            int dblValue;
            for (n = 0; n < 14; n += 2)
            {
                sum += values[n];
                dblValue = values[n + 1] * 2;
                sum += dblValue / 10;
                sum += dblValue % 10;
            }
            if (sum % 10 == 0)
            {
                values[14] = 0;
            }
            else
            {
                values[14] = (byte)(10 - (sum % 10));
            }

            for (n = 0; n < 15; n++)
            {
                values[n] += 0x30;
            }


            return System.Text.ASCIIEncoding.ASCII.GetString(values);
        }
        /// <summary>
        /// 生成硬件id
        /// </summary>
        /// <returns></returns>
        public static string GenDeviceID()
        {
            Random rd = new Random();
            string str = "A";
            string[] s = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            for (int i = 0; i < 14; i++)
            {
                str += s[rd.Next(16)].ToString();
            }

            return str;
        }
        /// <summary>
        /// 随机gps位置
        /// </summary>
        /// <param name="gps"></param>
        /// <returns></returns>
        public float[] gpsRamdom(string gps)
        {
            float[] _gps = new float[2];
            //38.     0     0      5    6      4   8442217
            //     十公里  公里 一百米  十米  米
            string weidu = gps.Split(new string[] { "," }, StringSplitOptions.None)[0].ToString();
            int w = weidu.Length - 7;
            weidu = weidu.Substring(0, w);
            string jingdu = gps.Split(new string[] { "," }, StringSplitOptions.None)[1].ToString();
            int j = jingdu.Length - 7;
            jingdu = jingdu.Substring(0, j);
            float fweidu = Convert.ToSingle(weidu);
            float fjingdu = Convert.ToSingle(jingdu);
            //随机纬度
            float tempw;
            tempw = Convert.ToSingle(r.Next(-100, 101)) / 10000;

            _gps[0] = fweidu + tempw;
            //随机经度
            float tempj;
            tempj = Convert.ToSingle(r.Next(-100, 101)) / 10000;
            _gps[1] = fjingdu + tempj;

            return _gps;

        }
        /// <summary>
        /// UNICODE字符转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string uncode(string str)
        {
            string outStr = "";
            Regex reg = new Regex(@"(?i)//u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate(Match m1)
            {
                return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
            });
            return outStr;
        }

        /// <summary>
        /// 用正则表达式提取字符串
        /// </summary>
        /// <param name="inputstr">数据</param>
        /// <param name="str">正在表达式</param>
        /// <returns></returns>
        public string myRegex(string inputstr, string str)
        {
            string temp = "";
            Regex r = new Regex(str, RegexOptions.IgnoreCase);
            Match m = r.Match(inputstr);
            if (m.Success)
            {
                temp = m.Result("$1");
            }
            return temp;
        }
        /// <summary>
        /// 用正则表达式提取字符串                    Regex panduan = new Regex("如果没有自动跳转,请<a href=\"http://weibo.cn/dpool/ttt/crossDomain.php\\?g=(.*?)&amp;t=.*?&amp;m=.*?&amp;r=1&amp;u=.*?&amp;vt=.*?\">点击这里</a>");//如果没有自动跳转,请<a href=\"(.*?)\">点击这里</a>

        /// </summary>
        /// <param name="inputstr">数据</param>
        /// <param name="str">正在表达式</param>
        /// <returns></returns>
        public string[] myRegexList(string inputstr, string str)
        {

            Regex r = new Regex(str);
            MatchCollection mc = r.Matches(inputstr);
            string[] temp = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                temp[i] = mc[i].Result("$1");
            }

            return temp;
        }

        /// <summary>
        /// 判断字符串是否可以转换成数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool toInt(string str)
        {
            bool bl = false;
            int a = 0;
            if (int.TryParse(str, out a) == true) //判断是否可以转换为整型  
            {
                bl = true;
            }

            return bl;
        }

        ///// 保存好友
        ///// </summary>
        ///// <param name="strangerlist"></param>
        ///// <param name="user"></param>
        //public List<SysncInfo> saveFriend(List<SysncInfo> strangerlist, string user)
        //{

        //    //检查库中是否已经包含
        //    for (int i = 0; i < strangerlist.Count; i++)
        //    {
        //        string friendSysName = strangerlist[i].strangerUserName;
        //        int x = db.cxFriends(friendSysName);
        //        if (x > 0)
        //        {
        //            strangerlist.RemoveAt(i);
        //            i--;
        //        }
        //    }
        //    db.SaveFriends(user, strangerlist);
        //    return strangerlist;
        //}

    }
    public struct userInfo
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string nick;

        public string user;
        public string pwd;
        /// <summary>
        /// 
        /// </summary>
        public byte[] sessionKey;
        /// <summary>
        /// 
        /// </summary>
        public uint uin;
        /// <summary>
        ///  内部用户名
        /// </summary>
        public string userName;
        /// <summary>
        ///  串号
        /// </summary>
        public string imei;
        /// <summary>
        /// 硬件id
        /// </summary>
        public string deviceID;
        /// <summary>
        /// 手机品牌
        /// </summary>
        public string manufacturer;
        /// <summary>
        /// 手机型号
        /// </summary>
        public string Model;
        /// <summary>
        /// 版本号
        /// </summary>
        public string release;
        /// <summary>
        /// 增量版本号
        /// </summary>
        public string incremental;
        /// <summary>
        /// 显示名
        /// </summary>
        public string display;
        /// <summary>
        /// model+abi
        /// </summary>
        public string abi;
        public string fingerprint;
        public string clientid;
        public string androidid;
        public string mac;
        public string devicetype;
        /// <summary>
        /// 内部版本号
        /// </summary>
        public string ostype;
        /// <summary>
        /// 最后定位位置
        /// </summary>
        public string site;
        /// <summary>
        /// 账号分组
        /// </summary>
        public string group;
        /// <summary>
        /// 同步key
        /// </summary>
        public byte[] initSyncKey;
        /// <summary>
        /// 签名是否设置
        /// </summary>
        public int QianMing;
        /// <summary>
        /// 头像是否设置
        /// </summary>
        public int headImg;
        /// <summary>
        /// 好友数
        /// </summary>
        public int friendCou;
        /// <summary>
        /// 分组
        /// </summary>
        public string task;
    }
    public struct login
    {
        public string zt;
    }
    public struct weixinzt
    {
        public bool EditInfo;
        public bool EditImg;
        public bool sycn;
        public byte[] initSyncKey;
    }
    public struct Sync
    {
        /// <summary>
        /// 同步key
        /// </summary>
        public byte[] iniSyncKey;
        public List<SysncInfo> strangerInfo;

    }
    public struct SysncInfo
    {
        /// <summary>
        /// 陌生人名
        /// </summary>
        public string strangerName;
        /// <summary>
        /// 陌生人消息
        /// </summary>
        public string strangerInfo;
        /// <summary>
        /// 加为好友参数
        /// </summary>
        public string ticket;
        /// <summary>
        /// 陌生人内部名
        /// </summary>
        public string strangerUserName;
    }
    public struct ImgSX
    {
        public int Height;
        public int Width;
    }
    public delegate void UpdateStatus(userInfo info, int nCol, string strTip);
    public delegate void ShowInfo(int x, string str, string info);
}
