namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckBigFileDownloadRequest")]
    public class CheckBigFileDownloadRequest : IExtensible
    {
        private string _AESKey = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _FileExt = "";
        private string _FileMd5 = "";
        private string _FileName = "";
        private ulong _FileSize;
        private uint _FileType;
        private string _FromUserName = "";
        private string _ToUserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="AESKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AESKey
        {
            get
            {
                return this._AESKey;
            }
            set
            {
                this._AESKey = value;
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

        [ProtoMember(8, IsRequired=false, Name="FileExt", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileExt
        {
            get
            {
                return this._FileExt;
            }
            set
            {
                this._FileExt = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="FileMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileMd5
        {
            get
            {
                return this._FileMd5;
            }
            set
            {
                this._FileMd5 = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="FileName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="FileSize", DataFormat=DataFormat.TwosComplement)]
        public ulong FileSize
        {
            get
            {
                return this._FileSize;
            }
            set
            {
                this._FileSize = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="FileType", DataFormat=DataFormat.TwosComplement)]
        public uint FileType
        {
            get
            {
                return this._FileType;
            }
            set
            {
                this._FileType = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FromUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUserName
        {
            get
            {
                return this._FromUserName;
            }
            set
            {
                this._FromUserName = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUserName
        {
            get
            {
                return this._ToUserName;
            }
            set
            {
                this._ToUserName = value;
            }
        }
    }
}

