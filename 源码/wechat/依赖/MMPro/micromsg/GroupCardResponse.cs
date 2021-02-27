namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GroupCardResponse")]
    public class GroupCardResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _GroupUserName = "";
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

        [ProtoMember(2, IsRequired=false, Name="GroupUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GroupUserName
        {
            get
            {
                return this._GroupUserName;
            }
            set
            {
                this._GroupUserName = value;
            }
        }
    }
}

