namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="OnlineInfo")]
    public class OnlineInfo : IExtensible
    {
        private SKBuiltinBuffer_t _ClientKey;
        private uint _DeviceHelperType;
        private byte[] _DeviceID = null;
        private uint _DeviceType;
        private uint _OnlineStatus;
        private string _WordingXML = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="ClientKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ClientKey
        {
            get
            {
                return this._ClientKey;
            }
            set
            {
                this._ClientKey = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="DeviceHelperType", DataFormat=DataFormat.TwosComplement)]
        public uint DeviceHelperType
        {
            get
            {
                return this._DeviceHelperType;
            }
            set
            {
                this._DeviceHelperType = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="DeviceID", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] DeviceID
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

        [ProtoMember(1, IsRequired=true, Name="DeviceType", DataFormat=DataFormat.TwosComplement)]
        public uint DeviceType
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

        [ProtoMember(5, IsRequired=true, Name="OnlineStatus", DataFormat=DataFormat.TwosComplement)]
        public uint OnlineStatus
        {
            get
            {
                return this._OnlineStatus;
            }
            set
            {
                this._OnlineStatus = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="WordingXML", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WordingXML
        {
            get
            {
                return this._WordingXML;
            }
            set
            {
                this._WordingXML = value;
            }
        }
    }
}

