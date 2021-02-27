namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetLocImgRequest")]
    public class GetLocImgRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Height;
        private string _Language = "";
        private float _Latitude;
        private float _Longitude;
        private uint _MapType;
        private uint _Width;
        private uint _ZoomLevel;
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

        [ProtoMember(5, IsRequired=true, Name="Height", DataFormat=DataFormat.TwosComplement)]
        public uint Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                this._Height = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this._Language = value;
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

        [ProtoMember(7, IsRequired=true, Name="MapType", DataFormat=DataFormat.TwosComplement)]
        public uint MapType
        {
            get
            {
                return this._MapType;
            }
            set
            {
                this._MapType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Width", DataFormat=DataFormat.TwosComplement)]
        public uint Width
        {
            get
            {
                return this._Width;
            }
            set
            {
                this._Width = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="ZoomLevel", DataFormat=DataFormat.TwosComplement)]
        public uint ZoomLevel
        {
            get
            {
                return this._ZoomLevel;
            }
            set
            {
                this._ZoomLevel = value;
            }
        }
    }
}

