namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="HeartBeatResponse")]
    public class HeartBeatResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _BlueToothBroadCastContent = null;
        private uint _NextTime;
        private uint _Selector = 0;
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

        [ProtoMember(4, IsRequired=false, Name="BlueToothBroadCastContent", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(2, IsRequired=true, Name="NextTime", DataFormat=DataFormat.TwosComplement)]
        public uint NextTime
        {
            get
            {
                return this._NextTime;
            }
            set
            {
                this._NextTime = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Selector", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Selector
        {
            get
            {
                return this._Selector;
            }
            set
            {
                this._Selector = value;
            }
        }
    }
}

