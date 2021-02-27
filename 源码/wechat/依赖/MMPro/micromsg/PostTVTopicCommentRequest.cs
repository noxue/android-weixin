namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PostTVTopicCommentRequest")]
    public class PostTVTopicCommentRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Content = "";
        private string _TVTopic = "";
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

        [ProtoMember(3, IsRequired=false, Name="Content", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="TVTopic", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TVTopic
        {
            get
            {
                return this._TVTopic;
            }
            set
            {
                this._TVTopic = value;
            }
        }
    }
}

