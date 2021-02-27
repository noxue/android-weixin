namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RegisterNewPatternLockRequest")]
    public class RegisterNewPatternLockRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _cmd = 0;
        private SKBuiltinBuffer_t _patternhash = null;
        private SKBuiltinBuffer_t _paytoken = null;
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

        [ProtoMember(4, IsRequired=false, Name="cmd", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint cmd
        {
            get
            {
                return this._cmd;
            }
            set
            {
                this._cmd = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="patternhash", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t patternhash
        {
            get
            {
                return this._patternhash;
            }
            set
            {
                this._patternhash = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="paytoken", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t paytoken
        {
            get
            {
                return this._paytoken;
            }
            set
            {
                this._paytoken = value;
            }
        }
    }
}

