namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Donor")]
    public class Donor : IExtensible
    {
        private string _DonateTitle = "";
        private string _DonateUrl = "";
        private string _NickName = "";
        private uint _Time;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="DonateTitle", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=false, Name="DonateUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
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

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

