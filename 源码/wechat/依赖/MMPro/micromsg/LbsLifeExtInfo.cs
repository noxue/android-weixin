namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LbsLifeExtInfo")]
    public class LbsLifeExtInfo : IExtensible
    {
        private uint _Limit;
        private uint _Page;
        private string _sessiontoken = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Limit", DataFormat=DataFormat.TwosComplement)]
        public uint Limit
        {
            get
            {
                return this._Limit;
            }
            set
            {
                this._Limit = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Page", DataFormat=DataFormat.TwosComplement)]
        public uint Page
        {
            get
            {
                return this._Page;
            }
            set
            {
                this._Page = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="sessiontoken", DataFormat=DataFormat.Default), DefaultValue("")]
        public string sessiontoken
        {
            get
            {
                return this._sessiontoken;
            }
            set
            {
                this._sessiontoken = value;
            }
        }
    }
}

