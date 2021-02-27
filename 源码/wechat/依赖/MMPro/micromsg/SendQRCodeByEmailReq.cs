namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SendQRCodeByEmailReq")]
    public class SendQRCodeByEmailReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Content = "";
        private string _QRCodeUserName = "";
        private string _Tittle = "";
        private uint _ToCount;
        private readonly List<SKBuiltinString_t> _ToList = new List<SKBuiltinString_t>();
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

        [ProtoMember(6, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="QRCodeUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QRCodeUserName
        {
            get
            {
                return this._QRCodeUserName;
            }
            set
            {
                this._QRCodeUserName = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Tittle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Tittle
        {
            get
            {
                return this._Tittle;
            }
            set
            {
                this._Tittle = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ToCount", DataFormat=DataFormat.TwosComplement)]
        public uint ToCount
        {
            get
            {
                return this._ToCount;
            }
            set
            {
                this._ToCount = value;
            }
        }

        [ProtoMember(4, Name="ToList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> ToList
        {
            get
            {
                return this._ToList;
            }
        }
    }
}

