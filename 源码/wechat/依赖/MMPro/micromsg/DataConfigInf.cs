namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DataConfigInf")]
    public class DataConfigInf : IExtensible
    {
        private uint _CreateTime = 0;
        private string _Deviceid = "";
        private BackupStartGeneralInfo _DeviceInfo = null;
        private uint _LastModifyTime = 0;
        private string _UserName;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="CreateTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="Deviceid", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Deviceid
        {
            get
            {
                return this._Deviceid;
            }
            set
            {
                this._Deviceid = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="DeviceInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public BackupStartGeneralInfo DeviceInfo
        {
            get
            {
                return this._DeviceInfo;
            }
            set
            {
                this._DeviceInfo = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="LastModifyTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint LastModifyTime
        {
            get
            {
                return this._LastModifyTime;
            }
            set
            {
                this._LastModifyTime = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="UserName", DataFormat=DataFormat.Default)]
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
    }
}

