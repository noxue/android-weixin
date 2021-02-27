namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LangDesc")]
    public class LangDesc : IExtensible
    {
        private string _Desc = "";
        private string _Lang = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(1, IsRequired=false, Name="Lang", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Lang
        {
            get
            {
                return this._Lang;
            }
            set
            {
                this._Lang = value;
            }
        }
    }
}

