namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SearchContactRequest")]
    public class SearchContactRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _FromScene = 0;
        private uint _OpCode = 0;
        private SKBuiltinBuffer_t _ReqBuf = null;
        private uint _SearchScene = 0;
        private SKBuiltinString_t _UserName;
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

        [ProtoMember(5, IsRequired=false, Name="FromScene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FromScene
        {
            get
            {
                return this._FromScene;
            }
            set
            {
                this._FromScene = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="OpCode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ReqBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t ReqBuf
        {
            get
            {
                return this._ReqBuf;
            }
            set
            {
                this._ReqBuf = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="SearchScene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SearchScene
        {
            get
            {
                return this._SearchScene;
            }
            set
            {
                this._SearchScene = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t UserName
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

