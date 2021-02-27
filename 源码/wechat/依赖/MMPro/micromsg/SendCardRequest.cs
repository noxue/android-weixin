namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SendCardRequest")]
    public class SendCardRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Content = "";
        private string _ContentEx = "";
        private uint _SendCardBitFlag = 0;
        private uint _Style = 0;
        private string _UserName = "";
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

        [ProtoMember(3, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=false, Name="ContentEx", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ContentEx
        {
            get
            {
                return this._ContentEx;
            }
            set
            {
                this._ContentEx = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="SendCardBitFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SendCardBitFlag
        {
            get
            {
                return this._SendCardBitFlag;
            }
            set
            {
                this._SendCardBitFlag = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Style", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                this._Style = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

