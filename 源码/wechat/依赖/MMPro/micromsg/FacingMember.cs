namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FacingMember")]
    public class FacingMember : IExtensible
    {
        private string _EncodeUserName = "";
        private string _NickName = "";
        private string _SmallImgUrl = "";
        private uint _Status;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="EncodeUserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
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

