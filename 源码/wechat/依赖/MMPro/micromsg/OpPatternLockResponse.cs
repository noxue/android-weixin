namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="OpPatternLockResponse")]
    public class OpPatternLockResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private PatternLockBuffer _patternlockbuf = null;
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

        [ProtoMember(2, IsRequired=false, Name="patternlockbuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public PatternLockBuffer patternlockbuf
        {
            get
            {
                return this._patternlockbuf;
            }
            set
            {
                this._patternlockbuf = value;
            }
        }
    }
}

