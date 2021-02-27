namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AppSetting")]
    public class AppSetting : IExtensible
    {
        private uint _AppFlag;
        private string _AppID = "";
        private string _OpenID = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="AppFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(1, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(3, IsRequired=false, Name="OpenID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OpenID
        {
            get
            {
                return this._OpenID;
            }
            set
            {
                this._OpenID = value;
            }
        }
    }
}

