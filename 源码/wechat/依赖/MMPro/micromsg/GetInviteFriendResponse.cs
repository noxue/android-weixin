namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="GetInviteFriendResponse")]
    public class GetInviteFriendResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _FriendCount;
        private readonly List<InviteFriend> _FriendList = new List<InviteFriend>();
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

        [ProtoMember(2, IsRequired=true, Name="FriendCount", DataFormat=DataFormat.TwosComplement)]
        public uint FriendCount
        {
            get
            {
                return this._FriendCount;
            }
            set
            {
                this._FriendCount = value;
            }
        }

        [ProtoMember(3, Name="FriendList", DataFormat=DataFormat.Default)]
        public List<InviteFriend> FriendList
        {
            get
            {
                return this._FriendList;
            }
        }
    }
}

