namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FacebookInfo")]
    public class FacebookInfo : IExtensible
    {
        private string _Name = "";
        private string _Token = "";
        private ulong _Uid;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Token
        {
            get
            {
                return this._Token;
            }
            set
            {
                this._Token = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Uid", DataFormat=DataFormat.TwosComplement)]
        public ulong Uid
        {
            get
            {
                return this._Uid;
            }
            set
            {
                this._Uid = value;
            }
        }
    }
}

