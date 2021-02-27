namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ModUserImg")]
    public class ModUserImg : IExtensible
    {
        private string _BigHeadImgUrl = "";
        private byte[] _ImgBuf = null;
        private uint _ImgLen;
        private string _ImgMd5 = "";
        private uint _ImgType;
        private string _SmallHeadImgUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(3, IsRequired=false, Name="ImgBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] ImgBuf
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

        [ProtoMember(2, IsRequired=true, Name="ImgLen", DataFormat=DataFormat.TwosComplement)]
        public uint ImgLen
        {
            get
            {
                return this._ImgLen;
            }
            set
            {
                this._ImgLen = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ImgMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgMd5
        {
            get
            {
                return this._ImgMd5;
            }
            set
            {
                this._ImgMd5 = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ImgType", DataFormat=DataFormat.TwosComplement)]
        public uint ImgType
        {
            get
            {
                return this._ImgType;
            }
            set
            {
                this._ImgType = value;
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
    }
}

