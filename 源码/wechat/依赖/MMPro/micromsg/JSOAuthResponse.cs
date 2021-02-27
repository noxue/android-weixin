namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="JSOAuthResponse")]
    public class JSOAuthResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _OAuthTitle = "";
        private uint _ScopeCount;
        private readonly List<BizScopeInfo> _ScopeList = new List<BizScopeInfo>();
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

        [ProtoMember(4, IsRequired=false, Name="OAuthTitle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OAuthTitle
        {
            get
            {
                return this._OAuthTitle;
            }
            set
            {
                this._OAuthTitle = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ScopeCount", DataFormat=DataFormat.TwosComplement)]
        public uint ScopeCount
        {
            get
            {
                return this._ScopeCount;
            }
            set
            {
                this._ScopeCount = value;
            }
        }

        [ProtoMember(3, Name="ScopeList", DataFormat=DataFormat.Default)]
        public List<BizScopeInfo> ScopeList
        {
            get
            {
                return this._ScopeList;
            }
        }
    }
}

