namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CreatePoiResponse")]
    public class CreatePoiResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _PoiId = "";
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

        [ProtoMember(2, IsRequired=false, Name="PoiId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PoiId
        {
            get
            {
                return this._PoiId;
            }
            set
            {
                this._PoiId = value;
            }
        }
    }
}

