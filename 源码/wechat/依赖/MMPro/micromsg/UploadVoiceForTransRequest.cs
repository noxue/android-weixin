namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadVoiceForTransRequest")]
    public class UploadVoiceForTransRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _Data;
        private UploadVoiceCtx _UploadCtx;
        private micromsg.VoiceAttr _VoiceAttr;
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

        [ProtoMember(5, IsRequired=true, Name="Data", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="UploadCtx", DataFormat=DataFormat.Default)]
        public UploadVoiceCtx UploadCtx
        {
            get
            {
                return this._UploadCtx;
            }
            set
            {
                this._UploadCtx = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="VoiceAttr", DataFormat=DataFormat.Default)]
        public micromsg.VoiceAttr VoiceAttr
        {
            get
            {
                return this._VoiceAttr;
            }
            set
            {
                this._VoiceAttr = value;
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

