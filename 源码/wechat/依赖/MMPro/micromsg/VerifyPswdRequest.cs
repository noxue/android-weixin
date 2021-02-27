namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VerifyPswdRequest")]
    public class VerifyPswdRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinString_t _ImgCode = null;
        private SKBuiltinString_t _ImgEncryptKey = null;
        private SKBuiltinString_t _ImgSid = null;
        private SKBuiltinBuffer_t _KSid = null;
        private uint _OpCode;
        private string _Pwd1 = "";
        private string _Pwd2 = "";
        private uint _Scence = 0;
        private SKBuiltinBuffer_t _WTLoginReqBuff = null;
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

        [ProtoMember(6, IsRequired=false, Name="ImgCode", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ImgCode
        {
            get
            {
                return this._ImgCode;
            }
            set
            {
                this._ImgCode = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ImgEncryptKey
        {
            get
            {
                return this._ImgEncryptKey;
            }
            set
            {
                this._ImgEncryptKey = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ImgSid", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinString_t ImgSid
        {
            get
            {
                return this._ImgSid;
            }
            set
            {
                this._ImgSid = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="KSid", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t KSid
        {
            get
            {
                return this._KSid;
            }
            set
            {
                this._KSid = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="OpCode", DataFormat=DataFormat.TwosComplement)]
        public uint OpCode
        {
            get
            {
                return this._OpCode;
            }
            set
            {
                this._OpCode = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Pwd1", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd1
        {
            get
            {
                return this._Pwd1;
            }
            set
            {
                this._Pwd1 = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Pwd2", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd2
        {
            get
            {
                return this._Pwd2;
            }
            set
            {
                this._Pwd2 = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Scence", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint Scence
        {
            get
            {
                return this._Scence;
            }
            set
            {
                this._Scence = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="WTLoginReqBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t WTLoginReqBuff
        {
            get
            {
                return this._WTLoginReqBuff;
            }
            set
            {
                this._WTLoginReqBuff = value;
            }
        }
    }
}

