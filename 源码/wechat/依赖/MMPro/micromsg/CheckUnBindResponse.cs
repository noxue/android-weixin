namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckUnBindResponse")]
    public class CheckUnBindResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _CanUnbindNotice = "";
        private string _RandomPasswd = "";
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

        [ProtoMember(3, IsRequired=false, Name="CanUnbindNotice", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CanUnbindNotice
        {
            get
            {
                return this._CanUnbindNotice;
            }
            set
            {
                this._CanUnbindNotice = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="RandomPasswd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RandomPasswd
        {
            get
            {
                return this._RandomPasswd;
            }
            set
            {
                this._RandomPasswd = value;
            }
        }
    }
}

