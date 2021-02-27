namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipHeartBeatReq")]
    public class VoipHeartBeatReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private int _ConnectionType = 0;
        private int _RoomId;
        private long _RoomKey;
        private uint _Timestamp = 0;
        private ulong _Timestamp64 = 0L;
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

        [ProtoMember(6, IsRequired=false, Name="ConnectionType", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ConnectionType
        {
            get
            {
                return this._ConnectionType;
            }
            set
            {
                this._ConnectionType = value;
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

        [ProtoMember(4, IsRequired=false, Name="Timestamp", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Timestamp
        {
            get
            {
                return this._Timestamp;
            }
            set
            {
                this._Timestamp = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Timestamp64", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong Timestamp64
        {
            get
            {
                return this._Timestamp64;
            }
            set
            {
                this._Timestamp64 = value;
            }
        }
    }
}

