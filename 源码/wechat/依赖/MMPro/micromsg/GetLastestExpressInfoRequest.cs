namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLastestExpressInfoRequest")]
    public class GetLastestExpressInfoRequest : IExtensible
    {
        private micromsg.Address _Address;
        private micromsg.BaseRequest _BaseRequest;
        private string _LockId = "";
        private string _ProductId = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="Address", DataFormat=DataFormat.Default)]
        public micromsg.Address Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this._Address = value;
            }
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

        [ProtoMember(3, IsRequired=false, Name="LockId", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="ProductId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ProductId
        {
            get
            {
                return this._ProductId;
            }
            set
            {
                this._ProductId = value;
            }
        }
    }
}

