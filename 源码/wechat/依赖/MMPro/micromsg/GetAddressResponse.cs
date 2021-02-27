namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetAddressResponse")]
    public class GetAddressResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _RetJson = "";
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

        [ProtoMember(2, IsRequired=false, Name="RetJson", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RetJson
        {
            get
            {
                return this._RetJson;
            }
            set
            {
                this._RetJson = value;
            }
        }
    }
}

