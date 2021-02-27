namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Express")]
    public class Express : IExtensible
    {
        private uint _Id = 0;
        private string _Name = "";
        private uint _Price = 0;
        private string _PriceType = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="Id", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="Price", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="PriceType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PriceType
        {
            get
            {
                return this._PriceType;
            }
            set
            {
                this._PriceType = value;
            }
        }
    }
}

