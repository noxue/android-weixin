namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsGetCityRequest")]
    public class SnsGetCityRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _CellId = "";
        private int _GPSSource;
        private int _Latitude;
        private int _Longitude;
        private string _MacAddr = "";
        private int _Precision;
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

        [ProtoMember(7, IsRequired=false, Name="CellId", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=true, Name="GPSSource", DataFormat=DataFormat.TwosComplement)]
        public int GPSSource
        {
            get
            {
                return this._GPSSource;
            }
            set
            {
                this._GPSSource = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
        public int Latitude
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

        [ProtoMember(3, IsRequired=true, Name="Longitude", DataFormat=DataFormat.TwosComplement)]
        public int Longitude
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

        [ProtoMember(6, IsRequired=false, Name="MacAddr", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(5, IsRequired=true, Name="Precision", DataFormat=DataFormat.TwosComplement)]
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
    }
}

