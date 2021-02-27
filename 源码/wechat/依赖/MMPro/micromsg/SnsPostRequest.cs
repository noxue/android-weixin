namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsPostRequest")]
    public class SnsPostRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private readonly List<SKBuiltinString_t> _BlackList = new List<SKBuiltinString_t>();
        private uint _BlackListCount = 0;
        private string _ClientId = "";
        private SnsPostCtocUploadInfo _CtocUploadInfo = null;
        private uint _GroupCount = 0;
        private readonly List<SnsGroup> _GroupIds = new List<SnsGroup>();
        private readonly List<SKBuiltinString_t> _GroupUser = new List<SKBuiltinString_t>();
        private uint _GroupUserCount = 0;
        private SKBuiltinBuffer_t _ObjectDesc;
        private uint _ObjectSource = 0;
        private uint _PostBGImgType = 0;
        private uint _Privacy = 0;
        private ulong _ReferId = 0L;
        private uint _SyncFlag = 0;
        private micromsg.TwitterInfo _TwitterInfo = null;
        private readonly List<SKBuiltinString_t> _WithUserList = new List<SKBuiltinString_t>();
        private uint _WithUserListCount = 0;
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

        [ProtoMember(14, Name="BlackList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> BlackList
        {
            get
            {
                return this._BlackList;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="BlackListCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(7, IsRequired=false, Name="ClientId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="CtocUploadInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SnsPostCtocUploadInfo CtocUploadInfo
        {
            get
            {
                return this._CtocUploadInfo;
            }
            set
            {
                this._CtocUploadInfo = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="GroupCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(10, Name="GroupIds", DataFormat=DataFormat.Default)]
        public List<SnsGroup> GroupIds
        {
            get
            {
                return this._GroupIds;
            }
        }

        [ProtoMember(0x11, Name="GroupUser", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> GroupUser
        {
            get
            {
                return this._GroupUser;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="GroupUserCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(2, IsRequired=true, Name="ObjectDesc", DataFormat=DataFormat.Default)]
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

        [ProtoMember(11, IsRequired=false, Name="ObjectSource", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ObjectSource
        {
            get
            {
                return this._ObjectSource;
            }
            set
            {
                this._ObjectSource = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="PostBGImgType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PostBGImgType
        {
            get
            {
                return this._PostBGImgType;
            }
            set
            {
                this._PostBGImgType = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Privacy", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Privacy
        {
            get
            {
                return this._Privacy;
            }
            set
            {
                this._Privacy = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="ReferId", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
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

        [ProtoMember(6, IsRequired=false, Name="SyncFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SyncFlag
        {
            get
            {
                return this._SyncFlag;
            }
            set
            {
                this._SyncFlag = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="TwitterInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.TwitterInfo TwitterInfo
        {
            get
            {
                return this._TwitterInfo;
            }
            set
            {
                this._TwitterInfo = value;
            }
        }

        [ProtoMember(4, Name="WithUserList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> WithUserList
        {
            get
            {
                return this._WithUserList;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="WithUserListCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

