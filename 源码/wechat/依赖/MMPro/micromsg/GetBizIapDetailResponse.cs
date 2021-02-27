namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetBizIapDetailResponse")]
    public class GetBizIapDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _DetailBuff = "";
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

        [ProtoMember(2, IsRequired=false, Name="DetailBuff", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DetailBuff
        {
            get
            {
                return this._DetailBuff;
            }
            set
            {
                this._DetailBuff = value;
            }
        }
    }
}

