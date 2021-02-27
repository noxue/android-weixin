namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SyncKey")]
    public class SyncKey : IExtensible
    {
        private readonly List<KeyVal> _Key = new List<KeyVal>();
        private uint _KeyCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, Name="Key", DataFormat=DataFormat.Default)]
        public List<KeyVal> Key
        {
            get
            {
                return this._Key;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="KeyCount", DataFormat=DataFormat.TwosComplement)]
        public uint KeyCount
        {
            get
            {
                return this._KeyCount;
            }
            set
            {
                this._KeyCount = value;
            }
        }
    }
}

