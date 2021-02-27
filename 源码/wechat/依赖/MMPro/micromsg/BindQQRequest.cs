namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindQQRequest")]
    public class BindQQRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private string _ImgCode = "";
        private SKBuiltinString_t _ImgEncryptKey = null;
        private string _ImgSid = "";
        private SKBuiltinBuffer_t _KSid = null;
        private uint _OPCode;
        private string _Pwd = "";
        private string _Pwd2 = "";
        private uint _QQ;
        private string _SafeDeviceName = "";
        private string _SafeDeviceType = "";
        private uint _SetAsMainAcct = 0;
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

        [ProtoMember(6, IsRequired=false, Name="ImgCode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgCode
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

        [ProtoMember(8, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(5, IsRequired=false, Name="ImgSid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgSid
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

        [ProtoMember(9, IsRequired=false, Name="KSid", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(7, IsRequired=true, Name="OPCode", DataFormat=DataFormat.TwosComplement)]
        public uint OPCode
        {
            get
            {
                return this._OPCode;
            }
            set
            {
                this._OPCode = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Pwd", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Pwd
        {
            get
            {
                return this._Pwd;
            }
            set
            {
                this._Pwd = value;
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

        [ProtoMember(2, IsRequired=true, Name="QQ", DataFormat=DataFormat.TwosComplement)]
        public uint QQ
        {
            get
            {
                return this._QQ;
            }
            set
            {
                this._QQ = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="SafeDeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SafeDeviceName
        {
            get
            {
                return this._SafeDeviceName;
            }
            set
            {
                this._SafeDeviceName = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="SafeDeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SafeDeviceType
        {
            get
            {
                return this._SafeDeviceType;
            }
            set
            {
                this._SafeDeviceType = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="SetAsMainAcct", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SetAsMainAcct
        {
            get
            {
                return this._SetAsMainAcct;
            }
            set
            {
                this._SetAsMainAcct = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="WTLoginReqBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

