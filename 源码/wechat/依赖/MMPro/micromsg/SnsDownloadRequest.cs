namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsDownloadRequest")]
    public class SnsDownloadRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _BufferId = "";
        private uint _DownBufLen;
        private int _StartPos;
        private int _TotalLen = 0;
        private uint _Type;
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

        [ProtoMember(4, IsRequired=false, Name="BufferId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BufferId
        {
            get
            {
                return this._BufferId;
            }
            set
            {
                this._BufferId = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="DownBufLen", DataFormat=DataFormat.TwosComplement)]
        public uint DownBufLen
        {
            get
            {
                return this._DownBufLen;
            }
            set
            {
                this._DownBufLen = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public int StartPos
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

        [ProtoMember(3, IsRequired=false, Name="TotalLen", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int TotalLen
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

        [ProtoMember(5, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

