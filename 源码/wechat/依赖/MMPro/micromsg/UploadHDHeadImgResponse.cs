namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadHDHeadImgResponse")]
    public class UploadHDHeadImgResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _BigHeadImgUrl = "";
        private string _FinalImgMd5sum = "";
        private string _SmallHeadImgUrl = "";
        private uint _StartPos;
        private uint _TotalLen;
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

        [ProtoMember(5, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BigHeadImgUrl
        {
            get
            {
                return this._BigHeadImgUrl;
            }
            set
            {
                this._BigHeadImgUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="FinalImgMd5sum", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FinalImgMd5sum
        {
            get
            {
                return this._FinalImgMd5sum;
            }
            set
            {
                this._FinalImgMd5sum = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallHeadImgUrl
        {
            get
            {
                return this._SmallHeadImgUrl;
            }
            set
            {
                this._SmallHeadImgUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public uint StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }
    }
}

