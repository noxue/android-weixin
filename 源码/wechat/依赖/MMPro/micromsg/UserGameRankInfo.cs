namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UserGameRankInfo")]
    public class UserGameRankInfo : IExtensible
    {
        private uint _Rank = 0;
        private uint _Score;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Rank", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Rank
        {
            get
            {
                return this._Rank;
            }
            set
            {
                this._Rank = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Score", DataFormat=DataFormat.TwosComplement)]
        public uint Score
        {
            get
            {
                return this._Score;
            }
            set
            {
                this._Score = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

