namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ModDescription")]
    public class ModDescription : IExtensible
    {
        private string _ContactUsername = "";
        private string _Desc = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="ContactUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ContactUsername
        {
            get
            {
                return this._ContactUsername;
            }
            set
            {
                this._ContactUsername = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Desc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Desc
        {
            get
            {
                return this._Desc;
            }
            set
            {
                this._Desc = value;
            }
        }
    }
}

