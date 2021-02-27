namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetIosExtensionKeyResponse")]
    public class GetIosExtensionKeyResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private ExtSession _ExtensionSession = null;
        private SKBuiltinBuffer_t _Key;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ExtensionSession", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ExtSession ExtensionSession
        {
            get
            {
                return this._ExtensionSession;
            }
            set
            {
                this._ExtensionSession = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Key", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }
    }
}

