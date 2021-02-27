namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExtDeviceLoginConfirmGetResponse")]
    public class ExtDeviceLoginConfirmGetResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _DeviceNameStr = "";
        private ExtDeviceLoginConfirmErrorRet _ErrorRet = null;
        private ExtDeviceLoginConfirmExpiredRet _ExpiredRet = null;
        private ExtDeviceLoginConfirmOKRet _OKRet = null;
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

        [ProtoMember(5, IsRequired=false, Name="DeviceNameStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceNameStr
        {
            get
            {
                return this._DeviceNameStr;
            }
            set
            {
                this._DeviceNameStr = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ErrorRet", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ExtDeviceLoginConfirmErrorRet ErrorRet
        {
            get
            {
                return this._ErrorRet;
            }
            set
            {
                this._ErrorRet = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ExpiredRet", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ExtDeviceLoginConfirmExpiredRet ExpiredRet
        {
            get
            {
                return this._ExpiredRet;
            }
            set
            {
                this._ExpiredRet = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="OKRet", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ExtDeviceLoginConfirmOKRet OKRet
        {
            get
            {
                return this._OKRet;
            }
            set
            {
                this._OKRet = value;
            }
        }
    }
}

