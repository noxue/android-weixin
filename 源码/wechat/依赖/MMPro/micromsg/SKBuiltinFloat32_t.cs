namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SKBuiltinFloat32_t")]
    public class SKBuiltinFloat32_t : IExtensible
    {
        private float _fVal;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="fVal", DataFormat=DataFormat.FixedSize)]
        public float fVal
        {
            get
            {
                return this._fVal;
            }
            set
            {
                this._fVal = value;
            }
        }
    }
}

