namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipAckResp")]
    public class VoipAckResp : IExtensible
    {
        private int _AudioEnableRmioAndS3A = 0;
        private micromsg.BaseResponse _BaseResponse;
        private int _PreConnect = 0;
        private VoipMultiRelayData _RelayData = null;
        private int _RoomId;
        private long _RoomKey;
        private int _RoomMemberID = 0;
        private uint _TcpCnt = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(8, IsRequired=false, Name="AudioEnableRmioAndS3A", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int AudioEnableRmioAndS3A
        {
            get
            {
                return this._AudioEnableRmioAndS3A;
            }
            set
            {
                this._AudioEnableRmioAndS3A = value;
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

        [ProtoMember(6, IsRequired=false, Name="PreConnect", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(5, IsRequired=false, Name="RelayData", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public VoipMultiRelayData RelayData
        {
            get
            {
                return this._RelayData;
            }
            set
            {
                this._RelayData = value;
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

        [ProtoMember(4, IsRequired=false, Name="RoomMemberID", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int RoomMemberID
        {
            get
            {
                return this._RoomMemberID;
            }
            set
            {
                this._RoomMemberID = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="TcpCnt", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TcpCnt
        {
            get
            {
                return this._TcpCnt;
            }
            set
            {
                this._TcpCnt = value;
            }
        }
    }
}

