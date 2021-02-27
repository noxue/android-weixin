namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="BuiltinIPList")]
    public class BuiltinIPList : IExtensible
    {
        private uint _LongConnectIPCount;
        private readonly List<BuiltinIP> _LongConnectIPList = new List<BuiltinIP>();
        private uint _Seq;
        private uint _ShortConnectIPCount;
        private readonly List<BuiltinIP> _ShortConnectIPList = new List<BuiltinIP>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="LongConnectIPCount", DataFormat=DataFormat.TwosComplement)]
        public uint LongConnectIPCount
        {
            get
            {
                return this._LongConnectIPCount;
            }
            set
            {
                this._LongConnectIPCount = value;
            }
        }

        [ProtoMember(3, Name="LongConnectIPList", DataFormat=DataFormat.Default)]
        public List<BuiltinIP> LongConnectIPList
        {
            get
            {
                return this._LongConnectIPList;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Seq", DataFormat=DataFormat.TwosComplement)]
        public uint Seq
        {
            get
            {
                return this._Seq;
            }
            set
            {
                this._Seq = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ShortConnectIPCount", DataFormat=DataFormat.TwosComplement)]
        public uint ShortConnectIPCount
        {
            get
            {
                return this._ShortConnectIPCount;
            }
            set
            {
                this._ShortConnectIPCount = value;
            }
        }

        [ProtoMember(4, Name="ShortConnectIPList", DataFormat=DataFormat.Default)]
        public List<BuiltinIP> ShortConnectIPList
        {
            get
            {
                return this._ShortConnectIPList;
            }
        }
    }
}

