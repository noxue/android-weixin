namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="TransCDNItem")]
    public class TransCDNItem : IExtensible
    {
        private string _AesKey = "";
        private uint _ClientId;
        private uint _FavDataType;
        private string _FileId = "";
        private ulong _Size;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AesKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AesKey
        {
            get
            {
                return this._AesKey;
            }
            set
            {
                this._AesKey = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ClientId", DataFormat=DataFormat.TwosComplement)]
        public uint ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="FavDataType", DataFormat=DataFormat.TwosComplement)]
        public uint FavDataType
        {
            get
            {
                return this._FavDataType;
            }
            set
            {
                this._FavDataType = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FileId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileId
        {
            get
            {
                return this._FileId;
            }
            set
            {
                this._FileId = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Size", DataFormat=DataFormat.TwosComplement)]
        public ulong Size
        {
            get
            {
                return this._Size;
            }
            set
            {
                this._Size = value;
            }
        }
    }
}

