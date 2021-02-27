namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetCardSerialResponse")]
    public class GetCardSerialResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _serial_number = "";
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

        [ProtoMember(2, IsRequired=false, Name="serial_number", DataFormat=DataFormat.Default), DefaultValue("")]
        public string serial_number
        {
            get
            {
                return this._serial_number;
            }
            set
            {
                this._serial_number = value;
            }
        }
    }
}

