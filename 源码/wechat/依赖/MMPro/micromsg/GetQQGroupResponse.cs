namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetQQGroupResponse")]
    public class GetQQGroupResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _OpType;
        private QQFriendList _QQFriend;
        private QQGroupList _QQGroup;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpType", DataFormat=DataFormat.TwosComplement)]
        public uint OpType
        {
            get
            {
                return this._OpType;
            }
            set
            {
                this._OpType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="QQFriend", DataFormat=DataFormat.Default)]
        public QQFriendList QQFriend
        {
            get
            {
                return this._QQFriend;
            }
            set
            {
                this._QQFriend = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="QQGroup", DataFormat=DataFormat.Default)]
        public QQGroupList QQGroup
        {
            get
            {
                return this._QQGroup;
            }
            set
            {
                this._QQGroup = value;
            }
        }
    }
}

