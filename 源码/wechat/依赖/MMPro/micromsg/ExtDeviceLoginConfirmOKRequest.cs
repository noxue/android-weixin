namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExtDeviceLoginConfirmOKRequest")]
    public class ExtDeviceLoginConfirmOKRequest : IExtensible
    {
        private string _LoginUrl;
        private string _SessionList = "";
        private readonly List<string> _UnReadChatContactList = new List<string>();
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="LoginUrl", DataFormat=DataFormat.Default)]
        public string LoginUrl
        {
            get
            {
                return this._LoginUrl;
            }
            set
            {
                this._LoginUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="SessionList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SessionList
        {
            get
            {
                return this._SessionList;
            }
            set
            {
                this._SessionList = value;
            }
        }

        [ProtoMember(3, Name="UnReadChatContactList", DataFormat=DataFormat.Default)]
        public List<string> UnReadChatContactList
        {
            get
            {
                return this._UnReadChatContactList;
            }
        }
    }
}

