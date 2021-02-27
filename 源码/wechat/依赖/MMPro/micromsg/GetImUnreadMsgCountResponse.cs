namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetImUnreadMsgCountResponse")]
    public class GetImUnreadMsgCountResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _DisplayMsg = "";
        private string _DownloadUrl = "";
        private string _QQScheme = "";
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

        [ProtoMember(2, IsRequired=false, Name="DisplayMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DisplayMsg
        {
            get
            {
                return this._DisplayMsg;
            }
            set
            {
                this._DisplayMsg = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="DownloadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DownloadUrl
        {
            get
            {
                return this._DownloadUrl;
            }
            set
            {
                this._DownloadUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="QQScheme", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QQScheme
        {
            get
            {
                return this._QQScheme;
            }
            set
            {
                this._QQScheme = value;
            }
        }
    }
}

