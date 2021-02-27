namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GameEndReq")]
    public class GameEndReq : IExtensible
    {
        private string _AppID = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _ClientUseReviveNum = 0;
        private uint _ConsumeTime;
        private int _Count;
        private uint _DeadCount = 0;
        private readonly List<EnemyGameKilled> _EnemyKilled = new List<EnemyGameKilled>();
        private uint _GameCoinCount = 0;
        private int _GameConsumePropsCount = 0;
        private readonly List<GameConsumeProps> _GameConsumePropsList = new List<GameConsumeProps>();
        private uint _GameEndTime = 0;
        private uint _GameStartTime = 0;
        private uint _LocalScore;
        private uint _ShieldNum = 0;
        private string _Token = "";
        private uint _TotalShots = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppID
        {
            get
            {
                return this._AppID;
            }
            set
            {
                this._AppID = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="ClientUseReviveNum", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ClientUseReviveNum
        {
            get
            {
                return this._ClientUseReviveNum;
            }
            set
            {
                this._ClientUseReviveNum = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="ConsumeTime", DataFormat=DataFormat.TwosComplement)]
        public uint ConsumeTime
        {
            get
            {
                return this._ConsumeTime;
            }
            set
            {
                this._ConsumeTime = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public int Count
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

        [ProtoMember(8, IsRequired=false, Name="DeadCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DeadCount
        {
            get
            {
                return this._DeadCount;
            }
            set
            {
                this._DeadCount = value;
            }
        }

        [ProtoMember(4, Name="EnemyKilled", DataFormat=DataFormat.Default)]
        public List<EnemyGameKilled> EnemyKilled
        {
            get
            {
                return this._EnemyKilled;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="GameCoinCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GameCoinCount
        {
            get
            {
                return this._GameCoinCount;
            }
            set
            {
                this._GameCoinCount = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="GameConsumePropsCount", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int GameConsumePropsCount
        {
            get
            {
                return this._GameConsumePropsCount;
            }
            set
            {
                this._GameConsumePropsCount = value;
            }
        }

        [ProtoMember(10, Name="GameConsumePropsList", DataFormat=DataFormat.Default)]
        public List<GameConsumeProps> GameConsumePropsList
        {
            get
            {
                return this._GameConsumePropsList;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="GameEndTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GameEndTime
        {
            get
            {
                return this._GameEndTime;
            }
            set
            {
                this._GameEndTime = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="GameStartTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GameStartTime
        {
            get
            {
                return this._GameStartTime;
            }
            set
            {
                this._GameStartTime = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="LocalScore", DataFormat=DataFormat.TwosComplement)]
        public uint LocalScore
        {
            get
            {
                return this._LocalScore;
            }
            set
            {
                this._LocalScore = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="ShieldNum", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ShieldNum
        {
            get
            {
                return this._ShieldNum;
            }
            set
            {
                this._ShieldNum = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Token
        {
            get
            {
                return this._Token;
            }
            set
            {
                this._Token = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="TotalShots", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TotalShots
        {
            get
            {
                return this._TotalShots;
            }
            set
            {
                this._TotalShots = value;
            }
        }
    }
}

