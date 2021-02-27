namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ModBottleContact")]
    public class ModBottleContact : IExtensible
    {
        private string _BigHeadImgUrl = "";
        private string _City = "";
        private string _Country = "";
        private uint _HDImgFlag;
        private uint _ImgFlag;
        private string _Province = "";
        private uint _Sex;
        private string _Signature = "";
        private string _SmallHeadImgUrl = "";
        private uint _Type;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(10, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BigHeadImgUrl
        {
            get
            {
                return this._BigHeadImgUrl;
            }
            set
            {
                this._BigHeadImgUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="City", DataFormat=DataFormat.Default), DefaultValue("")]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Country", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                this._Country = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="HDImgFlag", DataFormat=DataFormat.TwosComplement)]
        public uint HDImgFlag
        {
            get
            {
                return this._HDImgFlag;
            }
            set
            {
                this._HDImgFlag = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="ImgFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ImgFlag
        {
            get
            {
                return this._ImgFlag;
            }
            set
            {
                this._ImgFlag = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Province", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Province
        {
            get
            {
                return this._Province;
            }
            set
            {
                this._Province = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Sex", DataFormat=DataFormat.TwosComplement)]
        public uint Sex
        {
            get
            {
                return this._Sex;
            }
            set
            {
                this._Sex = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Signature
        {
            get
            {
                return this._Signature;
            }
            set
            {
                this._Signature = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallHeadImgUrl
        {
            get
            {
                return this._SmallHeadImgUrl;
            }
            set
            {
                this._SmallHeadImgUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

