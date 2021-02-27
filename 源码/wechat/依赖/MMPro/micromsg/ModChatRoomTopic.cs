namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ModChatRoomTopic")]
    public class ModChatRoomTopic : IExtensible
    {
        private SKBuiltinString_t _ChatRoomName;
        private SKBuiltinString_t _ChatRoomTopic;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="ChatRoomName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ChatRoomName
        {
            get
            {
                return this._ChatRoomName;
            }
            set
            {
                this._ChatRoomName = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="ChatRoomTopic", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ChatRoomTopic
        {
            get
            {
                return this._ChatRoomTopic;
            }
            set
            {
                this._ChatRoomTopic = value;
            }
        }
    }
}

