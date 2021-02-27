namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetTransTextResponse")]
    public class GetTransTextResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _MsgCount;
        private readonly List<TranslatedMsg> _MsgList = new List<TranslatedMsg>();
        private string _ToLang = "";
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

        [ProtoMember(2, IsRequired=true, Name="MsgCount", DataFormat=DataFormat.TwosComplement)]
        public uint MsgCount
        {
            get
            {
                return this._MsgCount;
            }
            set
            {
                this._MsgCount = value;
            }
        }

        [ProtoMember(3, Name="MsgList", DataFormat=DataFormat.Default)]
        public List<TranslatedMsg> MsgList
        {
            get
            {
                return this._MsgList;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ToLang", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToLang
        {
            get
            {
                return this._ToLang;
            }
            set
            {
                this._ToLang = value;
            }
        }
    }
}

