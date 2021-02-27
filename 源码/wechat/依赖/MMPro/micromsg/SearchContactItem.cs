namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SearchContactItem")]
    public class SearchContactItem : IExtensible
    {
        private string _AlbumBGImgID = "";
        private int _AlbumFlag = 0;
        private int _AlbumStyle = 0;
        private string _Alias = "";
        private string _AntispamTicket = "";
        private string _BigHeadImgUrl = "";
        private string _City = "";
        private string _Country = "";
        private micromsg.CustomizedInfo _CustomizedInfo = null;
        private SKBuiltinBuffer_t _ImgBuf;
        private uint _MatchType = 0;
        private string _MyBrandList = "";
        private SKBuiltinString_t _NickName;
        private uint _PersonalCard = 0;
        private string _Province = "";
        private SKBuiltinString_t _PYInitial;
        private SKBuiltinString_t _QuanPin;
        private int _Sex;
        private string _Signature = "";
        private string _SmallHeadImgUrl = "";
        private micromsg.SnsUserInfo _SnsUserInfo = null;
        private SKBuiltinString_t _UserName;
        private uint _VerifyFlag = 0;
        private string _VerifyInfo = "";
        private string _Weibo = "";
        private uint _WeiboFlag = 0;
        private string _WeiboNickname = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x13, IsRequired=false, Name="AlbumBGImgID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AlbumBGImgID
        {
            get
            {
                return this._AlbumBGImgID;
            }
            set
            {
                this._AlbumBGImgID = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="AlbumFlag", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AlbumFlag
        {
            get
            {
                return this._AlbumFlag;
            }
            set
            {
                this._AlbumFlag = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="AlbumStyle", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AlbumStyle
        {
            get
            {
                return this._AlbumStyle;
            }
            set
            {
                this._AlbumStyle = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Alias
        {
            get
            {
                return this._Alias;
            }
            set
            {
                this._Alias = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="AntispamTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AntispamTicket
        {
            get
            {
                return this._AntispamTicket;
            }
            set
            {
                this._AntispamTicket = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=false, Name="City", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x15, IsRequired=false, Name="Country", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x17, IsRequired=false, Name="CustomizedInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.CustomizedInfo CustomizedInfo
        {
            get
            {
                return this._CustomizedInfo;
            }
            set
            {
                this._CustomizedInfo = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="ImgBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ImgBuf
        {
            get
            {
                return this._ImgBuf;
            }
            set
            {
                this._ImgBuf = value;
            }
        }

        [ProtoMember(0x1b, IsRequired=false, Name="MatchType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MatchType
        {
            get
            {
                return this._MatchType;
            }
            set
            {
                this._MatchType = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="MyBrandList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MyBrandList
        {
            get
            {
                return this._MyBrandList;
            }
            set
            {
                this._MyBrandList = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="NickName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="PersonalCard", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PersonalCard
        {
            get
            {
                return this._PersonalCard;
            }
            set
            {
                this._PersonalCard = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Province", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=true, Name="PYInitial", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t PYInitial
        {
            get
            {
                return this._PYInitial;
            }
            set
            {
                this._PYInitial = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="QuanPin", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t QuanPin
        {
            get
            {
                return this._QuanPin;
            }
            set
            {
                this._QuanPin = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Sex", DataFormat=DataFormat.TwosComplement)]
        public int Sex
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

        [ProtoMember(9, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x19, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(20, IsRequired=false, Name="SnsUserInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.SnsUserInfo SnsUserInfo
        {
            get
            {
                return this._SnsUserInfo;
            }
            set
            {
                this._SnsUserInfo = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t UserName
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

        [ProtoMember(11, IsRequired=false, Name="VerifyFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint VerifyFlag
        {
            get
            {
                return this._VerifyFlag;
            }
            set
            {
                this._VerifyFlag = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="VerifyInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyInfo
        {
            get
            {
                return this._VerifyInfo;
            }
            set
            {
                this._VerifyInfo = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="Weibo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Weibo
        {
            get
            {
                return this._Weibo;
            }
            set
            {
                this._Weibo = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="WeiboFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint WeiboFlag
        {
            get
            {
                return this._WeiboFlag;
            }
            set
            {
                this._WeiboFlag = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="WeiboNickname", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WeiboNickname
        {
            get
            {
                return this._WeiboNickname;
            }
            set
            {
                this._WeiboNickname = value;
            }
        }
    }
}

