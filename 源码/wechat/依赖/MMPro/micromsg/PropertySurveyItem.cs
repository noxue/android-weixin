namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PropertySurveyItem")]
    public class PropertySurveyItem : IExtensible
    {
        private uint _AvgElapsedTime;
        private uint _BeginTime;
        private string _DeviceModel = "";
        private uint _EndTime;
        private string _Expand = "";
        private uint _MaxElapsedTime;
        private uint _MinElapsedTime;
        private string _Module = "";
        private string _OsType = "";
        private string _SubModule = "";
        private uint _UseModuleCount;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(5, IsRequired=true, Name="AvgElapsedTime", DataFormat=DataFormat.TwosComplement)]
        public uint AvgElapsedTime
        {
            get
            {
                return this._AvgElapsedTime;
            }
            set
            {
                this._AvgElapsedTime = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="BeginTime", DataFormat=DataFormat.TwosComplement)]
        public uint BeginTime
        {
            get
            {
                return this._BeginTime;
            }
            set
            {
                this._BeginTime = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="DeviceModel", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DeviceModel
        {
            get
            {
                return this._DeviceModel;
            }
            set
            {
                this._DeviceModel = value;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="EndTime", DataFormat=DataFormat.TwosComplement)]
        public uint EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                this._EndTime = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Expand", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Expand
        {
            get
            {
                return this._Expand;
            }
            set
            {
                this._Expand = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="MaxElapsedTime", DataFormat=DataFormat.TwosComplement)]
        public uint MaxElapsedTime
        {
            get
            {
                return this._MaxElapsedTime;
            }
            set
            {
                this._MaxElapsedTime = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="MinElapsedTime", DataFormat=DataFormat.TwosComplement)]
        public uint MinElapsedTime
        {
            get
            {
                return this._MinElapsedTime;
            }
            set
            {
                this._MinElapsedTime = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Module", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Module
        {
            get
            {
                return this._Module;
            }
            set
            {
                this._Module = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="OsType", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OsType
        {
            get
            {
                return this._OsType;
            }
            set
            {
                this._OsType = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="SubModule", DataFormat=DataFormat.Default), DefaultValue("")]
        public string SubModule
        {
            get
            {
                return this._SubModule;
            }
            set
            {
                this._SubModule = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="UseModuleCount", DataFormat=DataFormat.TwosComplement)]
        public uint UseModuleCount
        {
            get
            {
                return this._UseModuleCount;
            }
            set
            {
                this._UseModuleCount = value;
            }
        }
    }
}

