namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RadarSearchMember")]
    public class RadarSearchMember : IExtensible
    {
        private string _AntispamTicket = "";
        private uint _Distance;
        private string _EncodeUserName = "";
        private string _NickName = "";
        private string _SmallImgUrl = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=false, Name="AntispamTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=true, Name="Distance", DataFormat=DataFormat.TwosComplement)]
        public uint Distance
        {
            get
            {
                return this._Distance;
            }
            set
            {
                this._Distance = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="EncodeUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string EncodeUserName
        {
            get
            {
                return this._EncodeUserName;
            }
            set
            {
                this._EncodeUserName = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="SmallImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallImgUrl
        {
            get
            {
                return this._SmallImgUrl;
            }
            set
            {
                this._SmallImgUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

