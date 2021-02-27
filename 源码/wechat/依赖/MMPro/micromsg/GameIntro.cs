namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GameIntro")]
    public class GameIntro : IExtensible
    {
        private string _AndroidApkMd5 = "";
        private string _AppDownloadUrl = "";
        private string _AppID = "";
        private string _GameIntroPage = "";
        private string _GooglePlayDownloadUrl = "";
        private YYBStruct _SYYB = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AndroidApkMd5", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="AppDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="GameIntroPage", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GameIntroPage
        {
            get
            {
                return this._GameIntroPage;
            }
            set
            {
                this._GameIntroPage = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="GooglePlayDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=false, Name="SYYB", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

