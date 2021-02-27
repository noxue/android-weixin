namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="CardSyncRequest")]
    public class CardSyncRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _KeyBuf;
        private uint _Selector;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
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

        [ProtoMember(2, IsRequired=true, Name="Selector", DataFormat=DataFormat.TwosComplement)]
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

