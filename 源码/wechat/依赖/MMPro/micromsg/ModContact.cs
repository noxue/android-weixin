namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ModContact")]
    public class ModContact : IExtensible
    {
        private uint _AddContactScene = 0;
        private micromsg.AdditionalContactList _AdditionalContactList = null;
        private string _AlbumBGImgID = "";
        private int _AlbumFlag = 0;
        private int _AlbumStyle = 0;
        private string _Alias = "";
        private string _BigHeadImgUrl = "";
        private uint _BitMask;
        private uint _BitVal;
        private string _CardImgUrl = "";
        private string _ChatRoomData = "";
        private uint _ChatroomMaxCount = 0;
        private uint _ChatRoomNotify = 0;
        private string _ChatRoomOwner = "";
        private uint _ChatroomType = 0;
        private uint _ChatroomVersion = 0;
        private string _City = "";
        private uint _ContactType = 0;
        private string _Country = "";
        private micromsg.CustomizedInfo _CustomizedInfo = null;
        private int _DeleteFlag = 0;
        private string _Description = "";
        private SKBuiltinString_t _DomainList = null;
        private string _EncryptUserName = "";
        private string _ExtInfo = "";
        private uint _HasWeiXinHdHeadImg = 0;
        private string _HeadImgMd5 = "";
        private string _IDCardNum = "";
        private SKBuiltinBuffer_t _ImgBuf;
        private uint _ImgFlag;
        private string _LabelIDList = "";
        private int _Level = 0;
        private string _MobileFullHash = "";
        private string _MobileHash = "";
        private string _MyBrandList = "";
        private ChatRoomMemberData _NewChatroomData = null;
        private SKBuiltinString_t _NickName;
        private uint _PersonalCard = 0;
        private string _Province = "";
        private SKBuiltinString_t _PYInitial;
        private SKBuiltinString_t _QuanPin;
        private string _RealName = "";
        private SKBuiltinString_t _Remark = null;
        private SKBuiltinString_t _RemarkPYInitial = null;
        private SKBuiltinString_t _RemarkQuanPin = null;
        private uint _RoomInfoCount = 0;
        private readonly List<RoomInfo> _RoomInfoList = new List<RoomInfo>();
        private int _Sex;
        private string _Signature = "";
        private string _SmallHeadImgUrl = "";
        private micromsg.SnsUserInfo _SnsUserInfo = null;
        private uint _Source = 0;
        private SKBuiltinString_t _UserName;
        private string _VerifyContent = "";
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

        [ProtoMember(0x12, IsRequired=false, Name="AddContactScene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AddContactScene
        {
            get
            {
                return this._AddContactScene;
            }
            set
            {
                this._AddContactScene = value;
            }
        }

        [ProtoMember(50, IsRequired=false, Name="AdditionalContactList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.AdditionalContactList AdditionalContactList
        {
            get
            {
                return this._AdditionalContactList;
            }
            set
            {
                this._AdditionalContactList = value;
            }
        }

        [ProtoMember(0x24, IsRequired=false, Name="AlbumBGImgID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x23, IsRequired=false, Name="AlbumFlag", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(0x22, IsRequired=false, Name="AlbumStyle", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(30, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x27, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=true, Name="BitMask", DataFormat=DataFormat.TwosComplement)]
        public uint BitMask
        {
            get
            {
                return this._BitMask;
            }
            set
            {
                this._BitMask = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="BitVal", DataFormat=DataFormat.TwosComplement)]
        public uint BitVal
        {
            get
            {
                return this._BitVal;
            }
            set
            {
                this._BitVal = value;
            }
        }

        [ProtoMember(60, IsRequired=false, Name="CardImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CardImgUrl
        {
            get
            {
                return this._CardImgUrl;
            }
            set
            {
                this._CardImgUrl = value;
            }
        }

        [ProtoMember(0x2b, IsRequired=false, Name="ChatRoomData", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomData
        {
            get
            {
                return this._ChatRoomData;
            }
            set
            {
                this._ChatRoomData = value;
            }
        }

        [ProtoMember(0x37, IsRequired=false, Name="ChatroomMaxCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChatroomMaxCount
        {
            get
            {
                return this._ChatroomMaxCount;
            }
            set
            {
                this._ChatroomMaxCount = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="ChatRoomNotify", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChatRoomNotify
        {
            get
            {
                return this._ChatRoomNotify;
            }
            set
            {
                this._ChatRoomNotify = value;
            }
        }

        [ProtoMember(0x1f, IsRequired=false, Name="ChatRoomOwner", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomOwner
        {
            get
            {
                return this._ChatRoomOwner;
            }
            set
            {
                this._ChatRoomOwner = value;
            }
        }

        [ProtoMember(0x38, IsRequired=false, Name="ChatroomType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChatroomType
        {
            get
            {
                return this._ChatroomType;
            }
            set
            {
                this._ChatroomType = value;
            }
        }

        [ProtoMember(0x35, IsRequired=false, Name="ChatroomVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChatroomVersion
        {
            get
            {
                return this._ChatroomVersion;
            }
            set
            {
                this._ChatroomVersion = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="City", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(13, IsRequired=false, Name="ContactType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ContactType
        {
            get
            {
                return this._ContactType;
            }
            set
            {
                this._ContactType = value;
            }
        }

        [ProtoMember(0x26, IsRequired=false, Name="Country", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x2a, IsRequired=false, Name="CustomizedInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(0x3a, IsRequired=false, Name="DeleteFlag", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int DeleteFlag
        {
            get
            {
                return this._DeleteFlag;
            }
            set
            {
                this._DeleteFlag = value;
            }
        }

        [ProtoMember(0x3b, IsRequired=false, Name="Description", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="DomainList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t DomainList
        {
            get
            {
                return this._DomainList;
            }
            set
            {
                this._DomainList = value;
            }
        }

        [ProtoMember(0x2d, IsRequired=false, Name="EncryptUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string EncryptUserName
        {
            get
            {
                return this._EncryptUserName;
            }
            set
            {
                this._EncryptUserName = value;
            }
        }

        [ProtoMember(0x36, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="HasWeiXinHdHeadImg", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint HasWeiXinHdHeadImg
        {
            get
            {
                return this._HasWeiXinHdHeadImg;
            }
            set
            {
                this._HasWeiXinHdHeadImg = value;
            }
        }

        [ProtoMember(0x2c, IsRequired=false, Name="HeadImgMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string HeadImgMd5
        {
            get
            {
                return this._HeadImgMd5;
            }
            set
            {
                this._HeadImgMd5 = value;
            }
        }

        [ProtoMember(0x2e, IsRequired=false, Name="IDCardNum", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IDCardNum
        {
            get
            {
                return this._IDCardNum;
            }
            set
            {
                this._IDCardNum = value;
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

        [ProtoMember(9, IsRequired=true, Name="ImgFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x3d, IsRequired=false, Name="LabelIDList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LabelIDList
        {
            get
            {
                return this._LabelIDList;
            }
            set
            {
                this._LabelIDList = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="Level", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Level
        {
            get
            {
                return this._Level;
            }
            set
            {
                this._Level = value;
            }
        }

        [ProtoMember(0x31, IsRequired=false, Name="MobileFullHash", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MobileFullHash
        {
            get
            {
                return this._MobileFullHash;
            }
            set
            {
                this._MobileFullHash = value;
            }
        }

        [ProtoMember(0x30, IsRequired=false, Name="MobileHash", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MobileHash
        {
            get
            {
                return this._MobileHash;
            }
            set
            {
                this._MobileHash = value;
            }
        }

        [ProtoMember(0x29, IsRequired=false, Name="MyBrandList", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x39, IsRequired=false, Name="NewChatroomData", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ChatRoomMemberData NewChatroomData
        {
            get
            {
                return this._NewChatroomData;
            }
            set
            {
                this._NewChatroomData = value;
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

        [ProtoMember(0x16, IsRequired=false, Name="PersonalCard", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x13, IsRequired=false, Name="Province", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x2f, IsRequired=false, Name="RealName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RealName
        {
            get
            {
                return this._RealName;
            }
            set
            {
                this._RealName = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Remark", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="RemarkPYInitial", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t RemarkPYInitial
        {
            get
            {
                return this._RemarkPYInitial;
            }
            set
            {
                this._RemarkPYInitial = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="RemarkQuanPin", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t RemarkQuanPin
        {
            get
            {
                return this._RemarkQuanPin;
            }
            set
            {
                this._RemarkQuanPin = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="RoomInfoCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RoomInfoCount
        {
            get
            {
                return this._RoomInfoCount;
            }
            set
            {
                this._RoomInfoCount = value;
            }
        }

        [ProtoMember(15, Name="RoomInfoList", DataFormat=DataFormat.Default)]
        public List<RoomInfo> RoomInfoList
        {
            get
            {
                return this._RoomInfoList;
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

        [ProtoMember(0x15, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(40, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x25, IsRequired=false, Name="SnsUserInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(0x1b, IsRequired=false, Name="Source", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = value;
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

        [ProtoMember(0x1d, IsRequired=false, Name="VerifyContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyContent
        {
            get
            {
                return this._VerifyContent;
            }
            set
            {
                this._VerifyContent = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="VerifyFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x19, IsRequired=false, Name="VerifyInfo", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x1c, IsRequired=false, Name="Weibo", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x21, IsRequired=false, Name="WeiboFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x20, IsRequired=false, Name="WeiboNickname", DataFormat=DataFormat.Default), DefaultValue("")]
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

