namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="RecommendGroup")]
    public class RecommendGroup : IExtensible
    {
        private SKBuiltinString_t _GroupName;
        private readonly List<SearchOrRecommendItem> _Members = new List<SearchOrRecommendItem>();
        private uint _MemCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="GroupName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                this._GroupName = value;
            }
        }

        [ProtoMember(3, Name="Members", DataFormat=DataFormat.Default)]
        public List<SearchOrRecommendItem> Members
        {
            get
            {
                return this._Members;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="MemCount", DataFormat=DataFormat.TwosComplement)]
        public uint MemCount
        {
            get
            {
                return this._MemCount;
            }
            set
            {
                this._MemCount = value;
            }
        }
    }
}

