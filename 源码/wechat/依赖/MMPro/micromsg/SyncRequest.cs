namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SyncRequest")]
    public class SyncRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _KeyBuf = "";
        private CmdList _Oplog;
        private int _Option;
        private int _Scene;
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

        [ProtoMember(7, IsRequired=false, Name="KeyBuf", DataFormat=DataFormat.Default), DefaultValue("")]
        public string KeyBuf
        {
            get
            {
                return this._KeyBuf;
            }
            set
            {
                this._KeyBuf = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Oplog", DataFormat=DataFormat.Default)]
        public CmdList Oplog
        {
            get
            {
                return this._Oplog;
            }
            set
            {
                this._Oplog = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Option", DataFormat=DataFormat.TwosComplement)]
        public int Option
        {
            get
            {
                return this._Option;
            }
            set
            {
                this._Option = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
        public int Scene
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

