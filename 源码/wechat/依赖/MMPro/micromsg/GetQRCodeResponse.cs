namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetQRCodeResponse")]
    public class GetQRCodeResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _FooterWording = "";
        private SKBuiltinBuffer_t _QRCode;
        private uint _Style;
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

        [ProtoMember(6, IsRequired=false, Name="FooterWording", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FooterWording
        {
            get
            {
                return this._FooterWording;
            }
            set
            {
                this._FooterWording = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="QRCode", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t QRCode
        {
            get
            {
                return this._QRCode;
            }
            set
            {
                this._QRCode = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Style", DataFormat=DataFormat.TwosComplement)]
        public uint Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                this._Style = value;
            }
        }
    }
}

