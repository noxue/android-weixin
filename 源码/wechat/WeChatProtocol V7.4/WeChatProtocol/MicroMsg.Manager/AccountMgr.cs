namespace MicroMsg.Manager
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System.Collections.Generic;

    public class AccountMgr
    {
        private static Account curAccount = new Account();
        public static bool hasHeadImg = false;
        private const int JPEG_QUALITY = 70;
        private static List<onAccountLoginCallback> loginNotifyList = new List<onAccountLoginCallback>();
        public static string mConstAccountInfo = "";
        //public static string mConstDeviceInfo = string.Concat(new object[] { "DeviceID = ", Util.getDeviceUniqueId(), ", Name=", DeviceStatus.get_DeviceName(), ", Manufacturer=", DeviceStatus.get_DeviceManufacturer(), ", FW=", DeviceStatus.get_DeviceFirmwareVersion(), ", Power=", DeviceStatus.get_PowerSource(), ", OS =", Environment.OSVersion.Platform, ", OSVer=", Environment.OSVersion.Version, ", HW=", DeviceStatus.get_DeviceHardwareVersion() });
        public static int mInitRetryTimes = 0;
        private const int MM_HEADIMG_MAX_HEIGHT = 0x84;
        private const int MM_HEADIMG_MAX_WIDTH = 0x84;
        public const int MODIFY_ALL = 0xfff;
        public const int MODIFY_BINDEMAIL = 8;
        public const int MODIFY_BINDMOBILE = 0x10;
        public const int MODIFY_BINDQQ = 4;
        public const int MODIFY_BOTTLEHDHEADIMGMD5SUM = 0x400;
        public const int MODIFY_BOTTLEIMG = 0x100;
        public const int MODIFY_HDHEADIMGMD5SUM = 0x200;
        public const int MODIFY_NICKNAME = 2;
        public const int MODIFY_PERSONALCARD = 0x80;
        public const int MODIFY_PHOTO = 0x40;
        public const int MODIFY_PLUGINFLAG = 0x800;
        public const int MODIFY_STATUS = 0x20;
        public const int MODIFY_USERNAME = 1;
        public static int mSyncRetryTimes = 0;
        private const uint Status_Field_BottleInChat = 0x8000;
        private const uint Status_Field_BottleMsgNotify = 0x1000;
        private const uint Status_Field_DisableAutoAddFriendWhenReply = 0x400;
        private const uint Status_Field_DisableFindMeByMobile = 0x200;
        private const uint Status_Field_DisableFindMeByQQ = 8;
        private const uint Status_Field_DisablePushMsgDetail = 0x800;
        private const uint Status_Field_DisableRecommendMeToQQFriends = 0x10;
        private const uint Status_Field_DisableRecommendMobileFirendsToMe = 0x100;
        private const uint Status_Field_DisableRecommendQQFriendsToMe = 0x80;
        private const uint Status_Field_DisableUploadMContact = 0x20000;
        private const uint Status_Field_DisplayWeixinOnLine = 0x2000;
        private const uint Status_Field_EmailVerfiy = 2;
        private const uint Status_Field_MediaNote = 0x4000;
        private const uint Status_Field_MobileVerfiy = 4;
        private const uint Status_Field_NeedVerify = 0x20;
        private const uint Status_Field_Open = 1;
        private const uint Status_Field_RecvOfflineQQMsg = 0x40;
        public const string TAG = "AccountMgr";

        public static void checkSessionKeyTimeout()
        {
        }

        // public static BitmapImage getBottletHeadimg()
        // {
        //     return HeadImgMgr.getHeadImage(curAccount.strUsrName + "@bottle", false);
        // }

        public static Account getCurAccount()
        {
            return curAccount;
        }

        public static string GetCurrentLanguage()
        {
            // string strUserSetLanguage = GConfigMgr.settings.strUserSetLanguage;
            // if ("zh-CN" == strUserSetLanguage)
            // {
                return "zh_CN";
            // }
            // if ("zh-TW" == strUserSetLanguage)
            // {
            //     return "zh_TW";
            // }
            // if ("de-DE" == strUserSetLanguage)
            // {
            //     return "de_DE";
            // }
            // return "en";
        }
//获取个人头像
        // public static BitmapImage getHeadimg()
        // {
        //     return HeadImgMgr.getHeadImage(curAccount.strUsrName, false);
        // }
//个人名片
        // public static SelfPersonCard getSelfPersonCard()
        // {
        //     return new SelfPersonCard { nPersonalCard = curAccount.nPersonalCard, nSex = curAccount.nSex, strProvince = curAccount.strProvince, strCity = curAccount.strCity, strSignature = curAccount.strSignature, strCountry = curAccount.strCountry };
        // }

        public static bool hasTWeibo()
        {
            return !string.IsNullOrEmpty(strTWeibo);
        }

        public static bool HasTWeibo()
        {
            return !string.IsNullOrEmpty(strTWeibo);
        }

        public static bool hasValidAccount()
        {
            if (curAccount == null)
            {
                return false;
            }
            if ((curAccount.strUsrName == null) || (curAccount.strUsrName.Length <= 0))
            {
                return false;
            }
            return ((curAccount.strPwd != null) && (curAccount.strPwd.Length > 0));
        }
//手机是否验证
        // public static bool HasVerifiedBindedPhone()
        // {
        //     return ((!string.IsNullOrWhiteSpace(strBindMobile) && (strBindMobile.Length > 3)) && MobileVerify);
        // }
//初始化
        public static void init()
        {
           // isLogin = false;
            //if (!string.IsNullOrEmpty(GConfigMgr.settings.strUsrName))
           // {
                loadAccount(null);
                loginStartup();
            //}
        }

        public static bool isAccountOwner(string userName)
        {
            return (strUsrName == userName);
        }

        public static bool IsAcountInfoCompleted()
        {
            Account account = getCurAccount();
            if (account == null)
            {
                return false;
            }
            return (((!string.IsNullOrEmpty(account.strProvince) || !string.IsNullOrEmpty(account.strCity)) || !string.IsNullOrEmpty(account.strCountry)) && ((account.nSex == 1) || (account.nSex == 2)));
        }

        private static bool isSessionkeyValid(byte[] Sessionkey)
        {
            if ((Sessionkey != null) && (Sessionkey.Length > 0))
            {
                byte[] buffer = Sessionkey;
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void loadAccount(string usrName)
        {
            // Account account = StorageMgr.account.get(usrName);
            // if (account == null)
            // {
                curAccount = new Account(usrName);
                Log.d("AccountMgr", "load no exist new account " + usrName);
           // }
            // else
            // {
            //     Log.d("AccountMgr", "load exist account " + usrName);
            //     curAccount = account;
            // }
        }

        private static void loginNotify(string usrName)
        {
            foreach (onAccountLoginCallback callback in loginNotifyList)
            {
                callback();
            }
        }

        public static void loginStartup()
        {
            if (hasValidAccount())
            {
                //CrashLogMgr.onLogin(curAccount.strUsrName);
                //StorageMgr.onLogin(curAccount.strUsrName);
                isLogin = true;
                //hasHeadImg = HeadImageMgr.exists(curAccount.strUsrName, true);
                SessionPackMgr.getAccount().setAuthInfo(curAccount.strUsrName, curAccount.strPwd, curAccount.strPwd2);
                if (isSessionkeyValid(curAccount.bytesSessionkey))
                {
                    SessionPackMgr.setSessionKey(curAccount.bytesSessionkey);
                    checkSessionKeyTimeout();
                }
                SessionPackMgr.getAccount().setUin((int) curAccount.nUin);
                mConstAccountInfo = string.Concat(new object[] { "User = ", curAccount.strUsrName, ", UIN =", curAccount.nUin, " ,Nick=", curAccount.strNickName });
                //FlowControl.initialize();
                NetSceneNewInit.onLoginHandler();
                NetSceneNewSync.onLoginHandler();
                //Deployment.get_Current().get_Dispatcher().BeginInvoke(() => 
                    EventCenter.postEvent(EventConst.ON_ACCOUNT_LOGIN, curAccount.strUsrName, null);
                Log.d("AccountMgr", "#### loginStartup " + curAccount.strUsrName);
            }
        }

        public static void loginStartupNotify()
        {
            if (isLogin)
            {
                loginNotify(strUsrName);
            }
        }

        public static void logout()
        {
            Log.d("AccountMgr", "#### logout");
            if (isLogin)
            {
                isLogin = false;
                //ExtentCenter.uninitialize();
                curAccount.strPwd = null;
                //StorageMgr.account.update(curAccount);
                //QFriendMgr.onLogout();
               // Sender.getInstance().logoutAccount();
                mConstAccountInfo = "";
                logoutNotify();
                //StorageMgr.onLogout();
            }
        }

        private static void logoutNotify()
        {
            EventCenter.sendEvent(EventConst.ON_ACCOUNT_LOGOUT, null);
        }

        private static bool modifyPluginSwitch(uint bit, bool bSet)
        {
            uint nPluginSwitch = curAccount.nPluginSwitch;
            if (bSet)
            {
                curAccount.nPluginSwitch |= bit;
            }
            else
            {
                curAccount.nPluginSwitch &= ~bit;
            }
            return (nPluginSwitch != curAccount.nPluginSwitch);
        }

        private static bool modifyStatus(uint bit, bool bSet)
        {
            uint nStatus = curAccount.nStatus;
            if (bSet)
            {
                curAccount.nStatus |= bit;
            }
            else
            {
                curAccount.nStatus &= ~bit;
            }
            return (nStatus != curAccount.nStatus);
        }

        public static void onLogin(string userName)
        {
            //CrashLogMgr.onLogin(userName);
            //StorageMgr.onLogin(userName);
            loadAccount(userName);
            //GConfigMgr.settings.strUsrName = userName;
            //GConfigMgr.saveSetting();
            isLogin = true;
            //hasHeadImg = HeadImageMgr.exists(userName, true);
            mConstAccountInfo = string.Concat(new object[] { "User = ", curAccount.strUsrName, ", UIN =", curAccount.nUin, " ,Nick=", curAccount.strNickName });
            //FlowControl.initialize();
            //NetSceneNewInit.onLoginHandler();
            //NetSceneNewSync.onLoginHandler();
            loginNotify(userName);
            EventCenter.postEvent(EventConst.ON_ACCOUNT_LOGIN, curAccount.strUsrName, null);
            Log.d("AccountMgr", "#### login " + userName);
        }

        public static void registerLoginNotify(onAccountLoginCallback listener)
        {
            if (!loginNotifyList.Contains(listener))
            {
                loginNotifyList.Add(listener);
            }
        }

        // public static void saveBottleHeadImg(Stream fileStream)
        // {
        //     HeadImageMgr.save(strUsrName + "@bottle", fileStream);
        // }

        // public static void saveHeadImg(Stream fileStream)
        // {
        //     HeadImageMgr.save(strUsrName, fileStream);
        // }

        public static void saveSyncInfoAsync(SyncInfo info, bool isLocalSync = false)
        {
            if (info != null)
            {
                //ConversationMgr.SetSaveCompleteHandler(delegate {
                    if (info.bSetCurSyncKey)
                    {
                        curAccount.bytesCurSyncKey = info.bytesCurSyncKey;
                    }
                    if (info.bSetMaxSyncKey)
                    {
                        curAccount.bytesMaxSyncKey = info.bytesMaxSyncKey;
                    }
                    if (info.bSetInitStatus)
                    {
                        curAccount.nInitStatus = info.nInitStatus;
                    }
                    if (info.bSetSyncKey)
                    {
                        curAccount.bytesSyncKeyBuf = info.bytesSyncKeyBuf;
                    }
                    updateAccount();
                    if (isLocalSync)
                    {
                        //AccountMgr.delBackgroundData();
                    }
                    //Log.d("AccountMgr", "ConversationMgr save  sync Key finish!");
                    EventCenter.postEvent(EventConst.ON_CONVERSATION_POST_SYCN_DATA,null , null);
               // });
            }
        }

        public static void SetLoginStatus(bool bLogin)
        {
            isLogin = bLogin;
        }

        public static void SetOpenSafeDevice(bool bNewValue)
        {
            openSafeDevice = bNewValue;
            ServiceCenter.sceneNewSync.doScene(7, syncScene.MM_NEWSYNC_SCENE_OTHER);
        }

        public static void setSelfPersonCard(SelfPersonCard card)
        {
            curAccount.nPersonalCard = 1;
            curAccount.nSex = card.nSex;
            curAccount.strProvince = card.strProvince;
            curAccount.strCity = card.strCity;
            curAccount.strSignature = card.strSignature;
            curAccount.strCountry = card.strCountry;
            //OpLogMgr.opModUserInfo(0x80, curAccount);
            updateAccount();
        }

        public static void uninit()
        {
        }

        public static bool updateAccount()
        {
            mConstAccountInfo = string.Concat(new object[] { "User = ", curAccount.strUsrName, ", UIN =", curAccount.nUin, " ,Nick=", curAccount.strNickName });
               //  Log.i("AccountMgr", mConstAccountInfo);
           // EventCenter.postEvent(EventConst.ON_STORAGE_ACCOUNT_UPDATED, null, null);
            return true;//StorageMgr.account.update(curAccount);
        }

        public static bool UserHasSetHeadImg()
        {
            // if (hasHeadImg)
            // {
            //     Log.d("AccountMgr", "UserHasSetHeadImg, AccountMgr.hasHeadImg == TRUE");
            //     return true;
            // }
            Account account = getCurAccount();
            if (account == null)
            {
                return false;
            }
            // if (string.IsNullOrEmpty(account.strSmallHeadImgUrl) && string.IsNullOrEmpty(account.strBigHeadImgUrl))
            // {
            //     return false;
            // }
            return true;
        }

        public static bool BottleMsgNotify
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x1000));
            }
            set
            {
                if (modifyStatus(0x1000, value))
                {
                    ////OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.BottleMsgNotify, value);
                    updateAccount();
                }
            }
        }

        public static string curUserName
        {
            get
            {
                if (curAccount.strUsrName == null)
                {
                    return "";
                }
                return curAccount.strUsrName;
            }
        }

        public static bool DisableAutoAddFriendWhenReply
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x400));
            }
            set
            {
                if (modifyStatus(0x400, value))
                {
                    ////OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableAutoAddFriendWhenReply, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableFindMeByID
        {
            get
            {
                return (0 != (curAccount.nPluginSwitch & 0x200));
            }
            set
            {
                if (modifyPluginSwitch(0x200, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.MM_FUNCTIONSWITCH_USERNAME_SEARCH_CLOSE, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableFindMeByMobile
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x200));
            }
            set
            {
                if (modifyStatus(0x200, value))
                {
                    ////OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableFindMeByMobile, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableFindMeByQQ
        {
            get
            {
                return (0 != (curAccount.nStatus & 8));
            }
            set
            {
                if (modifyStatus(8, value))
                {
                    ////OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableFindMeByQQ, value);
                    updateAccount();
                }
            }
        }

        public static bool DisablePushMsgDetail
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x800));
            }
            set
            {
                if (modifyStatus(0x800, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisablePushMsgDetail, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableRecommendMeToQQFriends
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x10));
            }
            set
            {
                if (modifyStatus(0x10, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableRecommendMeToQQFriends, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableRecommendMobileFirendsToMe
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x100));
            }
            set
            {
                if (modifyStatus(0x100, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableRecommendMobileFirendsToMe, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableRecommendQQFriendsToMe
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x80));
            }
            set
            {
                if (modifyStatus(0x80, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableRecommendQQFriendsToMe, value);
                    updateAccount();
                }
            }
        }

        public static bool disableRecommendTXNewsToMe
        {
            get
            {
                return (0 != (curAccount.nPluginSwitch & 0x400));
            }
            set
            {
                if (modifyPluginSwitch(0x400, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.MM_FUNCTIONSWITCH_NEWSAPP_TXNEWS_CLOSE, value);
                    updateAccount();
                }
            }
        }

        public static bool DisableUploadMContact
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x20000));
            }
            set
            {
                if (modifyStatus(0x20000, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisableUploadMContact, value);
                    updateAccount();
                }
            }
        }

        public static bool DisplayWeixinOnLine
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x2000));
            }
            set
            {
                if (modifyStatus(0x2000, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.DisplayWeixinOnLine, value);
                    updateAccount();
                }
            }
        }

        public static bool EmailVerify
        {
            get
            {
                return (0 != (curAccount.nStatus & 2));
            }
        }
        private static bool _isLogin;
        public static bool isLogin
        {
            
            get
            {
                return _isLogin;
            }
            
            private set
            {
                _isLogin = value;
            }
        }

        public static bool MediaNote
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x4000));
            }
            set
            {
                if (modifyStatus(0x4000, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.MediaNote, value);
                    updateAccount();
                }
            }
        }

        public static bool MobileVerify
        {
            get
            {
                return (0 != (curAccount.nStatus & 4));
            }
            set
            {
                modifyStatus(4, value);
            }
        }

        public static uint nBindQQ
        {
            get
            {
                return curAccount.nBindQQ;
            }
            set
            {
                if (curAccount.nBindQQ != value)
                {
                    curAccount.nBindQQ = value;
                    //OpLogMgr.opModUserInfo(4, curAccount);
                    updateAccount();
                }
            }
        }

        public static bool NeedVerify
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x20));
            }
            set
            {
                if (modifyStatus(0x20, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.NeedVerify, value);
                    updateAccount();
                }
            }
        }

        public static uint nStatus
        {
            get
            {
                return curAccount.nStatus;
            }
            set
            {
                if (curAccount.nStatus != value)
                {
                    curAccount.nStatus = value;
                    //OpLogMgr.opModUserInfo(0x20, curAccount);
                    updateAccount();
                }
            }
        }

        public static bool openSafeDevice
        {
            get
            {
                return (0 != (curAccount.nPluginSwitch & 0x4000));
            }
            set
            {
                if (modifyPluginSwitch(0x4000, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.MM_FUNCTIONSWITCH_SAFEDEVICE_OPEN, value);
                    updateAccount();
                }
            }
        }

        public static bool QQMailPushNotify
        {
            get
            {
                return (curAccount.nPushMailStatus == 1);
            }
            set
            {
                int num = value ? 1 : 0;
                if (curAccount.nPushMailStatus != num)
                {
                    curAccount.nPushMailStatus = num;
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.QQMailPushNotify, value);
                    updateAccount();
                }
            }
        }

        public static bool RecommendFacebookToMe
        {
            get
            {
                return (0 != (curAccount.nPluginSwitch & 4));
            }
            set
            {
                if (modifyPluginSwitch(4, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.RECFBFriend, value);
                    updateAccount();
                }
            }
        }

        public static bool RecvOfflineQQMsg
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x40));
            }
            set
            {
                if (modifyStatus(0x40, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.RecvOfflineQQMsg, value);
                    updateAccount();
                }
            }
        }

        public static bool ShowBottleInChat
        {
            get
            {
                return (0 != (curAccount.nStatus & 0x8000));
            }
            set
            {
                if (modifyStatus(0x8000, value))
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.BottleInChat, value);
                    updateAccount();
                }
            }
        }

        public static bool ShowWeibo
        {
            get
            {
                return ((curAccount.nWeiboFlag & 1) != 0);
            }
            set
            {
                uint nWeiboFlag = curAccount.nWeiboFlag;
                if (value)
                {
                    curAccount.nWeiboFlag |= 1;
                }
                else
                {
                    curAccount.nWeiboFlag &= 0xfffffffe;
                }
                if (nWeiboFlag != curAccount.nStatus)
                {
                    //OpLogMgr.opFunctionSwitch(OpLogMgr.FunctionID.TXWeibo, value);
                    updateAccount();
                }
            }
        }

        public static string strBindEmail
        {
            get
            {
                return curAccount.strBindEmail;
            }
            set
            {
                if (curAccount.strBindEmail != value)
                {
                    curAccount.strBindEmail = value;
                    //OpLogMgr.opModUserInfo(8, curAccount);
                    updateAccount();
                }
            }
        }

        public static string strBindMobile
        {
            get
            {
                return curAccount.strBindMobile;
            }
            set
            {
                if (curAccount.strBindMobile != value)
                {
                    curAccount.strBindMobile = value;
                    //OpLogMgr.opModUserInfo(0x10, curAccount);
                    updateAccount();
                }
            }
        }

        public static string strNickName
        {
            get
            {
                return curAccount.strNickName;
            }
            set
            {
                if (curAccount.strNickName != value)
                {
                    curAccount.strNickName = value;
                    //OpLogMgr.opModUserInfo(2, curAccount);
                    updateAccount();
                }
            }
        }

        public static string strTWeibo
        {
            get
            {
                if (curAccount.strWeibo == null)
                {
                    return "";
                }
                return curAccount.strWeibo;
            }
        }

        public static string strTWeiboNickName
        {
            get
            {
                if (curAccount.strWeiboNickname == null)
                {
                    return "";
                }
                return curAccount.strWeiboNickname;
            }
        }

        public static string strUsrName
        {
            get
            {
                return curAccount.strUsrName;
            }
        }

        public enum PluginSwitch : uint
        {
            MM_STATUS_GMAIL_OPEN = 1,
            MM_STATUS_MASSSEND_SHOW_IN_CHATLIST_OPEN = 0x80,
            MM_STATUS_MEISHI_CARD_OPEN = 0x100,
            MM_STATUS_NEWSAPP_TXNEWS_CLOSE = 0x400,
            MM_STATUS_PRIVATEMSG_RECV_CLOSE = 0x800,
            MM_STATUS_PUSHMAIL_NOTIFY_CLOSE = 0x1000,
            MM_STATUS_READAPP_PUSH_OPEN = 8,
            MM_STATUS_READAPP_TXNEWS_OPEN = 0x10,
            MM_STATUS_READAPP_WB_OPEN = 0x20,
            MM_STATUS_RECFBFRIEND_OPEN = 4,
            MM_STATUS_SAFEDEVICE_OPEN = 0x4000,
            MM_STATUS_TXWEIBO_ICON_OPEN = 0x40,
            MM_STATUS_TXWEIBO_OPEN = 2,
            MM_STATUS_USERNAME_SEARCH_CLOSE = 0x200,
            MM_STATUS_WEBONLINE_PUSH_OPEN = 0x2000
        }
    }
}

