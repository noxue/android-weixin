using Google.ProtocolBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mm.command;

namespace aliyun
{
    public class GoogleProto
    {
        private const int VERSION = 0x29998c32;
        private const Int32 ver = 369593792;

        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long GetCurTime()
        {
            return (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds);
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
            bytSD = Encoding.Default.GetBytes(deviceID);
            brb.SetDeviceID(ByteString.CopyFrom(bytSD));
            brb.SetClientVersion(ver);
            brb.SetDeviceType(ByteString.CopyFrom(osType, Encoding.Default));
            brb.SetScene(scene);
            BaseRequest br = brb.Build();

            return br;
        }

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
            aqb.SetLanguage("zh_CN");
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
            
            aqb.SetSignature("18c867f0717aa67b2ab7347505ba07ed");
            SKBuiltinBuffer_t.Builder skbb = new SKBuiltinBuffer_t.Builder();
            skbb.SetILen(16);

            skbb.SetBuffer(ByteString.CopyFrom(randomEncryKey));
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
            aqb.SetDeviceModel(MODEL + abi);
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



        public static AuthRequest CreateAuthRequestEntity(BaseRequest br, string wxAccount, string wxPwd, string imei, string deviceBrand, string deviceModel, byte[] randomEncryKey, string imgSID, string code, string imgKey)
        {
            AuthRequest ar = null; 

            return ar;
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

        public static NewSyncRequest CreateNewSyncRequestEntity(byte[] keyBuffer, string deviceID)
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
            nsrb.SetDeviceType(deviceID);
            nsrb.SetSyncMsgDigest(1);

            NewSyncRequest nsr = nsrb.Build();

            return nsr;
        }

        public static NewRegRequest CreateNewRegReqest() {
            NewRegRequest.Builder reg = new NewRegRequest.Builder();
            //reg.set

            return reg.Build();
        }

        public static MMSnsSyncRequest CreateMMSnsSyncRequest(string deviceID,string sessionKey,uint uin,string OSType, byte[] keyBuffer) {
            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType, 3);
            MMSnsSyncRequest.Builder SnsSync = new MMSnsSyncRequest.Builder();
            SnsSync.SetSelector(509);
            SnsSync.SetBase(br);
            SKBuiltinBuffer_t.Builder skb = new SKBuiltinBuffer_t.Builder();
            skb.SetBuffer(ByteString.CopyFrom(keyBuffer));
            skb.SetILen(keyBuffer.Length);

            SnsSync.SetKeyBuf(skb);

            MMSnsSyncRequest MMsnsyn = SnsSync.Build();
            return MMsnsyn;
        }

        //public static MMSnsPostResponse CreateMMSnsPostResponse(string deviceID, string sessionKey, uint uin, string OSType, byte[] keyBuffer) {
        //    BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType, 3);
        //    MMSnsPostRequest.Builder builder = new MMSnsPostRequest.Builder();


        //    mm.command.NewInitResponse niReceive = mm.command.NewInitResponse.ParseFrom("0A570A105D64797E40587E3653492B3770767C6D10E5DA8D81031A20353332363435314632303045304431333043453441453237323632423631363920A28498B0012A1369506164206950686F6E65204F53392E332E33300012810808FB0712FB073C54696D656C696E654F626A6563743E3C69643E31323534323132393139333538343234323934373C2F69643E3C757365726E616D653E777869645F6B727862626D68316A75646533313C2F757365726E616D653E3C63726561746554696D653E313439353133383331303C2F63726561746554696D653E3C636F6E74656E74446573633EE2809CE7BEA1E68595E982A3E4BA9BE4B880E6B2BEE79D80E69E95E5A4B4E5B0B1E883BDE5AE89E79DA1E79A84E4BABAE5928CE982A3E4BA9BE586B3E5BF83E694BEE6898BE4B98BE5908EE5B0B1E4B88DE5868DE59B9EE5A4B4E79A84E4BABAE2809D3C2F636F6E74656E74446573633E3C636F6E74656E744465736353686F77547970653E303C2F636F6E74656E744465736353686F77547970653E3C636F6E74656E74446573635363656E653E333C2F636F6E74656E74446573635363656E653E3C707269766174653E303C2F707269766174653E3C7369676874466F6C6465643E303C2F7369676874466F6C6465643E3C617070496E666F3E3C69643E3C2F69643E3C76657273696F6E3E3C2F76657273696F6E3E3C6170704E616D653E3C2F6170704E616D653E3C696E7374616C6C55726C3E3C2F696E7374616C6C55726C3E3C66726F6D55726C3E3C2F66726F6D55726C3E3C6973466F7263655570646174653E303C2F6973466F7263655570646174653E3C2F617070496E666F3E3C736F75726365557365724E616D653E3C2F736F75726365557365724E616D653E3C736F757263654E69636B4E616D653E3C2F736F757263654E69636B4E616D653E3C73746174697374696373446174613E3C2F73746174697374696373446174613E3C737461744578745374723E3C2F737461744578745374723E3C436F6E74656E744F626A6563743E3C636F6E74656E745374796C653E323C2F636F6E74656E745374796C653E3C7469746C653E3C2F7469746C653E3C6465736372697074696F6E3E3C2F6465736372697074696F6E3E3C6D656469614C6973743E3C2F6D656469614C6973743E3C636F6E74656E7455726C3E3C2F636F6E74656E7455726C3E3C2F436F6E74656E744F626A6563743E3C616374696F6E496E666F3E3C6170704D73673E3C6D657373616765416374696F6E3E3C2F6D657373616765416374696F6E3E3C2F6170704D73673E3C2F616374696F6E496E666F3E3C6C6F636174696F6E20636974793D5C225C2220706F69436C61737369667949643D5C225C2220706F694E616D653D5C225C2220706F69416464726573733D5C225C2220706F69436C617373696679547970653D5C22305C223E3C2F6C6F636174696F6E3E3C7075626C6963557365724E616D653E3C2F7075626C6963557365724E616D653E3C2F54696D656C696E654F626A6563743E0D0A1800280030003A13736E735F706F73745F313533343933333731384001580068008001009A010A0A0012001A0020002800AA010408001200C00100".ToByteArray(16,2));

        //    niReceive.
        //}

        public static ShakereportRequest shakereport(float Latitude, float Longitude,string deviceID, string sessionKey, uint uin, string OSType) {

            BaseRequest br = CreateBaseRequestEntity(deviceID, sessionKey, uin, OSType, 0);

            ShakereportRequest.Builder shakereport = new ShakereportRequest.Builder();
            shakereport.SetBase(br);
            shakereport.SetOpCode(0);
            shakereport.SetLongitude(Longitude);
            shakereport.SetLatitude(Latitude);
            shakereport.SetImgId(0);
            shakereport.SetTimes(1);
            shakereport.SetPrecision(0);
            ShakereportRequest request = shakereport.Build();

            return request;
        }
    }
}

