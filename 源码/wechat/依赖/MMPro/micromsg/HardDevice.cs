namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="HardDevice")]
    public class HardDevice : IExtensible
    {
        private string _DeviceID = "";
        private string _DeviceType = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="DeviceID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceID
        {
            get
            {
                return this._DeviceID;
            }
            set
            {
                this._DeviceID = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="DeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceType
        {
            get
            {
                return this._DeviceType;
            }
            set
            {
                this._DeviceType = value;
            }
        }
    }
}

