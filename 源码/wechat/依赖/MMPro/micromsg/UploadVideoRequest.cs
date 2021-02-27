namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadVideoRequest")]
    public class UploadVideoRequest : IExtensible
    {
        private string _AESKey = "";
        private micromsg.BaseRequest _BaseRequest;
        private uint _CameraType = 0;
        private string _CDNThumbAESKey = "";
        private int _CDNThumbImgHeight = 0;
        private int _CDNThumbImgSize = 0;
        private int _CDNThumbImgWidth = 0;
        private string _CDNThumbUrl = "";
        private string _CDNVideoUrl = "";
        private string _ClientMsgId = "";
        private uint _CRC32 = 0;
        private int _EncryVer = 0;
        private string _FromUserName = "";
        private uint _FuncFlag = 0;
        private uint _HitMd5 = 0;
        private uint _MsgForwardType = 0;
        private string _MsgSource = "";
        private uint _NetworkEnv = 0;
        private uint _PlayLength;
        private uint _ReqTime = 0;
        private string _StatExtStr = "";
        private string _StreamVideoAdUxInfo = "";
        private string _StreamVideoPublishId = "";
        private string _StreamVideoThumbUrl = "";
        private string _StreamVideoTitle = "";
        private uint _StreamVideoTotalTime = 0;
        private string _StreamVideoUrl = "";
        private string _StreamVideoWebUrl = "";
        private string _StreamVideoWording = "";
        private SKBuiltinBuffer_t _ThumbData;
        private uint _ThumbStartPos;
        private uint _ThumbTotalLen;
        private string _ToUserName = "";
        private SKBuiltinBuffer_t _VideoData;
        private int _VideoFrom = 0;
        private string _VideoMd5 = "";
        private string _VideoNewMd5 = "";
        private uint _VideoStartPos;
        private uint _VideoTotalLen;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(0x11, IsRequired=false, Name="AESKey", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(13, IsRequired=false, Name="CameraType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x17, IsRequired=false, Name="CDNThumbAESKey", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CDNThumbAESKey
        {
            get
            {
                return this._CDNThumbAESKey;
            }
            set
            {
                this._CDNThumbAESKey = value;
            }
        }

        [ProtoMember(0x15, IsRequired=false, Name="CDNThumbImgHeight", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int CDNThumbImgHeight
        {
            get
            {
                return this._CDNThumbImgHeight;
            }
            set
            {
                this._CDNThumbImgHeight = value;
            }
        }

        [ProtoMember(20, IsRequired=false, Name="CDNThumbImgSize", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int CDNThumbImgSize
        {
            get
            {
                return this._CDNThumbImgSize;
            }
            set
            {
                this._CDNThumbImgSize = value;
            }
        }

        [ProtoMember(0x16, IsRequired=false, Name="CDNThumbImgWidth", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int CDNThumbImgWidth
        {
            get
            {
                return this._CDNThumbImgWidth;
            }
            set
            {
                this._CDNThumbImgWidth = value;
            }
        }

        [ProtoMember(0x13, IsRequired=false, Name="CDNThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CDNThumbUrl
        {
            get
            {
                return this._CDNThumbUrl;
            }
            set
            {
                this._CDNThumbUrl = value;
            }
        }

        [ProtoMember(0x10, IsRequired=false, Name="CDNVideoUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string CDNVideoUrl
        {
            get
            {
                return this._CDNVideoUrl;
            }
            set
            {
                this._CDNVideoUrl = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="ClientMsgId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientMsgId
        {
            get
            {
                return this._ClientMsgId;
            }
            set
            {
                this._ClientMsgId = value;
            }
        }

        [ProtoMember(0x26, IsRequired=false, Name="CRC32", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(0x12, IsRequired=false, Name="EncryVer", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
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

        [ProtoMember(3, IsRequired=false, Name="FromUserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(14, IsRequired=false, Name="FuncFlag", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint FuncFlag
        {
            get
            {
                return this._FuncFlag;
            }
            set
            {
                this._FuncFlag = value;
            }
        }

        [ProtoMember(0x24, IsRequired=false, Name="HitMd5", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint HitMd5
        {
            get
            {
                return this._HitMd5;
            }
            set
            {
                this._HitMd5 = value;
            }
        }

        [ProtoMember(0x27, IsRequired=false, Name="MsgForwardType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
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

        [ProtoMember(15, IsRequired=false, Name="MsgSource", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(12, IsRequired=false, Name="NetworkEnv", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint NetworkEnv
        {
            get
            {
                return this._NetworkEnv;
            }
            set
            {
                this._NetworkEnv = value;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="PlayLength", DataFormat=DataFormat.TwosComplement)]
        public uint PlayLength
        {
            get
            {
                return this._PlayLength;
            }
            set
            {
                this._PlayLength = value;
            }
        }

        [ProtoMember(0x19, IsRequired=false, Name="ReqTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ReqTime
        {
            get
            {
                return this._ReqTime;
            }
            set
            {
                this._ReqTime = value;
            }
        }

        [ProtoMember(0x23, IsRequired=false, Name="StatExtStr", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StatExtStr
        {
            get
            {
                return this._StatExtStr;
            }
            set
            {
                this._StatExtStr = value;
            }
        }

        [ProtoMember(0x22, IsRequired=false, Name="StreamVideoAdUxInfo", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoAdUxInfo
        {
            get
            {
                return this._StreamVideoAdUxInfo;
            }
            set
            {
                this._StreamVideoAdUxInfo = value;
            }
        }

        [ProtoMember(0x21, IsRequired=false, Name="StreamVideoPublishId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoPublishId
        {
            get
            {
                return this._StreamVideoPublishId;
            }
            set
            {
                this._StreamVideoPublishId = value;
            }
        }

        [ProtoMember(0x20, IsRequired=false, Name="StreamVideoThumbUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoThumbUrl
        {
            get
            {
                return this._StreamVideoThumbUrl;
            }
            set
            {
                this._StreamVideoThumbUrl = value;
            }
        }

        [ProtoMember(0x1d, IsRequired=false, Name="StreamVideoTitle", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoTitle
        {
            get
            {
                return this._StreamVideoTitle;
            }
            set
            {
                this._StreamVideoTitle = value;
            }
        }

        [ProtoMember(0x1c, IsRequired=false, Name="StreamVideoTotalTime", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint StreamVideoTotalTime
        {
            get
            {
                return this._StreamVideoTotalTime;
            }
            set
            {
                this._StreamVideoTotalTime = value;
            }
        }

        [ProtoMember(0x1b, IsRequired=false, Name="StreamVideoUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoUrl
        {
            get
            {
                return this._StreamVideoUrl;
            }
            set
            {
                this._StreamVideoUrl = value;
            }
        }

        [ProtoMember(0x1f, IsRequired=false, Name="StreamVideoWebUrl", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoWebUrl
        {
            get
            {
                return this._StreamVideoWebUrl;
            }
            set
            {
                this._StreamVideoWebUrl = value;
            }
        }

        [ProtoMember(30, IsRequired=false, Name="StreamVideoWording", DataFormat=DataFormat.Default), DefaultValue("")]
        public string StreamVideoWording
        {
            get
            {
                return this._StreamVideoWording;
            }
            set
            {
                this._StreamVideoWording = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="ThumbData", DataFormat=DataFormat.Default)]
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

        [ProtoMember(6, IsRequired=true, Name="ThumbStartPos", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=true, Name="ThumbTotalLen", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(4, IsRequired=false, Name="ToUserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(10, IsRequired=true, Name="VideoData", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t VideoData
        {
            get
            {
                return this._VideoData;
            }
            set
            {
                this._VideoData = value;
            }
        }

        [ProtoMember(0x18, IsRequired=false, Name="VideoFrom", DataFormat=DataFormat.TwosComplement), DefaultValue(0)]
        public int VideoFrom
        {
            get
            {
                return this._VideoFrom;
            }
            set
            {
                this._VideoFrom = value;
            }
        }

        [ProtoMember(0x1a, IsRequired=false, Name="VideoMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VideoMd5
        {
            get
            {
                return this._VideoMd5;
            }
            set
            {
                this._VideoMd5 = value;
            }
        }

        [ProtoMember(0x25, IsRequired=false, Name="VideoNewMd5", DataFormat=DataFormat.Default), DefaultValue("")]
        public string VideoNewMd5
        {
            get
            {
                return this._VideoNewMd5;
            }
            set
            {
                this._VideoNewMd5 = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="VideoStartPos", DataFormat=DataFormat.TwosComplement)]
        public uint VideoStartPos
        {
            get
            {
                return this._VideoStartPos;
            }
            set
            {
                this._VideoStartPos = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="VideoTotalLen", DataFormat=DataFormat.TwosComplement)]
        public uint VideoTotalLen
        {
            get
            {
                return this._VideoTotalLen;
            }
            set
            {
                this._VideoTotalLen = value;
            }
        }
    }
}

