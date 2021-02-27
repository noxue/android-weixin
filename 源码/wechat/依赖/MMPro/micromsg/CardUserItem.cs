namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CardUserItem")]
    public class CardUserItem : IExtensible
    {
        private string _CardId = "";
        private uint _StateFlag = 0;
        private uint _Status = 0;
        private ulong _UpdateSequence = 0L;
        private uint _UpdateTime = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="CardId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CardId
        {
            get
            {
                return this._CardId;
            }
            set
            {
                this._CardId = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="StateFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint StateFlag
        {
            get
            {
                return this._StateFlag;
            }
            set
            {
                this._StateFlag = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Status", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UpdateSequence", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong UpdateSequence
        {
            get
            {
                return this._UpdateSequence;
            }
            set
            {
                this._UpdateSequence = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="UpdateTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this._UpdateTime = value;
            }
        }
    }
}

