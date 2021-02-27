namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ChatRoomMemberData")]
    public class ChatRoomMemberData : IExtensible
    {
        private readonly List<ChatRoomMemberInfo> _ChatRoomMember = new List<ChatRoomMemberInfo>();
        private uint _InfoMask = 0;
        private uint _MemberCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, Name="ChatRoomMember", DataFormat=DataFormat.Default)]
        public List<ChatRoomMemberInfo> ChatRoomMember
        {
            get
            {
                return this._ChatRoomMember;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="InfoMask", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InfoMask
        {
            get
            {
                return this._InfoMask;
            }
            set
            {
                this._InfoMask = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="MemberCount", DataFormat=DataFormat.TwosComplement)]
        public uint MemberCount
        {
            get
            {
                return this._MemberCount;
            }
            set
            {
                this._MemberCount = value;
            }
        }
    }
}

