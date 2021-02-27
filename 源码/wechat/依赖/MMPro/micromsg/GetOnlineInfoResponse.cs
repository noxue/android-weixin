namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetOnlineInfoResponse")]
    public class GetOnlineInfoResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Flag = 0;
        private uint _IConType = 0;
        private uint _OnlineCount;
        private readonly List<OnlineInfo> _OnlineList = new List<OnlineInfo>();
        private string _SummaryXML = "";
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

        [ProtoMember(5, IsRequired=false, Name="Flag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="IConType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IConType
        {
            get
            {
                return this._IConType;
            }
            set
            {
                this._IConType = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OnlineCount", DataFormat=DataFormat.TwosComplement)]
        public uint OnlineCount
        {
            get
            {
                return this._OnlineCount;
            }
            set
            {
                this._OnlineCount = value;
            }
        }

        [ProtoMember(3, Name="OnlineList", DataFormat=DataFormat.Default)]
        public List<OnlineInfo> OnlineList
        {
            get
            {
                return this._OnlineList;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="SummaryXML", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SummaryXML
        {
            get
            {
                return this._SummaryXML;
            }
            set
            {
                this._SummaryXML = value;
            }
        }
    }
}

