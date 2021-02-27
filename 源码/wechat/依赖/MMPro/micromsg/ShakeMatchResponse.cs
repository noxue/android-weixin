namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeMatchResponse")]
    public class ShakeMatchResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _Ret;
        private string _Tips = "";
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

        [ProtoMember(3, IsRequired=true, Name="Ret", DataFormat=DataFormat.TwosComplement)]
        public uint Ret
        {
            get
            {
                return this._Ret;
            }
            set
            {
                this._Ret = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Tips", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Tips
        {
            get
            {
                return this._Tips;
            }
            set
            {
                this._Tips = value;
            }
        }
    }
}

