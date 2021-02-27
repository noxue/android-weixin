namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindSafeMobileRequest")]
    public class BindSafeMobileRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Mobile = "";
        private int _Opcode;
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

        [ProtoMember(2, IsRequired=false, Name="Mobile", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Mobile
        {
            get
            {
                return this._Mobile;
            }
            set
            {
                this._Mobile = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Opcode", DataFormat=DataFormat.TwosComplement)]
        public int Opcode
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

