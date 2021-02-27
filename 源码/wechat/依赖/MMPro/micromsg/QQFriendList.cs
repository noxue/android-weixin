namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="QQFriendList")]
    public class QQFriendList : IExtensible
    {
        private uint _Count;
        private uint _GroupID;
        private readonly List<QQFriendInGroup> _QQFriends = new List<QQFriendInGroup>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="GroupID", DataFormat=DataFormat.TwosComplement)]
        public uint GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                this._GroupID = value;
            }
        }

        [ProtoMember(3, Name="QQFriends", DataFormat=DataFormat.Default)]
        public List<QQFriendInGroup> QQFriends
        {
            get
            {
                return this._QQFriends;
            }
        }
    }
}

