namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ExtDeviceLoginConfirmOKRet")]
    public class ExtDeviceLoginConfirmOKRet : IExtensible
    {
        private string _ButtonCancelStr = "";
        private string _ButtonOkStr = "";
        private uint _ConfirmTimeOut = 0;
        private string _ContentStr = "";
        private uint _IconType = 0;
        private string _LoginedDevTip = "";
        private uint _ReqSessionLimit = 0;
        private string _TitleStr = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=false, Name="ButtonCancelStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ButtonCancelStr
        {
            get
            {
                return this._ButtonCancelStr;
            }
            set
            {
                this._ButtonCancelStr = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ButtonOkStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ButtonOkStr
        {
            get
            {
                return this._ButtonOkStr;
            }
            set
            {
                this._ButtonOkStr = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ConfirmTimeOut", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ConfirmTimeOut
        {
            get
            {
                return this._ConfirmTimeOut;
            }
            set
            {
                this._ConfirmTimeOut = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ContentStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ContentStr
        {
            get
            {
                return this._ContentStr;
            }
            set
            {
                this._ContentStr = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="IconType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint IconType
        {
            get
            {
                return this._IconType;
            }
            set
            {
                this._IconType = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="LoginedDevTip", DataFormat=DataFormat.Default), DefaultValue("")]
        public string LoginedDevTip
        {
            get
            {
                return this._LoginedDevTip;
            }
            set
            {
                this._LoginedDevTip = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ReqSessionLimit", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ReqSessionLimit
        {
            get
            {
                return this._ReqSessionLimit;
            }
            set
            {
                this._ReqSessionLimit = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="TitleStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TitleStr
        {
            get
            {
                return this._TitleStr;
            }
            set
            {
                this._TitleStr = value;
            }
        }
    }
}

