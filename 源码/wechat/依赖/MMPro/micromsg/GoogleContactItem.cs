namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GoogleContactItem")]
    public class GoogleContactItem : IExtensible
    {
        private string _AntispamTicket = "";
        private string _BigHeadUrl = "";
        private string _GoogleContactName = "";
        private string _NickName = "";
        private int _Ret;
        private string _SmallHeadUrl = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=false, Name="AntispamTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AntispamTicket
        {
            get
            {
                return this._AntispamTicket;
            }
            set
            {
                this._AntispamTicket = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="BigHeadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BigHeadUrl
        {
            get
            {
                return this._BigHeadUrl;
            }
            set
            {
                this._BigHeadUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="GoogleContactName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GoogleContactName
        {
            get
            {
                return this._GoogleContactName;
            }
            set
            {
                this._GoogleContactName = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="SmallHeadUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallHeadUrl
        {
            get
            {
                return this._SmallHeadUrl;
            }
            set
            {
                this._SmallHeadUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

