namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="HardDeviceAttr")]
    public class HardDeviceAttr : IExtensible
    {
        private string _Alias = "";
        private string _AuthKey = "";
        private long _bleSimpleProtocol = 0L;
        private string _BrandName = "";
        private string _Category = "";
        private uint _CloseStrategy = 0;
        private string _ConnProto = "";
        private uint _ConnStrategy = 0;
        private string _DeviceDesc = "";
        private string _DeviceTitle = "";
        private uint _DeviceTypeMainDevice = 0;
        private string _HardDeviceAttrDesc = "";
        private string _IconUrl = "";
        private uint _isEnterMyDevice = 0;
        private string _JumpUrl = "";
        private string _Mac = "";
        private int _ManuMacPos = 0;
        private int _SerMacPos = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(10, IsRequired=false, Name="Alias", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Alias
        {
            get
            {
                return this._Alias;
            }
            set
            {
                this._Alias = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="AuthKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AuthKey
        {
            get
            {
                return this._AuthKey;
            }
            set
            {
                this._AuthKey = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="bleSimpleProtocol", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public long bleSimpleProtocol
        {
            get
            {
                return this._bleSimpleProtocol;
            }
            set
            {
                this._bleSimpleProtocol = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="BrandName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string BrandName
        {
            get
            {
                return this._BrandName;
            }
            set
            {
                this._BrandName = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="Category", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Category
        {
            get
            {
                return this._Category;
            }
            set
            {
                this._Category = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="CloseStrategy", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CloseStrategy
        {
            get
            {
                return this._CloseStrategy;
            }
            set
            {
                this._CloseStrategy = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ConnProto", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ConnProto
        {
            get
            {
                return this._ConnProto;
            }
            set
            {
                this._ConnProto = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ConnStrategy", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ConnStrategy
        {
            get
            {
                return this._ConnStrategy;
            }
            set
            {
                this._ConnStrategy = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="DeviceDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceDesc
        {
            get
            {
                return this._DeviceDesc;
            }
            set
            {
                this._DeviceDesc = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="DeviceTitle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceTitle
        {
            get
            {
                return this._DeviceTitle;
            }
            set
            {
                this._DeviceTitle = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="DeviceTypeMainDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint DeviceTypeMainDevice
        {
            get
            {
                return this._DeviceTypeMainDevice;
            }
            set
            {
                this._DeviceTypeMainDevice = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="HardDeviceAttrDesc", DataFormat=DataFormat.Default), DefaultValue("")]
        public string HardDeviceAttrDesc
        {
            get
            {
                return this._HardDeviceAttrDesc;
            }
            set
            {
                this._HardDeviceAttrDesc = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="IconUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string IconUrl
        {
            get
            {
                return this._IconUrl;
            }
            set
            {
                this._IconUrl = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="isEnterMyDevice", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint isEnterMyDevice
        {
            get
            {
                return this._isEnterMyDevice;
            }
            set
            {
                this._isEnterMyDevice = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="JumpUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string JumpUrl
        {
            get
            {
                return this._JumpUrl;
            }
            set
            {
                this._JumpUrl = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Mac", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Mac
        {
            get
            {
                return this._Mac;
            }
            set
            {
                this._Mac = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="ManuMacPos", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int ManuMacPos
        {
            get
            {
                return this._ManuMacPos;
            }
            set
            {
                this._ManuMacPos = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="SerMacPos", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int SerMacPos
        {
            get
            {
                return this._SerMacPos;
            }
            set
            {
                this._SerMacPos = value;
            }
        }
    }
}

