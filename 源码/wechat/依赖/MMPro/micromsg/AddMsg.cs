namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AddMsg")]
    public class AddMsg : IExtensible
    {
        private SKBuiltinString_t _Content;
        private uint _CreateTime;
        private SKBuiltinString_t _FromUserName;
        private SKBuiltinBuffer_t _ImgBuf;
        private uint _ImgStatus;
        private int _MsgId;
        private string _MsgSource = "";
        private int _MsgType;
        private long _NewMsgId = 0L;
        private string _PushContent = "";
        private uint _Status;
        private SKBuiltinString_t _ToUserName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=true, Name="Content", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t Content
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

        [ProtoMember(9, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(2, IsRequired=true, Name="FromUserName", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t FromUserName
        {
            get
            {
                return this._FromUserName;
            }
            set
            {
                this._FromUserName = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="ImgBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ImgBuf
        {
            get
            {
                return this._ImgBuf;
            }
            set
            {
                this._ImgBuf = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="ImgStatus", DataFormat=DataFormat.TwosComplement)]
        public uint ImgStatus
        {
            get
            {
                return this._ImgStatus;
            }
            set
            {
                this._ImgStatus = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="MsgId", DataFormat=DataFormat.TwosComplement)]
        public int MsgId
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

        [ProtoMember(10, IsRequired=false, Name="MsgSource", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=true, Name="MsgType", DataFormat=DataFormat.TwosComplement)]
        public int MsgType
        {
            get
            {
                return this._MsgType;
            }
            set
            {
                this._MsgType = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="NewMsgId", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public long NewMsgId
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

        [ProtoMember(11, IsRequired=false, Name="PushContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PushContent
        {
            get
            {
                return this._PushContent;
            }
            set
            {
                this._PushContent = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="ToUserName", DataFormat=DataFormat.Default)]
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
    }
}

