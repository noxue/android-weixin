namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GiftList")]
    public class GiftList : IExtensible
    {
        private string _Desc = "";
        private string _GiftID = "";
        private string _GiftUrl = "";
        private string _IconUrl = "";
        private string _Title = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="Desc", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(1, IsRequired=false, Name="GiftID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GiftID
        {
            get
            {
                return this._GiftID;
            }
            set
            {
                this._GiftID = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="GiftUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GiftUrl
        {
            get
            {
                return this._GiftUrl;
            }
            set
            {
                this._GiftUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="IconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrl
        {
            get
            {
                return this._IconUrl;
            }
            set
            {
                this._IconUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Title", DataFormat=DataFormat.Default), DefaultValue("")]
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

