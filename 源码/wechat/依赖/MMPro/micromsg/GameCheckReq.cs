namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="GameCheckReq")]
    public class GameCheckReq : IExtensible
    {
        private string _AppID = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _LocalLifeNum;
        private string _Token = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AppID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppID
        {
            get
            {
                return this._AppID;
            }
            set
            {
                this._AppID = value;
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

        [ProtoMember(3, IsRequired=true, Name="LocalLifeNum", DataFormat=DataFormat.TwosComplement)]
        public uint LocalLifeNum
        {
            get
            {
                return this._LocalLifeNum;
            }
            set
            {
                this._LocalLifeNum = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Token", DataFormat=DataFormat.Default), DefaultValue("")]
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
    }
}

