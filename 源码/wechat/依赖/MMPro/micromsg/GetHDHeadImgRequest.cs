namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetHDHeadImgRequest")]
    public class GetHDHeadImgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _HeadImgType;
        private string _ImgFormat = "";
        private uint _ImgHeight;
        private uint _ImgWidth;
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

        [ProtoMember(8, IsRequired=true, Name="HeadImgType", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=false, Name="ImgFormat", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgFormat
        {
            get
            {
                return this._ImgFormat;
            }
            set
            {
                this._ImgFormat = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ImgHeight", DataFormat=DataFormat.TwosComplement)]
        public uint ImgHeight
        {
            get
            {
                return this._ImgHeight;
            }
            set
            {
                this._ImgHeight = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ImgWidth", DataFormat=DataFormat.TwosComplement)]
        public uint ImgWidth
        {
            get
            {
                return this._ImgWidth;
            }
            set
            {
                this._ImgWidth = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

