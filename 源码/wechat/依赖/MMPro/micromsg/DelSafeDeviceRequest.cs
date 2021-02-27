﻿namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DelSafeDeviceRequest")]
    public class DelSafeDeviceRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Uuid = "";
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

        [ProtoMember(2, IsRequired=false, Name="Uuid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Uuid
        {
            get
            {
                return this._Uuid;
            }
            set
            {
                this._Uuid = value;
            }
        }
    }
}

