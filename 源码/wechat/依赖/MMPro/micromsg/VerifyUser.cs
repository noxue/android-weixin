namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VerifyUser")]
    public class VerifyUser : IExtensible
    {
        private string _AntispamTicket = "";
        private string _ChatRoomUserName = "";
        private uint _FriendFlag = 0;
        private string _SourceNickName = "";
        private string _SourceUserName = "";
        private string _Value = "";
        private string _VerifyUserTicket = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AntispamTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AntispamTicket
        {
            get
            {
                return this._AntispamTicket;
            }
            set
            {
                this._AntispamTicket = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ChatRoomUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomUserName
        {
            get
            {
                return this._ChatRoomUserName;
            }
            set
            {
                this._ChatRoomUserName = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="FriendFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FriendFlag
        {
            get
            {
                return this._FriendFlag;
            }
            set
            {
                this._FriendFlag = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="SourceNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SourceNickName
        {
            get
            {
                return this._SourceNickName;
            }
            set
            {
                this._SourceNickName = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="SourceUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SourceUserName
        {
            get
            {
                return this._SourceUserName;
            }
            set
            {
                this._SourceUserName = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Value", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="VerifyUserTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VerifyUserTicket
        {
            get
            {
                return this._VerifyUserTicket;
            }
            set
            {
                this._VerifyUserTicket = value;
            }
        }
    }
}

