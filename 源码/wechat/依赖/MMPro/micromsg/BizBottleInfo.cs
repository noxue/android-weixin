namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BizBottleInfo")]
    public class BizBottleInfo : IExtensible
    {
        private string _BottleID = "";
        private string _FromUserName = "";
        private uint _MsgType;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=false, Name="BottleID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BottleID
        {
            get
            {
                return this._BottleID;
            }
            set
            {
                this._BottleID = value;
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

        [ProtoMember(2, IsRequired=true, Name="MsgType", DataFormat=DataFormat.TwosComplement)]
        public uint MsgType
        {
            get
            {
                return this._MsgType;
            }
            set
            {
                this._MsgType = value;
            }
        }
    }
}

