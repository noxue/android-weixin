namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SearchQRCodeReq")]
    public class SearchQRCodeReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _QRCode = "";
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

        [ProtoMember(2, IsRequired=false, Name="QRCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QRCode
        {
            get
            {
                return this._QRCode;
            }
            set
            {
                this._QRCode = value;
            }
        }
    }
}

