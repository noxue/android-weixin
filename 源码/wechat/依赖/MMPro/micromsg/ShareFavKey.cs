namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShareFavKey")]
    public class ShareFavKey : IExtensible
    {
        private uint _FavId;
        private string _FavItemCheck = "";
        private uint _FromUin;
        private string _ParamCheck = "";
        private uint _Scene;
        private uint _ShareTime;
        private string _ToUser = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=true, Name="FavId", DataFormat=DataFormat.TwosComplement)]
        public uint FavId
        {
            get
            {
                return this._FavId;
            }
            set
            {
                this._FavId = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="FavItemCheck", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FavItemCheck
        {
            get
            {
                return this._FavItemCheck;
            }
            set
            {
                this._FavItemCheck = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="FromUin", DataFormat=DataFormat.TwosComplement)]
        public uint FromUin
        {
            get
            {
                return this._FromUin;
            }
            set
            {
                this._FromUin = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ParamCheck", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ParamCheck
        {
            get
            {
                return this._ParamCheck;
            }
            set
            {
                this._ParamCheck = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=true, Name="ShareTime", DataFormat=DataFormat.TwosComplement)]
        public uint ShareTime
        {
            get
            {
                return this._ShareTime;
            }
            set
            {
                this._ShareTime = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ToUser", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUser
        {
            get
            {
                return this._ToUser;
            }
            set
            {
                this._ToUser = value;
            }
        }
    }
}

