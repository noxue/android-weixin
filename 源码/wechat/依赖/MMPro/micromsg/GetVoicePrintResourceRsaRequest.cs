namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetVoicePrintResourceRsaRequest")]
    public class GetVoicePrintResourceRsaRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _RandomEncryKey;
        private uint _ResScence;
        private string _VerifyTicket = "";
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

        [ProtoMember(4, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(2, IsRequired=true, Name="ResScence", DataFormat=DataFormat.TwosComplement)]
        public uint ResScence
        {
            get
            {
                return this._ResScence;
            }
            set
            {
                this._ResScence = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="VerifyTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyTicket
        {
            get
            {
                return this._VerifyTicket;
            }
            set
            {
                this._VerifyTicket = value;
            }
        }
    }
}

