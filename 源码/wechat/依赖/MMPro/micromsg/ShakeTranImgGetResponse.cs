namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeTranImgGetResponse")]
    public class ShakeTranImgGetResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _Buffer = null;
        private uint _Count;
        private readonly List<ShakeTranImgGetItem> _ImgUrlList = new List<ShakeTranImgGetItem>();
        private string _PageUrl = "";
        private string _Title = "";
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

        [ProtoMember(5, IsRequired=false, Name="Buffer", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(4, Name="ImgUrlList", DataFormat=DataFormat.Default)]
        public List<ShakeTranImgGetItem> ImgUrlList
        {
            get
            {
                return this._ImgUrlList;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="PageUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PageUrl
        {
            get
            {
                return this._PageUrl;
            }
            set
            {
                this._PageUrl = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Title", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }
    }
}

