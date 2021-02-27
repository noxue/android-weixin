namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="MicroMsgRequestNew")]
    public class MicroMsgRequestNew : IExtensible
    {
        private uint _ClientMsgId;
        private string _Content = "";
        private uint _CreateTime;
        private uint _CtrlBit = 0;
        private string _MsgSource = "";
        private SKBuiltinString_t _ToUserName;
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=true, Name="ClientMsgId", DataFormat=DataFormat.TwosComplement)]
        public uint ClientMsgId
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

        [ProtoMember(2, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
        public uint CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="CtrlBit", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CtrlBit
        {
            get
            {
                return this._CtrlBit;
            }
            set
            {
                this._CtrlBit = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="MsgSource", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MsgSource
        {
            get
            {
                return this._MsgSource;
            }
            set
            {
                this._MsgSource = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ToUserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ToUserName
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

        [ProtoMember(3, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

