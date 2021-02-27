namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipSyncResp")]
    public class VoipSyncResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private VoipCmdList _CmdList;
        private int _ContinueFlag;
        private SKBuiltinBuffer_t _KeyBuf;
        private int _RoomId;
        private long _RoomKey;
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

        [ProtoMember(5, IsRequired=true, Name="CmdList", DataFormat=DataFormat.Default)]
        public VoipCmdList CmdList
        {
            get
            {
                return this._CmdList;
            }
            set
            {
                this._CmdList = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="ContinueFlag", DataFormat=DataFormat.TwosComplement)]
        public int ContinueFlag
        {
            get
            {
                return this._ContinueFlag;
            }
            set
            {
                this._ContinueFlag = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="KeyBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KeyBuf
        {
            get
            {
                return this._KeyBuf;
            }
            set
            {
                this._KeyBuf = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(8, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
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

