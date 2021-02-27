namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindLinkedinContactRequest")]
    public class BindLinkedinContactRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _LinkedinMemberID = "";
        private string _LinkedinName = "";
        private string _LinkedinPublicUrl = "";
        private string _LinkedinReturnSignature = "";
        private string _LinkedinSignature = "";
        private string _Nounce = "";
        private uint _Opcode;
        private string _Timestamp = "";
        private uint _Visible;
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

        [ProtoMember(4, IsRequired=false, Name="LinkedinMemberID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LinkedinMemberID
        {
            get
            {
                return this._LinkedinMemberID;
            }
            set
            {
                this._LinkedinMemberID = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="LinkedinName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LinkedinName
        {
            get
            {
                return this._LinkedinName;
            }
            set
            {
                this._LinkedinName = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="LinkedinPublicUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LinkedinPublicUrl
        {
            get
            {
                return this._LinkedinPublicUrl;
            }
            set
            {
                this._LinkedinPublicUrl = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="LinkedinReturnSignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LinkedinReturnSignature
        {
            get
            {
                return this._LinkedinReturnSignature;
            }
            set
            {
                this._LinkedinReturnSignature = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="LinkedinSignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LinkedinSignature
        {
            get
            {
                return this._LinkedinSignature;
            }
            set
            {
                this._LinkedinSignature = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Nounce", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Nounce
        {
            get
            {
                return this._Nounce;
            }
            set
            {
                this._Nounce = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Opcode", DataFormat=DataFormat.TwosComplement)]
        public uint Opcode
        {
            get
            {
                return this._Opcode;
            }
            set
            {
                this._Opcode = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Timestamp", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Timestamp
        {
            get
            {
                return this._Timestamp;
            }
            set
            {
                this._Timestamp = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Visible", DataFormat=DataFormat.TwosComplement)]
        public uint Visible
        {
            get
            {
                return this._Visible;
            }
            set
            {
                this._Visible = value;
            }
        }
    }
}

