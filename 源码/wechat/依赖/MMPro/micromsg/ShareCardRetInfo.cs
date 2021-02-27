namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShareCardRetInfo")]
    public class ShareCardRetInfo : IExtensible
    {
        private int _RetCode;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="RetCode", DataFormat=DataFormat.TwosComplement)]
        public int RetCode
        {
            get
            {
                return this._RetCode;
            }
            set
            {
                this._RetCode = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

