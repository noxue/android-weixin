namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetMailOAuthUrlResponse")]
    public class GetMailOAuthUrlResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _OAuthUrl = "";
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

        [ProtoMember(2, IsRequired=false, Name="OAuthUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OAuthUrl
        {
            get
            {
                return this._OAuthUrl;
            }
            set
            {
                this._OAuthUrl = value;
            }
        }
    }
}

