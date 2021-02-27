namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SetOAuthScopeRequest")]
    public class SetOAuthScopeRequest : IExtensible
    {
        private string _AppID = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _ScopeCount;
        private readonly List<BizScopeInfo> _ScopeList = new List<BizScopeInfo>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppID
        {
            get
            {
                return this._AppID;
            }
            set
            {
                this._AppID = value;
            }
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

        [ProtoMember(3, IsRequired=true, Name="ScopeCount", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(4, Name="ScopeList", DataFormat=DataFormat.Default)]
        public List<BizScopeInfo> ScopeList
        {
            get
            {
                return this._ScopeList;
            }
        }
    }
}

