namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsUserPageResponse")]
    public class SnsUserPageResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _FirstPageMd5 = "";
        private uint _NewRequestTime = 0;
        private uint _ObjectCount;
        private uint _ObjectCountForSameMd5 = 0;
        private readonly List<SnsObject> _ObjectList = new List<SnsObject>();
        private uint _ObjectTotalCount;
        private SnsServerConfig _ServerConfig = null;
        private micromsg.SnsUserInfo _SnsUserInfo = null;
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

        [ProtoMember(7, IsRequired=false, Name="NewRequestTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NewRequestTime
        {
            get
            {
                return this._NewRequestTime;
            }
            set
            {
                this._NewRequestTime = value;
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

        [ProtoMember(8, IsRequired=false, Name="ObjectCountForSameMd5", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(5, IsRequired=true, Name="ObjectTotalCount", DataFormat=DataFormat.TwosComplement)]
        public uint ObjectTotalCount
        {
            get
            {
                return this._ObjectTotalCount;
            }
            set
            {
                this._ObjectTotalCount = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="ServerConfig", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(6, IsRequired=false, Name="SnsUserInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.SnsUserInfo SnsUserInfo
        {
            get
            {
                return this._SnsUserInfo;
            }
            set
            {
                this._SnsUserInfo = value;
            }
        }
    }
}

