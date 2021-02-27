namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GmailSwitchOplog")]
    public class GmailSwitchOplog : IExtensible
    {
        private string _GmailAcct = "";
        private uint _GmailSwitch;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="GmailAcct", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GmailAcct
        {
            get
            {
                return this._GmailAcct;
            }
            set
            {
                this._GmailAcct = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="GmailSwitch", DataFormat=DataFormat.TwosComplement)]
        public uint GmailSwitch
        {
            get
            {
                return this._GmailSwitch;
            }
            set
            {
                this._GmailSwitch = value;
            }
        }
    }
}

