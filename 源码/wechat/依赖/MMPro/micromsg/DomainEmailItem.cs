namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="DomainEmailItem")]
    public class DomainEmailItem : IExtensible
    {
        private SKBuiltinString_t _Email;
        private uint _Status;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Email", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
    }
}

