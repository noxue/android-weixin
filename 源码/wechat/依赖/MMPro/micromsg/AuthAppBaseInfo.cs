namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AuthAppBaseInfo")]
    public class AuthAppBaseInfo : IExtensible
    {
        private uint _AppFlag;
        private string _AppID = "";
        private string _AppName = "";
        private string _AppType = "";
        private string _AuthInfo = "";
        private string _DevInfo = "";
        private string _ExternInfo = "";
        private string _IconUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=true, Name="AppFlag", DataFormat=DataFormat.TwosComplement)]
        public uint AppFlag
        {
            get
            {
                return this._AppFlag;
            }
            set
            {
                this._AppFlag = value;
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

        [ProtoMember(4, IsRequired=false, Name="AppName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="AppType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="AuthInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthInfo
        {
            get
            {
                return this._AuthInfo;
            }
            set
            {
                this._AuthInfo = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="DevInfo", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=false, Name="ExternInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExternInfo
        {
            get
            {
                return this._ExternInfo;
            }
            set
            {
                this._ExternInfo = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="IconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrl
        {
            get
            {
                return this._IconUrl;
            }
            set
            {
                this._IconUrl = value;
            }
        }
    }
}

