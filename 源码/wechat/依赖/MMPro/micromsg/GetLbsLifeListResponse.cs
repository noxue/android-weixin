namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLbsLifeListResponse")]
    public class GetLbsLifeListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _Buff;
        private uint _ContinueFlag;
        private uint _IconCount;
        private readonly List<SKBuiltinString_t> _IconList = new List<SKBuiltinString_t>();
        private uint _LifeCount;
        private readonly List<LbsLife> _LifeList = new List<LbsLife>();
        private string _LogoUrl = "";
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

        [ProtoMember(2, IsRequired=true, Name="Buff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buff
        {
            get
            {
                return this._Buff;
            }
            set
            {
                this._Buff = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ContinueFlag
        {
            get
            {
                return this._ContinueFlag;
            }
            set
            {
                this._ContinueFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="IconCount", DataFormat=DataFormat.TwosComplement)]
        public uint IconCount
        {
            get
            {
                return this._IconCount;
            }
            set
            {
                this._IconCount = value;
            }
        }

        [ProtoMember(4, Name="IconList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> IconList
        {
            get
            {
                return this._IconList;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="LifeCount", DataFormat=DataFormat.TwosComplement)]
        public uint LifeCount
        {
            get
            {
                return this._LifeCount;
            }
            set
            {
                this._LifeCount = value;
            }
        }

        [ProtoMember(6, Name="LifeList", DataFormat=DataFormat.Default)]
        public List<LbsLife> LifeList
        {
            get
            {
                return this._LifeList;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="LogoUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LogoUrl
        {
            get
            {
                return this._LogoUrl;
            }
            set
            {
                this._LogoUrl = value;
            }
        }
    }
}

