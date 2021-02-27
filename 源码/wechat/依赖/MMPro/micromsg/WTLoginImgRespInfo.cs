namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="WTLoginImgRespInfo")]
    public class WTLoginImgRespInfo : IExtensible
    {
        private SKBuiltinBuffer_t _ImgBuf;
        private string _ImgEncryptKey = "";
        private string _ImgSid = "";
        private SKBuiltinBuffer_t _KSid;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="ImgBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ImgBuf
        {
            get
            {
                return this._ImgBuf;
            }
            set
            {
                this._ImgBuf = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgEncryptKey
        {
            get
            {
                return this._ImgEncryptKey;
            }
            set
            {
                this._ImgEncryptKey = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ImgSid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgSid
        {
            get
            {
                return this._ImgSid;
            }
            set
            {
                this._ImgSid = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="KSid", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KSid
        {
            get
            {
                return this._KSid;
            }
            set
            {
                this._KSid = value;
            }
        }
    }
}

