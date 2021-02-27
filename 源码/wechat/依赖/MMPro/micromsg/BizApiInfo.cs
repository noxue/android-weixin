namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BizApiInfo")]
    public class BizApiInfo : IExtensible
    {
        private string _ApiName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="ApiName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ApiName
        {
            get
            {
                return this._ApiName;
            }
            set
            {
                this._ApiName = value;
            }
        }
    }
}

