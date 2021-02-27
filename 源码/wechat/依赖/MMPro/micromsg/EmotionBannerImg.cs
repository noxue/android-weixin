namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionBannerImg")]
    public class EmotionBannerImg : IExtensible
    {
        private uint _Height;
        private string _ImgUrl = "";
        private string _StripUrl = "";
        private uint _Width;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="Height", DataFormat=DataFormat.TwosComplement)]
        public uint Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                this._Height = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="ImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgUrl
        {
            get
            {
                return this._ImgUrl;
            }
            set
            {
                this._ImgUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="StripUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StripUrl
        {
            get
            {
                return this._StripUrl;
            }
            set
            {
                this._StripUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Width", DataFormat=DataFormat.TwosComplement)]
        public uint Width
        {
            get
            {
                return this._Width;
            }
            set
            {
                this._Width = value;
            }
        }
    }
}

