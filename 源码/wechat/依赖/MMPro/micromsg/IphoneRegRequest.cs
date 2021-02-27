namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="IphoneRegRequest")]
    public class IphoneRegRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _Sound = "";
        private uint _Status = 0;
        private string _Token = "";
        private uint _TokenCert = 0;
        private uint _TokenEnv = 0;
        private uint _TokenScene = 0;
        private string _VoipSound = "";
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

        [ProtoMember(3, IsRequired=false, Name="Sound", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Sound
        {
            get
            {
                return this._Sound;
            }
            set
            {
                this._Sound = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Status", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Token
        {
            get
            {
                return this._Token;
            }
            set
            {
                this._Token = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="TokenCert", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TokenCert
        {
            get
            {
                return this._TokenCert;
            }
            set
            {
                this._TokenCert = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="TokenEnv", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TokenEnv
        {
            get
            {
                return this._TokenEnv;
            }
            set
            {
                this._TokenEnv = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="TokenScene", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TokenScene
        {
            get
            {
                return this._TokenScene;
            }
            set
            {
                this._TokenScene = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="VoipSound", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VoipSound
        {
            get
            {
                return this._VoipSound;
            }
            set
            {
                this._VoipSound = value;
            }
        }
    }
}

