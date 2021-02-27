namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FavObject")]
    public class FavObject : IExtensible
    {
        private uint _FavId;
        private uint _Flag;
        private string _Object = "";
        private int _Status;
        private uint _UpdateSeq;
        private uint _UpdateTime;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="FavId", DataFormat=DataFormat.TwosComplement)]
        public uint FavId
        {
            get
            {
                return this._FavId;
            }
            set
            {
                this._FavId = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Flag", DataFormat=DataFormat.TwosComplement)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Object", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Object
        {
            get
            {
                return this._Object;
            }
            set
            {
                this._Object = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public int Status
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

        [ProtoMember(6, IsRequired=true, Name="UpdateSeq", DataFormat=DataFormat.TwosComplement)]
        public uint UpdateSeq
        {
            get
            {
                return this._UpdateSeq;
            }
            set
            {
                this._UpdateSeq = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="UpdateTime", DataFormat=DataFormat.TwosComplement)]
        public uint UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this._UpdateTime = value;
            }
        }
    }
}

