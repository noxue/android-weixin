namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="InitBuffer")]
    public class InitBuffer : IExtensible
    {
        private uint _MaxSyncKey;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="MaxSyncKey", DataFormat=DataFormat.TwosComplement)]
        public uint MaxSyncKey
        {
            get
            {
                return this._MaxSyncKey;
            }
            set
            {
                this._MaxSyncKey = value;
            }
        }
    }
}

