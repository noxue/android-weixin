namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SKBuiltinUint64_t")]
    public class SKBuiltinUint64_t : IExtensible
    {
        private ulong _ullVal;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ullVal", DataFormat=DataFormat.TwosComplement)]
        public ulong ullVal
        {
            get
            {
                return this._ullVal;
            }
            set
            {
                this._ullVal = value;
            }
        }
    }
}

