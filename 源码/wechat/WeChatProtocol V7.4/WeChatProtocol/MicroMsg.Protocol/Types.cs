namespace MicroMsg.Protocol
{
    using System;

    public class Types
    {
        public static class FriendType
        {
            public const int MM_FRIENDTYPE_BLOG = 1;
            public const int MM_FRIENDTYPE_EMAIL = 2;
            public const int MM_FRIENDTYPE_QQ = 0;
            public const int MM_FRIENDTYPE_SMS = 4;
            public const int MM_FRIENDTYPE_WEIXIN = 3;
        }

        public static class MMContactType
        {
            public const int MM_CONTACT_CHATROOM = 2;
            public const int MM_CONTACT_EMAIL = 3;
            public const int MM_CONTACT_QQ = 4;
            public const int MM_CONTACT_QQMICROBLOG = 1;
            public const int MM_CONTACT_WEIXIN = 0;
        }

        public static class MMMicroBlogType
        {
            public const int MM_MICROBLOG_QQ = 1;
        }

        public static class MMSexType
        {
            public const int MM_SEX_FEMALE = 2;
            public const int MM_SEX_MALE = 1;
            public const int MM_SEX_UNKNOWN = 0;
        }

        public static class QQGroupOpType
        {
            public const int MM_OP_FRIEND = 1;
            public const int MM_OP_GROUP = 0;
        }

        public static class SendCardType
        {
            public const int MM_SENDCARD_MODIFYHEADIMG = 0x20;
            public const int MM_SENDCARD_QQBLOG = 1;
            public const int MM_SENDCARD_QQFRIEND = 8;
            public const int MM_SENDCARD_QQSIGN = 2;
            public const int MM_SENDCARD_QZONE = 4;
            public const int MM_SENDCARD_SINABLOG = 0x10;
        }

        public static class WeiXinStatus
        {
            public const int MM_WEiXinStatus_FRIEND = 2;
            public const int MM_WEiXinStatus_NOREG = 0;
            public const int MM_WEiXinStatus_REG = 1;
        }
    }
}

