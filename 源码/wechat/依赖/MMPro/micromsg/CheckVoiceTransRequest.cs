namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckVoiceTransRequest")]
    public class CheckVoiceTransRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _MsgId = 0;
        private ulong _NewMsgId = 0L;
        private uint _TotalLen;
        private micromsg.VoiceAttr _VoiceAttr = null;
        private string _VoiceId = "";
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

        [ProtoMember(4, IsRequired=false, Name="MsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(3, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
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

        [ProtoMember(5, IsRequired=false, Name="VoiceAttr", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.VoiceAttr VoiceAttr
        {
            get
            {
                return this._VoiceAttr;
            }
            set
            {
                this._VoiceAttr = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="VoiceId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VoiceId
        {
            get
            {
                return this._VoiceId;
            }
            set
            {
                this._VoiceId = value;
            }
        }
    }
}

