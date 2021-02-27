namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VerifyPurchaseResponse")]
    public class VerifyPurchaseResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _BizType;
        private string _SeriesID = "";
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

        [ProtoMember(3, IsRequired=true, Name="BizType", DataFormat=DataFormat.TwosComplement)]
        public uint BizType
        {
            get
            {
                return this._BizType;
            }
            set
            {
                this._BizType = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="SeriesID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SeriesID
        {
            get
            {
                return this._SeriesID;
            }
            set
            {
                this._SeriesID = value;
            }
        }
    }
}

