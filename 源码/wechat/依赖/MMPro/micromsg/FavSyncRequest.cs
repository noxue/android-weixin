namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="FavSyncRequest")]
    public class FavSyncRequest : IExtensible
    {
        private SKBuiltinBuffer_t _KeyBuf;
        private uint _Selector;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="KeyBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuf
        {
            get
            {
                return this._KeyBuf;
            }
            set
            {
                this._KeyBuf = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Selector", DataFormat=DataFormat.TwosComplement)]
        public uint Selector
        {
            get
            {
                return this._Selector;
            }
            set
            {
                this._Selector = value;
            }
        }
    }
}

