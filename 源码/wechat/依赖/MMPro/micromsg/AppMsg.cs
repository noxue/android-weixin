namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AppMsg")]
    public class AppMsg : IExtensible
    {
        private string _AppId = "";
        private string _ClientMsgId = "";
        private string _Content = "";
        private uint _CreateTime;
        private string _FromUserName = "";
        private string _JsAppId = "";
        private string _MsgSource = "";
        private int _RemindId = 0;
        private uint _SdkVersion;
        private string _ShareUrlOpen = "";
        private string _ShareUrlOriginal = "";
        private int _Source = 0;
        private SKBuiltinBuffer_t _Thumb = null;
        private string _ToUserName = "";
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AppId", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=false, Name="ClientMsgId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientMsgId
        {
            get
            {
                return this._ClientMsgId;
            }
            set
            {
                this._ClientMsgId = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
        public uint CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="FromUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUserName
        {
            get
            {
                return this._FromUserName;
            }
            set
            {
                this._FromUserName = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="JsAppId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string JsAppId
        {
            get
            {
                return this._JsAppId;
            }
            set
            {
                this._JsAppId = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="MsgSource", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MsgSource
        {
            get
            {
                return this._MsgSource;
            }
            set
            {
                this._MsgSource = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="RemindId", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int RemindId
        {
            get
            {
                return this._RemindId;
            }
            set
            {
                this._RemindId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="SdkVersion", DataFormat=DataFormat.TwosComplement)]
        public uint SdkVersion
        {
            get
            {
                return this._SdkVersion;
            }
            set
            {
                this._SdkVersion = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="ShareUrlOpen", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ShareUrlOpen
        {
            get
            {
                return this._ShareUrlOpen;
            }
            set
            {
                this._ShareUrlOpen = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="ShareUrlOriginal", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ShareUrlOriginal
        {
            get
            {
                return this._ShareUrlOriginal;
            }
            set
            {
                this._ShareUrlOriginal = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Source", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Thumb", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Thumb
        {
            get
            {
                return this._Thumb;
            }
            set
            {
                this._Thumb = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUserName
        {
            get
            {
                return this._ToUserName;
            }
            set
            {
                this._ToUserName = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

