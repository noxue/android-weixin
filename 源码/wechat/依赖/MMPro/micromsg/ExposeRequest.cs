namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExposeRequest")]
    public class ExposeRequest : IExtensible
    {
        private string _AlbumPhotoId = "";
        private uint _AlbumType = 0;
        private micromsg.BaseRequest _BaseRequest;
        private string _ExposeContent = "";
        private uint _Scene;
        private ulong _SnsId = 0L;
        private uint _Type;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=false, Name="AlbumPhotoId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AlbumPhotoId
        {
            get
            {
                return this._AlbumPhotoId;
            }
            set
            {
                this._AlbumPhotoId = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="AlbumType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AlbumType
        {
            get
            {
                return this._AlbumType;
            }
            set
            {
                this._AlbumType = value;
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

        [ProtoMember(8, IsRequired=false, Name="ExposeContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExposeContent
        {
            get
            {
                return this._ExposeContent;
            }
            set
            {
                this._ExposeContent = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="SnsId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong SnsId
        {
            get
            {
                return this._SnsId;
            }
            set
            {
                this._SnsId = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
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

