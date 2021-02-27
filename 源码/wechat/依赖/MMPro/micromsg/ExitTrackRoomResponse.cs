namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExitTrackRoomResponse")]
    public class ExitTrackRoomResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _RetMsg = "";
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

        [ProtoMember(2, IsRequired=false, Name="RetMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RetMsg
        {
            get
            {
                return this._RetMsg;
            }
            set
            {
                this._RetMsg = value;
            }
        }
    }
}

