namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoiceTransRes")]
    public class VoiceTransRes : IExtensible
    {
        private uint _EndFlag;
        private string _Result = "";
        private uint _Sequence;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="EndFlag", DataFormat=DataFormat.TwosComplement)]
        public uint EndFlag
        {
            get
            {
                return this._EndFlag;
            }
            set
            {
                this._EndFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Result", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                this._Result = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="Sequence", DataFormat=DataFormat.TwosComplement)]
        public uint Sequence
        {
            get
            {
                return this._Sequence;
            }
            set
            {
                this._Sequence = value;
            }
        }
    }
}

