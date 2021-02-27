namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipAckReq")]
    public class VoipAckReq : IExtensible
    {
        private int _AckStatus = 0;
        private micromsg.BaseRequest _BaseRequest;
        private string _CallerName = "";
        private VoipRelayData _CapInfo = null;
        private string _FromUsername = "";
        private int _NetType = 0;
        private VoipRelayData _PeerId = null;
        private int _PreConnect = 0;
        private int _RoomId;
        private long _RoomKey;
        private ulong _Timestamp64 = 0L;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(10, IsRequired=false, Name="AckStatus", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AckStatus
        {
            get
            {
                return this._AckStatus;
            }
            set
            {
                this._AckStatus = value;
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

        [ProtoMember(9, IsRequired=false, Name="CallerName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CallerName
        {
            get
            {
                return this._CallerName;
            }
            set
            {
                this._CallerName = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="CapInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipRelayData CapInfo
        {
            get
            {
                return this._CapInfo;
            }
            set
            {
                this._CapInfo = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="FromUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUsername
        {
            get
            {
                return this._FromUsername;
            }
            set
            {
                this._FromUsername = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="NetType", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NetType
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

        [ProtoMember(6, IsRequired=false, Name="PeerId", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipRelayData PeerId
        {
            get
            {
                return this._PeerId;
            }
            set
            {
                this._PeerId = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PreConnect", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int PreConnect
        {
            get
            {
                return this._PreConnect;
            }
            set
            {
                this._PreConnect = value;
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

        [ProtoMember(11, IsRequired=false, Name="Timestamp64", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

