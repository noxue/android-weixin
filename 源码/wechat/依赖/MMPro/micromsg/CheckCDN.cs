namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckCDN")]
    public class CheckCDN : IExtensible
    {
        private string _DataId = "";
        private string _DataSourceId = "";
        private uint _DataSourceType;
        private string _FullMd5 = "";
        private uint _FullSize;
        private string _Head256Md5 = "";
        private uint _IsThumb;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="DataId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DataId
        {
            get
            {
                return this._DataId;
            }
            set
            {
                this._DataId = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="DataSourceId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DataSourceId
        {
            get
            {
                return this._DataSourceId;
            }
            set
            {
                this._DataSourceId = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="DataSourceType", DataFormat=DataFormat.TwosComplement)]
        public uint DataSourceType
        {
            get
            {
                return this._DataSourceType;
            }
            set
            {
                this._DataSourceType = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FullMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FullMd5
        {
            get
            {
                return this._FullMd5;
            }
            set
            {
                this._FullMd5 = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="FullSize", DataFormat=DataFormat.TwosComplement)]
        public uint FullSize
        {
            get
            {
                return this._FullSize;
            }
            set
            {
                this._FullSize = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Head256Md5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Head256Md5
        {
            get
            {
                return this._Head256Md5;
            }
            set
            {
                this._Head256Md5 = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="IsThumb", DataFormat=DataFormat.TwosComplement)]
        public uint IsThumb
        {
            get
            {
                return this._IsThumb;
            }
            set
            {
                this._IsThumb = value;
            }
        }
    }
}

