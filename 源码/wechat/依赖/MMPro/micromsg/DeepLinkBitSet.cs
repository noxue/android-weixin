namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="DeepLinkBitSet")]
    public class DeepLinkBitSet : IExtensible
    {
        private ulong _BitValue;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BitValue", DataFormat=DataFormat.TwosComplement)]
        public ulong BitValue
        {
            get
            {
                return this._BitValue;
            }
            set
            {
                this._BitValue = value;
            }
        }
    }
}

