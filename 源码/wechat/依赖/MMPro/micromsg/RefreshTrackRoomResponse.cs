namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RefreshTrackRoomResponse")]
    public class RefreshTrackRoomResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Count;
        private readonly List<UserPositionItem> _Positions = new List<UserPositionItem>();
        private uint _RefreshTime = 0;
        private string _RetMsg = "";
        private TrackPOIItem _RoomPoi = null;
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

        [ProtoMember(2, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
        public uint Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        [ProtoMember(3, Name="Positions", DataFormat=DataFormat.Default)]
        public List<UserPositionItem> Positions
        {
            get
            {
                return this._Positions;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="RefreshTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RefreshTime
        {
            get
            {
                return this._RefreshTime;
            }
            set
            {
                this._RefreshTime = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="RetMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string RetMsg
        {
            get
            {
                return this._RetMsg;
            }
            set
            {
                this._RetMsg = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="RoomPoi", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public TrackPOIItem RoomPoi
        {
            get
            {
                return this._RoomPoi;
            }
            set
            {
                this._RoomPoi = value;
            }
        }
    }
}

