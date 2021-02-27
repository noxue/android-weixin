namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CancelPreOrderRequest")]
    public class CancelPreOrderRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _LockId = "";
        private readonly List<SampleProduct> _Product = new List<SampleProduct>();
        private uint _ProductCount;
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

        [ProtoMember(4, IsRequired=false, Name="LockId", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, Name="Product", DataFormat=DataFormat.Default)]
        public List<SampleProduct> Product
        {
            get
            {
                return this._Product;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ProductCount", DataFormat=DataFormat.TwosComplement)]
        public uint ProductCount
        {
            get
            {
                return this._ProductCount;
            }
            set
            {
                this._ProductCount = value;
            }
        }
    }
}

