namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GeneralShareBaseInfo")]
    public class GeneralShareBaseInfo : IExtensible
    {
        private uint _DestType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="DestType", DataFormat=DataFormat.TwosComplement)]
        public uint DestType
        {
            get
            {
                return this._DestType;
            }
            set
            {
                this._DestType = value;
            }
        }
    }
}

