namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="KeyBuffer")]
    public class KeyBuffer : IExtensible
    {
        private SKBuiltinString_t _synckey;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="synckey", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t synckey
        {
            get
            {
                return this._synckey;
            }
            set
            {
                this._synckey = value;
            }
        }
    }
}

