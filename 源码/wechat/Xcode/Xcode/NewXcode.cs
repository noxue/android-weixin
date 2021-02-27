using aliyun;
using CRYPT;
using DotNet4.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MMPro.MM;

namespace Xcode
{
    

    public class Device_
    {
        //设备Id
        public string devicelId = "49aa7db2f4a3ffe0e96218f6b92cde11";
        // imei
        public string UUid = System.Guid.NewGuid().ToString();
        //设备信息结构
        public string DevicelType = String.Format(@"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>{0}</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>b8:73:cf:87:a9:53</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>","xx");
        //登录设备名
        public string DeviceName = "iPad";

        public string IMEI = ((new Random()).NextBytes(16).ToString(16, 2)).ToLower();

        /// <summary>
        /// 加密秘钥 可固定也可以随机
        /// </summary>
        public string AesKey;

        //用户标识
        public int m_Uin;

        public string cookie = "";

        public string wxid;

        public byte[] autoAuthKey_buff = new byte[0];

        public byte[] pri_key_buf = new byte[328];
        public byte[] pub_key_buf = new byte[57];

        public byte[] SyncKey = new byte[0];

        public string shortUrl = "http://hkshort.weixin.qq.com";

    }


    class NewXcode
    {
        class GetLoginQrcode_info
        {

            /// <summary>
            /// 用于解密CheckLogin 包
            /// </summary>
            public static byte[] GetLoignQrcode_AESKey;
            /// <summary>
            /// CheckLogin 接口提交
            /// </summary>
            public static string UUid;

        }
        private class PACKINFO
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
        class iPad_info
        {
            //版本号
            public static Int32 ver =369558056; 
            //RSA秘钥版本
            public static UInt32 LOGIN_RSA_VER = 174;
            //oSType
            public static string oSType = "iPad iPhone OS9.3.3";
        }
        
        private Device_ _Device;

        //设备信息
        public Device_ DeviceS
        {
            get
            {
                return _Device;
            }
        }

        /// <summary>
        /// xcode 构造函数
        /// </summary>
        /// <param name="DevicelId">设备ID 默认49aa7db2f4a3ffe0e96218f6b92cde11</param>
        /// <param name="DevicelType">设备信息结构</param>
        /// <param name="UUid">UUid</param>
        /// <param name="DeviceName">设备名字 例如 xxx的iPad</param>
        public NewXcode(string DevicelId, string DevicelType, string UUid, string DeviceName)
        {

            this._Device = new Device_();

            this._Device.devicelId = DevicelId;

            this._Device.UUid = UUid;

            this._Device.DevicelType = DevicelType;

            this._Device.DeviceName = DeviceName;

            //this.
        }

        public NewXcode(Device_ dev)
        {
            this._Device = dev;

        }

        /// <summary>
        /// 随机生成设备参数
        /// </summary>
        public NewXcode()
        {
            this._Device = new Device_();
            this._Device.UUid = System.Guid.NewGuid().ToString();

            this._Device.devicelId = "49" + ((new Random()).NextBytes(15).ToString(16, 2)).ToLower();

            this._Device.AesKey = GetAESkey();

            this._Device.DevicelType = String.Format(@"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>{1}</k19><k20>{0}</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>c4:aa:96:69:d8:c2</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>", this._Device.UUid, System.Guid.NewGuid().ToString());

            Util.GenerateECKey(713, this._Device.pub_key_buf, this._Device.pri_key_buf);
        }

        /// <summary>
        /// 得到AES 秘钥 如果是空的就生成一个16位的随机秘钥
        /// </summary>
        /// <returns></returns>
        public static string GetAESkey()
        {
            string AESKey = "";

            AESKey = (new Random()).NextBytes(16).ToString(16, 2);
            return AESKey;
        }


        /// <summary>
        /// 获取登录二维码
        /// </summary>
        /// <returns></returns>
        public micromsg.GetLoginQRCodeResponse GetLoginQRcode()
        {
            var getLoginQRCodeResponse = new micromsg.GetLoginQRCodeResponse();
            micromsg.GetLoginQRCodeRequest getLoginQRCodeRequest = new micromsg.GetLoginQRCodeRequest()
            {
                RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = this._Device.AesKey.ToByteArray(16, 2),
                    iLen = 16
                },
                OPCode = 0,
                BaseRequest = new micromsg.BaseRequest()
                {
                    ClientVersion = iPad_info.ver,
                    DeviceID = System.Text.Encoding.Default.GetBytes(this._Device.devicelId),
                    Scene = 0,
                    Uin = (uint)this._Device.m_Uin,
                    DeviceType = System.Text.Encoding.Default.GetBytes(iPad_info.oSType),
                    SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                },
                DeviceName = this._Device.DeviceName,


            };


            //序列化 protobuf
            var src = Util.Serialize(getLoginQRCodeRequest);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETLOGINQRCODE, bufferlen, 7);
            //发包
            byte[] RetDate = HttpPost(SendDate, URL.CGI_GETLOGINQRCODE);
            // 解包头
            if (RetDate == null) { return new micromsg.GetLoginQRCodeResponse(); }
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                var RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                getLoginQRCodeResponse = Util.Deserialize<micromsg.GetLoginQRCodeResponse>(RespProtobuf);
            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }

            /*
             保存 解密秘钥 及 UUid
             */
            if (getLoginQRCodeResponse.BaseResponse.Ret == (int)RetConst.MM_OK)
            {
                GetLoginQrcode_info.GetLoignQrcode_AESKey = getLoginQRCodeResponse.NotifyKey.Buffer;
                GetLoginQrcode_info.UUid = getLoginQRCodeResponse.UUID;
            }
            return getLoginQRCodeResponse;
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <returns></returns>
        public micromsg.LoginQRCodeNotify CheckLoginQRCode()
        {
            micromsg.CheckLoginQRCodeRequest checkLoginQRCodeRequest = new micromsg.CheckLoginQRCodeRequest()
            {
                BaseRequest = new micromsg.BaseRequest()
                {
                    ClientVersion = iPad_info.ver,
                    DeviceID = System.Text.Encoding.Default.GetBytes(this._Device.devicelId),
                    Scene = 0,
                    Uin = (uint)this._Device.m_Uin,
                    DeviceType = System.Text.Encoding.Default.GetBytes(iPad_info.oSType),
                    SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                },
                RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = this._Device.AesKey.ToByteArray(16, 2),
                    iLen = 16
                },
                OPCode = 0,
                TimeStamp = (uint)CurrentTime_(),
                UUID = GetLoginQrcode_info.UUid,

            };

            byte[] RespProtobuf = new byte[0];
            //序列化 protobuf
            var src = Util.Serialize(checkLoginQRCodeRequest);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_CHECKLOGINQRCODE, bufferlen, 7);
            //发包
            byte[] RetDate = HttpPost(SendDate, URL.CGI_CHECKLOGINQRCODE);
            // 解包头
            if (RetDate == null) { return new micromsg.LoginQRCodeNotify(); }
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);

                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }


                var checkLoginQRCode = Util.Deserialize<micromsg.CheckLoginQRCodeResponse>(RespProtobuf);
                if (checkLoginQRCode.BaseResponse.Ret == 0)
                {
                    var NotifyData = Util.nouncompress_aes(checkLoginQRCode.NotifyPkg.NotifyData.Buffer, GetLoginQrcode_info.GetLoignQrcode_AESKey);
                    var r = Util.Deserialize<micromsg.LoginQRCodeNotify>(NotifyData);

                    return r;
                }
            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }


            return new micromsg.LoginQRCodeNotify();
        }

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="UserName">wxid</param>
        /// <param name="pass">返回密码串</param>
        /// <returns></returns>
        public ManualAuthResponse ManualAuth(string UserName, string pass)
        {

            byte[] Clientcheckdat = "0a08303030303030303510011a800ac7609e47230c19d485e87031cf2b1a4c0273197c591057c750e97b4caa9e7aa6783a56298e79bf878c43bb1d8c01c49bc779638170287518ac71071a637175b754ff4d9866f72b54a03cd463f20a691e39c37d840a55d2cdd382c88765627d6accdb465d94166677ddf73e6b4f3577171bf4ff5fa771b18ef7dd653ee7048bba55737ab5255ceb09b8378b1a35d894f8046f3b44023974f787cbd73ea28bab1345a3c2923611178433c59cec88746a42e124265a65649a9e06ffd611dd3916b966ad5fb4cbe6f7429f371063b9c0cf198248c01b793592ca4d9372422ab28ac67201f3fe31f09bb7cc6336ced25f7a48c10c85e396d2cd1b77266856fdd2668bf624cd86b269c66ae11e54dfc7148ea2cac7f5636f0920e1f8f6dc142554eb697e5001d8a86ae80f6174a00e1f8e70607be71947814d0a7de03da34ac4900229ed8efd8dfe3a997aaefc506dd4e44d4b2fe69fc3820ffc3291847dee2bc4534e6f23ce3febf1ec79436908781dc79aa39a5fbff63d5f1a3b7d08e9c263e25fef8f53c3ccc54947063fb7139333fa0b7189fa4bc4cefbbfffec4159556d4ff16c0c09c740620ea017eabafcf9da87280fc86544557b5f01193699e4ae893e142973f7a1933a194afa3ddcc59ee3bf77e51aeddc738e08083489401d24af7622f4b6fc59e1503053e1ebab1778a1347c105158dc632bfb7ae58b8648e42d2849e12caa4786943f89cfe5aebc5b08336df9f8eb48947357302d179db65b7c366bfa22513a560c2b0b004bdfcc6a1b55dee7ac497bca5975301026d3796e21ead0860e21f8d190986cf24eba3e0bb79e14198e569e8c3c8e377ec15074f928fe8c523147ce5691b8f17520c95256da74ac2ed91166b22be3e2fb3c362174949c7ab850b2a0ed2722311849f614380d21c8ea2a1ce133774f92c8f4659ca730270f552d9c8566cec669779661ca69553da564eeae88de1052afdac7f5bace925f761880fb7cac0c842f5e2d8a8624daf2524130bbcdb00357468b4a203cd775a32e4b9e20cf33ea10cc689507a7828ef354507a5aa2cfa5b5415138c29e07e9075282293e2b608662e3ee736f2037aa19a436c4aca559ac1f752d0ba49206308043815ea04110da1cb6a6ad9d8b278a6912ad49848f828a2e23cefed765b39a5cad32ac0339939e08b1ed6de4856fa877e6a58b4d5a609c811eef28d3713a0e1dc4b09edc935971408e470ab3e89c4e1bc4d97d5154daedba6456343c6d53a41cae4e1e414a902cfd17c846994953ae18ec86c02999e0cb05f8aa430f67e04e8abd55b691de6dc0776a9ee4a600e4024a1d7266b9931e9bc8acd81731912067e13c81e295cbf77f95eddf6470272163e17379095251da7f4f4c6774870a50b7723002a17d2dd54c93831a3f9ccc281658dbdb81d5a870616b10e1f1f838574f71c571223b7956b92017f0512306d3f872c92e5b1d9a4684d05586c1553b23532b0f331c2c794e1cf84d89615150e625bfe84bd3bbad76bc0f640de228d72bed441539562b86e64b065e007f08d084824455c93ff35b205e826e5377e5385748a2b0c5de1e957e92311e89213fc24d07b43b0fdaa6c81b4d44dd63545d67bc699c41a7ae20ab21a4348b56fc2941bcd193e565f2fc33785f296d9cffff634eaf8acf03f3968da91bde0be874eaedc6a5d327ab6406c1431a97c402ad4c1977927a75a887e536f61e3e048068dcdb32d16bcedabad4476e90be340ff58076fb8041f8121b9dede668b255cbbd43ab6c44109d459e92434b057c240f052b877b41ef78cf".ToByteArray(16, 2);
            micromsg.ManualAuthRequest manualAuthRequest = new micromsg.ManualAuthRequest()
            {
                RsaReqData = new micromsg.ManualAuthRsaReqData()
                {
                    CliPubECDHKey = new micromsg.ECDHKey()
                    {
                        Key = new micromsg.SKBuiltinBuffer_t()
                        {
                            Buffer = this._Device.pub_key_buf,
                            iLen = 57,
                        },
                        Nid = 713
                    },
                    RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = this._Device.AesKey.ToByteArray(16, 2),
                        iLen = 16
                    },
                    Pwd = pass,
                    Pwd2 = null,
                    UserName = UserName
                },
                AesReqData = new micromsg.ManualAuthAesReqData()
                {
                    BaseRequest = new micromsg.BaseRequest()
                    {
                        ClientVersion = iPad_info.ver,
                        DeviceID = System.Text.Encoding.Default.GetBytes(this._Device.devicelId),
                        Scene = 1,
                        Uin = (uint)this._Device.m_Uin,
                        DeviceType = System.Text.Encoding.Default.GetBytes(iPad_info.oSType),
                        SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                    },
                    BaseReqInfo = new micromsg.BaseAuthReqInfo()
                    {
                        AuthReqFlag = 0,
                        AuthTicket = "",

                    },
                    BuiltinIPSeq = 0,
                    BundleID = "daivis.IPAD",
                    Channel = 0,
                    InputType = 2,
                    IPhoneVer = "iPad2,5",
                    DeviceBrand = "Apple",
                    Language = "zh_CN",
                    TimeZone = "8.00",
                    DeviceName = this._Device.DeviceName,
                    RealCountry = "CN",
                    SoftType = this._Device.DevicelType,
                    DeviceType = "iPad",
                    IMEI = this._Device.IMEI,
                    TimeStamp = (uint)CurrentTime_(),
                    ClientSeqID = this._Device.IMEI + "-" + CurrentTime_().ToString(),
                    //AdSource = this._Device.UUid,
                    Clientcheckdat = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = Clientcheckdat,
                        iLen = (uint)Clientcheckdat.Length
                    }

                }


            };
            byte[] account = Util.Serialize(manualAuthRequest.RsaReqData);
            byte[] device = Util.Serialize(manualAuthRequest.AesReqData);

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

                body = body.Concat(Util.compress_aes(device, this._Device.AesKey.ToByteArray(16, 2))).ToArray();
                //var head = MakeHead( body, MM.CGI_TYPE.CGI_TYPE_MANUALAUTH, 7);
                var head = MakeHead((int)CGI_TYPE.CGI_TYPE_MANUALAUTH, body.Length, 7, false);

                body = head.Concat(body).ToArray();
                byte[] RespProtobuf = new byte[0];

                byte[] RetDate = HttpPost(body, URL.CGI_MANUALAUTH);
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
                        RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                if (RespProtobuf == null) return new ManualAuthResponse();
                var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
                if (ManualAuthResponse.baseResponse.ret == RetConst.MM_OK)
                {
                    byte[] strECServrPubKey = ManualAuthResponse.authParam.ecdh.ecdhkey.key;
                    byte[] aesKey = new byte[16];
                    Util.ComputerECCKeyMD5(strECServrPubKey, 57, this._Device.pri_key_buf, 328, aesKey);
                    //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                    //wechat.CheckEcdh = aesKey.ToString(16, 2);
                    this._Device.AesKey = AES.AESDecrypt(ManualAuthResponse.authParam.session.key, aesKey).ToString(16, 2);
                    this._Device.autoAuthKey_buff = ManualAuthResponse.authParam.autoAuthKey.buffer;
                    this._Device.wxid = ManualAuthResponse.accountInfo.wxid;
                }
                else if (ManualAuthResponse.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT) {
                    this._Device.shortUrl = "http://" + ManualAuthResponse.dnsInfo.newHostList.list[1].substitute;
                }
                return ManualAuthResponse;
            }
            else
                return null;


        }

        /// <summary>
        /// 62数据登录
        /// </summary>
        /// <param name="UserName">wxid</param>
        /// <param name="pass">返回密码串</param>
        /// <returns></returns>
        public ManualAuthResponse UserLogin(string UserName, string pass,string data62="")
        {
            if (data62 != string.Empty)
            {
                //string II = Util.Get62Key(data62);
                this._Device.IMEI = Util.Get62Key(data62);
                this._Device.devicelId = "49" + this._Device.IMEI.Substring(2, this._Device.IMEI.Length - 2);

            }

            byte[] Clientcheckdat = "0a08303030303030303510011a800a1ea07107338a52c371528cb24b59c3ec17821232dbf83706ba0236e20908decad21f56551e0b1557281d01cb6c68f485bb322be22b5c6fc8c1f4b4abb502f7e1fa62752768ecda7c156f9d8980c826678b7250717758f4a8777927e826d56f9b6a232c1bf7f6092be7fe2019d495c4e54cd5589a198647e149a2cd54d5c03af4c154ad92b72b39a3018d0d85b30031eb041b00fd08b2bd77ad024457201c49927fb980a443fee750c4257db81c8f8709ba4c0d888870ee9620de132a7a457065f5ef440551eb1ffbc52e8e8df20184c320051322adc01fd310500f9e4137934c72e6c940fb2a279b5702a58d257a3ebc6b5632c6e8780438b0b8ab08b8e3a3c5f2ff513f0402776e6c69330eca3dc7636346605a464c3f08731c98eef27cbe3e8db121be4110aaec6dcc19cfc3aa2a925f3cd77b36f213adcd822ec6c90e67b794940a2e996da274fa187ef57f17abfb78481746aa5af6ce2d384fcd09401f6be96ab02efd83f64146af4455ba999b9c5f8d9123f4fe0bf96abd8bfc3af2043f5351389bdf72a4227b753971615a8ec8821201677a872691c09fb766a07463b23f87cdd66d9c40ba060443fb855e45966afcaf84c7d1c62dcb4f6004c06a93a855a17eb784cb9d038f0ea2d5bc5f5f8ca26c34585bea1853f22420761555a558ba1ba0b4e5cf8f234857e39f90c95fd7ad709d5845e0cce308150b5bb9585b6b6890fb74ea1927fcc038825f01bc4f101ea3699e5e56604aee88dce1c019aa9bd20806793f6e8a6595c487b74a3d3163674387e9ede3328357a0508b791180db376e3d7242afc7e562cdfab059e98b7448f5a016c6382ec81192e9a71636e5148133f373480d664c0c4666e4fb85a85e231873ffecce4f1700c964c341f8e9fe84dcd1fceb41de44e88697068ca3b6bcb1b1e41f8b5500535c392c4d8c27130d1ea0c1ba51f1da97a8dc2a9751944c57d6abdc6489b523ff887fe27d6eeefb4ac43a788c5766c44f7a476a6bee8e61acf3fb38d19b45b5478e9390d6e355578214ff51859fb3d0deb6da209a6a976e0a3350159ea6c272d005406672321fb65bdb6671869648fab4c278a7b968d2ae0ae4377dca272e5c86d9649e3903d77886de20f426121a19ded0f69e0379a59718d8d5f32e0776191927252abb85f9541954f74a1356b9ed795b5a5223ee100e4a80e61e6c554666225e57cb0dd2af155db8be1fd25faf0b791d02b0f7b40463ea57c991b5157224ddc2a41bc3404d658c7afe92aa2855fd80addc7b9f484128aeac471aa6a3b8856ec5d6b0a321833430b7c861cbb2b635383608748c858c5b44432606bb48fa62c69c9f00a7ed5a329118c3623ce0cccee6fb682164ffde856964902f5b26753bcec7b1cc9c93846048d8bbd7e4829a2690a8b2a157afa16777b61d0d8519532e97768b332baba883c6c6d0e7d748013923fe168566ee5923d4c18e7413de409fc5815d0943ba50513288336557907bc80dc50fb1222fd2984d684a86d1f6fb618ab3b33f9d217953eb07faf2bed03f68f940326a4e7a595eb836c25258ce352d335c42d8df6dc48045f6c659373415c59669993684c450eeeb4d5224682092b1dbe30ba751339476e925409c2c5395c6f0225271b97426b9d21606503c0e27f0d2eb48cd91d1a72ec335919174b3ab588aefab2d9a13cb543b9c66f840044df02ac16b1991550257c2facfe3aad126dff69ac4655d8f2eb9450bf0a78132ce5ee1eecbd4c223692876bb26acf34cf9924fc9cb1d88e7f26c6f479a48c29489260c8e0859a6ec156c6b".ToByteArray(16, 2);
            //byte[] Clientcheckdat = "".ToByteArray(16, 2);

            micromsg.ManualAuthRequest manualAuthRequest = new micromsg.ManualAuthRequest()
            {
                RsaReqData = new micromsg.ManualAuthRsaReqData()
                {
                    CliPubECDHKey = new micromsg.ECDHKey()
                    {
                        Key = new micromsg.SKBuiltinBuffer_t()
                        {
                            Buffer = this._Device.pub_key_buf,
                            iLen = 57,
                        },
                        Nid = 713
                    },
                    RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = this._Device.AesKey.ToByteArray(16, 2),
                        iLen = 16
                    },
                    Pwd = Util.EncryptWithMD5(pass),
                    Pwd2 = Util.EncryptWithMD5(pass),
                    UserName = UserName
                },
                AesReqData = new micromsg.ManualAuthAesReqData()
                {
                    BaseRequest = new micromsg.BaseRequest()
                    {
                        ClientVersion = iPad_info.ver,
                        DeviceID = this._Device.devicelId.ToByteArray(16, 2),
                        Scene = 1,
                        Uin = (uint)this._Device.m_Uin,
                        DeviceType = System.Text.Encoding.Default.GetBytes("iPad iPhone OS9.3.3"),
                        SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                    },
                    BaseReqInfo = new micromsg.BaseAuthReqInfo()
                    {
                        AuthReqFlag = 0,
                        AuthTicket = "",

                    },
                    BuiltinIPSeq = 0,
                    BundleID = "daivis.IPAD",
                    Channel = 0,
                    InputType = 2,
                    IPhoneVer = "iPad2,5",
                    DeviceBrand = "Apple",
                    Language = "zh_CN",
                    TimeZone = "8.00",
                    DeviceName = this._Device.DeviceName,
                    RealCountry = "CN",
                    SoftType = this._Device.DevicelType,
                    DeviceType = "",
                    IMEI = this._Device.IMEI,
                    TimeStamp = (uint)CurrentTime_(),
                    ClientSeqID = this._Device.IMEI + "-" + CurrentTime_().ToString(),
                    AdSource = this._Device.UUid,
                    //Clientcheckdat = new micromsg.SKBuiltinBuffer_t()
                    //{
                    //    Buffer = Clientcheckdat,
                    //    iLen = (uint)Clientcheckdat.Length
                    //},
                    //OSType = iPad_info.oSType,
                    Signature = "",
                    
                    
                }


            };
            byte[] account = Util.Serialize(manualAuthRequest.RsaReqData);
            byte[] device = Util.Serialize(manualAuthRequest.AesReqData);

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

                body = body.Concat(Util.compress_aes(device, this._Device.AesKey.ToByteArray(16, 2))).ToArray();
                //var head = MakeHead( body, MM.CGI_TYPE.CGI_TYPE_MANUALAUTH, 7);
                var head = MakeHead((int)CGI_TYPE.CGI_TYPE_MANUALAUTH, body.Length, 7, false);

                body = head.Concat(body).ToArray();
                byte[] RespProtobuf = new byte[0];

                byte[] RetDate = HttpPost(body, URL.CGI_MANUALAUTH);
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
                        RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                if (RespProtobuf == null) return new ManualAuthResponse();
                var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
                if (ManualAuthResponse.baseResponse.ret == RetConst.MM_OK)
                {
                    byte[] strECServrPubKey = ManualAuthResponse.authParam.ecdh.ecdhkey.key;
                    byte[] aesKey = new byte[16];
                    Util.ComputerECCKeyMD5(strECServrPubKey, 57, this._Device.pri_key_buf, 328, aesKey);
                    //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                    //wechat.CheckEcdh = aesKey.ToString(16, 2);
                    this._Device.AesKey = AES.AESDecrypt(ManualAuthResponse.authParam.session.key, aesKey).ToString(16, 2);
                    this._Device.autoAuthKey_buff = ManualAuthResponse.authParam.autoAuthKey.buffer;
                }
                else if (ManualAuthResponse.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
                {
                    this._Device.shortUrl = "http://" + ManualAuthResponse.dnsInfo.newHostList.list[1].substitute;
                }
                return ManualAuthResponse;
            }
            else
                return null;


        }

        /// <summary>
        /// 62数据+账号密码登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pass">登录密码</param>
        /// <param name="Data62">62数据</param>
        /// <returns></returns>
        public ManualAuthResponse UserLogin_(string username, string pass, string Data62)
        {
            string imei;
            if (Data62 != "")
            {
                this._Device.IMEI = Util.Get62Key(Data62);
                //imei = devicelId;
                this._Device.devicelId = "49" + this._Device.IMEI.Substring(2, this._Device.IMEI.Length - 2);
            }
            

            var RespProtobuf = new byte[0];
            //GenerateECKey(713, pub_key_buf, pri_key_buf);
            //OpenSSLNativeClass.ECDH.GenEcdh(713, ref pub_key_buf, ref pri_key_buf);
            ManualAuthAccountRequest manualAuthAccountRequest = new ManualAuthAccountRequest()
            {
                aes = new AesKey()
                {
                    len = 16,
                    key = this._Device.AesKey.ToByteArray(16, 2)
                },
                ecdh = new Ecdh()
                {
                    ecdhkey = new EcdhKey()
                    {
                        key = this._Device.pub_key_buf,
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
            manualAuthDeviceRequest.Clientcheckdat = new SKBuiltinString_() { buffer = new byte[] { }, iLen = 0 };
            manualAuthDeviceRequest.imei = Encoding.UTF8.GetBytes(this._Device.IMEI);
            manualAuthDeviceRequest.clientSeqID = manualAuthDeviceRequest.imei + "-" + ((int)CurrentTime_()).ToString();
            manualAuthDeviceRequest.baseRequest = new BaseRequest()
            {
                clientVersion = (int)iPad_info.ver,

                devicelId = this._Device.devicelId.ToByteArray(16, 2),
                scene = 0,
                sessionKey = this._Device.AesKey.ToByteArray(16, 2),
                osType = "iPad iPhone OS8.4",
                uin = 0
            };

            // baseRequest = manualAuthDeviceRequest.baseRequest;
            //manualAuthDeviceRequest.Adsource = "52336395-2829-C5BB-CF9C-1B65A2E52EA6";
            //manualAuthDeviceRequest.Bundleid = "com.tencent.xin";

            //manualAuthDeviceRequest.Iphonever = "iPad4,4";

            //manualAuthDeviceRequest.softInfoXml = @"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>52336395-2829-C5BB-CF9C-1B65A2E52EA6</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>b8:73:cf:87:a9:53</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>";
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

                body = body.Concat(Util.compress_aes(device, this._Device.AesKey.ToByteArray(16, 2))).ToArray();
                //var head = MakeHead( body, MM.CGI_TYPE.CGI_TYPE_MANUALAUTH, 7);
                var head = MakeHead((int)CGI_TYPE.CGI_TYPE_MANUALAUTH, body.Length, 7, false);

                body = head.Concat(body).ToArray();

                byte[] RetDate = HttpPost(body, URL.CGI_MANUALAUTH);
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
                        RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                if (RespProtobuf == null) return new ManualAuthResponse();
                var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
                if (ManualAuthResponse.baseResponse.ret == RetConst.MM_OK)
                {
                    byte[] strECServrPubKey = ManualAuthResponse.authParam.ecdh.ecdhkey.key;
                    byte[] aesKey = new byte[16];
                    Util.ComputerECCKeyMD5(strECServrPubKey, 57, this._Device.pri_key_buf, 328, aesKey);
                    //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                    //wechat.CheckEcdh = aesKey.ToString(16, 2);
                    this._Device.AesKey = AES.AESDecrypt(ManualAuthResponse.authParam.session.key, aesKey).ToString(16, 2);
                    this._Device.autoAuthKey_buff = ManualAuthResponse.authParam.autoAuthKey.buffer;
                }
                else if (ManualAuthResponse.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
                {
                    this._Device.shortUrl = "http://" + ManualAuthResponse.dnsInfo.newHostList.list[1].substitute;
                }
                return ManualAuthResponse;
            }
            else
                return null;
            //return null;

        }


        /// <summary>
        /// 安卓登录调用该接口所有接口变为安卓接口
        /// </summary>
        /// <param name="UserName">wxid</param>
        /// <param name="pass">返回密码串</param>
        /// <returns></returns>
        public ManualAuthResponse Android_Login(string UserName, string pass,Device_ Android_device)
        {

            iPad_info.ver = 637929010;

            iPad_info.oSType = "android-25";

            this._Device = Android_device;
           // this._Device.devicelId = this._Device.devicelId.Substring(0,15);

            this._Device.AesKey = "57727A43654171444F486F786A37646D";
            Util.GenerateECKey(713, this._Device.pub_key_buf, this._Device.pri_key_buf);
            //byte[] Clientcheckdat = "0a08303030303030303510011a800ac7609e47230c19d485e87031cf2b1a4c0273197c591057c750e97b4caa9e7aa6783a56298e79bf878c43bb1d8c01c49bc779638170287518ac71071a637175b754ff4d9866f72b54a03cd463f20a691e39c37d840a55d2cdd382c88765627d6accdb465d94166677ddf73e6b4f3577171bf4ff5fa771b18ef7dd653ee7048bba55737ab5255ceb09b8378b1a35d894f8046f3b44023974f787cbd73ea28bab1345a3c2923611178433c59cec88746a42e124265a65649a9e06ffd611dd3916b966ad5fb4cbe6f7429f371063b9c0cf198248c01b793592ca4d9372422ab28ac67201f3fe31f09bb7cc6336ced25f7a48c10c85e396d2cd1b77266856fdd2668bf624cd86b269c66ae11e54dfc7148ea2cac7f5636f0920e1f8f6dc142554eb697e5001d8a86ae80f6174a00e1f8e70607be71947814d0a7de03da34ac4900229ed8efd8dfe3a997aaefc506dd4e44d4b2fe69fc3820ffc3291847dee2bc4534e6f23ce3febf1ec79436908781dc79aa39a5fbff63d5f1a3b7d08e9c263e25fef8f53c3ccc54947063fb7139333fa0b7189fa4bc4cefbbfffec4159556d4ff16c0c09c740620ea017eabafcf9da87280fc86544557b5f01193699e4ae893e142973f7a1933a194afa3ddcc59ee3bf77e51aeddc738e08083489401d24af7622f4b6fc59e1503053e1ebab1778a1347c105158dc632bfb7ae58b8648e42d2849e12caa4786943f89cfe5aebc5b08336df9f8eb48947357302d179db65b7c366bfa22513a560c2b0b004bdfcc6a1b55dee7ac497bca5975301026d3796e21ead0860e21f8d190986cf24eba3e0bb79e14198e569e8c3c8e377ec15074f928fe8c523147ce5691b8f17520c95256da74ac2ed91166b22be3e2fb3c362174949c7ab850b2a0ed2722311849f614380d21c8ea2a1ce133774f92c8f4659ca730270f552d9c8566cec669779661ca69553da564eeae88de1052afdac7f5bace925f761880fb7cac0c842f5e2d8a8624daf2524130bbcdb00357468b4a203cd775a32e4b9e20cf33ea10cc689507a7828ef354507a5aa2cfa5b5415138c29e07e9075282293e2b608662e3ee736f2037aa19a436c4aca559ac1f752d0ba49206308043815ea04110da1cb6a6ad9d8b278a6912ad49848f828a2e23cefed765b39a5cad32ac0339939e08b1ed6de4856fa877e6a58b4d5a609c811eef28d3713a0e1dc4b09edc935971408e470ab3e89c4e1bc4d97d5154daedba6456343c6d53a41cae4e1e414a902cfd17c846994953ae18ec86c02999e0cb05f8aa430f67e04e8abd55b691de6dc0776a9ee4a600e4024a1d7266b9931e9bc8acd81731912067e13c81e295cbf77f95eddf6470272163e17379095251da7f4f4c6774870a50b7723002a17d2dd54c93831a3f9ccc281658dbdb81d5a870616b10e1f1f838574f71c571223b7956b92017f0512306d3f872c92e5b1d9a4684d05586c1553b23532b0f331c2c794e1cf84d89615150e625bfe84bd3bbad76bc0f640de228d72bed441539562b86e64b065e007f08d084824455c93ff35b205e826e5377e5385748a2b0c5de1e957e92311e89213fc24d07b43b0fdaa6c81b4d44dd63545d67bc699c41a7ae20ab21a4348b56fc2941bcd193e565f2fc33785f296d9cffff634eaf8acf03f3968da91bde0be874eaedc6a5d327ab6406c1431a97c402ad4c1977927a75a887e536f61e3e048068dcdb32d16bcedabad4476e90be340ff58076fb8041f8121b9dede668b255cbbd43ab6c44109d459e92434b057c240f052b877b41ef78cf".ToByteArray(16, 2);
            byte[] uuid = Encoding.Default.GetBytes(_Device.devicelId);
            uuid[15] = 0;

            micromsg.ManualAuthRequest manualAuthRequest = new micromsg.ManualAuthRequest()
            {
                RsaReqData = new micromsg.ManualAuthRsaReqData()
                {
                    CliPubECDHKey = new micromsg.ECDHKey()
                    {
                        Key = new micromsg.SKBuiltinBuffer_t()
                        {
                            Buffer = this._Device.pub_key_buf,
                            iLen = 57,
                        },
                        Nid = 713
                    },
                    RandomEncryKey = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = this._Device.AesKey.ToByteArray(16, 2),
                        iLen = 16
                    },
                    Pwd = Util.MD5Encrypt(pass),
                    Pwd2 = Util.MD5Encrypt(pass),
                    UserName = UserName
                },
                AesReqData = new micromsg.ManualAuthAesReqData()
                {
                    BaseRequest = new micromsg.BaseRequest()
                    {

                        ClientVersion = 637927472,
                        DeviceID = uuid,
                        Scene = 1,
                        Uin = (uint)this._Device.m_Uin,
                        DeviceType = System.Text.Encoding.Default.GetBytes("android-25"),
                        SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                    },
                    BaseReqInfo = new micromsg.BaseAuthReqInfo() {
                        AuthReqFlag = 0,
                    },
                    //TimeStamp = (uint)CurrentTime_(),
                    BuiltinIPSeq = 0,
                    BundleID = "",
                    Channel = 0,
                    InputType = 2,
                    IPhoneVer = "",
                    DeviceBrand = this._Device.DeviceName,
                    Language = "zh_CN",
                    TimeZone = "8.00",
                    DeviceName = this._Device.DeviceName,
                    RealCountry = "cn",
                    SoftType = this._Device.DevicelType,
                   DeviceType= "<deviceinfo><MANUFACTURER name=\"iPhon\"><MODEL name=\"100\"><VERSION_RELEASE name=\"5.1.1\"><VERSION_INCREMENTAL name=\"eng.denglibo.20171224.164708\"><DISPLAY name=\"android_x86-userdebug 5.1.1 LMY48Z eng.denglibo.20171224.164708 test-keys\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>",
                    IMEI = this._Device.IMEI,
                    TimeStamp = (uint)CurrentTime_(),
                    ClientSeqID = this._Device.devicelId + "_" + CurrentTime_().ToString(),
                    OSType = "android-25",
                    DeviceModel = "100armeabi-v7a",
                    //Signature = "e89b158e4bcf922ebd09eb83f5378e11"
                    //AdSource = this._Device.UUid,
                    //Clientcheckdat = new micromsg.SKBuiltinBuffer_t()
                    //{
                    //    Buffer = Clientcheckdat,
                    //    iLen = (uint)Clientcheckdat.Length
                    //}

                    
                }


            };

            //manualAuthRequest.AesReqData = Util.Deserialize<micromsg.ManualAuthAesReqData>("0A3A0A1057727A43654171444F486F786A37646D10001A104163383436383662333234343363630020B08098B0022A0A616E64726F69642D3232300112001A0F38363531363630323436373132313922E9073C736F6674747970653E3C6C63746D6F633E303C2F6C63746D6F633E3C6C6576656C3E313C2F6C6576656C3E3C6B313E41524D76372070726F636573736F72207265762031202876376C29203C2F6B313E3C6B323E3C2F6B323E3C6B333E352E312E313C2F6B333E3C6B343E3836353136363032343637313231393C2F6B343E3C6B353E3436303030373333373736363534313C2F6B353E3C6B363E38393836303031323232313734363532373338313C2F6B363E3C6B373E643331353132333363666262346664343C2F6B373E3C6B383E756E6B6E6F776E3C2F6B383E3C6B393E6950686F6E203130303C2F6B393E3C6B31303E323C2F6B31303E3C6B31313E706C616365686F6C6465723C2F6B31313E3C6B31323E303030313C2F6B31323E3C6B31333E303030303030303030303030303030313C2F6B31333E3C6B31343E30313A36313A31393A35383A37383A64323C2F6B31343E3C6B31353E3C2F6B31353E3C6B31363E6E656F6E20766670207377702068616C66207468756D6220666173746D756C7420656473702076667076332069646976612069646976743C2F6B31363E3C6B31383E65383962313538653462636639323265626430396562383366353337386531313C2F6B31383E3C6B32313E22776972656C657373223C2F6B32313E3C6B32323E3C2F6B32323E3C6B32343E34313A32373A39313A31323A35653A31343C2F6B32343E3C6B32363E303C2F6B32363E3C6B33303E22776972656C657373223C2F6B33303E3C6B33333E636F6D2E74656E63656E742E6D6D3C2F6B33333E3C6B33343E416E64726F69642D7838362F616E64726F69645F7838362F7838363A352E312E312F4C4D5934385A2F64656E676C69626F30383032313634373A7573657264656275672F746573742D6B6579733C2F6B33343E3C6B33353E7669766F2076333C2F6B33353E3C6B33363E756E6B6E6F776E3C2F6B33363E3C6B33373E6950686F6E3C2F6B33373E3C6B33383E7838363C2F6B33383E3C6B33393E616E64726F69645F7838363C2F6B33393E3C6B34303E7461757275733C2F6B34303E3C6B34313E313C2F6B34313E3C6B34323E3130303C2F6B34323E3C6B34333E6E756C6C3C2F6B34333E3C6B34343E303C2F6B34343E3C6B34353E3C2F6B34353E3C6B34363E3C2F6B34363E3C6B34373E776966693C2F6B34373E3C6B34383E3836353136363032343637313231393C2F6B34383E3C6B34393E2F646174612F646174612F636F6D2E74656E63656E742E6D6D2F3C2F6B34393E3C6B35323E303C2F6B35323E3C6B35333E303C2F6B35333E3C6B35373E313038303C2F6B35373E3C6B35383E3C2F6B35383E3C6B35393E303C2F6B35393E3C2F736F6674747970653E2800321E416338343638366233323434336363395F313532323832373131303736323A20653839623135386534626366393232656264303965623833663533373865313142096950686F6E203130304AC0023C646576696365696E666F3E3C4D414E554641435455524552206E616D653D226950686F6E223E3C4D4F44454C206E616D653D22313030223E3C56455253494F4E5F52454C45415345206E616D653D22352E312E31223E3C56455253494F4E5F494E4352454D454E54414C206E616D653D22656E672E64656E676C69626F2E32303137313232342E313634373038223E3C444953504C4159206E616D653D22616E64726F69645F7838362D75736572646562756720352E312E31204C4D5934385A20656E672E64656E676C69626F2E32303137313232342E31363437303820746573742D6B657973223E3C2F444953504C41593E3C2F56455253494F4E5F494E4352454D454E54414C3E3C2F56455253494F4E5F52454C454153453E3C2F4D4F44454C3E3C2F4D414E5546414354555245523E3C2F646576696365696E666F3E52057A685F434E5A04382E3030680070007A056950686F6E82010E31303061726D656162692D7637618A010A616E64726F69642D3232920102636EB00102".ToByteArray(16, 2));

            //manualAuthRequest.AesReqData.BaseRequest = new micromsg.BaseRequest()
            //{

            //    ClientVersion = 637927472,
            //    DeviceID = uuid,
            //    Scene = 1,
            //    Uin = (uint)this._Device.m_Uin,
            //    DeviceType = System.Text.Encoding.Default.GetBytes("android-25"),
            //    SessionKey = this._Device.AesKey.ToByteArray(16, 2),
            //};
            byte[] account = Util.Serialize(manualAuthRequest.RsaReqData);
            Debug.Print(account.ToString(16, 2));
            byte[] device = Util.Serialize(manualAuthRequest.AesReqData);


            //byte[] device = "".ToByteArray(16, 2);
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
                byte[] deviceZip  = ZipUtils.compressBytes(device);
                byte[] deviceZiph = Util.nocompress_aes(deviceZip, this._Device.AesKey.ToByteArray(16, 2));
                body = body.Concat(deviceZiph).ToArray();
                //var head = MakeHead( body, MM.CGI_TYPE.CGI_TYPE_MANUALAUTH, 7);
                Debug.Print(body.ToString(16, 2));
                var head = MakeHead((int)CGI_TYPE.CGI_TYPE_MANUALAUTH, body.Length, 7, true);
                //var head = "8A7F2606003000000000000000000000000000000000000000BD058C098C09AE0102".ToByteArray(16,2);
                 body = head.Concat(body).ToArray();
                byte[] RespProtobuf = new byte[0];
                Debug.Print(head.ToString(16, 2));
                byte[] RetDate = HttpPost(body, URL.CGI_MANUALAUTH);
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
                        RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }
                    else
                    {
                        RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                    }

                }
                else
                {
                    Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
                }
                if (RespProtobuf == null) return new ManualAuthResponse();
                var ManualAuthResponse = Util.Deserialize<ManualAuthResponse>(RespProtobuf);
                if (ManualAuthResponse.baseResponse.ret == RetConst.MM_OK)
                {
                    byte[] strECServrPubKey = ManualAuthResponse.authParam.ecdh.ecdhkey.key;
                    byte[] aesKey = new byte[16];
                    Util.ComputerECCKeyMD5(strECServrPubKey, 57, this._Device.pri_key_buf, 328, aesKey);
                    //var aesKey = OpenSSLNativeClass.ECDH.DoEcdh(ManualAuth.authParam.ecdh.nid, strECServrPubKey, wechat.pri_key_buf);
                    //wechat.CheckEcdh = aesKey.ToString(16, 2);
                    this._Device.AesKey = AES.AESDecrypt(ManualAuthResponse.authParam.session.key, aesKey).ToString(16, 2);
                    this._Device.autoAuthKey_buff = ManualAuthResponse.authParam.autoAuthKey.buffer;
                }
                else if (ManualAuthResponse.baseResponse.ret == MMPro.MM.RetConst.MM_ERR_IDC_REDIRECT)
                {
                    this._Device.shortUrl = "http://" + ManualAuthResponse.dnsInfo.newHostList.list[1].substitute;
                }
                return ManualAuthResponse;
            }
            else
                return null;


        }

        /// <summary>
        /// 授权阅读连接
        /// </summary>
        /// <param name="userName">公众号用户名</param>
        /// <param name="url">访问的连接</param>
        /// <returns></returns>
        public micromsg.GetA8KeyResp GetA8Key(string userName, string url, int opcode = 2)
        {

            byte[] RespProtobuf = new byte[0];

            GetA8KeyRequest getA8Key_ = new GetA8KeyRequest()
            {
                baseRequest = new BaseRequest()
                {
                    clientVersion = iPad_info.ver,
                    devicelId = System.Text.Encoding.Default.GetBytes(this._Device.devicelId),
                    scene = 0,
                    osType = iPad_info.oSType,
                    sessionKey = this._Device.AesKey.ToByteArray(16, 2),
                    uin = this._Device.m_Uin,

                },
                codeType = 0,
                codeVersion = 0,
                flag = 0,
                fontScale = (uint)100,
                netType = "WIFI",
                opCode = (uint)opcode,
                userName = userName,
                reqUrl = new SKBuiltinString()
                {
                    @string = url
                },
                friendQQ = 0,

            };

            var src = Util.Serialize(getA8Key_);
            int bufferlen = src.Length;
            //组包
            Debug.Print(src.ToString(16, 2));
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_GETA8KEY, bufferlen, 5, true, true);
            Debug.Print(SendDate.ToString(16, 2));
            //发包
            byte[] RetDate = HttpPost(SendDate, "/cgi-bin/micromsg-bin/mp-geta8key");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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
        /// 生成62数据
        /// </summary>
        /// <returns></returns>
        public string GenerateWxDat() {
            return Util.SixTwoData(this._Device.devicelId);
        }

        /// <summary>
        /// 发送盆友圈
        /// </summary>
        /// <param name="content">欲发送内容 使用朋友圈结构发送</param>
        /// <returns></returns>
        public SnsPostResponse SnsPost(string content, int GroupId)
        {

            var RespProtobuf = new byte[0];
            SnsPostRequest SnsPostReq = Util.Deserialize<SnsPostRequest>("0A570A105D64797E40587E3653492B3770767C6D10E5DA8D81031A20353332363435314632303045304431333043453441453237323632423631363920A28498B0012A1369506164206950686F6E65204F53392E332E33300012810808FB0712FB073C54696D656C696E654F626A6563743E3C69643E31323534323132393139333538343234323934373C2F69643E3C757365726E616D653E777869645F6B727862626D68316A75646533313C2F757365726E616D653E3C63726561746554696D653E313439353133383331303C2F63726561746554696D653E3C636F6E74656E74446573633EE2809CE7BEA1E68595E982A3E4BA9BE4B880E6B2BEE79D80E69E95E5A4B4E5B0B1E883BDE5AE89E79DA1E79A84E4BABAE5928CE982A3E4BA9BE586B3E5BF83E694BEE6898BE4B98BE5908EE5B0B1E4B88DE5868DE59B9EE5A4B4E79A84E4BABAE2809D3C2F636F6E74656E74446573633E3C636F6E74656E744465736353686F77547970653E303C2F636F6E74656E744465736353686F77547970653E3C636F6E74656E74446573635363656E653E333C2F636F6E74656E74446573635363656E653E3C707269766174653E303C2F707269766174653E3C7369676874466F6C6465643E303C2F7369676874466F6C6465643E3C617070496E666F3E3C69643E3C2F69643E3C76657273696F6E3E3C2F76657273696F6E3E3C6170704E616D653E3C2F6170704E616D653E3C696E7374616C6C55726C3E3C2F696E7374616C6C55726C3E3C66726F6D55726C3E3C2F66726F6D55726C3E3C6973466F7263655570646174653E303C2F6973466F7263655570646174653E3C2F617070496E666F3E3C736F75726365557365724E616D653E3C2F736F75726365557365724E616D653E3C736F757263654E69636B4E616D653E3C2F736F757263654E69636B4E616D653E3C73746174697374696373446174613E3C2F73746174697374696373446174613E3C737461744578745374723E3C2F737461744578745374723E3C436F6E74656E744F626A6563743E3C636F6E74656E745374796C653E323C2F636F6E74656E745374796C653E3C7469746C653E3C2F7469746C653E3C6465736372697074696F6E3E3C2F6465736372697074696F6E3E3C6D656469614C6973743E3C2F6D656469614C6973743E3C636F6E74656E7455726C3E3C2F636F6E74656E7455726C3E3C2F436F6E74656E744F626A6563743E3C616374696F6E496E666F3E3C6170704D73673E3C6D657373616765416374696F6E3E3C2F6D657373616765416374696F6E3E3C2F6170704D73673E3C2F616374696F6E496E666F3E3C6C6F636174696F6E20636974793D5C225C2220706F69436C61737369667949643D5C225C2220706F694E616D653D5C225C2220706F69416464726573733D5C225C2220706F69436C617373696679547970653D5C22305C223E3C2F6C6F636174696F6E3E3C7075626C6963557365724E616D653E3C2F7075626C6963557365724E616D653E3C2F54696D656C696E654F626A6563743E0D0A1800280030003A13736E735F706F73745F313533343933333731384001580068008001009A010A0A0012001A0020002800AA010408001200C00100".ToByteArray(16, 2));



            SnsPostReq.baseRequest = new BaseRequest() {
                clientVersion = iPad_info.ver,
                devicelId = System.Text.Encoding.Default.GetBytes(this._Device.devicelId),
                scene = 0,
                osType = iPad_info.oSType,
                sessionKey = this._Device.AesKey.ToByteArray(16, 2),
                uin = this._Device.m_Uin,

            };
            SnsPostReq.objectDesc.iLen = (uint)content.Length;
            SnsPostReq.objectDesc.buffer = content;

            SnsPostReq.clientId = "sns_post_" + CurrentTime_().ToString();
            SnsPostReq.groupNum = 1;
            SnsPostReq.groupIds = new SnsGroup[1];
            SnsPostReq.groupIds[0] = new SnsGroup() { GroupId = 3 };
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
            byte[] RetDate = HttpPost(SendDate, URL.CGI_MMSNSPORT);
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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
        /// 同步信息用来同步未读信息和好友
        /// </summary>
        /// <param name="scane">4</param>
        /// <param name="synckey">使用NewInit 发送后会返回synckey</param>
        /// <returns></returns>
        public NewSyncResponse NewSyncEcode()
        {
            var RespProtobuf = new byte[0];
            //0a020800108780101a8a02088402128402081f1208080110aaa092ba021208080210a9a092ba0212080803109aa092ba021208080410f28292ba021208080510f28292ba021208080710f28292ba02120408081000120808091099a092ba021204080a10001208080b10839f92ba021204080d10001208080e10f28292ba021208081010f28292ba021204086510001204086610001204086710001204086810001204086910001204086b10001204086d10001204086f1000120408701000120408721000120908c90110f5d7fbd705120908cb0110c6bcf3d705120508cc011000120508cd011000120908e80710fdd0fad705120908e90710ba92fad705120908ea07109bf1c9d705120908d10f10d1b9f0d70520032a0a616e64726f69642d31393001
            //MemoryStream memoryStream = new MemoryStream();
            NewSyncRequest request = new NewSyncRequest()
            {
                continueflag = new FLAG() { flag = 0 },
                device = iPad_info.oSType,
                scene = 3,
                selector = 7,//3
                syncmsgdigest = 1,
                tagmsgkey = new syncMsgKey()
                {
                    msgkey = new Synckey()
                    {
                        size = 32
                    }
                }
            };

            request.tagmsgkey = Util.Deserialize<syncMsgKey>(this._Device.SyncKey);

            var src = Util.Serialize(request);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_NEWSYNC, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = HttpPost(SendDate, URL.CGI_NEWSYNC);

            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                Debug.Print(RespProtobuf.ToString(16, 2));
                var NewSync = Util.Deserialize<NewSyncResponse>(RespProtobuf);
                if (NewSync != null)
                {
                    this._Device.SyncKey = NewSync.sync_key;
                    return NewSync;
                }
            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            
            return new NewSyncResponse() ;
        }

        public micromsg.NewVerifyPasswdResponse NewVerifyPasswd(string passwd)
        {
            micromsg.NewVerifyPasswdRequest newVerifyPasswd = new micromsg.NewVerifyPasswdRequest()
            {

                BaseRequest = new micromsg.BaseRequest()
                {
                    ClientVersion = iPad_info.ver,
                    DeviceID = Encoding.Default.GetBytes(this._Device.devicelId),
                    Scene = (uint)0,
                    SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                    DeviceType = Encoding.Default.GetBytes(iPad_info.oSType),
                    Uin = (uint)this._Device.m_Uin,
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
            byte[] RetDate = HttpPost(SendDate, "/cgi-bin/micromsg-bin/newverifypasswd");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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
        public micromsg.NewSetPasswdResponse NewSetPasswd(string NewPasswd, string Ticket)
        {

            micromsg.SetPwdRequest newSetPasswdRequest = new micromsg.SetPwdRequest()
            {
                AutoAuthKey = new micromsg.SKBuiltinBuffer_t()
                {
                    Buffer = this._Device.autoAuthKey_buff,
                    iLen = (uint)this._Device.autoAuthKey_buff.Length
                },
                BaseRequest = new micromsg.BaseRequest()
                {
                    ClientVersion = iPad_info.ver,
                    DeviceID = Encoding.Default.GetBytes(this._Device.devicelId),
                    Scene = (uint)0,
                    SessionKey = this._Device.AesKey.ToByteArray(16,2),
                    DeviceType = Encoding.Default.GetBytes(iPad_info.oSType),
                    Uin = (uint)this._Device.m_Uin,
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
            byte[] RetDate = HttpPost(SendDate, "/cgi-bin/micromsg-bin/newsetpasswd");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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
        /// 二次登录
        /// </summary>
        /// <param name="AutoAuthKey">一次成功登录时返回的autoauthkey</param>
        /// <returns></returns>
        public ManualAuthResponse AutoAuthRequest()
        {
            //this._Device.devicelId = ((new Random()).NextBytes(16).ToString(16, 2)).ToLower();

            this._Device.UUid = System.Guid.NewGuid().ToString();

            this._Device.AesKey = GetAESkey();

            this._Device.DevicelType = String.Format(@"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>{1}</k19><k20>{0}</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>b1:72:cf:83:a9:53</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>", this._Device.UUid, System.Guid.NewGuid().ToString());
            //this._Device.pub_key_buf = new byte[]

            //Util.GenerateECKey(713, this._Device.pub_key_buf, this._Device.pri_key_buf);


            this._Device.IMEI = ((new Random()).NextBytes(16).ToString(16, 2)).ToLower();
            //GenerateECKey(713, pub_key_buf, pri_key_buf);
            micromsg.AutoAuthRequest autoAuthRequest = new micromsg.AutoAuthRequest()
            {
                AesReqData = new micromsg.AutoAuthAesReqData()
                {
                    BaseRequest = new micromsg.BaseRequest()
                    {
                        ClientVersion = (int)iPad_info.ver,
                        Scene = 1,
                        DeviceID =this._Device.devicelId.ToByteArray(16,2),
                        SessionKey = this._Device.AesKey.ToByteArray(16, 2),
                        DeviceType = Encoding.Default.GetBytes(iPad_info.oSType),
                        Uin = (uint)this._Device.m_Uin
                    },
                    AutoAuthKey = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = this._Device.autoAuthKey_buff,
                        iLen = (uint)this._Device.autoAuthKey_buff.Length
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
                    IMEI = this._Device.IMEI,
                    TimeZone = "8.00",
                    DeviceName = "",
                    Language = "zh_CN",
                    BuiltinIPSeq = 0,
                    //DeviceType = "",
                    SoftType="",
                    Signature="",

                    DeviceType = @"<softtype><k3>9.0.2</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>52336395-2829-C5BB-CF9C-1B65A2E52EA6</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>b8:73:cf:87:a9:53</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>",
                    //DeviceType = this._Device.DevicelType

                },
                RsaReqData = new micromsg.AutoAuthRsaReqData()
                {
                    AesEncryptKey = new micromsg.SKBuiltinBuffer_t()
                    {
                        Buffer = this._Device.AesKey.ToByteArray(16, 2),
                        iLen = 16
                    },
                    CliPubECDHKey = new micromsg.ECDHKey()
                    {
                        Key = new micromsg.SKBuiltinBuffer_t()
                        {
                            Buffer = this._Device.pub_key_buf,
                            iLen = 57
                        },
                        Nid = 713,

                    }
                }

            };

            autoAuthRequest.AesReqData.ClientSeqID = autoAuthRequest.AesReqData.IMEI + "-" + ((int)CurrentTime_()).ToString();

            //用rsa对authkey进行压缩加密
            byte[] RsaReqBuf = Util.Serialize(autoAuthRequest.RsaReqData);
            //Console.WriteLine("RsaReq:\r\n" + CommUtil.ToHexStr(RsaReqBuf));
            byte[] rsaData = Util.compress_rsa_LOGIN(RsaReqBuf);

            //用aes对authkey进行压缩加密
            byte[] AuthAesData = Util.compress_aes(RsaReqBuf, this._Device.AesKey.ToByteArray(16, 2));

            //用aes对设备信息进行压缩加密
            byte[] AesReqBuf = Util.Serialize(autoAuthRequest.AesReqData);
            //Console.WriteLine("aesReq:\r\n" + CommUtil.ToHexStr(AesReqBuf));
            byte[] aesData = Util.compress_aes(AesReqBuf, this._Device.AesKey.ToByteArray(16, 2));


            byte[] Body = new byte[] { };
            Body = Body.Concat(RsaReqBuf.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(AesReqBuf.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(rsaData.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(AuthAesData.Length.ToByteArray(Endian.Big)).ToArray();
            Body = Body.Concat(rsaData).ToArray();
            Body = Body.Concat(AuthAesData).ToArray();
            Body = Body.Concat(aesData).ToArray();

            var head = MakeHead(702, Body.Length, 9, true, true);

            byte[] retData = head.Concat(Body).ToArray();

            Debug.Print(retData.ToString(16, 2));

            byte[] RespProtobuf = new byte[0];

            byte[] RetDate = HttpPost(retData, "/cgi-bin/micromsg-bin/autoauth");
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
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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
        public micromsg.PushLoginURLResponse PushLoginURL()
        {
            //byte[] uuid = Encoding.Default.GetBytes(_Device.devicelId);
            //uuid[15] = 0;
            PushLoginURLRequest pushLoginURLRequest = new PushLoginURLRequest()
            {
                baseRequest = new BaseRequest() {
                       clientVersion = iPad_info.ver,
                       devicelId = System.Text.Encoding.Default.GetBytes(this._Device.devicelId),
                       osType = iPad_info.oSType,
                       scene = 1,
                       sessionKey = this._Device.AesKey.ToByteArray(16,2),
                       uin = this._Device.m_Uin
                },
                Autoauthkey = new SKBuiltinString_()
                {
                    buffer = this._Device.autoAuthKey_buff,
                    iLen = (uint)this._Device.autoAuthKey_buff.Length
                },
                Autoauthticket = "",
                ClientId = this._Device.devicelId + "-" + ((int)CurrentTime_()).ToString(),
                Devicename = "ipad",
                //DeviceName ="ipad",
                opcode = 1,
                randomEncryKey = new AesKey()
                {
                    key = this._Device.AesKey.ToByteArray(16, 2),
                    len = 16
                },
                username = this._Device.wxid,
                rsa = new RSAPem()
                {

                }

            };


            var src = Util.Serialize(pushLoginURLRequest);
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, (int)CGI_TYPE.CGI_TYPE_PUSHLOGINURL, bufferlen, 1, true, true);
            //发包
            byte[] RetDate = HttpPost(SendDate, URL.CGI_PUSHLOGINURL
);
            byte[] RespProtobuf = new byte[0];

            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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
        /// 扫码登录
        /// </summary>
        /// <param name="url">二维码地址</param>
        /// <returns></returns>
        public micromsg.ExtDeviceLoginConfirmGetResponse ExtDeviceLoginConfirmGet(string url)
        {

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
            byte[] RetDate = HttpPost(SendDate, "/cgi-bin/micromsg-bin/extdeviceloginconfirmget");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
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

        /// <summary>
        /// 确认扫码登录
        /// </summary>
        /// <param name="url">二维码地址</param>
        /// <returns></returns>
        public micromsg.ExtDeviceLoginConfirmOKResponse ExtDeviceLoginConfirmOK(string url)
        {

            micromsg.ExtDeviceLoginConfirmOKRequest extDeviceLoginConfirmGet_ = new micromsg.ExtDeviceLoginConfirmOKRequest()
            {
                LoginUrl = url,
                
            };

            var src = Util.Serialize(extDeviceLoginConfirmGet_);

            byte[] RespProtobuf = new byte[0];

            Debug.Print(src.ToString(16, 2));
            int bufferlen = src.Length;
            //组包
            byte[] SendDate = pack(src, 972, bufferlen, 5, true, true);
            //发包
            byte[] RetDate = Util.HttpPost(SendDate, "/cgi-bin/micromsg-bin/extdeviceloginconfirmok");
            if (RetDate.Length > 32)
            {
                var packinfo = UnPackHeader(RetDate);
                //Console.WriteLine("CGI {0} BodyLength {1} m_bCompressed {2}", packinfo.CGI, packinfo.body.Length, packinfo.m_bCompressed);
                RespProtobuf = packinfo.body;
                if (packinfo.m_bCompressed)
                {
                    RespProtobuf = Util.uncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }
                else
                {
                    RespProtobuf = Util.nouncompress_aes(packinfo.body, this._Device.AesKey.ToByteArray(16, 2));
                }

            }
            else
            {
                Console.WriteLine("数据包可能有问题：{0} 数据包长度 :{1}", RetDate.ToString(16, 2), RetDate.Length);
            }
            Debug.Print(RespProtobuf.ToString(16, 2));

            var ExtDeviceLoginConfirmGetResponse_ = Util.Deserialize<micromsg.ExtDeviceLoginConfirmOKResponse>(RespProtobuf);
            return ExtDeviceLoginConfirmGetResponse_;
        }


        private static byte[] Dword2String(UInt32 dw)
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
        private static int DecodeVByte32(ref int apuValue, byte[] apcBuffer, int off)
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
        private byte[] toVariant(int toValue)
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


        /// <summary>
        /// 解包头 返回 包数据结构
        /// </summary>
        /// <param name="pack"></param>
        /// <returns></returns>
        private PACKINFO UnPackHeader(byte[] pack)
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
            this._Device.m_Uin = (int)pack.Copy(nCur, 4).GetUInt32(Endian.Big);
            //memcpy(&dwUin, &(pack[nCur]), 4);
            //s_dwUin = ntohl(dwUin);
            nCur += 4;
            //刷新cookie(超过15字节说明协议头已更新)
            if (nCookieLen > 0 && nCookieLen <= 0xf)
            {
                string s_cookie = pack.Copy(nCur, nCookieLen).ToString(16, 2);
                //pAuthInfo->m_cookie = s_cookie;
                this._Device.cookie = s_cookie;
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


        /// <summary>
        /// 组包头
        /// </summary>
        /// <param name="cgi"></param>
        /// <param name="nLenProtobuf"></param>
        /// <param name="encodetypr"></param>
        /// <param name="iscookie"></param>
        /// <param name="isuin"></param>
        /// <returns></returns>
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
                dwUin = this._Device.m_Uin;
            strHeader = strHeader.Concat(iPad_info.ver.ToByteArray(Endian.Big).ToList()).ToList();
            nCur += 4;
            Debug.Print(iPad_info.ver.ToByteArray(Endian.Big).ToArray().ToString(16, 2));
            strHeader = strHeader.Concat(dwUin.ToByteArray(Endian.Big).ToList()).ToList();
            nCur += 4;

            if (iscookie)
            {
                //登录包不需要cookie 全0占位即可
                if (this._Device.cookie == null || this._Device.cookie == "" || this._Device.cookie.Length < 0xf)
                {
                    strHeader = strHeader.Concat(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }).ToList();
                    nCur += 15;
                }
                else
                {
                    strHeader = strHeader.Concat(this._Device.cookie.ToByteArray(16, 2).ToList()).ToList();
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
            var rsaVer = Dword2String((UInt32)iPad_info.LOGIN_RSA_VER);
            strHeader = strHeader.Concat(rsaVer).ToList();
            nCur += rsaVer.Length;
            strHeader = strHeader.Concat(new byte[] { 1, 2 }.ToList()).ToList();
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
        /// 组包头
        /// </summary>
        /// <param name="lengthBeforeZip"></param>
        /// <param name="lengthAfterZip"></param>
        /// <param name="aesDataPacket"></param>
        /// <param name="uin"></param>
        /// <param name="deviceID"></param>
        /// <param name="_byteVar"></param>
        /// <returns></returns>
        private byte[] CommonRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] aesDataPacket, uint uin,
            short cmd, short cmd2, byte[] cookie, uint check)
        {
            byte[] frontPacket = {
                                     0xBF, 0x62, 0x50, 0x16, 0x07, 0x03, 0x21
                                 };
            //Debug.Print(frontPacket.ToString(16, 2));
            //16070321
            byte[] endTag = { 0x02 };
            byte[] byteUin = new byte[4];

            uint a = (uin & 0xff000000);
            byteUin[0] = (byte)(((uin & 0xff000000) >> 24) & 0xff);
            byteUin[1] = (byte)(((uin & 0x00ff0000) >> 16) & 0xff);
            byteUin[2] = (byte)(((uin & 0x0000ff00) >> 8) & 0xff);
            byteUin[3] = (byte)((uin & 0x000000ff) & 0xff);

            byte[] packet = frontPacket.Concat(byteUin).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(cookie).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(cmd2)).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(lengthBeforeZip)).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(lengthAfterZip)).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(10000)).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(endTag).ToArray();
            packet = packet.Concat(toVariant((int)check)).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(toVariant(0x01004567)).ToArray();
            //Debug.Print(packet.ToString(16, 2));
            int HeadLen = packet.Length;
            //Debug.Print(packet.ToString(16, 2));
            packet[1] = (byte)((HeadLen * 4) + 1);
            packet[2] = (byte)(0x50 + cookie.Length);

            Debug.Print(packet.ToString(16, 2));
            packet = packet.Concat(aesDataPacket).ToArray();

            return packet;
        }

        public byte[] AuthRequestPacket(int lengthBeforeZip, int lengthAfterZip, byte[] rsaDataPacket, string deviceID, ushort cmd)
        {

            //8A7F2606003000000000000000000000000000000000000000BD058C098C099E0102
            byte[] frontPacket = {
                                     0x4E, 0x70, 0x26, 0x05, 0x04, 0x30, 0x00, 0x00, 0x00, 0x00
                                 };
            byte[] endTag = { 0x9E, 0x01, 0x02 };


            byte[] packet = frontPacket.Concat(toVariant(cmd)).Concat(toVariant(lengthBeforeZip)).Concat(toVariant(lengthAfterZip)).Concat(endTag).ToArray();
            int HeadLen = packet.Length;

            packet[0] = (byte)((HeadLen << 2) + 2);

            packet = packet.Concat(rsaDataPacket).ToArray();

            return packet;
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
        private byte[] pack(byte[] src, int cgi_, int nLenProtobuf, byte encodetypr = 7, bool iscookie = false, bool isuin = false)
        {
            //组包头
            var pbody = new byte[0];
            if (encodetypr == 7)
            {
                var head = MakeHead(cgi_, src.Length, encodetypr, iscookie, isuin);
                pbody = head.Concat(Util.nocompress_rsa(src)).ToArray();
            }
            else if (encodetypr == 5)
            {
                //计算校验
                //uint check_ = check((uint)this.m_uid, src, pri_key_buf);
                //压缩
                byte[] zipData = DeflateCompression.DeflateZip(src);

                //byte[] zipData = ZipUtils.deCompressBytes(src);
                int lenAfterZip = zipData.Length;

                //aes加密
                byte[] aesData = Util.AESEncryptorData(zipData, this._Device.AesKey.ToByteArray(16, 2));

                pbody = CommonRequestPacket(src.Length, lenAfterZip, aesData, (uint)this._Device.m_Uin, 0xd, (short)cgi_, this._Device.cookie.ToByteArray(16, 2), 0);
            }
            else if (encodetypr == 1)
            {
                var head = MakeHead(cgi_, src.Length, encodetypr, iscookie, isuin);
                pbody = head.Concat(Util.compress_rsa(src)).ToArray();
            }
            return pbody;
        }

        /// <summary>
        /// 发送数据封包
        /// </summary>
        /// <param name="data">数据封包</param>
        /// <param name="Url_GCI">请求地址</param>
        /// <returns></returns>
        public byte[] HttpPost(byte[] data, string Url_GCI)
        {
            
            //Console.WriteLine(shortUrl + Url_GCI);
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL =this._Device.shortUrl + Url_GCI,
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

        public static long CurrentTime_()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }

}

