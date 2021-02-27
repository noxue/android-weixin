namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FavVideoInfo")]
    public class FavVideoInfo : IExtensible
    {
        private string _AesKey = "";
        private string _FileId = "";
        private string _VideoId = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AesKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AesKey
        {
            get
            {
                return this._AesKey;
            }
            set
            {
                this._AesKey = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="FileId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileId
        {
            get
            {
                return this._FileId;
            }
            set
            {
                this._FileId = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="VideoId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VideoId
        {
            get
            {
                return this._VideoId;
            }
            set
            {
                this._VideoId = value;
            }
        }
    }
}

