namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SpeedResult")]
    public class SpeedResult : IExtensible
    {
        private VoipAddr _Addr;
        private uint _AvgRtt;
        private uint _ClientIp;
        private readonly List<uint> _DownSeq = new List<uint>();
        private uint _DownSeqCnt;
        private uint _MaxRtt;
        private uint _MinRtt;
        private readonly List<uint> _Rtt = new List<uint>();
        private uint _RttCnt;
        private uint _TestCnt;
        private uint _TestPktSize;
        private readonly List<uint> _UpSeq = new List<uint>();
        private uint _UpSeqCnt;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="Addr", DataFormat=DataFormat.Default)]
        public VoipAddr Addr
        {
            get
            {
                return this._Addr;
            }
            set
            {
                this._Addr = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="AvgRtt", DataFormat=DataFormat.TwosComplement)]
        public uint AvgRtt
        {
            get
            {
                return this._AvgRtt;
            }
            set
            {
                this._AvgRtt = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ClientIp", DataFormat=DataFormat.TwosComplement)]
        public uint ClientIp
        {
            get
            {
                return this._ClientIp;
            }
            set
            {
                this._ClientIp = value;
            }
        }

        [ProtoMember(11, Name="DownSeq", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> DownSeq
        {
            get
            {
                return this._DownSeq;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="DownSeqCnt", DataFormat=DataFormat.TwosComplement)]
        public uint DownSeqCnt
        {
            get
            {
                return this._DownSeqCnt;
            }
            set
            {
                this._DownSeqCnt = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="MaxRtt", DataFormat=DataFormat.TwosComplement)]
        public uint MaxRtt
        {
            get
            {
                return this._MaxRtt;
            }
            set
            {
                this._MaxRtt = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="MinRtt", DataFormat=DataFormat.TwosComplement)]
        public uint MinRtt
        {
            get
            {
                return this._MinRtt;
            }
            set
            {
                this._MinRtt = value;
            }
        }

        [ProtoMember(9, Name="Rtt", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> Rtt
        {
            get
            {
                return this._Rtt;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="RttCnt", DataFormat=DataFormat.TwosComplement)]
        public uint RttCnt
        {
            get
            {
                return this._RttCnt;
            }
            set
            {
                this._RttCnt = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TestCnt", DataFormat=DataFormat.TwosComplement)]
        public uint TestCnt
        {
            get
            {
                return this._TestCnt;
            }
            set
            {
                this._TestCnt = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="TestPktSize", DataFormat=DataFormat.TwosComplement)]
        public uint TestPktSize
        {
            get
            {
                return this._TestPktSize;
            }
            set
            {
                this._TestPktSize = value;
            }
        }

        [ProtoMember(13, Name="UpSeq", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> UpSeq
        {
            get
            {
                return this._UpSeq;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="UpSeqCnt", DataFormat=DataFormat.TwosComplement)]
        public uint UpSeqCnt
        {
            get
            {
                return this._UpSeqCnt;
            }
            set
            {
                this._UpSeqCnt = value;
            }
        }
    }
}

