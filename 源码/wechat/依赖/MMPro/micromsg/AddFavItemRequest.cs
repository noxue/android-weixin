namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AddFavItemRequest")]
    public class AddFavItemRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ClientId = "";
        private string _Object = "";
        private string _SourceId = "";
        private uint _SourceType;
        private uint _Type;
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

        [ProtoMember(2, IsRequired=false, Name="ClientId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Object", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Object
        {
            get
            {
                return this._Object;
            }
            set
            {
                this._Object = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="SourceId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SourceId
        {
            get
            {
                return this._SourceId;
            }
            set
            {
                this._SourceId = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="SourceType", DataFormat=DataFormat.TwosComplement)]
        public uint SourceType
        {
            get
            {
                return this._SourceType;
            }
            set
            {
                this._SourceType = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

