namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BaseAuthReqInfo")]
    public class BaseAuthReqInfo : IExtensible
    {
        private uint _AuthReqFlag = 0;
        private string _AuthTicket = "";
        private SKBuiltinBuffer_t _CliDBEncryptInfo = null;
        private SKBuiltinBuffer_t _CliDBEncryptKey = null;
        private micromsg.WTLoginImgReqInfo _WTLoginImgReqInfo = null;
        private SKBuiltinBuffer_t _WTLoginReqBuff = null;
        private micromsg.WxVerifyCodeReqInfo _WxVerifyCodeReqInfo = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=false, Name="AuthReqFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AuthReqFlag
        {
            get
            {
                return this._AuthReqFlag;
            }
            set
            {
                this._AuthReqFlag = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="AuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthTicket
        {
            get
            {
                return this._AuthTicket;
            }
            set
            {
                this._AuthTicket = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="CliDBEncryptInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t CliDBEncryptInfo
        {
            get
            {
                return this._CliDBEncryptInfo;
            }
            set
            {
                this._CliDBEncryptInfo = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="CliDBEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t CliDBEncryptKey
        {
            get
            {
                return this._CliDBEncryptKey;
            }
            set
            {
                this._CliDBEncryptKey = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="WTLoginImgReqInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.WTLoginImgReqInfo WTLoginImgReqInfo
        {
            get
            {
                return this._WTLoginImgReqInfo;
            }
            set
            {
                this._WTLoginImgReqInfo = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="WTLoginReqBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t WTLoginReqBuff
        {
            get
            {
                return this._WTLoginReqBuff;
            }
            set
            {
                this._WTLoginReqBuff = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="WxVerifyCodeReqInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.WxVerifyCodeReqInfo WxVerifyCodeReqInfo
        {
            get
            {
                return this._WxVerifyCodeReqInfo;
            }
            set
            {
                this._WxVerifyCodeReqInfo = value;
            }
        }
    }
}

