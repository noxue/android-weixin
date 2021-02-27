namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FaceBookAuthRequest")]
    public class FaceBookAuthRequest : IExtensible
    {
        private string _AccessToken = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _OpType;
        private string _RandomEncryKey = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AccessToken", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AccessToken
        {
            get
            {
                return this._AccessToken;
            }
            set
            {
                this._AccessToken = value;
            }
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

        [ProtoMember(2, IsRequired=true, Name="OpType", DataFormat=DataFormat.TwosComplement)]
        public uint OpType
        {
            get
            {
                return this._OpType;
            }
            set
            {
                this._OpType = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="RandomEncryKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RandomEncryKey
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
    }
}

