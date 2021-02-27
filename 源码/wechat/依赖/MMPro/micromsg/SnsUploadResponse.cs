namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsUploadResponse")]
    public class SnsUploadResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SnsBufferUrl _BufferUrl;
        private string _ClientId = "";
        private ulong _Id;
        private uint _StartPos;
        private uint _ThumbUrlCount;
        private readonly List<SnsBufferUrl> _ThumbUrls = new List<SnsBufferUrl>();
        private uint _TotalLen;
        private uint _Type;
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

        [ProtoMember(5, IsRequired=true, Name="BufferUrl", DataFormat=DataFormat.Default)]
        public SnsBufferUrl BufferUrl
        {
            get
            {
                return this._BufferUrl;
            }
            set
            {
                this._BufferUrl = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ClientId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public ulong Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public uint StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="ThumbUrlCount", DataFormat=DataFormat.TwosComplement)]
        public uint ThumbUrlCount
        {
            get
            {
                return this._ThumbUrlCount;
            }
            set
            {
                this._ThumbUrlCount = value;
            }
        }

        [ProtoMember(7, Name="ThumbUrls", DataFormat=DataFormat.Default)]
        public List<SnsBufferUrl> ThumbUrls
        {
            get
            {
                return this._ThumbUrls;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

