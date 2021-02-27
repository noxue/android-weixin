namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RcAppList")]
    public class RcAppList : IExtensible
    {
        private string _AndroidApkMd5 = "";
        private string _AndroidPackageName = "";
        private string _AppCoverUrl = "";
        private string _AppDesc = "";
        private string _AppDetailDesc = "";
        private string _AppDevInfo = "";
        private string _AppDownloadUrl = "";
        private string _AppIconUrl = "";
        private string _AppID = "";
        private uint _AppInfoFlag = 0;
        private string _AppLaunchScheme = "";
        private string _AppName = "";
        private string _AppNameEnUS = "";
        private string _AppNamezhTW = "";
        private int _AppScreenShotCount;
        private readonly List<SKBuiltinString_t> _AppScreenShotList = new List<SKBuiltinString_t>();
        private string _AppSnapshotUrl = "";
        private string _AppSnsDesc = "";
        private string _AppSuggestionIconUrl = "";
        private string _AppSuggestionIntroUrl = "";
        private string _AppType = "";
        private uint _FriendCount = 0;
        private uint _GooglePlayDownloadFlag = 0;
        private string _GooglePlayDownloadUrl = "";
        private YYBStruct _SYYB = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(15, IsRequired=false, Name="AndroidApkMd5", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x10, IsRequired=false, Name="AndroidPackageName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=false, Name="AppCoverUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="AppDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDesc
        {
            get
            {
                return this._AppDesc;
            }
            set
            {
                this._AppDesc = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="AppDetailDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDetailDesc
        {
            get
            {
                return this._AppDetailDesc;
            }
            set
            {
                this._AppDetailDesc = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="AppDevInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDevInfo
        {
            get
            {
                return this._AppDevInfo;
            }
            set
            {
                this._AppDevInfo = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="AppDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=false, Name="AppIconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x19, IsRequired=false, Name="AppInfoFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x17, IsRequired=false, Name="AppLaunchScheme", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppLaunchScheme
        {
            get
            {
                return this._AppLaunchScheme;
            }
            set
            {
                this._AppLaunchScheme = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="AppName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x12, IsRequired=false, Name="AppNameEnUS", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppNameEnUS
        {
            get
            {
                return this._AppNameEnUS;
            }
            set
            {
                this._AppNameEnUS = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="AppNamezhTW", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppNamezhTW
        {
            get
            {
                return this._AppNamezhTW;
            }
            set
            {
                this._AppNamezhTW = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="AppScreenShotCount", DataFormat=DataFormat.TwosComplement)]
        public int AppScreenShotCount
        {
            get
            {
                return this._AppScreenShotCount;
            }
            set
            {
                this._AppScreenShotCount = value;
            }
        }

        [ProtoMember(9, Name="AppScreenShotList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> AppScreenShotList
        {
            get
            {
                return this._AppScreenShotList;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="AppSnapshotUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppSnapshotUrl
        {
            get
            {
                return this._AppSnapshotUrl;
            }
            set
            {
                this._AppSnapshotUrl = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="AppSnsDesc", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(20, IsRequired=false, Name="AppSuggestionIconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppSuggestionIconUrl
        {
            get
            {
                return this._AppSuggestionIconUrl;
            }
            set
            {
                this._AppSuggestionIconUrl = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="AppSuggestionIntroUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppSuggestionIntroUrl
        {
            get
            {
                return this._AppSuggestionIntroUrl;
            }
            set
            {
                this._AppSuggestionIntroUrl = value;
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

        [ProtoMember(13, IsRequired=false, Name="FriendCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FriendCount
        {
            get
            {
                return this._FriendCount;
            }
            set
            {
                this._FriendCount = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="GooglePlayDownloadFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GooglePlayDownloadFlag
        {
            get
            {
                return this._GooglePlayDownloadFlag;
            }
            set
            {
                this._GooglePlayDownloadFlag = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="GooglePlayDownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x16, IsRequired=false, Name="SYYB", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

