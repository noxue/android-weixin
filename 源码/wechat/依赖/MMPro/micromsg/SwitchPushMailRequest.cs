namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SwitchPushMailRequest")]
    public class SwitchPushMailRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _SecPwdMd5 = "";
        private uint _SwitchValue;
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

        [ProtoMember(3, IsRequired=false, Name="SecPwdMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SecPwdMd5
        {
            get
            {
                return this._SecPwdMd5;
            }
            set
            {
                this._SecPwdMd5 = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="SwitchValue", DataFormat=DataFormat.TwosComplement)]
        public uint SwitchValue
        {
            get
            {
                return this._SwitchValue;
            }
            set
            {
                this._SwitchValue = value;
            }
        }
    }
}

