namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsCommentRequest")]
    public class SnsCommentRequest : IExtensible
    {
        private SnsActionGroup _Action;
        private micromsg.BaseRequest _BaseRequest;
        private string _ClientId = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=true, Name="Action", DataFormat=DataFormat.Default)]
        public SnsActionGroup Action
        {
            get
            {
                return this._Action;
            }
            set
            {
                this._Action = value;
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

        [ProtoMember(3, IsRequired=false, Name="ClientId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }
    }
}

