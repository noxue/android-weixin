namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="Report")]
    public class Report : IExtensible
    {
        private string _Log = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="Log", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Log
        {
            get
            {
                return this._Log;
            }
            set
            {
                this._Log = value;
            }
        }
    }
}

