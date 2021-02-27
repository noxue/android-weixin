namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoipRedirectReq")]
    public class VoipRedirectReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private int _RoomId;
        private long _RoomKey;
        private int _RoomMemberId;
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
    }
}

