namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LbsLife")]
    public class LbsLife : IExtensible
    {
        private string _BId = "";
        private uint _DescCount;
        private readonly List<SKBuiltinString_t> _DescList = new List<SKBuiltinString_t>();
        private uint _IconIdxCount;
        private readonly List<uint> _IconIdxList = new List<uint>();
        private string _Link = "";
        private float _Price;
        private float _Rate;
        private string _Title = "";
        private uint _Type = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="BId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BId
        {
            get
            {
                return this._BId;
            }
            set
            {
                this._BId = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="DescCount", DataFormat=DataFormat.TwosComplement)]
        public uint DescCount
        {
            get
            {
                return this._DescCount;
            }
            set
            {
                this._DescCount = value;
            }
        }

        [ProtoMember(7, Name="DescList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> DescList
        {
            get
            {
                return this._DescList;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="IconIdxCount", DataFormat=DataFormat.TwosComplement)]
        public uint IconIdxCount
        {
            get
            {
                return this._IconIdxCount;
            }
            set
            {
                this._IconIdxCount = value;
            }
        }

        [ProtoMember(5, Name="IconIdxList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> IconIdxList
        {
            get
            {
                return this._IconIdxList;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Link", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=true, Name="Price", DataFormat=DataFormat.FixedSize)]
        public float Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="Rate", DataFormat=DataFormat.FixedSize)]
        public float Rate
        {
            get
            {
                return this._Rate;
            }
            set
            {
                this._Rate = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Title", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

