namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SearchHardDeviceRequest")]
    public class SearchHardDeviceRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _HardDeviceQRCode = "";
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

        [ProtoMember(2, IsRequired=false, Name="HardDeviceQRCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string HardDeviceQRCode
        {
            get
            {
                return this._HardDeviceQRCode;
            }
            set
            {
                this._HardDeviceQRCode = value;
            }
        }
    }
}

