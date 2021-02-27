namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DelContactLabelRequest")]
    public class DelContactLabelRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _LabelIDList = "";
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

        [ProtoMember(2, IsRequired=false, Name="LabelIDList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LabelIDList
        {
            get
            {
                return this._LabelIDList;
            }
            set
            {
                this._LabelIDList = value;
            }
        }
    }
}

