namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PatternLockInfo")]
    public class PatternLockInfo : IExtensible
    {
        private uint _LockStatus = 0;
        private uint _PatternVersion = 0;
        private SKBuiltinBuffer_t _Sign = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="LockStatus", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint LockStatus
        {
            get
            {
                return this._LockStatus;
            }
            set
            {
                this._LockStatus = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="PatternVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PatternVersion
        {
            get
            {
                return this._PatternVersion;
            }
            set
            {
                this._PatternVersion = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Sign", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Sign
        {
            get
            {
                return this._Sign;
            }
            set
            {
                this._Sign = value;
            }
        }
    }
}

