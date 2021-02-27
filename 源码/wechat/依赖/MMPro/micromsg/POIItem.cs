namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="POIItem")]
    public class POIItem : IExtensible
    {
        private string _Addr = "";
        private string _City = "";
        private string _District = "";
        private double _Latitude;
        private string _Link = "";
        private double _Longitude;
        private string _Name = "";
        private string _Nation = "";
        private string _Province = "";
        private string _Street = "";
        private string _SubAddr = "";
        private string _TypeId = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="Addr", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=false, Name="City", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(10, IsRequired=false, Name="District", DataFormat=DataFormat.Default), DefaultValue("")]
        public string District
        {
            get
            {
                return this._District;
            }
            set
            {
                this._District = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=false, Name="Link", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Link
        {
            get
            {
                return this._Link;
            }
            set
            {
                this._Link = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Longitude", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(1, IsRequired=false, Name="Name", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="Nation", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Nation
        {
            get
            {
                return this._Nation;
            }
            set
            {
                this._Nation = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Province", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Province
        {
            get
            {
                return this._Province;
            }
            set
            {
                this._Province = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="Street", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Street
        {
            get
            {
                return this._Street;
            }
            set
            {
                this._Street = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="SubAddr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SubAddr
        {
            get
            {
                return this._SubAddr;
            }
            set
            {
                this._SubAddr = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="TypeId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TypeId
        {
            get
            {
                return this._TypeId;
            }
            set
            {
                this._TypeId = value;
            }
        }
    }
}

