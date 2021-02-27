namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AutoAuthAesReqData")]
    public class AutoAuthAesReqData : IExtensible
    {
        private SKBuiltinBuffer_t _AutoAuthKey;
        private BaseAuthReqInfo _BaseReqInfo = null;
        private micromsg.BaseRequest _BaseRequest;
        private uint _BuiltinIPSeq;
        private string _ClientSeqID = "";
        private string _DeviceName = "";
        private string _DeviceType = "";
        private string _IMEI = "";
        private string _Language = "";
        private string _Signature = "";
        private string _SoftType = "";
        private string _TimeZone = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="AutoAuthKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t AutoAuthKey
        {
            get
            {
                return this._AutoAuthKey;
            }
            set
            {
                this._AutoAuthKey = value;
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

        [ProtoMember(6, IsRequired=true, Name="BuiltinIPSeq", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(7, IsRequired=false, Name="ClientSeqID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=false, Name="DeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(10, IsRequired=false, Name="DeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="IMEI", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=false, Name="SoftType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(12, IsRequired=false, Name="TimeZone", DataFormat=DataFormat.Default), DefaultValue("")]
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

