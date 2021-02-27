namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SendPhoto2FBWallRequest")]
    public class SendPhoto2FBWallRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Caption = "";
        private string _Description = "";
        private string _Link = "";
        private string _Message = "";
        private string _Name = "";
        private string _Picture = "";
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

        [ProtoMember(7, IsRequired=false, Name="Caption", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Caption
        {
            get
            {
                return this._Caption;
            }
            set
            {
                this._Caption = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Description", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Link", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Link
        {
            get
            {
                return this._Link;
            }
            set
            {
                this._Link = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Message", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Picture", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Picture
        {
            get
            {
                return this._Picture;
            }
            set
            {
                this._Picture = value;
            }
        }
    }
}

