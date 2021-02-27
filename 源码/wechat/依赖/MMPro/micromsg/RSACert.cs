namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RSACert")]
    public class RSACert : IExtensible
    {
        private string _KeyE = "";
        private string _KeyN = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="KeyE", DataFormat=DataFormat.Default), DefaultValue("")]
        public string KeyE
        {
            get
            {
                return this._KeyE;
            }
            set
            {
                this._KeyE = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="KeyN", DataFormat=DataFormat.Default), DefaultValue("")]
        public string KeyN
        {
            get
            {
                return this._KeyN;
            }
            set
            {
                this._KeyN = value;
            }
        }
    }
}

