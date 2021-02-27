namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetGameRankListResponse")]
    public class GetGameRankListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private uint _FriendsCount = 0;
        private uint _HasReportScore = 0;
        private readonly List<UserGameRankInfo> _RankList = new List<UserGameRankInfo>();
        private YYBStruct _SYYB = null;
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

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="FriendsCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FriendsCount
        {
            get
            {
                return this._FriendsCount;
            }
            set
            {
                this._FriendsCount = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="HasReportScore", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint HasReportScore
        {
            get
            {
                return this._HasReportScore;
            }
            set
            {
                this._HasReportScore = value;
            }
        }

        [ProtoMember(3, Name="RankList", DataFormat=DataFormat.Default)]
        public List<UserGameRankInfo> RankList
        {
            get
            {
                return this._RankList;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="SYYB", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public YYBStruct SYYB
        {
            get
            {
                return this._SYYB;
            }
            set
            {
                this._SYYB = value;
            }
        }
    }
}

