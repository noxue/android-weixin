namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Receipt")]
    public class Receipt : IExtensible
    {
        private string _Detail = "";
        private uint _IsNeed;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Detail", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                this._Detail = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="IsNeed", DataFormat=DataFormat.TwosComplement)]
        public uint IsNeed
        {
            get
            {
                return this._IsNeed;
            }
            set
            {
                this._IsNeed = value;
            }
        }
    }
}

