namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GameInitResp")]
    public class GameInitResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _BuyEngineOilWording = "";
        private uint _CheckLeftTime;
        private int _FriendCount;
        private readonly List<UserGameInfo> _FriendList = new List<UserGameInfo>();
        private int _GameAnnouncementCount = 0;
        private readonly List<GameAnnouncementInfo> _GameAnnouncementList = new List<GameAnnouncementInfo>();
        private uint _GameCoinCount = 0;
        private string _GameNumerConfig = "";
        private int _GamePropsCount = 0;
        private readonly List<GameUserPropsInfo> _GamePropsList = new List<GameUserPropsInfo>();
        private uint _LifeNum;
        private string _OilCurrency = "";
        private string _OilPrice = "";
        private uint _ProductIdCount = 0;
        private readonly List<SKBuiltinString_t> _ProductIdList = new List<SKBuiltinString_t>();
        private string _PropsViewTip = "";
        private string _RankViewTip = "";
        private string _Token = "";
        private int _WishCount;
        private readonly List<UserGameWishInfo> _WishList = new List<UserGameWishInfo>();
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

        [ProtoMember(0x13, IsRequired=false, Name="BuyEngineOilWording", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BuyEngineOilWording
        {
            get
            {
                return this._BuyEngineOilWording;
            }
            set
            {
                this._BuyEngineOilWording = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="CheckLeftTime", DataFormat=DataFormat.TwosComplement)]
        public uint CheckLeftTime
        {
            get
            {
                return this._CheckLeftTime;
            }
            set
            {
                this._CheckLeftTime = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="FriendCount", DataFormat=DataFormat.TwosComplement)]
        public int FriendCount
        {
            get
            {
                return this._FriendCount;
            }
            set
            {
                this._FriendCount = value;
            }
        }

        [ProtoMember(7, Name="FriendList", DataFormat=DataFormat.Default)]
        public List<UserGameInfo> FriendList
        {
            get
            {
                return this._FriendList;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="GameAnnouncementCount", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int GameAnnouncementCount
        {
            get
            {
                return this._GameAnnouncementCount;
            }
            set
            {
                this._GameAnnouncementCount = value;
            }
        }

        [ProtoMember(13, Name="GameAnnouncementList", DataFormat=DataFormat.Default)]
        public List<GameAnnouncementInfo> GameAnnouncementList
        {
            get
            {
                return this._GameAnnouncementList;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="GameCoinCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(3, IsRequired=false, Name="GameNumerConfig", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GameNumerConfig
        {
            get
            {
                return this._GameNumerConfig;
            }
            set
            {
                this._GameNumerConfig = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="GamePropsCount", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int GamePropsCount
        {
            get
            {
                return this._GamePropsCount;
            }
            set
            {
                this._GamePropsCount = value;
            }
        }

        [ProtoMember(11, Name="GamePropsList", DataFormat=DataFormat.Default)]
        public List<GameUserPropsInfo> GamePropsList
        {
            get
            {
                return this._GamePropsList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="LifeNum", DataFormat=DataFormat.TwosComplement)]
        public uint LifeNum
        {
            get
            {
                return this._LifeNum;
            }
            set
            {
                this._LifeNum = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="OilCurrency", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OilCurrency
        {
            get
            {
                return this._OilCurrency;
            }
            set
            {
                this._OilCurrency = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="OilPrice", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OilPrice
        {
            get
            {
                return this._OilPrice;
            }
            set
            {
                this._OilPrice = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="ProductIdCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ProductIdCount
        {
            get
            {
                return this._ProductIdCount;
            }
            set
            {
                this._ProductIdCount = value;
            }
        }

        [ProtoMember(0x12, Name="ProductIdList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> ProductIdList
        {
            get
            {
                return this._ProductIdList;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="PropsViewTip", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PropsViewTip
        {
            get
            {
                return this._PropsViewTip;
            }
            set
            {
                this._PropsViewTip = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="RankViewTip", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RankViewTip
        {
            get
            {
                return this._RankViewTip;
            }
            set
            {
                this._RankViewTip = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=true, Name="WishCount", DataFormat=DataFormat.TwosComplement)]
        public int WishCount
        {
            get
            {
                return this._WishCount;
            }
            set
            {
                this._WishCount = value;
            }
        }

        [ProtoMember(9, Name="WishList", DataFormat=DataFormat.Default)]
        public List<UserGameWishInfo> WishList
        {
            get
            {
                return this._WishList;
            }
        }
    }
}

