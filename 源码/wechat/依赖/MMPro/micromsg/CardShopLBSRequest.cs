namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CardShopLBSRequest")]
    public class CardShopLBSRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _card_tp_id = "";
        private float _latitude;
        private float _longitude;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="card_tp_id", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=true, Name="latitude", DataFormat=DataFormat.FixedSize)]
        public float latitude
        {
            get
            {
                return this._latitude;
            }
            set
            {
                this._latitude = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="longitude", DataFormat=DataFormat.FixedSize)]
        public float longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                this._longitude = value;
            }
        }
    }
}

