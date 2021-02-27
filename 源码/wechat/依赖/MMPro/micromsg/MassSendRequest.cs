namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="MassSendRequest")]
    public class MassSendRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _CameraType;
        private string _ClientID = "";
        private uint _CompressType = 0;
        private SKBuiltinBuffer_t _DataBuffer;
        private uint _DataStartPos;
        private uint _DataTotalLen;
        private uint _IsSendAgain;
        private string _MD5 = "";
        private uint _MediaTime;
        private uint _MsgType;
        private string _ThumbAESKey = "";
        private SKBuiltinBuffer_t _ThumbData;
        private uint _ThumbHeight = 0;
        private uint _ThumbStartPos;
        private uint _ThumbTotalLen;
        private string _ThumbUrl = "";
        private uint _ThumbWidth = 0;
        private string _ToList = "";
        private uint _ToListCount;
        private string _ToListMD5 = "";
        private string _VideoAESKey = "";
        private uint _VideoSource;
        private string _VideoUrl = "";
        private uint _VoiceFormat = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
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

        [ProtoMember(13, IsRequired=true, Name="CameraType", DataFormat=DataFormat.TwosComplement)]
        public uint CameraType
        {
            get
            {
                return this._CameraType;
            }
            set
            {
                this._CameraType = value;
            }
        }

        [ProtoMember(4, IsRequired=false, Name="ClientID", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientID
        {
            get
            {
                return this._ClientID;
            }
            set
            {
                this._ClientID = value;
            }
        }

        [ProtoMember(0x11, IsRequired=false, Name="CompressType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint CompressType
        {
            get
            {
                return this._CompressType;
            }
            set
            {
                this._CompressType = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="DataBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t DataBuffer
        {
            get
            {
                return this._DataBuffer;
            }
            set
            {
                this._DataBuffer = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="DataStartPos", DataFormat=DataFormat.TwosComplement)]
        public uint DataStartPos
        {
            get
            {
                return this._DataStartPos;
            }
            set
            {
                this._DataStartPos = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="DataTotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint DataTotalLen
        {
            get
            {
                return this._DataTotalLen;
            }
            set
            {
                this._DataTotalLen = value;
            }
        }

        [ProtoMember(0x10, IsRequired=true, Name="IsSendAgain", DataFormat=DataFormat.TwosComplement)]
        public uint IsSendAgain
        {
            get
            {
                return this._IsSendAgain;
            }
            set
            {
                this._IsSendAgain = value;
            }
        }

        [ProtoMember(0x19, IsRequired=false, Name="MD5", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(6, IsRequired=true, Name="MediaTime", DataFormat=DataFormat.TwosComplement)]
        public uint MediaTime
        {
            get
            {
                return this._MediaTime;
            }
            set
            {
                this._MediaTime = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="MsgType", DataFormat=DataFormat.TwosComplement)]
        public uint MsgType
        {
            get
            {
                return this._MsgType;
            }
            set
            {
                this._MsgType = value;
            }
        }

        [ProtoMember(0x17, IsRequired=false, Name="ThumbAESKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ThumbAESKey
        {
            get
            {
                return this._ThumbAESKey;
            }
            set
            {
                this._ThumbAESKey = value;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="ThumbData", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ThumbData
        {
            get
            {
                return this._ThumbData;
            }
            set
            {
                this._ThumbData = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="ThumbHeight", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ThumbHeight
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

        [ProtoMember(11, IsRequired=true, Name="ThumbStartPos", DataFormat=DataFormat.TwosComplement)]
        public uint ThumbStartPos
        {
            get
            {
                return this._ThumbStartPos;
            }
            set
            {
                this._ThumbStartPos = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="ThumbTotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint ThumbTotalLen
        {
            get
            {
                return this._ThumbTotalLen;
            }
            set
            {
                this._ThumbTotalLen = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="ThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ThumbUrl
        {
            get
            {
                return this._ThumbUrl;
            }
            set
            {
                this._ThumbUrl = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="ThumbWidth", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ThumbWidth
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

        [ProtoMember(2, IsRequired=false, Name="ToList", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToList
        {
            get
            {
                return this._ToList;
            }
            set
            {
                this._ToList = value;
            }
        }

        [ProtoMember(15, IsRequired=true, Name="ToListCount", DataFormat=DataFormat.TwosComplement)]
        public uint ToListCount
        {
            get
            {
                return this._ToListCount;
            }
            set
            {
                this._ToListCount = value;
            }
        }

        [ProtoMember(3, IsRequired=false, Name="ToListMD5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ToListMD5
        {
            get
            {
                return this._ToListMD5;
            }
            set
            {
                this._ToListMD5 = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="VideoAESKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VideoAESKey
        {
            get
            {
                return this._VideoAESKey;
            }
            set
            {
                this._VideoAESKey = value;
            }
        }

        [ProtoMember(14, IsRequired=true, Name="VideoSource", DataFormat=DataFormat.TwosComplement)]
        public uint VideoSource
        {
            get
            {
                return this._VideoSource;
            }
            set
            {
                this._VideoSource = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="VideoUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VideoUrl
        {
            get
            {
                return this._VideoUrl;
            }
            set
            {
                this._VideoUrl = value;
            }
        }

        [ProtoMember(0x12, IsRequired=false, Name="VoiceFormat", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint VoiceFormat
        {
            get
            {
                return this._VoiceFormat;
            }
            set
            {
                this._VoiceFormat = value;
            }
        }
    }
}

