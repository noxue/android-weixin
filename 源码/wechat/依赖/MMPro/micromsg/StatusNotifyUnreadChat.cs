namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="StatusNotifyUnreadChat")]
    public class StatusNotifyUnreadChat : IExtensible
    {
        private uint _LastReadTime;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="LastReadTime", DataFormat=DataFormat.TwosComplement)]
        public uint LastReadTime
        {
            get
            {
                return this._LastReadTime;
            }
            set
            {
                this._LastReadTime = value;
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

