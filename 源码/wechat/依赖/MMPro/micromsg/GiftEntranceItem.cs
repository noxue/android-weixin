namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GiftEntranceItem")]
    public class GiftEntranceItem : IExtensible
    {
        private string _AllGiftUrl = "";
        private string _CellTitle = "";
        private string _EnranceWording = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AllGiftUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AllGiftUrl
        {
            get
            {
                return this._AllGiftUrl;
            }
            set
            {
                this._AllGiftUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="CellTitle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CellTitle
        {
            get
            {
                return this._CellTitle;
            }
            set
            {
                this._CellTitle = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="EnranceWording", DataFormat=DataFormat.Default), DefaultValue("")]
        public string EnranceWording
        {
            get
            {
                return this._EnranceWording;
            }
            set
            {
                this._EnranceWording = value;
            }
        }
    }
}

