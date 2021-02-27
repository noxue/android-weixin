namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewAuthRequest")]
    public class NewAuthRequest : IExtensible
    {
        private string _AdSource = "";
        private string _AuthTicket = "";
        private string _AutoAuthTicket = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _BuiltinIPSeq = 0;
        private string _BundleID = "";
        private int _Channel = 0;
        private SKBuiltinBuffer_t _CliDBEncryptInfo = null;
        private SKBuiltinBuffer_t _CliDBEncryptKey = null;
        private string _ClientSeqID = "";
        private string _DeviceBrand = "";
        private string _DeviceModel = "";
        private string _DeviceName = "";
        private string _DeviceType = "";
        private string _extPwd = "";
        private string _extPwd2 = "";
        private string _IMEI = "";
        private SKBuiltinString_t _ImgCode;
        private SKBuiltinString_t _ImgEncryptKey = null;
        private SKBuiltinString_t _ImgSid;
        private uint _InputType = 0;
        private string _IPhoneVer = "";
        private SKBuiltinBuffer_t _KSid = null;
        private string _Language = "";
        private string _OSType = "";
        private SKBuiltinString_t _Pwd;
        private string _Pwd2 = "";
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _RealCountry = "";
        private uint _SessionMode = 0;
        private string _Signature = "";
        private string _SoftType = "";
        private uint _TimeStamp = 0;
        private string _TimeZone = "";
        private SKBuiltinString_t _UserName;
        private string _VerifyContent = "";
        private string _VerifySignature = "";
        private SKBuiltinBuffer_t _WTLoginReqBuff = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x21, IsRequired=false, Name="AdSource", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x17, IsRequired=false, Name="AuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x1b, IsRequired=false, Name="AutoAuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="BuiltinIPSeq", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x20, IsRequired=false, Name="BundleID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(13, IsRequired=false, Name="Channel", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Channel
        {
            get
            {
                return this._Channel;
            }
            set
            {
                this._Channel = value;
            }
        }

        [ProtoMember(0x23, IsRequired=false, Name="CliDBEncryptInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(0x22, IsRequired=false, Name="CliDBEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(0x12, IsRequired=false, Name="DeviceBrand", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceBrand
        {
            get
            {
                return this._DeviceBrand;
            }
            set
            {
                this._DeviceBrand = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="DeviceModel", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceModel
        {
            get
            {
                return this._DeviceModel;
            }
            set
            {
                this._DeviceModel = value;
            }
        }

        [ProtoMember(0x1c, IsRequired=false, Name="DeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceName
        {
            get
            {
                return this._DeviceName;
            }
            set
            {
                this._DeviceName = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="DeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceType
        {
            get
            {
                return this._DeviceType;
            }
            set
            {
                this._DeviceType = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="extPwd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string extPwd
        {
            get
            {
                return this._extPwd;
            }
            set
            {
                this._extPwd = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="extPwd2", DataFormat=DataFormat.Default), DefaultValue("")]
        public string extPwd2
        {
            get
            {
                return this._extPwd2;
            }
            set
            {
                this._extPwd2 = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="IMEI", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IMEI
        {
            get
            {
                return this._IMEI;
            }
            set
            {
                this._IMEI = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ImgCode", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ImgCode
        {
            get
            {
                return this._ImgCode;
            }
            set
            {
                this._ImgCode = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(4, IsRequired=true, Name="ImgSid", DataFormat=DataFormat.Default)]
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

        [ProtoMember(30, IsRequired=false, Name="InputType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InputType
        {
            get
            {
                return this._InputType;
            }
            set
            {
                this._InputType = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="IPhoneVer", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IPhoneVer
        {
            get
            {
                return this._IPhoneVer;
            }
            set
            {
                this._IPhoneVer = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="KSid", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(11, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(20, IsRequired=false, Name="OSType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OSType
        {
            get
            {
                return this._OSType;
            }
            set
            {
                this._OSType = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Pwd", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t Pwd
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

        [ProtoMember(6, IsRequired=false, Name="Pwd2", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd2
        {
            get
            {
                return this._Pwd2;
            }
            set
            {
                this._Pwd2 = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(0x18, IsRequired=false, Name="RealCountry", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x26, IsRequired=false, Name="SessionMode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SessionMode
        {
            get
            {
                return this._SessionMode;
            }
            set
            {
                this._SessionMode = value;
            }
        }

        [ProtoMember(0x19, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Signature
        {
            get
            {
                return this._Signature;
            }
            set
            {
                this._Signature = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="SoftType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SoftType
        {
            get
            {
                return this._SoftType;
            }
            set
            {
                this._SoftType = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="TimeStamp", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(10, IsRequired=false, Name="TimeZone", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
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

        [ProtoMember(0x25, IsRequired=false, Name="VerifyContent", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x24, IsRequired=false, Name="VerifySignature", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x1d, IsRequired=false, Name="WTLoginReqBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t WTLoginReqBuff
        {
            get
            {
                return this._WTLoginReqBuff;
            }
            set
            {
                this._WTLoginReqBuff = value;
            }
        }
    }
}

