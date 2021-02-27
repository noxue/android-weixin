namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BlackBerryRegRequest")]
    public class BlackBerryRegRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Pin = "";
        private uint _Port;
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

        [ProtoMember(2, IsRequired=false, Name="Pin", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pin
        {
            get
            {
                return this._Pin;
            }
            set
            {
                this._Pin = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Port", DataFormat=DataFormat.TwosComplement)]
        public uint Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                this._Port = value;
            }
        }
    }
}

