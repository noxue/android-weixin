namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="UpdateStatOpLog")]
    public class UpdateStatOpLog : IExtensible
    {
        private uint _OpCode;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }
    }
}

