namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipAnswerResp")]
    public class VoipAnswerResp : IExtensible
    {
        private int _AddrCount;
        private readonly List<VoipAddr> _AddrList = new List<VoipAddr>();
        private int _AudioDTX = 0;
        private int _AudioEnableRmioAndS3A = 0;
        private int _AudioEnableSpkec = 0;
        private int _AudioTsdfBeyond3G = 0;
        private int _AudioTsdfEdge = 0;
        private micromsg.BaseResponse _BaseResponse;
        private int _FastPlayRepair = 0;
        private int _Hw264SvrCfg = 0;
        private int _NetQualityCnt = 0;
        private readonly List<VoipNetQuality> _NetQualityList = new List<VoipNetQuality>();
        private int _NewP2S = 0;
        private int _PassthroughQosAlgorithm = 0;
        private VoipMultiRelayData _RelayData;
        private int _RoomId;
        private long _RoomKey;
        private int _RoomMemberID;
        private uint _SwitchInterval = 0;
        private uint _TcpCnt = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AddrCount", DataFormat=DataFormat.TwosComplement)]
        public int AddrCount
        {
            get
            {
                return this._AddrCount;
            }
            set
            {
                this._AddrCount = value;
            }
        }

        [ProtoMember(3, Name="AddrList", DataFormat=DataFormat.Default)]
        public List<VoipAddr> AddrList
        {
            get
            {
                return this._AddrList;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="AudioDTX", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AudioDTX
        {
            get
            {
                return this._AudioDTX;
            }
            set
            {
                this._AudioDTX = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="AudioEnableRmioAndS3A", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AudioEnableRmioAndS3A
        {
            get
            {
                return this._AudioEnableRmioAndS3A;
            }
            set
            {
                this._AudioEnableRmioAndS3A = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="AudioEnableSpkec", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AudioEnableSpkec
        {
            get
            {
                return this._AudioEnableSpkec;
            }
            set
            {
                this._AudioEnableSpkec = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="AudioTsdfBeyond3G", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AudioTsdfBeyond3G
        {
            get
            {
                return this._AudioTsdfBeyond3G;
            }
            set
            {
                this._AudioTsdfBeyond3G = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="AudioTsdfEdge", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AudioTsdfEdge
        {
            get
            {
                return this._AudioTsdfEdge;
            }
            set
            {
                this._AudioTsdfEdge = value;
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

        [ProtoMember(13, IsRequired=false, Name="FastPlayRepair", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int FastPlayRepair
        {
            get
            {
                return this._FastPlayRepair;
            }
            set
            {
                this._FastPlayRepair = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="Hw264SvrCfg", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Hw264SvrCfg
        {
            get
            {
                return this._Hw264SvrCfg;
            }
            set
            {
                this._Hw264SvrCfg = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="NetQualityCnt", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NetQualityCnt
        {
            get
            {
                return this._NetQualityCnt;
            }
            set
            {
                this._NetQualityCnt = value;
            }
        }

        [ProtoMember(9, Name="NetQualityList", DataFormat=DataFormat.Default)]
        public List<VoipNetQuality> NetQualityList
        {
            get
            {
                return this._NetQualityList;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="NewP2S", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NewP2S
        {
            get
            {
                return this._NewP2S;
            }
            set
            {
                this._NewP2S = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="PassthroughQosAlgorithm", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int PassthroughQosAlgorithm
        {
            get
            {
                return this._PassthroughQosAlgorithm;
            }
            set
            {
                this._PassthroughQosAlgorithm = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="RelayData", DataFormat=DataFormat.Default)]
        public VoipMultiRelayData RelayData
        {
            get
            {
                return this._RelayData;
            }
            set
            {
                this._RelayData = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
        public int RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
        public long RoomKey
        {
            get
            {
                return this._RoomKey;
            }
            set
            {
                this._RoomKey = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="RoomMemberID", DataFormat=DataFormat.TwosComplement)]
        public int RoomMemberID
        {
            get
            {
                return this._RoomMemberID;
            }
            set
            {
                this._RoomMemberID = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="SwitchInterval", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SwitchInterval
        {
            get
            {
                return this._SwitchInterval;
            }
            set
            {
                this._SwitchInterval = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="TcpCnt", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TcpCnt
        {
            get
            {
                return this._TcpCnt;
            }
            set
            {
                this._TcpCnt = value;
            }
        }
    }
}

