namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ChatRoomMemberInfo")]
    public class ChatRoomMemberInfo : IExtensible
    {
        private string _BigHeadImgUrl = "";
        private uint _ChatroomMemberFlag;
        private string _DisplayName = "";
        private string _NickName = "";
        private string _SmallHeadImgUrl = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="BigHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BigHeadImgUrl
        {
            get
            {
                return this._BigHeadImgUrl;
            }
            set
            {
                this._BigHeadImgUrl = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="ChatroomMemberFlag", DataFormat=DataFormat.TwosComplement)]
        public uint ChatroomMemberFlag
        {
            get
            {
                return this._ChatroomMemberFlag;
            }
            set
            {
                this._ChatroomMemberFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="DisplayName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                this._DisplayName = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="NickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="SmallHeadImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmallHeadImgUrl
        {
            get
            {
                return this._SmallHeadImgUrl;
            }
            set
            {
                this._SmallHeadImgUrl = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

