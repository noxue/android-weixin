namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetVoiceprintTicketRsaRequest")]
    public class GetVoiceprintTicketRsaRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _GetScence;
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _UserName = "";
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

        [ProtoMember(3, IsRequired=true, Name="GetScence", DataFormat=DataFormat.TwosComplement)]
        public uint GetScence
        {
            get
            {
                return this._GetScence;
            }
            set
            {
                this._GetScence = value;
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

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

