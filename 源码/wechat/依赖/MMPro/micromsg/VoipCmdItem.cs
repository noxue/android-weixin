namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoipCmdItem")]
    public class VoipCmdItem : IExtensible
    {
        private SKBuiltinBuffer_t _CmdBuf;
        private int _CmdId;
        private string _FromUserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="CmdBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t CmdBuf
        {
            get
            {
                return this._CmdBuf;
            }
            set
            {
                this._CmdBuf = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="CmdId", DataFormat=DataFormat.TwosComplement)]
        public int CmdId
        {
            get
            {
                return this._CmdId;
            }
            set
            {
                this._CmdId = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="FromUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUserName
        {
            get
            {
                return this._FromUserName;
            }
            set
            {
                this._FromUserName = value;
            }
        }
    }
}

