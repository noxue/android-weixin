namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetEmotionDonorListResponse")]
    public class GetEmotionDonorListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _DonorNum;
        private readonly List<EmotionDonor> _Donors = new List<EmotionDonor>();
        private SKBuiltinBuffer_t _RespBuf = null;
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

        [ProtoMember(2, IsRequired=true, Name="DonorNum", DataFormat=DataFormat.TwosComplement)]
        public uint DonorNum
        {
            get
            {
                return this._DonorNum;
            }
            set
            {
                this._DonorNum = value;
            }
        }

        [ProtoMember(3, Name="Donors", DataFormat=DataFormat.Default)]
        public List<EmotionDonor> Donors
        {
            get
            {
                return this._Donors;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="RespBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t RespBuf
        {
            get
            {
                return this._RespBuf;
            }
            set
            {
                this._RespBuf = value;
            }
        }
    }
}

