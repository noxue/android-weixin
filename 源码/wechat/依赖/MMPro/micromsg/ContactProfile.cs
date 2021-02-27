namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ContactProfile")]
    public class ContactProfile : IExtensible
    {
        private uint _AddContactScene;
        private string _Alias = "";
        private uint _BitMask;
        private uint _BitVal;
        private uint _ChatRoomNotify;
        private uint _ContactType;
        private string _DomainList = "";
        private string _ExtInfo = "";
        private uint _ExtUpdateSeq;
        private SKBuiltinBuffer_t _ImgBuf;
        private uint _ImgRet;
        private uint _ImgUpdateSeq;
        private string _NickName = "";
        private string _PYInitial = "";
        private string _QuanPin = "";
        private string _Remark = "";
        private string _RemarkPYInitial = "";
        private string _RemarkQuanPin = "";
        private uint _RoomInfoCount;
        private readonly List<RoomInfo> _RoomInfoList = new List<RoomInfo>();
        private int _Sex;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x12, IsRequired=true, Name="AddContactScene", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x16, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x11, IsRequired=true, Name="ChatRoomNotify", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(13, IsRequired=true, Name="ContactType", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(0x10, IsRequired=false, Name="DomainList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DomainList
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

        [ProtoMember(0x13, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(20, IsRequired=true, Name="ExtUpdateSeq", DataFormat=DataFormat.TwosComplement)]
        public uint ExtUpdateSeq
        {
            get
            {
                return this._ExtUpdateSeq;
            }
            set
            {
                this._ExtUpdateSeq = value;
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

        [ProtoMember(10, IsRequired=true, Name="ImgRet", DataFormat=DataFormat.TwosComplement)]
        public uint ImgRet
        {
            get
            {
                return this._ImgRet;
            }
            set
            {
                this._ImgRet = value;
            }
        }

        [ProtoMember(0x15, IsRequired=true, Name="ImgUpdateSeq", DataFormat=DataFormat.TwosComplement)]
        public uint ImgUpdateSeq
        {
            get
            {
                return this._ImgUpdateSeq;
            }
            set
            {
                this._ImgUpdateSeq = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NickName
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

        [ProtoMember(3, IsRequired=false, Name="PYInitial", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PYInitial
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

        [ProtoMember(4, IsRequired=false, Name="QuanPin", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QuanPin
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

        [ProtoMember(9, IsRequired=false, Name="Remark", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Remark
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

        [ProtoMember(11, IsRequired=false, Name="RemarkPYInitial", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RemarkPYInitial
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

        [ProtoMember(12, IsRequired=false, Name="RemarkQuanPin", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RemarkQuanPin
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

        [ProtoMember(14, IsRequired=true, Name="RoomInfoCount", DataFormat=DataFormat.TwosComplement)]
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

