namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="NewAuthRsaReqData")]
    public class NewAuthRsaReqData : IExtensible
    {
        private SKBuiltinBuffer_t _RandomEncryKey;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="RandomEncryKey", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t RandomEncryKey
        {
            get
            {
                return this._RandomEncryKey;
            }
            set
            {
                this._RandomEncryKey = value;
            }
        }
    }
}

