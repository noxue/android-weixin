namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckCanSubscribeBizRequest")]
    public class CheckCanSubscribeBizRequest : IExtensible
    {
        private readonly List<SKBuiltinString_t> _AndroidPackNameList = new List<SKBuiltinString_t>();
        private string _AppID = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _ExtInfo = "";
        private string _FromURL = "";
        private string _IosBunddleID = "";
        private uint _PackNum;
        private uint _Scene = 0;
        private uint _Source;
        private string _ToUserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, Name="AndroidPackNameList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> AndroidPackNameList
        {
            get
            {
                return this._AndroidPackNameList;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppID
        {
            get
            {
                return this._AppID;
            }
            set
            {
                this._AppID = value;
            }
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

        [ProtoMember(4, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="FromURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromURL
        {
            get
            {
                return this._FromURL;
            }
            set
            {
                this._FromURL = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="IosBunddleID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IosBunddleID
        {
            get
            {
                return this._IosBunddleID;
            }
            set
            {
                this._IosBunddleID = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="PackNum", DataFormat=DataFormat.TwosComplement)]
        public uint PackNum
        {
            get
            {
                return this._PackNum;
            }
            set
            {
                this._PackNum = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Source", DataFormat=DataFormat.TwosComplement)]
        public uint Source
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

        [ProtoMember(3, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUserName
        {
            get
            {
                return this._ToUserName;
            }
            set
            {
                this._ToUserName = value;
            }
        }
    }
}

