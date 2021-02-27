namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetStreamUrlResponse")]
    public class GetStreamUrlResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _StreamUrl = "";
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

        [ProtoMember(2, IsRequired=false, Name="StreamUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamUrl
        {
            get
            {
                return this._StreamUrl;
            }
            set
            {
                this._StreamUrl = value;
            }
        }
    }
}

