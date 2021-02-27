namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DownloadVoiceResponse")]
    public class DownloadVoiceResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _CancelFlag = 0;
        private string _ClientMsgId = "";
        private SKBuiltinBuffer_t _Data;
        private uint _EndFlag;
        private uint _Length;
        private uint _MsgId;
        private ulong _NewMsgId = 0L;
        private uint _Offset;
        private uint _VoiceLength;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(9, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
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

        [ProtoMember(10, IsRequired=false, Name="CancelFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CancelFlag
        {
            get
            {
                return this._CancelFlag;
            }
            set
            {
                this._CancelFlag = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ClientMsgId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientMsgId
        {
            get
            {
                return this._ClientMsgId;
            }
            set
            {
                this._ClientMsgId = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="Data", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="EndFlag", DataFormat=DataFormat.TwosComplement)]
        public uint EndFlag
        {
            get
            {
                return this._EndFlag;
            }
            set
            {
                this._EndFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Length", DataFormat=DataFormat.TwosComplement)]
        public uint Length
        {
            get
            {
                return this._Length;
            }
            set
            {
                this._Length = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="MsgId", DataFormat=DataFormat.TwosComplement)]
        public uint MsgId
        {
            get
            {
                return this._MsgId;
            }
            set
            {
                this._MsgId = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="NewMsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong NewMsgId
        {
            get
            {
                return this._NewMsgId;
            }
            set
            {
                this._NewMsgId = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Offset", DataFormat=DataFormat.TwosComplement)]
        public uint Offset
        {
            get
            {
                return this._Offset;
            }
            set
            {
                this._Offset = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="VoiceLength", DataFormat=DataFormat.TwosComplement)]
        public uint VoiceLength
        {
            get
            {
                return this._VoiceLength;
            }
            set
            {
                this._VoiceLength = value;
            }
        }
    }
}

