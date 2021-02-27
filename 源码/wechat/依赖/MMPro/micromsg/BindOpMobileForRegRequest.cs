namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindOpMobileForRegRequest")]
    public class BindOpMobileForRegRequest : IExtensible
    {
        private uint _AdjustRet = 0;
        private string _AuthTicket = "";
        private micromsg.BaseRequest _BaseRequest;
        private string _ClientSeqID = "";
        private int _DialFlag = 0;
        private string _DialLang = "";
        private uint _ForceReg = 0;
        private uint _InputMobileRetrys = 0;
        private string _Language = "";
        private string _Mobile = "";
        private uint _MobileCheckType;
        private int _Opcode = 0;
        private SKBuiltinBuffer_t _RandomEncryKey = null;
        private string _regSessionId = "";
        private string _SafeDeviceName = "";
        private string _SafeDeviceType = "";
        private string _UserName = "";
        private string _Verifycode = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(15, IsRequired=false, Name="AdjustRet", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AdjustRet
        {
            get
            {
                return this._AdjustRet;
            }
            set
            {
                this._AdjustRet = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="AuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthTicket
        {
            get
            {
                return this._AuthTicket;
            }
            set
            {
                this._AuthTicket = value;
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

        [ProtoMember(0x10, IsRequired=false, Name="ClientSeqID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientSeqID
        {
            get
            {
                return this._ClientSeqID;
            }
            set
            {
                this._ClientSeqID = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="DialFlag", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int DialFlag
        {
            get
            {
                return this._DialFlag;
            }
            set
            {
                this._DialFlag = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="DialLang", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DialLang
        {
            get
            {
                return this._DialLang;
            }
            set
            {
                this._DialLang = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="ForceReg", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ForceReg
        {
            get
            {
                return this._ForceReg;
            }
            set
            {
                this._ForceReg = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="InputMobileRetrys", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint InputMobileRetrys
        {
            get
            {
                return this._InputMobileRetrys;
            }
            set
            {
                this._InputMobileRetrys = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="Language", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Language
        {
            get
            {
                return this._Language;
            }
            set
            {
                this._Language = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Mobile", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Mobile
        {
            get
            {
                return this._Mobile;
            }
            set
            {
                this._Mobile = value;
            }
        }

        [ProtoMember(0x11, IsRequired=true, Name="MobileCheckType", DataFormat=DataFormat.TwosComplement)]
        public uint MobileCheckType
        {
            get
            {
                return this._MobileCheckType;
            }
            set
            {
                this._MobileCheckType = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="Opcode", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Opcode
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

        [ProtoMember(12, IsRequired=false, Name="RandomEncryKey", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(0x12, IsRequired=false, Name="regSessionId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string regSessionId
        {
            get
            {
                return this._regSessionId;
            }
            set
            {
                this._regSessionId = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="SafeDeviceName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="SafeDeviceType", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Verifycode", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Verifycode
        {
            get
            {
                return this._Verifycode;
            }
            set
            {
                this._Verifycode = value;
            }
        }
    }
}

