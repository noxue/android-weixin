namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [Serializable, ProtoContract(Name="VoipSpeedResultReq")]
    public class VoipSpeedResultReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private double _Latitude;
        private double _Longitude;
        private SKBuiltinString_t _NetName;
        private uint _NetType;
        private readonly List<SpeedResult> _Result = new List<SpeedResult>();
        private uint _ResultCnt;
        private uint _RoomId;
        private ulong _RoomKey;
        private ulong _TestId;
        private SKBuiltinString_t _WifiName;
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

        [ProtoMember(8, IsRequired=true, Name="Latitude", DataFormat=DataFormat.TwosComplement)]
        public double Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                this._Latitude = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="Longitude", DataFormat=DataFormat.TwosComplement)]
        public double Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                this._Longitude = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="NetName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t NetName
        {
            get
            {
                return this._NetName;
            }
            set
            {
                this._NetName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="NetType", DataFormat=DataFormat.TwosComplement)]
        public uint NetType
        {
            get
            {
                return this._NetType;
            }
            set
            {
                this._NetType = value;
            }
        }

        [ProtoMember(11, Name="Result", DataFormat=DataFormat.Default)]
        public List<SpeedResult> Result
        {
            get
            {
                return this._Result;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="ResultCnt", DataFormat=DataFormat.TwosComplement)]
        public uint ResultCnt
        {
            get
            {
                return this._ResultCnt;
            }
            set
            {
                this._ResultCnt = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="RoomId", DataFormat=DataFormat.TwosComplement)]
        public uint RoomId
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
        public ulong RoomKey
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

        [ProtoMember(2, IsRequired=true, Name="TestId", DataFormat=DataFormat.TwosComplement)]
        public ulong TestId
        {
            get
            {
                return this._TestId;
            }
            set
            {
                this._TestId = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="WifiName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t WifiName
        {
            get
            {
                return this._WifiName;
            }
            set
            {
                this._WifiName = value;
            }
        }
    }
}

