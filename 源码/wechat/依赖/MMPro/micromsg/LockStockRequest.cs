namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LockStockRequest")]
    public class LockStockRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Count = 0;
        private string _Pid = "";
        private string _SkuId = "";
        private string _Url = "";
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

        [ProtoMember(4, IsRequired=false, Name="Count", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Pid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pid
        {
            get
            {
                return this._Pid;
            }
            set
            {
                this._Pid = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="SkuId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SkuId
        {
            get
            {
                return this._SkuId;
            }
            set
            {
                this._SkuId = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

