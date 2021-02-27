namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsGetCityResponse")]
    public class SnsGetCityResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _City = "";
        private string _Country = "";
        private int _Latitude;
        private int _Longitude;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="City", DataFormat=DataFormat.Default), DefaultValue("")]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Country", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                this._Country = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(2, IsRequired=true, Name="Longitude", DataFormat=DataFormat.TwosComplement)]
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
    }
}

