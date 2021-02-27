namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetCardConfigInfoResponse")]
    public class GetCardConfigInfoResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _json_ret = "";
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

        [ProtoMember(2, IsRequired=false, Name="json_ret", DataFormat=DataFormat.Default), DefaultValue("")]
        public string json_ret
        {
            get
            {
                return this._json_ret;
            }
            set
            {
                this._json_ret = value;
            }
        }
    }
}

