namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="OCRTranslationResponse")]
    public class OCRTranslationResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ClientScanID;
        private uint _ImageType;
        private string _Source = "";
        private string _Translation = "";
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

        [ProtoMember(2, IsRequired=true, Name="ClientScanID", DataFormat=DataFormat.TwosComplement)]
        public uint ClientScanID
        {
            get
            {
                return this._ClientScanID;
            }
            set
            {
                this._ClientScanID = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ImageType", DataFormat=DataFormat.TwosComplement)]
        public uint ImageType
        {
            get
            {
                return this._ImageType;
            }
            set
            {
                this._ImageType = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Source", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Translation", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Translation
        {
            get
            {
                return this._Translation;
            }
            set
            {
                this._Translation = value;
            }
        }
    }
}

