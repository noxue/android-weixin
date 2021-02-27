namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="CDNUploadMsgImgPrepareRequest")]
    public class CDNUploadMsgImgPrepareRequest : IExtensible
    {
        private string _AESKey = "";
        private string _AttachedContent = "";
        private string _ClientImgId = "";
        private SKBuiltinBuffer_t _ClientStat = null;
        private uint _CRC32 = 0;
        private int _EncryVer = 0;
        private string _FromUserName = "";
        private int _HDHeight = 0;
        private int _HDWidth = 0;
        private float _Latitude = 0f;
        private float _Longitude = 0f;
        private int _MidHeight = 0;
        private int _MidWidth = 0;
        private uint _MsgForwardType = 0;
        private string _MsgSource = "";
        private int _Scene = 0;
        private int _ThumbHeight;
        private int _ThumbWidth;
        private string _ToUserName = "";
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x10, IsRequired=false, Name="AESKey", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(11, IsRequired=false, Name="AttachedContent", DataFormat=DataFormat.Default), DefaultValue("")]
        public string AttachedContent
        {
            get
            {
                return this._AttachedContent;
            }
            set
            {
                this._AttachedContent = value;
            }
        }

        [ProtoMember(1, IsRequired=false, Name="ClientImgId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientImgId
        {
            get
            {
                return this._ClientImgId;
            }
            set
            {
                this._ClientImgId = value;
            }
        }

        [ProtoMember(7, IsRequired=false, Name="ClientStat", DataFormat=DataFormat.Default), DefaultValue((string) null)]
        public SKBuiltinBuffer_t ClientStat
        {
            get
            {
                return this._ClientStat;
            }
            set
            {
                this._ClientStat = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="CRC32", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CRC32
        {
            get
            {
                return this._CRC32;
            }
            set
            {
                this._CRC32 = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="EncryVer", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(2, IsRequired=false, Name="FromUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string FromUserName
        {
            get
            {
                return this._FromUserName;
            }
            set
            {
                this._FromUserName = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="HDHeight", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int HDHeight
        {
            get
            {
                return this._HDHeight;
            }
            set
            {
                this._HDHeight = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="HDWidth", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int HDWidth
        {
            get
            {
                return this._HDWidth;
            }
            set
            {
                this._HDWidth = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="Latitude", DataFormat=DataFormat.FixedSize), DefaultValue((float) 0f)]
        public float Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                this._Latitude = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="Longitude", DataFormat=DataFormat.FixedSize), DefaultValue((float) 0f)]
        public float Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                this._Longitude = value;
            }
        }

        [ProtoMember(12, IsRequired=false, Name="MidHeight", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int MidHeight
        {
            get
            {
                return this._MidHeight;
            }
            set
            {
                this._MidHeight = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="MidWidth", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int MidWidth
        {
            get
            {
                return this._MidWidth;
            }
            set
            {
                this._MidWidth = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="MsgForwardType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint MsgForwardType
        {
            get
            {
                return this._MsgForwardType;
            }
            set
            {
                this._MsgForwardType = value;
            }
        }

        [ProtoMember(6, IsRequired=false, Name="MsgSource", DataFormat=DataFormat.Default), DefaultValue("")]
        public string MsgSource
        {
            get
            {
                return this._MsgSource;
            }
            set
            {
                this._MsgSource = value;
            }
        }

        [ProtoMember(8, IsRequired=false, Name="Scene", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int Scene
        {
            get
            {
                return this._Scene;
            }
            set
            {
                this._Scene = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="ThumbHeight", DataFormat=DataFormat.TwosComplement)]
        public int ThumbHeight
        {
            get
            {
                return this._ThumbHeight;
            }
            set
            {
                this._ThumbHeight = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="ThumbWidth", DataFormat=DataFormat.TwosComplement)]
        public int ThumbWidth
        {
            get
            {
                return this._ThumbWidth;
            }
            set
            {
                this._ThumbWidth = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToUserName
        {
            get
            {
                return this._ToUserName;
            }
            set
            {
                this._ToUserName = value;
            }
        }
    }
}

