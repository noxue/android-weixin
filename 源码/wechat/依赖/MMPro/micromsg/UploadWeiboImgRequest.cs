namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadWeiboImgRequest")]
    public class UploadWeiboImgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ClientImgId = "";
        private string _Content = "";
        private byte[] _Data = null;
        private uint _DataLen;
        private uint _FilterType;
        private uint _Flag = 0;
        private uint _StartPos;
        private uint _TotalLen;
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

        [ProtoMember(2, IsRequired=false, Name="ClientImgId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientImgId
        {
            get
            {
                return this._ClientImgId;
            }
            set
            {
                this._ClientImgId = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Data", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="DataLen", DataFormat=DataFormat.TwosComplement)]
        public uint DataLen
        {
            get
            {
                return this._DataLen;
            }
            set
            {
                this._DataLen = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="FilterType", DataFormat=DataFormat.TwosComplement)]
        public uint FilterType
        {
            get
            {
                return this._FilterType;
            }
            set
            {
                this._FilterType = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Flag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(4, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
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
    }
}

