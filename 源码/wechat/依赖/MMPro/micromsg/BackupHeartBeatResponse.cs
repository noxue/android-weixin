namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="BackupHeartBeatResponse")]
    public class BackupHeartBeatResponse : IExtensible
    {
        private ulong _ack;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ack", DataFormat=DataFormat.TwosComplement)]
        public ulong ack
        {
            get
            {
                return this._ack;
            }
            set
            {
                this._ack = value;
            }
        }
    }
}

