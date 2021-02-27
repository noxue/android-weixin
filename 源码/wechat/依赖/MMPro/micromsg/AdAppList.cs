namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AdAppList")]
    public class AdAppList : IExtensible
    {
        private string _AndroidApkMd5 = "";
        private string _AndroidPackageName = "";
        private string _AppCoverUrl = "";
        private string _AppDownloadUrl = "";
        private string _AppIconUrl = "";
        private string _AppID = "";
        private string _AppName = "";
        private string _AppSnsDesc = "";
        private string _ExtAsXML = "";
        private string _GooglePlayDownloadUrl = "";
        private YYBStruct _SYYB = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="AndroidApkMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AndroidApkMd5
        {
            get
            {
                return this._AndroidApkMd5;
            }
            set
            {
                this._AndroidApkMd5 = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="AndroidPackageName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AndroidPackageName
        {
            get
            {
                return this._AndroidPackageName;
            }
            set
            {
                this._AndroidPackageName = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="AppCoverUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppCoverUrl
        {
            get
            {
                return this._AppCoverUrl;
            }
            set
            {
                this._AppCoverUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="AppDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDownloadUrl
        {
            get
            {
                return this._AppDownloadUrl;
            }
            set
            {
                this._AppDownloadUrl = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="AppIconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppIconUrl
        {
            get
            {
                return this._AppIconUrl;
            }
            set
            {
                this._AppIconUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppID
        {
            get
            {
                return this._AppID;
            }
            set
            {
                this._AppID = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="AppName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppName
        {
            get
            {
                return this._AppName;
            }
            set
            {
                this._AppName = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="AppSnsDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppSnsDesc
        {
            get
            {
                return this._AppSnsDesc;
            }
            set
            {
                this._AppSnsDesc = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="ExtAsXML", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtAsXML
        {
            get
            {
                return this._ExtAsXML;
            }
            set
            {
                this._ExtAsXML = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="GooglePlayDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GooglePlayDownloadUrl
        {
            get
            {
                return this._GooglePlayDownloadUrl;
            }
            set
            {
                this._GooglePlayDownloadUrl = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="SYYB", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public YYBStruct SYYB
        {
            get
            {
                return this._SYYB;
            }
            set
            {
                this._SYYB = value;
            }
        }
    }
}

