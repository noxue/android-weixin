namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UserActionReportReq")]
    public class UserActionReportReq : IExtensible
    {
        private uint _ActionCnt;
        private micromsg.BaseRequest _BaseRequest;
        private string _DeviceBrand = "";
        private string _DeviceModel = "";
        private string _LanguageVer = "";
        private readonly List<UserActionItem> _List = new List<UserActionItem>();
        private string _OsName = "";
        private string _OsVersion = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=true, Name="ActionCnt", DataFormat=DataFormat.TwosComplement)]
        public uint ActionCnt
        {
            get
            {
                return this._ActionCnt;
            }
            set
            {
                this._ActionCnt = value;
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
        public List<UserActionItem> List
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
    }
}

