namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SKBuiltinInt32_t")]
    public class SKBuiltinInt32_t : IExtensible
    {
        private uint _iVal;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="iVal", DataFormat=DataFormat.TwosComplement)]
        public uint iVal
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

