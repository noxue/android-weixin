namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLbsLifeDetailRequest")]
    public class GetLbsLifeDetailRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _BusinessId = "";
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

        [ProtoMember(2, IsRequired=false, Name="BusinessId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BusinessId
        {
            get
            {
                return this._BusinessId;
            }
            set
            {
                this._BusinessId = value;
            }
        }
    }
}

