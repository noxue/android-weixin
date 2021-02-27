using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MMPro.MM;
using System.IO;
using System.Runtime.InteropServices;
using aliyun;
using System.Diagnostics;
using Newtonsoft.Json;
using Google.ProtocolBuffers;


namespace Wchat
{
    public class Xcode
    {
        [DllImport("Common.dll")]
        private static extern int GenerateECKey(int nid, byte[] pub, byte[] pri);

        [DllImport("Common.dll")]
        public static extern int ComputerECCKeyMD5(byte[] pub, int pubLen, byte[] pri, int priLen, byte[] eccKey);

        [DllImport("Common.dll")]
        private static extern uint Adler32(uint adler, byte[] buf, int len);

        public BaseRequest baseRequest { set; get; }
        //16位 AEs秘钥
        public string AESKey { set; get; }

        public byte[] pri_key_buf = new byte[328];
        public byte[] pub_key_buf = new byte[57];

        //用户cookie
        string cookie { set; get; }
        //uin
        public Int32 m_uid { set; get; }
        public byte[] initSyncKey { get; private set; }
        public byte[] maxSyncKey { get; private set; }

        public string devicelId = "49ba7db2f4a3ffe0e96218f6b92cde15";

        //版本号
        Int32 ver = 369558056;
                    
  
                    

        //RSA秘钥版本
        UInt32 LOGIN_RSA_VER = 174;

        string synckey = "";
        public static byte[] Dword2String(UInt32 dw)
        {
            List<byte> apcBuffer = new List<byte>();

            while (dw >= 0x80)
            {

                apcBuffer.Add((byte)(0x80 + (dw & 0x7f)));
                dw = dw >> 7;
            }
            apcBuffer.Add((byte)dw);
            return apcBuffer.ToArray();
            //Int32 dwData = dw;
            //Int32 dwData2 = 0x80 * 0x80 * 0x80 * 0x80;
            //int nLen = 4;
            //byte[] hex = new byte[5];
            //Int32 dwOutLen = 0;
            //while (nLen > 0)
            //{
            //    if (dwData > dwData2)
            //    {
            //        hex[nLen] = (byte)(dwData / dwData2);
            //        dwData = dwData % dwData2;
            //        dwOutLen++;
            //    }

            //    dwData2 /= 0x80;
            //    nLen--;
            //}

            //hex[0] = (byte)dwData;

            //dwOutLen++;

            //for (int i = 0; i < (int)(dwOutLen - 1); i++)
            //{
            //    hex[i] += 0x80;
            //}

            //return hex;
        }
        public static int DecodeVByte32(ref int apuValue, byte[] apcBuffer, int off)
        {
            int num3;
            int num = 0;
            int num2 = 0;
            int num4 = 0;
            byte num5 = apcBuffer[off + num++];
            while ((num5 & 0xff) >= 0x80)
            {
                num3 = num5 & 0x7f;
                num4 += num3 << num2;
                num2 += 7;
                num5 = apcBuffer[off + num++];
            }
            num3 = num5;
            num4 += num3 << num2;
            apuValue = num4;
            return num;
        }
        //组包体头
        byte[] MakeHead(int cgi, int nLenProtobuf, byte encodetypr = 7, bool iscookie = false, bool isuin = false)
        {

            List<byte> strHeader = new List<byte>();
            int nCur = 0;
            byte SecondByte = 0x2;
            strHeader.Add(SecondByte);
            nCur++;
            //加密算法(前4bits),RSA加密(7)AES(5)
            byte ThirdByte = (byte)(encodetypr << 4);
            if (iscookie)
                ThirdByte += 0xf;
            strHeader.Add((byte)ThirdByte);
            nCur++;
            int dwUin = 0;
            if (isuin)
                dwUin = m_uid;
            strHeader = strHeader.Concat(ver.ToByteArray(Endian.Big).ToList()).ToList();
            nCur += 4;

            strHeader = strHeader.Concat(dwUin.ToByteArray(Endian.Big).ToList()).ToList();
            nCur += 4;

            if (iscookie)
            {
                //登录包不需要cookie 全0占位即可
                if (cookie == null || cookie == "" || cookie.Length < 0xf)
                {
                    strHeader = strHeader.Concat(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }).ToList();
                    nCur += 15;
                }
                else
                {
                    strHeader = strHeader.Concat(cookie.ToByteArray(16, 2).ToList()).ToList();
                    nCur += 15;
                }
            }
            byte[] strcgi = Dword2String((UInt32)cgi);
            strHeader = strHeader.Concat(strcgi.ToList()).ToList();
            nCur += strcgi.Length;
            byte[] strLenProtobuf = Dword2String((UInt32)nLenProtobuf);
            strHeader = strHeader.Concat(strLenProtobuf.ToList()).ToList();
            nCur += strLenProtobuf.Length;
            byte[] strLenCompressed = Dword2String((UInt32)nLenProtobuf);
            strHeader = strHeader.Concat(strLenCompressed.ToList()).ToList();
            nCur += strLenCompressed.Length;
            var rsaVer = Dword2String((UInt32)LOGIN_RSA_VER);
            strHeader = strHeader.Concat(rsaVer).ToList();
            nCur += rsaVer.Length;
            strHeader = strHeader.Concat(new byte[] { 1,2}.ToList()).ToList();
            nCur += 2;

            //var unkwnow = (9).ToByteArray(Endian.Little).Copy(2);// "0100".ToByteArray(16, 2);
            //strHeader = strHeader.Concat(unkwnow.ToList()).ToList();
            //nCur += unkwnow.Length;
            nCur++;
            SecondByte += (byte)(nCur << 2);
            strHeader[0] = SecondByte;

            strHeader.Insert(0, 0xbf);
            return strHeader.ToArray();


        }



        /// <summary>
        /// 得到AES 秘钥 如果是空的就生成一个16位的随机秘钥
        /// </summary>
        /// <returns></returns>
        public byte[] GetAESkey() {
            if (string.IsNullOrEmpty(AESKey))
                AESKey = (new Random()).NextBytes(16).ToString(16, 2);
            return AESKey.ToByteArray(16, 2);
        }
        public GetLoginQRCodeResponse GetLoginQRcode() {
            GetLoginQRCodeResponse getLoginQRCodeResponse = null;
            GetLoginQRCodeRequest getLoginQRCodeRequest = new GetLoginQRCodeRequest()
            {
                aes = new AesKey()
                {
                    key = AESKey.ToByteArray(16, 2),
                    len = 16
                },
                baseRequest = GetBaseRequest(0),

                opcode = 0
            };

            getLoginQRCodeRequest.aes = new AesKey()
            {
                key = AESKey.ToByteArray(16, 2),
                len = 16
            };
            //序列化 protobuf
            var src = Util.Serialize(getLoginQRCodeRequest);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETLOGINQRCODE, bufferlen, 7);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_GETLOGINQRCODE);
            // 解包头
            if (RetDate == null) { return new GetLoginQRCodeResponse(); }
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                var RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed) {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                getLoginQRCodeResponse = Util.Deserialize<GetLoginQRCodeResponse>(RespProtobuf);
            }
            else {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }


            return getLoginQRCodeResponse;
        }

        /// <summary>
        /// 检查扫码状态
        /// </summary>
        /// <param name="qrname">获取二维码时返回的uuid</param>
        /// <returns></returns>
        public CheckLoginQRCodeResponse CheckLoginQRCode(string qrname)
        {
            var RespProtobuf = new byte[0];
            CheckLoginQRCodeRequest checkLoginQRCodeRequest = new CheckLoginQRCodeRequest()
            {
                aes = new AesKey()
                {
                    key = AESKey.ToByteArray(16, 2),
                    len = 16
                },
                baseRequest = GetBaseRequest(0),
                uuid = qrname,
                timeStamp = (uint)CurrentTime_(),
                opcode = 0
            };
            var src = Util.Serialize(checkLoginQRCodeRequest);

            //src = LongLinkPack(LongLinkCmdId.SEND_CHECKLOGINQRCODE_CMDID, CGI_TYPE.CGI_TYPE_CHECKLOGINQRCODE, src, 1);

            //return m_client.Send(src, src.Length);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_CHECKLOGINQRCODE, bufferlen, 7);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_CHECKLOGINQRCODE);
            if (RetDate == null) { return null; }
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{}", RetDate.ToString(16, 2), RetDate.Length);
            }

            var CheckLoginQRCode = Util.Deserialize<CheckLoginQRCodeResponse>(RespProtobuf);
            return CheckLoginQRCode;
        }

        /// <summary>
        /// 确定登录包 改用长连接
        /// </summary>
        /// <param name="wxnewpass">这是扫码成功过来伪密码</param>
        /// <param name="wxid">wxid</param>
        /// <returns></returns>
        public ManualAuthResponse ManualAuth(string wxnewpass, string wxid)
        {
            devicelId = "49aa6db2f4a3ffe0e96618f6b92cde11"; //49aa7db2f4a3ffe0e96218f6b91cde31
             var RespProtobuf = new byte[0];
            GenerateECKey(713, pub_key_buf, pri_key_buf);
            //OpenSSLNativeClass.ECDH.GenEcdh(713, ref pub_key_buf, ref pri_key_buf);
            ManualAuthAccountRequest manualAuthAccountRequest = new ManualAuthAccountRequest()
            {
                aes = new AesKey()
                {
                    len = 16,
                    key = AESKey.ToByteArray(16, 2)
                },
                ecdh = new Ecdh()
                {
                    ecdhkey = new EcdhKey()
                    {
                        key = pub_key_buf,
                        len = 57
                    },
                    nid = 713
                },

                password1 = wxnewpass,
                password2 = null,
                userName = wxid
            };
            ManualAuthDeviceRequest manualAuthDeviceRequest = new ManualAuthDeviceRequest();
            manualAuthDeviceRequest = Util.Deserialize<ManualAuthDeviceRequest>("0A310A0010001A1049AA7DB2F4A3FFE0E96218F6B92CDE3220A08E98B0012A1169506164206950686F6E65204F53382E34300112023A001A203363616137646232663461336666653065393632313866366239326364653332228D023C736F6674747970653E3C6B333E382E343C2F6B333E3C6B393E695061643C2F6B393E3C6B31303E323C2F6B31303E3C6B31393E45313841454344332D453630422D344635332D423838372D4339343436344437303836393C2F6B31393E3C6B32303E3C2F6B32303E3C6B32313E313030333C2F6B32313E3C6B32323E286E756C6C293C2F6B32323E3C6B32343E62383A66383A38333A33393A61643A62393C2F6B32343E3C6B33333EE5BEAEE4BFA13C2F6B33333E3C6B34373E313C2F6B34373E3C6B35303E313C2F6B35303E3C6B35313E6461697669732E495041443C2F6B35313E3C6B35343E69506164322C353C2F6B35343E3C6B36313E323C2F6B36313E3C2F736F6674747970653E2800322B33636161376462326634613366666530653936323138663662393263646533322D313532383535343230314204695061644A046950616452057A685F434E5A04382E3030680070AFC6EFD8057A054170706C65920102434E9A010B6461697669732E49504144AA010769506164322C35B00102BA01D50608CF0612CF060A08303030303030303310011AC0068A8DCEEE5AB9F4E16054EDA0545F7288B7951621A41446C1AEC0621B3CFE6926737F8298D0B52F467FDFC5EC936D512D332A1AC664E7DFEE734A5E403A72225F852734BF32F6FD623B95D17B64DC8D18FBB2CA2015113CD17518274BED4687D26F5D9E270687745541FA84921A16B50CFE487B1A88C3A91D838A2520AF8757F0E5ACE55BA599B9FCDF1595C3DAAD8E3A34C28BA39951D7A4CF9075CCC28721BA61E48C2DA1B853F3BE0D79AC63F47F2E3C4FF10D4D1CCC1D3002B6F63C228641C1EEB24686BA300853C355C268057D733B7898D20E6B43621419D8BCFCAED82C45377653234B7421238D00B25089670DDEBB03274B1D0D8C45D5A0EA7ECA9086254CCEAA8674ADE4DF905914437BC73D4C9D50CEC9ABCB927590D068DC10A810D376DAFB17A31F947765FF6A7F3B191EC40EEC4AA86FF8771CD2D717D25EE2B7555179AF4C611B9C6AD802B8FDAEAE36CA3497C438E8D4A06B1A7A570D74AAF6C244E8D23BA635FF0F27DCFCF5F6C4754A0049A620AE99012EB4936D34BAD267EAFDB12B67D5274272D3BC795B6454B4C2B768929007D0993F742A519D567ACD0369FCC9196D3CC04578F795026C336F2A29A012608C66E2068F5994210173C5A3B2720A4D040A6D2C3E873D56CE88F85CEFE4847743DEF1102653D42FBC3A31CA5BFE2E666D3542E6E1C5BCCE54D99EC934B183EED69FEA87D975666065E5903F366EFFE04627603FD64861C142A5A19EBD344BF194DE427FB4B70AA0D3CD972AC0A11EA6913E17366CA48966090E10B246BABABA553DBF89BEA4F55004C37E546ABABB8AA20E80B2A0ED21B6700F89699FD01983EDA71ACE6A44B6397605D30E88683BA4BB92A50DC7AFFB820089F157B8C83F7B5DCD35BABCC90501E2E6BDF83327A1059908C72EAF1B5A07CA6565A0888883966D26386C69293649BEC0913FE12C1ABA7B0B16261176E2F7D109FCF68A46B7C3AF7126E77224AA36891B703655CFEA2AAA8B5E095D8B204308133E63D0F0309E8B1CB5A21E9C8B27090859139C076723DE4C74578F6584888220A11A45CDDEC43A1F542552604C96FFE3A01006946086A864C182361B3659C1BDE9ECEA5236F5F38BA98A4C7E8C81A39D5CBA39B7A0F9FFA75AC59BB956595B58DAED58A0851D48B0B7A7407FA576E4956C".ToByteArray(16, 2));
            manualAuthDeviceRequest.Timestamp = (int)CurrentTime_();
            //manualAuthDeviceRequest.Clientcheckdat = new SKBuiltinString_() { buffer = new byte[] { }, iLen = 0 };
            manualAuthDeviceRequest.imei = Encoding.UTF8.GetBytes(devicelId);
            manualAuthDeviceRequest.clientSeqID = manualAuthDeviceRequest.imei + "-" + ((int)CurrentTime_()).ToString();
            manualAuthDeviceRequest.baseRequest = GetBaseRequest(1);


            var account = Util.Serialize(manualAuthAccountRequest);
            byte[] device = Util.Serialize(manualAuthDeviceRequest);
            byte[] subHeader = new byte[] { };
            int dwLenAccountProtobuf = account.Length;
            subHeader = subHeader.Concat(dwLenAccountProtobuf.ToByteArray(Endian.Big)).ToArray();
            int dwLenDeviceProtobuf = device.Length;
            subHeader = subHeader.Concat(dwLenDeviceProtobuf.ToByteArray(Endian.Big)).ToArray();
            Debug.Print(Util.SixTwoData(Encoding.Default.GetString(manualAuthDeviceRequest.imei)));
            if (subHeader.Length > 0 && account.Length > 0 && device.Length > 0)
            {
                var cdata = Util.compress_rsa_LOGIN(account);
                int dwLenAccountRsa = cdata.Length;
                subHeader = subHeader.Concat(dwLenAccountRsa.ToByteArray(Endian.Big)).ToArray();
                byte[] body = subHeader;
                ManualAuthDeviceRequest m_ManualAuthDeviceRequest = Util.Deserialize<ManualAuthDeviceRequest>(device);
                //var t2=m_ManualAuthDeviceRequest.tag2.ToString(16, 2);

                var memoryStream = Util.Serialize(m_ManualAuthDeviceRequest);

                body = body.Concat(cdata).ToArray();

                body = body.Concat(Util.compress_aes(device, AESKey.ToByteArray(16, 2))).ToArray();
                //var head = MakeHead( body, MM.CGI_TYPE.CGI_TYPE_MANUALAUTH, 7);
                var head = MakeHead((int)CGI_TYPE.CGI_TYPE_MANUALAUTH, body.Length, 7, false);

                body = head.Concat(body).ToArray();

                byte[] RetDate = Util.HttpPost(body, URL.CGI_MANUALAUTH);
                //Console.WriteLine(RetDate.ToString(16, 2));
                //var ret = HttpPost(@short + MM.URL.CGI_MANUALAUTH, head, null);
                //var lhead = LongLinkPack(LongLinkCmdId.SEND_MANUALAUTH_CMDID, seq++, head.Length);
                if (RetDate.Length > 32)
                {
                    var packinfo = UnPackHeader(RetDate);
                    //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                    RespProtobuf = packinfo.body;
                    if (packinfo.m_bCompressed)
                    {
                        RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                if (RespProtobuf == null) return null;
                var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
                return ManualAuthResponse;
            }
            else
                return null;
            //return null;

        }
        /// <summary>
        /// 62数据+账号密码登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pass">登录密码</param>
        /// <param name="Data62">62数据</param>
        /// <returns></returns>
        public ManualAuthResponse UserLogin(string username, string pass, string Data62)
        {
            string imei;
            if (Data62 != "")
            {
                devicelId = Util.Get62Key(Data62);
                imei = devicelId;
                devicelId = "49" + imei.Substring(2, imei.Length - 2);
            }
            else {
                imei = devicelId;
            }
            
            var RespProtobuf = new byte[0];
            GenerateECKey(713, pub_key_buf, pri_key_buf);
            //OpenSSLNativeClass.ECDH.GenEcdh(713, ref pub_key_buf, ref pri_key_buf);
            ManualAuthAccountRequest manualAuthAccountRequest = new ManualAuthAccountRequest()
            {
                aes = new AesKey()
                {
                    len = 16,
                    key = AESKey.ToByteArray(16, 2)
                },
                ecdh = new Ecdh()
                {
                    ecdhkey = new EcdhKey()
                    {
                        key = pub_key_buf,
                        len = 57
                    },
                    nid = 713
                },

                password1 = Util.MD5Encrypt(pass),
                password2 = Util.MD5Encrypt(pass),
                userName = username
            };
            ManualAuthDeviceRequest manualAuthDeviceRequest = new ManualAuthDeviceRequest();
            manualAuthDeviceRequest = Util.Deserialize<ManualAuthDeviceRequest>("0A310A0010001A1049AA7DB2F4A3FFE0E96218F6B92CDE3220A08E98B0012A1169506164206950686F6E65204F53382E34300112023A001A203363616137646232663461336666653065393632313866366239326364653332228D023C736F6674747970653E3C6B333E382E343C2F6B333E3C6B393E695061643C2F6B393E3C6B31303E323C2F6B31303E3C6B31393E45313841454344332D453630422D344635332D423838372D4339343436344437303836393C2F6B31393E3C6B32303E3C2F6B32303E3C6B32313E313030333C2F6B32313E3C6B32323E286E756C6C293C2F6B32323E3C6B32343E62383A66383A38333A33393A61643A62393C2F6B32343E3C6B33333EE5BEAEE4BFA13C2F6B33333E3C6B34373E313C2F6B34373E3C6B35303E313C2F6B35303E3C6B35313E6461697669732E495041443C2F6B35313E3C6B35343E69506164322C353C2F6B35343E3C6B36313E323C2F6B36313E3C2F736F6674747970653E2800322B33636161376462326634613366666530653936323138663662393263646533322D313532383535343230314204695061644A046950616452057A685F434E5A04382E3030680070AFC6EFD8057A054170706C65920102434E9A010B6461697669732E49504144AA010769506164322C35B00102BA01D50608CF0612CF060A08303030303030303310011AC0068A8DCEEE5AB9F4E16054EDA0545F7288B7951621A41446C1AEC0621B3CFE6926737F8298D0B52F467FDFC5EC936D512D332A1AC664E7DFEE734A5E403A72225F852734BF32F6FD623B95D17B64DC8D18FBB2CA2015113CD17518274BED4687D26F5D9E270687745541FA84921A16B50CFE487B1A88C3A91D838A2520AF8757F0E5ACE55BA599B9FCDF1595C3DAAD8E3A34C28BA39951D7A4CF9075CCC28721BA61E48C2DA1B853F3BE0D79AC63F47F2E3C4FF10D4D1CCC1D3002B6F63C228641C1EEB24686BA300853C355C268057D733B7898D20E6B43621419D8BCFCAED82C45377653234B7421238D00B25089670DDEBB03274B1D0D8C45D5A0EA7ECA9086254CCEAA8674ADE4DF905914437BC73D4C9D50CEC9ABCB927590D068DC10A810D376DAFB17A31F947765FF6A7F3B191EC40EEC4AA86FF8771CD2D717D25EE2B7555179AF4C611B9C6AD802B8FDAEAE36CA3497C438E8D4A06B1A7A570D74AAF6C244E8D23BA635FF0F27DCFCF5F6C4754A0049A620AE99012EB4936D34BAD267EAFDB12B67D5274272D3BC795B6454B4C2B768929007D0993F742A519D567ACD0369FCC9196D3CC04578F795026C336F2A29A012608C66E2068F5994210173C5A3B2720A4D040A6D2C3E873D56CE88F85CEFE4847743DEF1102653D42FBC3A31CA5BFE2E666D3542E6E1C5BCCE54D99EC934B183EED69FEA87D975666065E5903F366EFFE04627603FD64861C142A5A19EBD344BF194DE427FB4B70AA0D3CD972AC0A11EA6913E17366CA48966090E10B246BABABA553DBF89BEA4F55004C37E546ABABB8AA20E80B2A0ED21B6700F89699FD01983EDA71ACE6A44B6397605D30E88683BA4BB92A50DC7AFFB820089F157B8C83F7B5DCD35BABCC90501E2E6BDF83327A1059908C72EAF1B5A07CA6565A0888883966D26386C69293649BEC0913FE12C1ABA7B0B16261176E2F7D109FCF68A46B7C3AF7126E77224AA36891B703655CFEA2AAA8B5E095D8B204308133E63D0F0309E8B1CB5A21E9C8B27090859139C076723DE4C74578F6584888220A11A45CDDEC43A1F542552604C96FFE3A01006946086A864C182361B3659C1BDE9ECEA5236F5F38BA98A4C7E8C81A39D5CBA39B7A0F9FFA75AC59BB956595B58DAED58A0851D48B0B7A7407FA576E4956C".ToByteArray(16, 2));
            manualAuthDeviceRequest.Timestamp = (int)CurrentTime_();
            //manualAuthDeviceRequest.Clientcheckdat = new SKBuiltinString_() { buffer = new byte[] { }, iLen = 0 };
            manualAuthDeviceRequest.imei = imei.ToByteArray(16, 2);
            manualAuthDeviceRequest.clientSeqID = imei + "-" + ((int)CurrentTime_()).ToString();
            manualAuthDeviceRequest.baseRequest = new BaseRequest() {
                clientVersion = ver,
                devicelId = devicelId.ToByteArray(16, 2),
                scene = 1,
                uin = 0,
                osType = "iMac iPhone OS9.3.3",
                sessionKey = AESKey.ToByteArray(16, 2)
            };

            baseRequest = manualAuthDeviceRequest.baseRequest;
            manualAuthDeviceRequest.Adsource = "52336395-2829-C5BB-CF9C-1B65A2E52EA6";
            manualAuthDeviceRequest.Bundleid = "com.tencent.xin";
            
            manualAuthDeviceRequest.Iphonever = "iPad4,4";

            manualAuthDeviceRequest.softInfoXml = @"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>52336395-2829-C5BB-CF9C-1B65A2E52EA6</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>b8:73:cf:87:a9:53</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>";
            //manualAuthDeviceRequest.baseRequest.devicelId = "6636666433636135393335363139366466396431393531303239366133333231".ToByteArray(16,2);
            var account = Util.Serialize(manualAuthAccountRequest);
            byte[] device = Util.Serialize(manualAuthDeviceRequest);
            Debug.Print(device.ToString(16, 2));
            byte[] subHeader = new byte[] { };
            int dwLenAccountProtobuf = account.Length;
            subHeader = subHeader.Concat(dwLenAccountProtobuf.ToByteArray(Endian.Big)).ToArray();
            int dwLenDeviceProtobuf = device.Length;
            subHeader = subHeader.Concat(dwLenDeviceProtobuf.ToByteArray(Endian.Big)).ToArray();

            if (subHeader.Length > 0 && account.Length > 0 && device.Length > 0)
            {
                var cdata = Util.compress_rsa_LOGIN(account);
                int dwLenAccountRsa = cdata.Length;
                subHeader = subHeader.Concat(dwLenAccountRsa.ToByteArray(Endian.Big)).ToArray();
                byte[] body = subHeader;
                ManualAuthDeviceRequest m_ManualAuthDeviceRequest = Util.Deserialize<ManualAuthDeviceRequest>(device);
                //var t2=m_ManualAuthDeviceRequest.tag2.ToString(16, 2);

                var memoryStream = Util.Serialize(m_ManualAuthDeviceRequest);

                body = body.Concat(cdata).ToArray();

                body = body.Concat(Util.compress_aes(device, AESKey.ToByteArray(16, 2))).ToArray();
                //var head = MakeHead( body, MM.CGI_TYPE.CGI_TYPE_MANUALAUTH, 7);
                var head = MakeHead((int)CGI_TYPE.CGI_TYPE_MANUALAUTH, body.Length, 7, false);

                body = head.Concat(body).ToArray();

                byte[] RetDate = Util.HttpPost(body, URL.CGI_MANUALAUTH);
                //Console.WriteLine(RetDate.ToString(16, 2));
                //var ret = HttpPost(@short + MM.URL.CGI_MANUALAUTH, head, null);
                //var lhead = LongLinkPack(LongLinkCmdId.SEND_MANUALAUTH_CMDID, seq++, head.Length);
                if (RetDate.Length > 32)
                {
                    var packinfo = UnPackHeader(RetDate);
                    //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                    RespProtobuf = packinfo.body;
                    if (packinfo.m_bCompressed)
                    {
                        RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                if (RespProtobuf == null) return null;
                var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
                return ManualAuthResponse;
            }
            else
                return null;
            //return null;

        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="passwd">明文密码</param>
        /// <returns></returns>
        public micromsg.NewVerifyPasswdResponse NewVerifyPasswd(string passwd){
            micromsg.NewVerifyPasswdRequest newVerifyPasswd = new micromsg.NewVerifyPasswdRequest()
            {

                BaseRequest = new micromsg.BaseRequest() {
                    ClientVersion = ver,
                    DeviceID = Encoding.Default.GetBytes(devicelId),
                    Scene  = (uint)baseRequest.scene,
                    SessionKey = baseRequest.sessionKey,
                    DeviceType = Encoding.Default.GetBytes(baseRequest.osType),
                    Uin = (uint)baseRequest.uin,
                },
                Pwd1 = Util.MD5Encrypt(passwd),
                Pwd2 = Util.MD5Encrypt(passwd),
                OpCode = 1,
                
                
            };

            var RespProtobuf = new byte[0];
            //newVerifyPasswd.BaseRequest.ClientVersion = ver;
            //newVerifyPasswd.BaseRequest.Scene = 0;
            //newVerifyPasswd.BaseRequest.DeviceType = Encoding.Default.GetBytes("iPad iPhone OS9.0.2");

            //newVerifyPasswd.BaseRequest.Scene = 0;
            var src = Util.Serialize(newVerifyPasswd);

            Debug.Print(src.ToString(16, 2));

            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 384, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/newverifypasswd");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var NewVerifyPasswdResponse = Util.Deserialize<micromsg.NewVerifyPasswdResponse>(RespProtobuf);
            return NewVerifyPasswdResponse;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="NewPasswd"></param>
        /// <param name="Ticket"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        public micromsg.NewSetPasswdResponse NewSetPasswd(string NewPasswd, string Ticket, byte[] authKey) {

            micromsg.SetPwdRequest newSetPasswdRequest = new micromsg.SetPwdRequest()
            {
                AutoAuthKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = authKey,
                    iLen = (uint)authKey.Length
                },
                BaseRequest = new micromsg.BaseRequest()
                {
                    ClientVersion = ver,
                    DeviceID = Encoding.Default.GetBytes(devicelId),
                    Scene = (uint)baseRequest.scene,
                    SessionKey = baseRequest.sessionKey,
                    DeviceType = Encoding.Default.GetBytes(baseRequest.osType),
                    Uin = (uint)baseRequest.uin,
                },
                Password = Util.EncryptWithMD5(NewPasswd),
                Ticket = Ticket,
                

            };

            // newSetPasswd.SetAutoAuthKey(skb);
            //newSetPasswd.ImgSid = 0;
            var RespProtobuf = new byte[0];

            //newSetPasswd.BaseRequest.Scene = 0;
            var src = Util.Serialize(newSetPasswdRequest);
            src = src.Concat("2801".ToByteArray(16, 2)).ToArray();

            Debug.Print("SetPasswdret:{0}", src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 383, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/newsetpasswd");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SetPwdResponse = Util.Deserialize<micromsg.NewSetPasswdResponse>(RespProtobuf);
            return SetPwdResponse;
        }

        /// <summary>
        /// 获取个人二维码或群二维码
        /// </summary>
        /// <param name="name">微信wxid</param>
        /// <returns></returns>
        public micromsg.GetQRCodeResponse GetQRCode(string name) {
            var RespProtobuf = new byte[0];
            SKBuiltinString skb = new SKBuiltinString();
            skb.@string = name;
            SKBuiltinString[] skb_ = new SKBuiltinString[1];
            skb_[0] = skb;
            GetQRCodeRequest getqrcode = new GetQRCodeRequest()
            {
                baseRequest = baseRequest,
                opcode = 0,
                style = (uint)0,
                userName = skb_
            };
            var src = Util.Serialize(getqrcode);



            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETQRCODE, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_GETQRCODE);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var GetQRcode = Util.Deserialize<micromsg.GetQRCodeResponse>(RespProtobuf);
            return GetQRcode;
        }

        /// <summary>
        /// 取收款码
        /// </summary>
        /// <returns></returns>
        public F2FQrcodeResponse F2FQrcode()
        {
            var RespProtobuf = new byte[0];

            F2FQrcodeRequest F2FQrcode_ = new F2FQrcodeRequest()
            {
                baseRequest = baseRequest,

            };
            var src = Util.Serialize(F2FQrcode_);



            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_F2FQRCODE, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_F2FQRCODE);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var f2fcode = Util.Deserialize<F2FQrcodeResponse>(RespProtobuf);
            return f2fcode;
        }

        /// <summary>
        /// 设备信息和秘钥建议
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="sessionKey"></param>
        /// <param name="uIn"></param>
        /// <param name="osType"></param>
        /// <returns></returns>
        public BaseRequest GetBaseRequest(string deviceID, byte[] sessionKey, uint uIn, string osType) {
            var baseRequest_ = GoogleProto.CreateBaseRequestEntity(deviceID, Encoding.Default.GetString(sessionKey), uIn, osType, 3);

            return Util.Deserialize<BaseRequest>(baseRequest_.ToByteArray());
        }
        /// <summary>
        /// 设备信息和秘钥建议
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="sessionKey"></param>
        /// <param name="uIn"></param>
        /// <param name="osType"></param>
        /// <returns></returns>
        public BaseRequest GetBaseRequest(int scene = 0)
        {

            if (baseRequest == null)
            {
                MemoryStream memoryStream = new MemoryStream();
                baseRequest = new BaseRequest()
                {
                    clientVersion = (int)ver,

                    devicelId = devicelId.ToByteArray(16,2),
                    scene = scene,
                    sessionKey = AESKey.ToByteArray(16,2),
                    osType = "iPad iPhone OS9.3.3",
                    uin = m_uid
                };
            }
            else
            {


                baseRequest.scene = scene;
            }
            return baseRequest;
        }

        /// <summary>
        /// 发送文字信息
        /// </summary>
        /// <param name="to">接受者</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public NewSendMsgRespone SendNewMsg(string to, string content,int type = 1)
        {
            var RespProtobuf = new byte[0];
            byte[] Msg = "0801129B050A150A13777869645F32306E763668336A63333735323212EC043C3F786D6C2076657273696F6E3D22312E30223F3E0A3C6D73672062696768656164696D6775726C3D22687474703A2F2F77782E716C6F676F2E636E2F6D6D686561642F7665725F312F666864536D6F503670744B483467537852495842394643544A455136436169635648317338787675506B787551706E4D344A647956696154626E64696354335172574444616E56624F48535955344F3437396D65796D5947672F302220736D616C6C68656164696D6775726C3D22687474703A2F2F77782E716C6F676F2E636E2F6D6D686561642F7665725F312F666864536D6F503670744B483467537852495842394643544A455136436169635648317338787675506B787551706E4D344A647956696154626E64696354335172574444616E56624F48535955344F3437396D65796D5947672F3133322220757365726E616D653D22777869645F7062673530786463696B7875323222206E69636B6E616D653D224C694152222066756C6C70793D224C694152222073686F727470793D222220616C6961733D2248333130373333343633322220696D6167657374617475733D223322207363656E653D223137222070726F76696E63653D22E5AF86E5858BE7BD97E5B0BCE8A5BFE4BA9A2220636974793D22E5AF86E5858BE7BD97E5B0BCE8A5BFE4BA9A22207369676E3D2222207365783D2232222063657274666C61673D2230222063657274696E666F3D2222206272616E6449636F6E55726C3D2222206272616E64486F6D6555726C3D2222206272616E64537562736372697074436F6E66696755726C3D2222206272616E64466C6167733D22302220726567696F6E436F64653D22464D22202F3E0A182A20D1ED8FDC0528DCD885C681C5ADD69B013200".ToByteArray(16, 2);
            var asd = new byte[] { };
            var sda = ProtoBuf.Serializer.Deserialize<NewSendMsgRequest>(new MemoryStream(Msg));
            byte[] apc = new byte[] { };

            sda.info.clientMsgId = (ulong)CurrentTime_();
            sda.info.toid.@string = to;
            sda.info.content = content;
            sda.info.utc = CurrentTime_();
            sda.info.type = type;
            //sda.info.msgSource = content;
            var src = Util.Serialize(sda);


            int bufferlen = src.Length;
            Debug.Print(src.ToString(16, 2));
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_NEWSENDMSG, bufferlen, 5, true, true);
            
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_NEWSENDMSG);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            //var NewSendMsg = mm.command.NewSendMsgResponse.ParseFrom(RespProtobuf);
            var NewSendMsg = Util.Deserialize<NewSendMsgRespone>(RespProtobuf);
            return NewSendMsg;
        }

        /// <summary>
        /// 初始化第一次登录使用会返回微信列表和信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="deviceID"></param>
        /// <param name="OSType"></param>
        /// <returns></returns>
        public mm.command.NewInitResponse NewInit(string userName)
        {
            var RespProtobuf = new byte[0];
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            //生成google对象
            mm.command.NewInitRequest niqObj = GoogleProto.CreateNewInitRequestEntity((uint)m_uid, Encoding.Default.GetString(GetAESkey()), userName, devicelId, "iPad iPhone OS9.3.3", initSyncKey, maxSyncKey);

            byte[] niqData = niqObj.ToByteArray();




            int bufferlen = niqData.Length;
            //组包
            byte[] SendDate = pack(niqData, 139, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/newinit");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            mm.command.NewInitResponse niReceive = mm.command.NewInitResponse.ParseFrom(RespProtobuf);
            if(niReceive.Base.Ret == 0)
            {
                initSyncKey = niReceive.CurrentSynckey.Buffer.ToByteArray();
                maxSyncKey = niReceive.MaxSynckey.Buffer.ToByteArray();

            }

            return niReceive;
        }

        /// <summary>
        /// 同步信息用来同步未读信息和好友
        /// </summary>
        /// <param name="scane">4</param>
        /// <param name="synckey">使用NewInit 发送后会返回synckey</param>
        /// <returns></returns>
        public NewSyncResponse NewSyncEcode(int scane, byte[] synckey)
        {
            var RespProtobuf = new byte[0];
            //0a020800108780101a8a02088402128402081f1208080110aaa092ba021208080210a9a092ba0212080803109aa092ba021208080410f28292ba021208080510f28292ba021208080710f28292ba02120408081000120808091099a092ba021204080a10001208080b10839f92ba021204080d10001208080e10f28292ba021208081010f28292ba021204086510001204086610001204086710001204086810001204086910001204086b10001204086d10001204086f1000120408701000120408721000120908c90110f5d7fbd705120908cb0110c6bcf3d705120508cc011000120508cd011000120908e80710fdd0fad705120908e90710ba92fad705120908ea07109bf1c9d705120908d10f10d1b9f0d70520032a0a616e64726f69642d31393001
            MemoryStream memoryStream = new MemoryStream();
            NewSyncRequest request = new NewSyncRequest()
            {
                continueflag = new FLAG() { flag = 0 },
                device = "iPad iPhone OS8.4",
                scene = scane,
                selector = 262151,//3
                syncmsgdigest = 3,
                tagmsgkey = new syncMsgKey()
                {
                    msgkey = new Synckey()
                    {
                        size = 32
                    }
                }
            };

            request.tagmsgkey = Util.Deserialize<syncMsgKey>(synckey);

            var src = Util.Serialize(request);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_NEWSYNC, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_NEWSYNC);
            
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var NewSync = Util.Deserialize<NewSyncResponse>(RespProtobuf);
            
            return NewSync;
        }

        /// <summary>
        /// 同步信息用来同步未读信息和好友同上 
        /// </summary>
        /// <param name="initSyncKey"></param>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public mm.command.NewSyncResponse NewSync(byte[] initSyncKey, string deviceID)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            var RespProtobuf = new byte[0];
            //生成google对象
            mm.command.NewSyncRequest nsrObj = GoogleProto.CreateNewSyncRequestEntity(initSyncKey, deviceID);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;
            int bufferlen = nsrData.Length;
            //组包
            byte[] SendDate = pack(nsrData, (int)CGI_TYPE.CGI_TYPE_NEWSYNC, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_NEWSYNC);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            mm.command.NewSyncResponse nsrReceive = mm.command.NewSyncResponse.ParseFrom(RespProtobuf);

            return nsrReceive;
        }


        /// <summary>
        /// 同步盆友圈
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="OStype"></param>
        /// <returns></returns>
        public SnsSyncResponse SnsSync( string deviceID, string OStype)
        {
            //压缩前长度和压缩后长度
            int lenBeforeZip = 0;
            int lenAfterZip = 0;

            var RespProtobuf = new byte[0];
            //生成google对象
            mm.command.MMSnsSyncRequest nsrObj = GoogleProto.CreateMMSnsSyncRequest(deviceID, Encoding.Default.GetString(GetAESkey()), (uint)m_uid, OStype, initSyncKey);

            byte[] nsrData = nsrObj.ToByteArray();
            lenBeforeZip = nsrData.Length;
            Debug.Print(nsrData.ToString(16, 2));
            int bufferlen = nsrData.Length;
            //组包
            byte[] SendDate = pack(nsrData, (int)CGI_TYPE.CGI_TYPE_MMSNSSYNC, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_MMSNSSYNC);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            //mm.command.MMSnsSyncRequest nsrReceive = mm.command.MMSnsSyncRequest.ParseFrom(RespProtobuf);
            var SnsSyncResponse = Util.Deserialize<SnsSyncResponse>(RespProtobuf);
            return SnsSyncResponse;
        }

        public SnsSyncResponse SnsSyncRequest(syncMsgKey bufkey) {
            var RespProtobuf = new byte[0];
            SnsSyncRequest snsSync = new SnsSyncRequest()
            {
                baseRequest = baseRequest,
                selector = (uint)509,
                keyBuf = bufkey,
            };
            var src = Util.Serialize(snsSync);

            Debug.Print(src.ToString(16, 2));

            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_MMSNSSYNC, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_MMSNSSYNC);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SnsSyncResponse = Util.Deserialize<SnsSyncResponse>(RespProtobuf);

            return SnsSyncResponse;
        }

        /// <summary>
        /// 获取指定人的朋友圈
        /// </summary>
        /// <param name="fristPageMd5">/首页为空 第二页请附带md5</param>
        /// <param name="Username">要访问人的wxid</param>
        /// <param name="maxid">首页为0 次页朋友圈数据id 的最小值</param>
        /// <returns></returns>
        public SnsUserPageResponse SnsUserPage(string fristPageMd5, string Username, int maxid = 0)
        {
            byte[] RespProtobuf = new byte[0];
            SnsUserPageRequest snsUserPage = new SnsUserPageRequest()
            {
                baseRequest = baseRequest,
                fristPageMd5 = fristPageMd5,
                username = Username,
                maxid = (ulong)maxid,
                source = 0,
                minFilterId = 0,
                lastRequestTime = 0

            };


            var src = Util.Serialize(snsUserPage);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_MMSNSUSERPAGE, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_MMSNSUSERPAGE);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SnsUserPageResponse_ = Util.Deserialize<SnsUserPageResponse>(RespProtobuf);
            return SnsUserPageResponse_;
        }

        /// <summary>
        /// 发送盆友圈
        /// </summary>
        /// <param name="content">欲发送内容 使用朋友圈结构发送</param>
        /// <returns></returns>
        public SnsPostResponse SnsPost(string content,int GroupId)
        {

            var RespProtobuf = new byte[0];
            SnsPostRequest SnsPostReq = Util.Deserialize<SnsPostRequest>("0A570A105D64797E40587E3653492B3770767C6D10E5DA8D81031A20353332363435314632303045304431333043453441453237323632423631363920A28498B0012A1369506164206950686F6E65204F53392E332E33300012810808FB0712FB073C54696D656C696E654F626A6563743E3C69643E31323534323132393139333538343234323934373C2F69643E3C757365726E616D653E777869645F6B727862626D68316A75646533313C2F757365726E616D653E3C63726561746554696D653E313439353133383331303C2F63726561746554696D653E3C636F6E74656E74446573633EE2809CE7BEA1E68595E982A3E4BA9BE4B880E6B2BEE79D80E69E95E5A4B4E5B0B1E883BDE5AE89E79DA1E79A84E4BABAE5928CE982A3E4BA9BE586B3E5BF83E694BEE6898BE4B98BE5908EE5B0B1E4B88DE5868DE59B9EE5A4B4E79A84E4BABAE2809D3C2F636F6E74656E74446573633E3C636F6E74656E744465736353686F77547970653E303C2F636F6E74656E744465736353686F77547970653E3C636F6E74656E74446573635363656E653E333C2F636F6E74656E74446573635363656E653E3C707269766174653E303C2F707269766174653E3C7369676874466F6C6465643E303C2F7369676874466F6C6465643E3C617070496E666F3E3C69643E3C2F69643E3C76657273696F6E3E3C2F76657273696F6E3E3C6170704E616D653E3C2F6170704E616D653E3C696E7374616C6C55726C3E3C2F696E7374616C6C55726C3E3C66726F6D55726C3E3C2F66726F6D55726C3E3C6973466F7263655570646174653E303C2F6973466F7263655570646174653E3C2F617070496E666F3E3C736F75726365557365724E616D653E3C2F736F75726365557365724E616D653E3C736F757263654E69636B4E616D653E3C2F736F757263654E69636B4E616D653E3C73746174697374696373446174613E3C2F73746174697374696373446174613E3C737461744578745374723E3C2F737461744578745374723E3C436F6E74656E744F626A6563743E3C636F6E74656E745374796C653E323C2F636F6E74656E745374796C653E3C7469746C653E3C2F7469746C653E3C6465736372697074696F6E3E3C2F6465736372697074696F6E3E3C6D656469614C6973743E3C2F6D656469614C6973743E3C636F6E74656E7455726C3E3C2F636F6E74656E7455726C3E3C2F436F6E74656E744F626A6563743E3C616374696F6E496E666F3E3C6170704D73673E3C6D657373616765416374696F6E3E3C2F6D657373616765416374696F6E3E3C2F6170704D73673E3C2F616374696F6E496E666F3E3C6C6F636174696F6E20636974793D5C225C2220706F69436C61737369667949643D5C225C2220706F694E616D653D5C225C2220706F69416464726573733D5C225C2220706F69436C617373696679547970653D5C22305C223E3C2F6C6F636174696F6E3E3C7075626C6963557365724E616D653E3C2F7075626C6963557365724E616D653E3C2F54696D656C696E654F626A6563743E0D0A1800280030003A13736E735F706F73745F313533343933333731384001580068008001009A010A0A0012001A0020002800AA010408001200C00100".ToByteArray(16, 2));

            

            SnsPostReq.baseRequest = baseRequest;
            SnsPostReq.objectDesc.iLen = (uint)content.Length;
            SnsPostReq.objectDesc.buffer = content;
            
            SnsPostReq.clientId = "sns_post_" + CurrentTime_().ToString();
            SnsPostReq.groupNum = 1;
            SnsPostReq.groupIds = new SnsGroup[1];
            SnsPostReq.groupIds[0] = new SnsGroup() { GroupId =3 };
            SnsPostReq.blackListNum = 1;
            SnsPostReq.blackList = new SKBuiltinString[1];
            SnsPostReq.blackList[0] = new SKBuiltinString() { @string = "wxid_xhdqvshqkor822" };
            //SnsPostReq.privacy = 3;
            //SnsPostReq.groupIds[1] = new SnsGroup() { GroupId = 1 };
            SnsPostReq.withUserList = new SKBuiltinString[1];
            SnsPostReq.withUserList[0] = new SKBuiltinString() { @string = "wxid_xhdqvshqkor822" };
            SnsPostReq.withUserListNum = 1;
            //SnsPostReq.ctocUploadInfo = new SnsPostCtocUploadInfo() { flag = (uint)GroupId };
            SnsPostReq.syncFlag = (uint)GroupId;
            //SnsPostReq.referId = (uint)GroupId;
            //SnsPostReq.objectSource = (uint)GroupId;
            //SnsPostReq.objectSource = ;
            var src = Util.Serialize(SnsPostReq);


            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_MMSNSPORT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_MMSNSPORT);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SnsPostResponse = Util.Deserialize<SnsPostResponse>(RespProtobuf);
            return SnsPostResponse;
        }

        /// <summary>
        /// 取朋友圈首页
        /// </summary>
        /// <param name="fristPageMd5">/首页为空 第二页请附带md5</param>
        /// <param name="maxid">首页为0 次页朋友圈数据id 的最小值</param>
        /// <returns></returns>
        public SnsTimeLineResponse SnsTimeLine(string fristPageMd5 = "", int maxid = 0)
        {
            byte[] RespProtobuf = new byte[0];
            SnsTimeLineRequest snsTimeLine = new SnsTimeLineRequest() {
                baseRequest = baseRequest,
                clientLastestId = 0,
                firstPageMd5 = fristPageMd5,
                lastRequestTime = 0,
                maxId = (ulong)maxid,
                minFilterId = 0,
                networkType = 1,

            };


            var src = Util.Serialize(snsTimeLine);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_MMSNSTIMELINE, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_MMSNSTIMELINE);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SnsTimeLineResponse_ = Util.Deserialize<SnsTimeLineResponse>(RespProtobuf);
            return SnsTimeLineResponse_;
        }

        /// <summary>
        /// 操作朋友圈
        /// </summary>
        /// <param name="id">要操作的id</param>
        /// <param name="type">//1删除朋友圈2设为隐私3设为公开4删除评论5取消点赞</param>
        /// <returns></returns>
        public SnsObjectOpResponse GetSnsObjectOp(ulong id, SnsObjectOpType type) {
            byte[] RespProtobuf = new byte[0];
            SnsObjectOp snsObject = new SnsObjectOp()
            {
                id = id,
                opType = type
            };

            SnsObjectOpRequest snsObjectOp_ = new SnsObjectOpRequest() {
                baseRequest = baseRequest,
                opCount = 1,
                opList = snsObject
            };

            var src = Util.Serialize(snsObjectOp_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_SNSOBJECTOP, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_MMSNSOBJECTOP);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SnsObjectOpResponse_ = Util.Deserialize<SnsObjectOpResponse>(RespProtobuf);
            return SnsObjectOpResponse_;
        }

        /// <summary>
        /// 附近人
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="logitude"></param>
        /// <param name="type">查找精度</param>
        /// <returns></returns>
        public LbsResponse LbsLBSFind(float latitude, float logitude, int type = 1) {
            byte[] RespProtobuf = new byte[0];
            LbsRequest lbs = new LbsRequest()
            {
                baseRequest = baseRequest,
                gPSSource = 0,
                latitude = latitude,
                logitude = logitude,
                opCode = (uint)type,
                precision = 65,
            };

            var src = Util.Serialize(lbs);


            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_LBSFIND, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_LBSFIND);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }

            var LbsResponse_ = Util.Deserialize<LbsResponse>(RespProtobuf);
            return LbsResponse_;
        }

        /// <summary>
        /// 获取标签分组
        /// </summary>
        /// <returns></returns>
        public GetContactLabelListResponse GetContactLabelList() {
            byte[] RespProtobuf = new byte[0];
            GetContactLabelListRequest getContactLabelList_ = new GetContactLabelListRequest() {
                baseRequest = baseRequest
            };

            var src = Util.Serialize(getContactLabelList_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETCONTACTLABELLIST, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_GETCONTACTLABELLIST);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var GetContactLabelListResponse_ = Util.Deserialize<GetContactLabelListResponse>(RespProtobuf);
            return GetContactLabelListResponse_;
        }

        /// <summary>
        /// 创建群
        /// </summary>
        /// <param name="list">要添加进来的好友第一个必须是自己的wxid</param>
        /// <param name="topic">群名</param>
        /// <returns></returns>
        public CreateChatRoomResponese CreateChatRoom(MemberReq[] list, string topic = "") {

            byte[] RespProtobuf = new byte[0];
            SKBuiltinString topic_ = new SKBuiltinString();
            topic_.@string = topic;
            CreateChatRoomRequest createChatRoom_ = new CreateChatRoomRequest()
            {
                baseRequest = baseRequest,
                scene = 0,
                topic = topic_,
                memberCount = (uint)list.Length,
                memberList = list
            };

            var src = Util.Serialize(createChatRoom_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_CREATECHATROOM, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_CREATECHATROOM);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var CreateChatRoomResponse_ = Util.Deserialize<CreateChatRoomResponese>(RespProtobuf);
            return CreateChatRoomResponse_;
        }

        /// <summary>
        /// 邀请好友
        /// </summary>
        /// <param name="chatRoomName">群id</param>
        /// <param name="list">要邀请的联系人["xxxx","xxxx1","xxxx2"]</param>
        /// <returns></returns>
        public AddChatRoomMemberResponse AddChatRoomMember(string chatRoomName, string list) {
            SKBuiltinString chatRoomName_ = new SKBuiltinString();
            chatRoomName_.@string = chatRoomName;
            byte[] RespProtobuf = new byte[0];
            var json = Util.JsonToObject(list);
            object[] ojb = json;
            int memberCount = ojb.Length;

            MemberReq[] list_ = new MemberReq[memberCount];

            for (int i = 0; i <= memberCount - 1; i++) {
                MemberReq memberReq_ = new MemberReq();
                SKBuiltinString sKBuiltinString = new SKBuiltinString();
                sKBuiltinString.@string = ojb[i].ToString();
                memberReq_.member = sKBuiltinString;

                list_[i] = memberReq_;
            }
            AddChatRoomMemberRequest addChat = new AddChatRoomMemberRequest()
            {
                baseRequest = baseRequest,
                chatRoomName = chatRoomName_,
                memberCount = (uint)memberCount,
                memberList = list_
            };

            var src = Util.Serialize(addChat);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_ADDCHATROOMMEMBER, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_ADDCHATROOMMEMBER);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var AddChatRoomMemberResponse_ = Util.Deserialize<AddChatRoomMemberResponse>(RespProtobuf);
            return AddChatRoomMemberResponse_;

        }

        /// <summary>
        /// 删除群成员
        /// </summary>
        /// <param name="chatRoomName">群id</param>
        /// <param name="list">要删除的联系人["xxxx","xxxx1","xxxx2"]</param>
        /// <returns></returns>
        public DelChatRoomMemberResponse DelChatRoomMember(string chatRoomName, string list)
        {

            byte[] RespProtobuf = new byte[0];
            var json = Util.JsonToObject(list);
            object[] ojb = json;
            int memberCount = ojb.Length;

            DelMemberReq[] list_ = new DelMemberReq[memberCount];

            for (int i = 0; i <= memberCount - 1; i++)
            {
                DelMemberReq memberReq_ = new DelMemberReq();
                SKBuiltinString sKBuiltinString = new SKBuiltinString();
                sKBuiltinString.@string = ojb[i].ToString();
                memberReq_.memberName = sKBuiltinString;

                list_[i] = memberReq_;
            }
            DelChatRoomMemberRequest addChat = new DelChatRoomMemberRequest()
            {
                baseRequest = baseRequest,
                chatRoomName = chatRoomName,
                memberCount = (uint)memberCount,
                memberList = list_
            };

            var src = Util.Serialize(addChat);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_DELCHATROOMMEMBER, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_DELCHATROOMMEMBER);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var DelChatRoomMemberResponse_ = Util.Deserialize<DelChatRoomMemberResponse>(RespProtobuf);
            return DelChatRoomMemberResponse_;

        }

        /// <summary>
        /// 取群成员详细
        /// </summary>
        /// <param name="chatroomUserName">群id</param>
        /// <returns></returns>
        public GetChatroomMemberDetailResponse GetChatroomMemberDetail(string chatroomUserName) {
            byte[] RespProtobuf = new byte[0];
            GetChatroomMemberDetailRequest getChatroomMember_ = new GetChatroomMemberDetailRequest()
            {
                baseRequest = baseRequest,
                clientVersion = (uint)0,
                chatroomUserName = chatroomUserName,
            };

            var src = Util.Serialize(getChatroomMember_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETCHATROOMMEMBERDETAIL, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_GETCHATROOMMEMBERDETAIL);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var GetChatroomMemberDetailResponse_ = Util.Deserialize<GetChatroomMemberDetailResponse>(RespProtobuf);
            return GetChatroomMemberDetailResponse_;
        }

        /// <summary>
        /// 授权阅读连接
        /// </summary>
        /// <param name="userName">公众号用户名</param>
        /// <param name="url">访问的连接</param>
        /// <returns></returns>
        public micromsg.GetA8KeyResp GetA8Key(string userName, string url,int opcode = 2) {

            byte[] RespProtobuf = new byte[0];
            SKBuiltinString requrl_ = new SKBuiltinString();
            requrl_.@string = url;
            GetA8KeyRequest getA8Key_ = new GetA8KeyRequest()
            {
                baseRequest = baseRequest,
                codeType = 0,
                codeVersion = 0,
                flag = 0,
                fontScale = (uint)100,
                netType = "WIFI",
                opCode = (uint)opcode,
                userName = userName,
                reqUrl = requrl_,
                friendQQ = 0,
                
            };

            var src = Util.Serialize(getA8Key_);
            int bufferlen = src.Length;
            //组包
            Debug.Print(src.ToString(16, 2));
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETA8KEY, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/geta8key");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var GetA8KeyResponse_ = Util.Deserialize<micromsg.GetA8KeyResp>(RespProtobuf);
            return GetA8KeyResponse_;
        }

        /// <summary>
        /// 精准获取通讯录  有问题
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public InitContactResponse InitContact(string username,int currentWxcontactSeq = 0) {

            byte[] RespProtobuf = new byte[0];
            InitContactRequest initContact_ = new InitContactRequest()
            {
                currentChatRoomContactSeq =0,
                currentWxcontactSeq = currentWxcontactSeq,
                username = username,
                
            };

            var src = Util.Serialize(initContact_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_INITCONTACT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_INITCONTACT);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var InitContactResponse_ = Util.Deserialize<InitContactResponse>(RespProtobuf);
            return InitContactResponse_;
        }

        /// <summary>
        ///  不支持公众号搜索 不支持原始wxid搜索支持 QQ号 手机号微信号搜索
        /// </summary>
        /// <param name="userName">QQ号 手机号微信号</param>
        /// <returns></returns>
        public SearchContactResponse SearchContact(string userName) {
            byte[] RespProtobuf = new byte[0];
            SKBuiltinString userName_ = new SKBuiltinString();
            userName_.@string = userName;
            SearchContactRequest searchContact = new SearchContactRequest()
            {
                baseRequest = baseRequest,
                searchScene = (uint)1,
                opCode = (uint)0,
                fromScene = (uint)1,
                userName = userName_,


            };
            var src = Util.Serialize(searchContact);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_SEARCHCONTACT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_SEARCHCONTACT);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SearchContactResponse_ = Util.Deserialize<SearchContactResponse>(RespProtobuf);
            return SearchContactResponse_;
        }

        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">发送者</param>
        /// <param name="path">图片路径</param>
        /// <returns></returns>
        public UploadMsgImgResponse UploadMsgImg(string to, string from, string path) {

            byte[] RespProtobuf = new byte[0];
            MemoryStream imgStream = new MemoryStream();
            using (FileStream fsRead = new FileStream(path, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                fsRead.Seek(0, SeekOrigin.Begin);
                fsRead.CopyTo(imgStream, fsLen);
                Console.WriteLine("Img buf length {0}", fsLen);

            }
            int Startpos = 0;//起始位置
            int datalen = 65535;//数据分块长度
            long datatotalength = imgStream.Length;

            SKBuiltinString ClientImgId_ = new SKBuiltinString();
            ClientImgId_.@string = CurrentTime_().ToString();

            SKBuiltinString to_ = new SKBuiltinString();
            to_.@string = to;

            SKBuiltinString from_ = new SKBuiltinString();
            from_.@string = from;

            var UploadMsgImgResponse_ = new UploadMsgImgResponse();
            while (Startpos != (int)datatotalength)
            {//
                int count = 0;
                if (datatotalength - Startpos > datalen)
                {
                    count = datalen;
                }
                else
                {

                    count = (int)datatotalength - Startpos;
                }

                byte[] data = new byte[count];
                imgStream.Seek(Startpos, SeekOrigin.Begin);
                imgStream.Read(data, 0, count);


                SKBuiltinString_ data_ = new SKBuiltinString_();
                data_.buffer = data;
                data_.iLen = (uint)data.Length;

                UploadMsgImgRequest uploadMsgImg_ = new UploadMsgImgRequest()
                {
                    BaseRequest = baseRequest,
                    clientImgId = ClientImgId_,
                    data = data_,
                    dataLen = (uint)data.Length,
                    totalLen = (uint)datatotalength,
                    to = to_,
                    msgType = (uint)3,
                    from = from_,
                    startPos = (uint)Startpos
                };
                Startpos = Startpos + count;
                var src = Util.Serialize(uploadMsgImg_);
                int bufferlen = src.Length;
                //组包
                byte[] SendDate = pack(src, (int)110, bufferlen, 5, true, true);
                //发包
                byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/uploadmsgimg");
                if (RetDate.Length > 32)
                {
                    var packinfo = UnPackHeader(RetDate);
                    //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                    RespProtobuf = packinfo.body;
                    if (packinfo.m_bCompressed)
                    {
                        RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                Debug.Print(RespProtobuf.ToString(16, 2));
                UploadMsgImgResponse_ = Util.Deserialize<UploadMsgImgResponse>(RespProtobuf);

                string ret = JsonConvert.SerializeObject(UploadMsgImgResponse_);
                Console.WriteLine(ret);
            }//

            return UploadMsgImgResponse_;
        }

        /// <summary>
        /// 发送音频文件
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">发送者</param>
        /// <param name="path">音频路径</param>
        /// <param name="voiceFormat">音频格式</param>
        /// <param name="voiceLen">音频长度 10为一秒</param>
        /// <returns></returns>
        public UploadVoiceResponse UploadVoice(string to, string from, string path, VoiceFormat voiceFormat = VoiceFormat.MM_VOICE_FORMAT_AMR, int voiceLen = 100) {
            byte[] RespProtobuf = new byte[0];
            byte[] buf;
            using (FileStream fsRead = new FileStream(path, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                buf = new byte[fsLen];
                fsRead.Read(buf, 0, buf.Length);

            }

            SKBuiltinString_ data_ = new SKBuiltinString_();
            data_.buffer = buf;
            data_.iLen = (uint)buf.Length;
            UploadVoiceRequest uploadVoice_ = new UploadVoiceRequest()
            {
                baseRequest = baseRequest,
                from = from,
                to = to,
                clientMsgId = CurrentTime_().ToString(),
                voiceFormat = voiceFormat,
                voiceLen = voiceLen,
                length = buf.Length,
                data = data_,
                offset = 0,
                endFlag = 1
            };

            var src = Util.Serialize(uploadVoice_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 127, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/uploadvoice");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var UploadVoiceResponse_ = Util.Deserialize<UploadVoiceResponse>(RespProtobuf);

            string ret = JsonConvert.SerializeObject(UploadVoiceResponse_);
            Console.WriteLine(ret);

            return UploadVoiceResponse_;
        }

        /// <summary>
        /// 支付操作 自己研究吧
        /// </summary>
        /// <param name="cgiCmd"></param>
        /// <param name="reqText"></param>
        /// <param name="reqTextWx"></param>
        /// <returns></returns>
        public TenPayResponse TenPay(enMMTenPayCgiCmd cgiCmd, string reqText = "", string reqTextWx = "") {

            byte[] RespProtobuf = new byte[0];

            SKBuiltinString_S reqText_ = new SKBuiltinString_S();
            reqText_.buffer = reqText;
            reqText_.iLen = (uint)reqText.Length;

            SKBuiltinString_S reqTextWx_ = new SKBuiltinString_S();
            reqTextWx_.buffer = reqTextWx;
            reqTextWx_.iLen = (uint)reqTextWx.Length;
            TenPayRequest tenPay_ = new TenPayRequest()
            {
                baseRequest = baseRequest,
                cgiCmd = cgiCmd,
                outPutType = (uint)1,
                reqText = reqText_,
                reqTextWx = reqTextWx_

            };

            var src = Util.Serialize(tenPay_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_TENPAY, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_TENPAY);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var TenPayResponse_ = Util.Deserialize<TenPayResponse>(RespProtobuf);
            return TenPayResponse_;
        }

        /// <summary>
        /// v1 v2操作
        /// </summary>
        /// <param name="opCode">1关注公众号2打招呼 主动添加好友3通过好友请求</param>
        /// <param name="Content">内容</param>
        /// <param name="antispamTicket">v2</param>
        /// <param name="value">v1</param>
        /// <param name="sceneList">1来源QQ2来源邮箱3来源微信号14群聊15手机号18附近的人25漂流瓶29摇一摇30二维码13来源通讯录</param>
        /// <returns></returns>
        public VerifyUserResponese VerifyUser(VerifyUserOpCode opCode, string Content, string antispamTicket, string value, byte sceneList = 0x12) {
            byte[] RespProtobuf = new byte[0];
            VerifyUser[] verifyUser_ = new VerifyUser[1];
            VerifyUser user = new VerifyUser();

            user.value = value;
            
            user.antispamTicket = antispamTicket;
            user.friendFlag = 0;
            user.scanQrcodeFromScene = 0;
            if (opCode == VerifyUserOpCode.MM_VERIFYUSER_VERIFYOK)
            {
                user.verifyUserTicket = antispamTicket;
            }

            

            verifyUser_[0] = user;
            VerifyUserRequest1 verifyUser_b = new VerifyUserRequest1()
            {
                baseRequest = baseRequest,
                SceneListNumFieldNumber = (uint)1,
                opcode = opCode,
                sceneList = new byte[] { sceneList },
                verifyContent = Content,
                verifyUserListSize = 1,
                verifyUserList = verifyUser_,
            };

            var src = Util.Serialize(verifyUser_b);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_VERIFYUSER, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_VERIFYUSER);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var VerifyUseResponse_ = Util.Deserialize<VerifyUserResponese>(RespProtobuf);
            return VerifyUseResponse_;
        }
        /// <summary>
        /// 删除好友 好像有点问题
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public OpLogResponse OpLogDelUser(string userName) {
            byte[] RespProtobuf = new byte[0];

            CmdList optlist = new CmdList();
            CmdItem[] cmdItems_ = new CmdItem[2];


            SKBuiltinString userName_ = new SKBuiltinString();
            userName_.@string = userName;

            ModContact modContact = new ModContact();
            modContact.userName = userName_;
            modContact.bitVal = (uint)2;
            modContact.bitMask = (uint)4294967295;
            modContact.sex = 2;
            DelChatContact delChatContact_ = new DelChatContact();
            delChatContact_.UserName = userName_;

            byte[] modContactBuf = Util.Serialize(modContact);
            byte[] delChatContact = Util.Serialize(delChatContact_);
            //CmdItem item = Util.Deserialize<CmdItem>("0802128B010885011285010A0B0A0978787878787878787812020A001A0022020A0028023202080038FFFFFFFF0F4002480052005A00620068007000820100880101900100B00100B80100C00100D00100D80100880200900200980200AA0206080018002000D2020208009203040A001000A80300B80300C00300CA030408001800D00300F203020800800400880400".ToByteArray(16, 2));
            cmdItems_[0] = new CmdItem();
            cmdItems_[0].cmdid = SyncCmdID.CmdIdModContact;
            cmdItems_[0].cmdBuf = new CmdItem.DATA();
            cmdItems_[0].cmdBuf.data = modContactBuf;
            cmdItems_[0].cmdBuf.len = modContactBuf.Length;

            cmdItems_[1] = new CmdItem();
            cmdItems_[1].cmdid = SyncCmdID.CmdIdDelChatContact;
            cmdItems_[1].cmdBuf = new CmdItem.DATA();
            cmdItems_[1].cmdBuf.data = delChatContact;
            cmdItems_[1].cmdBuf.len = delChatContact.Length;

            optlist.list = cmdItems_;
            optlist.count = cmdItems_.Length;

            OpLogRequest opLog_ = new OpLogRequest() {
                cmd = optlist,

            };

            var src = Util.Serialize(opLog_);
            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_OPLOG, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_OPLOG);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var OpLogResponse_ = Util.Deserialize<OpLogResponse>(RespProtobuf);
            return OpLogResponse_;
        }

        /// <summary>
        /// 操作公众号菜单 接口已不行
        /// </summary>
        /// <param name="BizUserName"></param>
        /// <param name="ClickInfo"></param>
        /// <returns></returns>
        public micromsg.ClickCommandResponse ClickCommand(string BizUserName,string ClickInfo)
        {
            byte[] RespProtobuf = new byte[0];

            micromsg.ClickCommandRequest clickCommand_ = new micromsg.ClickCommandRequest() {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                BizUserName = BizUserName,
                ClickInfo = ClickInfo,
                ClickType = (uint)1,
            };

            var src = Util.Serialize(clickCommand_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 359, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/clickcommand");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var ClickCommandResponse_ = Util.Deserialize<micromsg.ClickCommandResponse>(RespProtobuf);
           
            return ClickCommandResponse_;
        }

        /// <summary>
        ///摇一摇
        /// </summary>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <returns></returns>
        public micromsg.ShakeGetResponse ShakeReport(float Latitude,float Longitude) {

            byte[] RespProtobuf = new byte[0];

            //mm.command.ShakereportRequest shakeReport_ = GoogleProto.shakereport(Latitude, Longitude, "49aa7db2f4a3ffe0e96218f6b92cde32", Encoding.Default.GetString(GetAESkey()), (uint)m_uid, "iPad iPhone OS9.3.3");
            micromsg.ShakeReportRequest shakeReport_ = new micromsg.ShakeReportRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                GPSSource = 0,
                ImgId = 0,
                Latitude = Latitude,
                Longitude = Longitude,
                OpCode = 0,
                Precision = 0,
                Times = 1,
            };


            var src = Util.Serialize(shakeReport_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 161, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/shakereport");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var ShakeReportResponse_ = Util.Deserialize<micromsg.ShakeReportResponse>(RespProtobuf);

            if (ShakeReportResponse_.BaseResponse.Ret == 0 && ShakeReportResponse_.Buffer.Buffer != null)
            {

                var ShakeGetResponse_ = this.ShakeGet(ShakeReportResponse_.Buffer);
                return ShakeGetResponse_;
            }

            return null;
        }

        /// <summary>
        /// 摇一摇提交bufkey
        /// </summary>
        /// <param name="sKBuiltinBuffer_T"></param>
        /// <returns></returns>
        private micromsg.ShakeGetResponse ShakeGet(micromsg.SKBuiltinBuffer_t sKBuiltinBuffer_T) {

            byte[] RespProtobuf = new byte[0];
            micromsg.ShakeGetRequest shakeGet_ = new micromsg.ShakeGetRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                Buffer = sKBuiltinBuffer_T
            };

            var src = Util.Serialize(shakeGet_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 162, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/shakeget");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var ShakeGetResponse_ = Util.Deserialize<micromsg.ShakeGetResponse>(RespProtobuf);
            return ShakeGetResponse_;
        }

        /// <summary>
        /// 搜索wxid 
        /// </summary>
        /// <param name="UserName">欲搜索的微信id</param>
        /// <param name="ChatRoom">可获取取=群详细</param>
        /// <returns></returns>
        public micromsg.GetContactResponse GetContact_b(string UserName,string ChatRoom ="") {

            byte[] RespProtobuf = new byte[0];

            micromsg.SKBuiltinString_t UserName_ = new micromsg.SKBuiltinString_t();
            UserName_.String = UserName;

            micromsg.GetContactRequest getContact_a = new micromsg.GetContactRequest() {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };
            getContact_a.UserCount = 1;
            getContact_a.UserNameList.Add(UserName_);

            if (ChatRoom!="") {
                micromsg.SKBuiltinString_t ChatRoom_ = new micromsg.SKBuiltinString_t();
                ChatRoom_.String = ChatRoom;

                getContact_a.FromChatRoomCount = 1;
                getContact_a.FromChatRoom.Add(ChatRoom_);
            }

            var src = Util.Serialize(getContact_a);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETCONTACT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate,URL.CGI_GETCONTACT);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var GetContactResponse_ = Util.Deserialize<micromsg.GetContactResponse>(RespProtobuf);
            return GetContactResponse_;
        }


        /// <summary>
        /// 短信操作 用于登录
        /// </summary>
        /// <param name="Mobile_"></param>
        /// <param name="verifycode"></param>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public micromsg.BindOpMobileResponse MobileLogin(string Mobile_,string verifycode = "" , int opcode = 16) {

            byte[] RespProtobuf = new byte[0];

            micromsg.BindOpMobileRequest bindOpMobile_ = new micromsg.BindOpMobileRequest()
            {
                BaseRequest = new micromsg.BaseRequest() {
                    ClientVersion = ver,
                    Scene =1,
                    Uin = 0,
                    DeviceID = "49aa7db2f4a3ffe0e96218f6b91cde31".ToByteArray(16,2),
                    DeviceType = System.Text.Encoding.ASCII.GetBytes("iPhone OS7.1.2"),
                    SessionKey = GetAESkey(),
                    
                },
                Mobile = Mobile_,
                Opcode = opcode, 
                Verifycode = verifycode,
                DialFlag = 0,//0短信验证码1语音验证码
                ClientSeqID = "49aa7db2f4a3ffe0e96218f6b91cde31",
                SafeDeviceName = "iPhone OS7.1.2",
                SafeDeviceType = "Nagibator",
                InputMobileRetrys = 5,
                AdjustRet = 0,
                RandomEncryKey = new micromsg.SKBuiltinBuffer_t() {
                    Buffer = GetAESkey(),
                    iLen = 16,
                },
            };

            var src = Util.Serialize(bindOpMobile_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] pbody;
            var head = MakeHead(145, src.Length,7);

            pbody = head.Concat(Util.nocompress_rsa(src)).ToArray();
            byte[] SendDate = pbody;
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/bindopmobileforreg");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var BindOpMobileResponse_ = Util.Deserialize<micromsg.BindOpMobileResponse>(RespProtobuf);
            //var BindOpMobileResponse_ = mm.command.BindopMobileForRegResponse.ParseFrom(RespProtobuf);
            return BindOpMobileResponse_;
        }

        /// <summary>
        /// 短信操作 用于注册
        /// </summary>
        /// <param name="Mobile_"></param>
        /// <param name="verifycode"></param>
        /// <param name="opcode"></param>
        /// <param name="AuthTicket">获取滑块时返回</param>
        /// <returns></returns>
        public micromsg.BindOpMobileForRegResponse BindOpMobileRegFor(string Mobile_, string regSessionId = "", int opcode = 16, string AuthTicket = "",string Verifycode = "")
        {

            byte[] RespProtobuf = new byte[0];

            micromsg.BindOpMobileForRegRequest bindOpMobile_ = new micromsg.BindOpMobileForRegRequest()
            {
                BaseRequest = new micromsg.BaseRequest()
                {
                    ClientVersion = 637993779,
                                    




                    Scene = 0,
                    Uin = 0,
                    DeviceID = Encoding.Default.GetBytes("A4c115f4d7ae5c4"),
                    DeviceType = System.Text.Encoding.ASCII.GetBytes("android-25"),
                    SessionKey = GetAESkey(),
                    
                    
                },
                Mobile = Mobile_,
                Opcode = opcode,
                Verifycode = Verifycode,//DialFlag = 1 国外注册用 时 填写语音验证码或国外短信验证码 DialFlag = 0时Verifycode = "" 国内注册用
                DialFlag = 0,//0短信验证码1语音验证
                ClientSeqID = "A4c115f4d7ae5c4_" + CurrentTime_().ToString(),
                SafeDeviceName = "iPhone",
                SafeDeviceType = @"<softtype><lctmoc>1</lctmoc><level>1</level><k25>79ed40c93970f31fc8ee7d8a14a5c32a</k25><k27>4ff485ad567ca4eacfc37ea93e3c3d3a37893d6f</k27><k32></k32><k1>Qualcomm MSM8974PRO-AC</k1><k2>M4333A-2.0.50.2.29</k2><k3>3.2</k3><k4>A0000037306755</k4><k5>460032673141447</k5><k6></k6><k7>ff82dfc8005e8c3d</k7><k8>b46252dc4eef3</k8><k9>ZTE N855D</k9><k10>13</k10><k11>Qualcomm MSM 8974 HAMMERHEAD (Flattened Device Tree)</k11><k12>000b</k12><k13>0000000000000000</k13><k14>79:e3:18:dd:69:42</k14><k15>dd:24:50:71:30:e0</k15><k16>swp half thumb fastmult vfp edsp neon vfpv3 tls vfpv4 idiva idivt</k16><k18>62c103f6222aa52b2ab3222425ba62ed</k18><k21>573f4b30b2</k21><k22>Verizon</k22><k24>62:52:cb:be:42:32</k24><k26>0</k26><k30></k30><k33>com.tencent.mm</k33><k34>google/hammerhead/hammerhead:4.4/KRT16M/893803:user/release-keys</k34><k35>ZTE N855D</k35><k36>N855D</k36><k37>ZTE</k37><k38>ZTE N855D</k38><k39>Qualcomm MSM8974PRO-AC</k39><k40>ZTE N855D</k40><k41>0</k41><k42>ZTE</k42><k43>null</k43><k44>0</k44><k45>db4e103bdc62a52d3b3d3bf42d32cb22</k45><k46>1</k46><k47>wifi</k47><k48>661052735676225</k48><k49>/data/data/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>1300</k57><k58></k58><k59>0</k59><k61>true</k61></softtype>",
                InputMobileRetrys = 5,
                AdjustRet = 0,
                ForceReg = 1,
                //AuthTicket = AuthTicket,
                regSessionId = regSessionId,
                MobileCheckType = 0,//国外号码注册时时0国内请填写1
                
                RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = GetAESkey(),
                    iLen = 16,
                },
            };

            var src = Util.Serialize(bindOpMobile_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] pbody;
            var head = MakeHead(145, src.Length, 7);

            pbody = head.Concat(Util.compress_rsa(src)).ToArray();
            byte[] SendDate = pbody;
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/bindopmobileforreg");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var BindOpMobileResponse_ = Util.Deserialize<micromsg.BindOpMobileForRegResponse>(RespProtobuf);
            //var BindOpMobileResponse_ = mm.command.BindopMobileForRegResponse.ParseFrom(RespProtobuf);
            return BindOpMobileResponse_;
        }

        /// <summary>
        /// 注册微信账号
        /// </summary>
        /// <param name="RegSessionId">滑块时返回</param>
        /// <param name="Ticket">成功发送注册码时返回</param>
        /// <param name="ClientFingerprint">设备信息</param>
        /// <returns></returns>
        public micromsg.NewRegResponse NewReg(string RegSessionId,string Ticket,string BindMobile) {

            OpenSSLNativeClass.ECDH.GenEcdh(713, ref pub_key_buf, ref pri_key_buf);
            byte[] RespProtobuf = new byte[0];
            mm.command.BaseRequest.Builder base_ = new mm.command.BaseRequest.Builder() {
                ClientVersion = 637993779,
                Scene = 0,
                Uin = 0,
                SessionKey = Google.ProtocolBuffers.ByteString.CopyFrom(new byte[0]),
                DeviceID = Google.ProtocolBuffers.ByteString.CopyFrom("A4c115f4d7ae5c4", Encoding.Default),
                DeviceType = Google.ProtocolBuffers.ByteString.CopyFrom("android-25", Encoding.Default),
            };

            mm.command.NewRegRequest.Builder newReg_ = new mm.command.NewRegRequest.Builder() {

                Base = base_.Build(),
                UserName = "",
                
                Language = "zh_CN",
                //AdSource = "1B9D23EE-10FB-4500-99E5-615559AF39B1",
                //AndroidId = "a25be0fba88b586f",
                RegSessionId = RegSessionId,
                MobileCheckType = 0,
                BindMobile = BindMobile,
                Ticket = Ticket,
                BindUin = 0,
                GoogleAid = "",
                DLSrc = 0,
                BuiltinIPSeq = 0,
                BindEmail = "",
                RegMode  =1,
                ClientSeqId = "A4c115f4d7ae5c4_" + CurrentTime_().ToString(),
                NickName = "xxx",
                Pwd = Util.MD5Encrypt("Zz123456789"),
                TimeZone = "8.00",
                AndroidInstallRef = "",
                BioSigCheckType = 0,
                Alias = "",
                RealCountry = "cn",
                MacAddr = "00:24:91:44:88:C6",
                AndroidId = "98ji6ho4f0hrnml",
                ForceReg = 1,
                ClientFingerprint = @"<softtype><lctmoc>0</lctmoc><level>1</level><k1>ARMv7 Processor rev 1 (v7l) </k1><k2>MPSS.DI.4.0-eaa9d90</k2><k3>4.3.1</k3><k4>866775983822339</k4><k5>460053260245314</k5><k6>06725660317388918913</k6><k7>98ji6ho4f0hrnml</k7><k8>o39rnsj4ucc0g</k8><k9>G0128</k9><k10>4</k10><k11>Qualcomm MSM8974PRO-AC</k11><k12>0000</k12><k13>0000000000000000</k13><k14>00:24:91:44:88:C6</k14><k15></k15><k16>swp half thumb fastmult vfp edsp neon vfpv3 tls vfpv4 idiva idivt</k16><k18>18c867f0717aa67b2ab7347505ba07ed</k18><k21>FAST_KYBC9XVX</k21><k22></k22><k24>48:7d:2e:b6:1b:ec</k24><k26>0</k26><k30></k30><k33>com.tencent.mm</k33><k34>Xiaomi/cancro_lte_ct/cancro:6.0.1/MMB29M/8.4.12:user/release-keys</k34><k35>GREE</k35><k36>unknown</k36><k37>GREE</k37><k38>G0128</k38><k39>G0128</k39><k40>GREE</k40><k41>0</k41><k42>GREE</k42><k43>null</k43><k44>0</k44><k45>720ae5f1bb602cc7e44eae2f4e920774</k45><k46></k46><k47>13</k47><k48>866775983822339</k48><k49>/data/user/0/com.tencent.mm/</k49><k52>0</k52><k53>0</k53><k57>1260</k57><k58></k58><k59>2</k59></softtype>",
                
                VerifyContent = "",
                VerifySignature = "",
                HasHeadImg = 0,
                SuggestRet = 0,
                




            };
            newReg_.SetRandomEncryKey(new mm.command.SKBuiltinBuffer_t.Builder().SetILen(16).SetBuffer(ByteString.CopyFrom(GetAESkey())).Build());
            newReg_.SetCliPubEcdhkey(new mm.command.ECDHKey.Builder().SetKey(new mm.command.SKBuiltinBuffer_t.Builder().SetBuffer(ByteString.CopyFrom(pub_key_buf)).SetILen(pub_key_buf.Length)).SetNID(713));
            var src = newReg_.Build().ToByteArray();

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] pbody;

            //byte[] subHeader = new byte[] { };




            //byte[] zipData  = ZipUtils.compressBytes(src);
            // int lenAfterZip = zipData.Length;

            //src = Util.nocompress_rsa (src);



            //RegRequestPacket(bufferlen, src.Length, src, 126);

            var head = MakeHead(126, src.Length, 7);
            //src = Util.nocompress_rsa(src);
            pbody = head.Concat(Util.compress_rsa(src)).ToArray();
            //RegRequestPacket(src.Length, lenAfterZip, src, 126);
            byte[] SendDate = pbody; //RegRequestPacket(bufferlen,src.Length,src, 126); ;
            Debug.Print(SendDate.ToString(16, 2));
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/newreg");
            
            if (RetDate.Length > 32)
            {


                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var NewRegResponse_ = Util.Deserialize<micromsg.NewRegResponse>(RespProtobuf);
            //var BindOpMobileResponse_ = mm.command.BindopMobileForRegResponse.ParseFrom(RespProtobuf);
            return NewRegResponse_;
        }


        public micromsg.BindOpMobileResponse BindMobile(string Mobile_, string verifycode = "", int opcode = 16)
        {

            byte[] RespProtobuf = new byte[0];

            micromsg.BindOpMobileRequest bindOpMobile_ = new micromsg.BindOpMobileRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                Mobile = Mobile_,
                Opcode = opcode,
                Verifycode = verifycode,
                DialFlag = 0,//0短信验证码1语音验证码
                ClientSeqID = "49aa7db2f4a3ffe0e96218f6b91cde31"+"-"+CurrentTime_().ToString(),
                SafeDeviceName = "1",
                Language = "zh_CN",
                //InputMobileRetrys = 5,
                //AdjustRet = 0,
                RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = GetAESkey(),
                    iLen = 16,
                },
                UserName = ""
            };

            var src = Util.Serialize(bindOpMobile_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] pbody;
            byte[] SendDate = pack(src, 145, bufferlen,5,true,true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/bindopmobile");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var BindOpMobileResponse_ = Util.Deserialize<micromsg.BindOpMobileResponse>(RespProtobuf);
            //var BindOpMobileResponse_ = mm.command.BindopMobileForRegResponse.ParseFrom(RespProtobuf);
            return BindOpMobileResponse_;
        }

        public micromsg.NewRegResponse NewReg2(string ticket) {
            micromsg.NewRegRequest newReg = new micromsg.NewRegRequest()
            {
                BaseRequest = new micromsg.BaseRequest
                {
                    ClientVersion = ver,
                    Scene = 0,
                    Uin = 0,
                    SessionKey = GetAESkey(),
                    DeviceID = System.Text.Encoding.ASCII.GetBytes("Aff0aef642a31fc1"),
                    DeviceType = System.Text.Encoding.ASCII.GetBytes("iPhone OS7.1.2")
                },
                RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = GetAESkey(),
                    iLen = 16
                },
                RealCountry = "cn",
                Ticket = ticket,
                ForceReg = 1,
                BindMobile = "+8617607567005",
                TimeZone = "8.00",
                ClientSeqID = "49aa7db2f4a3ffe0e96218f6b92cde11_" + CurrentTime_().ToString(),
                Language = "zh_CN",
                RegMode = 1,
                MacAddr = "00:E3:B2:76:17:D7",
                NickName = "测试注册",
                Pwd = Util.MD5Encrypt("z13692062050"),
                BindUin = 0,
                ClientFingerprint = "<softtype><k3>8.1.3</k3><k9>iPhone</k9><k10>2</k10><k19>2D4F30F1-7A99-4AE5-A5D0-EE225154E486</k19><k20>1B9D23EE-10FB-4500-99E5-615559AF39B1</k20><k21></k21><k22>中国联通</k22><k24></k24><k33>微信</k33><k47>3</k47><k50>0</k50><k51>com.tencent.xin</k51><k54>iPhone52</k54></softtype>",
                BundleID = "com.tencent.xin",
                AndroidID = "a25be0fba88b586f",
                DLSrc = 0,
                
                
                //GoogleAid = "android-16"
            };

            var src = Util.Serialize(newReg);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] pbody, RespProtobuf = new byte[0];


            var head = MakeHead(126, src.Length, 7);
            //src = Util.nocompress_rsa(src);
            pbody = head.Concat(Util.compress_rsa(src)).ToArray();
            //RegRequestPacket(src.Length, lenAfterZip, src, 126);
            byte[] SendDate = pbody; //RegRequestPacket(bufferlen,src.Length,src, 126); ;
            Debug.Print(SendDate.ToString(16, 2));
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/newreg");

            if (RetDate.Length > 32)
            {


                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var NewRegResponse_ = Util.Deserialize<micromsg.NewRegResponse>(RespProtobuf);
            //var BindOpMobileResponse_ = mm.command.BindopMobileForRegResponse.ParseFrom(RespProtobuf);
            return NewRegResponse_;
        }

        /// <summary>
        /// 同步收藏 
        /// </summary>
        /// <param name="keybuf">第二次请求需要带上第一次返回的</param>
        /// <returns></returns>
        public micromsg.FavSyncResponse FavSync(byte[] keybuf = null) {
            byte[] RespProtobuf = keybuf;

            micromsg.SKBuiltinBuffer_t keybuf_ = new micromsg.SKBuiltinBuffer_t();
            if (keybuf != null) {
                keybuf_.Buffer = keybuf;
                keybuf_.iLen = (uint)keybuf.Length;
            }

            micromsg.FavSyncRequest favSync_ = new micromsg.FavSyncRequest()
            {
                KeyBuf = keybuf_,
                Selector = 1,
            };

            var src = Util.Serialize(favSync_);

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_FAVSYNC, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_FAVSYNC);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var FavSyncResponse_ = Util.Deserialize<micromsg.FavSyncResponse>(RespProtobuf);
            return FavSyncResponse_;
        }

        /// <summary>
        ///获取单条收藏
        /// </summary>
        /// <param name="FavId">收藏id</param>
        /// <returns></returns>
        public micromsg.BatchGetFavItemResponse GetFavItem(int FavId) {



            micromsg.BatchGetFavItemRequest batchGetFavItem = new micromsg.BatchGetFavItemRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };
            batchGetFavItem.Count = 1;
            batchGetFavItem.FavIdList.Add((uint)FavId);

            var src = Util.Serialize(batchGetFavItem);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_BATCHGETFAVITEM, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_BATCHGETFAVITEM);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var BatchGetFavItemResponse_ = Util.Deserialize<micromsg.BatchGetFavItemResponse>(RespProtobuf);
            return BatchGetFavItemResponse_;
        }

        /// <summary>
        /// 删除收藏 这里可删除多条收藏
        /// </summary>
        /// <param name="FavId">收藏id</param>
        /// <returns></returns>
        public micromsg.BatchDelFavItemResponse DelFavItem(uint[] FavId)
        {



            micromsg.BatchDelFavItemRequest DelFavItem = new micromsg.BatchDelFavItemRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };
            DelFavItem.Count = (uint)FavId.Length;
            foreach (uint ID in FavId) {
                DelFavItem.FavIdList.Add(ID);
            }
           

            var src = Util.Serialize(DelFavItem);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 484, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/batchdelfavitem");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var BatchDelFavItemResponse_ = Util.Deserialize<micromsg.BatchDelFavItemResponse>(RespProtobuf);
            return BatchDelFavItemResponse_;
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="object_"></param>
        /// <param name="SourceId_"></param>
        /// <returns></returns>
        public micromsg.AddFavItemResponse addFavItem(string object_ ,string SourceId_ = "11986025506179002475") {
            micromsg.AddFavItemRequest addFav = new micromsg.AddFavItemRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                ClientId = CurrentTime_().ToString(),
                Object = object_,
                Type = 1,
                SourceId = SourceId_,
                SourceType = 2,
            };

            var src = Util.Serialize(addFav);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 401, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/addfavitem");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var AddFavItemResponse_ = Util.Deserialize<micromsg.AddFavItemResponse>(RespProtobuf);
            return AddFavItemResponse_;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public micromsg.LogOutResponse logOut() {
            micromsg.LogOutRequest logOut_ = new micromsg.LogOutRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                Scene = 0,
            };

            var src = Util.Serialize(logOut_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 282, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/logout");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var LogOutResponse_ = Util.Deserialize<micromsg.LogOutResponse>(RespProtobuf);
            return LogOutResponse_;
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="LabelName">标签名</param>
        /// <returns></returns>
        public micromsg.AddContactLabelResponse AddContactLabel(string LabelName) {
            micromsg.AddContactLabelRequest addContactLabel_ = new micromsg.AddContactLabelRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };
            
            micromsg.LabelPair label = new micromsg.LabelPair()
            {
                LabelID = 0,
                LabelName = LabelName
            };

            addContactLabel_.LabelPairList.Add(label);
            addContactLabel_.LabelCount = 1;

            var src = Util.Serialize(addContactLabel_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 635, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/addcontactlabel");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var AddContactLabelResponse_ = Util.Deserialize<micromsg.AddContactLabelResponse>(RespProtobuf);
            return AddContactLabelResponse_;
        }

        /// <summary>
        /// 修改标签列表
        /// </summary>
        /// <param name="UserLabelInfo"></param>
        /// <returns></returns>
        public micromsg.ModifyContactLabelListResponse ModifyContactLabelList(micromsg.UserLabelInfo[] UserLabelInfo) {
            micromsg.ModifyContactLabelListRequest ModifyContactLabelList_ = new micromsg.ModifyContactLabelListRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };
            foreach (var id in UserLabelInfo) {
                ModifyContactLabelList_.UserLabelInfoList.Add(id);

            }
            ModifyContactLabelList_.UserCount = (uint)UserLabelInfo.Length;

            var src = Util.Serialize(ModifyContactLabelList_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 638, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/modifycontactlabellist");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var ModifyContactLabelListResponse_ = Util.Deserialize<micromsg.ModifyContactLabelListResponse>(RespProtobuf);
            return ModifyContactLabelListResponse_;


        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="LabelIDList_">欲删除的标签id</param>
        /// <returns></returns>
        public micromsg.DelContactLabelResponse DelContactLabel(string LabelIDList_) {
            micromsg.DelContactLabelRequest delContactLabel = new micromsg.DelContactLabelRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                LabelIDList = LabelIDList_
            };

            var src = Util.Serialize(delContactLabel);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 636, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/delcontactlabel");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var DelContactLabelResponse_ = Util.Deserialize<micromsg.DelContactLabelResponse>(RespProtobuf);
            return DelContactLabelResponse_;

        }

        /// <summary>
        /// 上传朋友圈图片不是发朋友圈图片
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns></returns>
        public micromsg.SnsUploadResponse SnsUpload(string path) {

             byte[] RespProtobuf = new byte[0];

            MemoryStream imgStream = new MemoryStream();
            using (FileStream fsRead = new FileStream(path, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                fsRead.Seek(0, SeekOrigin.Begin);
                fsRead.CopyTo(imgStream, fsLen);
                Console.WriteLine("Img buf length {0}", fsLen);

            }
            int Startpos = 0;//起始位置
            int datalen = 65535;//数据分块长度
            long datatotalength = imgStream.Length;
            var SnsUploadResponse_ = new micromsg.SnsUploadResponse();
            
            while (Startpos != (int)datatotalength)
            {//
                int count = 0;
                if (datatotalength - Startpos > datalen)
                {
                    count = datalen;
                }
                else
                {

                    count = (int)datatotalength - Startpos;
                }

                byte[] data = new byte[count];
                imgStream.Seek(Startpos, SeekOrigin.Begin);
                imgStream.Read(data, 0, count);


                micromsg.SKBuiltinBuffer_t data_ = new micromsg.SKBuiltinBuffer_t();
                data_.Buffer = data;
                data_.iLen = (uint)data.Length;

                micromsg.SnsUploadRequest snsUpload_ = new micromsg.SnsUploadRequest()
                {
                    BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                    ClientId = CurrentTime_().ToString(),
                    TotalLen = (uint)datatotalength,
                    StartPos = (uint)Startpos,
                    Buffer = data_,
                    Type = 2
                };
               

                var src = Util.Serialize(snsUpload_);

                //byte[] RespProtobuf = new byte[0];

                Debug.Print(src.ToString(16, 2));
                int bufferlen = src.Length;
                //组包
                byte[] SendDate = pack(src, 207, bufferlen, 5, true, true);
                //发包
                byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/snsupload");
                if (RetDate.Length > 32)
                {
                    var packinfo = UnPackHeader(RetDate);
                    //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                    RespProtobuf = packinfo.body;
                    if (packinfo.m_bCompressed)
                    {
                        RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                Debug.Print(RespProtobuf.ToString(16, 2));
                SnsUploadResponse_ = Util.Deserialize<micromsg.SnsUploadResponse>(RespProtobuf);

                if (SnsUploadResponse_.BaseResponse.Ret == 0) {
                    Startpos = Startpos + count;
                }
            }
            return SnsUploadResponse_;
        }

        /// <summary>
        /// 发送app信息
        /// </summary>
        /// <param name="Content">xml内容</param>
        /// <param name="ToUserName">接收人</param>
        /// <param name="FromUserName">发送人</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public micromsg.SendAppMsgResponse SendAppMsg(string Content,string ToUserName,string FromUserName, int type=8)
        {
            micromsg.SendAppMsgRequest sendAppMsg_ = new micromsg.SendAppMsgRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };
            sendAppMsg_.Msg = new micromsg.AppMsg();
            sendAppMsg_.Msg.ClientMsgId = CurrentTime_().ToString();
            sendAppMsg_.Msg.Content = Content;
            sendAppMsg_.Msg.ToUserName = ToUserName;
            sendAppMsg_.Msg.FromUserName = FromUserName;
            sendAppMsg_.Msg.Type = 8;

            var src = Util.Serialize(sendAppMsg_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 222, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/sendappmsg");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SendAppMsgResponse_ = Util.Deserialize<micromsg.SendAppMsgResponse>(RespProtobuf);
            return SendAppMsgResponse_;

        }

        /// <summary>
        /// 发送群公告
        /// </summary>
        /// <param name="ChatRoomName">群wxid</param>
        /// <param name="Announcement">公告内容</param>
        /// <returns></returns>
        public micromsg.SetChatRoomAnnouncementResponse setChatRoomAnnouncement(string ChatRoomName, string Announcement) {
            micromsg.SetChatRoomAnnouncementRequest setChatRoomAnnouncement_ = new micromsg.SetChatRoomAnnouncementRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                Announcement = Announcement,
                ChatRoomName = ChatRoomName
            };

            var src = Util.Serialize(setChatRoomAnnouncement_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_SETCHATROOMANNOUNCEMENT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/setchatroomannouncement");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var SetChatRoomAnnouncementResponse_ = Util.Deserialize<micromsg.SetChatRoomAnnouncementResponse>(RespProtobuf);
            return SetChatRoomAnnouncementResponse_;
        }

        /// <summary>
        /// 接收高清大图
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="datatotalength">图片长度xml里面找</param>
        /// <param name="MsgId">必须有这个才行</param>
        /// <param name="ToUserName">接收人</param>
        /// <param name="FromUserName">发送人</param>
        /// <param name="CompressType">
        /// //当xml只中含有length 字段时 CompressType = 0 可以下载高清(有压缩情况)
        /// 当xml中含有hdlength 字段时 CompressType = 1 可以下载高清
        /// </param>
        /// <returns></returns>
        public byte[] GetMsgImg(string path,long datatotalength, uint MsgId,string ToUserName,string FromUserName, uint CompressType = 1) {

            int Startpos = 0;//起始位置
            int datalen = 30720;//数据分块长度
            //long datatotalength = 1238782;//根据需要选择下载 高清图还是缩略图 长度自然是对应 高清长度和缩略长度
            List<byte> downImgData = new List<byte>();
            var GetMsgImgResponse_ = new micromsg.GetMsgImgResponse();
            while (Startpos != (int)datatotalength)
            {//
                int count = 0;
                if (datatotalength - Startpos > datalen)
                {
                    count = datalen;
                }
                else
                {
                    count = (int)datatotalength - Startpos;
                }

                micromsg.SKBuiltinString_t ToUserName_ = new micromsg.SKBuiltinString_t();
                ToUserName_.String = ToUserName;

                micromsg.SKBuiltinString_t FromUserName_ = new micromsg.SKBuiltinString_t();
                FromUserName_.String = FromUserName;
                micromsg.GetMsgImgRequest getMsgImg_ = new micromsg.GetMsgImgRequest() {
                    BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                    MsgId = MsgId,
                    StartPos = (uint)Startpos,
                    DataLen = (uint)count,
                    TotalLen = (uint)datatotalength,
                    CompressType = CompressType,//hdlength  1高清0缩略
                    ToUserName = ToUserName_,
                    FromUserName = FromUserName_
                };

                var src = Util.Serialize(getMsgImg_);

                byte[] RespProtobuf = new byte[0];

                Debug.Print(src.ToString(16, 2));
                int bufferlen = src.Length;
                //组包
                byte[] SendDate = pack(src, 109, bufferlen, 5, true, true);
                //发包
                byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/getmsgimg");
                if (RetDate.Length > 32)
                {
                    var packinfo = UnPackHeader(RetDate);
                    //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                    RespProtobuf = packinfo.body;
                    if (packinfo.m_bCompressed)
                    {
                        RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                Debug.Print(RespProtobuf.ToString(16, 2));
                GetMsgImgResponse_ = Util.Deserialize<micromsg.GetMsgImgResponse>(RespProtobuf);
                if (GetMsgImgResponse_.Data == null) { continue; }
                if (GetMsgImgResponse_.Data.iLen != 0) {
                    downImgData.AddRange(GetMsgImgResponse_.Data.Buffer);
                    Startpos = Startpos + GetMsgImgResponse_.Data.Buffer.Length;
                }
            }

            return downImgData.ToArray();
        }

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="url">二维码地址</param>
        /// <returns></returns>
        public micromsg.ExtDeviceLoginConfirmGetResponse ExtDeviceLoginConfirmGet(string url) {

            micromsg.ExtDeviceLoginConfirmGetRequest extDeviceLoginConfirmGet_ = new micromsg.ExtDeviceLoginConfirmGetRequest()
            {
                LoginUrl = url
            };

            var src = Util.Serialize(extDeviceLoginConfirmGet_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 971, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/extdeviceloginconfirmget");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
        
            var ExtDeviceLoginConfirmGetResponse_ = Util.Deserialize<micromsg.ExtDeviceLoginConfirmGetResponse>(RespProtobuf);
            return ExtDeviceLoginConfirmGetResponse_;
        }




        public micromsg.BindEmailResponse BindEmail(string Email,int opcode = 1) {
            micromsg.BindEmailRequest bindEmail_ = new micromsg.BindEmailRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                Email = Email,
                OpCode = (uint)opcode,
            };

            var src = Util.Serialize(bindEmail_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 256, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/bindemail");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var BindEmailResponse_ = Util.Deserialize<micromsg.BindEmailResponse>(RespProtobuf);
            return BindEmailResponse_;
        }

        /// <summary>
        ///发送cdn图片 
        /// </summary>
        /// <param name="AESKey_">aes key</param>
        /// <param name="CDNMidImgSize"></param>
        /// <param name="CDNThumbImgSize"></param>
        /// <param name="Url"></param>
        /// <param name="to"></param>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public micromsg.UploadMsgImgResponse UploadMsgImgCDN(string AESKey_,int CDNMidImgSize,int CDNThumbImgSize,string Url,string to,string FromUserName) {
            micromsg.UploadMsgImgRequest msgImgRequestCDN = new micromsg.UploadMsgImgRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                AESKey = AESKey_,
                CDNThumbAESKey = AESKey_,
                CDNMidImgSize = CDNMidImgSize,
                CDNThumbImgSize = CDNThumbImgSize,
                CDNMidImgUrl = Url,
                CDNThumbImgUrl = Url,
                TotalLen = (uint)CDNMidImgSize,
                DataLen = (uint)CDNMidImgSize,
                ToUserName = new micromsg.SKBuiltinString_t() {
                    String = to
                },
                EncryVer = 1,
                MsgType = 3,
                ClientImgId = new micromsg.SKBuiltinString_t() {
                    String = CurrentTime_().ToString()
                },
                StartPos = 0,
                FromUserName = new micromsg.SKBuiltinString_t() {
                    String = FromUserName,
                },
                Data = new micromsg.SKBuiltinBuffer_t() {
                    Buffer = new byte[0],
                    iLen =0,
                }
                
            };

            var src = Util.Serialize(msgImgRequestCDN);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 110, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/uploadmsgimg");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var  UploadMsgImgResponse_ = Util.Deserialize<micromsg.UploadMsgImgResponse>(RespProtobuf);
            return UploadMsgImgResponse_;
        }

        /// <summary>
        /// 发送视频 发送不了
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="VideoFrom"></param>
        /// <returns></returns>
        public micromsg.UploadVideoResponse UploadVideo(string path ,string FromUserName,string ToUserName,int VideoFrom) {
            byte[] buf;
            using (FileStream fsRead = new FileStream(path, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                buf = new byte[fsLen];
                fsRead.Read(buf, 0, buf.Length);

            }

            SKBuiltinString_ data_ = new SKBuiltinString_();
            data_.buffer = buf;
            data_.iLen = (uint)buf.Length;
            micromsg.UploadVideoRequest uploadVoice_ = new micromsg.UploadVideoRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                FromUserName = FromUserName,
                ToUserName = ToUserName,
                ClientMsgId = CurrentTime_().ToString(),
                VideoFrom = VideoFrom,
                VideoData = new micromsg.SKBuiltinBuffer_t() {
                    Buffer = buf,
                    iLen = (uint)buf.Length
        },
                
                PlayLength  = 2,
                VideoTotalLen = (uint)buf.Length,
                VideoStartPos = (uint)buf.Length,
                EncryVer = 1,
                NetworkEnv = 1,
                FuncFlag = 2,
                ThumbData = new micromsg.SKBuiltinBuffer_t() {
                    Buffer =new byte[0],
                    iLen = 0
                    
                },
                CameraType = 2,
                ThumbStartPos = (uint)buf.Length

            };

            var src = Util.Serialize(uploadVoice_);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 149, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/uploadvideo");
            byte[] RespProtobuf = new byte[0];

            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var UploadVideoResponse_ = Util.Deserialize<micromsg.UploadVideoResponse>(RespProtobuf);

            string ret = JsonConvert.SerializeObject(UploadVideoResponse_);
            Console.WriteLine(ret);

            return UploadVideoResponse_;
        }

        /// <summary>
        /// 二次登录
        /// </summary>
        /// <param name="AutoAuthKey">一次成功登录时返回的autoauthkey</param>
        /// <returns></returns>
        public ManualAuthResponse AutoAuthRequest(byte[] AutoAuthKey)
        {
            
            GenerateECKey(713, pub_key_buf, pri_key_buf);
            micromsg.AutoAuthRequest autoAuthRequest = new micromsg.AutoAuthRequest()
            {
                AesReqData = new micromsg.AutoAuthAesReqData()
                {
                    BaseRequest = new micromsg.BaseRequest()
                    {
                        ClientVersion = (int)ver,
                        Scene = 1,
                        DeviceID = devicelId.ToByteArray(16, 2),
                        SessionKey = AESKey.ToByteArray(16, 2),
                        DeviceType = Encoding.Default.GetBytes("iPad iPhone OS9.3.3"),
                        Uin = (uint)m_uid
                    },
                    AutoAuthKey = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = AutoAuthKey,
                        iLen = (uint)AutoAuthKey.Length
                    },
                    BaseReqInfo = new micromsg.BaseAuthReqInfo()
                    {
                        AuthReqFlag = new uint(),
                        AuthTicket = "",
                        CliDBEncryptInfo = new micromsg.SKBuiltinBuffer_t()
                        {
                            Buffer = new byte[0],
                            iLen = 0
                        },
                        CliDBEncryptKey = new micromsg.SKBuiltinBuffer_t()
                        {
                            Buffer = new byte[0],
                            iLen = 0
                        },
                        WTLoginImgReqInfo = new micromsg.WTLoginImgReqInfo()
                        {
                            ImgCode = "",
                            ImgSid = "",
                            KSid = new micromsg.SKBuiltinBuffer_t()
                            {
                                Buffer = new byte[0],
                                iLen = 0
                            },

                        },

                        WxVerifyCodeReqInfo = new micromsg.WxVerifyCodeReqInfo()//3
                        {
                            VerifyContent = "",//2
                            VerifySignature = ""//1
                        },
                        WTLoginReqBuff = new micromsg.SKBuiltinBuffer_t()//1
                        {
                            Buffer = new byte[] { },//2
                            iLen = 0,//1
                        },
                    },
                    IMEI = "3caa7db2f4a3ffe0e96218f6b92cde11",
                    TimeZone = "8.00",
                    DeviceName = "ipad",
                    Language = "zh_CN",
                    BuiltinIPSeq = 0,
                    //DeviceType = "",
                    Signature= "",
                    DeviceType = @"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>52336395-2829-C5BB-CF9C-1B65A2E52EA6</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>b8:73:cf:87:a9:53</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>",
                    
                },
                RsaReqData = new micromsg.AutoAuthRsaReqData() {
                    AesEncryptKey = new micromsg.SKBuiltinBuffer_t() {
                        Buffer = AESKey.ToByteArray(16,2),
                        iLen = 16
                    },
                    CliPubECDHKey = new micromsg.ECDHKey() {
                        Key = new micromsg.SKBuiltinBuffer_t() {
                            Buffer = pub_key_buf,
                            iLen = 57
                        },
                        Nid = 713,
                        
                    }
                }

            };

            autoAuthRequest.AesReqData.ClientSeqID = autoAuthRequest.AesReqData.IMEI +"-"+ ((int)CurrentTime_()).ToString();

            //用rsa对authkey进行压缩加密
            byte[] RsaReqBuf = Util.Serialize(autoAuthRequest.RsaReqData);
            //Console.WriteLine("RsaReq:\r\n" + CommUtil.ToHexStr(RsaReqBuf));
            byte[] rsaData = Util.compress_rsa_LOGIN(RsaReqBuf);

            //用aes对authkey进行压缩加密
            byte[] AuthAesData = Util.compress_aes(RsaReqBuf, AESKey.ToByteArray(16,2));

            //用aes对设备信息进行压缩加密
            byte[] AesReqBuf = Util.Serialize(autoAuthRequest.AesReqData);
            //Console.WriteLine("aesReq:\r\n" + CommUtil.ToHexStr(AesReqBuf));
            byte[] aesData = Util.compress_aes(AesReqBuf, AESKey.ToByteArray(16, 2));


            byte[] Body = new byte[] { };
            Body = Body.Concat(RsaReqBuf.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(AesReqBuf.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(rsaData.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(AuthAesData.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(rsaData).ToArray();
            Body = Body.Concat(AuthAesData).ToArray();
            Body = Body.Concat(aesData).ToArray();

            var head = MakeHead(702, Body.Length, 9, true,true);

            byte[] retData = head.Concat(Body).ToArray();

            Debug.Print(retData.ToString(16, 2));

            byte[] RespProtobuf = new byte[0];

            byte[] RetDate = Util.HttpPost(retData, "/cgi-bin/micromsg-bin/autoauth");
            //Console.WriteLine(RetDate.ToString(16, 2));
            //var ret = HttpPost(@short + MM.URL.CGI_MANUALAUTH, head, null);
            //var lhead = LongLinkPack(LongLinkCmdId.SEND_MANUALAUTH_CMDID, seq++, head.Length);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }

            Debug.Print(RespProtobuf.ToString(16, 2));
            var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
            return ManualAuthResponse;
        }

        /// <summary>
        /// 扫码二次登陆不可用
        /// </summary>
        /// <param name="AutoAuthKey"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public micromsg.PushLoginURLResponse PushLoginURL(byte[] AutoAuthKey,string UserName) {
            PushLoginURLRequest pushLoginURLRequest = new PushLoginURLRequest()
            {
                baseRequest = baseRequest,
                Autoauthkey = new SKBuiltinString_() {
                    buffer = AutoAuthKey,
                    iLen = (uint)AutoAuthKey.Length
                },
                Autoauthticket = "",
                ClientId = "3caa7db2f4a3ffe0e96218f6b92cde32" + "-" + ((int)CurrentTime_()).ToString(),
                Devicename = "ipad",
                //DeviceName ="ipad",
                opcode = 1,
                randomEncryKey = new AesKey() {
                    key = AESKey.ToByteArray(16,2),
                    len = 16
                },
                username = UserName,
                rsa = new RSAPem() {
                    
                }

            };


            var src = Util.Serialize(pushLoginURLRequest);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_PUSHLOGINURL, bufferlen, 1,true,true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_PUSHLOGINURL
);
            byte[] RespProtobuf = new byte[0];

            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var PushLoginURLResponse = Util.Deserialize<micromsg.PushLoginURLResponse>(RespProtobuf);
            return PushLoginURLResponse;
        }

        /// <summary>
        /// 获取详细资料
        /// </summary>
        /// <param name="UserNmae"></param>
        /// <param name="type">1 为获取wxid详细资料 2 为群 3为v2</param>
        /// <returns></returns>
        public micromsg.GetContactResponse GetContact(string UserNmae,int type= 1) {


            micromsg.GetContactRequest getContact = new micromsg.GetContactRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
            };

            if (type == 1)//type == 1 获取为wxid详细资料
            {
                getContact.UserCount = 1;
                getContact.UserNameList.Add(new micromsg.SKBuiltinString_t()
                {
                    String = UserNmae
                });
            }
            else if (type == 2) {//type == 2 获取群详细资料
                getContact.FromChatRoomCount = 1;
                getContact.FromChatRoom.Add(new micromsg.SKBuiltinString_t()
                {
                    String = UserNmae
                });
            } else if (type == 3) {//type == 3 获取为v1 的详细资料
                getContact.AntispamTicketCount = 1;
                getContact.AntispamTicket.Add(new micromsg.SKBuiltinString_t()
                {

                    String = UserNmae
                });
            }

            var src = Util.Serialize(getContact);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETCONTACT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_GETCONTACT);
            byte[] RespProtobuf = new byte[0];

            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var GetContactResponse = Util.Deserialize<micromsg.GetContactResponse>(RespProtobuf);
            return GetContactResponse;

        }

        public micromsg.UploadMContactResponse UploadMContact(string Mobile_,micromsg.Mobile[] UPMobile,string UserName) {
            micromsg.UploadMContactRequest uploadMContact = new micromsg.UploadMContactRequest()
            {
                BaseRequest = Util.Deserialize<micromsg.BaseRequest>(Util.Serialize(baseRequest)),
                Opcode = 1,
                UserName = UserName
            };

            uploadMContact.Mobile = Mobile_;
            foreach (micromsg.Mobile mobile in UPMobile) {
                uploadMContact.MobileList.Add(mobile);
            }

            uploadMContact.MobileListSize = UPMobile.Length;

            var src = Util.Serialize(uploadMContact);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_UPLOADMCONTACT, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, URL.CGI_UPLOADMCONTACT);
            byte[] RespProtobuf = new byte[0];

            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, AESKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));
            var UploadMContactResponse = Util.Deserialize<micromsg.UploadMContactResponse>(RespProtobuf);
            return UploadMContactResponse;

        }

        /// <summary>
        /// 组包
        /// </summary>
        /// <param name="src"></param>
        /// <param name="cgi_"></param>
        /// <param name="nLenProtobuf"></param>
        /// <param name="encodetypr"></param>
        /// <param name="iscookie"></param>
        /// <param name="isuin"></param>
        /// <returns></returns>
        public byte[] pack(byte [] src,int cgi_,int nLenProtobuf, byte encodetypr = 7, bool iscookie = false, bool isuin = false) {
            //组包头
            var pbody = new byte [0];
            if (encodetypr == 7)
            {
                var head = MakeHead(cgi_, src.Length, encodetypr, iscookie, isuin);
                pbody = head.Concat(Util.nocompress_rsa(src)).ToArray();
            }
            else if (encodetypr == 5)
            {
                //计算校验
                uint check_ = check((uint)this.m_uid, src, pri_key_buf);
                //压缩
                byte[] zipData = DeflateCompression.DeflateZip(src);
                int lenAfterZip = zipData.Length;

                //aes加密
                byte[] aesData = Util.AESEncryptorData(zipData, GetAESkey());

                pbody = CommonRequestPacket(src.Length, lenAfterZip, aesData, (uint)m_uid, 0xd, (short)cgi_, cookie.ToByteArray(16, 2), 0);
            }
            else if (encodetypr == 1) {
                var head = MakeHead(cgi_, src.Length, encodetypr, iscookie, isuin);
                pbody = head.Concat(Util.compress_rsa(src)).ToArray();
            }
            return pbody;
        }

        /// <summary>
        /// 解包头 返回 包数据结构
        /// </summary>
        /// <param name="pack"></param>
        /// <returns></returns>
        public PACKINFO UnPackHeader(byte[] pack)
        {
            //Console.WriteLine(pack.ToString(16, 2));
            PACKINFO pACKINFO = new PACKINFO();
            byte[] body = new byte[] { };
            pACKINFO.body = body;
            if (pack.Length < 0x20) return pACKINFO;
            int nCur = 0;
            if (0xbf == pack[nCur])
            {
                nCur++;
            }
            //解析包头长度(前6bits)
            int nHeadLen = pack[nCur] >> 2;

            //是否使用压缩(后2bits)
            pACKINFO.m_bCompressed = (1 == (pack[nCur] & 0x3)) ? true : false;

            nCur++;

            //解密算法(前4 bits)(05:aes / 07:rsa)(仅握手阶段的发包使用rsa公钥加密,由于没有私钥收包一律aes解密)
            pACKINFO.m_nDecryptType = pack[nCur] >> 4;

            //cookie长度(后4 bits)
            int nCookieLen = pack[nCur] & 0xF;

            nCur++;

            //服务器版本,无视(4字节)
            nCur += 4;

            //登录包 保存uin
            //int dwUin;
            m_uid = (int)pack.Copy(nCur, 4).GetUInt32(Endian.Big);
            //memcpy(&dwUin, &(pack[nCur]), 4);
            //s_dwUin = ntohl(dwUin);
            nCur += 4;
            //刷新cookie(超过15字节说明协议头已更新)
            if (nCookieLen > 0 && nCookieLen <= 0xf)
            {
                string s_cookie = pack.Copy(nCur, nCookieLen).ToString(16, 2);
                //pAuthInfo->m_cookie = s_cookie;
                cookie = s_cookie;
                nCur += nCookieLen;
            }
            else if (nCookieLen > 0xf)
            {
                return null;
            }

            //cgi type,变长整数,无视

            int dwLen = DecodeVByte32(ref pACKINFO.CGI, pack.Copy(nCur, 5), 0);
            //pACKINFO. CGI = String2Dword(pack.Copy(nCur, 5));
            nCur += dwLen;

            //解压后protobuf长度，变长整数
            int m_nLenRespProtobuf = 0;//String2Dword(pack.Copy(nCur, 5));
            dwLen = DecodeVByte32(ref m_nLenRespProtobuf, pack.Copy(nCur, 5), 0);
            nCur += dwLen;

            //压缩后(加密前)的protobuf长度，变长整数
            int m_nLenRespCompressed = 0;//String2Dword(pack.Copy(nCur, 5));
            dwLen = DecodeVByte32(ref m_nLenRespCompressed, pack.Copy(nCur, 5), 0);
            nCur += dwLen;

            //后面数据无视

            //解包完毕,取包体
            if (nHeadLen < pack.Length)
            {
                body = pack.Copy(nHeadLen, pack.Length - nHeadLen);
            }
            pACKINFO.body = body;
            //Console.WriteLine("body len{0}", pACKINFO.body.Length);
           // Console.WriteLine(body.ToString(16, 2));
            return pACKINFO;
        }

        public class PACKINFO
        {
            //是否压缩
            public bool m_bCompressed;
            //是否加密
            public int m_nDecryptType;
            // CGi
            public int CGI;
            //包体
            public byte[] body;
        }

        public static long CurrentTime_()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// 计算校验
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="niqData"></param>
        /// <param name="eccKey"></param>
        /// <returns></returns>
        public uint check(uint uin,byte[] niqData,byte[] eccKey) {
            int lenBeforeZip = niqData.Length;
            byte[] byteInt = new byte[4];
            //byteInt[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            //byteInt[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            //byteInt[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            //byteInt[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] md5 = Util.MD5(byteInt.Concat(eccKey).ToArray());

            byteInt[0] = (byte)(((lenBeforeZip & 0xff000000) >> 24) & 0xff);
            byteInt[1] = (byte)(((lenBeforeZip & 0x00ff0000) >> 16) & 0xff);
            byteInt[2] = (byte)(((lenBeforeZip & 0x0000ff00) >> 8) & 0xff);
            byteInt[3] = (byte)((lenBeforeZip & 0x000000ff) & 0xff);
            md5 = Util.MD5(byteInt.Concat(eccKey).Concat(md5).ToArray());

            uint check = Adler32(1, md5, md5.Length);
            check = Adler32(check, niqData, lenBeforeZip);
            return check;
        }

        /// <summary>
        /// 组吧
        /// </summary>
        /// <param name="lengthBeforeZip"></param>
        /// <param name="lengthAfterZip"></param>
        /// <param name="aesDataPacket"></param>
        /// <param name="uin"></param>
        /// <param name="deviceID"></param>
        /// <param name="_byteVar"></param>
        /// <returns></returns>
        public byte[] CommonRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] aesDataPacket, uint uin,
            short cmd, short cmd2, byte[] cookie, uint check)
        {
            byte[] frontPacket = {
                                     0xBF, 0x62, 0x50, 0x16, 0x07, 0x03, 0x21
                                 };
            Debug.Print(frontPacket.ToString(16, 2));
            byte[] endTag = { 0x02 };
            byte[] byteUin = new byte[4];

            uint a = (uin & 0xff000000);
            byteUin[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteUin[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteUin[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteUin[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] packet = frontPacket.Concat(byteUin).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(cookie).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(cmd2)).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(lengthBeforeZip)).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(lengthAfterZip)).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(10000)).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(endTag).ToArray();
            packet = packet.Concat(toVariant((int)check)).ToArray();
            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(0x01004567)).ToArray();
            Debug.Print(packet.ToString(16, 2));
            int HeadLen = packet.Length;
            Debug.Print(packet.ToString(16, 2));
            packet[1] = (byte)((HeadLen * 4) + 1);
            packet[2] = (byte)(0x50 + cookie.Length);

            Debug.Print(packet.ToString(16,2));
            packet = packet.Concat(aesDataPacket).ToArray();

            return packet;
        }

        public byte[] RegRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] rsaDataPacket, int cmd)
        {
            byte[] frontPacket = {
                                     0xBF, 0x5a, 0x70, 0x26, 0x05, 0x04, 0x30, 0x00, 0x00, 0x00, 0x00
                                 };
            byte[] endTag = { 0x89, 0x01, 0x02, 0x00, 0x01, 0x23, 0x32, 0x20 };

            byte[] packet = frontPacket.Concat(toVariant(cmd)).Concat(toVariant(lengthBeforeZip)).Concat(toVariant(lengthAfterZip)).Concat(endTag).ToArray();
            int HeadLen = packet.Length;

            //packet[1] = (byte)((HeadLen * 4) + 1);

            packet = packet.Concat(rsaDataPacket).ToArray();

            return packet;
        }
        public byte[] toVariant(int toValue)
        {
            uint va = (uint)toValue;
            List<byte> result = new List<byte>();

            while (va >= 0x80)
            {
                result.Add((byte)(0x80 + va % 0x80));
                va /= 0x80;
            }
            result.Add((byte)(va % 0x80));

            return result.ToArray();
        }
    }
}
 