namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="ShakeMusicResponse")]
    public class ShakeMusicResponse : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private uint _EndFlag;
        private uint _MusicId;
        private float _Offset;
        private uint _ResultType = 0;
        private SKBuiltinBuffer_t _SongAlbum;
        private SKBuiltinBuffer_t _SongAlbumUrl;
        private SKBuiltinBuffer_t _SongLyric;
        private SKBuiltinBuffer_t _SongName;
        private SKBuiltinBuffer_t _SongSinger;
        private SKBuiltinBuffer_t _SongWapLinkUrl;
        private SKBuiltinBuffer_t _SongWebUrl;
        private SKBuiltinBuffer_t _SongWifiUrl;
        private string _TVDescriptionXML = "";
        private uint _TVType = 0;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(1, IsRequired=true, Name="BaseResponse", DataFormat=DataFormat.Default)]
        public micromsg.BaseResponse BaseResponse
        {
            get
            {
                return this._BaseResponse;
            }
            set
            {
                this._BaseResponse = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="EndFlag", DataFormat=DataFormat.TwosComplement)]
        public uint EndFlag
        {
            get
            {
                return this._EndFlag;
            }
            set
            {
                this._EndFlag = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="MusicId", DataFormat=DataFormat.TwosComplement)]
        public uint MusicId
        {
            get
            {
                return this._MusicId;
            }
            set
            {
                this._MusicId = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Offset", DataFormat=DataFormat.FixedSize)]
        public float Offset
        {
            get
            {
                return this._Offset;
            }
            set
            {
                this._Offset = value;
            }
        }

        [ProtoMember(13, IsRequired=false, Name="ResultType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ResultType
        {
            get
            {
                return this._ResultType;
            }
            set
            {
                this._ResultType = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="SongAlbum", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongAlbum
        {
            get
            {
                return this._SongAlbum;
            }
            set
            {
                this._SongAlbum = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="SongAlbumUrl", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongAlbumUrl
        {
            get
            {
                return this._SongAlbumUrl;
            }
            set
            {
                this._SongAlbumUrl = value;
            }
        }

        [ProtoMember(12, IsRequired=true, Name="SongLyric", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongLyric
        {
            get
            {
                return this._SongLyric;
            }
            set
            {
                this._SongLyric = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="SongName", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongName
        {
            get
            {
                return this._SongName;
            }
            set
            {
                this._SongName = value;
            }
        }

        [ProtoMember(6, IsRequired=true, Name="SongSinger", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongSinger
        {
            get
            {
                return this._SongSinger;
            }
            set
            {
                this._SongSinger = value;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="SongWapLinkUrl", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongWapLinkUrl
        {
            get
            {
                return this._SongWapLinkUrl;
            }
            set
            {
                this._SongWapLinkUrl = value;
            }
        }

        [ProtoMember(11, IsRequired=true, Name="SongWebUrl", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongWebUrl
        {
            get
            {
                return this._SongWebUrl;
            }
            set
            {
                this._SongWebUrl = value;
            }
        }

        [ProtoMember(9, IsRequired=true, Name="SongWifiUrl", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t SongWifiUrl
        {
            get
            {
                return this._SongWifiUrl;
            }
            set
            {
                this._SongWifiUrl = value;
            }
        }

        [ProtoMember(15, IsRequired=false, Name="TVDescriptionXML", DataFormat=DataFormat.Default), DefaultValue("")]
        public string TVDescriptionXML
        {
            get
            {
                return this._TVDescriptionXML;
            }
            set
            {
                this._TVDescriptionXML = value;
            }
        }

        [ProtoMember(14, IsRequired=false, Name="TVType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint TVType
        {
            get
            {
                return this._TVType;
            }
            set
            {
                this._TVType = value;
            }
        }
    }
}

