namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="OCRTranslationRequest")]
    public class OCRTranslationRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _ClientScanID;
        private string _FromLanguage = "";
        private SKBuiltinBuffer_t _ImageBuffer;
        private uint _ImageType;
        private uint _SessionID = 0;
        private string _ToLanguage = "";
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

        [ProtoMember(5, IsRequired=false, Name="FromLanguage", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromLanguage
        {
            get
            {
                return this._FromLanguage;
            }
            set
            {
                this._FromLanguage = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ImageBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ImageBuffer
        {
            get
            {
                return this._ImageBuffer;
            }
            set
            {
                this._ImageBuffer = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ImageType", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(7, IsRequired=false, Name="SessionID", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SessionID
        {
            get
            {
                return this._SessionID;
            }
            set
            {
                this._SessionID = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ToLanguage", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToLanguage
        {
            get
            {
                return this._ToLanguage;
            }
            set
            {
                this._ToLanguage = value;
            }
        }
    }
}

