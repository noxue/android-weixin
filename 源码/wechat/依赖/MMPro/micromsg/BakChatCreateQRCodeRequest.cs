namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BakChatCreateQRCodeRequest")]
    public class BakChatCreateQRCodeRequest : IExtensible
    {
        private uint _AddrCount;
        private readonly List<ConnectInfoAddr> _AddrList = new List<ConnectInfoAddr>();
        private micromsg.BaseRequest _BaseRequest;
        private ulong _DataSize = 0L;
        private int _Op = 0;
        private string _PCAcctName = "";
        private string _PCName = "";
        private uint _Scene;
        private string _WifiName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AddrCount", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, Name="AddrList", DataFormat=DataFormat.Default)]
        public List<ConnectInfoAddr> AddrList
        {
            get
            {
                return this._AddrList;
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

        [ProtoMember(7, IsRequired=false, Name="DataSize", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

        [ProtoMember(9, IsRequired=false, Name="Op", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Op
        {
            get
            {
                return this._Op;
            }
            set
            {
                this._Op = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PCAcctName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="PCName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(8, IsRequired=false, Name="WifiName", DataFormat=DataFormat.Default), DefaultValue("")]
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

