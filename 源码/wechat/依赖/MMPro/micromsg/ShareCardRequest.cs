namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShareCardRequest")]
    public class ShareCardRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _DestFriendCount;
        private readonly List<ShareCardDestInfo> _DestFriendList = new List<ShareCardDestInfo>();
        private uint _OpCode;
        private string _SrcFriendUserName = "";
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

        [ProtoMember(4, IsRequired=true, Name="DestFriendCount", DataFormat=DataFormat.TwosComplement)]
        public uint DestFriendCount
        {
            get
            {
                return this._DestFriendCount;
            }
            set
            {
                this._DestFriendCount = value;
            }
        }

        [ProtoMember(5, Name="DestFriendList", DataFormat=DataFormat.Default)]
        public List<ShareCardDestInfo> DestFriendList
        {
            get
            {
                return this._DestFriendList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="SrcFriendUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SrcFriendUserName
        {
            get
            {
                return this._SrcFriendUserName;
            }
            set
            {
                this._SrcFriendUserName = value;
            }
        }
    }
}

