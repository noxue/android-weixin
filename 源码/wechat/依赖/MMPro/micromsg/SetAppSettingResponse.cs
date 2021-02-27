namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SetAppSettingResponse")]
    public class SetAppSettingResponse : IExtensible
    {
        private uint _AppFlag;
        private string _AppID = "";
        private micromsg.BaseResponse _BaseResponse;
        private uint _CmdID;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=true, Name="AppFlag", DataFormat=DataFormat.TwosComplement)]
        public uint AppFlag
        {
            get
            {
                return this._AppFlag;
            }
            set
            {
                this._AppFlag = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppID
        {
            get
            {
                return this._AppID;
            }
            set
            {
                this._AppID = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="CmdID", DataFormat=DataFormat.TwosComplement)]
        public uint CmdID
        {
            get
            {
                return this._CmdID;
            }
            set
            {
                this._CmdID = value;
            }
        }
    }
}

