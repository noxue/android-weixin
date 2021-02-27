namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="OpenAppInfo")]
    public class OpenAppInfo : IExtensible
    {
        private string _AndroidPackageName = "";
        private string _AndroidSignature = "";
        private string _AppDescription = "";
        private string _AppDescription4EnUS = "";
        private string _AppDescription4ZhTW = "";
        private string _AppIconUrl = "";
        private string _AppID = "";
        private uint _AppInfoFlag = 0;
        private string _AppName = "";
        private string _AppName4EnUS = "";
        private string _AppName4ZhTW = "";
        private string _AppStoreUrl = "";
        private uint _AppVersion;
        private string _AppWatermarkUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(9, IsRequired=false, Name="AndroidSignature", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=false, Name="AppDescription", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDescription
        {
            get
            {
                return this._AppDescription;
            }
            set
            {
                this._AppDescription = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="AppDescription4EnUS", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDescription4EnUS
        {
            get
            {
                return this._AppDescription4EnUS;
            }
            set
            {
                this._AppDescription4EnUS = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="AppDescription4ZhTW", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppDescription4ZhTW
        {
            get
            {
                return this._AppDescription4ZhTW;
            }
            set
            {
                this._AppDescription4ZhTW = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="AppIconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(14, IsRequired=false, Name="AppInfoFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(10, IsRequired=false, Name="AppName4EnUS", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppName4EnUS
        {
            get
            {
                return this._AppName4EnUS;
            }
            set
            {
                this._AppName4EnUS = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="AppName4ZhTW", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppName4ZhTW
        {
            get
            {
                return this._AppName4ZhTW;
            }
            set
            {
                this._AppName4ZhTW = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="AppStoreUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppStoreUrl
        {
            get
            {
                return this._AppStoreUrl;
            }
            set
            {
                this._AppStoreUrl = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="AppVersion", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(7, IsRequired=false, Name="AppWatermarkUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppWatermarkUrl
        {
            get
            {
                return this._AppWatermarkUrl;
            }
            set
            {
                this._AppWatermarkUrl = value;
            }
        }
    }
}

