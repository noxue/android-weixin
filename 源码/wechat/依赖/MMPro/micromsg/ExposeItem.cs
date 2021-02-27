namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="ExposeItem")]
    public class ExposeItem : IExtensible
    {
        private uint _Count;
        private readonly List<uint> _CountList = new List<uint>();
        private readonly List<uint> _TimeList = new List<uint>();
        private readonly List<uint> _UinList = new List<uint>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(4, Name="CountList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> CountList
        {
            get
            {
                return this._CountList;
            }
        }

        [ProtoMember(3, Name="TimeList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> TimeList
        {
            get
            {
                return this._TimeList;
            }
        }

        [ProtoMember(2, Name="UinList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> UinList
        {
            get
            {
                return this._UinList;
            }
        }
    }
}

