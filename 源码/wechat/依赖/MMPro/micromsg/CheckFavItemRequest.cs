namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckFavItemRequest")]
    public class CheckFavItemRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _SourceId = "";
        private uint _SourceType;
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

        [ProtoMember(3, IsRequired=false, Name="SourceId", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=true, Name="SourceType", DataFormat=DataFormat.TwosComplement)]
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
    }
}

