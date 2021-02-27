namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ManualAuthAesReqData")]
    public class ManualAuthAesReqData : IExtensible
    {
        private string _AdSource = "";
        private BaseAuthReqInfo _BaseReqInfo = null;
        private micromsg.BaseRequest _BaseRequest;
        private uint _BuiltinIPSeq;
        private string _BundleID = "";
        private int _Channel = 0;
        private SKBuiltinBuffer_t _Clientcheckdat = null;
        private string _ClientSeqID = "";
        private string _DeviceBrand = "";
        private string _DeviceModel = "";
        private string _DeviceName = "";
        private string _DeviceType = "";
        private string _IMEI = "";
        private uint _InputType;
        private string _IPhoneVer = "";
        private string _Language = "";
        private string _OSType = "";
        private string _RealCountry = "";
        private string _Signature = "";
        private string _SoftType = "";
        private uint _TimeStamp = 0;
        private string _TimeZone = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(20, IsRequired=false, Name="AdSource", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="BaseReqInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public BaseAuthReqInfo BaseReqInfo
        {
            get
            {
                return this._BaseReqInfo;
            }
            set
            {
                this._BaseReqInfo = value;
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

        [ProtoMember(5, IsRequired=true, Name="BuiltinIPSeq", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x13, IsRequired=false, Name="BundleID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x17, IsRequired=false, Name="Clientcheckdat", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Clientcheckdat
        {
            get
            {
                return this._Clientcheckdat;
            }
            set
            {
                this._Clientcheckdat = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ClientSeqID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(15, IsRequired=false, Name="DeviceBrand", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x10, IsRequired=false, Name="DeviceModel", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=false, Name="DeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=false, Name="DeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=false, Name="IMEI", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x16, IsRequired=true, Name="InputType", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x15, IsRequired=false, Name="IPhoneVer", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(10, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x11, IsRequired=false, Name="OSType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="SoftType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(14, IsRequired=false, Name="TimeStamp", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(11, IsRequired=false, Name="TimeZone", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

