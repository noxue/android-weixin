namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GameStartReq")]
    public class GameStartReq : IExtensible
    {
        private string _AppID = "";
        private micromsg.BaseRequest _BaseRequest;
        private int _GameConsumePropsCount = 0;
        private readonly List<GameConsumeProps> _GameConsumePropsList = new List<GameConsumeProps>();
        private uint _LocalLifeNum;
        private uint _NeedClearWishList;
        private string _Token = "";
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

        [ProtoMember(6, IsRequired=false, Name="GameConsumePropsCount", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(7, Name="GameConsumePropsList", DataFormat=DataFormat.Default)]
        public List<GameConsumeProps> GameConsumePropsList
        {
            get
            {
                return this._GameConsumePropsList;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="LocalLifeNum", DataFormat=DataFormat.TwosComplement)]
        public uint LocalLifeNum
        {
            get
            {
                return this._LocalLifeNum;
            }
            set
            {
                this._LocalLifeNum = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="NeedClearWishList", DataFormat=DataFormat.TwosComplement)]
        public uint NeedClearWishList
        {
            get
            {
                return this._NeedClearWishList;
            }
            set
            {
                this._NeedClearWishList = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

