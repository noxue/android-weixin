namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ApplyResetPawRequest")]
    public class ApplyResetPawRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _GetMethod = 0;
        private SKBuiltinBuffer_t _RandomEncryKey = null;
        private string _ResetInfo = "";
        private int _Type;
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

        [ProtoMember(4, IsRequired=false, Name="GetMethod", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint GetMethod
        {
            get
            {
                return this._GetMethod;
            }
            set
            {
                this._GetMethod = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="RandomEncryKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t RandomEncryKey
        {
            get
            {
                return this._RandomEncryKey;
            }
            set
            {
                this._RandomEncryKey = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ResetInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ResetInfo
        {
            get
            {
                return this._ResetInfo;
            }
            set
            {
                this._ResetInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

