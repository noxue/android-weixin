namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SearchNBSRequest")]
    public class SearchNBSRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _BizMarkets = "";
        private string _KeyWord = "";
        private SKBuiltinBuffer_t _PageBuff;
        private string _Tags = "";
        private PositionInfo _UserPos;
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

        [ProtoMember(4, IsRequired=false, Name="BizMarkets", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BizMarkets
        {
            get
            {
                return this._BizMarkets;
            }
            set
            {
                this._BizMarkets = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="KeyWord", DataFormat=DataFormat.Default), DefaultValue("")]
        public string KeyWord
        {
            get
            {
                return this._KeyWord;
            }
            set
            {
                this._KeyWord = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="PageBuff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t PageBuff
        {
            get
            {
                return this._PageBuff;
            }
            set
            {
                this._PageBuff = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Tags", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Tags
        {
            get
            {
                return this._Tags;
            }
            set
            {
                this._Tags = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="UserPos", DataFormat=DataFormat.Default)]
        public PositionInfo UserPos
        {
            get
            {
                return this._UserPos;
            }
            set
            {
                this._UserPos = value;
            }
        }
    }
}

