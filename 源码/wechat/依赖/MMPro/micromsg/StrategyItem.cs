namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="StrategyItem")]
    public class StrategyItem : IExtensible
    {
        private uint _Cycle;
        private uint _Enalbe;
        private string _ExtInfo = "";
        private uint _LogType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="Cycle", DataFormat=DataFormat.TwosComplement)]
        public uint Cycle
        {
            get
            {
                return this._Cycle;
            }
            set
            {
                this._Cycle = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Enalbe", DataFormat=DataFormat.TwosComplement)]
        public uint Enalbe
        {
            get
            {
                return this._Enalbe;
            }
            set
            {
                this._Enalbe = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ExtInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ExtInfo
        {
            get
            {
                return this._ExtInfo;
            }
            set
            {
                this._ExtInfo = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="LogType", DataFormat=DataFormat.TwosComplement)]
        public uint LogType
        {
            get
            {
                return this._LogType;
            }
            set
            {
                this._LogType = value;
            }
        }
    }
}

