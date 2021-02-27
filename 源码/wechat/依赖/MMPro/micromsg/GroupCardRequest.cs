namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GroupCardRequest")]
    public class GroupCardRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _GroupNickName = "";
        private string _GroupUserName = "";
        private uint _MemberCount;
        private readonly List<RoomInfo> _MemberList = new List<RoomInfo>();
        private uint _OpCode;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="GroupNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GroupNickName
        {
            get
            {
                return this._GroupNickName;
            }
            set
            {
                this._GroupNickName = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="GroupUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GroupUserName
        {
            get
            {
                return this._GroupUserName;
            }
            set
            {
                this._GroupUserName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="MemberCount", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, Name="MemberList", DataFormat=DataFormat.Default)]
        public List<RoomInfo> MemberList
        {
            get
            {
                return this._MemberList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }
    }
}

