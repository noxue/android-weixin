namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindBlackberryPPIDRequest")]
    public class BindBlackberryPPIDRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _BBMNickName = "";
        private string _BBPIN = "";
        private string _BBPPID = "";
        private uint _Force;
        private uint _Opcode;
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

        [ProtoMember(5, IsRequired=false, Name="BBMNickName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBMNickName
        {
            get
            {
                return this._BBMNickName;
            }
            set
            {
                this._BBMNickName = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="BBPIN", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBPIN
        {
            get
            {
                return this._BBPIN;
            }
            set
            {
                this._BBPIN = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="BBPPID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BBPPID
        {
            get
            {
                return this._BBPPID;
            }
            set
            {
                this._BBPPID = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="Force", DataFormat=DataFormat.TwosComplement)]
        public uint Force
        {
            get
            {
                return this._Force;
            }
            set
            {
                this._Force = value;
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
    }
}

