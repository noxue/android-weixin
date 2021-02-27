namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SendYoRequest")]
    public class SendYoRequest : IExtensible
    {
        private uint _ClientMsgId;
        private uint _Count;
        private uint _CreateTime;
        private string _MsgSource = "";
        private string _ToUserName = "";
        private uint _Type;
        private uint _YoType = 0;
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

        [ProtoMember(3, IsRequired=true, Name="Count", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(1, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(7, IsRequired=false, Name="YoType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint YoType
        {
            get
            {
                return this._YoType;
            }
            set
            {
                this._YoType = value;
            }
        }
    }
}

