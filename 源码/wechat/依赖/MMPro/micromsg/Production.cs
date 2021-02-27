namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Production")]
    public class Production : IExtensible
    {
        private uint _Count;
        private uint _DiscountCount = 0;
        private readonly List<Discount> _Discounts = new List<Discount>();
        private string _Name = "";
        private string _Pid = "";
        private string _PreferentialPrice = "";
        private string _PriceType = "";
        private uint _RealPrice;
        private string _SafeUrl = "";
        private uint _Scene = 0;
        private uint _SkuCount;
        private string _SkuId = "";
        private readonly List<KVItem> _Skus = new List<KVItem>();
        private uint _SubType = 0;
        private string _ThumbUrl = "";
        private uint _Type = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(15, IsRequired=false, Name="DiscountCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DiscountCount
        {
            get
            {
                return this._DiscountCount;
            }
            set
            {
                this._DiscountCount = value;
            }
        }

        [ProtoMember(0x10, Name="Discounts", DataFormat=DataFormat.Default)]
        public List<Discount> Discounts
        {
            get
            {
                return this._Discounts;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Pid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pid
        {
            get
            {
                return this._Pid;
            }
            set
            {
                this._Pid = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="PreferentialPrice", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PreferentialPrice
        {
            get
            {
                return this._PreferentialPrice;
            }
            set
            {
                this._PreferentialPrice = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PriceType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=true, Name="RealPrice", DataFormat=DataFormat.TwosComplement)]
        public uint RealPrice
        {
            get
            {
                return this._RealPrice;
            }
            set
            {
                this._RealPrice = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="SafeUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SafeUrl
        {
            get
            {
                return this._SafeUrl;
            }
            set
            {
                this._SafeUrl = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="SkuCount", DataFormat=DataFormat.TwosComplement)]
        public uint SkuCount
        {
            get
            {
                return this._SkuCount;
            }
            set
            {
                this._SkuCount = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="SkuId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SkuId
        {
            get
            {
                return this._SkuId;
            }
            set
            {
                this._SkuId = value;
            }
        }

        [ProtoMember(2, Name="Skus", DataFormat=DataFormat.Default)]
        public List<KVItem> Skus
        {
            get
            {
                return this._Skus;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="SubType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SubType
        {
            get
            {
                return this._SubType;
            }
            set
            {
                this._SubType = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="ThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ThumbUrl
        {
            get
            {
                return this._ThumbUrl;
            }
            set
            {
                this._ThumbUrl = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

