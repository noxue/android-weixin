namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetConnectInfoResponse")]
    public class GetConnectInfoResponse : IExtensible
    {
        private uint _AddrCount;
        private readonly List<ConnectInfoAddr> _AddrList = new List<ConnectInfoAddr>();
        private micromsg.BaseResponse _BaseResponse;
        private ulong _DataSize = 0L;
        private uint _EncryFlag = 0;
        private string _Hello = "";
        private string _ID = "";
        private SKBuiltinBuffer_t _Key;
        private string _OK = "";
        private string _PCAcctName = "";
        private string _PCName = "";
        private string _Resource = "";
        private uint _Scene = 0;
        private uint _Type;
        private string _WifiName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=true, Name="AddrCount", DataFormat=DataFormat.TwosComplement)]
        public uint AddrCount
        {
            get
            {
                return this._AddrCount;
            }
            set
            {
                this._AddrCount = value;
            }
        }

        [ProtoMember(8, Name="AddrList", DataFormat=DataFormat.Default)]
        public List<ConnectInfoAddr> AddrList
        {
            get
            {
                return this._AddrList;
            }
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

        [ProtoMember(14, IsRequired=false, Name="DataSize", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong DataSize
        {
            get
            {
                return this._DataSize;
            }
            set
            {
                this._DataSize = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="EncryFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint EncryFlag
        {
            get
            {
                return this._EncryFlag;
            }
            set
            {
                this._EncryFlag = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Hello", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Hello
        {
            get
            {
                return this._Hello;
            }
            set
            {
                this._Hello = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Key", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="OK", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OK
        {
            get
            {
                return this._OK;
            }
            set
            {
                this._OK = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="PCAcctName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PCAcctName
        {
            get
            {
                return this._PCAcctName;
            }
            set
            {
                this._PCAcctName = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="PCName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PCName
        {
            get
            {
                return this._PCName;
            }
            set
            {
                this._PCName = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Resource", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Resource
        {
            get
            {
                return this._Resource;
            }
            set
            {
                this._Resource = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(6, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="WifiName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string WifiName
        {
            get
            {
                return this._WifiName;
            }
            set
            {
                this._WifiName = value;
            }
        }
    }
}

