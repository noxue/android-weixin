namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindGoogleContactRequest")]
    public class BindGoogleContactRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Force;
        private string _GoogleContactName = "";
        private uint _Opcode;
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

        [ProtoMember(4, IsRequired=true, Name="Force", DataFormat=DataFormat.TwosComplement)]
        public uint Force
        {
            get
            {
                return this._Force;
            }
            set
            {
                this._Force = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="GoogleContactName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GoogleContactName
        {
            get
            {
                return this._GoogleContactName;
            }
            set
            {
                this._GoogleContactName = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Opcode", DataFormat=DataFormat.TwosComplement)]
        public uint Opcode
        {
            get
            {
                return this._Opcode;
            }
            set
            {
                this._Opcode = value;
            }
        }
    }
}

