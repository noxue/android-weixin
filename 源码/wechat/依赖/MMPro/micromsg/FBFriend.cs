namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FBFriend")]
    public class FBFriend : IExtensible
    {
        private ulong _ID;
        private uint _ImgKey;
        private string _Name = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ID", DataFormat=DataFormat.TwosComplement)]
        public ulong ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ImgKey", DataFormat=DataFormat.TwosComplement)]
        public uint ImgKey
        {
            get
            {
                return this._ImgKey;
            }
            set
            {
                this._ImgKey = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

