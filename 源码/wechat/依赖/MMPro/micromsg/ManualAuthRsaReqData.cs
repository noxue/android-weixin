namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ManualAuthRsaReqData")]
    public class ManualAuthRsaReqData : IExtensible
    {
        private ECDHKey _CliPubECDHKey;
        private string _Pwd = "";
        private string _Pwd2 = "";
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="CliPubECDHKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(4, IsRequired=false, Name="Pwd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd
        {
            get
            {
                return this._Pwd;
            }
            set
            {
                this._Pwd = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Pwd2", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd2
        {
            get
            {
                return this._Pwd2;
            }
            set
            {
                this._Pwd2 = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(3, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

