namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GeneralShareContent")]
    public class GeneralShareContent : IExtensible
    {
        private string _Content = "";
        private uint _contentType;
        private string _DataUrl = "";
        private string _Description = "";
        private string _ExtInfo = "";
        private string _ImageUrl = "";
        private string _LinkUrl = "";
        private string _Title = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="contentType", DataFormat=DataFormat.TwosComplement)]
        public uint contentType
        {
            get
            {
                return this._contentType;
            }
            set
            {
                this._contentType = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="DataUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DataUrl
        {
            get
            {
                return this._DataUrl;
            }
            set
            {
                this._DataUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Description", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ImageUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImageUrl
        {
            get
            {
                return this._ImageUrl;
            }
            set
            {
                this._ImageUrl = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="LinkUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LinkUrl
        {
            get
            {
                return this._LinkUrl;
            }
            set
            {
                this._LinkUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Title", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }
    }
}

