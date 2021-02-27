namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CardTpInfoItem")]
    public class CardTpInfoItem : IExtensible
    {
        private string _card_tp_id = "";
        private string _code = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="card_tp_id", DataFormat=DataFormat.Default), DefaultValue("")]
        public string card_tp_id
        {
            get
            {
                return this._card_tp_id;
            }
            set
            {
                this._card_tp_id = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="code", DataFormat=DataFormat.Default), DefaultValue("")]
        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }
    }
}

