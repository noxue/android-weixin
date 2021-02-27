namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoiceRemindInfo")]
    public class VoiceRemindInfo : IExtensible
    {
        private uint _RemindId;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="RemindId", DataFormat=DataFormat.TwosComplement)]
        public uint RemindId
        {
            get
            {
                return this._RemindId;
            }
            set
            {
                this._RemindId = value;
            }
        }
    }
}

