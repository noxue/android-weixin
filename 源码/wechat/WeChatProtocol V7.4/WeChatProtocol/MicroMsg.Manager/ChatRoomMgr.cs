namespace MicroMsg.Manager
{
    using micromsg;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    //using MicroMsg.Resource.string;
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;

    public class ChatRoomMgr
    {
        private const int ROOM_MUTE_OFF = 1;
        private const int ROOM_MUTE_ON = 0;
        private const string TAG = "ChatRoomMgr";
        public const string USER_NAMES_FIELD = "usernames=";
        private const string USERNAME_SEPARATOR = "、";

        public static bool addChatroomMember(string chatRoomName, AddChatRoomMemberResponse memberInfo, List<string> verifyUserList)
        {
            //if ((ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom) || (memberInfo.MemberListList.Count == 0))
            //{
            //    Log.e("ChatRoomMgr", string.Concat(new object[] { "CreateChatroom: room:", chatRoomName, " listCnt:", memberInfo.MemberListList.Count }));
            //    return false;
            //}
            //List<Contact> itemList = new List<Contact>();
            //List<string> userNameList = new List<string>();
            //foreach (MemberResp resp in memberInfo.MemberListList)
            //{
            //    if (resp.MemberStatus == 0)
            //    {
            //        Contact ct = memberToContact(resp);
            //        ContactMgr.setChatroomContact(ct, true);
            //        itemList.Add(ct);
            //        userNameList.Add(ct.strUsrName);
            //    }
            //}
            //if ((itemList.Count > 0) && !StorageMgr.contact.updateList(itemList))
            //{
            //    Log.d("ChatRoomMgr", "addChatroomMember failed");
            //    return false;
            //}
            //if ((userNameList.Count > 0) && !mergUserListByChatRoomName(chatRoomName, userNameList))
            //{
            //    Log.d("ChatRoomMgr", "mergUserListByChatRoomName failed");
            //    return false;
            //}
            //if ((verifyUserList != null) && (verifyUserList.Count > 0))
            //{
            //    addNeedVerifyNotifyMsg(chatRoomName, verifyUserList);
            //}
            return true;
        }

        private static bool addChatRoomNotifyMsg(string chatRoomName, List<string> userNameList)
        {
            ChatMsg msg = new ChatMsg {
                nMsgSvrID = -1,
                nMsgType = 0x2710,
                strTalker = chatRoomName,
                nCreateTime = (long) Util.getNowSeconds()
            };
            string str = userNameListToNickNameString(userNameList);
            msg.strMsg = string.Format("{0}.ChatInviteInfo", str);
            msg.nIsSender = 0;
            msg.nStatus = 2;
            Log.e("ChatRoomMgr", "addChatRoomNotifyMsg");
            return true;
            //return StorageMgr.chatMsg.addMsg(msg);
        }

        public static bool addGroupCardContact(GroupCardRequest req, GroupCardResponse resp)
        {
            //Contact ct = StorageMgr.contact.get(resp.GroupUserName);
            //if (ct == null)
            //{
            //    ct = new Contact
            //    {
            //        strUsrName = resp.GroupUserName
            //    };
            //}
            //ct.strNickName = req.GroupNickName;
            //ContactMgr.setContact(ct, true);
            //StorageMgr.contact.update(ct);
            //List<string> userNameList = new List<string>();
            //foreach (RoomInfo info in req.MemberListList)
            //{
            //    userNameList.Add(info.UserName.String);
            //}
           // return mergUserListByChatRoomName(ct.strUsrName, userNameList);
            return true;
        }

        private static bool addNeedVerifyNotifyMsg(string chatRoomName, List<string> verifyUserList)
        {
            ChatMsg msg = new ChatMsg {
                nMsgSvrID = -1,
                nMsgType = 0x2710,
                strTalker = chatRoomName,
                nCreateTime = (long) Util.getNowSeconds()
            };
            string str = userNameListToNickNameString(verifyUserList);
            msg.strMsg = string.Format("{0}strings.ChatVerifyInfo", str);
            string str2 = userNameListToString(verifyUserList);
            string strMsg = msg.strMsg;
            msg.strMsg = strMsg + "<a href=\"weixin://findfriend/verifycontact?usernames=" + str2 + "\">" + "strings.ChatRequestVerify" + "</a>";
            msg.nIsSender = 0;
            msg.nStatus = 2;
            Log.e("ChatRoomMgr", "addNeedVerifyNotifyMsg");
            return true;
            //return StorageMgr.chatMsg.addMsg(msg);
        }

        public static bool addToContact(string chatRoomName, string strRemarkName)
        {
            //if (string.IsNullOrEmpty(chatRoomName))
            //{
            //    return false;
            //}
            //if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
            //{
            //    Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
            //    return false;
            //}
            //Contact ct = StorageMgr.contact.get(chatRoomName);
            //if (ct == null)
            //{
            //    return false;
            //}
            //if (string.IsNullOrEmpty(strRemarkName))
            //{
            //    ct.strRemark = "";
            //    ct.strRemarkPYInitial = "";
            //    ct.strRemarkQuanPin = "";
            //}
            //else
            //{
            //    ct.strRemark = strRemarkName;
            //    ct.strRemarkPYInitial = SortStringsByAlpha.ConvertStringToPinyinInitial(strRemarkName);
            //    ct.strRemarkQuanPin = GetPinYin.ConvertStrToQuanPin(strRemarkName);
            //}
            //ContactMgr.setContact(ct, true);
            //if (!ContactMgr.operationModContact(ct))
            //{
            //    Log.e("ChatRoomMgr", "OpModContact failed when username=" + ct.strUsrName);
            //    return false;
            //}
            return true;
        }

        public static RetConst checkMemberStatusFromMemberList(List<string> invalidUsernameList, List<string> memeberBlackList, List<string> notExistUserList, List<string> verifyUserList, IList<MemberResp> memberList)
        {
            RetConst @const = RetConst.MM_OK;
            foreach (MemberResp resp in memberList)
            {
                uint memberStatus = resp.MemberStatus;
                string item = resp.MemberName.String;
                switch (memberStatus)
                {
                    case 0:
                        break;

                    case 3:
                        Log.d("ChatRoomMgr", " blacklist : " + item);
                        memeberBlackList.Add(item);
                        @const = RetConst.MM_ERR_BLACKLIST;
                        break;

                    case 1:
                        Log.d("ChatRoomMgr", " not user : " + item);
                        notExistUserList.Add(item);
                        @const = RetConst.MM_ERR_NOTCHATROOMCONTACT;
                        break;

                    case 2:
                        Log.d("ChatRoomMgr", " invalid username : " + item);
                        invalidUsernameList.Add(item);
                        @const = RetConst.MM_ERR_USERNAMEINVALID;
                        break;

                    case 4:
                        Log.d("ChatRoomMgr", " verify user : " + item);
                        verifyUserList.Add(item);
                        break;

                    default:
                        Log.e("ChatRoomMgr", "unknown member status : status = " + memberStatus);
                        @const = RetConst.MM_ERR_SYS;
                        break;
                }
            }
            return @const;
        }

        public static bool createChatroom(CreateChatRoomResponse createInfo, List<string> verifyUserList)
        {
        //    if ((ContactMgr.getUserType(createInfo.ChatRoomName.String) != ContactMgr.UserType.UserTypeChatRoom) || (createInfo.MemberListList.Count == 0))
        //    {
        //        Log.e("ChatRoomMgr", string.Concat(new object[] { "CreateChatroom: room:", createInfo.ChatRoomName.String, " listCnt:", createInfo.MemberListList.Count }));
        //        return false;
        //    }
        //    List<Contact> itemList = new List<Contact>();
        //    List<string> collection = new List<string>();
        //    List<string> userNameList = new List<string>();
        //    Contact item = respToContact(createInfo);
        //    itemList.Add(item);
        //    foreach (MemberResp resp in createInfo.MemberListList)
        //    {
        //        if (resp.MemberStatus == 0)
        //        {
        //            Contact ct = memberToContact(resp);
        //            ContactMgr.setChatroomContact(ct, true);
        //            itemList.Add(ct);
        //            if (ct.strUsrName != AccountMgr.getCurAccount().strUsrName)
        //            {
        //                collection.Add(ct.strUsrName);
        //            }
        //        }
        //    }
        //    userNameList.AddRange(collection);
        //    userNameList.Add(AccountMgr.getCurAccount().strUsrName);
        //    if ((itemList.Count > 0) && !StorageMgr.contact.updateList(itemList))
        //    {
        //        Log.d("ChatRoomMgr", "createChatroom failed");
        //        return false;
        //    }
        //    if (!mergUserListByChatRoomName(item.strUsrName, userNameList))
        //    {
        //        Log.d("ChatRoomMgr", "createChatroom failed");
        //        return false;
        //    }
        //    if (collection.Count > 0)
        //    {
        //        addChatRoomNotifyMsg(createInfo.ChatRoomName.String, collection);
        //    }
        //    if ((verifyUserList != null) && (verifyUserList.Count > 0))
        //    {
        //        addNeedVerifyNotifyMsg(createInfo.ChatRoomName.String, verifyUserList);
        //    }
           return true;
        }

        public static bool delChatroomMember(string chatRoomName, DelChatRoomMemberResponse memberInfo)
        {
            //Contact item = StorageMgr.contact.get(chatRoomName);
            //StorageMgr.contact.modify(item);
            //HeadImageMgr.downLoadHeadImage(item.strUsrName, null, null);
            //if ((ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom) || (memberInfo.MemberListList.Count == 0))
            //{
            //    Log.e("ChatRoomMgr", string.Concat(new object[] { "DelMemberChatroom: room:", chatRoomName, " listCnt:", memberInfo.MemberListList.Count }));
            //    return false;
            //}
            //List<string> userNameList = new List<string>();
            //foreach (DelMemberResp resp in memberInfo.MemberListList)
            //{
            //    Contact contact2 = StorageMgr.contact.get(resp.MemberName.String);
            //    userNameList.Add(contact2.strUsrName);
            //}
            //return reMergUserListByChatRoomName(chatRoomName, userNameList);
            return true;
        }

        //private static bool deleteWholeChatroom(string chatRoomName)
        //{
        //    if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
        //    {
        //        Log.e("ChatRoomMgr", "deleteWholeChatroom,room:" + chatRoomName);
        //        return false;
        //    }
        //    if (!StorageMgr.contact.del(chatRoomName))
        //    {
        //        Log.e("ChatRoomMgr", "del contact failed,room:" + chatRoomName);
        //        return false;
        //    }
        //    if (!StorageMgr.chatRoom.del(chatRoomName))
        //    {
        //        Log.e("ChatRoomMgr", "del chatRoom failed,room:" + chatRoomName);
        //        return false;
        //    }
        //    return true;
        //}

        public static List<Contact> getChatRoomMemberContactList(string chatRoomName)
        {
            List<string> userNameList = getMembersByChatRoomName(chatRoomName);
            if (userNameList == null)
            {
                Log.e("ChatRoomMgr", "getListUserNameAndNickName,userNameList==null");
                return null;
            }
            Log.e("ChatRoomMgr", "getListUserNameAndNickName,userNameList==null");
            return null;
            //return StorageMgr.contact.getByUserNameList(userNameList);
        }

        public static List<string> getChatRoomMemberNickNameList(string chatRoomName)
        {
            List<Contact> list = getChatRoomMemberContactList(chatRoomName);
            if (list == null)
            {
                Log.e("ChatRoomMgr", "getChatRoomMemberContactList returns null");
                return null;
            }
            List<string> list2 = new List<string>();
            foreach (Contact contact in list)
            {
                list2.Add(contact.strNickName);
            }
            return list2;
        }

        public static bool GetChatRoomNotify(string chatRoomName)
        {
            //if (string.IsNullOrEmpty(chatRoomName))
            //{
            //    return false;
            //}
            //if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
            //{
            //    Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
            //    return false;
            //}
            //Contact contact = StorageMgr.contact.get(chatRoomName);
            //if (contact == null)
            //{
            //    return false;
            //}
            //if (contact.nChatRoomNotify == 0)
            //{
            //    return false;
            //}
            return true;
        }

        //public static bool GetChatRoomShowTooMuchMsgNotify(string chatRoomName, int newMsgNum)
        //{
        //    int chatRoomTooMuchMsgNotifyNumber = GetChatRoomTooMuchMsgNotifyNumber(chatRoomName);
        //    return (((newMsgNum > 40) && GetChatRoomNotify(chatRoomName)) && (chatRoomTooMuchMsgNotifyNumber < 3));
        //}

      //  public static int GetChatRoomTooMuchMsgNotifyNumber(string chatRoomName)
       // {
            //if (string.IsNullOrEmpty(chatRoomName))
            //{
            //    return 0x7fffffff;
            //}
            //if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
            //{
            //    Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
            //    return 0x7fffffff;
            //}
            //Contact contact = StorageMgr.contact.get(chatRoomName);
            //if (contact == null)
            //{
            //    return 0x7fffffff;
            //}
            //return contact.nTooMuchMsgNotifyNumber;
       // }

        public static List<Contact> getFriendContatsFromChatRoomName(string strUsrName)
        {
            List<string> list = getMembersByChatRoomName(strUsrName);
            if (list == null)
            {
                return null;
            }
            List<Contact> list2 = new List<Contact>();
            //foreach (string str in list)
            //{
            //    Contact ct = StorageMgr.contact.get(str);
            //    if (((ct != null) && !ContactMgr.isBlackList(ct)) && ContactMgr.isContact(ct))
            //    {
            //        list2.Add(ct);
            //    }
            //}
            return list2;
        }

        private static List<string> getMemberListByChatroomName(string chatRoomName)
        {
            //ChatRoom room = StorageMgr.chatRoom.get(chatRoomName);
            //if (room == null)
            //{
            //    Log.e("ChatRoomMgr", "error ,getMemberListByChatroomName failed,chatRoomName=" + chatRoomName);
            //    StorageMgr.chatMsg.dumpTable();
            //    StorageMgr.contact.dumpTable();
            //    StorageMgr.chatRoom.dumpTable();
            //    return null;
            //}
            //return stringToUsernameList(room.strUserNameList);
            Log.e("ChatRoomMgr", "error ,getMemberListByChatroomName failed,chatRoomName=" + chatRoomName);
            return null;
        }

        public static List<string> getMembersByChatRoomName(string chatRoomName)
        {
            if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
            {
                Log.e("ChatRoomMgr", "getMembersByChatRoomName,room:" + chatRoomName);
                return null;
            }
            return getMemberListByChatroomName(chatRoomName);
        }

        //public static List<string> getMembersByGroupCardName(string groupcardName)
        //{
        //    if (ContactMgr.getUserType(groupcardName) != ContactMgr.UserType.UserTypeGroupCard)
        //    {
        //        Log.e("ChatRoomMgr", "getMembersByGroupCardName,groupcardName" + groupcardName);
        //        return null;
        //    }
        //    return getMemberListByChatroomName(groupcardName);
        //}

        //public static void IncreaseChatRoomTooMuchMsgNotifyNumber(string chatRoomName)
        //{
        //    int chatRoomTooMuchMsgNotifyNumber = GetChatRoomTooMuchMsgNotifyNumber(chatRoomName);
        //    if ((chatRoomTooMuchMsgNotifyNumber >= 0) && (chatRoomTooMuchMsgNotifyNumber < 3))
        //    {
        //        SetChatRoomTooMuchMsgNotifyNumber(chatRoomName, chatRoomTooMuchMsgNotifyNumber + 1);
        //    }
        //}

        //public static bool isKickedByChatRoom(string chatRoomName)
        //{
        //    bool flag = false;
        //    List<string> list = getMembersByChatRoomName(chatRoomName);
        //    if (((list != null) && (list.Count != 0)) && list.Contains(AccountMgr.curUserName))
        //    {
        //        return flag;
        //    }
        //    return true;
        //}

        //private static Contact memberToContact(MemberResp member)
        //{
        //    Contact contact = StorageMgr.contact.get(member.MemberName.String);
        //    if (contact == null)
        //    {
        //        contact = new Contact {
        //            strUsrName = member.MemberName.String
        //        };
        //    }
        //    contact.strNickName = member.NickName.String;
        //    contact.strPYInitial = member.PYInitial.String;
        //    contact.strQuanPin = member.QuanPin.String;
        //    contact.nSex = member.Sex;
        //    contact.strRemark = member.Remark.String;
        //    contact.strRemarkPYInitial = member.RemarkPYInitial.String;
        //    contact.strRemarkQuanPin = member.RemarkQuanPin.String;
        //    contact.nContactType = member.ContactType;
        //    contact.nPersonalCard = member.PersonalCard;
        //    contact.strCountry = member.Country;
        //    contact.strProvince = member.Province;
        //    contact.strCity = member.City;
        //    contact.strSignature = member.Signature;
        //    contact.nVerifyFlag = member.VerifyFlag;
        //    contact.strVerifyInfo = member.VerifyInfo;
        //    return contact;
        //}

        //public static bool mergUserListByChatRoomName(string chatRoomName, List<string> userNameList)
        //{
        //    ChatRoom room = StorageMgr.chatRoom.get(chatRoomName);
        //    if (room != null)
        //    {
        //        foreach (string str in stringToUsernameList(room.strUserNameList))
        //        {
        //            if (!userNameList.Contains(str))
        //            {
        //                userNameList.Add(str);
        //            }
        //        }
        //    }
        //    return replaceUserListByChatRoomName(chatRoomName, userNameList);
        //}

        //public static bool quitChatRoom(string chatRoomName)
        //{
        //    if (string.IsNullOrEmpty(chatRoomName))
        //    {
        //        return false;
        //    }
        //    if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
        //    {
        //        Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
        //        return false;
        //    }
        //    if (StorageMgr.contact.get(chatRoomName) == null)
        //    {
        //        return false;
        //    }
        //    ChatMsgMgr.deleteMsgByTalker(chatRoomName);
        //    if (!OpLogMgr.OpDelChatContact(chatRoomName))
        //    {
        //        Log.e("ChatRoomMgr", "OpDelChatContact failed when chatRoomName" + chatRoomName);
        //        return false;
        //    }
        //    StorageMgr.converation.del(chatRoomName);
        //    if (!OpLogMgr.OpDelContact(chatRoomName))
        //    {
        //        Log.e("ChatRoomMgr", "OpDelContact failed when chatRoomName" + chatRoomName);
        //        return false;
        //    }
        //    if (!OpLogMgr.OpQuitChatRoom(chatRoomName))
        //    {
        //        Log.e("ChatRoomMgr", "OpQuitChatRoom failed when chatRoomName" + chatRoomName);
        //        return false;
        //    }
        //    deleteWholeChatroom(chatRoomName);
        //    return true;
        //}

        //public static bool reMergUserListByChatRoomName(string chatRoomName, List<string> userNameList)
        //{
        //    ChatRoom room = StorageMgr.chatRoom.get(chatRoomName);
        //    List<string> list = new List<string>();
        //    if (room != null)
        //    {
        //        list = stringToUsernameList(room.strUserNameList);
        //        foreach (string str in userNameList)
        //        {
        //            if (list.Contains(str))
        //            {
        //                list.Remove(str);
        //            }
        //        }
        //    }
        //    return replaceUserListByChatRoomName(chatRoomName, list);
        //}

        public static bool replaceUserListByChatRoomName(string chatRoomName, List<string> userNameList)
        {
            //ChatRoom item = new ChatRoom {
            //    strChatRoomName = chatRoomName,
            //    strUserNameList = userNameListToString(userNameList),
            //    nTime = (uint) Util.getNowSeconds()
            //};
            //if (!StorageMgr.chatRoom.update(item))
            //{
            //    Log.d("ChatRoomMgr", "updateChatRoom failed");
            //    return false;
            //}
            return true;
        }

        //private static Contact respToContact(CreateChatRoomResponse resp)
        //{
        //    return new Contact { strNickName = resp.Topic.String, strPYInitial = resp.PYInitial.String, strQuanPin = resp.QuanPin.String, strUsrName = resp.ChatRoomName.String, strChatRoomOwner = AccountMgr.curUserName, nChatRoomNotify = 1, strBigHeadImgUrl = resp.BigHeadImgUrl, strSmallHeadImgUrl = resp.SmallHeadImgUrl };
        //}

        public static bool SetChatRoomTooMuchMsgNotifyNumber(string chatRoomName, int num)
        {
            //if (string.IsNullOrEmpty(chatRoomName))
            //{
            //    return false;
            //}
            //if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
            //{
            //    Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
            //    return false;
            //}
            //Contact item = StorageMgr.contact.get(chatRoomName);
            //if (item == null)
            //{
            //    return false;
            //}
            //if ((num < 0) && (num == item.nTooMuchMsgNotifyNumber))
            //{
            //    return false;
            //}
            //item.nTooMuchMsgNotifyNumber = num;
            //StorageMgr.contact.modify(item);
            return true;
        }

        //public static bool setChatRoomTopic(string chatRoomName, string chatRoomTopic)
        //{
        //    if (string.IsNullOrEmpty(chatRoomName) || (chatRoomTopic == null))
        //    {
        //        return false;
        //    }
        //    if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
        //    {
        //        Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
        //        return false;
        //    }
        //    Contact item = StorageMgr.contact.get(chatRoomName);
        //    if (item == null)
        //    {
        //        return false;
        //    }
        //    item.strNickName = chatRoomTopic;
        //    StorageMgr.contact.modify(item);
        //    return OpLogMgr.OpModChatRoomTopic(chatRoomName, chatRoomTopic);
        //}

        //public static bool SetModChatRoomNotify(string chatRoomName, bool enable)
        //{
        //    if (string.IsNullOrEmpty(chatRoomName))
        //    {
        //        return false;
        //    }
        //    if (ContactMgr.getUserType(chatRoomName) != ContactMgr.UserType.UserTypeChatRoom)
        //    {
        //        Log.e("ChatRoomMgr", "invalid chatRoomName" + chatRoomName);
        //        return false;
        //    }
        //    int num = enable ? 1 : 0;
        //    Contact item = StorageMgr.contact.get(chatRoomName);
        //    if (item == null)
        //    {
        //        return false;
        //    }
        //    item.nChatRoomNotify = (uint) num;
        //    StorageMgr.contact.modify(item);
        //    return OpLogMgr.OpModChatRoomNotify(chatRoomName, (uint) num);
        //}

        //public static List<string> stringToUsernameList(string userName)
        //{
        //    List<string> list = new List<string>();
        //    if (!string.IsNullOrWhiteSpace(userName))
        //    {
        //        string[] strArray = userName.Split("、".ToCharArray());
        //        for (int i = 0; i < strArray.Length; i++)
        //        {
        //            list.Add(strArray[i]);
        //        }
        //    }
        //    return list;
        //}

        public static string userNameListToNickNameString(List<string> userNameList)
        {
            string str = "";
            for (int i = 0; i < userNameList.Count; i++)
            {
                str = str + ContactHelper.getDisplayName(userNameList[i], (Contact)null);
                if (i < (userNameList.Count - 1))
                {
                    str = str + "、";
                }
            }
            return str;
        }

        public static string userNameListToString(List<string> userNameList)
        {
            string str = "";
            for (int i = 0; i < userNameList.Count; i++)
            {
                str = str + userNameList[i];
                if (i < (userNameList.Count - 1))
                {
                    str = str + "、";
                }
            }
            return str;
        }
    }
}

