namespace MicroMsg.Manager
{
    using MicroMsg.Common.Utils;
    //using MicroMsg.Plugin.QQInfo.Manager;
   // using MicroMsg.Resource.string;
    using MicroMsg.Scene;
    //using MicroMsg.Storage;
    //using MicroMsg.UI.Util;
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.InteropServices;
    using System.Xml.Linq;
    using MicroMsg.Storage;

    public static class ContactHelper
    {
        public const char cCFavoriteGroupKey = '*';
        public const char cCMeGroupKey = '^';
        public const char cCPluginGroupKey = '@';
        public const char cCQunGroupKey = '&';
        public const char cCVerifiedKey = '$';
        public const int cNAtGroupIndex = 3;
        public const int cNContactGroupIndexStated = 0x16;
        public const int cNFavoriteGroupIndex = 0;
        public const int cNMeGroupIndex = -1;
        public const int cNQunIndex = 1;
        public const int cNStrokCntMax = 0x13;
        public const int cNStrokeIndexStated = 3;
        public const int cNVerifiedUserIndex = 2;
        public static string cStrContactGroupHeaders = "ABCDEFGHIJKLMNOPQRSTUVWXYZ#";
        public const string cStrFileHelper = "filehelper";
        public const string cStrGroupCardHeader = "groupCardHeader@groupCardHeader";
        public const string cStrGroupUsrNameSuffix = "@chatroom";
        public const string cStrSuffixUserNameStranger = "@stranger";
        public const string cStrVerifiedGroupUserName = "$verifiedGroup@verifiedGroup";
        public const string cStrWeixinTeam = "weixin";
        public static string groupTitle = InitGroupTitle();
        public static string[] HELPER = new string[] { "qqmail", "fmessage", "tmessage", "qmessage", "qqsync", "floatbottle", "lbsapp", "shakeapp" };
        public static string[] HELPERTWNAME = new string[] { "QQ郵箱提醒", "好友推薦讯息", "私信助手", "QQ離線助手", "通訊錄安全助手", "漂流瓶", "查看附近的人", "搖一搖" };
        private static Contact sConVerifiedGroup=null;
        public const string SPUSER_BOTTLE = "floatbottle";
        public const string SPUSER_FMESSAGE = "fmessage";
        public const string SPUSER_LBS = "lbsapp";
        public const string SPUSER_QMESSAGE = "qmessage";
        public const string SPUSER_QQMAIL = "qqmail";
        public const string SPUSER_QQSYNC = "qqsync";
        public const string SPUSER_SHAKE = "shakeapp";
        public const string SPUSER_TMESSAGE = "tmessage";
        public const string SPUSER_WEIBO = "weibo";
        public static List<string> systemPluginName = new List<string> { "weibo", "fmessage", "floatbottle", "medianote", "qmessage", "qqmail", "tmessage", "masssend", "newsapp", "blogapp" };
        public static List<string> systemPluginNameCantDel = new List<string> { "fmessage" };
        public static List<string> systemPluginNameNeedShow = new List<string> { "fmessage", "masssend" };
        private const string TAG = "ContactHelper";
        public const string WEIXINTWNAME = "微信團隊";

        public static bool checkIsStrSystemPlugin(string uerName)
        {
            if (uerName != null)
            {
                foreach (string str in systemPluginName)
                {
                    if (str.CompareTo(uerName) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool checkIsSysPlugNeedShow(string uerName)
        {
            if (uerName != null)
            {
                foreach (string str in systemPluginNameNeedShow)
                {
                    if (str.CompareTo(uerName) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool DeleteContact(Contact con, bool bIsDelConv = true)
        {
            //if (con == null)
            //{
            //    return false;
            //}
            //if (bIsDelConv)
            //{
            //    ConversationMgr.delConversation(con.strUsrName);
            //}
            //ContactMgr.operationSetContact(con, false);
            //ServiceCenter.sceneNewSync.doScene(4, syncScene.MM_NEWSYNC_SCENE_OTHER);
            //QFriendMgr.SetQFriendContact(con.strUsrName, false);
            //MobileContact mContact = StorageMgr.mobileContact.getByUserName(con.strUsrName);
            //if (ChatContainerPage.getCuChatPageTalker() == con.strUsrName)
            //{
            //    ChatContainerPage.CloseChatPage();
            //}
            //if (mContact != null)
            //{
            //    MobileContactMgr.updatemContactToFriend(mContact, false);
            //}
            return true;
        }

        public static List<Contact> FilterContacts(List<Contact> listCons, int nFilterType)
        {
            if ((listCons != null) && (listCons.Count != 0))
            {
                int index = 0;
                while (index < listCons.Count)
                {
                    if (IsNeedFilter(listCons[index], nFilterType))
                    {
                        listCons.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            return listCons;
        }

        //public static List<BrandUser> getBrandList(this Contact c)
        //{
        //    return BrandUser.fromXml(c.strMyBrandList);
        //}

        public static string getChatRoomDisplayName(List<Contact> currentRoomInfo)
        {
            if ((currentRoomInfo == null) || (currentRoomInfo.Count < 1))
            {
                return "";
            }
            string str = "";
            foreach (Contact contact in currentRoomInfo)
            {
                if (str != "")
                {
                    str = str + "," + GetContactDisplayName(contact);
                }
                else
                {
                    str = str + GetContactDisplayName(contact);
                }
            }
            return str;
        }

        public static string getChatRoomDisplayName(string username, ref int usernum)
        {
            if ((username == null) || (username.Length <= 0))
            {
                return "";
            }
            if (ContactMgr.getUserType(username) != ContactMgr.UserType.UserTypeChatRoom)
            {
                return "";
            }
            List<Contact> list = ChatRoomMgr.getChatRoomMemberContactList(username);
            if ((list == null) || (list.Count < 1))
            {
                return "";
            }
            string str = "";
            foreach (Contact contact in list)
            {
                if (contact.strUsrName != AccountMgr.curUserName)
                {
                    if (str != "")
                    {
                        str = str + "," + GetContactDisplayName(contact);
                    }
                    else
                    {
                        str = str + GetContactDisplayName(contact);
                    }
                }
            }
            usernum = list.Count;
            return str;
        }

        public static string getChatRoomStringInChat(string username, ref int num)
        {
            num = 0;
            if (string.IsNullOrEmpty(username))
            {
                return "";
            }
            if (ContactMgr.getUserType(username) != ContactMgr.UserType.UserTypeChatRoom)
            {
                return "";
            }
            getChatRoomDisplayName(username, ref num);
            string str = getDisplayName(username, (Contact) null);
           // ChatDefaultTopic=Group Chat

            if ((!(str == "Group Chat") && !string.IsNullOrEmpty(str)) && !(str == username))
            {
                return str;
            }
            return "Group Chat";
        }

        public static string getChatRoomStringInSession(string username, ref int num)
        {
            num = 0;
            if (string.IsNullOrEmpty(username))
            {
                return "";
            }
            if (ContactMgr.getUserType(username) != ContactMgr.UserType.UserTypeChatRoom)
            {
                return "";
            }
            string str = getChatRoomDisplayName(username, ref num);
            string str2 = getDisplayName(username, (Contact) null);
            if ((!(str2 == "Group Chat") && !string.IsNullOrEmpty(str2)) && !(str2 == username))
            {
                return str2;
            }
            if (string.IsNullOrEmpty(str))
            {
                return str2;
            }
            return str;
        }

        public static List<Contact> GetConsFromBrandList(string strBrandList)
        {
            List<Contact> list = new List<Contact>();
            try
            {
                XDocument document = XDocument.Parse(strBrandList);
                if (document.Root.Name != "brandlist")
                {
                    Log.e("ContactHelper", "error object name=" + document.Root.Name);
                    return null;
                }
                foreach (XElement element in document.Root.Elements())
                {
                    if (!element.IsEmpty || element.HasAttributes)
                    {
                        Contact item = new Contact {
                            strUsrName = (string) element.Element("username"),
                            strNickName = (string) element.Element("nickname"),
                            strAlias = (string) element.Element("alias"),
                            strBrandIconURL = (string) element.Element("iconurl"),
                            nFlags = (uint) element.Element("Hidden")
                        };
                        list.Add(item);
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                Log.e("ContactHelper", "GetConsFromBrandList xml parse error=" + exception);
                return null;
            }
        }

        public static string GetContactDisplayName(Contact ct)
        {
            if (ct == null)
            {
                return "";
            }
            if ((ContactMgr.getUserType(ct.strUsrName) == ContactMgr.UserType.UserTypeChatRoom) && !string.IsNullOrEmpty(ct.strNickName))
            {
                if ((!(ct.strNickName == "群聊") && !(ct.strNickName == "聊天室")) && !(ct.strNickName == "Group Chat"))
                {
                    return ct.strNickName;
                }
                return "Group Chat";
            }
            //if (ToolFunction.checkIsLegalRemark(ct.strRemark))
            //{
            //    return ct.strRemark;
            //}
            if (!string.IsNullOrEmpty(ct.strNickName))
            {
                return ct.strNickName;
            }
            return ct.strUsrName;
        }

        public static int GetContactStroke(Contact con)
        {
            //string strRemark;
            //if (con != null)
            //{
            //    strRemark = "";
            //    if (!string.IsNullOrWhiteSpace(con.strRemark))
            //    {
            //        strRemark = con.strRemark;
            //        goto Label_0039;
            //    }
            //    if (!string.IsNullOrWhiteSpace(con.strNickName))
            //    {
            //        strRemark = con.strNickName;
            //        goto Label_0039;
            //    }
            //}
            return -1;
        //Label_0039:
        //    if (!ChineseHelper.IsChinese(strRemark[0]))
        //    {
        //        return -1;
        //    }
        //    int charStrokeCnt = GetChineseStrokeCnt.GetCharStrokeCnt(strRemark[0]);
        //    if (charStrokeCnt > 0x13)
        //    {
        //        charStrokeCnt = 0x13;
        //    }
        //    return charStrokeCnt;
        }

        public static string getDisplayName(string username, Contact con = null)
        {
            if ((username == null) || (username.Length <= 0))
            {
                return "";
            }
            if (username == "fmessage")
            {
                return "strings.Con_FMsg_TxtName";
            }
            if (username == "weixin")
            {
                return "strings.Con_Plugin_Weixin";
            }
            if (username == "qmessage")
            {
                return "strings.Con_Plugin_QQOfflineMsg_Nickname";
            }
            if (username == "floatbottle")
            {
                return "strings.Contacts_Plugin_FloatBottle_Nickname";
            }
            if (username == "masssend")
            {
                return "strings.Con_Plugin_MassSend_Nickname";
            }
            if (username == ConstantValue.TAG_BLOGAPP)
            {
                return "strings.Contacts_Plugin_Blog";
            }
            if (username == ConstantValue.TAG_NEWS)
            {
                return "strings.Plugin_news_title";
            }
            if (username == ConstantValue.TAG_QQMAIL)
            {
                return "strings.Plugin_qqmail_title";
            }
            if (username == "filehelper")
            {
                return "strings.WebMM_File_Transfer";
            }
            if (username == "$verifiedGroup@verifiedGroup")
            {
                return "strings.OfficialAccount_ContactNick";
            }
            //if (isBottleContact(username))
            //{
            //    return BContactMgr.getDisplayName(username);
            //}
            Contact ct = con;
            //if (con == null)
            //{
            //    ct = StorageMgr.contact.get(username);
            //}
            //if (ct != null)
            //{
            //    return GetContactDisplayName(ct);
            //}
            //if (isQContact(username))
            //{
            //    return getQContactDisplayName(username);
            //}
            if (isWeixin(username) || isChatRoom(username))
            {
                List<string> userNameList = new List<string> {
                    username
                };
                //ServiceCenter.sceneBatchGetContact.doScene(userNameList);
            }
            return username;
        }

        public static string getDisplayName(string strUserName, string strOptionalNickName)
        {
            //Contact con = null;
            //con = StorageMgr.contact.get(strUserName);
            //if (con != null)
            //{
            //    return getDisplayName(strUserName, con);
            //}
            return strOptionalNickName;
        }

        public static int getIndexOfGroupList(char cHeader)
        {
            int index = groupTitle.IndexOf(cHeader);
            if (-1 == index)
            {
                return groupTitle.IndexOf('#');
            }
            return index;
        }

        public static string GetNickNameByUserName(string strUser)
        {
            //Contact contact = StorageMgr.contact.get(strUser);
            //if ((contact != null) && (contact.strNickName != null))
            //{
            //    return contact.strNickName;
            //}
            return strUser;
        }

        //public static string getQContactDisplayName(string qConName)
        //{
        //    BatchGetContactProfileMgr.batchGetProfile(new List<string> { qConName });
        //    qConName = qConName.Substring(0, qConName.Length - 5);
        //    QFriend qf = QFriendMgr.getQFriend(uint.Parse(qConName));
        //    if (qf != null)
        //    {
        //        qConName = QFriendMgr.getDisplayName(qf);
        //    }
        //    return qConName;
        //}

        public static List<string> GetUsernameFromContacts(List<Contact> listCons)
        {
            if ((listCons == null) || (listCons.Count == 0))
            {
                return null;
            }
            List<string> list = new List<string>();
            foreach (Contact contact in listCons)
            {
                if ((contact != null) && !string.IsNullOrEmpty(contact.strUsrName))
                {
                    list.Add(contact.strUsrName);
                }
            }
            return list;
        }

        public static Contact GetVerifiedGroupContact()
        {
            if (sConVerifiedGroup != null)
            {
                return sConVerifiedGroup;
            }
            return new Contact { strUsrName = "$verifiedGroup@verifiedGroup", nFlags = 7 };
        }

        public static bool hasConferenceUrlInfo(this Contact con)
        {
            if (((con.BrandInfo == null) || (con.BrandInfo.urls == null)) || (con.BrandInfo.urls.Count == 0))
            {
                return false;
            }
            ConferenceUrlInfo info = con.BrandInfo.urls[0];
            if (string.IsNullOrEmpty(info.url))
            {
                return false;
            }
            return true;
        }

        public static bool hasContactDisplayUsrName(string strUsrName, string strAlias)
        {
            //if (string.IsNullOrEmpty(strAlias))
            //{
            //    if (string.IsNullOrEmpty(strUsrName))
            //    {
            //        return false;
            //    }
            //    if (strUsrName.StartsWith("wxid_") || strUsrName.StartsWith("gh_"))
            //    {
            //        return false;
            //    }
            //    string str = PackageDataMgr.getConfig("HideWechatID", "idprefix");
            //    if (!string.IsNullOrEmpty(str))
            //    {
            //        foreach (string str2 in str.Split(new char[] { ';' }))
            //        {
            //            string str1 = str2.Trim();
            //            if (!string.IsNullOrEmpty(str1) && strUsrName.StartsWith(str1))
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}
            return true;
        }

        private static string InitGroupTitle()
        {
            string str = "*&$@";
            for (int i = 1; i <= 0x13; i++)
            {
                str = str + ((char) i);
            }
            return (str + "ABCDEFGHIJKLMNOPQRSTUVWXYZ#");
        }

        public static bool isBlogAppContact(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals(ConstantValue.TAG_BLOGAPP);
        }

        public static bool isBottleContact(string username)
        {
            if (username == null)
            {
                return false;
            }
            if (!username.Contains(ConstantValue.TAG_BOTTLE + ":"))
            {
                return username.Contains(ConstantValue.TAG_BOTTLE);
            }
            return true;
        }

        public static bool isChatRoom(string username)
        {
            return (((username != null) && (username.Length > 0)) && username.EndsWith(ConstantValue.TAG_CHATROOM));
        }

        public static bool isConferenceAccount(this Contact con)
        {
            return ((con.ExtInfo != null) && (con.ExtInfo.ConferenceContactExpireTime > 0));
        }

        public static bool isConferenceExpired(this Contact con)
        {
            if (con.ExtInfo == null)
            {
                return false;
            }
            long num = (long) Util.getNowSeconds();
            long num2 = con.ExtInfo.ConferenceContactExpireTime - num;
            return (num2 < 0L);
        }

        public static bool IsFavoriteContact(Contact con)
        {
            if (con == null)
            {
                return false;
            }
            return ((con.nFlags & 0x40) != 0);
        }

        public static bool isFloatBottleHelper(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals("floatbottle", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool isFriendsRecommendHelper(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals("fmessage", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsGotoVerifiedProfile(Contact con)
        {
            return (IsVerifiedContact(con) && (con.strUsrName != "weixin"));
        }

        public static bool isGroupCard(string username)
        {
            return (((username != null) && (username.Length > 0)) && username.EndsWith(ConstantValue.TAG_GROUPCARD));
        }

        public static bool IsInVerifiedEntryContact(string strUserName)
        {
            return (IsVerifiedContact(strUserName) && (strUserName != "weixin"));
        }

        public static bool isMicroBlogPrivateMsgHelper(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals("tmessage", StringComparison.CurrentCultureIgnoreCase);
        }

        private static bool isMicroBlogTencent(string username)
        {
            return (((username != null) && (username.Length > 0)) && username.EndsWith(ConstantValue.TAG_MICROBLOG_TENCENT));
        }

        public static bool isNeedCustomPrompt(this Contact con)
        {
            return ((con.ExtInfo != null) && !string.IsNullOrEmpty(con.ExtInfo.VerifyContactPromptTitle));
        }

        public static bool IsNeedFilter(Contact ct, int nFilterType)
        {
            if ((ct == null) || string.IsNullOrEmpty(ct.strUsrName))
            {
                return true;
            }
            if (nFilterType == 0)
            {
                return false;
            }
            return ((((nFilterType | 1) != 0) && ct.strUsrName.EndsWith("@chatroom")) || ((((nFilterType | 4) != 0) && (ct.strUsrName == AccountMgr.strUsrName)) || (((nFilterType | 2) != 0) && IsVerifiedContact(ct))));
        }

        public static bool isNeedShowChatEntry(Contact con)
        {
            if (con == null)
            {
                return false;
            }
            if (con.strUsrName == AccountMgr.strUsrName)
            {
                return false;
            }
            if (con.strUsrName == "groupCardHeader@groupCardHeader")
            {
                return false;
            }
            if (systemPluginName.Contains(con.strUsrName))
            {
                return false;
            }
            return true;
        }

        public static bool isNeedShowMenuHeader(Contact con)
        {
            if (con == null)
            {
                return false;
            }
            if (con.strUsrName == AccountMgr.strUsrName)
            {
                return false;
            }
            if (con.strUsrName == "weixin")
            {
                return false;
            }
            if (con.strUsrName == "groupCardHeader@groupCardHeader")
            {
                return false;
            }
            if (systemPluginNameCantDel.Contains(con.strUsrName))
            {
                return false;
            }
            return true;
        }

        public static bool isNewsContact(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (!username.Equals(ConstantValue.TAG_NEWS) && !(username == ConstantValue.TAG_NEWS))
            {
                return false;
            }
            return true;
        }

        public static bool isQContact(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.EndsWith(ConstantValue.TAG_QQ);
        }

        public static bool isQQMailContact(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals(ConstantValue.TAG_QQMAIL);
        }

        public static bool isQQMailHelper(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals("qqmail", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool isQQOffLineMessageHelper(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.Equals("qmessage", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool isQQSyncHelper(string username)
        {
            return "qqsync".Equals(username, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool isTContact(string username)
        {
            if (username == null)
            {
                return false;
            }
            return username.EndsWith(ConstantValue.TAG_MICROBLOG_TENCENT);
        }

        public static bool IsUsernameEncrypt(string strUsrName)
        {
            if (string.IsNullOrEmpty(strUsrName))
            {
                return false;
            }
            return strUsrName.EndsWith("@stranger");
        }

        public static bool IsVerifiedContact(Contact con)
        {
            return (((con != null) && (con.nVerifyFlag != 0)) && !checkIsStrSystemPlugin(con.strUsrName));
        }

        public static bool IsVerifiedContact(string strUserName)
        {
            //return IsVerifiedContact(StorageMgr.contact.get(strUserName));
            return IsVerifiedContact(strUserName);
        }

        public static bool IsVerifiedContactVerified(Contact con)
        {
            return (((con != null) && ((con.nVerifyFlag & 0x10) != 0)) && !checkIsStrSystemPlugin(con.strUsrName));
        }

        //public static bool IsVerifiedContactVerified(string strUserName)
        //{
        //    return IsVerifiedContactVerified(StorageMgr.contact.get(strUserName));
        //}

        public static bool isVerifiedGroupUser(string usrname)
        {
            return "$verifiedGroup@verifiedGroup".Equals(usrname);
        }

        public static bool isWeixin(string username)
        {
            if ((username == null) || (username.Length <= 0))
            {
                return false;
            }
            if (username.Contains("@"))
            {
                return username.EndsWith(ConstantValue.TAG_WEIXIN);
            }
            return true;
        }

        public static BrandInfo parseBrandInfo(string jasonStr)
        {
            return (BrandInfo) Util.GetObjectFromJson(jasonStr, typeof(BrandInfo));
        }

        public static BrandInfo parseConferenceUrlInfo(SearchContactInfo con)
        {
            if (((con != null) && (con.CustomizedInfo != null)) && (con.CustomizedInfo.BrandInfo != null))
            {
                return parseBrandInfo(con.CustomizedInfo.BrandInfo);
            }
            return null;
        }

        public static ExtInfo parseExtInfo(SearchContactInfo con)
        {
            if (((con != null) && (con.CustomizedInfo != null)) && (con.CustomizedInfo.ExternalInfo != null))
            {
                return parseExtInfo(con.CustomizedInfo.ExternalInfo);
            }
            return null;
        }

        public static ExtInfo parseExtInfo(string jasonStr)
        {
            return (ExtInfo) Util.GetObjectFromJson(jasonStr, typeof(ExtInfo));
        }

        //public static void setBrandList(this Contact c, List<BrandUser> userList)
        //{
        //    c.strMyBrandList = BrandUser.toXml(userList);
        //}

        //public static void updateBrand(this Contact c, BrandUser brandUser)
        //{
        //    List<BrandUser> userList = BrandUser.fromXml(c.strMyBrandList);
        //    if (userList != null)
        //    {
        //        int num = 0;
        //        foreach (BrandUser user in userList)
        //        {
        //            if (brandUser.strUsrName == user.strUsrName)
        //            {
        //                userList[num] = brandUser;
        //                c.strMyBrandList = BrandUser.toXml(userList);
        //                return;
        //            }
        //            num++;
        //        }
        //        userList.Add(brandUser);
        //        c.strMyBrandList = BrandUser.toXml(userList);
        //    }
        //}

        public enum emMMUserAttrVerifyFlag
        {
            MM_USERATTRVERIFYFALG_BIZ = 1,
            MM_USERATTRVERIFYFALG_BIZ_BIG = 4,
            MM_USERATTRVERIFYFALG_BIZ_BRAND = 8,
            MM_USERATTRVERIFYFALG_BIZ_VERIFIED = 0x10,
            MM_USERATTRVERIFYFALG_FAMOUS = 2
        }
    }
}

