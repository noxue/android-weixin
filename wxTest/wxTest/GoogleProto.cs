using Google.ProtocolBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mm.command;

namespace 微信挂机
{
    public class GoogleProto
    {
        private const int VERSION = 0x26060737;

        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long GetCurTime()
        {
            return (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds);
        }
        public static BindopMobileForRegRequest CreateMobileRegPacket(BaseRequest baseRequest, int opCode, string mobile, string verifyCode, byte[] randomEncryKey, 
            string devicetype, string clientid, string regSession)
        {
            BindopMobileForRegRequest.Builder builder = new BindopMobileForRegRequest.Builder();
            builder.SetBase(baseRequest);
            builder.SetOpcode(opCode).SetMobile(mobile).SetVerifycode(verifyCode).SetDialFlag(0).SetDialLang("");
            builder.SetSafeDeviceName("Android设备");
            builder.SetSafeDeviceType(devicetype);
            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            skb.SetILen(randomEncryKey.Length);
            skb.SetBuffer(ByteString.CopyFrom(randomEncryKey));
            builder.SetRandomEncryKey(skb);
            builder.SetForceReg(0);
            builder.SetInputMobileRetrys(0);
            builder.SetAdjustRet(0);
            if (opCode == 15)
            {
                builder.SetMobileCheckType(0);
            }
            else
            {
                builder.SetMobileCheckType(0);
            }
            builder.SetClientSeqId(clientid);
            if (opCode != 12)
            {
                builder.SetRegSessionID(regSession);
            }

            return builder.Build();
        }

        internal static UploadMContact UploadMContact(string sessionKey, uint uin, string deviceID, string OSType, string mobile, List<string> contacts, string userName)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            UploadMContact.Builder lrb = new UploadMContact.Builder();
            lrb.SetBase(br);
            lrb.SetMobile(mobile);
            lrb.SetUserName(userName);
            lrb.SetOpcode(1);
            lrb.SetMobileListSize(contacts.Count);

            foreach (string item in contacts)
            {
                SKBuiltinString_t.Builder one = new SKBuiltinString_t.Builder();
                one.SetString(item);
                lrb.AddMobiles(one);
            }
            lrb.SetEmailListSize(0);


            return lrb.Build();
        }

        public static GetMFriendRequest GetMFriend(string sessionKey, uint uin, string deviceID, string OSType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            GetMFriendRequest.Builder lrb = new GetMFriendRequest.Builder();
            lrb.SetBase(br);
            lrb.SetOpType(0);
            lrb.SetMD5("");

            return lrb.Build();
        }

        public static ThrowBottleRequest CreateThrowBottleRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string text)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            ThrowBottleRequest.Builder lrb = new ThrowBottleRequest.Builder();
            lrb.SetBase(br);
            lrb.SetMsgType(1);
            lrb.SetBottleType(0);

            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            byte[] data = System.Text.Encoding.GetEncoding("utf-8").GetBytes(text);
            skb.SetILen(data.Length);
            skb.SetBuffer(ByteString.CopyFrom(data));
            lrb.SetContent(skb.Build());
            lrb.SetStartPos(0);
            lrb.SetTotalLen(data.Length);
            lrb.SetClientID(string.Format("127c322ff65c763{0}", new Random().Next(10000, 99999)));
            lrb.SetVoiceLength(0);

            return lrb.Build();
        }
        public static BindOpMobileRequest CreateBindMobileRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string mobile, string deviceName, string deviceType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            BindOpMobileRequest.Builder lrb = new BindOpMobileRequest.Builder();
            lrb.SetBase(br);
            lrb.SetMobile(mobile);
            lrb.SetOpcode(3);
            lrb.SetVerifycode("");
            lrb.SetDialFlag(0);
            lrb.SetDialLang("");
            lrb.SetForceReg(0);
            lrb.SetSafeDeviceName(deviceName);
            lrb.SetSafeDeviceType(deviceType);

            return lrb.Build();
        }

        public static BindQQRequest CreateBindMobileRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string qq, string pass, string deviceName, string deviceType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            BindQQRequest.Builder lrb = new BindQQRequest.Builder();
            lrb.SetBase(br);
            lrb.SetQQ((uint)long.Parse(qq));
            string strMD5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5").ToLower();
            lrb.SetPwd(strMD5Pwd);
            lrb.SetPwd2(strMD5Pwd);
            lrb.SetImgSid("");
            lrb.SetImgCode("");
            lrb.SetOPCode(1);
            lrb.SetImgEncryptKey(new SKBuiltinString_t.Builder().SetString(""));
            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            skb.SetILen(0);
            skb.SetBuffer(ByteString.CopyFrom(new byte[0]));

            lrb.SetKSid(skb);
            lrb.SetSafeDeviceName(deviceName);
            lrb.SetSafeDeviceType(deviceType);

            return lrb.Build();
        }

        public static BindEmailRequest BindEmailEntity(string sessionKey, uint uin, string deviceID, string OSType, string email)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            BindEmailRequest.Builder ber = new BindEmailRequest.Builder();
            ber.SetBase(br);
            ber.SetOpCode(1);
            ber.SetEmail(email);
            return ber.Build();
        }

        public static BaseRequest CreateBaseRequestEntity(string deviceID, string osType)
        {
            return CreateBaseRequestEntity(deviceID, osType, 1);
        }

        public static BaseRequest CreateBaseRequestEntity(string deviceID, string osType, int scene)
        {
            BaseRequest.Builder brb = new BaseRequest.Builder();
            byte[] byt = new byte[0];
            brb.SetSessionKey(ByteString.CopyFrom(byt));
            brb.SetUin(0);
            byte[] bytSD = new byte[16];
            bytSD = Encoding.Default.GetBytes(deviceID + "\x00");
            brb.SetDeviceID(ByteString.CopyFrom(bytSD));
            brb.SetClientVersion(VERSION);
            //osType = "iPad iPhone OS7.0.3";
            brb.SetDeviceType(ByteString.CopyFrom(osType, Encoding.Default));
            brb.SetScene(scene);
            BaseRequest br = brb.Build();

            return br;
        }

        public static BaseRequest CreateBaseRequestEntity(string deviceID, string sessionKey, uint uIn, string osType)
        {
            return CreateBaseRequestEntity(deviceID, sessionKey, uIn, osType, 0);
        }

        public static BaseRequest CreateBaseRequestEntity(string deviceID, string sessionKey, uint uIn, string osType, int scene)
        {
            BaseRequest.Builder brb = new BaseRequest.Builder();
            byte[] byt = new byte[36];
            brb.SetSessionKey(ByteString.CopyFrom(sessionKey, Encoding.Default));
            brb.SetUin(uIn);
            byte[] bytSD = new byte[16];
            bytSD = Encoding.Default.GetBytes(deviceID + "\x00");
            brb.SetDeviceID(ByteString.CopyFrom(bytSD));
            brb.SetClientVersion(VERSION);
            brb.SetDeviceType(ByteString.CopyFrom(osType, Encoding.Default));
            brb.SetScene(scene);
            BaseRequest br = brb.Build();

            return br;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="wxAccount">用户名</param>
        /// <param name="wxPwd">密码</param>
        /// <param name="imei">串号</param>
        /// <param name="MANUFACTURER">手机名(厂家)</param>
        /// <param name="MODEL">手机型号</param>
        /// <param name="RELEASE">版本号</param>
        /// <param name="INCREMENTAL">增量版本</param>
        /// <param name="DISPLAY">显示名</param>
        /// <param name="OSType">OSType</param>
        /// <param name="randomEncryKey"></param>
        /// <returns></returns>
        public static AuthRequest CreateAuthRequestEntity(BaseRequest br, string wxAccount, string wxPwd, string imei, string MANUFACTURER, string MODEL, string RELEASE, string INCREMENTAL, string DISPLAY, string OSType, byte[] randomEncryKey)
        {
            AuthRequest.Builder aqb = new AuthRequest.Builder();
            aqb.SetBase(br);
            aqb.SetUserName(new SKBuiltinString_t.Builder().SetString(wxAccount).Build());
            string strWxPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(wxPwd, "MD5");
            aqb.SetPwd(new SKBuiltinString_t.Builder().SetString(strWxPwd).Build());
            aqb.SetImgSid(new SKBuiltinString_t.Builder().SetString("").Build());
            aqb.SetImgCode(new SKBuiltinString_t.Builder().SetString("").Build());
            aqb.SetPwd2(strWxPwd);
            aqb.SetBuiltinIPSeq(0);
            aqb.SetExtPwd(strWxPwd);
            aqb.SetExtPwd2(strWxPwd);
            aqb.SetTimeZone("8.00");
            aqb.SetLanguage("en_US");
            aqb.SetIMEI(imei);
            aqb.SetChannel(0);
            aqb.SetImgEncryptKey(new SKBuiltinString_t.Builder().SetString("").Build());
            aqb.SetKSid(new SKBuiltinBuffer_t.Builder().SetILen(0).SetBuffer(ByteString.CopyFrom("", Encoding.Default)));
            aqb.SetDeviceBrand(MANUFACTURER);
            aqb.SetDeviceModel(MODEL);
            aqb.SetOSType(OSType);
            aqb.SetDeviceType("<deviceinfo><MANUFACTURER name=\"" + MANUFACTURER + "\"><MODEL name=\"" + MODEL + "\"><VERSION_RELEASE name=\"" 
                + RELEASE + "\"><VERSION_INCREMENTAL name=\"" + INCREMENTAL + "\"><DISPLAY name=\"" 
                + DISPLAY + "\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>");
            aqb.SetAuthTicket("");
            //auth.set_signature("18c867f0717aa67b2ab7347505ba07ed");//18c867f0717aa67b2ab7347505ba07ed 正式版 e89b158e4bcf988ebd09eb83f5378e87 修改版
            aqb.SetSignature("18c867f0717aa67b2ab7347505ba07ed");
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            //byte[] randomKey = new byte[] { 0x8e, 0x61, 0x93, 0x89, 0xe8, 0x99, 0x42, 0x1d, 0x07, 0x74, 0xF0, 0x09, 0x36, 0x5e, 0x4b, 0x1f };
            //byte[] randomKey = new byte[16];
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));//("\x8e\x61\x93\x89\xe8\x99\x42\x1d\x07\x74\xF0\x09\x36\x5e\x4b\x1f",Encoding.Default));
            SKBuiltinBuffer_t sbk = skbb.Build();
            aqb.SetRandomEncryKey(sbk);

            AuthRequest ar = aqb.Build();

            return ar;
        }
        public static InitKey CreateInitKeyEntity(byte[] randomEncryKey, ECDHKey cliPubECDHKey, string WXAccount, string wxPwd)
        {
            InitKey.Builder key = new InitKey.Builder();
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));
            key.SetRandomEncryKey(skbb);
            key.SetCliPubECDHKey(cliPubECDHKey);
            key.SetAccount(WXAccount);
            string strWxPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(wxPwd, "MD5");
            key.SetPwd(strWxPwd);
            key.SetPwd2(strWxPwd);

            return key.Build();
        }

        public static AutoAuthRsaReqData CreateAutoAuthKeyEntity(byte[] randomEncryKey, ECDHKey cliPubECDHKey)
        {
            AutoAuthRsaReqData.Builder key = new AutoAuthRsaReqData.Builder();
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));
            key.SetAesEncryptKey(skbb);
            key.SetCliPubECDHKey(cliPubECDHKey);            

            return key.Build();
        }
        
        public static ManualAuthRequest CreateManualAuthRequestEntity(BaseRequest br, string imei, string MANUFACTURER, string MODEL, string OSType,
            string fingerprint, string clientID, string abi, string deviceType)
        {
            ManualAuthRequest.Builder aqb = new ManualAuthRequest.Builder();
            aqb.SetBase(br);
            //aqb.SetData();
            aqb.SetIMEI(imei);
            aqb.SetSoftType(fingerprint);
            aqb.SetBuiltinIpseq(0);
            aqb.SetClientSeqId(clientID);
            aqb.SetSignature("18c867f0717aa67b2ab7347505ba07ed");
            aqb.SetDeviceName(MANUFACTURER + "-" + MODEL);
            aqb.SetDeviceType(deviceType);
            aqb.SetLanguage("zh_CN");            
            aqb.SetTimeZone("8.00");           
            aqb.SetChannel(0);
            aqb.SetTimeStamp(0);
            aqb.SetDeviceBrand(MANUFACTURER);
            aqb.SetDeviceModel(MODEL+abi);
            aqb.SetOSType(OSType);
            aqb.SetCountryCode("cn");
            aqb.SetInputType(1);

            ManualAuthRequest ar = aqb.Build();

            return ar;
        }

        public static AutoAuthRequest CreateAutoAuthRequestEntity(BaseRequest br, string imei, string MANUFACTURER, string MODEL, string RELEASE, 
            string INCREMENTAL, string DISPLAY, string OSType, SKBuiltinBuffer_t autoauthkey)
        {
            AutoAuthRequest.Builder aqb = new AutoAuthRequest.Builder();
            aqb.SetBase(br);
            aqb.SetAutoAuthKey(autoauthkey);
            aqb.SetIMEI(imei);
            aqb.SetSoftType("");
            aqb.SetBuiltinIpseq(0);
            aqb.SetClientSeqId("");
            aqb.SetSignature("18c867f0717aa67b2ab7347505ba07ed");
            aqb.SetDeviceName("Xiaomi-MI 2S");
            aqb.SetDeviceType("<deviceinfo><MANUFACTURER name=\"" + MANUFACTURER + "\"><MODEL name=\"" + MODEL + "\"><VERSION_RELEASE name=\""
                + RELEASE + "\"><VERSION_INCREMENTAL name=\"" + INCREMENTAL + "\"><DISPLAY name=\""
                + DISPLAY + "\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>");
            aqb.SetLanguage("zh_CN");
            aqb.SetTimeZone("8.00");

            AutoAuthRequest ar = aqb.Build();

            return ar;
        }

        internal static NewVerifyPasswdRequest NewVerifyPasswd(string sessionKey, uint uin, string deviceID, string OSType, string pass)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            NewVerifyPasswdRequest.Builder rb = new NewVerifyPasswdRequest.Builder();
            rb.SetBase(br);
            string md5Pass = Fun.CumputeMD5(pass);
            rb.SetOpCode(1);
            rb.SetPwd1(md5Pass);
            rb.SetPwd2(md5Pass);

            return rb.Build();
        }
        public static NewSetPasswdRequest CreateNewSetPassRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string newPass, string ticket, string authkey)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            NewSetPasswdRequest.Builder lrb = new NewSetPasswdRequest.Builder();
            lrb.SetBase(br);
            lrb.SetPassword(newPass);
            lrb.SetTicket(ticket);
            SKBuiltinBuffer_t.Builder bb = new SKBuiltinBuffer_t.Builder();
            byte[] auth = Convert.FromBase64String(authkey);
            bb.SetILen(auth.Length);
            bb.SetBuffer(ByteString.CopyFrom(auth));
            lrb.SetAutoAuthKey(bb.Build());

            return lrb.Build();
        }

        public static AuthRequest CreateAuthRequestEntity(BaseRequest br, string wxAccount, string wxPwd, string imei, string deviceBrand, string deviceModel, byte[] randomEncryKey, string imgSID, string code, string imgKey)
        {
            AuthRequest.Builder aqb = new AuthRequest.Builder();
            aqb.SetBase(br);
            aqb.SetUserName(new SKBuiltinString_t.Builder().SetString(wxAccount).Build());
            string strWxPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(wxPwd, "MD5");
            aqb.SetPwd(new SKBuiltinString_t.Builder().SetString(strWxPwd).Build());
            aqb.SetImgSid(new SKBuiltinString_t.Builder().SetString(imgSID).Build());
            aqb.SetImgCode(new SKBuiltinString_t.Builder().SetString(code).Build());
            aqb.SetPwd2(strWxPwd);
            aqb.SetBuiltinIPSeq(0);
            aqb.SetExtPwd(strWxPwd);
            aqb.SetExtPwd2(strWxPwd);
            aqb.SetTimeZone("8.00");
            aqb.SetLanguage("zh_CN");
            aqb.SetIMEI(imei);
            aqb.SetChannel(0);
            aqb.SetImgEncryptKey(new SKBuiltinString_t.Builder().SetString(imgKey).Build());
            aqb.SetKSid(new SKBuiltinBuffer_t.Builder().SetILen(0).SetBuffer(ByteString.CopyFrom("", Encoding.Default)));
            aqb.SetDeviceBrand(deviceBrand);
            aqb.SetDeviceModel(deviceModel);
            aqb.SetOSType("android-10");
            aqb.SetDeviceType("<deviceinfo><MANUFACTURER name=\"unknown\"><MODEL name=\"sdk\"><VERSION_RELEASE name=\"2.3.3\"><VERSION_INCREMENTAL name=\"101070\"><DISPLAY name=\"sdk-eng 2.3.3 GRI34 101070 test-keys\"></DISPLAY></VERSION_INCREMENTAL></VERSION_RELEASE></MODEL></MANUFACTURER></deviceinfo>");
            aqb.SetAuthTicket("");
            aqb.SetSignature("e89b158e4bcf988ebd09eb83f5378e87");
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            //byte[] randomKey = new byte[] { 0x8e, 0x61, 0x93, 0x89, 0xe8, 0x99, 0x42, 0x1d, 0x07, 0x74, 0xF0, 0x09, 0x36, 0x5e, 0x4b, 0x1f };
            //byte[] randomKey = new byte[16];
            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));//("\x8e\x61\x93\x89\xe8\x99\x42\x1d\x07\x74\xF0\x09\x36\x5e\x4b\x1f",Encoding.Default));
            SKBuiltinBuffer_t sbk = skbb.Build();
            aqb.SetRandomEncryKey(sbk);

            AuthRequest ar = aqb.Build();

            return ar;
        }

        public static NewRegRequest CreateNewRegRequestEntity(BaseRequest br, string wxAccount, string wxPwd, string nickName, string ticket, byte[] randomEncryKey, 
            ECDHKey cliPubECDHKey, string clientid, string androidid, string fingerprint, string mac, string regID)
        {
            NewRegRequest.Builder nggb = new NewRegRequest.Builder();
            nggb.SetBase(br);
            nggb.SetUserName("");
            string strPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(wxPwd, "MD5");
            nggb.SetPwd(strPwd);
            nggb.SetNickName(nickName);
            nggb.SetBindUin(0);
            nggb.SetBindEmail("");
            nggb.SetBindMobile(wxAccount);
            nggb.SetTicket(ticket);
            nggb.SetBuiltinIPSeq(0);
            nggb.SetDLSrc(0);
            nggb.SetRegMode(1);
            nggb.SetTimeZone("8.00");
            nggb.SetLanguage("zh_CN");
            nggb.SetForceReg(1);
            nggb.SetRealCountry("cn");
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);
            byte[] randomKey = randomEncryKey;
            skbb.SetBuffer(ByteString.CopyFrom(randomKey));
            SKBuiltinBuffer_t sbk = skbb.Build();
            nggb.SetRandomEncryKey(sbk);
            nggb.SetAlias("");
            nggb.SetVerifyContent("");
            nggb.SetVerifySignature("");
            nggb.SetHasHeadImg(0);
            nggb.SetSuggestRet(0);
            nggb.SetClientSeqId(clientid);
            //nggb.SetBundleId("");
            nggb.SetCliPubEcdhkey(cliPubECDHKey);
            //nggb.SetBundleId("");
            nggb.SetGoogleAid("");
            nggb.SetMobileCheckType(0);
            nggb.SetBioSigCheckType(0);
            nggb.SetRegSessionId(regID);
            nggb.SetAndroidInstallRef("");
            nggb.SetAndroidId(androidid);
            nggb.SetClientFingerprint(fingerprint);
            nggb.SetMacAddr(mac);

            NewRegRequest ngg = nggb.Build();

            return ngg;
        }

        public static NewInitRequest CreateNewInitRequestEntity(uint uin, string sessionKey, string userName, string deviceID, string OSType, byte[] init, byte[] max)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType, 3);

            NewInitRequest.Builder nirb = new NewInitRequest.Builder();
            nirb.SetBase(br);
            nirb.SetUserName(userName);
            nirb.SetLanguage("zh_CN");

            SKBuiltinBuffer_t.Builder csb = new SKBuiltinBuffer_t.Builder();
            
            if (init == null)
            {
                csb.SetILen(0);
                csb.SetBuffer(ByteString.CopyFrom(new byte[0] { }));
            }
            else
            {
                csb.SetILen(init.Length);
                csb.SetBuffer(ByteString.CopyFrom(init));
            }
            SKBuiltinBuffer_t cs = csb.Build();
            nirb.SetCurrentSynckey(cs);

            SKBuiltinBuffer_t.Builder msb = new SKBuiltinBuffer_t.Builder();
            if (max == null)
            {
                msb.SetILen(0);
                msb.SetBuffer(ByteString.CopyFrom(new byte[0] { }));
            }
            else
            {
                msb.SetILen(max.Length);
                msb.SetBuffer(ByteString.CopyFrom(max));
            }
            SKBuiltinBuffer_t ms = msb.Build();
            nirb.SetMaxSynckey(ms);

            NewInitRequest nir = nirb.Build();

            return nir;
        }

        public static NewSyncRequest CreateNewSyncRequestEntity(byte[] keyBuffer)
        {
            NewSyncRequest.Builder nsrb = new NewSyncRequest.Builder();
            CmdList.Builder clb = new CmdList.Builder();
            clb.SetCount(0);
            nsrb.SetOplog(clb.Build());
            nsrb.SetSelector(3);
            nsrb.SetScene(7);
            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            skb.SetBuffer(ByteString.CopyFrom(keyBuffer));
            skb.SetILen(keyBuffer.Length);
            nsrb.SetKeyBuf(skb.Build());
            nsrb.SetDeviceType("Android设备");
            nsrb.SetSyncMsgDigest(1);

            NewSyncRequest nsr = nsrb.Build();

            return nsr;
        }

        public static NewSyncRequest ModifyProfile(int sex, string province, string city, string signature, byte[] keyBuffer)
        {
            UserProfile.Builder upb = new UserProfile.Builder();
            upb.SetBitFlag(128);
            upb.SetUserName(new SKBuiltinString_t.Builder().SetString(""));
            upb.SetNickName(new SKBuiltinString_t.Builder().SetString("nick"));
            upb.SetBindUin(0);
            upb.SetBindEmail(new SKBuiltinString_t.Builder().SetString(""));
            upb.SetBindMobile(new SKBuiltinString_t.Builder().SetString(""));
            upb.SetStatus(0);
            upb.SetImgLen(0);
            upb.SetImgBuf(Google.ProtocolBuffers.ByteString.CopyFrom("", Encoding.Default));

            upb.SetSex(sex);
            upb.SetProvince(province);
            upb.SetCity(city);
            upb.SetSignature(signature);

            upb.SetPersonalCard(1);
            upb.SetPluginFlag(0);
            upb.SetPluginSwitch(0);
            upb.SetAlias("");
            upb.SetWeiboNickname("");
            upb.SetWeiboFlag(0);
            upb.SetCountry("CN");

            UserProfile upObj = upb.Build();
            byte[] byteUp = upObj.ToByteArray();

            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetBuffer(ByteString.CopyFrom(byteUp));
            skbb.SetILen(byteUp.Length);
            SKBuiltinBuffer_t skbObj = skbb.Build();

            CmdItem.Builder cib = new CmdItem.Builder();
            cib.SetCmdBuf(skbObj);
            cib.SetCmdId(1);
            CmdItem ciObj = cib.Build();

            CmdList.Builder clb = new CmdList.Builder();
            clb.SetCount(1);
            clb.AddList(ciObj);

            CmdList clObj = clb.Build();

            NewSyncRequest.Builder nsrb = new NewSyncRequest.Builder();
            nsrb.SetOplog(clObj);
            nsrb.SetSelector(7);
            nsrb.SetScene(7);
            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            skb.SetBuffer(ByteString.CopyFrom(keyBuffer));
            skb.SetILen(keyBuffer.Length);
            nsrb.SetKeyBuf(skb.Build());

            return nsrb.Build();
        }

        public static NewSyncRequest ModifyProfile(string nickName, byte[] keyBuffer)
        {
            UserProfile.Builder upb = new UserProfile.Builder();
            upb.SetBitFlag(2);
            upb.SetUserName(new SKBuiltinString_t.Builder().SetString(""));
            upb.SetNickName(new SKBuiltinString_t.Builder().SetString(nickName));
            upb.SetBindUin(0);
            upb.SetBindEmail(new SKBuiltinString_t.Builder().SetString(""));
            upb.SetBindMobile(new SKBuiltinString_t.Builder().SetString(""));
            upb.SetStatus(0);
            upb.SetImgLen(0);
            upb.SetImgBuf(Google.ProtocolBuffers.ByteString.CopyFrom("", Encoding.Default));

            upb.SetSex(0);
            upb.SetProvince("");
            upb.SetCity("");
            upb.SetSignature("");

            upb.SetPersonalCard(1);
            upb.SetPluginFlag(0);
            upb.SetPluginSwitch(0);
            upb.SetAlias("");
            upb.SetWeiboNickname("");
            upb.SetWeiboFlag(0);
            upb.SetCountry("CN");

            UserProfile upObj = upb.Build();
            byte[] byteUp = upObj.ToByteArray();

            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetBuffer(ByteString.CopyFrom(byteUp));
            skbb.SetILen(byteUp.Length);
            SKBuiltinBuffer_t skbObj = skbb.Build();

            CmdItem.Builder cib = new CmdItem.Builder();
            cib.SetCmdBuf(skbObj);
            cib.SetCmdId(1);
            CmdItem ciObj = cib.Build();

            CmdList.Builder clb = new CmdList.Builder();
            clb.SetCount(1);
            clb.AddList(ciObj);

            CmdList clObj = clb.Build();

            NewSyncRequest.Builder nsrb = new NewSyncRequest.Builder();
            nsrb.SetOplog(clObj);
            nsrb.SetSelector(7);
            nsrb.SetScene(7);
            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            skb.SetBuffer(ByteString.CopyFrom(keyBuffer));
            skb.SetILen(keyBuffer.Length);
            nsrb.SetKeyBuf(skb.Build());

            return nsrb.Build();
        }
        public static LBSFindRequest CreateLBSFindRequestEntity(string sessionKey, uint uin, float weidu, float jingdu, string deviceID, string OSType, int sex)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            LBSFindRequest.Builder lbsrb = new LBSFindRequest.Builder();
            lbsrb.SetBase(br);
            lbsrb.SetCellId("");
            lbsrb.SetMacAddr("");
            lbsrb.SetOpCode(sex);
            lbsrb.SetGPSSource(0);
            lbsrb.SetPrecision(5);
            lbsrb.SetLatitude(weidu);
            lbsrb.SetLongitude(jingdu);

            return lbsrb.Build();
        }
        public static ShakereportRequest CreateShakeReportRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, float weidu, float jingdu)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            ShakereportRequest.Builder lbsrb = new ShakereportRequest.Builder();
            lbsrb.SetBase(br);
            lbsrb.SetOpCode(0);
            lbsrb.SetLatitude(weidu);
            lbsrb.SetLongitude(jingdu);
            lbsrb.SetPrecision(5);
            lbsrb.SetCellId("");
            lbsrb.SetMacAddr("");
            lbsrb.SetImgId(0);
            lbsrb.SetTimes(4);
            lbsrb.SetGPSSource(1);

            return lbsrb.Build();
        }

        public static ShakegetRequest CreateShakeGetRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, SKBuiltinBuffer_t buffer)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            ShakegetRequest.Builder lbsrb = new ShakegetRequest.Builder();
            lbsrb.SetBase(br);
            lbsrb.SetBuffer(buffer);
            lbsrb.SetIsNewVerson(1);

            return lbsrb.Build();
        }
        /// <summary>
        /// 上传头像
         /// </summary> 
        /// <p aram name
        /// 
        /// ="sessionKey"></param>
        /// <param name="uin"></param>
        /// <param name="totalLen"></param>
        /// <param name="startPos"></param>
        /// <param name="imgBuffer"></param>
        /// <param name="deviceID"></param>
        /// <param name="OSType"></param>
        /// <returns></returns>
        public static UploadhdheadimgRequest CreateUploadhdheadimgRequestEntity(string sessionKey, uint uin, int totalLen, int startPos, byte[] imgBuffer, string deviceID, string OSType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            UploadhdheadimgRequest.Builder uhib = new UploadhdheadimgRequest.Builder();
            uhib.SetBase(br);
            uhib.SetTotalLen(totalLen);
            uhib.SetStartPos(startPos);
            uhib.SetHeadImgType(1);//1代表jpg
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(imgBuffer.Length);
            skbb.SetBuffer(ByteString.CopyFrom(imgBuffer));
            uhib.SetData(skbb.Build());


            return uhib.Build();
        }

        public static UploadMsgImgRequest CreateUploadMsgImgRequestEntity(string sessionKey, uint uin, int totalLen, int startPos, byte[] imgBuffer, string deviceID, 
            string OSType, string clientID, string fromUser, string toUser)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            UploadMsgImgRequest.Builder uhib = new UploadMsgImgRequest.Builder();
            uhib.SetBase(br);
            uhib.SetClientImgId(new SKBuiltinString_t.Builder().SetString(clientID));
            uhib.SetFromUserName(new SKBuiltinString_t.Builder().SetString(fromUser));
            uhib.SetToUserName(new SKBuiltinString_t.Builder().SetString(toUser));
            uhib.SetTotalLen(totalLen);
            uhib.SetStartPos(startPos);
            uhib.SetDataLen(imgBuffer.Length);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(imgBuffer.Length);
            skbb.SetBuffer(ByteString.CopyFrom(imgBuffer));
            uhib.SetData(skbb.Build());
            uhib.SetMsgType(3);
            uhib.SetCompressType(1);


            return uhib.Build();
        }
        /// <summary>
        /// 相册 朋友圈上传图片
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="uin"></param>
        /// <param name="totalLen"></param>
        /// <param name="startPos"></param>
        /// <param name="imgBuffer"></param>
        /// <param name="deviceID"></param>
        /// <param name="OSType"></param>
        /// <param name="clientId">随机字符串(随机md5字符)</param>
        /// <param name="Description">发送消息</param>
        /// <returns></returns>
        public static MmsnsuploadRequest CreateUploadTwitterImgRequestEntity(string sessionKey, uint uin, int totalLen, int startPos, byte[] imgBuffer, string deviceID, string OSType, string clientId, string Description)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            MmsnsuploadRequest.Builder msl = new MmsnsuploadRequest.Builder();
            msl.SetBase(br);
            msl.SetType(2);
            msl.SetStartPos(startPos);
            msl.SetTotalLen(totalLen);
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(imgBuffer.Length);
            skbb.SetBuffer(ByteString.CopyFrom(imgBuffer));
            msl.SetBuffer(skbb.Build());
            msl.ClientId = clientId; //使用随机字符 md5加密
            msl.FilterStype = 0;
            msl.SyncFlag = 0;
            msl.Description = Description;
            TwitterInfoObj.Builder tio = new TwitterInfoObj.Builder();
            tio.OauthToken = "";
            tio.OauthTokenSecret = "";
            msl.SetTwitterInfo(tio.Build());

            return msl.Build();
        }
        /// <summary>
        /// 发送朋友圈消息
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="uin"></param>
        /// <param name="deviceID"></param>
        /// <param name="OSType"></param>
        /// <param name="clientId"></param>
        /// <param name="DescriptionHtml"></param>
        /// <param name="desLen"></param>
        /// <returns></returns>
        public static MMSnsPostRequest CreateSendTwitterRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string clientId, string DescriptionHtml)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            MMSnsPostRequest.Builder mpr = new MMSnsPostRequest.Builder();
            mpr.SetBase(br);
            SKBuiltinBuffer_t.Builder sbr = new SKBuiltinBuffer_t.Builder();
            sbr.Buffer = (ByteString.CopyFromUtf8(DescriptionHtml));
            sbr.ILen = sbr.Buffer.Length;
            mpr.SetObjectDesc(sbr);
            //mpr.WithUserListCount = 0;
            //mpr.MWithUserListCount = 0;
            mpr.Privacy = 0;
            mpr.SyncFlag = 0;
            mpr.ClientId = clientId;
            mpr.PostBGImgType = 0;
            mpr.ObjectSource = 0;
            return mpr.Build();

        }
        //好友发消息
        /// <summary>
        /// 发好友消息
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="uin">用户名</param>
        /// <param name="deviceID"></param>
        /// <param name="OSType"></param>
        /// <param name="Content">消息内容</param>
        /// <param name="CreateTime">发送时间</param>
        /// <param name="ClientMsgId"></param>
        /// <param name="ToUserName">接收用户名</param>
        /// <returns></returns>
        public static NewSendMsgRequest CreateSendMsgRequestEntity(string Content, uint CreateTime, uint ClientMsgId, string ToUserName, uint msgType)
        {
            NewSendMsgRequest.Builder smr = new NewSendMsgRequest.Builder();
            smr.SetCount(1);

            NewMsgRequestBody.Builder mrb = new NewMsgRequestBody.Builder();
            mrb.SetToUserName(new SKBuiltinString_t.Builder().SetString(ToUserName));
            mrb.SetType(msgType);
            mrb.SetContent(Content);
            mrb.SetCreateTime(CreateTime);
            mrb.SetClientMsgId(ClientMsgId);
            smr.AddList(mrb);
            return smr.Build();

        }

        public static LogoutRequest CreateLogoutRequestEntity(string sessionKey, uint uin, string deviceID, string OSType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            LogoutRequest.Builder lrb = new LogoutRequest.Builder();
            lrb.SetBase(br);
            lrb.SetScene(2);

            return lrb.Build();
        }
        /// <summary>
        /// 加打招呼的人为好友
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="uin"></param>
        /// <param name="deviceID"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static VerifyUserRequest CreateVerifyUserRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string strangerUserName, 
            string ticket, int opCode, string scene, string content)
        {
            VerifyUserObj.Builder vuobj = new VerifyUserObj.Builder();
            vuobj.SetValue(strangerUserName);
            if (opCode == 3)
            {
                vuobj.SetVerifyUserTicket(ticket);
            }
            else
            {
                vuobj.SetVerifyUserTicket("");
                vuobj.SetAntiSpamTicket(ticket);
            }
            vuobj.SetFriendFlag(0);
            vuobj.SetChatRoomUserName("");
            vuobj.SetSourceNickName("");
            vuobj.SetSourceUserName(""); 

            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            VerifyUserRequest.Builder vur = new VerifyUserRequest.Builder();
            vur.SetBase(br);
            vur.SetOpcode(opCode);//3
            vur.SetVerifyUserListSize(1);
            vur.AddVerifyUserList(vuobj);
            vur.SetVerifyContent(content);
            vur.SetSceneListNumb(1);
            vur.AddSceneList(scene);
            return vur.Build();
        }


        internal static SearchContactRequest CreateSearchContactEntity(string sessionKey, uint uin, string deviceID, string OSType, string peer)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            SearchContactRequest.Builder b = new SearchContactRequest.Builder();
            b.SetBase(br);
            b.SetOpCode(0);
            SKBuiltinString_t.Builder name = new SKBuiltinString_t.Builder();
            name.SetString(peer);
            b.SetUserName(name);

            return b.Build();
        }

        internal static GeneralSetRequest CreateSetIDEntity(string sessionKey, uint uin, string deviceID, string OSType, string wxID)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);

            GeneralSetRequest.Builder lrb = new GeneralSetRequest.Builder();
            lrb.SetBase(br);
            lrb.SetSetType(1);
            lrb.SetSetValue(wxID);

            return lrb.Build();
        }

        internal static OplogRequest CreateOpSetCheckRequestEntity(int cmdid, int key, int value)
        {
            KeyValPair.Builder rb = new KeyValPair.Builder();
            rb.SetKey(key);
            rb.SetVal(value);
            byte[] byteUp = rb.Build().ToByteArray();

            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetBuffer(ByteString.CopyFrom(byteUp));
            skbb.SetILen(byteUp.Length);
            SKBuiltinBuffer_t skbObj = skbb.Build();

            CmdItem.Builder cib = new CmdItem.Builder();
            cib.SetCmdBuf(skbObj);
            cib.SetCmdId(cmdid);
            CmdItem ciObj = cib.Build();

            CmdList.Builder clb = new CmdList.Builder();
            clb.SetCount(1);
            clb.AddList(ciObj);

            CmdList clObj = clb.Build();

            OplogRequest.Builder nsrb = new OplogRequest.Builder();
            nsrb.SetOplog(clObj);

            return nsrb.Build();
        }
        internal static OplogRequest CreateOpLogRequestEntity(int cmdid, string removeObj)
        {
            RemoveFriendObject.Builder rb = new RemoveFriendObject.Builder();
            rb.SetUserName(new SKBuiltinString_t.Builder().SetString(removeObj));
            byte[] byteUp = rb.Build().ToByteArray();

            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetBuffer(ByteString.CopyFrom(byteUp));
            skbb.SetILen(byteUp.Length);
            SKBuiltinBuffer_t skbObj = skbb.Build();

            CmdItem.Builder cib = new CmdItem.Builder();
            cib.SetCmdBuf(skbObj);
            cib.SetCmdId(cmdid);
            CmdItem ciObj = cib.Build();

            CmdList.Builder clb = new CmdList.Builder();
            clb.SetCount(1);
            clb.AddList(ciObj);

            CmdList clObj = clb.Build();

            OplogRequest.Builder nsrb = new OplogRequest.Builder();
            nsrb.SetOplog(clObj);

            return nsrb.Build();
        }
        internal static OplogRequest CreateExitChatroomRequestEntity(int cmdid, string chatroom, string self)
        {
            ExitChatroomObject.Builder rb = new ExitChatroomObject.Builder();
            rb.SetChatroom(new SKBuiltinString_t.Builder().SetString(chatroom));
            rb.SetUserName(new SKBuiltinString_t.Builder().SetString(self));
            byte[] byteUp = rb.Build().ToByteArray();

            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetBuffer(ByteString.CopyFrom(byteUp));
            skbb.SetILen(byteUp.Length);
            SKBuiltinBuffer_t skbObj = skbb.Build();

            CmdItem.Builder cib = new CmdItem.Builder();
            cib.SetCmdBuf(skbObj);
            cib.SetCmdId(cmdid);
            CmdItem ciObj = cib.Build();

            CmdList.Builder clb = new CmdList.Builder();
            clb.SetCount(1);
            clb.AddList(ciObj);

            CmdList clObj = clb.Build();

            OplogRequest.Builder nsrb = new OplogRequest.Builder();
            nsrb.SetOplog(clObj);

            return nsrb.Build();
        }

        internal static CreateChatRoomRequest CreateChatroomRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, List<string> memList)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            CreateChatRoomRequest.Builder request = new CreateChatRoomRequest.Builder();
            request.SetBase(br);
            request.SetTopic(new SKBuiltinString_t.Builder().SetString(""));
            request.SetMemberCount(memList.Count);
            foreach (string item in memList)
            {
                ChatRoomItem.Builder cib = new ChatRoomItem.Builder();
                cib.SetMemberName(new SKBuiltinString_t.Builder().SetString(item));
                request.AddMembers(cib);
            }

            return request.Build();
        }

        internal static AddChatRoomMemberRequest CreateChatroomMemRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string chatroomName, List<string> memList)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            AddChatRoomMemberRequest.Builder request = new AddChatRoomMemberRequest.Builder();
            request.SetBase(br);
            request.SetMemberCount(memList.Count);
            foreach (string item in memList)
            {
                ChatRoomItem.Builder cib = new ChatRoomItem.Builder();
                cib.SetMemberName(new SKBuiltinString_t.Builder().SetString(item));
                request.AddMembers(cib);
            }
            request.SetChatRoomName(new SKBuiltinString_t.Builder().SetString(chatroomName));

            return request.Build();
        }

        internal static GetChatRoomMemberDetailRequest CreateGetChatroomMemberListRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string chatroomName)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            GetChatRoomMemberDetailRequest.Builder request = new GetChatRoomMemberDetailRequest.Builder();
            request.SetBase(br);
            request.SetChatroomUserName(chatroomName);
            request.SetClientVersion(0);

            return request.Build();
        }

        internal static Geta8keyRequest CreateGetOAuthRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, string url)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            Geta8keyRequest.Builder request = new Geta8keyRequest.Builder();
            request.SetBase(br);
            request.SetOpCode(2);
            request.SetReqUrl(new SKBuiltinString_t.Builder().SetString(url));
            request.SetScene(4);

            return request.Build();
        }


        internal static UploadvoiceRequest CreateVoiceMsgRequestEntity(string sessionKey, uint uin, string deviceID, string OSType, 
                                                                        string toUserName, string userName, byte[] voiceData, string msgID)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            UploadvoiceRequest.Builder request = new UploadvoiceRequest.Builder();
            request.SetBase(br);
            request.SetFromUserName(userName);
            request.SetToUserName(toUserName);
            request.SetOffset(0);
            request.SetLength(voiceData.Length);
            request.SetClientMsgId(msgID);
            request.SetMsgId(new Random().Next() % 100);
            request.SetVoiceLength(1011);
            SKBuiltinBuffer_t.Builder voicebuf = new SKBuiltinBuffer_t.Builder();
            voicebuf.SetILen(voiceData.Length);
            voicebuf.SetBuffer(ByteString.CopyFrom(voiceData));
            request.SetData(voicebuf);
            request.SetEndFlag(1);
            request.SetCancelFlag(0);
            request.SetMsgSource("<msgsource></msgsource>");
            request.SetVoiceFormat(1);
            request.SetUICreateTime(0);
            request.SetForwardFlag(0);

            return request.Build();
        }

        internal static GetContactRequest CreateGetContactEntity(string sessionKey, uint uin, string deviceID, string OSType, string peer)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            GetContactRequest.Builder gb = new GetContactRequest.Builder();
            gb.SetBase(br);
            gb.SetUserCount(1);
            SKBuiltinString_t.Builder sb = new SKBuiltinString_t.Builder();
            sb.SetString(peer);
            gb.AddUserNameList(sb);
            gb.SetFromChatRoomNumb(1);
            gb.AddFromChatRoom(new SKBuiltinString_t.Builder());

            return gb.Build();
        }

        internal static ExtDeviceLoginConfirmGetRequest CreateGetExtDevConfirmEntity(string reqUrl)
        {
            ExtDeviceLoginConfirmGetRequest.Builder build = new ExtDeviceLoginConfirmGetRequest.Builder();
            build.SetLoginUrl(reqUrl);
            build.SetDeviceName("Android设备");

            return build.Build();
        }

        internal static ExtDeviceLoginConfirmOKRequest CreateGetExtDevConfirmOKEntity(string reqUrl)
        {
            ExtDeviceLoginConfirmOKRequest.Builder build = new ExtDeviceLoginConfirmOKRequest.Builder();
            build.SetLoginUrl(reqUrl);
            build.SetSessionList("");
            build.SetSyncMsg(0);

            return build.Build();
        }

        internal static LogOutWebWxRequest CreateLogoutWebEntity(string sessionKey, uint uin, string deviceID, string OSType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            LogOutWebWxRequest.Builder logout = new LogOutWebWxRequest.Builder();
            logout.SetBase(br);
            logout.SetOpCode(1);

            return logout.Build();
        }

        internal static GetSafetyInfoRequest GetSafetyInfo(string sessionKey, uint uin, string deviceID, string OSType)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            GetSafetyInfoRequest.Builder b = new GetSafetyInfoRequest.Builder();
            b.SetBase(br);

            return b.Build();
        }

        internal static MmsnsuserpageRequest CreateUserPage(string sessionKey, uint uin, string deviceID, string OSType, string userName, ulong maxid)
        {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType);
            MmsnsuserpageRequest.Builder page = new MmsnsuserpageRequest.Builder();
            page.SetBase(br);
            page.SetUsername(userName);
            page.SetMaxId(maxid);
            page.SetSource(0);

            return page.Build();

        }
    }
}

