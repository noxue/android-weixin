namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetChatRoomInfoDetailResponse")]
    public class GetChatRoomInfoDetailResponse : IExtensible
    {
        private string _Announcement = "";
        private string _AnnouncementEditor = "";
        private uint _AnnouncementPublishTime = 0;
        private micromsg.BaseResponse _BaseResponse;
        private uint _ChatRoomInfoVersion = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Announcement", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="AnnouncementEditor", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AnnouncementEditor
        {
            get
            {
                return this._AnnouncementEditor;
            }
            set
            {
                this._AnnouncementEditor = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="AnnouncementPublishTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AnnouncementPublishTime
        {
            get
            {
                return this._AnnouncementPublishTime;
            }
            set
            {
                this._AnnouncementPublishTime = value;
            }
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

        [ProtoMember(3, IsRequired=false, Name="ChatRoomInfoVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ChatRoomInfoVersion
        {
            get
            {
                return this._ChatRoomInfoVersion;
            }
            set
            {
                this._ChatRoomInfoVersion = value;
            }
        }
    }
}

