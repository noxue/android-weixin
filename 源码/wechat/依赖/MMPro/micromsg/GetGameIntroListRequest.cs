namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetGameIntroListRequest")]
    public class GetGameIntroListRequest : IExtensible
    {
        private readonly List<SKBuiltinString_t> _AppIdList = new List<SKBuiltinString_t>();
        private micromsg.BaseRequest _BaseRequest;
        private uint _Count;
        private string _DevicePlatform = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, Name="AppIdList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> AppIdList
        {
            get
            {
                return this._AppIdList;
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

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(4, IsRequired=false, Name="DevicePlatform", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DevicePlatform
        {
            get
            {
                return this._DevicePlatform;
            }
            set
            {
                this._DevicePlatform = value;
            }
        }
    }
}

