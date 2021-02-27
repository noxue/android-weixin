namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindHardDeviceRequest")]
    public class BindHardDeviceRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _BindTicket = "";
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

        [ProtoMember(4, IsRequired=false, Name="BindTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BindTicket
        {
            get
            {
                return this._BindTicket;
            }
            set
            {
                this._BindTicket = value;
            }
        }
    }
}

