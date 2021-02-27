namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ModFavObject")]
    public class ModFavObject : IExtensible
    {
        private string _AttrName = "";
        private string _TagName = "";
        private uint _Type = 0;
        private string _Value = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AttrName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AttrName
        {
            get
            {
                return this._AttrName;
            }
            set
            {
                this._AttrName = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="TagName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TagName
        {
            get
            {
                return this._TagName;
            }
            set
            {
                this._TagName = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(3, IsRequired=false, Name="Value", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }
    }
}

