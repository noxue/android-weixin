namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetProductInfoRequest")]
    public class GetProductInfoRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ProductID = "";
        private string _QrUrl = "";
        private uint _Scene;
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

        [ProtoMember(2, IsRequired=false, Name="ProductID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this._ProductID = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="QrUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QrUrl
        {
            get
            {
                return this._QrUrl;
            }
            set
            {
                this._QrUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }
    }
}

