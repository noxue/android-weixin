namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RegEquipmentResponse")]
    public class RegEquipmentResponse : IExtensible
    {
        private string _AuthKey = "";
        private micromsg.BaseResponse _BaseResponse;
        private string _BindEmail = "";
        private micromsg.BuiltinIPList _BuiltinIPList;
        private string _FSURL = "";
        private micromsg.NetworkControl _NetworkControl;
        private HostList _NewHostList;
        private string _OfficialNickName = "";
        private string _OfficialUserName = "";
        private string _Password = "";
        private micromsg.PluginKeyList _PluginKeyList;
        private uint _ProfileFlag;
        private string _PushMailSettingTicket = "";
        private uint _PushMailStatus;
        private string _QQMicroBlogUserName = "";
        private uint _RegType;
        private uint _ReturnFlag;
        private uint _SendCardBitFlag;
        private string _SessionKey = "";
        private uint _Status;
        private uint _Uin;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x15, IsRequired=false, Name="AuthKey", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="BindEmail", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BindEmail
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

        [ProtoMember(14, IsRequired=true, Name="BuiltinIPList", DataFormat=DataFormat.Default)]
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

        [ProtoMember(15, IsRequired=false, Name="FSURL", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x12, IsRequired=true, Name="NetworkControl", DataFormat=DataFormat.Default)]
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

        [ProtoMember(0x19, IsRequired=true, Name="NewHostList", DataFormat=DataFormat.Default)]
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

        [ProtoMember(5, IsRequired=false, Name="OfficialNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OfficialNickName
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

        [ProtoMember(4, IsRequired=false, Name="OfficialUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OfficialUserName
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

        [ProtoMember(0x17, IsRequired=false, Name="Password", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x16, IsRequired=true, Name="PluginKeyList", DataFormat=DataFormat.Default)]
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

        [ProtoMember(0x18, IsRequired=true, Name="ProfileFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(10, IsRequired=false, Name="PushMailSettingTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=true, Name="PushMailStatus", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=false, Name="QQMicroBlogUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QQMicroBlogUserName
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

        [ProtoMember(20, IsRequired=true, Name="RegType", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x13, IsRequired=true, Name="ReturnFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ReturnFlag
        {
            get
            {
                return this._ReturnFlag;
            }
            set
            {
                this._ReturnFlag = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="SendCardBitFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=false, Name="SessionKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SessionKey
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

        [ProtoMember(0x11, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x10, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
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
    }
}

