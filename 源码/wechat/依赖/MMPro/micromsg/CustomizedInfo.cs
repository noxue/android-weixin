namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CustomizedInfo")]
    public class CustomizedInfo : IExtensible
    {
        private uint _BrandFlag;
        private string _BrandIconURL = "";
        private string _BrandInfo = "";
        private string _ExternalInfo = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BrandFlag", DataFormat=DataFormat.TwosComplement)]
        public uint BrandFlag
        {
            get
            {
                return this._BrandFlag;
            }
            set
            {
                this._BrandFlag = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="BrandIconURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BrandIconURL
        {
            get
            {
                return this._BrandIconURL;
            }
            set
            {
                this._BrandIconURL = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="BrandInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BrandInfo
        {
            get
            {
                return this._BrandInfo;
            }
            set
            {
                this._BrandInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ExternalInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExternalInfo
        {
            get
            {
                return this._ExternalInfo;
            }
            set
            {
                this._ExternalInfo = value;
            }
        }
    }
}

