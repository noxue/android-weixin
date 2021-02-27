namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="RemindSyncRequest")]
    public class RemindSyncRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _KeyBuff;
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

        [ProtoMember(3, IsRequired=true, Name="KeyBuff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuff
        {
            get
            {
                return this._KeyBuff;
            }
            set
            {
                this._KeyBuff = value;
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

