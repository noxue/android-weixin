namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetVoiceTransResRequest")]
    public class GetVoiceTransResRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _VoiceId = "";
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

        [ProtoMember(2, IsRequired=false, Name="VoiceId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VoiceId
        {
            get
            {
                return this._VoiceId;
            }
            set
            {
                this._VoiceId = value;
            }
        }
    }
}

