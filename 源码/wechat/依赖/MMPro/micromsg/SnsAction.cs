namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsAction")]
    public class SnsAction : IExtensible
    {
        private int _CommentId = 0;
        private ulong _CommentId2 = 0L;
        private string _Content = "";
        private uint _CreateTime = 0;
        private string _FromNickname = "";
        private string _FromUsername = "";
        private uint _IsNotRichText = 0;
        private int _ReplyCommentId = 0;
        private ulong _ReplyCommentId2 = 0L;
        private uint _Source;
        private string _ToNickname = "";
        private string _ToUsername = "";
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(10, IsRequired=false, Name="CommentId", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int CommentId
        {
            get
            {
                return this._CommentId;
            }
            set
            {
                this._CommentId = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="CommentId2", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong CommentId2
        {
            get
            {
                return this._CommentId2;
            }
            set
            {
                this._CommentId2 = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(7, IsRequired=false, Name="CreateTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(3, IsRequired=false, Name="FromNickname", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromNickname
        {
            get
            {
                return this._FromNickname;
            }
            set
            {
                this._FromNickname = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="FromUsername", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="IsNotRichText", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IsNotRichText
        {
            get
            {
                return this._IsNotRichText;
            }
            set
            {
                this._IsNotRichText = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="ReplyCommentId", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ReplyCommentId
        {
            get
            {
                return this._ReplyCommentId;
            }
            set
            {
                this._ReplyCommentId = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="ReplyCommentId2", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong ReplyCommentId2
        {
            get
            {
                return this._ReplyCommentId2;
            }
            set
            {
                this._ReplyCommentId2 = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Source", DataFormat=DataFormat.TwosComplement)]
        public uint Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ToNickname", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToNickname
        {
            get
            {
                return this._ToNickname;
            }
            set
            {
                this._ToNickname = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ToUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUsername
        {
            get
            {
                return this._ToUsername;
            }
            set
            {
                this._ToUsername = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

