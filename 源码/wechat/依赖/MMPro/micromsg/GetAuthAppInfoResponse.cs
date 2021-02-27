namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetAuthAppInfoResponse")]
    public class GetAuthAppInfoResponse : IExtensible
    {
        private string _AuthInfo = "";
        private AuthAppBaseInfo _BaseInfo;
        private micromsg.BaseResponse _BaseResponse;
        private string _DevInfo = "";
        private string _ExternInfo = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AuthInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthInfo
        {
            get
            {
                return this._AuthInfo;
            }
            set
            {
                this._AuthInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="BaseInfo", DataFormat=DataFormat.Default)]
        public AuthAppBaseInfo BaseInfo
        {
            get
            {
                return this._BaseInfo;
            }
            set
            {
                this._BaseInfo = value;
            }
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

        [ProtoMember(4, IsRequired=false, Name="DevInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DevInfo
        {
            get
            {
                return this._DevInfo;
            }
            set
            {
                this._DevInfo = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ExternInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExternInfo
        {
            get
            {
                return this._ExternInfo;
            }
            set
            {
                this._ExternInfo = value;
            }
        }
    }
}

