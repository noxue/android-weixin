namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BizAppInfo")]
    public class BizAppInfo : IExtensible
    {
        private string _AndroidPackageName = "";
        private string _AndroidSignature = "";
        private string _AppId = "";
        private uint _AppInfoFlag;
        private string _AppType = "";
        private uint _AppUpdateVersion = 0;
        private uint _AppVersion;
        private string _Description = "";
        private string _Description4EnUS = "";
        private string _Description4ZhTW = "";
        private string _DevInfo = "";
        private string _DownloadUrl = "";
        private string _DownloadUrlMd5 = "";
        private string _GooglePlayDownloadUrl = "";
        private string _IconUrlHD = "";
        private string _IconUrlMDPI = "";
        private string _IconUrlSD = "";
        private string _Name = "";
        private string _Name4EnUS = "";
        private string _Name4ZhTW = "";
        private string _StoreUrl = "";
        private string _WatermarkUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(12, IsRequired=false, Name="AndroidPackageName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(13, IsRequired=false, Name="AndroidSignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AndroidSignature
        {
            get
            {
                return this._AndroidSignature;
            }
            set
            {
                this._AndroidSignature = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="AppId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppId
        {
            get
            {
                return this._AppId;
            }
            set
            {
                this._AppId = value;
            }
        }

        [ProtoMember(0x10, IsRequired=true, Name="AppInfoFlag", DataFormat=DataFormat.TwosComplement)]
        public uint AppInfoFlag
        {
            get
            {
                return this._AppInfoFlag;
            }
            set
            {
                this._AppInfoFlag = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="AppType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppType
        {
            get
            {
                return this._AppType;
            }
            set
            {
                this._AppType = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="AppUpdateVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AppUpdateVersion
        {
            get
            {
                return this._AppUpdateVersion;
            }
            set
            {
                this._AppUpdateVersion = value;
            }
        }

        [ProtoMember(0x11, IsRequired=true, Name="AppVersion", DataFormat=DataFormat.TwosComplement)]
        public uint AppVersion
        {
            get
            {
                return this._AppVersion;
            }
            set
            {
                this._AppVersion = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Description", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=false, Name="Description4EnUS", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description4EnUS
        {
            get
            {
                return this._Description4EnUS;
            }
            set
            {
                this._Description4EnUS = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Description4ZhTW", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description4ZhTW
        {
            get
            {
                return this._Description4ZhTW;
            }
            set
            {
                this._Description4ZhTW = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="DevInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DevInfo
        {
            get
            {
                return this._DevInfo;
            }
            set
            {
                this._DevInfo = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="DownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x15, IsRequired=false, Name="DownloadUrlMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DownloadUrlMd5
        {
            get
            {
                return this._DownloadUrlMd5;
            }
            set
            {
                this._DownloadUrlMd5 = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="GooglePlayDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=false, Name="IconUrlHD", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrlHD
        {
            get
            {
                return this._IconUrlHD;
            }
            set
            {
                this._IconUrlHD = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="IconUrlMDPI", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrlMDPI
        {
            get
            {
                return this._IconUrlMDPI;
            }
            set
            {
                this._IconUrlMDPI = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="IconUrlSD", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrlSD
        {
            get
            {
                return this._IconUrlSD;
            }
            set
            {
                this._IconUrlSD = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Name4EnUS", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name4EnUS
        {
            get
            {
                return this._Name4EnUS;
            }
            set
            {
                this._Name4EnUS = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Name4ZhTW", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name4ZhTW
        {
            get
            {
                return this._Name4ZhTW;
            }
            set
            {
                this._Name4ZhTW = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="StoreUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StoreUrl
        {
            get
            {
                return this._StoreUrl;
            }
            set
            {
                this._StoreUrl = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="WatermarkUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WatermarkUrl
        {
            get
            {
                return this._WatermarkUrl;
            }
            set
            {
                this._WatermarkUrl = value;
            }
        }
    }
}

