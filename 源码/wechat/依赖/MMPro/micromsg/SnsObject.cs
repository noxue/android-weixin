namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsObject")]
    public class SnsObject : IExtensible
    {
        private readonly List<SKBuiltinString_t> _BlackList = new List<SKBuiltinString_t>();
        private uint _BlackListCount = 0;
        private uint _CommentCount;
        private readonly List<SnsCommentInfo> _CommentUserList = new List<SnsCommentInfo>();
        private uint _CommentUserListCount;
        private uint _CreateTime;
        private uint _DeleteFlag = 0;
        private uint _ExtFlag = 0;
        private uint _GroupCount = 0;
        private readonly List<SnsGroup> _GroupList = new List<SnsGroup>();
        private readonly List<SKBuiltinString_t> _GroupUser = new List<SKBuiltinString_t>();
        private uint _GroupUserCount = 0;
        private ulong _Id;
        private uint _IsNotRichText = 0;
        private uint _LikeCount;
        private uint _LikeFlag;
        private readonly List<SnsCommentInfo> _LikeUserList = new List<SnsCommentInfo>();
        private uint _LikeUserListCount;
        private string _Nickname = "";
        private uint _NoChange = 0;
        private SKBuiltinBuffer_t _ObjectDesc;
        private ulong _ReferId = 0L;
        private string _ReferUsername = "";
        private string _Username = "";
        private uint _WithUserCount;
        private readonly List<SnsCommentInfo> _WithUserList = new List<SnsCommentInfo>();
        private uint _WithUserListCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x18, Name="BlackList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> BlackList
        {
            get
            {
                return this._BlackList;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="BlackListCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint BlackListCount
        {
            get
            {
                return this._BlackListCount;
            }
            set
            {
                this._BlackListCount = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="CommentCount", DataFormat=DataFormat.TwosComplement)]
        public uint CommentCount
        {
            get
            {
                return this._CommentCount;
            }
            set
            {
                this._CommentCount = value;
            }
        }

        [ProtoMember(12, Name="CommentUserList", DataFormat=DataFormat.Default)]
        public List<SnsCommentInfo> CommentUserList
        {
            get
            {
                return this._CommentUserList;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="CommentUserListCount", DataFormat=DataFormat.TwosComplement)]
        public uint CommentUserListCount
        {
            get
            {
                return this._CommentUserListCount;
            }
            set
            {
                this._CommentUserListCount = value;
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

        [ProtoMember(0x19, IsRequired=false, Name="DeleteFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x10, IsRequired=false, Name="ExtFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ExtFlag
        {
            get
            {
                return this._ExtFlag;
            }
            set
            {
                this._ExtFlag = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="GroupCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GroupCount
        {
            get
            {
                return this._GroupCount;
            }
            set
            {
                this._GroupCount = value;
            }
        }

        [ProtoMember(0x13, Name="GroupList", DataFormat=DataFormat.Default)]
        public List<SnsGroup> GroupList
        {
            get
            {
                return this._GroupList;
            }
        }

        [ProtoMember(0x1b, Name="GroupUser", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> GroupUser
        {
            get
            {
                return this._GroupUser;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="GroupUserCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GroupUserCount
        {
            get
            {
                return this._GroupUserCount;
            }
            set
            {
                this._GroupUserCount = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Id", DataFormat=DataFormat.TwosComplement)]
        public ulong Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="IsNotRichText", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(7, IsRequired=true, Name="LikeCount", DataFormat=DataFormat.TwosComplement)]
        public uint LikeCount
        {
            get
            {
                return this._LikeCount;
            }
            set
            {
                this._LikeCount = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="LikeFlag", DataFormat=DataFormat.TwosComplement)]
        public uint LikeFlag
        {
            get
            {
                return this._LikeFlag;
            }
            set
            {
                this._LikeFlag = value;
            }
        }

        [ProtoMember(9, Name="LikeUserList", DataFormat=DataFormat.Default)]
        public List<SnsCommentInfo> LikeUserList
        {
            get
            {
                return this._LikeUserList;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="LikeUserListCount", DataFormat=DataFormat.TwosComplement)]
        public uint LikeUserListCount
        {
            get
            {
                return this._LikeUserListCount;
            }
            set
            {
                this._LikeUserListCount = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Nickname", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(0x11, IsRequired=false, Name="NoChange", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NoChange
        {
            get
            {
                return this._NoChange;
            }
            set
            {
                this._NoChange = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ObjectDesc", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ObjectDesc
        {
            get
            {
                return this._ObjectDesc;
            }
            set
            {
                this._ObjectDesc = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="ReferId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong ReferId
        {
            get
            {
                return this._ReferId;
            }
            set
            {
                this._ReferId = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="ReferUsername", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ReferUsername
        {
            get
            {
                return this._ReferUsername;
            }
            set
            {
                this._ReferUsername = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Username", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(13, IsRequired=true, Name="WithUserCount", DataFormat=DataFormat.TwosComplement)]
        public uint WithUserCount
        {
            get
            {
                return this._WithUserCount;
            }
            set
            {
                this._WithUserCount = value;
            }
        }

        [ProtoMember(15, Name="WithUserList", DataFormat=DataFormat.Default)]
        public List<SnsCommentInfo> WithUserList
        {
            get
            {
                return this._WithUserList;
            }
        }

        [ProtoMember(14, IsRequired=true, Name="WithUserListCount", DataFormat=DataFormat.TwosComplement)]
        public uint WithUserListCount
        {
            get
            {
                return this._WithUserListCount;
            }
            set
            {
                this._WithUserListCount = value;
            }
        }
    }
}

