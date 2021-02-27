namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewRegRequest")]
    public class NewRegRequest : IExtensible
    {
        private string _AdSource = "";
        private string _Alias = "";
        private string _AndroidID = "";
        private string _AndroidInstallRef = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _BindEmail = "";
        private string _BindMobile = "";
        private uint _BindUin = 0;
        private uint _BuiltinIPSeq = 0;
        private string _BundleID = "";
        private string _ClientFingerprint = "";
        private string _ClientSeqID = "";
        private ECDHKey _CliPubECDHKey = null;
        private uint _DLSrc = 0;
        private uint _ForceReg = 0;
        private string _GoogleAid = "";
        private uint _HasHeadImg = 0;
        private string _Language = "";
        private string _MacAddr = "";
        private string _NickName = "";
        private string _Pwd = "";
        private SKBuiltinBuffer_t _RandomEncryKey = null;
        private string _RealCountry = "";
        private uint _RegMode = 0;
        private uint _SuggestRet = 0;
        private string _Ticket = "";
        private string _TimeZone = "";
        private string _UserName = "";
        private string _VerifyContent = "";
        private string _VerifySignature = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x20, IsRequired=false, Name="AdSource", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AdSource
        {
            get
            {
                return this._AdSource;
            }
            set
            {
                this._AdSource = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x21, IsRequired=false, Name="AndroidID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AndroidID
        {
            get
            {
                return this._AndroidID;
            }
            set
            {
                this._AndroidID = value;
            }
        }

        [ProtoMember(0x23, IsRequired=false, Name="AndroidInstallRef", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AndroidInstallRef
        {
            get
            {
                return this._AndroidInstallRef;
            }
            set
            {
                this._AndroidInstallRef = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="BindEmail", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="BindMobile", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BindMobile
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

        [ProtoMember(5, IsRequired=false, Name="BindUin", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(12, IsRequired=false, Name="BuiltinIPSeq", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BuiltinIPSeq
        {
            get
            {
                return this._BuiltinIPSeq;
            }
            set
            {
                this._BuiltinIPSeq = value;
            }
        }

        [ProtoMember(0x27, IsRequired=false, Name="BundleID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BundleID
        {
            get
            {
                return this._BundleID;
            }
            set
            {
                this._BundleID = value;
            }
        }

        [ProtoMember(0x24, IsRequired=false, Name="ClientFingerprint", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientFingerprint
        {
            get
            {
                return this._ClientFingerprint;
            }
            set
            {
                this._ClientFingerprint = value;
            }
        }

        [ProtoMember(0x1f, IsRequired=false, Name="ClientSeqID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientSeqID
        {
            get
            {
                return this._ClientSeqID;
            }
            set
            {
                this._ClientSeqID = value;
            }
        }

        [ProtoMember(0x25, IsRequired=false, Name="CliPubECDHKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ECDHKey CliPubECDHKey
        {
            get
            {
                return this._CliPubECDHKey;
            }
            set
            {
                this._CliPubECDHKey = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="DLSrc", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DLSrc
        {
            get
            {
                return this._DLSrc;
            }
            set
            {
                this._DLSrc = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="ForceReg", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ForceReg
        {
            get
            {
                return this._ForceReg;
            }
            set
            {
                this._ForceReg = value;
            }
        }

        [ProtoMember(0x26, IsRequired=false, Name="GoogleAid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GoogleAid
        {
            get
            {
                return this._GoogleAid;
            }
            set
            {
                this._GoogleAid = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="HasHeadImg", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint HasHeadImg
        {
            get
            {
                return this._HasHeadImg;
            }
            set
            {
                this._HasHeadImg = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this._Language = value;
            }
        }

        [ProtoMember(0x22, IsRequired=false, Name="MacAddr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MacAddr
        {
            get
            {
                return this._MacAddr;
            }
            set
            {
                this._MacAddr = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NickName
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

        [ProtoMember(3, IsRequired=false, Name="Pwd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd
        {
            get
            {
                return this._Pwd;
            }
            set
            {
                this._Pwd = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="RandomEncryKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t RandomEncryKey
        {
            get
            {
                return this._RandomEncryKey;
            }
            set
            {
                this._RandomEncryKey = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="RealCountry", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RealCountry
        {
            get
            {
                return this._RealCountry;
            }
            set
            {
                this._RealCountry = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="RegMode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RegMode
        {
            get
            {
                return this._RegMode;
            }
            set
            {
                this._RegMode = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="SuggestRet", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SuggestRet
        {
            get
            {
                return this._SuggestRet;
            }
            set
            {
                this._SuggestRet = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Ticket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(15, IsRequired=false, Name="TimeZone", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TimeZone
        {
            get
            {
                return this._TimeZone;
            }
            set
            {
                this._TimeZone = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x16, IsRequired=false, Name="VerifyContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyContent
        {
            get
            {
                return this._VerifyContent;
            }
            set
            {
                this._VerifyContent = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="VerifySignature", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

