namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UserGameInfo")]
    public class UserGameInfo : IExtensible
    {
        private string _HeadImageUrl = "";
        private string _NickName = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="HeadImageUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string HeadImageUrl
        {
            get
            {
                return this._HeadImageUrl;
            }
            set
            {
                this._HeadImageUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(1, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

