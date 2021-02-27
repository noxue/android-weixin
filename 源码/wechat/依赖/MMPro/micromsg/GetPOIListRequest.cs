namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetPOIListRequest")]
    public class GetPOIListRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _Buff;
        private string _Keyword = "";
        private double _Latitude;
        private double _Longitude;
        private uint _OpCode;
        private uint _Scene;
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

        [ProtoMember(5, IsRequired=true, Name="Buff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buff
        {
            get
            {
                return this._Buff;
            }
            set
            {
                this._Buff = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Keyword", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Keyword
        {
            get
            {
                return this._Keyword;
            }
            set
            {
                this._Keyword = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=true, Name="Longitude", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Scene", DataFormat=DataFormat.TwosComplement)]
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
    }
}

