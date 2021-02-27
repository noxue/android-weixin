namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="NewGetInviteFriendResponse")]
    public class NewGetInviteFriendResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _FriendCount;
        private readonly List<NewInviteFriend> _FriendList = new List<NewInviteFriend>();
        private uint _GroupCount;
        private readonly List<FriendGroup> _GroupList = new List<FriendGroup>();
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
        public List<NewInviteFriend> FriendList
        {
            get
            {
                return this._FriendList;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="GroupCount", DataFormat=DataFormat.TwosComplement)]
        public uint GroupCount
        {
            get
            {
                return this._GroupCount;
            }
            set
            {
                this._GroupCount = value;
            }
        }

        [ProtoMember(5, Name="GroupList", DataFormat=DataFormat.Default)]
        public List<FriendGroup> GroupList
        {
            get
            {
                return this._GroupList;
            }
        }
    }
}

