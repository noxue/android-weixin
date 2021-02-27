namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BuiltinIP")]
    public class BuiltinIP : IExtensible
    {
        private byte[] _Domain = null;
        private byte[] _IP = null;
        private uint _port;
        private uint _type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="Domain", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] Domain
        {
            get
            {
                return this._Domain;
            }
            set
            {
                this._Domain = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="IP", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                this._IP = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="port", DataFormat=DataFormat.TwosComplement)]
        public uint port
        {
            get
            {
                return this._port;
            }
            set
            {
                this._port = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="type", DataFormat=DataFormat.TwosComplement)]
        public uint type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

