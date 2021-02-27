namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RadarCreateChatRoomParameter")]
    public class RadarCreateChatRoomParameter : IExtensible
    {
        private uint _RadarMemberCount = 0;
        private readonly List<RadarMember> _RadarMemberList = new List<RadarMember>();
        private string _Ticket = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="RadarMemberCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint RadarMemberCount
        {
            get
            {
                return this._RadarMemberCount;
            }
            set
            {
                this._RadarMemberCount = value;
            }
        }

        [ProtoMember(3, Name="RadarMemberList", DataFormat=DataFormat.Default)]
        public List<RadarMember> RadarMemberList
        {
            get
            {
                return this._RadarMemberList;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Ticket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                this._Ticket = value;
            }
        }
    }
}

