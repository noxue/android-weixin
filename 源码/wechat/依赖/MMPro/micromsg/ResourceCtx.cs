namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ResourceCtx")]
    public class ResourceCtx : IExtensible
    {
        private SKBuiltinBuffer_t _ResData;
        private uint _ResId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="ResData", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ResData
        {
            get
            {
                return this._ResData;
            }
            set
            {
                this._ResData = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ResId", DataFormat=DataFormat.TwosComplement)]
        public uint ResId
        {
            get
            {
                return this._ResId;
            }
            set
            {
                this._ResId = value;
            }
        }
    }
}

