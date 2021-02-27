namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipMultiRelayData")]
    public class VoipMultiRelayData : IExtensible
    {
        private int _ARQCacheLen = 0;
        private int _ARQRespRateThreshold = 0;
        private int _ARQRttThreshold = 0;
        private int _ARQStrategy = 0;
        private int _ARQUsedRateThreshold = 0;
        private VoipRelayData _CapInfo;
        private SKBuiltinBuffer_t _ClientHeadSigns = null;
        private SKBuiltinBuffer_t _EncryptKeyBuf = null;
        private int _EncryptStrategy = 0;
        private SKBuiltinBuffer_t _FECKeyPara1 = null;
        private SKBuiltinBuffer_t _FECKeyPara2 = null;
        private int _FECSvrCtr = 0;
        private int _LinkDisconnectHbCnt = 0;
        private int _LinkDisconnectHbInterval = 0;
        private uint _OppositeClientVersion = 0;
        private uint _OppositeDeviceType = 0;
        private VoipRelayData _PeerId;
        private uint _ProtocolEncryptLength = 0;
        private VoipAddrSet _PunchSvrAddr = null;
        private int _QosStrategy = 0;
        private int _SendingType;
        private VoipAddrSet _TcpSvrAddr = null;
        private int _VoipNetQuality = 0;
        private VoipAddrSet _VoipSvrAddr = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(14, IsRequired=false, Name="ARQCacheLen", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ARQCacheLen
        {
            get
            {
                return this._ARQCacheLen;
            }
            set
            {
                this._ARQCacheLen = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="ARQRespRateThreshold", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ARQRespRateThreshold
        {
            get
            {
                return this._ARQRespRateThreshold;
            }
            set
            {
                this._ARQRespRateThreshold = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="ARQRttThreshold", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ARQRttThreshold
        {
            get
            {
                return this._ARQRttThreshold;
            }
            set
            {
                this._ARQRttThreshold = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="ARQStrategy", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ARQStrategy
        {
            get
            {
                return this._ARQStrategy;
            }
            set
            {
                this._ARQStrategy = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="ARQUsedRateThreshold", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ARQUsedRateThreshold
        {
            get
            {
                return this._ARQUsedRateThreshold;
            }
            set
            {
                this._ARQUsedRateThreshold = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="CapInfo", DataFormat=DataFormat.Default)]
        public VoipRelayData CapInfo
        {
            get
            {
                return this._CapInfo;
            }
            set
            {
                this._CapInfo = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="ClientHeadSigns", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t ClientHeadSigns
        {
            get
            {
                return this._ClientHeadSigns;
            }
            set
            {
                this._ClientHeadSigns = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="EncryptKeyBuf", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t EncryptKeyBuf
        {
            get
            {
                return this._EncryptKeyBuf;
            }
            set
            {
                this._EncryptKeyBuf = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="EncryptStrategy", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int EncryptStrategy
        {
            get
            {
                return this._EncryptStrategy;
            }
            set
            {
                this._EncryptStrategy = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="FECKeyPara1", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t FECKeyPara1
        {
            get
            {
                return this._FECKeyPara1;
            }
            set
            {
                this._FECKeyPara1 = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="FECKeyPara2", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t FECKeyPara2
        {
            get
            {
                return this._FECKeyPara2;
            }
            set
            {
                this._FECKeyPara2 = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="FECSvrCtr", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int FECSvrCtr
        {
            get
            {
                return this._FECSvrCtr;
            }
            set
            {
                this._FECSvrCtr = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="LinkDisconnectHbCnt", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int LinkDisconnectHbCnt
        {
            get
            {
                return this._LinkDisconnectHbCnt;
            }
            set
            {
                this._LinkDisconnectHbCnt = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="LinkDisconnectHbInterval", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int LinkDisconnectHbInterval
        {
            get
            {
                return this._LinkDisconnectHbInterval;
            }
            set
            {
                this._LinkDisconnectHbInterval = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="OppositeClientVersion", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint OppositeClientVersion
        {
            get
            {
                return this._OppositeClientVersion;
            }
            set
            {
                this._OppositeClientVersion = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="OppositeDeviceType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint OppositeDeviceType
        {
            get
            {
                return this._OppositeDeviceType;
            }
            set
            {
                this._OppositeDeviceType = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="PeerId", DataFormat=DataFormat.Default)]
        public VoipRelayData PeerId
        {
            get
            {
                return this._PeerId;
            }
            set
            {
                this._PeerId = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="ProtocolEncryptLength", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ProtocolEncryptLength
        {
            get
            {
                return this._ProtocolEncryptLength;
            }
            set
            {
                this._ProtocolEncryptLength = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PunchSvrAddr", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipAddrSet PunchSvrAddr
        {
            get
            {
                return this._PunchSvrAddr;
            }
            set
            {
                this._PunchSvrAddr = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="QosStrategy", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int QosStrategy
        {
            get
            {
                return this._QosStrategy;
            }
            set
            {
                this._QosStrategy = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="SendingType", DataFormat=DataFormat.TwosComplement)]
        public int SendingType
        {
            get
            {
                return this._SendingType;
            }
            set
            {
                this._SendingType = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="TcpSvrAddr", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipAddrSet TcpSvrAddr
        {
            get
            {
                return this._TcpSvrAddr;
            }
            set
            {
                this._TcpSvrAddr = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="VoipNetQuality", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int VoipNetQuality
        {
            get
            {
                return this._VoipNetQuality;
            }
            set
            {
                this._VoipNetQuality = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="VoipSvrAddr", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipAddrSet VoipSvrAddr
        {
            get
            {
                return this._VoipSvrAddr;
            }
            set
            {
                this._VoipSvrAddr = value;
            }
        }
    }
}

