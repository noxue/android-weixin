namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetSuggestAliasResponse")]
    public class GetSuggestAliasResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private readonly List<SKBuiltinString_t> _List = new List<SKBuiltinString_t>();
        private int _UserNameRet = 0;
        private SKBuiltinBuffer_t _VerifyBuff = null;
        private string _VerifySignature = "";
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

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="UserNameRet", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int UserNameRet
        {
            get
            {
                return this._UserNameRet;
            }
            set
            {
                this._UserNameRet = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="VerifyBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t VerifyBuff
        {
            get
            {
                return this._VerifyBuff;
            }
            set
            {
                this._VerifyBuff = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="VerifySignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifySignature
        {
            get
            {
                return this._VerifySignature;
            }
            set
            {
                this._VerifySignature = value;
            }
        }
    }
}

