namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BizRegRequest")]
    public class BizRegRequest : IExtensible
    {
        private string _ImgCode = "";
        private string _ImgSid = "";
        private string _NickName = "";
        private string _Pwd = "";
        private string _Pwd2 = "";
        private uint _QQUin;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="ImgCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgCode
        {
            get
            {
                return this._ImgCode;
            }
            set
            {
                this._ImgCode = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ImgSid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgSid
        {
            get
            {
                return this._ImgSid;
            }
            set
            {
                this._ImgSid = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="Pwd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd
        {
            get
            {
                return this._Pwd;
            }
            set
            {
                this._Pwd = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Pwd2", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd2
        {
            get
            {
                return this._Pwd2;
            }
            set
            {
                this._Pwd2 = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="QQUin", DataFormat=DataFormat.TwosComplement)]
        public uint QQUin
        {
            get
            {
                return this._QQUin;
            }
            set
            {
                this._QQUin = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

