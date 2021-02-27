namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="EmojiUploadInfoReq")]
    public class EmojiUploadInfoReq : IExtensible
    {
        private string _ClientMsgID = "";
        private SKBuiltinBuffer_t _EmojiBuffer;
        private string _ExternXML = "";
        private string _MD5 = "";
        private string _MsgSource = "";
        private int _NewXmlFlag = 0;
        private string _Report = "";
        private int _StartPos;
        private int _TotalLen;
        private string _ToUserName = "";
        private int _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(9, IsRequired=false, Name="ClientMsgID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientMsgID
        {
            get
            {
                return this._ClientMsgID;
            }
            set
            {
                this._ClientMsgID = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="EmojiBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t EmojiBuffer
        {
            get
            {
                return this._EmojiBuffer;
            }
            set
            {
                this._EmojiBuffer = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="ExternXML", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExternXML
        {
            get
            {
                return this._ExternXML;
            }
            set
            {
                this._ExternXML = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="MD5", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="NewXmlFlag", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NewXmlFlag
        {
            get
            {
                return this._NewXmlFlag;
            }
            set
            {
                this._NewXmlFlag = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Report", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Report
        {
            get
            {
                return this._Report;
            }
            set
            {
                this._Report = value;
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

        [ProtoMember(5, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public int Type
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

