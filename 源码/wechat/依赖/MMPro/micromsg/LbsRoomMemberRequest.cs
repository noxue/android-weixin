namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LbsRoomMemberRequest")]
    public class LbsRoomMemberRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _RoomName = "";
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

        [ProtoMember(2, IsRequired=false, Name="RoomName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RoomName
        {
            get
            {
                return this._RoomName;
            }
            set
            {
                this._RoomName = value;
            }
        }
    }
}

