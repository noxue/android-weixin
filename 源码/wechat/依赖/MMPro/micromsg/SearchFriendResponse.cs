namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="SearchFriendResponse")]
    public class SearchFriendResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _FriendCount;
        private readonly List<FriendInfo> _FriendList = new List<FriendInfo>();
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
        public List<FriendInfo> FriendList
        {
            get
            {
                return this._FriendList;
            }
        }
    }
}

