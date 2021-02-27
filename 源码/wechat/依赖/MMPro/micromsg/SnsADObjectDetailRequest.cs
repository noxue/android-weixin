namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsADObjectDetailRequest")]
    public class SnsADObjectDetailRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private ulong _Id;
        private uint _Scene = 0;
        private SKBuiltinBuffer_t _Session = null;
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

        [ProtoMember(2, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public ulong Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(4, IsRequired=false, Name="Session", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t Session
        {
            get
            {
                return this._Session;
            }
            set
            {
                this._Session = value;
            }
        }
    }
}

