namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetChatroomMemberDetailResponse")]
    public class GetChatroomMemberDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _ChatroomUserName = "";
        private ChatRoomMemberData _NewChatroomData;
        private uint _ServerVersion;
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

        [ProtoMember(2, IsRequired=false, Name="ChatroomUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatroomUserName
        {
            get
            {
                return this._ChatroomUserName;
            }
            set
            {
                this._ChatroomUserName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="NewChatroomData", DataFormat=DataFormat.Default)]
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

        [ProtoMember(3, IsRequired=true, Name="ServerVersion", DataFormat=DataFormat.TwosComplement)]
        public uint ServerVersion
        {
            get
            {
                return this._ServerVersion;
            }
            set
            {
                this._ServerVersion = value;
            }
        }
    }
}

