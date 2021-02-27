namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PatternLockBuffer")]
    public class PatternLockBuffer : IExtensible
    {
        private uint _lockstatus = 0;
        private SKBuiltinBuffer_t _sign = null;
        private SKBuiltinBuffer_t _svrpatternhash = null;
        private uint _uin = 0;
        private uint _version = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="lockstatus", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint lockstatus
        {
            get
            {
                return this._lockstatus;
            }
            set
            {
                this._lockstatus = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="sign", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t sign
        {
            get
            {
                return this._sign;
            }
            set
            {
                this._sign = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="svrpatternhash", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t svrpatternhash
        {
            get
            {
                return this._svrpatternhash;
            }
            set
            {
                this._svrpatternhash = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="uin", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint uin
        {
            get
            {
                return this._uin;
            }
            set
            {
                this._uin = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="version", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }
    }
}

