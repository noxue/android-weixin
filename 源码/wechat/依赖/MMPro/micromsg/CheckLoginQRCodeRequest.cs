namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckLoginQRCodeRequest")]
    public class CheckLoginQRCodeRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _OPCode = 0;
        private SKBuiltinBuffer_t _RandomEncryKey;
        private uint _TimeStamp;
        private string _UUID = "";
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

        [ProtoMember(5, IsRequired=false, Name="OPCode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(2, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t RandomEncryKey
        {
            get
            {
                return this._RandomEncryKey;
            }
            set
            {
                this._RandomEncryKey = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="TimeStamp", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=false, Name="UUID", DataFormat=DataFormat.Default), DefaultValue("")]
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

