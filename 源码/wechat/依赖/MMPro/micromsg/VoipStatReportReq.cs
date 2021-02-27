namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipStatReportReq")]
    public class VoipStatReportReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private VoipStatReportData _ChannelReportData = null;
        private VoipStatReportData _DialReportData = null;
        private VoipStatReportData _EngineExtReportData = null;
        private VoipStatReportData _EngineReportData = null;
        private VoipStatReportData _ReportData;
        private ulong _Timestamp64 = 0L;
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

        [ProtoMember(3, IsRequired=false, Name="ChannelReportData", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipStatReportData ChannelReportData
        {
            get
            {
                return this._ChannelReportData;
            }
            set
            {
                this._ChannelReportData = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="DialReportData", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipStatReportData DialReportData
        {
            get
            {
                return this._DialReportData;
            }
            set
            {
                this._DialReportData = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="EngineExtReportData", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipStatReportData EngineExtReportData
        {
            get
            {
                return this._EngineExtReportData;
            }
            set
            {
                this._EngineExtReportData = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="EngineReportData", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipStatReportData EngineReportData
        {
            get
            {
                return this._EngineReportData;
            }
            set
            {
                this._EngineReportData = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ReportData", DataFormat=DataFormat.Default)]
        public VoipStatReportData ReportData
        {
            get
            {
                return this._ReportData;
            }
            set
            {
                this._ReportData = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Timestamp64", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong Timestamp64
        {
            get
            {
                return this._Timestamp64;
            }
            set
            {
                this._Timestamp64 = value;
            }
        }
    }
}

