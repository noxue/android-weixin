namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLbsFunctionListResponse")]
    public class GetLbsFunctionListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _LbsFunctionList = "";
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

        [ProtoMember(2, IsRequired=false, Name="LbsFunctionList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LbsFunctionList
        {
            get
            {
                return this._LbsFunctionList;
            }
            set
            {
                this._LbsFunctionList = value;
            }
        }
    }
}

