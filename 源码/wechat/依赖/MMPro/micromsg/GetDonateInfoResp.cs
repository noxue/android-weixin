namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetDonateInfoResp")]
    public class GetDonateInfoResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _DonateInfo = "";
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

        [ProtoMember(3, IsRequired=false, Name="DonateInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DonateInfo
        {
            get
            {
                return this._DonateInfo;
            }
            set
            {
                this._DonateInfo = value;
            }
        }
    }
}

