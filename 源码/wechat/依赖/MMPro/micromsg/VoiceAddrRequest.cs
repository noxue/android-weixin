namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="VoiceAddrRequest")]
    public class VoiceAddrRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _BitsPerSample;
        private string _ClientVoiceId = "";
        private SKBuiltinBuffer_t _Data;
        private uint _EndFlag;
        private uint _ExtraNameCount = 0;
        private readonly List<SKBuiltinBuffer_t> _ExtraNameRemarkNameList = new List<SKBuiltinBuffer_t>();
        private readonly List<SKBuiltinString_t> _ExtraNameUserNameList = new List<SKBuiltinString_t>();
        private uint _FileType;
        private uint _Offset;
        private uint _SamplePerSec;
        private uint _SearchType = 0;
        private string _UserName = "";
        private uint _VoiceEncodeType;
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

        [ProtoMember(8, IsRequired=true, Name="BitsPerSample", DataFormat=DataFormat.TwosComplement)]
        public uint BitsPerSample
        {
            get
            {
                return this._BitsPerSample;
            }
            set
            {
                this._BitsPerSample = value;
            }
        }

        [ProtoMember(5, IsRequired=false, Name="ClientVoiceId", DataFormat=DataFormat.Default), DefaultValue("")]
        public string ClientVoiceId
        {
            get
            {
                return this._ClientVoiceId;
            }
            set
            {
                this._ClientVoiceId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="Data", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t Data
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

        [ProtoMember(6, IsRequired=true, Name="EndFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(12, IsRequired=false, Name="ExtraNameCount", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint ExtraNameCount
        {
            get
            {
                return this._ExtraNameCount;
            }
            set
            {
                this._ExtraNameCount = value;
            }
        }

        [ProtoMember(14, Name="ExtraNameRemarkNameList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinBuffer_t> ExtraNameRemarkNameList
        {
            get
            {
                return this._ExtraNameRemarkNameList;
            }
        }

        [ProtoMember(13, Name="ExtraNameUserNameList", DataFormat=DataFormat.Default)]
        public List<SKBuiltinString_t> ExtraNameUserNameList
        {
            get
            {
                return this._ExtraNameUserNameList;
            }
        }

        [ProtoMember(10, IsRequired=true, Name="FileType", DataFormat=DataFormat.TwosComplement)]
        public uint FileType
        {
            get
            {
                return this._FileType;
            }
            set
            {
                this._FileType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="Offset", DataFormat=DataFormat.TwosComplement)]
        public uint Offset
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

        [ProtoMember(7, IsRequired=true, Name="SamplePerSec", DataFormat=DataFormat.TwosComplement)]
        public uint SamplePerSec
        {
            get
            {
                return this._SamplePerSec;
            }
            set
            {
                this._SamplePerSec = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="SearchType", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint SearchType
        {
            get
            {
                return this._SearchType;
            }
            set
            {
                this._SearchType = value;
            }
        }

        [ProtoMember(2, IsRequired=false, Name="UserName", DataFormat=DataFormat.Default), DefaultValue("")]
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

        [ProtoMember(9, IsRequired=true, Name="VoiceEncodeType", DataFormat=DataFormat.TwosComplement)]
        public uint VoiceEncodeType
        {
            get
            {
                return this._VoiceEncodeType;
            }
            set
            {
                this._VoiceEncodeType = value;
            }
        }
    }
}

