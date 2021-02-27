namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsADObject")]
    public class SnsADObject : IExtensible
    {
        private SKBuiltinString_t _ADXML = null;
        private micromsg.SnsObject _SnsObject;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="ADXML", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ADXML
        {
            get
            {
                return this._ADXML;
            }
            set
            {
                this._ADXML = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="SnsObject", DataFormat=DataFormat.Default)]
        public micromsg.SnsObject SnsObject
        {
            get
            {
                return this._SnsObject;
            }
            set
            {
                this._SnsObject = value;
            }
        }
    }
}

