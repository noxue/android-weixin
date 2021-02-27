namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="LbsResponse")]
    public class LbsResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _ContactCount;
        private readonly List<LbsContactInfo> _ContactList = new List<LbsContactInfo>();
        private uint _FlushTime = 0;
        private uint _IsShowRoom = 0;
        private uint _RoomMemberCount = 0;
        private uint _State = 0;
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

        [ProtoMember(2, IsRequired=true, Name="ContactCount", DataFormat=DataFormat.TwosComplement)]
        public uint ContactCount
        {
            get
            {
                return this._ContactCount;
            }
            set
            {
                this._ContactCount = value;
            }
        }

        [ProtoMember(3, Name="ContactList", DataFormat=DataFormat.Default)]
        public List<LbsContactInfo> ContactList
        {
            get
            {
                return this._ContactList;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="FlushTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FlushTime
        {
            get
            {
                return this._FlushTime;
            }
            set
            {
                this._FlushTime = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="IsShowRoom", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IsShowRoom
        {
            get
            {
                return this._IsShowRoom;
            }
            set
            {
                this._IsShowRoom = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="RoomMemberCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RoomMemberCount
        {
            get
            {
                return this._RoomMemberCount;
            }
            set
            {
                this._RoomMemberCount = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="State", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }
    }
}

