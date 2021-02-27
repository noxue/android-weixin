namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipCancelInviteReq")]
    public class VoipCancelInviteReq : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _FromUsername = "";
        private uint _InviteId = 0;
        private uint _InviteType = 0;
        private VoipStatReportData _ReportData;
        private int _RoomId;
        private long _RoomKey;
        private ulong _Timestamp64 = 0L;
        private string _ToUserName = "";
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

        [ProtoMember(2, IsRequired=false, Name="FromUsername", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="InviteId", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InviteId
        {
            get
            {
                return this._InviteId;
            }
            set
            {
                this._InviteId = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="InviteType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InviteType
        {
            get
            {
                return this._InviteType;
            }
            set
            {
                this._InviteType = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="ReportData", DataFormat=DataFormat.Default)]
        public VoipStatReportData ReportData
        {
            get
            {
                return this._ReportData;
            }
            set
            {
                this._ReportData = value;
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

        [ProtoMember(4, IsRequired=true, Name="RoomKey", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(10, IsRequired=false, Name="Timestamp64", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

        [ProtoMember(6, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUserName
        {
            get
            {
                return this._ToUserName;
            }
            set
            {
                this._ToUserName = value;
            }
        }
    }
}

