namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Mobile")]
    public class Mobile : IExtensible
    {
        private string _v = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="v", DataFormat=DataFormat.Default), DefaultValue("")]
        public string v
        {
            get
            {
                return this._v;
            }
            set
            {
                this._v = value;
            }
        }
    }
}

