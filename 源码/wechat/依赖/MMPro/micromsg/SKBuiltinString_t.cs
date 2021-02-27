namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SKBuiltinString_t")]
    public class SKBuiltinString_t : IExtensible
    {
        private string _String = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="String", DataFormat=DataFormat.Default), DefaultValue("")]
        public string String
        {
            get
            {
                return this._String;
            }
            set
            {
                this._String = value;
            }
        }
    }
}

