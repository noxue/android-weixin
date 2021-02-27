namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BlueToothBindLoginResponse")]
    public class BlueToothBindLoginResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _BlueToothBroadCastUUID = "";
        private string _DeviceName = "";
        private uint _ExpiredTime;
        private string _UUID = "";
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

        [ProtoMember(5, IsRequired=false, Name="BlueToothBroadCastUUID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BlueToothBroadCastUUID
        {
            get
            {
                return this._BlueToothBroadCastUUID;
            }
            set
            {
                this._BlueToothBroadCastUUID = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="DeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceName
        {
            get
            {
                return this._DeviceName;
            }
            set
            {
                this._DeviceName = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ExpiredTime", DataFormat=DataFormat.TwosComplement)]
        public uint ExpiredTime
        {
            get
            {
                return this._ExpiredTime;
            }
            set
            {
                this._ExpiredTime = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="UUID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UUID
        {
            get
            {
                return this._UUID;
            }
            set
            {
                this._UUID = value;
            }
        }
    }
}

