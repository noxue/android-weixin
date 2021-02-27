namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="IBeaconBoardcastResponse")]
    public class IBeaconBoardcastResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private IBeaconNotification _Notification = null;
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

        [ProtoMember(2, IsRequired=false, Name="Notification", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public IBeaconNotification Notification
        {
            get
            {
                return this._Notification;
            }
            set
            {
                this._Notification = value;
            }
        }
    }
}

