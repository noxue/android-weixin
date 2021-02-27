namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EnterTalkRoomResp")]
    public class EnterTalkRoomResp : IExtensible
    {
        private int _AddrCount = 0;
        private readonly List<TalkRelayAddr> _AddrList = new List<TalkRelayAddr>();
        private micromsg.BaseResponse _BaseResponse;
        private readonly List<TalkRoomMember> _MemberList = new List<TalkRoomMember>();
        private int _MemberNum;
        private int _MicSeq;
        private int _MyRoomMemberId;
        private int _RoomId;
        private long _RoomKey;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(8, IsRequired=false, Name="AddrCount", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AddrCount
        {
            get
            {
                return this._AddrCount;
            }
            set
            {
                this._AddrCount = value;
            }
        }

        [ProtoMember(9, Name="AddrList", DataFormat=DataFormat.Default)]
        public List<TalkRelayAddr> AddrList
        {
            get
            {
                return this._AddrList;
            }
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

        [ProtoMember(6, Name="MemberList", DataFormat=DataFormat.Default)]
        public List<TalkRoomMember> MemberList
        {
            get
            {
                return this._MemberList;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="MemberNum", DataFormat=DataFormat.TwosComplement)]
        public int MemberNum
        {
            get
            {
                return this._MemberNum;
            }
            set
            {
                this._MemberNum = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="MicSeq", DataFormat=DataFormat.TwosComplement)]
        public int MicSeq
        {
            get
            {
                return this._MicSeq;
            }
            set
            {
                this._MicSeq = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="MyRoomMemberId", DataFormat=DataFormat.TwosComplement)]
        public int MyRoomMemberId
        {
            get
            {
                return this._MyRoomMemberId;
            }
            set
            {
                this._MyRoomMemberId = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
        public int RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
        public long RoomKey
        {
            get
            {
                return this._RoomKey;
            }
            set
            {
                this._RoomKey = value;
            }
        }
    }
}

