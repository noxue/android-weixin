namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LockWebDeviceRequest")]
    public class LockWebDeviceRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _BlueToothBroadCastContent;
        private string _BlueToothBroadCastUUID = "";
        private uint _OPCode;
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

        [ProtoMember(4, IsRequired=true, Name="BlueToothBroadCastContent", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t BlueToothBroadCastContent
        {
            get
            {
                return this._BlueToothBroadCastContent;
            }
            set
            {
                this._BlueToothBroadCastContent = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="BlueToothBroadCastUUID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=true, Name="OPCode", DataFormat=DataFormat.TwosComplement)]
        public uint OPCode
        {
            get
            {
                return this._OPCode;
            }
            set
            {
                this._OPCode = value;
            }
        }
    }
}

