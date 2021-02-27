namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsClassifyTimeLineResponse")]
    public class SnsClassifyTimeLineResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ControlFlag;
        private string _Description = "";
        private string _FirstPageMd5 = "";
        private uint _ObjectCount;
        private uint _ObjectCountForSameMd5;
        private readonly List<SnsObject> _ObjectList = new List<SnsObject>();
        private SnsServerConfig _ServerConfig;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(7, IsRequired=true, Name="ControlFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ControlFlag
        {
            get
            {
                return this._ControlFlag;
            }
            set
            {
                this._ControlFlag = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Description", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FirstPageMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FirstPageMd5
        {
            get
            {
                return this._FirstPageMd5;
            }
            set
            {
                this._FirstPageMd5 = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ObjectCount", DataFormat=DataFormat.TwosComplement)]
        public uint ObjectCount
        {
            get
            {
                return this._ObjectCount;
            }
            set
            {
                this._ObjectCount = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ObjectCountForSameMd5", DataFormat=DataFormat.TwosComplement)]
        public uint ObjectCountForSameMd5
        {
            get
            {
                return this._ObjectCountForSameMd5;
            }
            set
            {
                this._ObjectCountForSameMd5 = value;
            }
        }

        [ProtoMember(4, Name="ObjectList", DataFormat=DataFormat.Default)]
        public List<SnsObject> ObjectList
        {
            get
            {
                return this._ObjectList;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="ServerConfig", DataFormat=DataFormat.Default)]
        public SnsServerConfig ServerConfig
        {
            get
            {
                return this._ServerConfig;
            }
            set
            {
                this._ServerConfig = value;
            }
        }
    }
}

