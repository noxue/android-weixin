namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VUserResponseItem")]
    public class VUserResponseItem : IExtensible
    {
        private string _Desc = "";
        private SKBuiltinBuffer_t _HDIcon;
        private SKBuiltinBuffer_t _Icon;
        private uint _ID;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Desc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Desc
        {
            get
            {
                return this._Desc;
            }
            set
            {
                this._Desc = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="HDIcon", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t HDIcon
        {
            get
            {
                return this._HDIcon;
            }
            set
            {
                this._HDIcon = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Icon", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Icon
        {
            get
            {
                return this._Icon;
            }
            set
            {
                this._Icon = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ID", DataFormat=DataFormat.TwosComplement)]
        public uint ID
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
    }
}

