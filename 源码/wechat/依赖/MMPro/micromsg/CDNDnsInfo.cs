namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CDNDnsInfo")]
    public class CDNDnsInfo : IExtensible
    {
        private SKBuiltinBuffer_t _AuthKey;
        private uint _ExpireTime;
        private int _FrontID;
        private int _FrontIPCount;
        private readonly List<SKBuiltinString_t> _FrontIPList = new List<SKBuiltinString_t>();
        private uint _Uin;
        private uint _Ver;
        private string _ZoneDomain = "";
        private int _ZoneID;
        private int _ZoneIPCount;
        private readonly List<SKBuiltinString_t> _ZoneIPList = new List<SKBuiltinString_t>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(8, IsRequired=true, Name="AuthKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t AuthKey
        {
            get
            {
                return this._AuthKey;
            }
            set
            {
                this._AuthKey = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ExpireTime", DataFormat=DataFormat.TwosComplement)]
        public uint ExpireTime
        {
            get
            {
                return this._ExpireTime;
            }
            set
            {
                this._ExpireTime = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="FrontID", DataFormat=DataFormat.TwosComplement)]
        public int FrontID
        {
            get
            {
                return this._FrontID;
            }
            set
            {
                this._FrontID = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="FrontIPCount", DataFormat=DataFormat.TwosComplement)]
        public int FrontIPCount
        {
            get
            {
                return this._FrontIPCount;
            }
            set
            {
                this._FrontIPCount = value;
            }
        }

        [ProtoMember(6, Name="FrontIPList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> FrontIPList
        {
            get
            {
                return this._FrontIPList;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Uin", DataFormat=DataFormat.TwosComplement)]
        public uint Uin
        {
            get
            {
                return this._Uin;
            }
            set
            {
                this._Uin = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Ver", DataFormat=DataFormat.TwosComplement)]
        public uint Ver
        {
            get
            {
                return this._Ver;
            }
            set
            {
                this._Ver = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="ZoneDomain", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ZoneDomain
        {
            get
            {
                return this._ZoneDomain;
            }
            set
            {
                this._ZoneDomain = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="ZoneID", DataFormat=DataFormat.TwosComplement)]
        public int ZoneID
        {
            get
            {
                return this._ZoneID;
            }
            set
            {
                this._ZoneID = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="ZoneIPCount", DataFormat=DataFormat.TwosComplement)]
        public int ZoneIPCount
        {
            get
            {
                return this._ZoneIPCount;
            }
            set
            {
                this._ZoneIPCount = value;
            }
        }

        [ProtoMember(11, Name="ZoneIPList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> ZoneIPList
        {
            get
            {
                return this._ZoneIPList;
            }
        }
    }
}

