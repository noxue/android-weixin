namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BackupCommandRequest")]
    public class BackupCommandRequest : IExtensible
    {
        private int _Command;
        private byte[] _Data = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Command", DataFormat=DataFormat.TwosComplement)]
        public int Command
        {
            get
            {
                return this._Command;
            }
            set
            {
                this._Command = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Data", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }
    }
}

