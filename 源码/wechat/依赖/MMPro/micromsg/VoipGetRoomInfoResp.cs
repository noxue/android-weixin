namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipGetRoomInfoResp")]
    public class VoipGetRoomInfoResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _CallerUserName = "";
        private uint _CreateTime;
        private uint _InviteType = 0;
        private int _MemberCount;
        private readonly List<VoipStatusItem> _MemberStatus = new List<VoipStatusItem>();
        private int _RoomId;
        private long _RoomKey;
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

        [ProtoMember(8, IsRequired=false, Name="CallerUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CallerUserName
        {
            get
            {
                return this._CallerUserName;
            }
            set
            {
                this._CallerUserName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
        public uint CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="InviteType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InviteType
        {
            get
            {
                return this._InviteType;
            }
            set
            {
                this._InviteType = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="MemberCount", DataFormat=DataFormat.TwosComplement)]
        public int MemberCount
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

        [ProtoMember(7, Name="MemberStatus", DataFormat=DataFormat.Default)]
        public List<VoipStatusItem> MemberStatus
        {
            get
            {
                return this._MemberStatus;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
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
    }
}

