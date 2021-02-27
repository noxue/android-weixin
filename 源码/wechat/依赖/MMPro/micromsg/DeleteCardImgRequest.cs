namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DeleteCardImgRequest")]
    public class DeleteCardImgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ContactUserName = "";
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

        [ProtoMember(2, IsRequired=false, Name="ContactUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ContactUserName
        {
            get
            {
                return this._ContactUserName;
            }
            set
            {
                this._ContactUserName = value;
            }
        }
    }
}

