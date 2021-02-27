namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ConnectInfoAddr")]
    public class ConnectInfoAddr : IExtensible
    {
        private string _IP = "";
        private readonly List<uint> _Port = new List<uint>();
        private uint _PortCount = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="IP", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                this._IP = value;
            }
        }

        [ProtoMember(2, Name="Port", DataFormat=DataFormat.TwosComplement)]
        public List<uint> Port
        {
            get
            {
                return this._Port;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="PortCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PortCount
        {
            get
            {
                return this._PortCount;
            }
            set
            {
                this._PortCount = value;
            }
        }
    }
}

