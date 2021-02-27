namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AutoAuthRsaReqData")]
    public class AutoAuthRsaReqData : IExtensible
    {
        private SKBuiltinBuffer_t _AesEncryptKey;
        private ECDHKey _CliPubECDHKey = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AesEncryptKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t AesEncryptKey
        {
            get
            {
                return this._AesEncryptKey;
            }
            set
            {
                this._AesEncryptKey = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="CliPubECDHKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ECDHKey CliPubECDHKey
        {
            get
            {
                return this._CliPubECDHKey;
            }
            set
            {
                this._CliPubECDHKey = value;
            }
        }
    }
}

