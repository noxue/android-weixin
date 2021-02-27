namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EchoRequest")]
    public class EchoRequest : IExtensible
    {
        private string _EchoStr = "";
        private string _Pwd = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="EchoStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string EchoStr
        {
            get
            {
                return this._EchoStr;
            }
            set
            {
                this._EchoStr = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Pwd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd
        {
            get
            {
                return this._Pwd;
            }
            set
            {
                this._Pwd = value;
            }
        }
    }
}

