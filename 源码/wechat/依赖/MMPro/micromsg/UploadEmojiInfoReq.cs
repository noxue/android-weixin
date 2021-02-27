namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadEmojiInfoReq")]
    public class UploadEmojiInfoReq : IExtensible
    {
        private byte[] _EmojiBuffer;
        private string _MD5 = "";
        private int _StartPos;
        private int _TotalLen;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="EmojiBuffer", DataFormat=DataFormat.Default)]
        public byte[] EmojiBuffer
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

