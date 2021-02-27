namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="BackupStartResponse")]
    public class BackupStartResponse : IExtensible
    {
        private ulong _BigDataSize = 0L;
        private BackupStartGeneralInfo _GeneralInfo = null;
        private string _ID;
        private uint _NetworkState = 0;
        private int _Status;
        private ulong _TotalCount;
        private ulong _TotalSize;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=false, Name="BigDataSize", DataFormat=DataFormat.TwosComplement), DefaultValue((float) 0f)]
        public ulong BigDataSize
        {
            get
            {
                return this._BigDataSize;
            }
            set
            {
                this._BigDataSize = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="GeneralInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public BackupStartGeneralInfo GeneralInfo
        {
            get
            {
                return this._GeneralInfo;
            }
            set
            {
                this._GeneralInfo = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="ID", DataFormat=DataFormat.Default)]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="NetworkState", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NetworkState
        {
            get
            {
                return this._NetworkState;
            }
            set
            {
                this._NetworkState = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
        public int Status
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

        [ProtoMember(2, IsRequired=true, Name="TotalCount", DataFormat=DataFormat.TwosComplement)]
        public ulong TotalCount
        {
            get
            {
                return this._TotalCount;
            }
            set
            {
                this._TotalCount = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="TotalSize", DataFormat=DataFormat.TwosComplement)]
        public ulong TotalSize
        {
            get
            {
                return this._TotalSize;
            }
            set
            {
                this._TotalSize = value;
            }
        }
    }
}

