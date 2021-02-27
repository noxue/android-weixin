namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="WTLoginImgReqInfo")]
    public class WTLoginImgReqInfo : IExtensible
    {
        private string _ImgCode = "";
        private string _ImgEncryptKey = "";
        private string _ImgSid = "";
        private SKBuiltinBuffer_t _KSid;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="ImgCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgCode
        {
            get
            {
                return this._ImgCode;
            }
            set
            {
                this._ImgCode = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(1, IsRequired=false, Name="ImgSid", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=true, Name="KSid", DataFormat=DataFormat.Default)]
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

