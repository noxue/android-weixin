namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmojiUploadInfoResp")]
    public class EmojiUploadInfoResp : IExtensible
    {
        private string _MD5 = "";
        private uint _MsgID;
        private ulong _NewMsgId = 0L;
        private int _Ret;
        private int _StartPos;
        private int _TotalLen;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="MD5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MD5
        {
            get
            {
                return this._MD5;
            }
            set
            {
                this._MD5 = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="MsgID", DataFormat=DataFormat.TwosComplement)]
        public uint MsgID
        {
            get
            {
                return this._MsgID;
            }
            set
            {
                this._MsgID = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="NewMsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

        [ProtoMember(1, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public int Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public int StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public int TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }
    }
}

