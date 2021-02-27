namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CheckBigFileUploadResponse")]
    public class CheckBigFileUploadResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private string _FakeAESKey = "";
        private string _FakeSignature = "";
        private uint _Fakeuin = 0;
        private string _Signature = "";
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

        [ProtoMember(4, IsRequired=false, Name="FakeAESKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FakeAESKey
        {
            get
            {
                return this._FakeAESKey;
            }
            set
            {
                this._FakeAESKey = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="FakeSignature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FakeSignature
        {
            get
            {
                return this._FakeSignature;
            }
            set
            {
                this._FakeSignature = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Fakeuin", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Fakeuin
        {
            get
            {
                return this._Fakeuin;
            }
            set
            {
                this._Fakeuin = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Signature", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Signature
        {
            get
            {
                return this._Signature;
            }
            set
            {
                this._Signature = value;
            }
        }
    }
}

