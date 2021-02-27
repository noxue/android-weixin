namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RemindItem")]
    public class RemindItem : IExtensible
    {
        private string _Content = "";
        private uint _CreateTime;
        private uint _Flag;
        private string _FromUser = "";
        private uint _RemindID;
        private ulong _RemindTime;
        private uint _ToUserCount;
        private readonly List<RemindMember> _ToUserList = new List<RemindMember>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(2, IsRequired=true, Name="CreateTime", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(7, IsRequired=true, Name="Flag", DataFormat=DataFormat.TwosComplement)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="FromUser", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUser
        {
            get
            {
                return this._FromUser;
            }
            set
            {
                this._FromUser = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="RemindID", DataFormat=DataFormat.TwosComplement)]
        public uint RemindID
        {
            get
            {
                return this._RemindID;
            }
            set
            {
                this._RemindID = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="RemindTime", DataFormat=DataFormat.TwosComplement)]
        public ulong RemindTime
        {
            get
            {
                return this._RemindTime;
            }
            set
            {
                this._RemindTime = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ToUserCount", DataFormat=DataFormat.TwosComplement)]
        public uint ToUserCount
        {
            get
            {
                return this._ToUserCount;
            }
            set
            {
                this._ToUserCount = value;
            }
        }

        [ProtoMember(6, Name="ToUserList", DataFormat=DataFormat.Default)]
        public List<RemindMember> ToUserList
        {
            get
            {
                return this._ToUserList;
            }
        }
    }
}

