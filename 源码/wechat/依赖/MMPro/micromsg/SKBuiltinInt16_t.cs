namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SKBuiltinInt16_t")]
    public class SKBuiltinInt16_t : IExtensible
    {
        private int _iVal;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="iVal", DataFormat=DataFormat.TwosComplement)]
        public int iVal
        {
            get
            {
                return this._iVal;
            }
            set
            {
                this._iVal = value;
            }
        }
    }
}

