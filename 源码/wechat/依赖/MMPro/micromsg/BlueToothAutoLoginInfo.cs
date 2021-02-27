namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BlueToothAutoLoginInfo")]
    public class BlueToothAutoLoginInfo : IExtensible
    {
        private uint _ClientVersion;
        private byte[] _DeviceID = null;
        private string _DeviceType = "";
        private uint _Uin;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="ClientVersion", DataFormat=DataFormat.TwosComplement)]
        public uint ClientVersion
        {
            get
            {
                return this._ClientVersion;
            }
            set
            {
                this._ClientVersion = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="DeviceID", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(2, IsRequired=false, Name="DeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(1, IsRequired=true, Name="Uin", DataFormat=DataFormat.TwosComplement)]
        public uint Uin
        {
            get
            {
                return this._Uin;
            }
            set
            {
                this._Uin = value;
            }
        }
    }
}

