namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetChatroomMemberDetailRequest")]
    public class GetChatroomMemberDetailRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ChatroomUserName = "";
        private uint _ClientVersion;
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

        [ProtoMember(3, IsRequired=true, Name="ClientVersion", DataFormat=DataFormat.TwosComplement)]
        public uint ClientVersion
        {
            get
            {
                return this._ClientVersion;
            }
            set
            {
                this._ClientVersion = value;
            }
        }
    }
}

