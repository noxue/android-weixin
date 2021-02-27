namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="QuitChatRoom")]
    public class QuitChatRoom : IExtensible
    {
        private SKBuiltinString_t _ChatRoomName;
        private SKBuiltinString_t _UserName;
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

        [ProtoMember(2, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

