namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SetChatRoomAnnouncementRequest")]
    public class SetChatRoomAnnouncementRequest : IExtensible
    {
        private string _Announcement = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _ChatRoomName = "";
        private uint _SetAnnouncementFlag = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Announcement", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Announcement
        {
            get
            {
                return this._Announcement;
            }
            set
            {
                this._Announcement = value;
            }
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

        [ProtoMember(2, IsRequired=false, Name="ChatRoomName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomName
        {
            get
            {
                return this._ChatRoomName;
            }
            set
            {
                this._ChatRoomName = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="SetAnnouncementFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SetAnnouncementFlag
        {
            get
            {
                return this._SetAnnouncementFlag;
            }
            set
            {
                this._SetAnnouncementFlag = value;
            }
        }
    }
}

