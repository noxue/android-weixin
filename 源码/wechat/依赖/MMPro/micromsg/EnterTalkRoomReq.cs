namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EnterTalkRoomReq")]
    public class EnterTalkRoomReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Scene = 0;
        private string _ToUsername = "";
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

        [ProtoMember(3, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(2, IsRequired=false, Name="ToUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUsername
        {
            get
            {
                return this._ToUsername;
            }
            set
            {
                this._ToUsername = value;
            }
        }
    }
}

