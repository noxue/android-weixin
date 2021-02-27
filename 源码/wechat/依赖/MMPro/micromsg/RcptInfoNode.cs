namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RcptInfoNode")]
    public class RcptInfoNode : IExtensible
    {
        private SKBuiltinString_t _city;
        private SKBuiltinString_t _country = null;
        private SKBuiltinString_t _detail;
        private SKBuiltinString_t _district;
        private uint _id = 0;
        private SKBuiltinString_t _name;
        private SKBuiltinString_t _nationalcode_gbt2260 = null;
        private SKBuiltinString_t _phone;
        private SKBuiltinString_t _province;
        private SKBuiltinString_t _zipcode;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="city", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t city
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="country", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="detail", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t detail
        {
            get
            {
                return this._detail;
            }
            set
            {
                this._detail = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="district", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t district
        {
            get
            {
                return this._district;
            }
            set
            {
                this._district = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="id", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="name", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="nationalcode_gbt2260", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t nationalcode_gbt2260
        {
            get
            {
                return this._nationalcode_gbt2260;
            }
            set
            {
                this._nationalcode_gbt2260 = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="phone", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="province", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t province
        {
            get
            {
                return this._province;
            }
            set
            {
                this._province = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="zipcode", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t zipcode
        {
            get
            {
                return this._zipcode;
            }
            set
            {
                this._zipcode = value;
            }
        }
    }
}

