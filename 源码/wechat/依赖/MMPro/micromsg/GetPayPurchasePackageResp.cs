namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetPayPurchasePackageResp")]
    public class GetPayPurchasePackageResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _ExtInfo = "";
        private string _Package = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Package", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Package
        {
            get
            {
                return this._Package;
            }
            set
            {
                this._Package = value;
            }
        }
    }
}

