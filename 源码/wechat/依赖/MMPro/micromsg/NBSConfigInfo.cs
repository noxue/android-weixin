namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NBSConfigInfo")]
    public class NBSConfigInfo : IExtensible
    {
        private uint _ConfId;
        private string _Summary = "";
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ConfId", DataFormat=DataFormat.TwosComplement)]
        public uint ConfId
        {
            get
            {
                return this._ConfId;
            }
            set
            {
                this._ConfId = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Summary", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Summary
        {
            get
            {
                return this._Summary;
            }
            set
            {
                this._Summary = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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
    }
}

