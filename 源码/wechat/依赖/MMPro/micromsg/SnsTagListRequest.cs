namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsTagListRequest")]
    public class SnsTagListRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _OpCode;
        private string _TagListMd5 = "";
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

        [ProtoMember(3, IsRequired=false, Name="TagListMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagListMd5
        {
            get
            {
                return this._TagListMd5;
            }
            set
            {
                this._TagListMd5 = value;
            }
        }
    }
}

