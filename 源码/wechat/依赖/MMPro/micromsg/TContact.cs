namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="TContact")]
    public class TContact : IExtensible
    {
        private string _DisplayName = "";
        private uint _ExtInfoSeq;
        private uint _ImgUpdateSeq;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="DisplayName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                this._DisplayName = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ExtInfoSeq", DataFormat=DataFormat.TwosComplement)]
        public uint ExtInfoSeq
        {
            get
            {
                return this._ExtInfoSeq;
            }
            set
            {
                this._ExtInfoSeq = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ImgUpdateSeq", DataFormat=DataFormat.TwosComplement)]
        public uint ImgUpdateSeq
        {
            get
            {
                return this._ImgUpdateSeq;
            }
            set
            {
                this._ImgUpdateSeq = value;
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

