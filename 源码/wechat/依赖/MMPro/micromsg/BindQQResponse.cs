namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindQQResponse")]
    public class BindQQResponse : IExtensible
    {
        private SKBuiltinBuffer_t _A2Key = null;
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _ImgBuf;
        private SKBuiltinString_t _ImgEncryptKey = null;
        private string _ImgSid = "";
        private SKBuiltinBuffer_t _KSid = null;
        private string _MicroBlogName = "";
        private uint _PrivateMsgStatus = 0;
        private uint _PushMailStatus = 0;
        private string _QQMailSkey = "";
        private uint _SafeDevice = 0;
        private micromsg.SafeDeviceList _SafeDeviceList = null;
        private uint _Status = 0;
        private SKBuiltinBuffer_t _WTLoginRspBuff = null;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(10, IsRequired=false, Name="A2Key", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(3, IsRequired=true, Name="ImgBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ImgBuf
        {
            get
            {
                return this._ImgBuf;
            }
            set
            {
                this._ImgBuf = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="ImgEncryptKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(2, IsRequired=false, Name="ImgSid", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="KSid", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(6, IsRequired=false, Name="MicroBlogName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MicroBlogName
        {
            get
            {
                return this._MicroBlogName;
            }
            set
            {
                this._MicroBlogName = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="PrivateMsgStatus", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PrivateMsgStatus
        {
            get
            {
                return this._PrivateMsgStatus;
            }
            set
            {
                this._PrivateMsgStatus = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="PushMailStatus", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint PushMailStatus
        {
            get
            {
                return this._PushMailStatus;
            }
            set
            {
                this._PushMailStatus = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="QQMailSkey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string QQMailSkey
        {
            get
            {
                return this._QQMailSkey;
            }
            set
            {
                this._QQMailSkey = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="SafeDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SafeDevice
        {
            get
            {
                return this._SafeDevice;
            }
            set
            {
                this._SafeDevice = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="SafeDeviceList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.SafeDeviceList SafeDeviceList
        {
            get
            {
                return this._SafeDeviceList;
            }
            set
            {
                this._SafeDeviceList = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Status", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(14, IsRequired=false, Name="WTLoginRspBuff", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t WTLoginRspBuff
        {
            get
            {
                return this._WTLoginRspBuff;
            }
            set
            {
                this._WTLoginRspBuff = value;
            }
        }
    }
}

