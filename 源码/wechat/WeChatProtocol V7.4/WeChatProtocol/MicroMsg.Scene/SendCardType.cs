namespace MicroMsg.Scene
{
    using System;

    public enum SendCardType : uint
    {
        MM_SENDCARD_FACEBOOK = 0x80,
        MM_SENDCARD_MODIFYHEADIMG = 0x20,
        MM_SENDCARD_QQBLOG = 1,
        MM_SENDCARD_QQFRIEND = 8,
        MM_SENDCARD_QQSIGN = 2,
        MM_SENDCARD_QZONE = 4,
        MM_SENDCARD_READER = 0x40,
        MM_SENDCARD_SINABLOG = 0x10
    }
}

