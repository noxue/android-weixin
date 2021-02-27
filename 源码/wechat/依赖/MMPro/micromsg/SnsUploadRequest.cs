namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="SnsUploadRequest")]
    public class SnsUploadRequest : IExtensible
    {
        private string _AppId = "";
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _Buffer;
        private string _ClientId = "";
        private string _Description = "";
        private uint _ExtFlag = 0;
        private uint _FilterStype = 0;
        private string _MD5 = "";
        private int _NetType = 0;
        private int _PhotoFrom = 0;
        private uint _StartPos;
        private uint _SyncFlag = 0;
        private uint _TotalLen;
        private micromsg.TwitterInfo _TwitterInfo = null;
        private uint _Type;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(13, IsRequired=false, Name="AppId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AppId
        {
            get
            {
                return this._AppId;
            }
            set
            {
                this._AppId = value;
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

        [ProtoMember(5, IsRequired=true, Name="Buffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Buffer
        {
            get
            {
                return this._Buffer;
            }
            set
            {
                this._Buffer = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="ClientId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Description", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="ExtFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ExtFlag
        {
            get
            {
                return this._ExtFlag;
            }
            set
            {
                this._ExtFlag = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="FilterStype", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FilterStype
        {
            get
            {
                return this._FilterStype;
            }
            set
            {
                this._FilterStype = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="MD5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MD5
        {
            get
            {
                return this._MD5;
            }
            set
            {
                this._MD5 = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="NetType", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int NetType
        {
            get
            {
                return this._NetType;
            }
            set
            {
                this._NetType = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="PhotoFrom", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int PhotoFrom
        {
            get
            {
                return this._PhotoFrom;
            }
            set
            {
                this._PhotoFrom = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
        public uint StartPos
        {
            get
            {
                return this._StartPos;
            }
            set
            {
                this._StartPos = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="SyncFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SyncFlag
        {
            get
            {
                return this._SyncFlag;
            }
            set
            {
                this._SyncFlag = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint TotalLen
        {
            get
            {
                return this._TotalLen;
            }
            set
            {
                this._TotalLen = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="TwitterInfo", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public micromsg.TwitterInfo TwitterInfo
        {
            get
            {
                return this._TwitterInfo;
            }
            set
            {
                this._TwitterInfo = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

