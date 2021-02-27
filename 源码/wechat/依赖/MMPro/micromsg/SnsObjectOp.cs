namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsObjectOp")]
    public class SnsObjectOp : IExtensible
    {
        private SKBuiltinBuffer_t _Ext = null;
        private ulong _Id;
        private uint _OpType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Ext", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Ext
        {
            get
            {
                return this._Ext;
            }
            set
            {
                this._Ext = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public ulong Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpType", DataFormat=DataFormat.TwosComplement)]
        public uint OpType
        {
            get
            {
                return this._OpType;
            }
            set
            {
                this._OpType = value;
            }
        }
    }
}

