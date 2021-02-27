namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GetVoiceprintTicketRsaResponse")]
    public class GetVoiceprintTicketRsaResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.BuiltinIPList _BuiltinIPList = null;
        private micromsg.NetworkControl _NetworkControl = null;
        private HostList _NewHostList = null;
        private string _VoiceprintTicket = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="BuiltinIPList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.BuiltinIPList BuiltinIPList
        {
            get
            {
                return this._BuiltinIPList;
            }
            set
            {
                this._BuiltinIPList = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="NetworkControl", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.NetworkControl NetworkControl
        {
            get
            {
                return this._NetworkControl;
            }
            set
            {
                this._NetworkControl = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="NewHostList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public HostList NewHostList
        {
            get
            {
                return this._NewHostList;
            }
            set
            {
                this._NewHostList = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="VoiceprintTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VoiceprintTicket
        {
            get
            {
                return this._VoiceprintTicket;
            }
            set
            {
                this._VoiceprintTicket = value;
            }
        }
    }
}

