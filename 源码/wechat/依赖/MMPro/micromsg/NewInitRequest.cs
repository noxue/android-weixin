namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NewInitRequest")]
    public class NewInitRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _CurrentSynckey;
        private string _Language = "";
        private SKBuiltinBuffer_t _MaxSynckey;
        private string _UserName = "";
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

        [ProtoMember(3, IsRequired=true, Name="CurrentSynckey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t CurrentSynckey
        {
            get
            {
                return this._CurrentSynckey;
            }
            set
            {
                this._CurrentSynckey = value;
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

        [ProtoMember(4, IsRequired=true, Name="MaxSynckey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t MaxSynckey
        {
            get
            {
                return this._MaxSynckey;
            }
            set
            {
                this._MaxSynckey = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
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

