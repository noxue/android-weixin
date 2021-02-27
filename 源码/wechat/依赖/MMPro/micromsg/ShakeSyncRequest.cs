namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeSyncRequest")]
    public class ShakeSyncRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _CellId = "";
        private uint _ImgId;
        private float _Latitude;
        private float _Longitude;
        private string _MacAddr = "";
        private int _Precision;
        private uint _Times;
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

        [ProtoMember(6, IsRequired=false, Name="CellId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CellId
        {
            get
            {
                return this._CellId;
            }
            set
            {
                this._CellId = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="ImgId", DataFormat=DataFormat.TwosComplement)]
        public uint ImgId
        {
            get
            {
                return this._ImgId;
            }
            set
            {
                this._ImgId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Latitude", DataFormat=DataFormat.FixedSize)]
        public float Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                this._Latitude = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Longitude", DataFormat=DataFormat.FixedSize)]
        public float Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                this._Longitude = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="MacAddr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MacAddr
        {
            get
            {
                return this._MacAddr;
            }
            set
            {
                this._MacAddr = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Precision", DataFormat=DataFormat.TwosComplement)]
        public int Precision
        {
            get
            {
                return this._Precision;
            }
            set
            {
                this._Precision = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="Times", DataFormat=DataFormat.TwosComplement)]
        public uint Times
        {
            get
            {
                return this._Times;
            }
            set
            {
                this._Times = value;
            }
        }
    }
}

