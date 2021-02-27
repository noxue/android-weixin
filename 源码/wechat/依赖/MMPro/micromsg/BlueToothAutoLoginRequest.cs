namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BlueToothAutoLoginRequest")]
    public class BlueToothAutoLoginRequest : IExtensible
    {
        private string _AutoAuthTicket = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _BlueToothBroadCastUUID = "";
        private SKBuiltinBuffer_t _LoginInfoData;
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _UserName = "";
        private string _UUID = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="AutoAuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AutoAuthTicket
        {
            get
            {
                return this._AutoAuthTicket;
            }
            set
            {
                this._AutoAuthTicket = value;
            }
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

        [ProtoMember(7, IsRequired=false, Name="BlueToothBroadCastUUID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=true, Name="LoginInfoData", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t LoginInfoData
        {
            get
            {
                return this._LoginInfoData;
            }
            set
            {
                this._LoginInfoData = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

