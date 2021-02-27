namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewAuthResponse")]
    public class NewAuthResponse : IExtensible
    {
        private SKBuiltinBuffer_t _A2Key = null;
        private string _Alias = "";
        private CDNDnsInfo _AppDnsInfo = null;
        private string _ApplyBetaUrl = "";
        private string _AuthKey = "";
        private string _AuthTicket = "";
        private string _AutoAuthTicket = "";
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinString_t _BindEmail;
        private SKBuiltinString_t _BindMobile;
        private uint _BindUin;
        private micromsg.BuiltinIPList _BuiltinIPList = null;
        private SKBuiltinBuffer_t _CliDBEncryptInfo = null;
        private SKBuiltinBuffer_t _CliDBEncryptKey = null;
        private string _DeviceInfoXml = "";
        private CDNDnsInfo _DnsInfo = null;
        private uint _Flag = 0;
        private string _FSURL = "";
        private string _HintMsg = "";
        private SKBuiltinBuffer_t _ImgBuf;
        private SKBuiltinString_t _ImgEncryptKey = null;
        private SKBuiltinString_t _ImgSid;
        private uint _IsAutoReg = 0;
        private string _KickResponse = "";
        private SKBuiltinBuffer_t _KSid = null;
        private uint _NeedSetEmailPwd = 0;
        private micromsg.NetworkControl _NetworkControl = null;
        private HostList _NewHostList = null;
        private uint _NewVersion = 0;
        private uint _NextAuthType = 0;
        private SKBuiltinString_t _NickName;
        private uint _ObsoleteItem1 = 0;
        private SKBuiltinString_t _OfficialNickName;
        private SKBuiltinString_t _OfficialUserName;
        private string _Password = "";
        private uint _PluginFlag = 0;
        private micromsg.PluginKeyList _PluginKeyList = null;
        private uint _ProfileFlag = 0;
        private string _PushMailSettingTicket = "";
        private uint _PushMailStatus = 0;
        private uint _QQMicroBlogStatus = 0;
        private SKBuiltinString_t _QQMicroBlogUserName = null;
        private uint _RegType = 0;
        private uint _SafeDevice = 0;
        private uint _SendCardBitFlag = 0;
        private byte[] _SessionKey = null;
        private ShowStyleKey _ShowStyle = null;
        private string _Sid = "";
        private CDNDnsInfo _SnsDnsInfo = null;
        private string _SoftConfigXml = "";
        private uint _Status;
        private string _Ticket = "";
        private uint _TimeStamp = 0;
        private uint _Uin;
        private SKBuiltinString_t _UserName;
        private SKBuiltinBuffer_t _VerifyBuff = null;
        private string _VerifySignature = "";
        private SKBuiltinBuffer_t _WTLoginRspBuff = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x1f, IsRequired=false, Name="A2Key", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t A2Key
        {
            get
            {
                return this._A2Key;
            }
            set
            {
                this._A2Key = value;
            }
        }

        [ProtoMember(0x19, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Alias
        {
            get
            {
                return this._Alias;
            }
            set
            {
                this._Alias = value;
            }
        }

        [ProtoMember(0x38, IsRequired=false, Name="AppDnsInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public CDNDnsInfo AppDnsInfo
        {
            get
            {
                return this._AppDnsInfo;
            }
            set
            {
                this._AppDnsInfo = value;
            }
        }

        [ProtoMember(0x26, IsRequired=false, Name="ApplyBetaUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ApplyBetaUrl
        {
            get
            {
                return this._ApplyBetaUrl;
            }
            set
            {
                this._ApplyBetaUrl = value;
            }
        }

        [ProtoMember(0x1b, IsRequired=false, Name="AuthKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthKey
        {
            get
            {
                return this._AuthKey;
            }
            set
            {
                this._AuthKey = value;
            }
        }

        [ProtoMember(0x2a, IsRequired=false, Name="AuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthTicket
        {
            get
            {
                return this._AuthTicket;
            }
            set
            {
                this._AuthTicket = value;
            }
        }

        [ProtoMember(0x2f, IsRequired=false, Name="AutoAuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AutoAuthTicket
        {
            get
            {
                return this._AutoAuthTicket;
            }
            set
            {
                this._AutoAuthTicket = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="BindEmail", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t BindEmail
        {
            get
            {
                return this._BindEmail;
            }
            set
            {
                this._BindEmail = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="BindMobile", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t BindMobile
        {
            get
            {
                return this._BindMobile;
            }
            set
            {
                this._BindMobile = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="BindUin", DataFormat=DataFormat.TwosComplement)]
        public uint BindUin
        {
            get
            {
                return this._BindUin;
            }
            set
            {
                this._BindUin = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="BuiltinIPList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.BuiltinIPList BuiltinIPList
        {
            get
            {
                return this._BuiltinIPList;
            }
            set
            {
                this._BuiltinIPList = value;
            }
        }

        [ProtoMember(0x35, IsRequired=false, Name="CliDBEncryptInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t CliDBEncryptInfo
        {
            get
            {
                return this._CliDBEncryptInfo;
            }
            set
            {
                this._CliDBEncryptInfo = value;
            }
        }

        [ProtoMember(0x34, IsRequired=false, Name="CliDBEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t CliDBEncryptKey
        {
            get
            {
                return this._CliDBEncryptKey;
            }
            set
            {
                this._CliDBEncryptKey = value;
            }
        }

        [ProtoMember(0x27, IsRequired=false, Name="DeviceInfoXml", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceInfoXml
        {
            get
            {
                return this._DeviceInfoXml;
            }
            set
            {
                this._DeviceInfoXml = value;
            }
        }

        [ProtoMember(0x30, IsRequired=false, Name="DnsInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public CDNDnsInfo DnsInfo
        {
            get
            {
                return this._DnsInfo;
            }
            set
            {
                this._DnsInfo = value;
            }
        }

        [ProtoMember(0x36, IsRequired=false, Name="Flag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="FSURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FSURL
        {
            get
            {
                return this._FSURL;
            }
            set
            {
                this._FSURL = value;
            }
        }

        [ProtoMember(0x2e, IsRequired=false, Name="HintMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string HintMsg
        {
            get
            {
                return this._HintMsg;
            }
            set
            {
                this._HintMsg = value;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="ImgBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ImgBuf
        {
            get
            {
                return this._ImgBuf;
            }
            set
            {
                this._ImgBuf = value;
            }
        }

        [ProtoMember(30, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ImgEncryptKey
        {
            get
            {
                return this._ImgEncryptKey;
            }
            set
            {
                this._ImgEncryptKey = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="ImgSid", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ImgSid
        {
            get
            {
                return this._ImgSid;
            }
            set
            {
                this._ImgSid = value;
            }
        }

        [ProtoMember(0x24, IsRequired=false, Name="IsAutoReg", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IsAutoReg
        {
            get
            {
                return this._IsAutoReg;
            }
            set
            {
                this._IsAutoReg = value;
            }
        }

        [ProtoMember(0x25, IsRequired=false, Name="KickResponse", DataFormat=DataFormat.Default), DefaultValue("")]
        public string KickResponse
        {
            get
            {
                return this._KickResponse;
            }
            set
            {
                this._KickResponse = value;
            }
        }

        [ProtoMember(0x20, IsRequired=false, Name="KSid", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t KSid
        {
            get
            {
                return this._KSid;
            }
            set
            {
                this._KSid = value;
            }
        }

        [ProtoMember(0x2d, IsRequired=false, Name="NeedSetEmailPwd", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NeedSetEmailPwd
        {
            get
            {
                return this._NeedSetEmailPwd;
            }
            set
            {
                this._NeedSetEmailPwd = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="NetworkControl", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.NetworkControl NetworkControl
        {
            get
            {
                return this._NetworkControl;
            }
            set
            {
                this._NetworkControl = value;
            }
        }

        [ProtoMember(0x29, IsRequired=false, Name="NewHostList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public HostList NewHostList
        {
            get
            {
                return this._NewHostList;
            }
            set
            {
                this._NewHostList = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="NewVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NewVersion
        {
            get
            {
                return this._NewVersion;
            }
            set
            {
                this._NewVersion = value;
            }
        }

        [ProtoMember(0x31, IsRequired=false, Name="NextAuthType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NextAuthType
        {
            get
            {
                return this._NextAuthType;
            }
            set
            {
                this._NextAuthType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="NickName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        [ProtoMember(0x2c, IsRequired=false, Name="ObsoleteItem1", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ObsoleteItem1
        {
            get
            {
                return this._ObsoleteItem1;
            }
            set
            {
                this._ObsoleteItem1 = value;
            }
        }

        [ProtoMember(13, IsRequired=true, Name="OfficialNickName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t OfficialNickName
        {
            get
            {
                return this._OfficialNickName;
            }
            set
            {
                this._OfficialNickName = value;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="OfficialUserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t OfficialUserName
        {
            get
            {
                return this._OfficialUserName;
            }
            set
            {
                this._OfficialUserName = value;
            }
        }

        [ProtoMember(0x22, IsRequired=false, Name="Password", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="PluginFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PluginFlag
        {
            get
            {
                return this._PluginFlag;
            }
            set
            {
                this._PluginFlag = value;
            }
        }

        [ProtoMember(0x1d, IsRequired=false, Name="PluginKeyList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.PluginKeyList PluginKeyList
        {
            get
            {
                return this._PluginKeyList;
            }
            set
            {
                this._PluginKeyList = value;
            }
        }

        [ProtoMember(0x21, IsRequired=false, Name="ProfileFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ProfileFlag
        {
            get
            {
                return this._ProfileFlag;
            }
            set
            {
                this._ProfileFlag = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="PushMailSettingTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PushMailSettingTicket
        {
            get
            {
                return this._PushMailSettingTicket;
            }
            set
            {
                this._PushMailSettingTicket = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="PushMailStatus", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PushMailStatus
        {
            get
            {
                return this._PushMailStatus;
            }
            set
            {
                this._PushMailStatus = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="QQMicroBlogStatus", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint QQMicroBlogStatus
        {
            get
            {
                return this._QQMicroBlogStatus;
            }
            set
            {
                this._QQMicroBlogStatus = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="QQMicroBlogUserName", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t QQMicroBlogUserName
        {
            get
            {
                return this._QQMicroBlogUserName;
            }
            set
            {
                this._QQMicroBlogUserName = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="RegType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RegType
        {
            get
            {
                return this._RegType;
            }
            set
            {
                this._RegType = value;
            }
        }

        [ProtoMember(0x2b, IsRequired=false, Name="SafeDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x13, IsRequired=false, Name="SendCardBitFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SendCardBitFlag
        {
            get
            {
                return this._SendCardBitFlag;
            }
            set
            {
                this._SendCardBitFlag = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="SessionKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] SessionKey
        {
            get
            {
                return this._SessionKey;
            }
            set
            {
                this._SessionKey = value;
            }
        }

        [ProtoMember(0x33, IsRequired=false, Name="ShowStyle", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ShowStyleKey ShowStyle
        {
            get
            {
                return this._ShowStyle;
            }
            set
            {
                this._ShowStyle = value;
            }
        }

        [ProtoMember(0x1c, IsRequired=false, Name="Sid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Sid
        {
            get
            {
                return this._Sid;
            }
            set
            {
                this._Sid = value;
            }
        }

        [ProtoMember(0x37, IsRequired=false, Name="SnsDnsInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public CDNDnsInfo SnsDnsInfo
        {
            get
            {
                return this._SnsDnsInfo;
            }
            set
            {
                this._SnsDnsInfo = value;
            }
        }

        [ProtoMember(40, IsRequired=false, Name="SoftConfigXml", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SoftConfigXml
        {
            get
            {
                return this._SoftConfigXml;
            }
            set
            {
                this._SoftConfigXml = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="Ticket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                this._Ticket = value;
            }
        }

        [ProtoMember(0x23, IsRequired=false, Name="TimeStamp", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TimeStamp
        {
            get
            {
                return this._TimeStamp;
            }
            set
            {
                this._TimeStamp = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Uin", DataFormat=DataFormat.TwosComplement)]
        public uint Uin
        {
            get
            {
                return this._Uin;
            }
            set
            {
                this._Uin = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        [ProtoMember(0x3a, IsRequired=false, Name="VerifyBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t VerifyBuff
        {
            get
            {
                return this._VerifyBuff;
            }
            set
            {
                this._VerifyBuff = value;
            }
        }

        [ProtoMember(0x39, IsRequired=false, Name="VerifySignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifySignature
        {
            get
            {
                return this._VerifySignature;
            }
            set
            {
                this._VerifySignature = value;
            }
        }

        [ProtoMember(50, IsRequired=false, Name="WTLoginRspBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t WTLoginRspBuff
        {
            get
            {
                return this._WTLoginRspBuff;
            }
            set
            {
                this._WTLoginRspBuff = value;
            }
        }
    }
}

