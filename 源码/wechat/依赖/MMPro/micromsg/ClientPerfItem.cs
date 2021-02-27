namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ClientPerfItem")]
    public class ClientPerfItem : IExtensible
    {
        private uint _AvgElapseTime;
        private uint _Cnt;
        private uint _EndTime;
        private uint _EventID;
        private uint _MaxElapseTime;
        private uint _MinElapseTime;
        private uint _StartTime;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AvgElapseTime", DataFormat=DataFormat.TwosComplement)]
        public uint AvgElapseTime
        {
            get
            {
                return this._AvgElapseTime;
            }
            set
            {
                this._AvgElapseTime = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Cnt", DataFormat=DataFormat.TwosComplement)]
        public uint Cnt
        {
            get
            {
                return this._Cnt;
            }
            set
            {
                this._Cnt = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="EndTime", DataFormat=DataFormat.TwosComplement)]
        public uint EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                this._EndTime = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="EventID", DataFormat=DataFormat.TwosComplement)]
        public uint EventID
        {
            get
            {
                return this._EventID;
            }
            set
            {
                this._EventID = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="MaxElapseTime", DataFormat=DataFormat.TwosComplement)]
        public uint MaxElapseTime
        {
            get
            {
                return this._MaxElapseTime;
            }
            set
            {
                this._MaxElapseTime = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="MinElapseTime", DataFormat=DataFormat.TwosComplement)]
        public uint MinElapseTime
        {
            get
            {
                return this._MinElapseTime;
            }
            set
            {
                this._MinElapseTime = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="StartTime", DataFormat=DataFormat.TwosComplement)]
        public uint StartTime
        {
            get
            {
                return this._StartTime;
            }
            set
            {
                this._StartTime = value;
            }
        }
    }
}

