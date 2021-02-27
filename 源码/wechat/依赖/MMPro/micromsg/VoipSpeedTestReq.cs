namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="VoipSpeedTestReq")]
    public class VoipSpeedTestReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _C2CLossrate;
        private readonly List<uint> _C2CRtt = new List<uint>();
        private uint _C2CRttCnt;
        private uint _C2SDownLossRate;
        private readonly List<uint> _C2SRtt = new List<uint>();
        private uint _C2SRttCnt;
        private uint _CallType;
        private uint _IsConnected;
        private uint _IsConnecting;
        private uint _NetType;
        private uint _RcvPkts;
        private int _RoomId;
        private uint _SendPkts;
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

        [ProtoMember(7, IsRequired=true, Name="C2CLossrate", DataFormat=DataFormat.TwosComplement)]
        public uint C2CLossrate
        {
            get
            {
                return this._C2CLossrate;
            }
            set
            {
                this._C2CLossrate = value;
            }
        }

        [ProtoMember(9, Name="C2CRtt", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> C2CRtt
        {
            get
            {
                return this._C2CRtt;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="C2CRttCnt", DataFormat=DataFormat.TwosComplement)]
        public uint C2CRttCnt
        {
            get
            {
                return this._C2CRttCnt;
            }
            set
            {
                this._C2CRttCnt = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="C2SDownLossRate", DataFormat=DataFormat.TwosComplement)]
        public uint C2SDownLossRate
        {
            get
            {
                return this._C2SDownLossRate;
            }
            set
            {
                this._C2SDownLossRate = value;
            }
        }

        [ProtoMember(12, Name="C2SRtt", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> C2SRtt
        {
            get
            {
                return this._C2SRtt;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="C2SRttCnt", DataFormat=DataFormat.TwosComplement)]
        public uint C2SRttCnt
        {
            get
            {
                return this._C2SRttCnt;
            }
            set
            {
                this._C2SRttCnt = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="CallType", DataFormat=DataFormat.TwosComplement)]
        public uint CallType
        {
            get
            {
                return this._CallType;
            }
            set
            {
                this._CallType = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="IsConnected", DataFormat=DataFormat.TwosComplement)]
        public uint IsConnected
        {
            get
            {
                return this._IsConnected;
            }
            set
            {
                this._IsConnected = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="IsConnecting", DataFormat=DataFormat.TwosComplement)]
        public uint IsConnecting
        {
            get
            {
                return this._IsConnecting;
            }
            set
            {
                this._IsConnecting = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="NetType", DataFormat=DataFormat.TwosComplement)]
        public uint NetType
        {
            get
            {
                return this._NetType;
            }
            set
            {
                this._NetType = value;
            }
        }

        [ProtoMember(14, IsRequired=true, Name="RcvPkts", DataFormat=DataFormat.TwosComplement)]
        public uint RcvPkts
        {
            get
            {
                return this._RcvPkts;
            }
            set
            {
                this._RcvPkts = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(13, IsRequired=true, Name="SendPkts", DataFormat=DataFormat.TwosComplement)]
        public uint SendPkts
        {
            get
            {
                return this._SendPkts;
            }
            set
            {
                this._SendPkts = value;
            }
        }
    }
}

