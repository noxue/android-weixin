namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UserInfoInApp")]
    public class UserInfoInApp : IExtensible
    {
        private string _PersonalSettingXml = "";
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(1, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

