namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="OrderDetailControlRequest")]
    public class OrderDetailControlRequest : IExtensible
    {
        private uint _ActionCode;
        private micromsg.BaseRequest _BaseRequest;
        private string _TransID = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="ActionCode", DataFormat=DataFormat.TwosComplement)]
        public uint ActionCode
        {
            get
            {
                return this._ActionCode;
            }
            set
            {
                this._ActionCode = value;
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

        [ProtoMember(3, IsRequired=false, Name="TransID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TransID
        {
            get
            {
                return this._TransID;
            }
            set
            {
                this._TransID = value;
            }
        }
    }
}

