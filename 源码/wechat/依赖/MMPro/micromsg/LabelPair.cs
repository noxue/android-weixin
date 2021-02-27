namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LabelPair")]
    public class LabelPair : IExtensible
    {
        private uint _LabelID;
        private string _LabelName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="LabelID", DataFormat=DataFormat.TwosComplement)]
        public uint LabelID
        {
            get
            {
                return this._LabelID;
            }
            set
            {
                this._LabelID = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="LabelName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LabelName
        {
            get
            {
                return this._LabelName;
            }
            set
            {
                this._LabelName = value;
            }
        }
    }
}

