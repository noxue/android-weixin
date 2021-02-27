namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AndroidGCMRegRequest")]
    public class AndroidGCMRegRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Token = "";
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

        [ProtoMember(2, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

