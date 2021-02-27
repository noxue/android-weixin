namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AdvertiseObject")]
    public class AdvertiseObject : IExtensible
    {
        private SKBuiltinString_t _ADInfo = null;
        private micromsg.SnsADObject _SnsADObject;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="ADInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ADInfo
        {
            get
            {
                return this._ADInfo;
            }
            set
            {
                this._ADInfo = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="SnsADObject", DataFormat=DataFormat.Default)]
        public micromsg.SnsADObject SnsADObject
        {
            get
            {
                return this._SnsADObject;
            }
            set
            {
                this._SnsADObject = value;
            }
        }
    }
}

