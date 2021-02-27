namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BindOpMobileResponse")]
    public class BindOpMobileResponse : IExtensible
    {
        private string _AuthTicket = "";
        private micromsg.BaseResponse _BaseResponse;
        private micromsg.BuiltinIPList _BuiltinIPList = null;
        private string _CC = "";
        private string _FormatedMobile = "";
        private uint _NeedSetPwd = 0;
        private micromsg.NetworkControl _NetworkControl = null;
        private HostList _NewHostList = null;
        private uint _ObsoleteItem1 = 0;
        private string _PureMobile = "";
        private string _Pwd = "";
        private uint _SafeDevice = 0;
        private micromsg.SafeDeviceList _SafeDeviceList = null;
        private ShowStyleKey _ShowStyle = null;
        private string _SmsNo = "";
        private string _ticket = "";
        private string _Username = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(10, IsRequired=false, Name="AuthTicket", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(8, IsRequired=false, Name="BuiltinIPList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.BuiltinIPList BuiltinIPList
        {
            get
            {
                return this._BuiltinIPList;
            }
            set
            {
                this._BuiltinIPList = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="CC", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CC
        {
            get
            {
                return this._CC;
            }
            set
            {
                this._CC = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="FormatedMobile", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FormatedMobile
        {
            get
            {
                return this._FormatedMobile;
            }
            set
            {
                this._FormatedMobile = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="NeedSetPwd", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NeedSetPwd
        {
            get
            {
                return this._NeedSetPwd;
            }
            set
            {
                this._NeedSetPwd = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="NetworkControl", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.NetworkControl NetworkControl
        {
            get
            {
                return this._NetworkControl;
            }
            set
            {
                this._NetworkControl = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="NewHostList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public HostList NewHostList
        {
            get
            {
                return this._NewHostList;
            }
            set
            {
                this._NewHostList = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="ObsoleteItem1", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ObsoleteItem1
        {
            get
            {
                return this._ObsoleteItem1;
            }
            set
            {
                this._ObsoleteItem1 = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="PureMobile", DataFormat=DataFormat.Default), DefaultValue("")]
        public string PureMobile
        {
            get
            {
                return this._PureMobile;
            }
            set
            {
                this._PureMobile = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="Pwd", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="SafeDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(14, IsRequired=false, Name="SafeDeviceList", DataFormat=DataFormat.Default), DefaultValue((string) null)]
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

        [ProtoMember(0x11, IsRequired=false, Name="ShowStyle", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public ShowStyleKey ShowStyle
        {
            get
            {
                return this._ShowStyle;
            }
            set
            {
                this._ShowStyle = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="SmsNo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SmsNo
        {
            get
            {
                return this._SmsNo;
            }
            set
            {
                this._SmsNo = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ticket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ticket
        {
            get
            {
                return this._ticket;
            }
            set
            {
                this._ticket = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="Username", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
            }
        }
    }
}

