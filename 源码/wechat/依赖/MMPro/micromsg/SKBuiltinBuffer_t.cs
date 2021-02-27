namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SKBuiltinBuffer_t")]
    public class SKBuiltinBuffer_t : IExtensible
    {
        private byte[] _Buffer = null;
        private uint _iLen;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Buffer", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="iLen", DataFormat=DataFormat.TwosComplement)]
        public uint iLen
        {
            get
            {
                return this._iLen;
            }
            set
            {
                this._iLen = value;
            }
        }
    }
}

