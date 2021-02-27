namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetProductDiscountRequest")]
    public class GetProductDiscountRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _LockId = "";
        private string _Url = "";
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

        [ProtoMember(2, IsRequired=false, Name="LockId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LockId
        {
            get
            {
                return this._LockId;
            }
            set
            {
                this._LockId = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

