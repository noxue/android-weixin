namespace MicroMsg.Scene
{
    using System;

    public enum SyncCmdID
    {
        CmdIdAddMsg = 5,
        CmdIdCloseMicroBlog = 12,
        CmdIdDelChatContact = 7,
        CmdIdDelContact = 4,
        CmdIdDelContactMsg = 8,
        CmdIdDelMsg = 9,
        CmdIdDelUserDomainEmail = 0x13,
        CmdIdFunctionSwitch = 0x17,
        CmdIdInviteFriendOpen = 0x16,
        CmdIdMax = 0x30,
        CmdIdModChatRoomMember = 15,
        CmdIdModChatRoomNotify = 20,
        CmdIdModChatRoomTopic = 0x1b,
        CmdIdModContact = 2,
        CmdIdModContactDomainEmail = 0x11,
        CmdIdModMicroBlog = 13,
        CmdIdModMsgStatus = 6,
        CmdIdModNotifyStatus = 14,
        CmdIdModQContact = 0x18,
        CmdIdModTContact = 0x19,
        CmdIdModUserDomainEmail = 0x12,
        CmdIdModUserInfo = 1,
        CmdIdOpenQQMicroBlog = 11,
        CmdIdPossibleFriend = 0x15,
        CmdIdPsmStat = 0x1a,
        CmdIdQuitChatRoom = 0x10,
        CmdIdReport = 10,
        CmdInvalid = 0,
        MM_SNS_SYNCCMD_ACTION = 0x2e,
        MM_SNS_SYNCCMD_OBJECT = 0x2d,
        MM_SYNCCMD_BRAND_SETTING = 0x2f,
        MM_SYNCCMD_DELBOTTLECONTACT = 0x22,
        MM_SYNCCMD_DELETEBOTTLE = 0x20,
        MM_SYNCCMD_KVSTAT = 0x24,
        MM_SYNCCMD_MODBOTTLECONTACT = 0x21,
        MM_SYNCCMD_MODDISTURBSETTING = 0x1f,
        MM_SYNCCMD_MODUSERIMG = 0x23,
        MM_SYNCCMD_UPDATESTAT = 30,
        MM_SYNCCMD_USERINFOEXT = 0x2c,
        NN_SYNCCMD_THEMESTAT = 0x25
    }
}

