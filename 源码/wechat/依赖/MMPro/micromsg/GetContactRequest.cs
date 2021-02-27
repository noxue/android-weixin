namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetContactRequest")]
    public class GetContactRequest : IExtensible
    {
        private readonly List<SKBuiltinString_t> _AntispamTicket = new List<SKBuiltinString_t>();
        private uint _AntispamTicketCount = 0;
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<SKBuiltinString_t> _FromChatRoom = new List<SKBuiltinString_t>();
        private uint _FromChatRoomCount = 0;
        private uint _UserCount;
        private readonly List<SKBuiltinString_t> _UserNameList = new List<SKBuiltinString_t>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, Name="AntispamTicket", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> AntispamTicket
        {
            get
            {
                return this._AntispamTicket;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="AntispamTicketCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AntispamTicketCount
        {
            get
            {
                return this._AntispamTicketCount;
            }
            set
            {
                this._AntispamTicketCount = value;
            }
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

        [ProtoMember(7, Name="FromChatRoom", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> FromChatRoom
        {
            get
            {
                return this._FromChatRoom;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="FromChatRoomCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FromChatRoomCount
        {
            get
            {
                return this._FromChatRoomCount;
            }
            set
            {
                this._FromChatRoomCount = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="UserCount", DataFormat=DataFormat.TwosComplement)]
        public uint UserCount
        {
            get
            {
                return this._UserCount;
            }
            set
            {
                this._UserCount = value;
            }
        }

        [ProtoMember(3, Name="UserNameList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> UserNameList
        {
            get
            {
                return this._UserNameList;
            }
        }
    }
}

