namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="AppPersonalInfo")]
    public class AppPersonalInfo : IExtensible
    {
        private string _AppID = "";
        private string _PersonalSettingXml = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(2, IsRequired=false, Name="PersonalSettingXml", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PersonalSettingXml
        {
            get
            {
                return this._PersonalSettingXml;
            }
            set
            {
                this._PersonalSettingXml = value;
            }
        }
    }
}

