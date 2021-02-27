namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NBSInfo")]
    public class NBSInfo : IExtensible
    {
        private uint _CardFlag;
        private string _Category = "";
        private float _Distance;
        private uint _NBSId;
        private uint _Rank;
        private uint _Status;
        private string _Summary = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="CardFlag", DataFormat=DataFormat.TwosComplement)]
        public uint CardFlag
        {
            get
            {
                return this._CardFlag;
            }
            set
            {
                this._CardFlag = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Category", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Category
        {
            get
            {
                return this._Category;
            }
            set
            {
                this._Category = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Distance", DataFormat=DataFormat.FixedSize)]
        public float Distance
        {
            get
            {
                return this._Distance;
            }
            set
            {
                this._Distance = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="NBSId", DataFormat=DataFormat.TwosComplement)]
        public uint NBSId
        {
            get
            {
                return this._NBSId;
            }
            set
            {
                this._NBSId = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Rank", DataFormat=DataFormat.TwosComplement)]
        public uint Rank
        {
            get
            {
                return this._Rank;
            }
            set
            {
                this._Rank = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Summary", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Summary
        {
            get
            {
                return this._Summary;
            }
            set
            {
                this._Summary = value;
            }
        }
    }
}

