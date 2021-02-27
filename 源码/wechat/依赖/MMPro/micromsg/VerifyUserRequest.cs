namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VerifyUserRequest")]
    public class VerifyUserRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _Opcode;
        private readonly List<uint> _SceneList = new List<uint>();
        private uint _SceneListCount = 0;
        private string _VerifyContent = "";
        private readonly List<VerifyUserInfo> _VerifyInfoList = new List<VerifyUserInfo>();
        private uint _VerifyInfoListCount = 0;
        private readonly List<VerifyUser> _VerifyUserList = new List<VerifyUser>();
        private uint _VerifyUserListSize;
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

        [ProtoMember(2, IsRequired=true, Name="Opcode", DataFormat=DataFormat.TwosComplement)]
        public uint Opcode
        {
            get
            {
                return this._Opcode;
            }
            set
            {
                this._Opcode = value;
            }
        }

        [ProtoMember(7, Name="SceneList", DataFormat=DataFormat.TwosComplement, Options=MemberSerializationOptions.Packed)]
        public List<uint> SceneList
        {
            get
            {
                return this._SceneList;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="SceneListCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SceneListCount
        {
            get
            {
                return this._SceneListCount;
            }
            set
            {
                this._SceneListCount = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="VerifyContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyContent
        {
            get
            {
                return this._VerifyContent;
            }
            set
            {
                this._VerifyContent = value;
            }
        }

        [ProtoMember(9, Name="VerifyInfoList", DataFormat=DataFormat.Default)]
        public List<VerifyUserInfo> VerifyInfoList
        {
            get
            {
                return this._VerifyInfoList;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="VerifyInfoListCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint VerifyInfoListCount
        {
            get
            {
                return this._VerifyInfoListCount;
            }
            set
            {
                this._VerifyInfoListCount = value;
            }
        }

        [ProtoMember(4, Name="VerifyUserList", DataFormat=DataFormat.Default)]
        public List<VerifyUser> VerifyUserList
        {
            get
            {
                return this._VerifyUserList;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="VerifyUserListSize", DataFormat=DataFormat.TwosComplement)]
        public uint VerifyUserListSize
        {
            get
            {
                return this._VerifyUserListSize;
            }
            set
            {
                this._VerifyUserListSize = value;
            }
        }
    }
}

