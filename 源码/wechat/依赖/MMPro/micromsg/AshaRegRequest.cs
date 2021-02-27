namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AshaRegRequest")]
    public class AshaRegRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Nid = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Nid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Nid
        {
            get
            {
                return this._Nid;
            }
            set
            {
                this._Nid = value;
            }
        }
    }
}

