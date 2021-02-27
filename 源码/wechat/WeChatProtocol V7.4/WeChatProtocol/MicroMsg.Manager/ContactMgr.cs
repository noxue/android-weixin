namespace MicroMsg.Manager
{
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    //using MicroMsg.UI.Page;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;

    public class ContactMgr
    {
        private static Dictionary<string, string> mapHeadImgUrl = new Dictionary<string, string>();
        public const int MM_CONTACT_BOTTLE = 5;
        public const int MM_CONTACT_CHATROOM = 2;
        public const int MM_CONTACT_EMAIL = 3;
        public const int MM_CONTACT_QQ = 4;
        public const int MM_CONTACT_QQMICROBLOG = 1;
        public const int MM_CONTACT_WEIXIN = 0;
        public const uint MM_CONTACTFLAG_3RDAPPCONTACT = 0x80;
        public const uint MM_CONTACTFLAG_ALL = 0x1ff;
        public const uint MM_CONTACTFLAG_BLACKLISTCONTACT = 8;
        public const uint MM_CONTACTFLAG_CHATCONTACT = 2;
        public const uint MM_CONTACTFLAG_CHATROOMCONTACT = 4;
        public const uint MM_CONTACTFLAG_CONTACT = 1;
        public const uint MM_CONTACTFLAG_DOMAINCONTACT = 0x10;
        public const uint MM_CONTACTFLAG_FAVOURCONTACT = 0x40;
        public const uint MM_CONTACTFLAG_HIDECONTACT = 0x20;
        public const int MM_CONTACTFLAG_NULL = 0;
        public const uint MM_CONTACTFLAG_SNSBLACKLISTCONTACT = 0x100;
        public const int MM_CONTACTIMGFLAG_HAS_HEADIMG = 3;
        public const int MM_CONTACTIMGFLAG_HAS_NO_HEADIMG = 4;
        public const int MM_CONTACTIMGFLAG_LOCAL_EXIST = 0x99;
        public const int MM_CONTACTIMGFLAG_MODIFY = 2;
        public const int MM_CONTACTIMGFLAG_NOTMODIFY = 1;
        public const int MM_DELFLAG_DEL = 2;
        public const int MM_DELFLAG_EXIST = 1;
        public const int MM_DOMAINEMAIL_NOTVERIFY = 1;
        public const int MM_DOMAINEMAIL_VERIFIED = 2;
        public const int MM_MICROBLOG_QQ = 1;
        public const int MM_NOTIFY_CLOSE = 0;
        public const int MM_NOTIFY_OPEN = 1;
        public const int MMBRAND_SUBSCRIPTION_BLOCK_MESSAGE_NOTIFY = 1;
        public const int MMBRAND_SUBSCRIPTION_HIDE_FROM_PROFILE = 2;
        private static EventWatcher mWatcherContact;
        private static EventWatcher mWatcherLogin;
        public const string TAG = "ContactMgr";

        static ContactMgr()
        {
            EventCenter.registerEventHandler(EventConst.ON_ACCOUNT_LOGIN, new EventHandlerDelegate(ContactMgr.onLogin));
            EventCenter.registerEventHandler(EventConst.ON_NETSCENE_BATCH_GET_CONTACT_SUCCESS, new EventHandlerDelegate(ContactMgr.onContactRefresh));
        }

        //public static Contact GetAccountContact()
        //{
        //    Contact contact = StorageMgr.contact.get(AccountMgr.strUsrName);
        //    if (contact == null)
        //    {
        //        return GetContactFromAccount(true);
        //    }
        //    return contact;
        //}

        //public static ListBrandLinks GetBrandCustomLinks(string strJson)
        //{
        //    return (ListBrandLinks) Util.GetObjectFromJson(strJson, typeof(ListBrandLinks));
        //}

        //public static BrandCustomSetting GetBrandCustomSetting(string strJson)
        //{
        //    return (BrandCustomSetting) Util.GetObjectFromJson(strJson, typeof(BrandCustomSetting));
        //}

        public static int getCheckSum(Contact con)
        {
            return Util.CheckSum(string.Concat(new object[] { con.strUsrName, con.strNickName, con.strSignature, con.strCity, con.strProvince, con.nSex, con.nFlags, con.nSnsFlag, con.strMyBrandList, con.strSnsBGImgID, con.strAlias, con.strMobile, con.nWeiboFlag, con.strWeibo, con.strWeiboNickname }));
        }

        //public static Contact GetContactFromAccount(bool bIsSyncToStorage = false)
        //{
        //    Account account = AccountMgr.getCurAccount();
        //    Contact con = StorageMgr.contact.get(AccountMgr.strUsrName);
        //    int num = 0;
        //    if (con == null)
        //    {
        //        con = new Contact();
        //        bIsSyncToStorage = true;
        //    }
        //    else
        //    {
        //        num = getCheckSum(con);
        //    }
        //    con.strUsrName = account.strUsrName;
        //    con.strNickName = account.strNickName;
        //    con.strSignature = account.strSignature;
        //    con.strCity = account.strCity;
        //    con.strProvince = account.strProvince;
        //    con.nSex = account.nSex;
        //    con.nFlags = 7;
        //    con.nSnsFlag = account.nSnsFlag;
        //    con.strMyBrandList = account.MyBrandList;
        //    con.strSnsBGImgID = account.strSnsBGImgID;
        //    con.strAlias = account.strAlias;
        //    con.strMobile = account.strBindMobile;
        //    con.nWeiboFlag = account.nWeiboFlag;
        //    con.strWeibo = account.strWeibo;
        //    con.strWeiboNickname = account.strWeiboNickname;
        //    if ((num != getCheckSum(con)) && bIsSyncToStorage)
        //    {
        //        StorageMgr.contact.update(con);
        //    }
        //    return con;
        //}

        //public ObservableCollection<Contact> getList()
        //{
        //    List<Contact> list = StorageMgr.contact.getListWithOrder();
        //    if (list != null)
        //    {
        //        return new ObservableCollection<Contact>(list);
        //    }
        //    return null;
        //}

        public static UserType getUserType(string userName)
        {
            if ((userName == null) || (userName.Length <= 0))
            {
                return UserType.UserTypeInvaild;
            }
            if (userName.EndsWith("@chatroom"))
            {
                return UserType.UserTypeChatRoom;
            }
            if (userName.EndsWith("@groupcard"))
            {
                return UserType.UserTypeGroupCard;
            }
            if (userName.EndsWith("@micromsg.qq.com"))
            {
                return UserType.UserTypeWeiXin;
            }
            if (userName.EndsWith("@t.qq.com"))
            {
                return UserType.UserTypeTencent;
            }
            if (userName.EndsWith("@t.sina.com"))
            {
                return UserType.UserTypeSina;
            }
            if (userName.EndsWith("@qqim"))
            {
                return UserType.UserTypeQQ;
            }
            if (userName.Contains("@bottle"))
            {
                return UserType.UserTypeBottle;
            }
            if (ContactHelper.systemPluginName.Contains(userName))
            {
                return UserType.UserTypePlugin;
            }
            return UserType.UserTypeNormal;
        }

        public static bool HasAblum(uint nSnsFlag)
        {
            return ((nSnsFlag & 1) != 0);
        }

        public static void init()
        {
            mWatcherContact = new EventWatcher(null, null, new EventHandlerDelegate(ContactMgr.onContactModify));
            EventCenter.registerEventWatcher(EventConst.ON_STORAGE_CONTACT_MODIFY, mWatcherContact);
            mWatcherLogin = new EventWatcher(null, null, new EventHandlerDelegate(ContactMgr.onAccountLogin));
            EventCenter.registerEventWatcher(EventConst.ON_ACCOUNT_LOGIN, mWatcherLogin);
        }

        public static bool isAccount(Contact ct)
        {
            return ((ct != null) && (ct.strUsrName == AccountMgr.strUsrName));
        }

        public static bool isBlackList(Contact ct)
        {
            return ((ct.nFlags & 8) > 0);
        }

        public static bool isChatContact(Contact ct)
        {
            return ((ct.nFlags & 2) > 0);
        }

        public static bool isChatroomContact(Contact ct)
        {
            return ((ct.nFlags & 4) > 0);
        }

        public static bool isContact(Contact ct)
        {
            return ((ct.nFlags & 1) > 0);
        }

        public static bool isDomainContact(Contact ct)
        {
            return ((ct.nFlags & 0x10) > 0);
        }

        public static bool isFamous(int VerifyFlag)
        {
            return (VerifyFlag != 0);
        }

        public static bool isFavourContact(Contact ct)
        {
            return ((ct.nFlags & 0x40) > 0);
        }

        public static bool IsFmessageUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            return (userName == "fmessage");
        }

        public static bool isHide(Contact ct)
        {
            return ((ct.nFlags & 0x20) > 0);
        }

        public static bool IsReceiveMsg(Contact ct)
        {
            if (ct == null)
            {
                return false;
            }
            return IsReceiveMsg(ct.nBrandFlag);
        }

        public static bool IsReceiveMsg(uint nFlag)
        {
            return ((~nFlag & 1) > 0);
        }

        public static bool IsShowInMyProfile(Contact ct)
        {
            if (ct == null)
            {
                return false;
            }
            return IsShowInMyProfile(ct.nBrandFlag);
        }

        public static bool IsShowInMyProfile(uint nFlag)
        {
            return ((~nFlag & 2) > 0);
        }

        public static bool isTWeiboShown(Contact ct)
        {
            if (ct == null)
            {
                return false;
            }
            return ((ct.nWeiboFlag & 1) == 1);
        }

        private static void onAccountLogin(EventWatcher watcher, BaseEventArgs evtArgs)
        {
        //    RemarkProfileMgr.clearRemark();
            Log.e("ConntactMgr", "onAccountLogin");
        }

        private static void onContactModify(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            //onContactModify
                 Log.e("ConntactMgr", "onContactModify");
        //    if (ContactDetailViewForAll.isEnableAddMobileBookMatch())
        //    {
        //        RemarkDic remarkDic = RemarkProfileMgr.loadRemarkProfile();
        //        if (((remarkDic != null) || (remarkDic.thisDic != null)) || (remarkDic.thisDic.Count != 0))
        //        {
        //            List<Contact> list = evtArgs.getListObject<Contact>();
        //            if ((list != null) || (list.Count != 0))
        //            {
        //                bool flag = false;
        //                foreach (Contact contact in list)
        //                {
        //                    if (isContact(contact) && remarkDic.findUsrName(contact.strUsrName))
        //                    {
        //                        string strToConvert = remarkDic.getRemark(contact.strUsrName);
        //                        contact.strRemark = strToConvert;
        //                        contact.strRemarkPYInitial = SortStringsByAlpha.ConvertStringToPinyinInitial(strToConvert);
        //                        contact.strRemarkQuanPin = GetPinYin.ConvertStrToQuanPin(strToConvert);
        //                        using ((StorageEventLock) (StorageMgr.contact.eventLock = 1))
        //                        {
        //                            operationModContact(contact);
        //                        }
        //                        remarkDic.delDic(contact.strUsrName);
        //                        ContactDetailViewForAll.refreshSwitch = 2;
        //                        flag = true;
        //                    }
        //                }
        //                if (flag)
        //                {
        //                    RemarkProfileMgr.saveRemarkProfile(remarkDic);
        //                }
        //            }
        //        }
        //    }
        }

        private static void onContactRefresh(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            if (((evtArgs != null) && (evtArgs.mObject != null)) && (mapHeadImgUrl.Count > 0))
            {
                List<string> mObject = evtArgs.mObject as List<string>;
                if (((mObject != null) && (mObject.Count == 1)) && mapHeadImgUrl.ContainsKey(mObject[0]))
                {
                    string key = mObject[0];
                    //Contact contact = StorageMgr.contact.get(key);
                    //string str2 = mapHeadImgUrl[key];
                    //mapHeadImgUrl.Remove(key);
                    //if (str2 != contact.strBrandIconURL)
                    //{
                    //   // HeadImageMgr.update(key);
                    //}
                }
            }
        }

        private static void onLogin(EventWatcher watcher, BaseEventArgs evtArgs)
        {
            mapHeadImgUrl.Clear();
        }

        public static bool operationModBrandSetting(Contact ct, uint BrandFlag)
        {
            if (!OpLogMgr.OpModBrandSetting(ct.strUsrName, BrandFlag))
            {
                Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
                return false;
            }
            ct.nBrandFlag = BrandFlag;
           // return StorageMgr.contact.modify(ct);
 Log.e("ContactMgr", "true");
            return true;
        }

        //public static bool operationModContact(Contact ct)
        //{
        //    if (!OpLogMgr.OpModContact(ct, 0x1ff))
        //    {
        //        Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
        //        return false;
        //    }
        //    return StorageMgr.contact.modify(ct);
        //}

        //public static void operationSetBlackList(Contact ct, bool isEnable)
        //{
        //    setBlackList(ct, isEnable);
        //    StorageMgr.contact.update(ct);
        //    if (!OpLogMgr.OpModContact(ct, 0x1ff))
        //    {
        //        Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
        //    }
        //}

        //public static void operationSetChatContact(Contact ct, bool isEnable)
        //{
        //    setChatContact(ct, isEnable);
        //    StorageMgr.contact.update(ct);
        //    if (!OpLogMgr.OpModContact(ct, 0x1ff))
        //    {
        //        Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
        //    }
        //}

        public static void operationSetChatroomContact(Contact ct, bool isEnable)
        {
            //if (ct == null)
            //{
            //    Log.e("ContactMgr", "operationSetChatroomContact ct == null");
            //}
            //else
            //{
            //    setChatroomContact(ct, isEnable);
            //    StorageMgr.contact.update(ct);
            //    if (!OpLogMgr.OpModContact(ct, 0x1ff))
            //    {
            //        Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
            //    }
            //}
        }

        public static void operationSetContact(Contact ct, bool isEnable)
        {
            //setContact(ct, isEnable);
            //StorageMgr.contact.update(ct);
            //ConversationMgr.clearBackGround(ct.strUsrName, true);
            //if (!OpLogMgr.OpModContact(ct, 0x1ff))
            //{
            //    Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
            //}
        }

       // public static void operationSetDomainContact(Contact ct, bool isEnable)
        //{
        //    setDomainContact(ct, isEnable);
        //    StorageMgr.contact.update(ct);
        //    if (!OpLogMgr.OpModContact(ct, 0x1ff))
        //    {
        //        Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
        //    }
        //}

        public static void operationSetFavourContact(Contact ct, bool isEnable)
        {
            //setFavourContact(ct, isEnable);
            //StorageMgr.contact.update(ct);
            //if (!OpLogMgr.OpModContact(ct, 0x1ff))
            //{
            //    Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
            //}
        }

        public static void operationSetHide(Contact ct, bool isEnable)
        {
            //setHide(ct, isEnable);
            //StorageMgr.contact.update(ct);
            //if (!OpLogMgr.OpModContact(ct, 0x1ff))
            //{
            //    Log.e("ContactMgr", "OpModContact failed when username=" + ct.strUsrName);
            //}
        }

        public static void setBlackList(Contact ct, bool isEnable)
        {
            if (isEnable)
            {
                ct.nFlags |= 8;
            }
            else
            {
                ct.nFlags &= 0xfffffff7;
            }
        }

        public static void setChatContact(Contact ct, bool isEnable)
        {
            if (isEnable)
            {
                ct.nFlags |= 2;
            }
            else
            {
                ct.nFlags &= 0xfffffffd;
            }
        }

        public static void setChatroomContact(Contact ct, bool isEnable)
        {
            if (ct == null)
            {
                Log.e("ContactMgr", "setChatroomContact ct == null");
            }
            else if (isEnable)
            {
                ct.nFlags |= 4;
            }
            else
            {
                ct.nFlags &= 0xfffffffb;
            }
        }

        public static void setContact(Contact ct, bool isEnable)
        {
            if (isEnable)
            {
                ct.nFlags |= 1;
            }
            else
            {
                ct.nFlags &= 0xfffffffe;
            }
        }

        public static void setDomainContact(Contact ct, bool isEnable)
        {
            if (isEnable)
            {
                ct.nFlags |= 0x10;
            }
            else
            {
                ct.nFlags &= 0xffffffef;
            }
        }

        public static void setFavourContact(Contact ct, bool isEnable)
        {
            if (isEnable)
            {
                ct.nFlags |= 0x40;
            }
            else
            {
                ct.nFlags &= 0xffffffbf;
            }
        }

        public static void setHide(Contact ct, bool isEnable)
        {
            if (isEnable)
            {
                ct.nFlags |= 0x20;
            }
            else
            {
                ct.nFlags &= 0xffffffdf;
            }
        }

        public static void SetReceiveMsg(Contact ct, bool bReceive)
        {
            if (ct != null)
            {
                if (bReceive)
                {
                    ct.nBrandFlag &= 0xfffffffe;
                }
                else
                {
                    ct.nBrandFlag |= 1;
                }
                operationModBrandSetting(ct, ct.nBrandFlag);
            }
        }

        //public static void SetShowInMyProfile(Contact ct, bool bShowInProfile)
        //{
        //    if (ct != null)
        //    {
        //        if (bShowInProfile)
        //        {
        //            ct.nBrandFlag &= 0xfffffffd;
        //        }
        //        else
        //        {
        //            ct.nBrandFlag |= 2;
        //        }
        //        operationModBrandSetting(ct, ct.nBrandFlag);
        //        Account account = AccountMgr.getCurAccount();
        //        Contact c = StorageMgr.contact.get(AccountMgr.strUsrName);
        //        if ((c != null) && (account != null))
        //        {
        //            BrandUser brandUser = new BrandUser {
        //                strUsrName = ct.strUsrName,
        //                nHidden = bShowInProfile ? 0 : 1,
        //                strNickName = ct.strNickName,
        //                strBrandIconURL = ct.strBrandIconURL
        //            };
        //            c.updateBrand(brandUser);
        //            StorageMgr.contact.update(c);
        //            if (account != null)
        //            {
        //                account.MyBrandList = c.strMyBrandList;
        //            }
        //            AccountMgr.updateAccount();
        //        }
        //    }
        //}

        //public static void tryRefreshContact(Contact ct)
        //{
        //    if (((ct != null) && isContact(ct)) && (ct.nUpdateDay != Util.getNowDays()))
        //    {
        //        if (ContactHelper.IsVerifiedContact(ct))
        //        {
        //            mapHeadImgUrl[ct.strUsrName] = ct.strBrandIconURL;
        //        }
        //        List<string> userNameList = new List<string> {
        //            ct.strUsrName
        //        };
        //        ServiceCenter.sceneBatchGetContact.doScene(userNameList);
        //    }
        //}

        public enum UserType
        {
            UserTypeInvaild,
            UserTypeChatRoom,
            UserTypeGroupCard,
            UserTypeWeiXin,
            UserTypeTencent,
            UserTypeSina,
            UserTypeQQ,
            UserTypeBottle,
            UserTypePlugin,
            UserTypeNormal
        }
    }
}

