namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetMailOAuthUrlRequest")]
    public class GetMailOAuthUrlRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _MailAccount = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(2, IsRequired=false, Name="MailAccount", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MailAccount
        {
            get
            {
                return this._MailAccount;
            }
            set
            {
                this._MailAccount = value;
            }
        }
    }
}

