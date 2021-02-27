namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ExtSession")]
    public class ExtSession : IExtensible
    {
        private SKBuiltinBuffer_t _ServerId;
        private SKBuiltinBuffer_t _SessionKey;
        private uint _SessionType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="ServerId", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ServerId
        {
            get
            {
                return this._ServerId;
            }
            set
            {
                this._ServerId = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="SessionKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SessionKey
        {
            get
            {
                return this._SessionKey;
            }
            set
            {
                this._SessionKey = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="SessionType", DataFormat=DataFormat.TwosComplement)]
        public uint SessionType
        {
            get
            {
                return this._SessionType;
            }
            set
            {
                this._SessionType = value;
            }
        }
    }
}

