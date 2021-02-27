namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SKBuiltinDouble64_t")]
    public class SKBuiltinDouble64_t : IExtensible
    {
        private double _dVal;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="dVal", DataFormat=DataFormat.TwosComplement)]
        public double dVal
        {
            get
            {
                return this._dVal;
            }
            set
            {
                this._dVal = value;
            }
        }
    }
}

