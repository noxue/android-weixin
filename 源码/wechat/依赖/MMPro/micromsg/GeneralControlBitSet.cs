namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GeneralControlBitSet")]
    public class GeneralControlBitSet : IExtensible
    {
        private uint _BitValue;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BitValue", DataFormat=DataFormat.TwosComplement)]
        public uint BitValue
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

