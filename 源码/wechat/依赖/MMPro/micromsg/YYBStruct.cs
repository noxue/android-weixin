namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="YYBStruct")]
    public class YYBStruct : IExtensible
    {
        private uint _AndroidDownloadFlag = 0;
        private string _ApkMd5 = "";
        private string _DownloadTipsWording = "";
        private string _DownloadUrl = "";
        private string _ExtInfo = "";
        private string _PreemptiveUrl = "";
        private int _SupportedVersionCode = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="AndroidDownloadFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AndroidDownloadFlag
        {
            get
            {
                return this._AndroidDownloadFlag;
            }
            set
            {
                this._AndroidDownloadFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ApkMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ApkMd5
        {
            get
            {
                return this._ApkMd5;
            }
            set
            {
                this._ApkMd5 = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="DownloadTipsWording", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DownloadTipsWording
        {
            get
            {
                return this._DownloadTipsWording;
            }
            set
            {
                this._DownloadTipsWording = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="DownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DownloadUrl
        {
            get
            {
                return this._DownloadUrl;
            }
            set
            {
                this._DownloadUrl = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="PreemptiveUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PreemptiveUrl
        {
            get
            {
                return this._PreemptiveUrl;
            }
            set
            {
                this._PreemptiveUrl = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="SupportedVersionCode", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int SupportedVersionCode
        {
            get
            {
                return this._SupportedVersionCode;
            }
            set
            {
                this._SupportedVersionCode = value;
            }
        }
    }
}

