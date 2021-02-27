namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetSuggestionAppListRequest")]
    public class GetSuggestionAppListRequest : IExtensible
    {
        private uint _AppType;
        private micromsg.BaseRequest _BaseRequest;
        private uint _InstalledAppCount = 0;
        private readonly List<SKBuiltinString_t> _InstalledAppList = new List<SKBuiltinString_t>();
        private string _Lang = "";
        private uint _Limit;
        private uint _OffSet;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AppType", DataFormat=DataFormat.TwosComplement)]
        public uint AppType
        {
            get
            {
                return this._AppType;
            }
            set
            {
                this._AppType = value;
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

        [ProtoMember(6, IsRequired=false, Name="InstalledAppCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InstalledAppCount
        {
            get
            {
                return this._InstalledAppCount;
            }
            set
            {
                this._InstalledAppCount = value;
            }
        }

        [ProtoMember(7, Name="InstalledAppList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> InstalledAppList
        {
            get
            {
                return this._InstalledAppList;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Lang", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Lang
        {
            get
            {
                return this._Lang;
            }
            set
            {
                this._Lang = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Limit", DataFormat=DataFormat.TwosComplement)]
        public uint Limit
        {
            get
            {
                return this._Limit;
            }
            set
            {
                this._Limit = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="OffSet", DataFormat=DataFormat.TwosComplement)]
        public uint OffSet
        {
            get
            {
                return this._OffSet;
            }
            set
            {
                this._OffSet = value;
            }
        }
    }
}

