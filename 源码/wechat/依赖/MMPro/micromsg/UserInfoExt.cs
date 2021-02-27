namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UserInfoExt")]
    public class UserInfoExt : IExtensible
    {
        private string _BBMNickName = "";
        private string _BBPIN = "";
        private string _BBPPID = "";
        private uint _BigChatRoomInvite = 0;
        private uint _BigChatRoomQuota = 0;
        private uint _BigChatRoomSize = 0;
        private string _BigHeadImgUrl = "";
        private SKBuiltinString_t _ExtXml = null;
        private string _GoogleContactName = "";
        private uint _GrayscaleFlag = 0;
        private string _IDCardNum = "";
        private string _KFInfo = "";
        private micromsg.LinkedinContactItem _LinkedinContactItem = null;
        private uint _MainAcctType = 0;
        private string _MsgPushSound = "";
        private string _MyBrandList = "";
        private micromsg.PatternLockInfo _PatternLockInfo = null;
        private uint _PayWalletType = 0;
        private string _RealName = "";
        private string _RegCountry = "";
        private uint _SafeDevice = 0;
        private micromsg.SafeDeviceList _SafeDeviceList = null;
        private string _SafeMobile = "";
        private string _SecurityDeviceId = "";
        private string _SmallHeadImgUrl = "";
        private micromsg.SnsUserInfo _SnsUserInfo;
        private string _VoipPushSound = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x16, IsRequired=false, Name="BBMNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBMNickName
        {
            get
            {
                return this._BBMNickName;
            }
            set
            {
                this._BBMNickName = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="BBPIN", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBPIN
        {
            get
            {
                return this._BBPIN;
            }
            set
            {
                this._BBPIN = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="BBPPID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBPPID
        {
            get
            {
                return this._BBPPID;
            }
            set
            {
                this._BBPPID = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="BigChatRoomInvite", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BigChatRoomInvite
        {
            get
            {
                return this._BigChatRoomInvite;
            }
            set
            {
                this._BigChatRoomInvite = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="BigChatRoomQuota", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BigChatRoomQuota
        {
            get
            {
                return this._BigChatRoomQuota;
            }
            set
            {
                this._BigChatRoomQuota = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="BigChatRoomSize", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BigChatRoomSize
        {
            get
            {
                return this._BigChatRoomSize;
            }
            set
            {
                this._BigChatRoomSize = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BigHeadImgUrl
        {
            get
            {
                return this._BigHeadImgUrl;
            }
            set
            {
                this._BigHeadImgUrl = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="ExtXml", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ExtXml
        {
            get
            {
                return this._ExtXml;
            }
            set
            {
                this._ExtXml = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="GoogleContactName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GoogleContactName
        {
            get
            {
                return this._GoogleContactName;
            }
            set
            {
                this._GoogleContactName = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="GrayscaleFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GrayscaleFlag
        {
            get
            {
                return this._GrayscaleFlag;
            }
            set
            {
                this._GrayscaleFlag = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="IDCardNum", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IDCardNum
        {
            get
            {
                return this._IDCardNum;
            }
            set
            {
                this._IDCardNum = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="KFInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string KFInfo
        {
            get
            {
                return this._KFInfo;
            }
            set
            {
                this._KFInfo = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="LinkedinContactItem", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.LinkedinContactItem LinkedinContactItem
        {
            get
            {
                return this._LinkedinContactItem;
            }
            set
            {
                this._LinkedinContactItem = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="MainAcctType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MainAcctType
        {
            get
            {
                return this._MainAcctType;
            }
            set
            {
                this._MainAcctType = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="MsgPushSound", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MsgPushSound
        {
            get
            {
                return this._MsgPushSound;
            }
            set
            {
                this._MsgPushSound = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="MyBrandList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MyBrandList
        {
            get
            {
                return this._MyBrandList;
            }
            set
            {
                this._MyBrandList = value;
            }
        }

        [ProtoMember(0x19, IsRequired=false, Name="PatternLockInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.PatternLockInfo PatternLockInfo
        {
            get
            {
                return this._PatternLockInfo;
            }
            set
            {
                this._PatternLockInfo = value;
            }
        }

        [ProtoMember(0x1b, IsRequired=false, Name="PayWalletType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PayWalletType
        {
            get
            {
                return this._PayWalletType;
            }
            set
            {
                this._PayWalletType = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="RealName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RealName
        {
            get
            {
                return this._RealName;
            }
            set
            {
                this._RealName = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="RegCountry", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RegCountry
        {
            get
            {
                return this._RegCountry;
            }
            set
            {
                this._RegCountry = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="SafeDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SafeDevice
        {
            get
            {
                return this._SafeDevice;
            }
            set
            {
                this._SafeDevice = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="SafeDeviceList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.SafeDeviceList SafeDeviceList
        {
            get
            {
                return this._SafeDeviceList;
            }
            set
            {
                this._SafeDeviceList = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="SafeMobile", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SafeMobile
        {
            get
            {
                return this._SafeMobile;
            }
            set
            {
                this._SafeMobile = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="SecurityDeviceId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SecurityDeviceId
        {
            get
            {
                return this._SecurityDeviceId;
            }
            set
            {
                this._SecurityDeviceId = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallHeadImgUrl
        {
            get
            {
                return this._SmallHeadImgUrl;
            }
            set
            {
                this._SmallHeadImgUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="SnsUserInfo", DataFormat=DataFormat.Default)]
        public micromsg.SnsUserInfo SnsUserInfo
        {
            get
            {
                return this._SnsUserInfo;
            }
            set
            {
                this._SnsUserInfo = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="VoipPushSound", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VoipPushSound
        {
            get
            {
                return this._VoipPushSound;
            }
            set
            {
                this._VoipPushSound = value;
            }
        }
    }
}

