namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetUpdateInfoResponse")]
    public class GetUpdateInfoResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<SKBuiltinString_t> _DownLoadUrl = new List<SKBuiltinString_t>();
        private string _PackDescription = "";
        private string _PackMd5 = "";
        private uint _PackSize;
        private uint _PackVersion;
        private string _PatchInfo = "";
        private uint _UrlCount = 0;
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

        [ProtoMember(7, Name="DownLoadUrl", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> DownLoadUrl
        {
            get
            {
                return this._DownLoadUrl;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PackDescription", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackDescription
        {
            get
            {
                return this._PackDescription;
            }
            set
            {
                this._PackDescription = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="PackMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackMd5
        {
            get
            {
                return this._PackMd5;
            }
            set
            {
                this._PackMd5 = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="PackSize", DataFormat=DataFormat.TwosComplement)]
        public uint PackSize
        {
            get
            {
                return this._PackSize;
            }
            set
            {
                this._PackSize = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="PackVersion", DataFormat=DataFormat.TwosComplement)]
        public uint PackVersion
        {
            get
            {
                return this._PackVersion;
            }
            set
            {
                this._PackVersion = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="PatchInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PatchInfo
        {
            get
            {
                return this._PatchInfo;
            }
            set
            {
                this._PatchInfo = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="UrlCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint UrlCount
        {
            get
            {
                return this._UrlCount;
            }
            set
            {
                this._UrlCount = value;
            }
        }
    }
}

