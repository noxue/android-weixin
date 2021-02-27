namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="KeyVal")]
    public class KeyVal : IExtensible
    {
        private uint _Key;
        private uint _Val;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="Key", DataFormat=DataFormat.TwosComplement)]
        public uint Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Val", DataFormat=DataFormat.TwosComplement)]
        public uint Val
        {
            get
            {
                return this._Val;
            }
            set
            {
                this._Val = value;
            }
        }
    }
}

