namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="HandleImgMsgRequest")]
    public class HandleImgMsgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinString_t _ClientImgId;
        private uint _CompressType;
        private SKBuiltinString_t _FromUserName;
        private uint _ImgLen;
        private string _MediaId = "";
        private string _MsgSource = "";
        private uint _MsgType;
        private SKBuiltinString_t _ToUserName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ClientImgId", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ClientImgId
        {
            get
            {
                return this._ClientImgId;
            }
            set
            {
                this._ClientImgId = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="CompressType", DataFormat=DataFormat.TwosComplement)]
        public uint CompressType
        {
            get
            {
                return this._CompressType;
            }
            set
            {
                this._CompressType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="FromUserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t FromUserName
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

        [ProtoMember(6, IsRequired=true, Name="ImgLen", DataFormat=DataFormat.TwosComplement)]
        public uint ImgLen
        {
            get
            {
                return this._ImgLen;
            }
            set
            {
                this._ImgLen = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="MediaId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MediaId
        {
            get
            {
                return this._MediaId;
            }
            set
            {
                this._MediaId = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="MsgSource", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=true, Name="MsgType", DataFormat=DataFormat.TwosComplement)]
        public uint MsgType
        {
            get
            {
                return this._MsgType;
            }
            set
            {
                this._MsgType = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ToUserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ToUserName
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
    }
}

