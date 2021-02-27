namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AdditionalContactList")]
    public class AdditionalContactList : IExtensible
    {
        private micromsg.LinkedinContactItem _LinkedinContactItem = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="LinkedinContactItem", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.LinkedinContactItem LinkedinContactItem
        {
            get
            {
                return this._LinkedinContactItem;
            }
            set
            {
                this._LinkedinContactItem = value;
            }
        }
    }
}

