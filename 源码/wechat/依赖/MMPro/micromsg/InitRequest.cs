namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="InitRequest")]
    public class InitRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinString_t _Buffer;
        private string _Language = "";
        private uint _SyncKey;
        private SKBuiltinString_t _UserName;
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

        [ProtoMember(4, IsRequired=true, Name="Buffer", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t Buffer
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

        [ProtoMember(5, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this._Language = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="SyncKey", DataFormat=DataFormat.TwosComplement)]
        public uint SyncKey
        {
            get
            {
                return this._SyncKey;
            }
            set
            {
                this._SyncKey = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t UserName
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

