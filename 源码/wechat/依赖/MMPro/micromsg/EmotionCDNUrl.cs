namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmotionCDNUrl")]
    public class EmotionCDNUrl : IExtensible
    {
        private string _AesKey = "";
        private uint _FileSize;
        private string _Url = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AesKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AesKey
        {
            get
            {
                return this._AesKey;
            }
            set
            {
                this._AesKey = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="FileSize", DataFormat=DataFormat.TwosComplement)]
        public uint FileSize
        {
            get
            {
                return this._FileSize;
            }
            set
            {
                this._FileSize = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Url", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }
    }
}

