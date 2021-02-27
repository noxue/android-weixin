namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PackThumbExt")]
    public class PackThumbExt : IExtensible
    {
        private string _Desc = "";
        private string _PreviewUrl = "";
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

        [ProtoMember(1, IsRequired=false, Name="PreviewUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PreviewUrl
        {
            get
            {
                return this._PreviewUrl;
            }
            set
            {
                this._PreviewUrl = value;
            }
        }
    }
}

