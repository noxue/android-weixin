namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BlackBerryUnRegRequest")]
    public class BlackBerryUnRegRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Pin = "";
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

        [ProtoMember(2, IsRequired=false, Name="Pin", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pin
        {
            get
            {
                return this._Pin;
            }
            set
            {
                this._Pin = value;
            }
        }
    }
}

