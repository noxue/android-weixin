namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ModTXNewsCategory")]
    public class ModTXNewsCategory : IExtensible
    {
        private uint _TXNewsCategory;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="TXNewsCategory", DataFormat=DataFormat.TwosComplement)]
        public uint TXNewsCategory
        {
            get
            {
                return this._TXNewsCategory;
            }
            set
            {
                this._TXNewsCategory = value;
            }
        }
    }
}

