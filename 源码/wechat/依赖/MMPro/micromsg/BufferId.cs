namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="BufferId")]
    public class BufferId : IExtensible
    {
        private ulong _MasterBufId;
        private ulong _SlaveBufId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="MasterBufId", DataFormat=DataFormat.TwosComplement)]
        public ulong MasterBufId
        {
            get
            {
                return this._MasterBufId;
            }
            set
            {
                this._MasterBufId = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="SlaveBufId", DataFormat=DataFormat.TwosComplement)]
        public ulong SlaveBufId
        {
            get
            {
                return this._SlaveBufId;
            }
            set
            {
                this._SlaveBufId = value;
            }
        }
    }
}

