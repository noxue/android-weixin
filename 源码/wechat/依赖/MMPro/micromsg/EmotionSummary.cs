namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionSummary")]
    public class EmotionSummary : IExtensible
    {
        private string _CoverUrl = "";
        private string _IconUrl = "";
        private string _Introduce = "";
        private string _PackAuthInfo = "";
        private string _PackCopyright = "";
        private string _PackDesc = "";
        private uint _PackExpire;
        private uint _PackFlag;
        private string _PackName = "";
        private string _PackPrice = "";
        private uint _PackType;
        private string _PanelUrl = "";
        private string _PriceNum = "";
        private string _PriceType = "";
        private string _ProductID = "";
        private string _SendInfo = "";
        private string _TagUri = "";
        private string _TimeLimitStr = "";
        private uint _Timestamp = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(9, IsRequired=false, Name="CoverUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x12, IsRequired=false, Name="Introduce", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Introduce
        {
            get
            {
                return this._Introduce;
            }
            set
            {
                this._Introduce = value;
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

        [ProtoMember(11, IsRequired=false, Name="PackCopyright", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(10, IsRequired=true, Name="PackExpire", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(13, IsRequired=false, Name="PanelUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x10, IsRequired=false, Name="SendInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SendInfo
        {
            get
            {
                return this._SendInfo;
            }
            set
            {
                this._SendInfo = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="TagUri", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagUri
        {
            get
            {
                return this._TagUri;
            }
            set
            {
                this._TagUri = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="TimeLimitStr", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(12, IsRequired=false, Name="Timestamp", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Timestamp
        {
            get
            {
                return this._Timestamp;
            }
            set
            {
                this._Timestamp = value;
            }
        }
    }
}

