namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetIosExtensionKeyRequest")]
    public class GetIosExtensionKeyRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private byte[] _ExtensionDeviceId = null;
        private uint _ExtensionSessionType = 0;
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

        [ProtoMember(3, IsRequired=false, Name="ExtensionDeviceId", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] ExtensionDeviceId
        {
            get
            {
                return this._ExtensionDeviceId;
            }
            set
            {
                this._ExtensionDeviceId = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ExtensionSessionType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ExtensionSessionType
        {
            get
            {
                return this._ExtensionSessionType;
            }
            set
            {
                this._ExtensionSessionType = value;
            }
        }
    }
}

