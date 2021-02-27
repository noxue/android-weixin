namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsTagOptionRequest")]
    public class SnsTagOptionRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _OpCode;
        private ulong _TagId;
        private string _TagName = "";
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

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TagId", DataFormat=DataFormat.TwosComplement)]
        public ulong TagId
        {
            get
            {
                return this._TagId;
            }
            set
            {
                this._TagId = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="TagName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagName
        {
            get
            {
                return this._TagName;
            }
            set
            {
                this._TagName = value;
            }
        }
    }
}

