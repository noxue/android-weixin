namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ListMFriendResp")]
    public class ListMFriendResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _EMailCount;
        private readonly List<ListMFriendMobileInfo> _EMailInfo = new List<ListMFriendMobileInfo>();
        private uint _MobileCount;
        private readonly List<ListMFriendMobileInfo> _MobileInfo = new List<ListMFriendMobileInfo>();
        private string _Ticket = "";
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

        [ProtoMember(5, IsRequired=true, Name="EMailCount", DataFormat=DataFormat.TwosComplement)]
        public uint EMailCount
        {
            get
            {
                return this._EMailCount;
            }
            set
            {
                this._EMailCount = value;
            }
        }

        [ProtoMember(6, Name="EMailInfo", DataFormat=DataFormat.Default)]
        public List<ListMFriendMobileInfo> EMailInfo
        {
            get
            {
                return this._EMailInfo;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="MobileCount", DataFormat=DataFormat.TwosComplement)]
        public uint MobileCount
        {
            get
            {
                return this._MobileCount;
            }
            set
            {
                this._MobileCount = value;
            }
        }

        [ProtoMember(4, Name="MobileInfo", DataFormat=DataFormat.Default)]
        public List<ListMFriendMobileInfo> MobileInfo
        {
            get
            {
                return this._MobileInfo;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Ticket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                this._Ticket = value;
            }
        }
    }
}

