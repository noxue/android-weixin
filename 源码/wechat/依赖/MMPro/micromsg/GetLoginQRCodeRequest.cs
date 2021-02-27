namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLoginQRCodeRequest")]
    public class GetLoginQRCodeRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _DeviceName = "";
        private uint _ExtDevLoginType = 0;
        private uint _OPCode = 0;
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _UserName = "";
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

        [ProtoMember(4, IsRequired=false, Name="DeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=false, Name="ExtDevLoginType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ExtDevLoginType
        {
            get
            {
                return this._ExtDevLoginType;
            }
            set
            {
                this._ExtDevLoginType = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="OPCode", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(5, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

