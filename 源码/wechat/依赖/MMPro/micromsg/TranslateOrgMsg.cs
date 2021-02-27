namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="TranslateOrgMsg")]
    public class TranslateOrgMsg : IExtensible
    {
        private string _ChatRoomID = "";
        private uint _ClientMsgID;
        private uint _Scene = 0;
        private string _TextMsg = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="ChatRoomID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ChatRoomID
        {
            get
            {
                return this._ChatRoomID;
            }
            set
            {
                this._ChatRoomID = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ClientMsgID", DataFormat=DataFormat.TwosComplement)]
        public uint ClientMsgID
        {
            get
            {
                return this._ClientMsgID;
            }
            set
            {
                this._ClientMsgID = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="TextMsg", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TextMsg
        {
            get
            {
                return this._TextMsg;
            }
            set
            {
                this._TextMsg = value;
            }
        }
    }
}

