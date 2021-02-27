namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AddGameMsg")]
    public class AddGameMsg : IExtensible
    {
        private uint _Flag;
        private string _FromUserName = "";
        private int _MsgId;
        private uint _Status;
        private string _ToUserName = "";
        private int _Type;
        private uint _UpdateSeq;
        private uint _UpdateTime;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=true, Name="Flag", DataFormat=DataFormat.TwosComplement)]
        public uint Flag
        {
            get
            {
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FromUserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(1, IsRequired=true, Name="MsgId", DataFormat=DataFormat.TwosComplement)]
        public int MsgId
        {
            get
            {
                return this._MsgId;
            }
            set
            {
                this._MsgId = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUserName
        {
            get
            {
                return this._ToUserName;
            }
            set
            {
                this._ToUserName = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="UpdateSeq", DataFormat=DataFormat.TwosComplement)]
        public uint UpdateSeq
        {
            get
            {
                return this._UpdateSeq;
            }
            set
            {
                this._UpdateSeq = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="UpdateTime", DataFormat=DataFormat.TwosComplement)]
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

