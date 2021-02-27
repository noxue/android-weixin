namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="DownloadAppAttachRequest")]
    public class DownloadAppAttachRequest : IExtensible
    {
        private string _AppId = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _CDNType = 0;
        private uint _DataLen;
        private string _MediaId = "";
        private string _OutFmt = "";
        private int _Rotation = 0;
        private uint _SdkVersion;
        private uint _StartPos;
        private uint _TotalLen;
        private uint _Type = 0;
        private string _UserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(2, IsRequired=false, Name="AppId", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(12, IsRequired=false, Name="CDNType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CDNType
        {
            get
            {
                return this._CDNType;
            }
            set
            {
                this._CDNType = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="DataLen", DataFormat=DataFormat.TwosComplement)]
        public uint DataLen
        {
            get
            {
                return this._DataLen;
            }
            set
            {
                this._DataLen = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="MediaId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MediaId
        {
            get
            {
                return this._MediaId;
            }
            set
            {
                this._MediaId = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="OutFmt", DataFormat=DataFormat.Default), DefaultValue("")]
        public string OutFmt
        {
            get
            {
                return this._OutFmt;
            }
            set
            {
                this._OutFmt = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Rotation", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Rotation
        {
            get
            {
                return this._Rotation;
            }
            set
            {
                this._Rotation = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="SdkVersion", DataFormat=DataFormat.TwosComplement)]
        public uint SdkVersion
        {
            get
            {
                return this._SdkVersion;
            }
            set
            {
                this._SdkVersion = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=true, Name="TotalLen", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(11, IsRequired=false, Name="Type", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(5, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

