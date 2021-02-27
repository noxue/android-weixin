namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckPCWechatFileRequest")]
    public class CheckPCWechatFileRequest : IExtensible
    {
        private string _AesKey = "";
        private string _FileId = "";
        private string _FileName = "";
        private string _FromUsername = "";
        private string _MD5 = "";
        private string _ToUsername = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="AesKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AesKey
        {
            get
            {
                return this._AesKey;
            }
            set
            {
                this._AesKey = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="FileId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileId
        {
            get
            {
                return this._FileId;
            }
            set
            {
                this._FileId = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="FileName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=false, Name="FromUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUsername
        {
            get
            {
                return this._FromUsername;
            }
            set
            {
                this._FromUsername = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="MD5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MD5
        {
            get
            {
                return this._MD5;
            }
            set
            {
                this._MD5 = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ToUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUsername
        {
            get
            {
                return this._ToUsername;
            }
            set
            {
                this._ToUsername = value;
            }
        }
    }
}

