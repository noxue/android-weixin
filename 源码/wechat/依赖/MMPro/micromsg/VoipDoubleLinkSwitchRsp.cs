namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipDoubleLinkSwitchRsp")]
    public class VoipDoubleLinkSwitchRsp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private int _ReportFlag;
        private int _RoomId;
        private long _RoomKey;
        private int _RoomMemberId;
        private int _SwitchToLinkType;
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

        [ProtoMember(5, IsRequired=true, Name="ReportFlag", DataFormat=DataFormat.TwosComplement)]
        public int ReportFlag
        {
            get
            {
                return this._ReportFlag;
            }
            set
            {
                this._ReportFlag = value;
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

        [ProtoMember(4, IsRequired=true, Name="RoomMemberId", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=true, Name="SwitchToLinkType", DataFormat=DataFormat.TwosComplement)]
        public int SwitchToLinkType
        {
            get
            {
                return this._SwitchToLinkType;
            }
            set
            {
                this._SwitchToLinkType = value;
            }
        }
    }
}

