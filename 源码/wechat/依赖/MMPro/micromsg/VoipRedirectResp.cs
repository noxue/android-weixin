namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipRedirectResp")]
    public class VoipRedirectResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private int _PunchAddrCount;
        private readonly List<VoipAddr> _PunchAddrList = new List<VoipAddr>();
        private int _RelayAddrCount;
        private readonly List<VoipAddr> _RelayAddrList = new List<VoipAddr>();
        private int _RoomId;
        private long _RoomKey;
        private int _RoomMemberId;
        private int _TcpAddrCount = 0;
        private readonly List<VoipAddr> _TcpAddrList = new List<VoipAddr>();
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

        [ProtoMember(5, IsRequired=true, Name="PunchAddrCount", DataFormat=DataFormat.TwosComplement)]
        public int PunchAddrCount
        {
            get
            {
                return this._PunchAddrCount;
            }
            set
            {
                this._PunchAddrCount = value;
            }
        }

        [ProtoMember(6, Name="PunchAddrList", DataFormat=DataFormat.Default)]
        public List<VoipAddr> PunchAddrList
        {
            get
            {
                return this._PunchAddrList;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="RelayAddrCount", DataFormat=DataFormat.TwosComplement)]
        public int RelayAddrCount
        {
            get
            {
                return this._RelayAddrCount;
            }
            set
            {
                this._RelayAddrCount = value;
            }
        }

        [ProtoMember(4, Name="RelayAddrList", DataFormat=DataFormat.Default)]
        public List<VoipAddr> RelayAddrList
        {
            get
            {
                return this._RelayAddrList;
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

        [ProtoMember(7, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(8, IsRequired=true, Name="RoomMemberId", DataFormat=DataFormat.TwosComplement)]
        public int RoomMemberId
        {
            get
            {
                return this._RoomMemberId;
            }
            set
            {
                this._RoomMemberId = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="TcpAddrCount", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int TcpAddrCount
        {
            get
            {
                return this._TcpAddrCount;
            }
            set
            {
                this._TcpAddrCount = value;
            }
        }

        [ProtoMember(10, Name="TcpAddrList", DataFormat=DataFormat.Default)]
        public List<VoipAddr> TcpAddrList
        {
            get
            {
                return this._TcpAddrList;
            }
        }
    }
}

