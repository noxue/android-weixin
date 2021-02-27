namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CreateTalkRoomResponse")]
    public class CreateTalkRoomResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _BigHeadImgUrl = "";
        private SKBuiltinBuffer_t _ImgBuf;
        private uint _MemberCount;
        private readonly List<MemberResp> _MemberList = new List<MemberResp>();
        private int _MicSeq;
        private int _MyRoomMemberId;
        private SKBuiltinString_t _PYInitial;
        private SKBuiltinString_t _QuanPin;
        private int _RoomId;
        private long _RoomKey;
        private string _SmallHeadImgUrl = "";
        private SKBuiltinString_t _TalkRoomName;
        private SKBuiltinString_t _Topic;
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

        [ProtoMember(9, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=true, Name="ImgBuf", DataFormat=DataFormat.Default)]
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

        [ProtoMember(5, IsRequired=true, Name="MemberCount", DataFormat=DataFormat.TwosComplement)]
        public uint MemberCount
        {
            get
            {
                return this._MemberCount;
            }
            set
            {
                this._MemberCount = value;
            }
        }

        [ProtoMember(6, Name="MemberList", DataFormat=DataFormat.Default)]
        public List<MemberResp> MemberList
        {
            get
            {
                return this._MemberList;
            }
        }

        [ProtoMember(13, IsRequired=true, Name="MicSeq", DataFormat=DataFormat.TwosComplement)]
        public int MicSeq
        {
            get
            {
                return this._MicSeq;
            }
            set
            {
                this._MicSeq = value;
            }
        }

        [ProtoMember(14, IsRequired=true, Name="MyRoomMemberId", DataFormat=DataFormat.TwosComplement)]
        public int MyRoomMemberId
        {
            get
            {
                return this._MyRoomMemberId;
            }
            set
            {
                this._MyRoomMemberId = value;
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

        [ProtoMember(11, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
        public int RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
        public long RoomKey
        {
            get
            {
                return this._RoomKey;
            }
            set
            {
                this._RoomKey = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=true, Name="TalkRoomName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t TalkRoomName
        {
            get
            {
                return this._TalkRoomName;
            }
            set
            {
                this._TalkRoomName = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Topic", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t Topic
        {
            get
            {
                return this._Topic;
            }
            set
            {
                this._Topic = value;
            }
        }
    }
}

