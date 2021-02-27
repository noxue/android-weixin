namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VOIPRUDPCmd")]
    public class VOIPRUDPCmd : IExtensible
    {
        private byte[] _CmdBuffer = null;
        private int _CmdType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="CmdBuffer", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] CmdBuffer
        {
            get
            {
                return this._CmdBuffer;
            }
            set
            {
                this._CmdBuffer = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="CmdType", DataFormat=DataFormat.TwosComplement)]
        public int CmdType
        {
            get
            {
                return this._CmdType;
            }
            set
            {
                this._CmdType = value;
            }
        }
    }
}

