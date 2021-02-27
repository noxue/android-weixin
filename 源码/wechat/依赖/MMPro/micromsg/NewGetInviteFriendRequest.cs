namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="NewGetInviteFriendRequest")]
    public class NewGetInviteFriendRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _FriendType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="FriendType", DataFormat=DataFormat.TwosComplement)]
        public uint FriendType
        {
            get
            {
                return this._FriendType;
            }
            set
            {
                this._FriendType = value;
            }
        }
    }
}

