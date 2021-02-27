namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetTVTopicCommentRequest")]
    public class GetTVTopicCommentRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _LastCommentId;
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

        [ProtoMember(3, IsRequired=true, Name="LastCommentId", DataFormat=DataFormat.TwosComplement)]
        public uint LastCommentId
        {
            get
            {
                return this._LastCommentId;
            }
            set
            {
                this._LastCommentId = value;
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

