namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="StatusNotifyFunction")]
    public class StatusNotifyFunction : IExtensible
    {
        private string _Arg = "";
        private string _Name = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Arg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Arg
        {
            get
            {
                return this._Arg;
            }
            set
            {
                this._Arg = value;
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
    }
}

