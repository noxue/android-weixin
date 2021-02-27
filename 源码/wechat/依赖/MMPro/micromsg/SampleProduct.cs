namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SampleProduct")]
    public class SampleProduct : IExtensible
    {
        private uint _Count = 0;
        private string _Pid = "";
        private string _SafeUrl = "";
        private string _SkuId = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Count", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(1, IsRequired=false, Name="Pid", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="SafeUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SafeUrl
        {
            get
            {
                return this._SafeUrl;
            }
            set
            {
                this._SafeUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="SkuId", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

