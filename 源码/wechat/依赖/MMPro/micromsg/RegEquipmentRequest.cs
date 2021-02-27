namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RegEquipmentRequest")]
    public class RegEquipmentRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _BuiltinIPSeq;
        private uint _DLSrc;
        private uint _ForceReg;
        private string _IMEI = "";
        private string _Language = "";
        private string _MAC = "";
        private string _NickName = "";
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _RealCountry = "";
        private uint _RegMode;
        private string _SerialNumber = "";
        private string _TimeZone = "";
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(7, IsRequired=true, Name="BuiltinIPSeq", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(8, IsRequired=true, Name="DLSrc", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(12, IsRequired=true, Name="ForceReg", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=false, Name="MAC", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MAC
        {
            get
            {
                return this._MAC;
            }
            set
            {
                this._MAC = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(14, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(13, IsRequired=false, Name="RealCountry", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=true, Name="RegMode", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=false, Name="SerialNumber", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SerialNumber
        {
            get
            {
                return this._SerialNumber;
            }
            set
            {
                this._SerialNumber = value;
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

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

