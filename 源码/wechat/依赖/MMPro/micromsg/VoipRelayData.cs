namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipRelayData")]
    public class VoipRelayData : IExtensible
    {
        private SKBuiltinBuffer_t _Buffer;
        private int _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Buffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

