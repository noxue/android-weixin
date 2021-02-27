namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="KVReportReq")]
    public class KVReportReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _DeviceBrand = "";
        private string _DeviceModel = "";
        private uint _KVCnt;
        private string _LanguageVer = "";
        private readonly List<KVReportItem> _List = new List<KVReportItem>();
        private string _OsName = "";
        private string _OsVersion = "";
        private SKBuiltinBuffer_t _RandomEncryKey = null;
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

        [ProtoMember(3, IsRequired=false, Name="DeviceBrand", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="DeviceModel", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=true, Name="KVCnt", DataFormat=DataFormat.TwosComplement)]
        public uint KVCnt
        {
            get
            {
                return this._KVCnt;
            }
            set
            {
                this._KVCnt = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="LanguageVer", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LanguageVer
        {
            get
            {
                return this._LanguageVer;
            }
            set
            {
                this._LanguageVer = value;
            }
        }

        [ProtoMember(8, Name="List", DataFormat=DataFormat.Default)]
        public List<KVReportItem> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="OsName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OsName
        {
            get
            {
                return this._OsName;
            }
            set
            {
                this._OsName = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="OsVersion", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OsVersion
        {
            get
            {
                return this._OsVersion;
            }
            set
            {
                this._OsVersion = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="RandomEncryKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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
    }
}

