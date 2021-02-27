namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ModChatRoomMemberFlag")]
    public class ModChatRoomMemberFlag : IExtensible
    {
        private string _ChatRoomName = "";
        private uint _FlagSwitch;
        private string _UserName = "";
        private uint _Value;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="ChatRoomName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomName
        {
            get
            {
                return this._ChatRoomName;
            }
            set
            {
                this._ChatRoomName = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="FlagSwitch", DataFormat=DataFormat.TwosComplement)]
        public uint FlagSwitch
        {
            get
            {
                return this._FlagSwitch;
            }
            set
            {
                this._FlagSwitch = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(4, IsRequired=true, Name="Value", DataFormat=DataFormat.TwosComplement)]
        public uint Value
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
    }
}

