namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmojiInfo")]
    public class EmojiInfo : IExtensible
    {
        private string _Md5;
        private string _Url = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Md5", DataFormat=DataFormat.Default)]
        public string Md5
        {
            get
            {
                return this._Md5;
            }
            set
            {
                this._Md5 = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

