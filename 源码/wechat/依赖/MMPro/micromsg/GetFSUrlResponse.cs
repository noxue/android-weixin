namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetFSUrlResponse")]
    public class GetFSUrlResponse : IExtensible
    {
        private string _FSURL = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="FSURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FSURL
        {
            get
            {
                return this._FSURL;
            }
            set
            {
                this._FSURL = value;
            }
        }
    }
}

