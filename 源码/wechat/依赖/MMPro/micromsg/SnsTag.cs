namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsTag")]
    public class SnsTag : IExtensible
    {
        private uint _Count;
        private readonly List<SKBuiltinString_t> _List = new List<SKBuiltinString_t>();
        private ulong _TagId;
        private string _TagName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(4, Name="List", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> List
        {
            get
            {
                return this._List;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="TagId", DataFormat=DataFormat.TwosComplement)]
        public ulong TagId
        {
            get
            {
                return this._TagId;
            }
            set
            {
                this._TagId = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="TagName", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

