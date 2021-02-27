namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetStreamUrlRequest")]
    public class GetStreamUrlRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _StreamId = "";
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

        [ProtoMember(2, IsRequired=false, Name="StreamId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamId
        {
            get
            {
                return this._StreamId;
            }
            set
            {
                this._StreamId = value;
            }
        }
    }
}

