namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VerifyPayTransitionResp")]
    public class VerifyPayTransitionResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _ResultMsg = "";
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

        [ProtoMember(2, IsRequired=false, Name="ResultMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ResultMsg
        {
            get
            {
                return this._ResultMsg;
            }
            set
            {
                this._ResultMsg = value;
            }
        }
    }
}

