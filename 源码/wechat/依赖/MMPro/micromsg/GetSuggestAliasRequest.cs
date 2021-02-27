namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetSuggestAliasRequest")]
    public class GetSuggestAliasRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ClientSeqID = "";
        private string _InputAlias = "";
        private string _Language = "";
        private string _NickName = "";
        private SKBuiltinBuffer_t _RandomEncryKey;
        private string _RegBindName = "";
        private uint _RegMode;
        private string _RegTicket = "";
        private string _VerifyContent = "";
        private string _VerifySignature = "";
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

        [ProtoMember(11, IsRequired=false, Name="ClientSeqID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientSeqID
        {
            get
            {
                return this._ClientSeqID;
            }
            set
            {
                this._ClientSeqID = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="InputAlias", DataFormat=DataFormat.Default), DefaultValue("")]
        public string InputAlias
        {
            get
            {
                return this._InputAlias;
            }
            set
            {
                this._InputAlias = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this._Language = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
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

        [ProtoMember(3, IsRequired=false, Name="RegBindName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RegBindName
        {
            get
            {
                return this._RegBindName;
            }
            set
            {
                this._RegBindName = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="RegMode", DataFormat=DataFormat.TwosComplement)]
        public uint RegMode
        {
            get
            {
                return this._RegMode;
            }
            set
            {
                this._RegMode = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="RegTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RegTicket
        {
            get
            {
                return this._RegTicket;
            }
            set
            {
                this._RegTicket = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="VerifyContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyContent
        {
            get
            {
                return this._VerifyContent;
            }
            set
            {
                this._VerifyContent = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="VerifySignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifySignature
        {
            get
            {
                return this._VerifySignature;
            }
            set
            {
                this._VerifySignature = value;
            }
        }
    }
}

