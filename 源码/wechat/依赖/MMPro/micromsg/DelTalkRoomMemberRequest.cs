namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DelTalkRoomMemberRequest")]
    public class DelTalkRoomMemberRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _MemberCount;
        private readonly List<DelMemberReq> _MemberList = new List<DelMemberReq>();
        private uint _Scene = 0;
        private string _TalkRoomName = "";
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

        [ProtoMember(2, IsRequired=true, Name="MemberCount", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, Name="MemberList", DataFormat=DataFormat.Default)]
        public List<DelMemberReq> MemberList
        {
            get
            {
                return this._MemberList;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(4, IsRequired=false, Name="TalkRoomName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TalkRoomName
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
    }
}

