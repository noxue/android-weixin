namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadHDHeadImgRequest")]
    public class UploadHDHeadImgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _Data;
        private uint _HeadImgType;
        private string _ImgHash = "";
        private uint _StartPos;
        private uint _TotalLen;
        private string _UserName = "";
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

        [ProtoMember(4, IsRequired=true, Name="HeadImgType", DataFormat=DataFormat.TwosComplement)]
        public uint HeadImgType
        {
            get
            {
                return this._HeadImgType;
            }
            set
            {
                this._HeadImgType = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ImgHash", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgHash
        {
            get
            {
                return this._ImgHash;
            }
            set
            {
                this._ImgHash = value;
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

        [ProtoMember(7, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

