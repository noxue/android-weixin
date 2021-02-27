namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewSyncRequest")]
    public class NewSyncRequest : IExtensible
    {
        private string _DeviceType = "";
        private SKBuiltinBuffer_t _KeyBuf;
        private CmdList _Oplog;
        private uint _Scene = 0;
        private uint _Selector;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="DeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceType
        {
            get
            {
                return this._DeviceType;
            }
            set
            {
                this._DeviceType = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="KeyBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuf
        {
            get
            {
                return this._KeyBuf;
            }
            set
            {
                this._KeyBuf = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Oplog", DataFormat=DataFormat.Default)]
        public CmdList Oplog
        {
            get
            {
                return this._Oplog;
            }
            set
            {
                this._Oplog = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Selector", DataFormat=DataFormat.TwosComplement)]
        public uint Selector
        {
            get
            {
                return this._Selector;
            }
            set
            {
                this._Selector = value;
            }
        }
    }
}

