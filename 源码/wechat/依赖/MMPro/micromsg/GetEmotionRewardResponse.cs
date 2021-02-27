namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetEmotionRewardResponse")]
    public class GetEmotionRewardResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _DonorNum = 0;
        private readonly List<EmotionDonor> _Donors = new List<EmotionDonor>();
        private readonly List<EmotionPrice> _Price = new List<EmotionPrice>();
        private EmotionReward _Reward = null;
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

        [ProtoMember(3, IsRequired=false, Name="DonorNum", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(4, Name="Donors", DataFormat=DataFormat.Default)]
        public List<EmotionDonor> Donors
        {
            get
            {
                return this._Donors;
            }
        }

        [ProtoMember(2, Name="Price", DataFormat=DataFormat.Default)]
        public List<EmotionPrice> Price
        {
            get
            {
                return this._Price;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Reward", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public EmotionReward Reward
        {
            get
            {
                return this._Reward;
            }
            set
            {
                this._Reward = value;
            }
        }
    }
}

