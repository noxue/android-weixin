namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ListMFriendReq")]
    public class ListMFriendReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _EMailCount;
        private readonly List<SKBuiltinString_t> _EMailList = new List<SKBuiltinString_t>();
        private uint _MobileCount;
        private readonly List<SKBuiltinString_t> _MobileList = new List<SKBuiltinString_t>();
        private string _Ticket = "";
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

        [ProtoMember(6, Name="EMailList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> EMailList
        {
            get
            {
                return this._EMailList;
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

        [ProtoMember(4, Name="MobileList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> MobileList
        {
            get
            {
                return this._MobileList;
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

