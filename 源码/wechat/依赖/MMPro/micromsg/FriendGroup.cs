namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FriendGroup")]
    public class FriendGroup : IExtensible
    {
        private uint _GroupId;
        private string _GroupName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="GroupId", DataFormat=DataFormat.TwosComplement)]
        public uint GroupId
        {
            get
            {
                return this._GroupId;
            }
            set
            {
                this._GroupId = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="GroupName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                this._GroupName = value;
            }
        }
    }
}

