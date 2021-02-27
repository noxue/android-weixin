namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsCommentInfo")]
    public class SnsCommentInfo : IExtensible
    {
        private int _CommentId = 0;
        private ulong _CommentId2 = 0L;
        private string _Content = "";
        private uint _CreateTime;
        private uint _DeleteFlag = 0;
        private uint _IsNotRichText = 0;
        private string _Nickname = "";
        private int _ReplyCommentId = 0;
        private ulong _ReplyCommentId2 = 0L;
        private string _ReplyUsername = "";
        private uint _Source;
        private uint _Type;
        private string _Username = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(7, IsRequired=false, Name="CommentId", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(12, IsRequired=false, Name="CommentId2", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

        [ProtoMember(5, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(13, IsRequired=false, Name="DeleteFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DeleteFlag
        {
            get
            {
                return this._DeleteFlag;
            }
            set
            {
                this._DeleteFlag = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="IsNotRichText", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(2, IsRequired=false, Name="Nickname", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Nickname
        {
            get
            {
                return this._Nickname;
            }
            set
            {
                this._Nickname = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="ReplyCommentId", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(11, IsRequired=false, Name="ReplyCommentId2", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

        [ProtoMember(9, IsRequired=false, Name="ReplyUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ReplyUsername
        {
            get
            {
                return this._ReplyUsername;
            }
            set
            {
                this._ReplyUsername = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Source", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(4, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(1, IsRequired=false, Name="Username", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
            }
        }
    }
}

