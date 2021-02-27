namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LbsLifeDetail")]
    public class LbsLifeDetail : IExtensible
    {
        private uint _ActionCount;
        private readonly List<LBSLifeActionList> _ActionList = new List<LBSLifeActionList>();
        private uint _CICount;
        private readonly List<SKBuiltinString_t> _CIList = new List<SKBuiltinString_t>();
        private LbsLife _Life;
        private string _PhotoLink = "";
        private string _ThumbUrl = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=true, Name="ActionCount", DataFormat=DataFormat.TwosComplement)]
        public uint ActionCount
        {
            get
            {
                return this._ActionCount;
            }
            set
            {
                this._ActionCount = value;
            }
        }

        [ProtoMember(7, Name="ActionList", DataFormat=DataFormat.Default)]
        public List<LBSLifeActionList> ActionList
        {
            get
            {
                return this._ActionList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="CICount", DataFormat=DataFormat.TwosComplement)]
        public uint CICount
        {
            get
            {
                return this._CICount;
            }
            set
            {
                this._CICount = value;
            }
        }

        [ProtoMember(3, Name="CIList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> CIList
        {
            get
            {
                return this._CIList;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Life", DataFormat=DataFormat.Default)]
        public LbsLife Life
        {
            get
            {
                return this._Life;
            }
            set
            {
                this._Life = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PhotoLink", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PhotoLink
        {
            get
            {
                return this._PhotoLink;
            }
            set
            {
                this._PhotoLink = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ThumbUrl
        {
            get
            {
                return this._ThumbUrl;
            }
            set
            {
                this._ThumbUrl = value;
            }
        }
    }
}

