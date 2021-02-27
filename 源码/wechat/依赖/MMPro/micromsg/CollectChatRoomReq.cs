namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="CollectChatRoomReq")]
    public class CollectChatRoomReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _GroupCardCount;
        private readonly List<GroupCardReq> _GroupCardList = new List<GroupCardReq>();
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

        [ProtoMember(2, IsRequired=true, Name="GroupCardCount", DataFormat=DataFormat.TwosComplement)]
        public uint GroupCardCount
        {
            get
            {
                return this._GroupCardCount;
            }
            set
            {
                this._GroupCardCount = value;
            }
        }

        [ProtoMember(3, Name="GroupCardList", DataFormat=DataFormat.Default)]
        public List<GroupCardReq> GroupCardList
        {
            get
            {
                return this._GroupCardList;
            }
        }
    }
}

