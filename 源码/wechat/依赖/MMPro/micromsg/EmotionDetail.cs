namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionDetail")]
    public class EmotionDetail : IExtensible
    {
        private string _ConsumeProductID = "";
        private string _CoverUrl = "";
        private string _IconUrl = "";
        private string _OldRedirectUrl = "";
        private string _PackAuthInfo = "";
        private string _PackCopyright = "";
        private string _PackDesc = "";
        private uint _PackExpire;
        private uint _PackFlag;
        private string _PackName = "";
        private string _PackPrice = "";
        private uint _PackThumbCnt;
        private readonly List<SKBuiltinString_t> _PackThumbList = new List<SKBuiltinString_t>();
        private uint _PackType;
        private string _PanelUrl = "";
        private string _PriceNum = "";
        private string _PriceType = "";
        private string _ProductID = "";
        private string _ShareDesc = "";
        private uint _ThumbExtCount = 0;
        private readonly List<PackThumbExt> _ThumbExtList = new List<PackThumbExt>();
        private string _TimeLimitStr = "";
        private int _Version = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x11, IsRequired=false, Name="ConsumeProductID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ConsumeProductID
        {
            get
            {
                return this._ConsumeProductID;
            }
            set
            {
                this._ConsumeProductID = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="CoverUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CoverUrl
        {
            get
            {
                return this._CoverUrl;
            }
            set
            {
                this._CoverUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="IconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrl
        {
            get
            {
                return this._IconUrl;
            }
            set
            {
                this._IconUrl = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="OldRedirectUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OldRedirectUrl
        {
            get
            {
                return this._OldRedirectUrl;
            }
            set
            {
                this._OldRedirectUrl = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PackAuthInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackAuthInfo
        {
            get
            {
                return this._PackAuthInfo;
            }
            set
            {
                this._PackAuthInfo = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="PackCopyright", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackCopyright
        {
            get
            {
                return this._PackCopyright;
            }
            set
            {
                this._PackCopyright = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="PackDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackDesc
        {
            get
            {
                return this._PackDesc;
            }
            set
            {
                this._PackDesc = value;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="PackExpire", DataFormat=DataFormat.TwosComplement)]
        public uint PackExpire
        {
            get
            {
                return this._PackExpire;
            }
            set
            {
                this._PackExpire = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="PackFlag", DataFormat=DataFormat.TwosComplement)]
        public uint PackFlag
        {
            get
            {
                return this._PackFlag;
            }
            set
            {
                this._PackFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="PackName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackName
        {
            get
            {
                return this._PackName;
            }
            set
            {
                this._PackName = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="PackPrice", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PackPrice
        {
            get
            {
                return this._PackPrice;
            }
            set
            {
                this._PackPrice = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="PackThumbCnt", DataFormat=DataFormat.TwosComplement)]
        public uint PackThumbCnt
        {
            get
            {
                return this._PackThumbCnt;
            }
            set
            {
                this._PackThumbCnt = value;
            }
        }

        [ProtoMember(10, Name="PackThumbList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> PackThumbList
        {
            get
            {
                return this._PackThumbList;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="PackType", DataFormat=DataFormat.TwosComplement)]
        public uint PackType
        {
            get
            {
                return this._PackType;
            }
            set
            {
                this._PackType = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="PanelUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PanelUrl
        {
            get
            {
                return this._PanelUrl;
            }
            set
            {
                this._PanelUrl = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="PriceNum", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PriceNum
        {
            get
            {
                return this._PriceNum;
            }
            set
            {
                this._PriceNum = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="PriceType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PriceType
        {
            get
            {
                return this._PriceType;
            }
            set
            {
                this._PriceType = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="ProductID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this._ProductID = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="ShareDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ShareDesc
        {
            get
            {
                return this._ShareDesc;
            }
            set
            {
                this._ShareDesc = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="ThumbExtCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ThumbExtCount
        {
            get
            {
                return this._ThumbExtCount;
            }
            set
            {
                this._ThumbExtCount = value;
            }
        }

        [ProtoMember(0x13, Name="ThumbExtList", DataFormat=DataFormat.Default)]
        public List<PackThumbExt> ThumbExtList
        {
            get
            {
                return this._ThumbExtList;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="TimeLimitStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TimeLimitStr
        {
            get
            {
                return this._TimeLimitStr;
            }
            set
            {
                this._TimeLimitStr = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="Version", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }
    }
}

