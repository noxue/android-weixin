namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ScanBarcodeRequest")]
    public class ScanBarcodeRequest : IExtensible
    {
        private string _Barcode = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _Scene = 0;
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="Barcode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Barcode
        {
            get
            {
                return this._Barcode;
            }
            set
            {
                this._Barcode = value;
            }
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

        [ProtoMember(4, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

