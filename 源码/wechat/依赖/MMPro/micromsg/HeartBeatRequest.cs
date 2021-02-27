namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="HeartBeatRequest")]
    public class HeartBeatRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _BlueToothBroadCastContent = null;
        private SKBuiltinBuffer_t _KeyBuf = null;
        private uint _Scene = 0;
        private uint _TimeStamp;
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

        [ProtoMember(3, IsRequired=false, Name="KeyBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t KeyBuf
        {
            get
            {
                return this._KeyBuf;
            }
            set
            {
                this._KeyBuf = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(2, IsRequired=true, Name="TimeStamp", DataFormat=DataFormat.TwosComplement)]
        public uint TimeStamp
        {
            get
            {
                return this._TimeStamp;
            }
            set
            {
                this._TimeStamp = value;
            }
        }
    }
}

