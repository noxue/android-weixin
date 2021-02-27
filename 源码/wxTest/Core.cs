  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.ProtocolBuffers;
using System.Net;
using System.Net.Sockets;
using mm.command;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace 微信挂机
{
    public class Core
    {

        List<string> ipList = new List<string>();
        Random r = new Random();
        private static MyIni Ini = new MyIni(Environment.CurrentDirectory + "\\setup.ini");
        private static string hostUrl = Ini.GetIniString("setup", "hostUrl", "", 256);
        private TcpClient client = new TcpClient();
        private string longServerName = Ini.GetIniString("setup", "longserver", "long.weixin.qq.com", 256);
        private int seq = new Random().Next(100);

        /// <summary>
        /// 手机串号
        /// </summary>
        string imei = "";
        /// <summary>
        /// 手机硬件id
        /// </summary>
        string deviceID = "";
        /// <summary>
        /// 手机品牌
        /// </summary>
        string deviceBrand = "";
        /// <summary>
        /// 手机型号
        /// </summary>
        string deviceModel = "";
        /// <summary>
        /// 版本号
        /// </summary>
        string RELEASE = "";
        /// <summary>
        /// 增量版本
        /// </summary>
        string INCREMENTAL = "";
        /// <summary>
        /// 显示名
        /// </summary>
        string DISPLAY = "";
        /// <summary>
        /// 内部版本号
        /// </summary>
        string OSType = "";
        /// <summary>
        /// 随机Key
        /// </summary>
        byte[] randomEncryKey = null;
        string uuid = string.Empty;
        byte[] notifykey;

        uint uin = 0;
        byte[] sessionKey = null;
        byte[] cookieToken = null;
        byte[] eccKey = new byte[16];
        public string userName = "";
        string imgSid = "";//登陆需要验证码时，返回包里的验证码图片ID
        string imgEncryptKey = "";//登陆需要验证码时，返回包里的验证码图片的加密key
        string ticket = "";//验证码验证成功后的ticket
        byte[] initSyncKey = new byte[32];
        byte[] maxSyncKey = null;
        private string regSession = "";
        private string clientid;
        private string androidid;
        private string fingerprint;
        private string mac;
        private string devicetype;
        /// <summary>
        ///  
        /// </summary>
        /// <param name="wxAccount">用户名</param>
        /// <param name="wxPwd">密码</param>
        public Core(userInfo userinfo)
        {
            WXAccount = userinfo.user;
            WXPwd = userinfo.pwd;

            devicetype = userinfo.devicetype;
            androidid = userinfo.androidid;
            clientid = userinfo.clientid;
            mac = userinfo.mac;
            fingerprint = userinfo.fingerprint;

            imei = userinfo.imei;//生成手机串号
            deviceID = userinfo.deviceID;//生成手机硬件id
            deviceBrand = userinfo.manufacturer;//手机名称
            deviceModel = userinfo.Model;//手机型号
            OSType = userinfo.ostype;
            RELEASE = userinfo.release;
            INCREMENTAL = userinfo.incremental;
            DISPLAY = userinfo.display;
            sessionKey = userinfo.sessionKey;
            uin = userinfo.uin;
            userName = userinfo.userName;
            initSyncKey = userinfo.initSyncKey;
            randomEncryKey = GenRandomEncryKey();//随机key
        }
        public AuthResponse LoginWeChat()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType);
            AuthRequest arObj = GoogleProto.CreateAuthRequestEntity(brObj, WXAccount, WXPwd, imei, deviceBrand, deviceModel, RELEASE, 
                INCREMENTAL, DISPLAY, OSType, randomEncryKey);

            byte[] arData = arObj.ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));//("\x8e\x61\x93\x89\xe8\x99\x42\x1d\x07\x74\xF0\x09\x36\x5e\x4b\x1f",Encoding.Default));
            SKBuiltinBuffer_t sbk = skbb.Build();
            byte[] pre = {0x0a, 0x14};
            byte[] part1 = pre.Concat(sbk.ToByteArray()).ToArray();
            int part1Len = part1.Length;
            byte[] part1ZipData = DeflateCompression.DeflateZip(part1);
            byte[] part1EnData = RSAEncryptData.RSAEncryptCoreData(part1ZipData);

            //加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, randomEncryKey);

            byte[] dataPacket = new byte[12 + aesData.Length + part1EnData.Length];
            dataPacket[3] = (byte)part1.Length;
            
            dataPacket[6] = (byte)(lenBeforeZip / 256);
            dataPacket[7] = (byte)(lenBeforeZip % 256);

            dataPacket[10] = (byte)(part1EnData.Length / 256);
            dataPacket[11] = (byte)(part1EnData.Length % 256);
            Array.Copy(part1EnData, 0, dataPacket, 12, part1EnData.Length);
            Array.Copy(aesData, 0, dataPacket, 12 + part1EnData.Length, aesData.Length);

            //构造数据包
            byte[] packet = ConstructPacket.AuthRequestPacket(lenBeforeZip, lenAfterZip, dataPacket, deviceID, 0x17c);
         

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/newauth";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            /*******************************************************/
            byte[] _re = receivePacket;            
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();            
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            AuthResponse arReceive = AuthResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0 || arReceive.Base.Ret == -17)
            {
                uin = arReceive.Uin;
                userName = arReceive.UserName.String;
                sessionKey = arReceive.SessionKey.ToByteArray().Take(16).ToArray();
                string hostUrl = arReceive.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
            }
            else if (arReceive.Base.Ret == -6)//需要验证码  -30激活
            {
                imgSid = arReceive.ImgSid.String;
                imgEncryptKey = arReceive.ImgEncryptKey.String;
            }
            else if (arReceive.Base.Ret == -30)//
            {
                ticket = arReceive.Ticket;
            }
            else if (arReceive.Base.Ret == -301)
            {                
                string hostUrl = arReceive.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
                goto L1;
            }
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        [DllImport("Common.dll")]
        private static extern int GenerateECKey(int nid, byte[] pub, byte[] pri);

        [DllImport("Common.dll")]
        private static extern int ComputerECCKeyMD5(byte[] pub, int pubLen, byte[] pri, int priLen, byte[] eccKey);

        [DllImport("Common.dll")]
        private static extern uint Adler32(uint adler, byte[] buf, int len);
        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long GetCurTime()
        {
            return (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds);
        }

        public NewAuthResponse ManualAuth(string fingerPrint, string deviceType, string abi)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType);
            string clientid = deviceID + "0_" + GetCurTime().ToString();
            ManualAuthRequest arObj = GoogleProto.CreateManualAuthRequestEntity(brObj, imei, deviceBrand, deviceModel, OSType,
                fingerPrint, clientid, abi, deviceType);
            Debug.Print(arObj.ToString());
            ECDHKey.Builder eb = new ECDHKey.Builder();
            eb.SetNID(713);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();

            byte[] eccData = new byte[57];
            byte[] eccPri = new byte[328];
            int l = GenerateECKey(713, eccData, eccPri);

            skbb.SetILen(eccData.Length);
            skbb.SetBuffer(ByteString.CopyFrom(eccData));
            eb.SetKey(skbb);
            ECDHKey cliPubECDHKey = eb.Build();
            InitKey AuthKey = GoogleProto.CreateInitKeyEntity(randomEncryKey, cliPubECDHKey, WXAccount, WXPwd);

            byte[] arData = arObj.ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            byte[] part1 = AuthKey.ToByteArray();
            int part1Len = part1.Length;
            byte[] part1ZipData = DeflateCompression.DeflateZip(part1);
            byte[] part1EnData = RSAEncryptData.RSAEncryptCoreData(part1ZipData);

            //加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, randomEncryKey);

            byte[] dataPacket = new byte[12 + aesData.Length + part1EnData.Length];
            dataPacket[2] = (byte)(part1.Length / 256);
            dataPacket[3] = (byte)(part1.Length % 256);

            dataPacket[6] = (byte)(lenBeforeZip / 256);
            dataPacket[7] = (byte)(lenBeforeZip % 256);
            
            dataPacket[10] = (byte)(part1EnData.Length / 256);
            dataPacket[11] = (byte)(part1EnData.Length % 256);
            Array.Copy(part1EnData, 0, dataPacket, 12, part1EnData.Length);
            Array.Copy(aesData, 0, dataPacket, 12 + part1EnData.Length, aesData.Length);

            //构造数据包
            byte[] packet = ConstructPacket.AuthRequestPacket(lenBeforeZip, lenAfterZip, dataPacket, deviceID, 0x2bd);

            byte[] abc = new byte[packet.Length + 16];

            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = 0xFD;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);
            //这里
            //string tt = "";
            //byte[] temp = Encoding.Unicode.GetBytes(tt);
            //abc = temp;


            IPHostEntry ips = Dns.GetHostByName(longServerName);
            string longIP = ips.AddressList[0].ToString();
            //发送数据包     
        L1:
            byte[] receivePacket = new byte[16];
            client = new TcpClient();
            client.Connect(longServerName, 8080);
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            toRead = ns.Read(receivePacket, 0, receivePacket.Length);
            
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[0] & 0x03) == 1;
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            /*******************************************************/
            byte[] _re = receivePacket;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            //string tt = "ᘊ\bሒည癅牥瑹楨杮椠\u2073歯ܐꐚࠄ틻ሂࡂ\u05c9㴒㤈㤒⨄艝먃赙얪ꔻ䛍ᒼ꺎㫍\u0dd5⪢퇷儁훃ఴ�鋯웚쑑轫繞둪\u18f7\u0de0䉮ᨐࠤሠ㸠꿟�㬾﹖\u0ea8ඉᷡ꾟䨝㘷㨏�ల��≘Ɲ霈ሁƗᐊဈဒ滦궅䚼不兄�覛킋缒笈笒금ሁ㙰벬觼鼯婤㉾⿷翄髥ᵅ刐숀烙킋郔དꏂﾥ퇖唞ቺ选ࠜㄸ乂胢ᬒ焻㔝ԭ㹏墻貙厡ሊ✗骩䖰▆茝៷㈌憣취믉ᣛ폃\u2be9㨾ᠯ꒘뚯⠆㈂ࠂ㨀ለࠂ∀ࠂ䈀ሄࠂ䨀ࠂ刀ࠂ戀ࠂ稀ࠗሃࠅላ、ԒለĒሰࠅሠ\u3101ƈ退\u0001Ƙꈄ萁栁瑴獰⼺眯洮楡\u2e6c煱挮浯振楧戭湩氯杯湩甿湩〽欦祥㠽㉢㉡㕡㕦㡢捦晣呍さ橎祣呏㕧睍欦祥祴数㈽琦牡敧㵴敳牴浥湩♤牦浯眽楥楸♮瑶眽楥楸♮㵦桸浴ꡬāưﺥ먅ᰁ᠈᠒�ㆶ砒㱻뛄쎜桶鉌樈쬙馀ǂࠜመ⸘뤋튘ퟹᒳ劾屪䅮袧έ레≓Ǣጊ硷摩ㅟ瑨洹煥浫然㉴ሲ堏癡敩\u2072牆湡汫湩\u0018పㄫ〷㌸〹㤹㌲ꕀ䠠뇱Ɖɐ`٪敷硩湩\u0c72뻥ꆿ鯥龘xƂƄ瑨灴㩳⼯\u2e77慭汩焮\u2e71潣⽭杣\u2d69楢⽮潬楧㽮極㵮☰敫㵹戸愲愲昵戵昸捣䵦啔丰捪佹杔䴵♷敫瑹灹㵥☲慴杲瑥猽瑥敲業摮昦潲㵭敷硩湩瘦㵴敷硩湩昦砽瑨汭줪\u0a29ϜਈⰒሊ潬杮眮楥楸\u2e6e煱挮浯ᐒ歨潬杮眮楥楸\u2e6e煱挮浯\u0018⸒ጊ桳牯\u2e74敷硩湩焮\u2e71潣ቭ栕獫潨瑲眮楥楸\u2e6e煱挮浯\u0018‒ऊ硷焮\u2e71潣ቭ眑扥眮捥慨慴灰挮浯\u0018㈒ᔊ畳灰牯\u2e74敷硩湩焮\u2e71潣ቭ栗獫灵潰瑲眮楥楸\u2e6e煱挮浯\u0018㐒ᘊ硥獴潨瑲眮楥楸\u2e6e煱挮浯᠒歨硥獴潨瑲眮楥楸\u2e6e煱挮浯\u0018㠒᠊業潮獲潨瑲眮楥楸\u2e6e煱挮浯ᨒ歨業潮獲潨瑲眮楥楸\u2e6e煱挮浯\u0018㘒ᜊ桳牯\u2e74慰\u2e79敷硩湩焮\u2e71潣ቭ栙獫潨瑲瀮祡眮楥楸\u2e6e煱挮浯\u0018〒ᔊ敮祷慥\u2e72敷硩湩焮\u2e71潣ቭ渕睥敹牡眮楥楸\u2e6e煱挮浯\u0018〒ᐊ桳牯㙴眮楥楸\u2e6e煱挮浯ᘒ歨桳牯㙴眮楥楸\u2e6e煱挮浯\u0018᠒ऊ潬慣桬獯ቴ氉捯污潨瑳\u0018ᰒଊ㐴㨳〸㠺㠰ሰ㔃㔺踘 Әਨ8줚ࠥဃᨉ\u038d؈倐耚㈁㌰㈮㔰\u312e㌴\u312e㌴\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨潬杮眮楥楸\u2e6e煱挮浯\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㐱⸳㐱3\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0ᨀ\u038d؈倐耚㈁㌰㈮㔰\u312eㄵ\u312e㤵\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨潬杮眮楥楸\u2e6e煱挮浯\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㔱⸱㔱9\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0ᨀ\u038d؈倐耚㈁㌰㈮㔰\u312e㜴\u312e㜶\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨潬杮眮楥楸\u2e6e煱挮浯\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㐱⸷㘱7\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312e㤲\u312e\u3130\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨桳牯\u2e74敷硩湩焮\u2e71潣m\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㈱⸹〱1\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312e㜴\u312e㠶\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨桳牯\u2e74敷硩湩焮\u2e71潣m\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㐱⸷㘱8\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312eㄵ\u312e〶\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨桳牯\u2e74敷硩湩焮\u2e71潣m\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㔱⸱㘱0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312e㤲\u312e\u3130\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨硥獴潨瑲眮楥楸\u2e6e煱挮浯\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㈱⸹〱1\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312e㜴\u312e㠶\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨硥獴潨瑲眮楥楸\u2e6e煱挮浯\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㐱⸷㘱8\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312eㄵ\u312e〶\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨硥獴潨瑲眮楥楸\u2e6e煱挮浯\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㔱⸱㘱0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312eㄵ㐮2\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨桳牯\u2e74慰\u2e79敷硩湩焮\u2e71潣m\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㔱⸱㈴0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚㈁㌰㈮㔰\u312e㘴\u312e㤱\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ歨桳牯\u2e74慰\u2e79敷硩湩焮\u2e71潣m\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦〲⸳〲⸵㐱⸶ㄱ9\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀\u038d؈倐耚\u3101㜲〮〮\u312e\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0∀ƀ潬慣桬獯t\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0耪㨁昺晦㩦㈱⸷⸰⸰㐱⸶ㄱ9\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0⠀펜ৌ";
            //byte[] temp = Encoding.Unicode.GetBytes(tt);
            //unzipPacket = temp;
            //string temp = Encoding.Unicode.GetString(unzipPacket);
            //这里

            //反序列化数据包
            NewAuthResponse arReceive = NewAuthResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0 || arReceive.Base.Ret == -17)
            {
                uin = arReceive.Auth.Uin;
                userName = arReceive.User.UserName;
                ComputerECCKeyMD5(arReceive.Auth.SvrPubECDHKey.Key.Buffer.Take(57).ToArray(), 57, eccPri, 328, eccKey);
                sessionKey = DecryptPacket.DecryptReceivedPacket(arReceive.Auth.SessionKey.Buffer.Take(32).ToArray(), eccKey);
                hostUrl = arReceive.Server.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
            }
            else if (arReceive.Base.Ret == -6)//需要验证码  -30激活
            {
                imgSid = arReceive.Auth.Str14;
                imgEncryptKey = arReceive.Auth.Str14;
            }
            else if (arReceive.Base.Ret == -30)//
            {
                ticket = arReceive.Auth.Str16;
            }
            else if (arReceive.Base.Ret == -301)
            {
                hostUrl = arReceive.Server.NewHostList.ListList[1].Substitute;                
                longServerName = arReceive.Server.NewHostList.ListList[0].Substitute;                
                goto L1;
            }
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        public NewAuthResponse ManualAuth短(string fingerPrint, string deviceType, string abi)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType);
            string clientid = deviceID + "0_" + GetCurTime().ToString();
            ManualAuthRequest arObj = GoogleProto.CreateManualAuthRequestEntity(brObj, imei, deviceBrand, deviceModel, OSType,
                fingerprint, clientid, abi, devicetype);

            ECDHKey.Builder eb = new ECDHKey.Builder();
            eb.SetNID(713);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();

            byte[] eccData = new byte[57];
            byte[] eccPri = new byte[328];
            int l = GenerateECKey(713, eccData, eccPri);

            skbb.SetILen(eccData.Length);
            skbb.SetBuffer(ByteString.CopyFrom(eccData));
            eb.SetKey(skbb);
            ECDHKey cliPubECDHKey = eb.Build();
            InitKey AuthKey = GoogleProto.CreateInitKeyEntity(randomEncryKey, cliPubECDHKey, WXAccount, WXPwd);

            byte[] arData = arObj.ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            byte[] part1 = AuthKey.ToByteArray();
            int part1Len = part1.Length;
            byte[] part1ZipData = DeflateCompression.DeflateZip(part1);
            byte[] part1EnData = RSAEncryptData.RSAEncryptCoreData(part1ZipData);

            //加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, randomEncryKey);

            byte[] dataPacket = new byte[12 + aesData.Length + part1EnData.Length];
            dataPacket[2] = (byte)(part1.Length / 256);
            dataPacket[3] = (byte)(part1.Length % 256);

            dataPacket[6] = (byte)(lenBeforeZip / 256);
            dataPacket[7] = (byte)(lenBeforeZip % 256);

            dataPacket[10] = (byte)(part1EnData.Length / 256);
            dataPacket[11] = (byte)(part1EnData.Length % 256);
            Array.Copy(part1EnData, 0, dataPacket, 12, part1EnData.Length);
            Array.Copy(aesData, 0, dataPacket, 12 + part1EnData.Length, aesData.Length);

            //构造数据包
            byte[] packet = ConstructPacket.AuthRequestPacket(lenBeforeZip, lenAfterZip, dataPacket, deviceID, 0x2bd);

            //发送数据包
           L1:
            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/manualauth";
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
            }
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[0] & 0x03) == 1;
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            /*******************************************************/
            byte[] _re = receivePacket;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            NewAuthResponse arReceive = NewAuthResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0 || arReceive.Base.Ret == -17)
            {
                uin = arReceive.Auth.Uin;
                userName = arReceive.User.UserName;
                ComputerECCKeyMD5(arReceive.Auth.SvrPubECDHKey.Key.Buffer.Take(57).ToArray(), 57, eccPri, 328, eccKey);
                sessionKey = DecryptPacket.DecryptReceivedPacket(arReceive.Auth.SessionKey.Buffer.Take(32).ToArray(), eccKey);
                hostUrl = arReceive.Server.NewHostList.ListList[1].Substitute;  
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
            }
            else if (arReceive.Base.Ret == -6)//需要验证码  -30激活
            {
                imgSid = arReceive.Auth.Str14;
                imgEncryptKey = arReceive.Auth.Str14;
            }
            else if (arReceive.Base.Ret == -30)//
            {
                ticket = arReceive.Auth.Str16;
            }
            else if (arReceive.Base.Ret == -301)
            {
                hostUrl = arReceive.Server.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
                goto L1;
            }
            return arReceive;
        }

        internal NewVerifyPasswdResponse NewVerifyPass(string pass)
        {
            byte cmdid = 0xB6;
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            NewVerifyPasswdRequest nsrObj = GoogleProto.NewVerifyPasswd(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, pass);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0xb6, 0x180, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包            
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            NewVerifyPasswdResponse nsrReceive = NewVerifyPasswdResponse.ParseFrom(unzipPacket);

            return nsrReceive;
        }

        internal NewSetPasswdResponse NewSetPass(string newpass, string ticket, string authkey)
        {
            byte cmdid = 0xB4;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            NewSetPasswdRequest lrObj = GoogleProto.CreateNewSetPassRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, Fun.CumputeMD5(newpass), ticket, authkey);

            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);
            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0xb4, 0x17F, cookieToken, check);

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包            
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }
            //byte a = receivePacket[0x40];
            /*******************************************************/
            byte[] _re = receivePacket;
            receivePacket = _re;
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            //receivePacket = _re;

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            if (unzipPacket.Length < 1)
            {
                return null;
            }

            //反序列化数据包
            NewSetPasswdResponse lrReceive = NewSetPasswdResponse.ParseFrom(unzipPacket);
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive;
        }

        public NewAuthResponse AutoAuth(SKBuiltinBuffer_t autokey)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType);
            AutoAuthRequest arObj = GoogleProto.CreateAutoAuthRequestEntity(brObj, imei, deviceBrand, deviceModel, RELEASE,
                INCREMENTAL, DISPLAY, OSType, autokey);

            ECDHKey.Builder eb = new ECDHKey.Builder();
            eb.SetNID(713);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();

            byte[] eccData = new byte[57];
            byte[] eccPri = new byte[328];
            int l = GenerateECKey(713, eccData, eccPri);

            skbb.SetILen(eccData.Length);
            skbb.SetBuffer(ByteString.CopyFrom(eccData));
            eb.SetKey(skbb);
            ECDHKey cliPubECDHKey = eb.Build();
            AutoAuthRsaReqData AuthKey = GoogleProto.CreateAutoAuthKeyEntity(randomEncryKey, cliPubECDHKey);

            byte[] arData = arObj.ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            byte[] part1 = AuthKey.ToByteArray();
            int part1Len = part1.Length;
            byte[] part1ZipData = DeflateCompression.DeflateZip(part1);
            byte[] part1EnData = RSAEncryptData.RSAEncryptCoreData(part1);

            //加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, randomEncryKey);

            byte[] dataPacket = new byte[12 + aesData.Length + part1EnData.Length];
            dataPacket[2] = (byte)(part1.Length / 256);
            dataPacket[3] = (byte)(part1.Length % 256);

            dataPacket[6] = (byte)(lenBeforeZip / 256);
            dataPacket[7] = (byte)(lenBeforeZip % 256);

            dataPacket[10] = (byte)(part1EnData.Length / 256);
            dataPacket[11] = (byte)(part1EnData.Length % 256);
            Array.Copy(part1EnData, 0, dataPacket, 12, part1EnData.Length);
            Array.Copy(aesData, 0, dataPacket, 12 + part1EnData.Length, aesData.Length);

            //构造数据包
            byte[] packet = ConstructPacket.AuthRequestPacket(lenBeforeZip, lenAfterZip, dataPacket, deviceID, 0x2be);

            //发送数据包

        L1:
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/autoauth";
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[0] & 0x03) == 1;
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            /*******************************************************/
            byte[] _re = receivePacket;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            NewAuthResponse arReceive = NewAuthResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0 || arReceive.Base.Ret == -17)
            {
                uin = arReceive.Auth.Uin;
                userName = arReceive.User.UserName;
                ComputerECCKeyMD5(arReceive.Auth.SvrPubECDHKey.Key.Buffer.Take(57).ToArray(), 57, eccPri, 328, eccKey);
                sessionKey = DecryptPacket.DecryptReceivedPacket(arReceive.Auth.SessionKey.Buffer.Take(32).ToArray(), eccKey);
                string hostUrl = arReceive.Server.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
            }
            else if (arReceive.Base.Ret == -6)//需要验证码  -30激活
            {
                imgSid = arReceive.Auth.Str14;
                imgEncryptKey = arReceive.Auth.Str14;
            }
            else if (arReceive.Base.Ret == -30)//
            {
                ticket = arReceive.Auth.Str16;
            }
            else if (arReceive.Base.Ret == -301)
            {
                string hostUrl = arReceive.Server.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
                goto L1;
            }
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        public AuthResponse SendVerifyCode(string code)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType);
            AuthRequest arObj = GoogleProto.CreateAuthRequestEntity(brObj, WXAccount, WXPwd, imei, deviceBrand, deviceModel, randomEncryKey, imgSid, code, imgEncryptKey);

            byte[] arData = arObj.ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //加密
            byte[] rsaData = RSAEncryptData.RSAEncryptCoreData(zipData);

            //构造数据包
            byte[] packet = ConstructPacket.AuthRequestPacket(lenBeforeZip, lenAfterZip, rsaData, deviceID, 0x17c);

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/newauth";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[0] & 0x03) == 1;
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            byte[] _re = receivePacket;

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            AuthResponse arReceive = AuthResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0)
            {
                uin = arReceive.Uin;
                userName = arReceive.UserName.String;
                sessionKey = arReceive.SessionKey.ToByteArray().Take(16).ToArray();
                string hostUrl = arReceive.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
            }
            else if (arReceive.Base.Ret == -30)
            {
                ticket = arReceive.Ticket;
            }
            else if (arReceive.Base.Ret == -301)
            {
                IList<IPEnd> iplist = arReceive.BuiltinIPList.LongConnectIPListList;
                for (int i = 0; i < iplist.Count; i++)
                {
                    IPEnd ip = iplist[i];
                    string _ip = ip.IP.ToStringUtf8().Replace("\0", "");
                    ipList.Add(_ip);
                }
                hostUrl = arReceive.NewHostList.ListList[1].Substitute;
                Ini.WriteIniString("setup", "hostUrl", hostUrl);
                goto L1;
            }

            return arReceive;
        }

        public NewRegResponse NewReg(string nickName, string mobile, string tick)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType, 0);

            ECDHKey.Builder eb = new ECDHKey.Builder();
            eb.SetNID(713);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();

            byte[] eccData = new byte[57];
            byte[] eccPri = new byte[328];
            int l = GenerateECKey(713, eccData, eccPri);

            skbb.SetILen(eccData.Length);
            skbb.SetBuffer(ByteString.CopyFrom(eccData));
            eb.SetKey(skbb);
            ECDHKey cliPubECDHKey = eb.Build();

            NewRegRequest nrrBoj = GoogleProto.CreateNewRegRequestEntity(brObj, mobile, WXPwd, nickName, tick, randomEncryKey, cliPubECDHKey, 
                clientid, androidid, fingerprint, mac, regSession);
            Debug.Print(nrrBoj.ToString());
            byte[] nrrData = nrrBoj.ToByteArray();
            lenBeforeZip = nrrData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nrrBoj.ToByteArray());
            lenAfterZip = zipData.Length;

            //加密
            byte[] rsaData = RSAEncryptData.RSAEncryptNewReg(zipData);

            //构造数据包
            byte[] packet = ConstructPacket.RegRequestPacket(lenBeforeZip, lenAfterZip, rsaData, deviceID, 0x7E);

            //发送数据包
            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/newreg";
        L1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            byte CookieLen = (byte)(receivePacket[2] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 11, cookieToken, 0, CookieLen);

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            //if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            NewRegResponse arReceive = NewRegResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0)
            {
                Debug.Print(arReceive.ToString());
                uin = arReceive.Uin;
                userName = arReceive.UserName;
                sessionKey = System.Text.Encoding.Default.GetBytes(arReceive.SessionKey).Take(16).ToArray();
                
                //byte[] eccKey = new byte[16];
                ComputerECCKeyMD5(arReceive.SecAuthRegKeySect.SvrPubEcdhkey.Key.Buffer.Take(57).ToArray(), 57, eccPri, 328, eccKey);
                sessionKey = DecryptPacket.DecryptReceivedPacket(arReceive.SecAuthRegKeySect.SessionKey.Buffer.Take(32).ToArray(), eccKey);
                sessionKey = sessionKey.Take(16).ToArray();
            }

            return arReceive;
        }

        public NewInitResponse NewInit()
        {
            byte cmdid = 0x1B;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            NewInitRequest niqObj = GoogleProto.CreateNewInitRequestEntity(uin, Encoding.Default.GetString(sessionKey), userName, deviceID, OSType, initSyncKey, maxSyncKey);

            byte[] niqData = niqObj.ToByteArray();
            lenBeforeZip = niqData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, niqData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(niqObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x1B, 0x8B, cookieToken, check);            

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包            
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length-readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            NewInitResponse niReceive = NewInitResponse.ParseFrom(unzipPacket);

            if (niReceive.Base.Ret == 0)
            {
                initSyncKey = niReceive.CurrentSynckey.Buffer.ToByteArray();
                maxSyncKey = niReceive.MaxSynckey.Buffer.ToByteArray();
            }
            System.Diagnostics.Debug.Print(niReceive.ToString());

            return niReceive;
        }

        public NewSyncResponse NewSync()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;
            byte cmdid = 0x79;
            //SyncKey k1 = SyncKey.ParseFrom(initSyncKey);
            //string str1 = k1.ToString();
            //System.Diagnostics.Debug.Print(str1);

            //生成google对象
            NewSyncRequest nsrObj = GoogleProto.CreateNewSyncRequestEntity(initSyncKey);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x79, 0x8A, cookieToken, check);

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            NewSyncResponse nsrReceive = NewSyncResponse.ParseFrom(unzipPacket);

            if (nsrReceive.Ret == 0)
            {
                initSyncKey = nsrReceive.KeyBuf.Buffer.ToByteArray();
            }
            //   SyncKey k = SyncKey.ParseFrom(initSyncKey);
            // string str = k.ToString();
            //System.Diagnostics.Debug.Print(str);
            return nsrReceive;
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="sex">性别</param>
        /// <param name="province">省</param>
        /// <param name="city">城市</param>
        /// <param name="signature">个性签名</param>
        /// <returns></returns>
        public NewSyncResponse ModifyProfile(int sex, string province, string city, string signature)
        {
            byte cmdid = 0x79;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            NewSyncRequest nsrObj = GoogleProto.ModifyProfile(sex, province, city, signature, initSyncKey);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x79, 0x8A, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            NewSyncResponse nsrReceive = NewSyncResponse.ParseFrom(unzipPacket);

            if (nsrReceive.Ret == 0)
            {
                initSyncKey = nsrReceive.KeyBuf.Buffer.ToByteArray();
            }

            return nsrReceive;
        }

        public NewSyncResponse ModifyNickName(string nickName)
        {
            byte cmdid = 0x79;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            NewSyncRequest nsrObj = GoogleProto.ModifyProfile(nickName, initSyncKey);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x79, 0x8A, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            NewSyncResponse nsrReceive = NewSyncResponse.ParseFrom(unzipPacket);

            if (nsrReceive.Ret == 0)
            {
                initSyncKey = nsrReceive.KeyBuf.Buffer.ToByteArray();
            }

            return nsrReceive;
        }
        public LBSFindResponse LBSFind(float weidu, float jingdu, int sex)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;
            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/lbsfind";//"http://short.weixin.qq.com/cgi-bin/micromsg-bin/lbsfind";
            //生成google对象
            LBSFindRequest lbsObj = GoogleProto.CreateLBSFindRequestEntity(Encoding.Default.GetString(sessionKey), uin, weidu, jingdu, deviceID, OSType, sex);
            System.Diagnostics.Debug.Print(lbsObj.ToString());
            byte[] nsrData = lbsObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lbsObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x79, 0x94, cookieToken, check);

        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                unzipPacket = DeflateCompression.DeflateLBSUnzip(aesPacket);
            }

            //反序列化数据包
            LBSFindResponse lbsReceive = LBSFindResponse.ParseFrom(unzipPacket);

            return lbsReceive;
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="imgPath">头像路径</param>
        /// <returns></returns>
        public UploadhdheadimgResponse UploadHeadImg(string imgPath)
        {
            UploadhdheadimgResponse urObj = null;
            int blockLen = 1024 * 8;

            byte[] imgByte = System.IO.File.ReadAllBytes(imgPath);

            int totalLen = imgByte.Length;
            int startPos = 0;

            List<byte[]> list = SplitBuffer(imgByte, blockLen);

            for (int i = 0; i < list.Count; i++)
            {
                urObj = UploadHeadImgBlock(totalLen, startPos, list[i]);
                startPos += blockLen;
            }

            return urObj;
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="totalLen"></param>
        /// <param name="startPos"></param>
        /// <param name="blockImgBuffer"></param>
        /// <returns></returns>
        UploadhdheadimgResponse UploadHeadImgBlock(int totalLen, int startPos, byte[] blockImgBuffer)
        {
            byte cmdid = 0x2e;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            UploadhdheadimgRequest urObj = GoogleProto.CreateUploadhdheadimgRequestEntity(Encoding.Default.GetString(sessionKey), uin, totalLen, startPos, blockImgBuffer, deviceID, OSType);

            byte[] urData = urObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(urObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x2E, 0x9D, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[01] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            UploadhdheadimgResponse urReceive = UploadhdheadimgResponse.ParseFrom(unzipPacket);

            return urReceive;
        }

        /// <summary>
        /// 上传相册或者朋友圈图片
        /// </summary>
        /// <param name="imgPath">头像路径</param>
        /// <param name="Description">发送的消息</param>
        /// <returns></returns>
        public MmsnsuploadResponse MMSUploadImg(string imgPath, string ranMd5, string Description, ref int totalLen)
        {
            MmsnsuploadResponse mlr = null;
            int blockLen = 1024 * 8;

            byte[] imgByte = System.IO.File.ReadAllBytes(imgPath);

            totalLen = imgByte.Length;
            int startPos = 0;

            List<byte[]> list = SplitBuffer(imgByte, blockLen);

            for (int i = 0; i < list.Count; i++)
            {
                mlr = UploadTwitterImgBlock(totalLen, startPos, list[i], ranMd5, Description);
                startPos += blockLen;
            }

            return mlr;
        }
        /// <summary>
        /// 上传相册或者朋友圈图片
        /// </summary>
        /// <param name="totalLen"></param>
        /// <param name="startPos"></param>
        /// <param name="blockImgBuffer"></param>
        /// <returns></returns>
        MmsnsuploadResponse UploadTwitterImgBlock(int totalLen, int startPos, byte[] blockImgBuffer, string md5r, string mes)
        {
            byte cmdid = 0x5f;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            MmsnsuploadRequest urObj = GoogleProto.CreateUploadTwitterImgRequestEntity(Encoding.Default.GetString(sessionKey), uin, totalLen, startPos, blockImgBuffer, deviceID, OSType, md5r, mes);

            System.Diagnostics.Debug.Print(urObj.ToString());

            byte[] urData = urObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff); 

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(urObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x5F, 0xCF, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Length == 0)
            {
                return null;
            }
            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (aesPacket[0] == 0x78 && aesPacket[1] == 0x9c)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            MmsnsuploadResponse urReceive = MmsnsuploadResponse.ParseFrom(unzipPacket);

            return urReceive;
        }
        /// <summary>
        /// 发送朋友圈消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="Description">发送的消息</param>
        /// <param name="desLen"></param>
        /// <returns></returns>
        public MMSnsPostResponse MMSPost(string clientId, string Desc, string imgUrl0, string imgUrl150, int totalLen, int imgH, int imgW)
        {
            byte cmdid = 0x61;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType);
            string DescHtml = "<TimelineObject><id><![CDATA[0]]></id><username><![CDATA[" + userName + "]]></username><createTime><![CDATA[0]]></createTime><private><![CDATA[0]]></private><contentDesc><![CDATA[" + Desc + "]]></contentDesc><sourceUserName></sourceUserName><sourceNickName></sourceNickName><location longitude =  \"0.0\"  city =  \"\"  latitude =  \"0.0\" ></location><ContentObject><contentStyle><![CDATA[1]]></contentStyle><title></title><description></description><contentUrl></contentUrl><mediaList><media><id><![CDATA[0]]></id><type><![CDATA[2]]></type><title></title><description></description><private><![CDATA[0]]></private><url type =  \"1\" ><![CDATA[" + imgUrl0 + "]]></url><thumb type =  \"1\" ><![CDATA[" + imgUrl150 + "]]></thumb><size totalSize =  \"" + totalLen + ".0\"  height =  \"" + imgH + ".0\"  width =  \"" + imgW + ".0\" ></size></media></mediaList></ContentObject></TimelineObject>";

            MMSnsPostRequest arObj = GoogleProto.CreateSendTwitterRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, clientId, DescHtml);

            System.Diagnostics.Debug.Print(arObj.ToString());

            byte[] urData = arObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x61, 0xD1, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            MMSnsPostResponse urReceive = MMSnsPostResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(urReceive.ToString());

            return urReceive;
        }
        //发好友消息
        public NewSendMsgResponse SendFriendMsg(uint ClientMsgId, string Content, uint CreateTime, string ToUserName, uint msgType)
        {
            byte cmdid = 0xed;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            NewSendMsgRequest arObj = GoogleProto.CreateSendMsgRequestEntity(Content, CreateTime, ClientMsgId, ToUserName, msgType);

            System.Diagnostics.Debug.Print(arObj.ToString());

            byte[] urData = arObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0xED, 0x020A, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            //反序列化
            NewSendMsgResponse urReceive = NewSendMsgResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(urReceive.ToString());

            return urReceive;
        }

        public LogoutResponse Logout()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            LogoutRequest lrObj = GoogleProto.CreateLogoutRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType);

            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x0, 0xFF, cookieToken, check);

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/queryhaspasswd";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            LogoutResponse lrReceive = LogoutResponse.ParseFrom(unzipPacket);

            return lrReceive;
        }

        List<byte[]> SplitBuffer(byte[] buffer, int blockLength)
        {
            List<byte[]> list = new List<byte[]>();

            int offset = 0;

            while (buffer.Length - offset > 0)
            {
                byte[] tmp = buffer.Take(offset + blockLength).Skip(offset).ToArray();
                list.Add(tmp);

                offset += blockLength;
            }

            return list;
        }

        int variantSkip(byte[] a)
        {
            int result = 0;
            foreach (byte item in a)
            {
                result++;
                if (item < 128)
                {
                    break;
                }
            }

            return result;
        }

        private byte[] GenRandomEncryKey()
        {
            byte[] randomKey = new byte[] { 0x8e, 0x61, 0x93, 0x89, 0xe8, 0x99, 0x42, 0x1d, 0x07, 0x74, 0xF0, 0x09, 0x36, 0x5e, 0x4b, 0x1f };
            return randomKey;
        }
        /// <summary>
        /// 微信登录名
        /// </summary>
        public string WXAccount
        {
            get;
            set;
        }
        /// <summary>
        /// 微信密码
        /// </summary>
        public string WXPwd
        {
            get;
            set;
        }
        public byte[] RandomEncryKey
        {
            get
            {
                return randomEncryKey;
            }
        }

        /// <summary>
        /// 打招呼的人通过验证 和 向附近人打招呼
        /// </summary>
        /// <param name="weidu"></param>
        /// <param name="jingdu"></param>
        /// <returns></returns>
        public VerifyUserResponse VerifyUser(string strangerUserName, string ticket, int opCode, string scene, string content)
        {
            byte cmdid = 0x2c;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;
            //生成google对象
            VerifyUserRequest vurObj = GoogleProto.CreateVerifyUserRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, strangerUserName, ticket, opCode, scene, content);
            Debug.Print(vurObj.ToString());
            byte[] nsrData = vurObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(vurObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x22, 0x89, cookieToken, check);

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            /*******************************************************/
            byte[] _re = receivePacket;
            //先要去掉http头协议数据。找到be 81
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();


            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }
            //反序列化数据包
            VerifyUserResponse vurReceive = VerifyUserResponse.ParseFrom(unzipPacket);

            return vurReceive;
        }

        UploadMsgImgResponse UploadMsgImgBlock(string clientID, string toUser, int totalLen, int startPos, byte[] blockImgBuffer)
        {
            byte cmdid = 0x09;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            UploadMsgImgRequest urObj = GoogleProto.CreateUploadMsgImgRequestEntity(Encoding.Default.GetString(sessionKey), uin, totalLen, startPos, blockImgBuffer, deviceID, OSType,
                                    clientID, userName, toUser);

            byte[] urData = urObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(urObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x09, 0x6E, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[01] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            UploadMsgImgResponse urReceive = UploadMsgImgResponse.ParseFrom(unzipPacket);

            return urReceive;
        }

        internal SearchContactResponse SearchOne(string peer)
        {
            byte cmdid = 0x22;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //SyncKey k1 = SyncKey.ParseFrom(initSyncKey);
            //string str1 = k1.ToString();
            //System.Diagnostics.Debug.Print(str1);

            //生成google对象
            SearchContactRequest nsrObj = GoogleProto.CreateSearchContactEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, peer);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);
            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 34, 106, cookieToken, check);

            //发送数据包
            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            SearchContactResponse nsrReceive = SearchContactResponse.ParseFrom(unzipPacket);
            
            return nsrReceive;
        }

        internal BindQQResponse BindQQ(string p1, string p2)
        {
            throw new NotImplementedException();
        }

        internal GeneralSetResponse SetWXID(string wxID)
        {
            int lenBeforeZip = 0;
            int lenAfterZip = 0;
            //生成google对象
            GeneralSetRequest nsrObj = GoogleProto.CreateSetIDEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, wxID);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0, 0xB1, cookieToken, check);

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/generalset";
        L1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            GeneralSetResponse nsrReceive = GeneralSetResponse.ParseFrom(unzipPacket);

            return nsrReceive;
        }

        public BindEmailResponse BingEmail(string Email)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            BindEmailRequest berObj = GoogleProto.BindEmailEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, Email);
            byte[] nsrData = berObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(berObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x22, 0x100, cookieToken, check);

            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/bindemail";
            WebClient wc = new WebClient();
        L2:
            // byte[] receivePacket = wc.UploadData(url, "POST", packet);
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto L2;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            if (aesPacket == null || aesPacket.Count() == 0)
            {
                return null;
            }
            //反序列化数据包
            BindEmailResponse berReceive = BindEmailResponse.ParseFrom(aesPacket);
            Debug.Print(berReceive.ToString());
            return berReceive;
        }

        public BindOpMobileResponse BindMobile(string mobile, string deviceName, string deviceType)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BindOpMobileRequest lrObj = GoogleProto.CreateBindMobileRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, mobile, deviceName, deviceType);
            //System.Diagnostics.Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x84, 0x84, cookieToken, check);

            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/bindopmobile";
        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (aesPacket[0] == 0x78 && aesPacket[1] == 0x9c)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            BindOpMobileResponse lrReceive = BindOpMobileResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive;
        }

        public BindQQResponse BindQQ(string qq, string pass, string deviceName, string deviceType)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BindQQRequest lrObj = GoogleProto.CreateBindMobileRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, qq, pass, deviceName, deviceType);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x90, 0x90, cookieToken, check);

            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/bindqq";
        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            BindQQResponse lrReceive = BindQQResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive;
        }

        internal UploadMContactResponse UploadMContact(string mobile, List<string> contacts)
        {
            int lenBeforeZip = 0;
            int lenAfterZip = 0;
            for(Int64 i=13803111230; i<13803111250; i++)
            {
                contacts.Add(string.Format("{0}", i));
            }

            //生成google对象
            UploadMContact nsrObj = GoogleProto.UploadMContact(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, mobile, contacts, userName);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, nsrData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(nsrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x85, 0x85, cookieToken, check);

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/uploadmcontact";
        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            UploadMContactResponse nsrReceive = UploadMContactResponse.ParseFrom(unzipPacket);

            return nsrReceive;
        }

        public BindopMobileForRegResponse MobileReg(int opCode, string mobile, string verifyCode)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType, 0);
            BindopMobileForRegRequest arObj = GoogleProto.CreateMobileRegPacket(brObj, opCode, mobile, verifyCode, randomEncryKey, devicetype, clientid, regSession);
            Debug.Print(arObj.ToString());

            byte[] arData = arObj.ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //加密
            byte[] rsaData = RSAEncryptData.RSAEncryptNewReg(zipData);

            //构造数据包
            byte[] packet = ConstructPacket.RegRequestPacket(lenBeforeZip, lenAfterZip, rsaData, deviceID, 0x91);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/bindopmobileforreg";
        l1:
            //WebClient wc = new WebClient();
            byte[] receivePacket = null;
            //try
            //{
            //    receivePacket = wc.UploadData(url, "POST", packet);
            //}
            //catch (Exception)
            //{                Thread.Sleep(1000 * 3);
            //    goto l1;
            //}
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.ContentType = "application/octet-stream";
                httpWebRequest.UserAgent = "MicroMessenger Client";
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 20000;

                httpWebRequest.ContentLength = packet.Length;
                httpWebRequest.GetRequestStream().Write(packet, 0, packet.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string cl = httpWebResponse.Headers["Content-Length"];
                int conLen = int.Parse(cl);

                Stream streamReader = httpWebResponse.GetResponseStream();
                receivePacket = new byte[conLen];

                int readed = streamReader.Read(receivePacket, 0, conLen);


                streamReader.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();

            }
            catch (Exception ex)
            {

            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null)
            {
                goto l1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            //反序列化数据包
            BindopMobileForRegResponse arReceive = BindopMobileForRegResponse.ParseFrom(unzipPacket);
            //System.Diagnostics.Debug.Print(arReceive.ToString());

            if (arReceive.Base.Ret == -301)
            {
                hostUrl = arReceive.NewHostList.ListList[1].Substitute;
            }
            if (arReceive.HasRegSessionID)
            {
                regSession = arReceive.RegSessionID;
            }
            //Debug.Print(arReceive.ToString());
            return arReceive;
        }
        internal ThrowBottleResponse ThrowBottle(string text)
        {
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            ThrowBottleRequest lrObj = GoogleProto.CreateThrowBottleRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, text);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x9A, cookieToken, check);

            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/throwbottle";
        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            ThrowBottleResponse lrReceive = ThrowBottleResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive;
        }
        SKBuiltinBuffer_t shakeBuffer;
        internal ShakereportResponse ShakeReprot(float latitude, float longitude)
        {
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            ShakereportRequest lrObj = GoogleProto.CreateShakeReportRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, latitude, longitude);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0xA1, cookieToken, check);

            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/shakereport";
        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            ShakereportResponse lrReceive = ShakereportResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());
            if (lrReceive.Base.Ret == 0)
            {
                shakeBuffer = lrReceive.Buffer;
            }

            return lrReceive;
        }

        internal ShakegetResponse ShakeGet()
        {
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            ShakegetRequest lrObj = GoogleProto.CreateShakeGetRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, shakeBuffer);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0xA2, cookieToken, check);

            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/shakeget";
        l1:
            WebClient wc = new WebClient();
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception)
            {
                Thread.Sleep(1000 * 3);
                goto l1;
            }
            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            ShakegetResponse lrReceive = ShakegetResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive;
        }
        internal GetLoginQRCodeResponse GetQRCode()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType, 0);
            GetLoginQRCodeRequest.Builder glb = new GetLoginQRCodeRequest.Builder();
            glb.SetBase(brObj);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));
            glb.SetRandomEncryKey(skbb);
            glb.SetOpcode(0);

            byte[] arData = glb.Build().ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arData);
            lenAfterZip = zipData.Length;

            byte[] part1EnData = RSAEncryptData.RSAEncryptCoreData(zipData);            

            //构造数据包
            byte[] packet = ConstructPacket.QRCodeRequestPacket(lenBeforeZip, lenAfterZip, part1EnData, deviceID, 0x1F6);

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/getloginqrcode";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            /*******************************************************/
            byte[] _re = receivePacket;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);

            //反序列化数据包
            GetLoginQRCodeResponse arReceive = GetLoginQRCodeResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0)
            {
                uuid = arReceive.UUID;
                notifykey = arReceive.NotifyKey.Buffer.ToByteArray();
            }
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal CheckLoginQRCodeResponse CheckQRCode()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            BaseRequest brObj = GoogleProto.CreateBaseRequestEntity(deviceID, OSType, 0);
            CheckLoginQRCodeRequest.Builder glb = new CheckLoginQRCodeRequest.Builder();
            glb.SetBase(brObj);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));
            glb.SetRandomEncryKey(skbb);
            glb.SetUuid(uuid);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            System.TimeSpan sp = DateTime.Now - startTime;
            UInt32 unix = (UInt32)sp.TotalSeconds;

            glb.SetTimeStamp(unix);
            glb.SetOpcode(0);

            byte[] arData = glb.Build().ToByteArray();
            lenBeforeZip = arData.Length;

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(arData);
            lenAfterZip = zipData.Length;

            byte[] part1EnData = RSAEncryptData.RSAEncryptCoreData(zipData);

            //构造数据包
            byte[] packet = ConstructPacket.QRCodeRequestPacket(lenBeforeZip, lenAfterZip, part1EnData, deviceID, 0x1F7);

            //发送数据包
            string url = "http://" + Ini.GetIniString("setup", "hostUrl", "", 256).Trim() + "/cgi-bin/micromsg-bin/checkloginqrcode";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }
            
            byte HeadLen = receivePacket[0];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;
            byte CookieLen = (byte)(receivePacket[1] % 16);
            cookieToken = new byte[CookieLen];

            Array.Copy(receivePacket, 10, cookieToken, 0, CookieLen);
            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, randomEncryKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            CheckLoginQRCodeResponse arReceive = CheckLoginQRCodeResponse.ParseFrom(unzipPacket);

            if (arReceive.Base.Ret == 0)
            {
                byte[] notify = DecryptPacket.DecryptReceivedPacket(arReceive.NotifyPkg.NotifyData.Buffer.ToByteArray(), notifykey);
            }
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal void SaveSession(string path)
        {
            string tokenStr = Fun.toHexStr(cookieToken);
            string sessionStr = Fun.toHexStr(sessionKey);
            StreamWriter sw = new StreamWriter(path);
            string con = string.Format("{0}----{1}----{2}----{3}----{4}----{5}----{6}----{7}", WXAccount, sessionStr, tokenStr, uin, userName, longServerName, deviceID, OSType);
            sw.Write(con);
            sw.Close();            
        }

        internal void LoadSession(string path)
        {
            string con = File.ReadAllText(path);
            string[] items = con.Split(new string[] {"----"}, StringSplitOptions.RemoveEmptyEntries);
            WXAccount = items[0];
            sessionKey = Fun.toBin(items[1]);
            cookieToken = Fun.toBin(items[2]);
            uin = (uint)long.Parse(items[3]);
            userName = items[4];
            longServerName = items[5];
            deviceID = items[6];
            OSType = items[7];
            client = new TcpClient();
            client.Connect(longServerName, 8080);
        }

        internal UploadMsgImgResponse UploadMsgImg(string imgPath, string toUser)
        {
            UploadMsgImgResponse urObj = null;
            int blockLen = 1024 * 8;
            string clientID = Fun.CumputeMD5(DateTime.Now.ToLongTimeString());

            byte[] imgByte = System.IO.File.ReadAllBytes(imgPath);

            int totalLen = imgByte.Length;
            int startPos = 0;

            List<byte[]> list = SplitBuffer(imgByte, blockLen);

            for (int i = 0; i < list.Count; i++)
            {
                urObj = UploadMsgImgBlock(clientID, toUser, totalLen, startPos, list[i]);
                startPos += blockLen;
            }

            return urObj;
        }
        internal OplogResponse OpLog(int cmdid, string removeObj)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            OplogRequest lrObj = GoogleProto.CreateOpLogRequestEntity(cmdid, removeObj);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x2a9, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/oplog";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;
            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            OplogResponse arReceive = OplogResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive; throw new NotImplementedException();
        }
        //
        internal OplogResponse OpExitChatroom(int cmdid, string chatroom, string self)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            OplogRequest lrObj = GoogleProto.CreateExitChatroomRequestEntity(cmdid, chatroom, self);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x2a9, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/oplog";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;
            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            OplogResponse arReceive = OplogResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive; throw new NotImplementedException();
        }

        internal OplogResponse OpSetCheck(int cmdid, int key, int value)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            OplogRequest lrObj = GoogleProto.CreateOpSetCheckRequestEntity(cmdid, key, value);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x2a9, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/oplog";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;
            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            OplogResponse arReceive = OplogResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive; throw new NotImplementedException();
        }
        //00 00 00 10 00 10 00 01 00 00 00 06 00 00 00 2e
        internal CreateChatRoomResponse CreateChatroom(List<string> memList)
        {
            byte cmdid = 0x25;
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            CreateChatRoomRequest lrObj = GoogleProto.CreateChatroomRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, memList);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0xb9, 0x77, cookieToken, check);

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            CreateChatRoomResponse lrReceive = CreateChatRoomResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive; 
        }

        internal AddChatRoomMemberResponse AddChatroomMember(string chatroomName, List<string> memList)
        {
            byte cmdid = 0x24;
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            AddChatRoomMemberRequest lrObj = GoogleProto.CreateChatroomMemRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, chatroomName, memList);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0xb9, 0x78, cookieToken, check);

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            AddChatRoomMemberResponse lrReceive = AddChatRoomMemberResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive;
        }

        internal GetChatRoomMemberDetailResponse GetChatroomMemberList(string chatroomName)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            GetChatRoomMemberDetailRequest lrObj = GoogleProto.CreateGetChatroomMemberListRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, chatroomName);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x0227, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/getchatroommemberdetail";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;
            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            GetChatRoomMemberDetailResponse arReceive = GetChatRoomMemberDetailResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive; throw new NotImplementedException();
        }

        internal Geta8keyResponse GetOAuth(string reqUrl)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            Geta8keyRequest lrObj = GoogleProto.CreateGetOAuthRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, reqUrl);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0xe9, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/geta8key";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            Geta8keyResponse arReceive = Geta8keyResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }
        //00 00 00 10 00 10 00 01 00 00 00 06 00 00 00 2e
        internal void HeartBeat()
        {
            byte cmdid = 0x06;
            byte[] abc = new byte[16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }
        }

        internal UploadvoiceResponse SendVoiceMsg(string toUserName, string voicePath)
        {
            byte cmdid = 0x13;
            int lenBeforeZip = 0;
            int lenAfterZip = 0;
            byte[] voiceData = File.ReadAllBytes(voicePath);
            string msgID = string.Format("{0}{1}_{2}", toUserName, GetCurTime()%100, GetCurTime()/1000); ;
            //生成google对象
            UploadvoiceRequest lrObj = GoogleProto.CreateVoiceMsgRequestEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, toUserName, userName, voiceData, msgID);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x13, 0x7F, cookieToken, check);

            byte[] abc = new byte[packet.Length + 16];
            seq++;
            abc[0] = 0x00;
            abc[1] = 0x00;
            abc[2] = (byte)(abc.Length / 256);
            abc[3] = (byte)(abc.Length % 256);

            abc[4] = 0x00;
            abc[5] = 0x10;
            abc[6] = 0x00;
            abc[7] = 0x01;

            abc[8] = 0x00;
            abc[9] = 0x00;
            abc[10] = 0x00;
            abc[11] = cmdid;

            abc[12] = 0x00;
            abc[13] = 0x00;
            abc[14] = (byte)(seq / 256);
            abc[15] = (byte)(seq % 256);
            Array.Copy(packet, 0, abc, 16, packet.Length);

            //发送数据包          
            NetworkStream ns = client.GetStream();
            ns.Write(abc, 0, abc.Length);
        NEXTPACKET:
            byte[] receivePacket = new byte[16];
            int readed = ns.Read(receivePacket, 0, receivePacket.Length);
            Debug.Print(string.Format("readed:{0}", readed));
            byte replyCmd = receivePacket[11];
            int toRead = receivePacket[2] * 256 + receivePacket[3] - 16;
            receivePacket = new byte[toRead];
            readed = 0;
            while (readed < toRead)
            {
                int oneRead = ns.Read(receivePacket, readed, receivePacket.Length - readed);
                readed += oneRead;
            }
            Debug.Print(string.Format("toRead{0}, readed:{1}", toRead, readed));
            if (replyCmd != cmdid)
            {
                goto NEXTPACKET;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            //先要去掉http头协议数据。找到be 81

            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();

            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);

            //特殊处理：如果返回的数据包可能会比较少，需要解压。就判断前两位是不是78 9c
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            UploadvoiceResponse lrReceive = UploadvoiceResponse.ParseFrom(unzipPacket);
            Debug.Print(lrReceive.ToString());
            //System.Diagnostics.Debug.Print(lrReceive.ToString());

            return lrReceive; 
        }

        internal GetContactResponse GetContact(string peer)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            GetContactRequest lrObj = GoogleProto.CreateGetContactEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, peer);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0xb6, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/getcontact";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            GetContactResponse arReceive = GetContactResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal ExtDeviceLoginConfirmGetResponse ExtDevLogin(string reqUrl)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            ExtDeviceLoginConfirmGetRequest lrObj = GoogleProto.CreateGetExtDevConfirmEntity(reqUrl);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x3cb, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/extdeviceloginconfirmget";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            ExtDeviceLoginConfirmGetResponse arReceive = ExtDeviceLoginConfirmGetResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal ExtDeviceLoginConfirmOKResponse ExtDevLoginOK(string reqUrl)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            ExtDeviceLoginConfirmOKRequest lrObj = GoogleProto.CreateGetExtDevConfirmOKEntity(reqUrl);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x3cc, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/extdeviceloginconfirmok";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            ExtDeviceLoginConfirmOKResponse arReceive = ExtDeviceLoginConfirmOKResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal LogOutWebWxResponse LogoutWeb()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            LogOutWebWxRequest lrObj = GoogleProto.CreateLogoutWebEntity(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x00, 0x119, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/logoutwebwx";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            LogOutWebWxResponse arReceive = LogOutWebWxResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal UploadMContactResponse UploadMContact(List<string> contacts)
        {
            int tryTimes = 0;
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            UploadMContact lrObj = GoogleProto.UploadMContact(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, WXAccount, contacts, userName);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x85, 0x85, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/uploadmcontact";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                if (++tryTimes >= 3)
                {
                    return null;
                }
                else
                {
                    goto L1;
                }
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            UploadMContactResponse arReceive = UploadMContactResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal GetMFriendResponse GetMFriend()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            GetMFriendRequest lrObj = GoogleProto.GetMFriend(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x20, 0x8E, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/getmfriend";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            GetMFriendResponse arReceive = GetMFriendResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal GetSafetyInfoRespsonse LoginDevice()
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            GetSafetyInfoRequest lrObj = GoogleProto.GetSafetyInfo(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x20, 0x352, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/getsafetyinfo";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            GetSafetyInfoRespsonse arReceive = GetSafetyInfoRespsonse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }

        internal MmsnsuserpageResponse UserPage(ulong maxid)
        {
            //userName = "v1_331af381e7de4de2f067eb11673b7d7e77f87adca12b60c04764692c42557356d9155d25ee744daee042b1d4be4d9f8d@stranger";
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            MmsnsuserpageRequest lrObj = GoogleProto.CreateUserPage(Encoding.Default.GetString(sessionKey), uin, deviceID, OSType, userName, maxid);
            Debug.Print(lrObj.ToString());
            byte[] urData = lrObj.ToByteArray();
            lenBeforeZip = urData.Length;

            //计算校验值
            byte[] byteInt = new byte[4];
            byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Fun.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Fun.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, urData, lenBeforeZip);

            //压缩
            byte[] zipData = DeflateCompression.DeflateZip(lrObj.ToByteArray());
            lenAfterZip = zipData.Length;

            //aes加密
            byte[] aesData = DecryptPacket.AESEncryptorData(zipData, sessionKey);

            //构造数据包
            byte[] packet = ConstructPacket.CommonRequestPacket(lenBeforeZip, lenAfterZip, aesData, uin, deviceID, 0x20, 0xd4, cookieToken, check);

            string url = "http://" + hostUrl + "/cgi-bin/micromsg-bin/mmsnsuserpage";
        L1:
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MicroMessenger Client");
            wc.Headers.Add("Content-Type", "application/octet-stream");
            byte[] receivePacket = null;
            try
            {
                receivePacket = wc.UploadData(url, "POST", packet);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000 * 3);
                goto L1;
            }

            byte HeadLen = receivePacket[1];
            HeadLen = (byte)(HeadLen >> 2);
            bool zip = (receivePacket[1] & 0x03) == 1;

            /*******************************************************/
            byte[] _re = receivePacket;
            //receivePacket = _re;
            receivePacket = receivePacket.Take(receivePacket.Length).Skip(HeadLen).ToArray();
            //解密返回数据包
            byte[] aesPacket = DecryptPacket.DecryptReceivedPacket(receivePacket, sessionKey);
            if (aesPacket == null || aesPacket.Count() == 0)
            {
                goto L1;
            }
            //解压缩返回数据包
            byte[] unzipPacket = aesPacket;
            if (zip)
            {
                //解压缩返回数据包
                unzipPacket = DeflateCompression.DeflateUnZip(aesPacket);
            }

            //反序列化数据包
            MmsnsuserpageResponse arReceive = MmsnsuserpageResponse.ParseFrom(unzipPacket);
            System.Diagnostics.Debug.Print(arReceive.ToString());
            return arReceive;
        }
    }
}
