namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="TrackPOIItem")]
    public class TrackPOIItem : IExtensible
    {
        private string _Addr = "";
        private double _Latitude;
        private double _Longitude;
        private string _Name = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="Addr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Addr
        {
            get
            {
                return this._Addr;
            }
            set
            {
                this._Addr = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
        public double Latitude
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
        public double Longitude
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

        [ProtoMember(3, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
    }
}

