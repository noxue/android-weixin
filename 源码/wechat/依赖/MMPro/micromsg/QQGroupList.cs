namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="QQGroupList")]
    public class QQGroupList : IExtensible
    {
        private uint _Count;
        private readonly List<QQGroup> _QQGroups = new List<QQGroup>();
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

        [ProtoMember(2, Name="QQGroups", DataFormat=DataFormat.Default)]
        public List<QQGroup> QQGroups
        {
            get
            {
                return this._QQGroups;
            }
        }
    }
}

