namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BBMContactUploadItem")]
    public class BBMContactUploadItem : IExtensible
    {
        private string _BBMNickName = "";
        private string _BBPIN = "";
        private string _BBPPID = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="BBMNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBMNickName
        {
            get
            {
                return this._BBMNickName;
            }
            set
            {
                this._BBMNickName = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="BBPIN", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBPIN
        {
            get
            {
                return this._BBPIN;
            }
            set
            {
                this._BBPIN = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="BBPPID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBPPID
        {
            get
            {
                return this._BBPPID;
            }
            set
            {
                this._BBPPID = value;
            }
        }
    }
}

