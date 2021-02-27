namespace MicroMsg.Protocol
{
    using System;

    public enum RetConst
    {
        MM_BOTTLE_COUNT_ERR = 0x10,
        MM_BOTTLE_ERR_UNKNOWNTYPE = 15,
        MM_BOTTLE_NOTEXIT = 0x11,
        MM_BOTTLE_PICKCOUNTINVALID = 0x13,
        MM_BOTTLE_UINNOTMATCH = 0x12,
        MM_ERR_ACCESS_DENIED = -5,
        MM_ERR_ACCOUNT_BAN = -200,
        MM_ERR_ALPHA_FORBIDDEN = -75,
        MM_ERR_ARG = -2,
        MM_ERR_AUTH_ANOTHERPLACE = -100,
        MM_ERR_BADEMAIL = -28,
        MM_ERR_BATCHGETCONTACTPROFILE_MODE = -45,
        MM_ERR_BIGBIZ_AUTH = -69,
        MM_ERR_BIND_EMAIL_SAME_AS_QMAIL = -86,
        MM_ERR_BINDED_BY_OTHER = -85,
        MM_ERR_BINDUIN_BINDED = -50,
        MM_ERR_BIZ_FANS_LIMITED = -87,
        MM_ERR_BLACKLIST = -22,
        MM_ERR_BOTTLEBANBYEXPOSE = -2002,
        MM_ERR_CLIENT = -800000,
        MM_ERR_CRITICALUPDATE = -16,
        MM_ERR_DOMAINDISABLE = -27,
        MM_ERR_DOMAINMAXLIMITED = -26,
        MM_ERR_DOMAINVERIFIED = -25,
        MM_ERR_EMAILEXIST = -8,
        MM_ERR_EMAILNOTVERIFY = -9,
        MM_ERR_FORCE_QUIT = -999999,
        MM_ERR_FREQ_LIMITED = -34,
        MM_ERR_GETMFRIEND_NOT_READY = -70,
        MM_ERR_GMAIL_IMAP = -63,
        MM_ERR_GMAIL_ONLINELIMITE = -61,
        MM_ERR_GMAIL_PWD = -60,
        MM_ERR_GMAIL_WEBLOGIN = -62,
        MM_ERR_HAS_BINDED = -84,
        MM_ERR_HAS_NO_HEADIMG = -53,
        MM_ERR_HAS_UNBINDED = -83,
        MM_ERR_HAVE_BIND_FACEBOOK = -67,
        MM_ERR_IDC_REDIRECT = -301,
        MM_ERR_IMG_READ = -1005,
        MM_ERR_INVALID_BIND_OPMODE = -37,
        MM_ERR_INVALID_GROUPCARD_CONTACT = -52,
        MM_ERR_INVALID_HDHEADIMG_REQ_TOTAL_LEN = -54,
        MM_ERR_INVALID_UPLOADMCONTACT_OPMODE = -38,
        MM_ERR_IS_NOT_OWNER = -66,
        MM_ERR_LBSBANBYEXPOSE = -2001,
        MM_ERR_LBSDATANOTFOUND = -2000,
        MM_ERR_MEMBER_TOOMUCH = -23,
        MM_ERR_MOBILE_BINDED = -35,
        MM_ERR_MOBILE_FORMAT = -41,
        MM_ERR_MOBILE_NEEDADJUST = -74,
        MM_ERR_MOBILE_NULL = -39,
        MM_ERR_MOBILE_UNBINDED = -36,
        MM_ERR_NEED_QQPWD = -49,
        MM_ERR_NEED_VERIFY = -6,
        MM_ERR_NEED_VERIFY_USER = -44,
        MM_ERR_NEEDREG = -30,
        MM_ERR_NEEDSECONDPWD = -31,
        MM_ERR_NEW_USER = -79,
        MM_ERR_NICEQQ_EXPIRED = -72,
        MM_ERR_NICKNAMEINVALID = -15,
        MM_ERR_NICKRESERVED = -11,
        MM_ERR_NO_BOTTLECOUNT = -56,
        MM_ERR_NO_HDHEADIMG = -55,
        MM_ERR_NODATA = -203,
        MM_ERR_NOTBINDQQ = -81,
        MM_ERR_NOTCHATROOMCONTACT = -21,
        MM_ERR_NOTMICROBLOGCONTACT = -20,
        MM_ERR_NOTOPENPRIVATEMSG = -19,
        MM_ERR_NOTQQCONTACT = -46,
        MM_ERR_NOUPDATEINFO = -18,
        MM_ERR_NOUSER = -4,
        MM_ERR_OIDBTIMEOUT = -29,
        MM_ERR_ONE_BINDTYPE_LEFT = -82,
        MM_ERR_OTHER_MAIN_ACCT = -204,
        MM_ERR_PARSE_MAIL = -64,
        MM_ERR_PASSWORD = -3,
        MM_ERR_PICKBOTTLE_NOBOTTLE = -58,
        MM_ERR_QQ_BAN = -201,
        MM_ERR_QQ_OK_NEED_MOBILE = -205,
        MM_ERR_QRCODEVERIFY_BANBYEXPOSE = -2004,
        MM_ERR_RECOMMENDEDUPDATE = -17,
        MM_ERR_SEND_VERIFYCODE = -57,
        MM_ERR_SESSIONTIMEOUT = -13,
        MM_ERR_SHAKE_TRAN_IMG_CANCEL = -90,
        MM_ERR_SHAKE_TRAN_IMG_CONTINUE = -92,
        MM_ERR_SHAKE_TRAN_IMG_NOTFOUND = -91,
        MM_ERR_SHAKE_TRAN_IMG_OTHER = -93,
        MM_ERR_SHAKEBANBYEXPOSE = -2003,
        MM_ERR_SPAM = -24,
        MM_ERR_SVR_MOBILE_FORMAT = -78,
        MM_ERR_SYS = -1,
        MM_ERR_TICKET_NOTFOUND = -48,
        MM_ERR_TICKET_UNMATCH = -47,
        MM_ERR_TOLIST_LIMITED = -71,
        MM_ERR_TRYQQPWD = -73,
        MM_ERR_UINEXIST = -12,
        MM_ERR_UNBIND_MAIN_ACCT = -206,
        MM_ERR_UNBIND_MOBILE_NEED_QQPWD = -202,
        MM_ERR_UNBIND_REGBYMOBILE = -65,
        MM_ERR_UNMATCH_MOBILE = -40,
        MM_ERR_UNSUPPORT_COUNTRY = -59,
        MM_ERR_USER_BIND_MOBILE = -43,
        MM_ERR_USER_MOBILE_UNMATCH = -42,
        MM_ERR_USEREXIST = -7,
        MM_ERR_USERNAMEINVALID = -14,
        MM_ERR_USERRESERVED = -10,
        MM_ERR_UUID_BINDED = -76,
        MM_ERR_VERIFYCODE_NOTEXIST = -51,
        MM_ERR_VERIFYCODE_TIMEOUT = -33,
        MM_ERR_VERIFYCODE_UNMATCH = -32,
        MM_ERR_WEIBO_PUSH_TRANS = -80,
        MM_ERR_WRONG_SESSION_KEY = -77,
        MM_FACEBOOK_ACCESSTOKEN_UNVALID = -68,
        MM_OK = 0,
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

