namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SearchHardDeviceResponse")]
    public class SearchHardDeviceResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _BindTicket = "";
        private ModContact _Contact;
        private micromsg.HardDevice _HardDevice;
        private micromsg.HardDeviceAttr _HardDeviceAttr;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="BindTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=true, Name="Contact", DataFormat=DataFormat.Default)]
        public ModContact Contact
        {
            get
            {
                return this._Contact;
            }
            set
            {
                this._Contact = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="HardDevice", DataFormat=DataFormat.Default)]
        public micromsg.HardDevice HardDevice
        {
            get
            {
                return this._HardDevice;
            }
            set
            {
                this._HardDevice = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="HardDeviceAttr", DataFormat=DataFormat.Default)]
        public micromsg.HardDeviceAttr HardDeviceAttr
        {
            get
            {
                return this._HardDeviceAttr;
            }
            set
            {
                this._HardDeviceAttr = value;
            }
        }
    }
}

