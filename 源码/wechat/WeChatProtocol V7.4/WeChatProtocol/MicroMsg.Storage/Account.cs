namespace MicroMsg.Storage
{
    using MicroMsg.Protocol;

    public class Account
    {
        
        public bool bHasAskOpenPush;
        
        public bool bIsEnabledAddrBookMatch;
        
        public int BitFlag;
        
        public bool bNotFirstInLbs;
        
        public bool bNotShowDialogInLbs;
        
        public byte[] bytesA2Key;
        
        public byte[] bytesCurSyncKey;
        
        public byte[] bytesMaxSyncKey;
        
        public byte[] bytesServerID;
        
        public byte[] bytesSessionkey;
        
        public byte[] bytesSyncKeyBuf;
        
        public double dbLastSessionKeyTimeStamp;
        
        public uint FaceBookFlag;
        
        public string FBToken;
        
        public ulong FBUserID;
        
        public string FBUserName;
        
        public string MsgPushSound;
        
        public string MyBrandList;
        
        public uint nBindQQ;
        
        public uint nGrayscaleFlag;
        
        public int nInitStatus;
        
        public uint nMainAcctType;
        
        public int nMainPageSelectedIndex;
        
        public int nNewUser;
        
        public uint nPersonalCard;
        
        public uint nPluginFlag;
        
        public uint nPluginSwitch;
        
        public int nPushMailStatus;
        
        public int nQQMBlogStatus;
        
        public uint nSafeDevice;
        
        public int nSex;
        
        public ulong nSnsBGObjectID;
        
        public uint nSnsFlag;
        
        public uint nStatus;
        
        public uint nUin;
        
        public int nVerifyFlag;
        
        public int nVersion;
        
        public uint nWeiboFlag;
        
        public string strAlias;
        
        public string strAuthTicket;
        
        public string strBigBHeadImgUrl;
        
        public string strBigHeadImgUrl;
        
        public string strBindEmail;
        
        public string strBindMobile;
        
        public string strBottleHDheadImgVersion;
        
        public string strCity;
        
        public string strCountry;
        
        public string strFSURL;
        
        public string strHDheadImgVersion;
        
        public string strNickName;
        
        public string strOfficalNickName;
        
        public string strOfficalUserName;
        
        public string strProvince;
        
        public string strPwd;
        
        public string strPwd2;
        
        public string strQQMBlogNickName;
        
        public string strQQMBlogUserName;
        
        public string strQQMicroBlog;
        
        public string strSignature;
        
        public string strSmallBHeadImgUrl;
        
        public string strSmallHeadImgUrl;
        
        public string strSnsBGImgID;
        
        public string strTicket;
        
        public string strUsrName;
        
        public string strVerifyInfo;
        
        public string strWeibo;
        
        public string strWeiboNickname;
        
        public string VoipPushSound;

        public Account()
        {
            this.FBUserName = "";
            this.FBToken = "";
            this.bytesSessionkey = new byte[0x24];
        }

        public Account(string userName) : this()
        {
            this.strUsrName = userName;
            this.nVersion = (int) ConstantsProtocol.CLIENT_MIN_VERSION;
        }
    }
}

