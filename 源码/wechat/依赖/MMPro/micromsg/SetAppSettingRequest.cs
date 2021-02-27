namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SetAppSettingRequest")]
    public class SetAppSettingRequest : IExtensible
    {
        private string _AppID = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _CmdID;
        private string _CmdValue = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(1, IsRequired=true, Name="BaseRequest", DataFormat=DataFormat.Default)]
        public micromsg.BaseRequest BaseRequest
        {
            get
            {
                return this._BaseRequest;
            }
            set
            {
                this._BaseRequest = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="CmdID", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(4, IsRequired=false, Name="CmdValue", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CmdValue
        {
            get
            {
                return this._CmdValue;
            }
            set
            {
                this._CmdValue = value;
            }
        }
    }
}

