namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CDNUploadMsgImgCommitRequest")]
    public class CDNUploadMsgImgCommitRequest : IExtensible
    {
        private string _AESKey = "";
        private int _BigSize = 0;
        private int _EncryVer;
        private string _FileInfo = "";
        private int _Hit = 0;
        private string _ImgUrl = "";
        private string _Md5Sum = "";
        private int _MidSize;
        private int _SafeProto = 0;
        private int _ThumbSize;
        private string _Ticket = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(3, IsRequired=false, Name="AESKey", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=false, Name="BigSize", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int BigSize
        {
            get
            {
                return this._BigSize;
            }
            set
            {
                this._BigSize = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="EncryVer", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(13, IsRequired=false, Name="FileInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FileInfo
        {
            get
            {
                return this._FileInfo;
            }
            set
            {
                this._FileInfo = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="Hit", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Hit
        {
            get
            {
                return this._Hit;
            }
            set
            {
                this._Hit = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ImgUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ImgUrl
        {
            get
            {
                return this._ImgUrl;
            }
            set
            {
                this._ImgUrl = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Md5Sum", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Md5Sum
        {
            get
            {
                return this._Md5Sum;
            }
            set
            {
                this._Md5Sum = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="MidSize", DataFormat=DataFormat.TwosComplement)]
        public int MidSize
        {
            get
            {
                return this._MidSize;
            }
            set
            {
                this._MidSize = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="SafeProto", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int SafeProto
        {
            get
            {
                return this._SafeProto;
            }
            set
            {
                this._SafeProto = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="ThumbSize", DataFormat=DataFormat.TwosComplement)]
        public int ThumbSize
        {
            get
            {
                return this._ThumbSize;
            }
            set
            {
                this._ThumbSize = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="Ticket", DataFormat=DataFormat.Default), DefaultValue("")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                this._Ticket = value;
            }
        }
    }
}

