namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetProductDetailRequest")]
    public class GetProductDetailRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Pid = "";
        private string _ScanCode = "";
        private uint _Version = 0;
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

        [ProtoMember(4, IsRequired=false, Name="ScanCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ScanCode
        {
            get
            {
                return this._ScanCode;
            }
            set
            {
                this._ScanCode = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Version", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }
    }
}

