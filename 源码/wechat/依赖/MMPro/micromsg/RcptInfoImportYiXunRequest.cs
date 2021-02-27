namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="RcptInfoImportYiXunRequest")]
    public class RcptInfoImportYiXunRequest : IExtensible
    {
        private SKBuiltinBuffer_t _A2Key = null;
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _NewA2Key = null;
        private uint _qq;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="A2Key", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t A2Key
        {
            get
            {
                return this._A2Key;
            }
            set
            {
                this._A2Key = value;
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

        [ProtoMember(4, IsRequired=false, Name="NewA2Key", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t NewA2Key
        {
            get
            {
                return this._NewA2Key;
            }
            set
            {
                this._NewA2Key = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="qq", DataFormat=DataFormat.TwosComplement)]
        public uint qq
        {
            get
            {
                return this._qq;
            }
            set
            {
                this._qq = value;
            }
        }
    }
}

