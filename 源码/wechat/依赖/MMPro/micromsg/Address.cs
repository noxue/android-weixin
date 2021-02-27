namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Address")]
    public class Address : IExtensible
    {
        private string _City = "";
        private string _Country = "";
        private string _Detail = "";
        private string _Province = "";
        private string _Tel = "";
        private string _UserName = "";
        private string _ZipCode = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="City", DataFormat=DataFormat.Default), DefaultValue("")]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Country", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                this._Country = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Detail", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                this._Detail = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Province", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Province
        {
            get
            {
                return this._Province;
            }
            set
            {
                this._Province = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Tel", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Tel
        {
            get
            {
                return this._Tel;
            }
            set
            {
                this._Tel = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="ZipCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ZipCode
        {
            get
            {
                return this._ZipCode;
            }
            set
            {
                this._ZipCode = value;
            }
        }
    }
}

