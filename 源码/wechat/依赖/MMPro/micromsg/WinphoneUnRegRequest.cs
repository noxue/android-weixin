namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="WinphoneUnRegRequest")]
    public class WinphoneUnRegRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Uri = "";
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

        [ProtoMember(2, IsRequired=false, Name="Uri", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Uri
        {
            get
            {
                return this._Uri;
            }
            set
            {
                this._Uri = value;
            }
        }
    }
}

