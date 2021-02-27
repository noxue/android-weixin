namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SendFeedbackRequest")]
    public class SendFeedbackRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Content = "";
        private string _MachineType = "";
        private uint _ReportType = 0;
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

        [ProtoMember(2, IsRequired=false, Name="MachineType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MachineType
        {
            get
            {
                return this._MachineType;
            }
            set
            {
                this._MachineType = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ReportType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ReportType
        {
            get
            {
                return this._ReportType;
            }
            set
            {
                this._ReportType = value;
            }
        }
    }
}

