namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="PacketBackupDataPush")]
    public class PacketBackupDataPush : IExtensible
    {
        private int _CheckSum = 0;
        private byte[] _Data = null;
        private string _DataID;
        private int _DataSize;
        private int _DataType;
        private int _EndOffset;
        private int _Progress = 0;
        private int _StartOffset;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=false, Name="CheckSum", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int CheckSum
        {
            get
            {
                return this._CheckSum;
            }
            set
            {
                this._CheckSum = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="Data", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public byte[] Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="DataID", DataFormat=DataFormat.Default)]
        public string DataID
        {
            get
            {
                return this._DataID;
            }
            set
            {
                this._DataID = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="DataSize", DataFormat=DataFormat.TwosComplement)]
        public int DataSize
        {
            get
            {
                return this._DataSize;
            }
            set
            {
                this._DataSize = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="DataType", DataFormat=DataFormat.TwosComplement)]
        public int DataType
        {
            get
            {
                return this._DataType;
            }
            set
            {
                this._DataType = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="EndOffset", DataFormat=DataFormat.TwosComplement)]
        public int EndOffset
        {
            get
            {
                return this._EndOffset;
            }
            set
            {
                this._EndOffset = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Progress", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Progress
        {
            get
            {
                return this._Progress;
            }
            set
            {
                this._Progress = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="StartOffset", DataFormat=DataFormat.TwosComplement)]
        public int StartOffset
        {
            get
            {
                return this._StartOffset;
            }
            set
            {
                this._StartOffset = value;
            }
        }
    }
}

