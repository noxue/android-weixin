namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AcceptCardListFromAppResponse")]
    public class AcceptCardListFromAppResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _json_ret = "";
        private int _ret_code = 0;
        private string _ret_msg = "";
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

        [ProtoMember(2, IsRequired=false, Name="json_ret", DataFormat=DataFormat.Default), DefaultValue("")]
        public string json_ret
        {
            get
            {
                return this._json_ret;
            }
            set
            {
                this._json_ret = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ret_code", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ret_code
        {
            get
            {
                return this._ret_code;
            }
            set
            {
                this._ret_code = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ret_msg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ret_msg
        {
            get
            {
                return this._ret_msg;
            }
            set
            {
                this._ret_msg = value;
            }
        }
    }
}

