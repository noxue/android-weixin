namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLoginURLRequest")]
    public class GetLoginURLRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private byte[] _FromDeviceID = null;
        private string _UUID = "";
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

        [ProtoMember(3, IsRequired=false, Name="FromDeviceID", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] FromDeviceID
        {
            get
            {
                return this._FromDeviceID;
            }
            set
            {
                this._FromDeviceID = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UUID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UUID
        {
            get
            {
                return this._UUID;
            }
            set
            {
                this._UUID = value;
            }
        }
    }
}

