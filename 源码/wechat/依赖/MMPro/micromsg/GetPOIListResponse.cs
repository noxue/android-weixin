namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetPOIListResponse")]
    public class GetPOIListResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _Buff;
        private uint _IsEnd;
        private uint _ListCount;
        private string _LogoUrl = "";
        private uint _OpCode;
        private string _OpenId = "";
        private readonly List<POIItem> _POIList = new List<POIItem>();
        private string _Text = "";
        private string _Url = "";
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

        [ProtoMember(6, IsRequired=true, Name="Buff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buff
        {
            get
            {
                return this._Buff;
            }
            set
            {
                this._Buff = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="IsEnd", DataFormat=DataFormat.TwosComplement)]
        public uint IsEnd
        {
            get
            {
                return this._IsEnd;
            }
            set
            {
                this._IsEnd = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ListCount", DataFormat=DataFormat.TwosComplement)]
        public uint ListCount
        {
            get
            {
                return this._ListCount;
            }
            set
            {
                this._ListCount = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="LogoUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LogoUrl
        {
            get
            {
                return this._LogoUrl;
            }
            set
            {
                this._LogoUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="OpenId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OpenId
        {
            get
            {
                return this._OpenId;
            }
            set
            {
                this._OpenId = value;
            }
        }

        [ProtoMember(3, Name="POIList", DataFormat=DataFormat.Default)]
        public List<POIItem> POIList
        {
            get
            {
                return this._POIList;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Text", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

