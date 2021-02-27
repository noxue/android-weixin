namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DonateHistory")]
    public class DonateHistory : IExtensible
    {
        private string _DonateThumbUrl = "";
        private string _DonateTitle = "";
        private string _DonateUrl = "";
        private uint _Price;
        private uint _Time;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="DonateThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DonateThumbUrl
        {
            get
            {
                return this._DonateThumbUrl;
            }
            set
            {
                this._DonateThumbUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="DonateTitle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DonateTitle
        {
            get
            {
                return this._DonateTitle;
            }
            set
            {
                this._DonateTitle = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="DonateUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DonateUrl
        {
            get
            {
                return this._DonateUrl;
            }
            set
            {
                this._DonateUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Price", DataFormat=DataFormat.TwosComplement)]
        public uint Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Time", DataFormat=DataFormat.TwosComplement)]
        public uint Time
        {
            get
            {
                return this._Time;
            }
            set
            {
                this._Time = value;
            }
        }
    }
}

