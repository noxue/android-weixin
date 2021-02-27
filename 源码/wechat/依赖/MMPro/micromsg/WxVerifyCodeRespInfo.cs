namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="WxVerifyCodeRespInfo")]
    public class WxVerifyCodeRespInfo : IExtensible
    {
        private SKBuiltinBuffer_t _VerifyBuff;
        private string _VerifySignature = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="VerifyBuff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t VerifyBuff
        {
            get
            {
                return this._VerifyBuff;
            }
            set
            {
                this._VerifyBuff = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="VerifySignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifySignature
        {
            get
            {
                return this._VerifySignature;
            }
            set
            {
                this._VerifySignature = value;
            }
        }
    }
}

