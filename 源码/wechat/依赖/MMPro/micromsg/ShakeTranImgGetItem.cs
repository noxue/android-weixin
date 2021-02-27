namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeTranImgGetItem")]
    public class ShakeTranImgGetItem : IExtensible
    {
        private string _ImgUrl = "";
        private string _ThumbUrl = "";
        private string _WebUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="ImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="ThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ThumbUrl
        {
            get
            {
                return this._ThumbUrl;
            }
            set
            {
                this._ThumbUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="WebUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WebUrl
        {
            get
            {
                return this._WebUrl;
            }
            set
            {
                this._WebUrl = value;
            }
        }
    }
}

