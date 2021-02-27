namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UpgradeChatRoomResp")]
    public class UpgradeChatRoomResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _CardQuota;
        private string _ChatRoomData = "";
        private uint _DonateQuota;
        private uint _MaxCount = 0;
        private uint _MobileQuota;
        private string _ResultMsg = "";
        private uint _TotalQuota = 0;
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

        [ProtoMember(6, IsRequired=true, Name="CardQuota", DataFormat=DataFormat.TwosComplement)]
        public uint CardQuota
        {
            get
            {
                return this._CardQuota;
            }
            set
            {
                this._CardQuota = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ChatRoomData", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomData
        {
            get
            {
                return this._ChatRoomData;
            }
            set
            {
                this._ChatRoomData = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="DonateQuota", DataFormat=DataFormat.TwosComplement)]
        public uint DonateQuota
        {
            get
            {
                return this._DonateQuota;
            }
            set
            {
                this._DonateQuota = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="MaxCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MaxCount
        {
            get
            {
                return this._MaxCount;
            }
            set
            {
                this._MaxCount = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="MobileQuota", DataFormat=DataFormat.TwosComplement)]
        public uint MobileQuota
        {
            get
            {
                return this._MobileQuota;
            }
            set
            {
                this._MobileQuota = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ResultMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ResultMsg
        {
            get
            {
                return this._ResultMsg;
            }
            set
            {
                this._ResultMsg = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="TotalQuota", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TotalQuota
        {
            get
            {
                return this._TotalQuota;
            }
            set
            {
                this._TotalQuota = value;
            }
        }
    }
}

