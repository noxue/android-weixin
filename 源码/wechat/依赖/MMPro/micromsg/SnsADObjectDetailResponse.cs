namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsADObjectDetailResponse")]
    public class SnsADObjectDetailResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SnsADObject _Object;
        private SKBuiltinBuffer_t _Session = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Object", DataFormat=DataFormat.Default)]
        public SnsADObject Object
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

        [ProtoMember(3, IsRequired=false, Name="Session", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

