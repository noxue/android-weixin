namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetChatRoomAnnouncementResponse")]
    public class GetChatRoomAnnouncementResponse : IExtensible
    {
        private string _Announcement = "";
        private micromsg.BaseResponse _BaseResponse;
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
    }
}

