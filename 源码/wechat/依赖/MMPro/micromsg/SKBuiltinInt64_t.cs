namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SKBuiltinInt64_t")]
    public class SKBuiltinInt64_t : IExtensible
    {
        private long _llVal;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="llVal", DataFormat=DataFormat.TwosComplement)]
        public long llVal
        {
            get
            {
                return this._llVal;
            }
            set
            {
                this._llVal = value;
            }
        }
    }
}

