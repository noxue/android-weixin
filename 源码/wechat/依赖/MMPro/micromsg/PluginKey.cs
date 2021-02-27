namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PluginKey")]
    public class PluginKey : IExtensible
    {
        private uint _Id = 0;
        private string _Key = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Id", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Id
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

        [ProtoMember(1, IsRequired=false, Name="Key", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }
    }
}

