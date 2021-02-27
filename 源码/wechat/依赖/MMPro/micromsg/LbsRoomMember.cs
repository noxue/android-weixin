namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LbsRoomMember")]
    public class LbsRoomMember : IExtensible
    {
        private string _AntispamTicket = "";
        private string _BigHeadImgUrl = "";
        private string _NickName = "";
        private string _SmallHeadImgUrl = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="AntispamTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BigHeadImgUrl
        {
            get
            {
                return this._BigHeadImgUrl;
            }
            set
            {
                this._BigHeadImgUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallHeadImgUrl
        {
            get
            {
                return this._SmallHeadImgUrl;
            }
            set
            {
                this._SmallHeadImgUrl = value;
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

