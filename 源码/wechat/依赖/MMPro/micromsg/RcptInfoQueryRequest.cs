namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RcptInfoQueryRequest")]
    public class RcptInfoQueryRequest : IExtensible
    {
        private string _appid = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _scene = 0;
        private uint _timestamp;
        private string _webviewurl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="appid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string appid
        {
            get
            {
                return this._appid;
            }
            set
            {
                this._appid = value;
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

        [ProtoMember(5, IsRequired=false, Name="scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint scene
        {
            get
            {
                return this._scene;
            }
            set
            {
                this._scene = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="timestamp", DataFormat=DataFormat.TwosComplement)]
        public uint timestamp
        {
            get
            {
                return this._timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="webviewurl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string webviewurl
        {
            get
            {
                return this._webviewurl;
            }
            set
            {
                this._webviewurl = value;
            }
        }
    }
}

