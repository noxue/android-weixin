namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetCDNDnsResponse")]
    public class GetCDNDnsResponse : IExtensible
    {
        private CDNDnsInfo _AppDnsInfo = null;
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _CDNDnsRuleBuf = null;
        private CDNDnsInfo _DnsInfo;
        private SKBuiltinBuffer_t _FakeCDNDnsRuleBuf = null;
        private CDNDnsInfo _FakeDnsInfo = null;
        private CDNDnsInfo _SnsDnsInfo = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="AppDnsInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public CDNDnsInfo AppDnsInfo
        {
            get
            {
                return this._AppDnsInfo;
            }
            set
            {
                this._AppDnsInfo = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="CDNDnsRuleBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t CDNDnsRuleBuf
        {
            get
            {
                return this._CDNDnsRuleBuf;
            }
            set
            {
                this._CDNDnsRuleBuf = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="DnsInfo", DataFormat=DataFormat.Default)]
        public CDNDnsInfo DnsInfo
        {
            get
            {
                return this._DnsInfo;
            }
            set
            {
                this._DnsInfo = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="FakeCDNDnsRuleBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t FakeCDNDnsRuleBuf
        {
            get
            {
                return this._FakeCDNDnsRuleBuf;
            }
            set
            {
                this._FakeCDNDnsRuleBuf = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="FakeDnsInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public CDNDnsInfo FakeDnsInfo
        {
            get
            {
                return this._FakeDnsInfo;
            }
            set
            {
                this._FakeDnsInfo = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="SnsDnsInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public CDNDnsInfo SnsDnsInfo
        {
            get
            {
                return this._SnsDnsInfo;
            }
            set
            {
                this._SnsDnsInfo = value;
            }
        }
    }
}

