namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="FavCDNItem")]
    public class FavCDNItem : IExtensible
    {
        private string _AESKey = "";
        private string _CDNURL = "";
        private string _DataId = "";
        private int _DataStatus;
        private int _EncryVer;
        private string _FullMd5 = "";
        private uint _FullSize;
        private string _Head256Md5 = "";
        private int _Status;
        private string _VideoId = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=false, Name="AESKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AESKey
        {
            get
            {
                return this._AESKey;
            }
            set
            {
                this._AESKey = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="CDNURL", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CDNURL
        {
            get
            {
                return this._CDNURL;
            }
            set
            {
                this._CDNURL = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="DataId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string DataId
        {
            get
            {
                return this._DataId;
            }
            set
            {
                this._DataId = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="DataStatus", DataFormat=DataFormat.TwosComplement)]
        public int DataStatus
        {
            get
            {
                return this._DataStatus;
            }
            set
            {
                this._DataStatus = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="EncryVer", DataFormat=DataFormat.TwosComplement)]
        public int EncryVer
        {
            get
            {
                return this._EncryVer;
            }
            set
            {
                this._EncryVer = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="FullMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FullMd5
        {
            get
            {
                return this._FullMd5;
            }
            set
            {
                this._FullMd5 = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="FullSize", DataFormat=DataFormat.TwosComplement)]
        public uint FullSize
        {
            get
            {
                return this._FullSize;
            }
            set
            {
                this._FullSize = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="Head256Md5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Head256Md5
        {
            get
            {
                return this._Head256Md5;
            }
            set
            {
                this._Head256Md5 = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="Status", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(8, IsRequired=false, Name="VideoId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VideoId
        {
            get
            {
                return this._VideoId;
            }
            set
            {
                this._VideoId = value;
            }
        }
    }
}

