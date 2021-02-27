namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetChatRoomPresentHistoryResp")]
    public class GetChatRoomPresentHistoryResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _DonateStatus;
        private readonly List<Presentation> _List = new List<Presentation>();
        private uint _TotalCount;
        private string _Url = "";
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

        [ProtoMember(5, IsRequired=true, Name="DonateStatus", DataFormat=DataFormat.TwosComplement)]
        public uint DonateStatus
        {
            get
            {
                return this._DonateStatus;
            }
            set
            {
                this._DonateStatus = value;
            }
        }

        [ProtoMember(3, Name="List", DataFormat=DataFormat.Default)]
        public List<Presentation> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="TotalCount", DataFormat=DataFormat.TwosComplement)]
        public uint TotalCount
        {
            get
            {
                return this._TotalCount;
            }
            set
            {
                this._TotalCount = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

