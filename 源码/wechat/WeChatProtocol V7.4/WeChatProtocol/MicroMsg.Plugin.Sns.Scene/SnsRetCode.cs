namespace MicroMsg.Plugin.Sns.Scene
{
    using System;

    public enum SnsRetCode
    {
        MMSNS_MM_OK = 0,
        MMSNS_RET_BAN = 0xca,
        MMSNS_RET_CLIENTID_EXIST = 0xce,
        MMSNS_RET_COMMENT_HAVE_LIKE = 0xcc,
        MMSNS_RET_COMMENT_NOT_ALLOW = 0xcd,
        MMSNS_RET_COMMENT_PRIVACY = 0xd0,
        MMSNS_RET_ISALL = 0xcf,
        MMSNS_RET_PRIVACY = 0xcb,
        MMSNS_RET_SPAM = 0xc9
    }
}

