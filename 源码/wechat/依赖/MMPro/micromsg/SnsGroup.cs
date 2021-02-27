namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="SnsGroup")]
    public class SnsGroup : IExtensible
    {
        private ulong _GroupId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="GroupId", DataFormat=DataFormat.TwosComplement)]
        public ulong GroupId
        {
            get
            {
                return this._GroupId;
            }
            set
            {
                this._GroupId = value;
            }
        }
    }
}

