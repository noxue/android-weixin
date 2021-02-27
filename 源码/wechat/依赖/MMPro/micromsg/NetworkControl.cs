namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="NetworkControl")]
    public class NetworkControl : IExtensible
    {
        private uint _MaxNoopInterval = 0;
        private uint _MinNoopInterval = 0;
        private int _NoopIntervalTime = 0;
        private string _PortList = "";
        private string _TimeoutList = "";
        private int _TypingInterval = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="MaxNoopInterval", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MaxNoopInterval
        {
            get
            {
                return this._MaxNoopInterval;
            }
            set
            {
                this._MaxNoopInterval = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="MinNoopInterval", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MinNoopInterval
        {
            get
            {
                return this._MinNoopInterval;
            }
            set
            {
                this._MinNoopInterval = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="NoopIntervalTime", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NoopIntervalTime
        {
            get
            {
                return this._NoopIntervalTime;
            }
            set
            {
                this._NoopIntervalTime = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="PortList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PortList
        {
            get
            {
                return this._PortList;
            }
            set
            {
                this._PortList = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="TimeoutList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TimeoutList
        {
            get
            {
                return this._TimeoutList;
            }
            set
            {
                this._TimeoutList = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="TypingInterval", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int TypingInterval
        {
            get
            {
                return this._TypingInterval;
            }
            set
            {
                this._TypingInterval = value;
            }
        }
    }
}

