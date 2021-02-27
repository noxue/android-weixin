namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SubmitMallFreeOrderRequest")]
    public class SubmitMallFreeOrderRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private micromsg.Snapshot _Snapshot = null;
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

        [ProtoMember(2, IsRequired=false, Name="Snapshot", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.Snapshot Snapshot
        {
            get
            {
                return this._Snapshot;
            }
            set
            {
                this._Snapshot = value;
            }
        }
    }
}

