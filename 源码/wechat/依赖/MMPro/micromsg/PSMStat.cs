namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PSMStat")]
    public class PSMStat : IExtensible
    {
        private string _AType = "";
        private uint _MType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AType
        {
            get
            {
                return this._AType;
            }
            set
            {
                this._AType = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="MType", DataFormat=DataFormat.TwosComplement)]
        public uint MType
        {
            get
            {
                return this._MType;
            }
            set
            {
                this._MType = value;
            }
        }
    }
}

