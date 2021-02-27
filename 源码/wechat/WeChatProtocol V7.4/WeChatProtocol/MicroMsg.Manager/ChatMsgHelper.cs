namespace MicroMsg.Manager
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Storage;
    using System;
    

    public static class ChatMsgHelper
    {
        //public static MsgUIStatus GetStatus(this ChatMsg msg)
        //{
        //    if (msg != null)
        //    {
        //        if (!msg.IsImage() && !msg.IsCustomSmiley())
        //        {
        //            return (MsgUIStatus) msg.nStatus;
        //        }
        //        if (msg.nIsSender == 1)
        //        {
        //            return (MsgUIStatus) msg.nStatus;
        //        }
        //    }
        //    return MsgUIStatus.Success;
        //}

        public static string getTalker(string toUserName, string fromUserName)
        {
            if (string.IsNullOrEmpty(toUserName) || string.IsNullOrEmpty(fromUserName))
            {
                //DebugEx.debugBreak();
                return "";
            }
            if (AccountMgr.getCurAccount().strUsrName.Equals(toUserName))
            {
                return fromUserName;
            }
            return toUserName;
        }

        public static bool IsAppMsg(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x31);
        }

        public static bool IsCustomSmiley(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x2f);
        }

        public static bool IsFmessage(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            if ((msg.nMsgType != 0x25) && (msg.nMsgType != 40))
            {
                return false;
            }
            return true;
        }

        public static bool IsImage(this ChatMsg msg)
        {
            if (msg != null)
            {
                switch (msg.nMsgType)
                {
                    case 0x17:
                    case 0x21:
                    case 0x27:
                    case 3:
                    case 13:
                        return true;
                }
            }
            return false;
        }

        public static bool IsMap(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x30);
        }

        public static bool IsPushMsg(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x26);
        }

        public static bool isQmessageTxt(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x24);
        }

        public static bool IsQQMailmessage(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x23);
        }

        public static bool IsSender(this ChatMsg msg)
        {
            return (msg.nIsSender > 0);
        }

        //public static bool IsShakeImageMsg(this ChatMsg msg)
        //{
        //    if (msg == null)
        //    {
        //        return false;
        //    }
        //    return ((msg.nMsgType == 0x31) && (AppMsgMgr.parseAppXml(msg.strMsg).showtype == 2));
        //}

        public static bool IsShareCard(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x2a);
        }

        public static bool IsSupport(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            ContactMgr.UserType type = ContactMgr.getUserType(msg.strTalker);
            if (msg.strTalker == "sysnotice")
            {
                return false;
            }
            if (msg.strTalker == "pc_share")
            {
                return false;
            }
            switch (type)
            {
                case ContactMgr.UserType.UserTypeChatRoom:
                case ContactMgr.UserType.UserTypeNormal:
                case ContactMgr.UserType.UserTypeQQ:
                    return true;
            }
            switch (type)
            {
                case ContactMgr.UserType.UserTypeQQ:
                    return true;

                case ContactMgr.UserType.UserTypeBottle:
                    return true;
            }
            //return (((type == ContactMgr.UserType.UserTypePlugin) && (((msg.strTalker == "weixin") || ((msg.strTalker == "fmessage") && (FMsgMgr.getFMsgType(msg) == FMsgType.other))) || (((msg.strTalker == ConstantValue.TAG_BLOGAPP) || (msg.strTalker == ConstantValue.TAG_NEWS)) || (msg.strTalker == ConstantValue.TAG_QQMAIL)))) || msg.IsPushMsg());
            return (((type == ContactMgr.UserType.UserTypePlugin) && (((msg.strTalker == "weixin") || ((msg.strTalker == "fmessage"))) || (((msg.strTalker == ConstantValue.TAG_BLOGAPP) || (msg.strTalker == ConstantValue.TAG_NEWS)) || (msg.strTalker == ConstantValue.TAG_QQMAIL)))) || msg.IsPushMsg());
        
        }

        public static bool IsSupportConversation(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            ContactMgr.UserType type = ContactMgr.getUserType(msg.strTalker);
            if (msg.strTalker == "sysnotice")
            {
                return false;
            }
            if (msg.strTalker == "pc_share")
            {
                return false;
            }
            if ((type == ContactMgr.UserType.UserTypeNormal) && ContactHelper.IsInVerifiedEntryContact(msg.strTalker))
            {
                return false;
            }
            if (type == ContactMgr.UserType.UserTypeQQ)
            {
                return false;
            }
            if (type == ContactMgr.UserType.UserTypeBottle)
            {
                return false;
            }
            //if ((((type != ContactMgr.UserType.UserTypeChatRoom) && (type != ContactMgr.UserType.UserTypeNormal)) && (type != ContactMgr.UserType.UserTypeQQ)) && ((type != ContactMgr.UserType.UserTypePlugin) || (((msg.strTalker != "weixin") && ((msg.strTalker != "fmessage") || (FMsgMgr.getFMsgType(msg) != FMsgType.other))) && ((!(msg.strTalker == ConstantValue.TAG_BLOGAPP) && !(msg.strTalker == ConstantValue.TAG_NEWS)) && !(msg.strTalker == ConstantValue.TAG_QQMAIL)))))
            if ((((type != ContactMgr.UserType.UserTypeChatRoom) && (type != ContactMgr.UserType.UserTypeNormal)) && (type != ContactMgr.UserType.UserTypeQQ)) && ((type != ContactMgr.UserType.UserTypePlugin) || (((msg.strTalker != "weixin") && ((msg.strTalker != "fmessage"))) && ((!(msg.strTalker == ConstantValue.TAG_BLOGAPP) && !(msg.strTalker == ConstantValue.TAG_NEWS)) && !(msg.strTalker == ConstantValue.TAG_QQMAIL)))))
            
            {
                return false;
            }
            return true;
        }

        public static bool IsSysInfo(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x2710);
        }

        public static bool IsTxt(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 1);
        }

        public static bool IsValidFmessage(this ChatMsg msg)
        {
            return (msg.nMsgType == 0x25);
        }

        public static bool IsVideo(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            if (msg.nMsgType != 0x2b)
            {
                return (msg.nMsgType == 0x2c);
            }
            return true;
        }

        public static bool IsVoice(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x22);
        }

        public static bool IsVoip(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 50);
        }

        public static bool IsVoipTime(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return (msg.nMsgType == 0x34);
        }

        public static bool isWithOfficialAcount(this ChatMsg msg)
        {
            if (msg == null)
            {
                return false;
            }
            return ((ContactMgr.getUserType(msg.strTalker) == ContactMgr.UserType.UserTypeNormal) && ContactHelper.IsVerifiedContact(msg.strTalker));
        }
    }
}

