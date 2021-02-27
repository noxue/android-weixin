namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GroupCardReq")]
    public class GroupCardReq : IExtensible
    {
        private string _GroupCardName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="GroupCardName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GroupCardName
        {
            get
            {
                return this._GroupCardName;
            }
            set
            {
                this._GroupCardName = value;
            }
        }
    }
}

