namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SpeedTestSvr")]
    public class SpeedTestSvr : IExtensible
    {
        private VoipAddr _Addr;
        private uint _PktSize;
        private uint _TestCnt;
        private uint _TestGap;
        private uint _Timeout;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Addr", DataFormat=DataFormat.Default)]
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

        [ProtoMember(5, IsRequired=true, Name="PktSize", DataFormat=DataFormat.TwosComplement)]
        public uint PktSize
        {
            get
            {
                return this._PktSize;
            }
            set
            {
                this._PktSize = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="TestCnt", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=true, Name="TestGap", DataFormat=DataFormat.TwosComplement)]
        public uint TestGap
        {
            get
            {
                return this._TestGap;
            }
            set
            {
                this._TestGap = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Timeout", DataFormat=DataFormat.TwosComplement)]
        public uint Timeout
        {
            get
            {
                return this._Timeout;
            }
            set
            {
                this._Timeout = value;
            }
        }
    }
}

