namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PushLoginURLRequest")]
    public class PushLoginURLRequest : IExtensible
    {
        private SKBuiltinBuffer_t _AutoAuthKey = null;
        private string _AutoAuthTicket = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _ClientID = "";
        private string _DeviceName = "";
        private uint _OPCode;
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(8, IsRequired=false, Name="AutoAuthKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t AutoAuthKey
        {
            get
            {
                return this._AutoAuthKey;
            }
            set
            {
                this._AutoAuthKey = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="AutoAuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="ClientID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientID
        {
            get
            {
                return this._ClientID;
            }
            set
            {
                this._ClientID = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="DeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=true, Name="OPCode", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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
    }
}

