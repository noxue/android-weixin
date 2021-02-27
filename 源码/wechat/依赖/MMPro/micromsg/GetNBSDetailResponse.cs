namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetNBSDetailResponse")]
    public class GetNBSDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _CardInfo = "";
        private string _DetailInfo = "";
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

        [ProtoMember(3, IsRequired=false, Name="CardInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CardInfo
        {
            get
            {
                return this._CardInfo;
            }
            set
            {
                this._CardInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="DetailInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DetailInfo
        {
            get
            {
                return this._DetailInfo;
            }
            set
            {
                this._DetailInfo = value;
            }
        }
    }
}

