namespace micromsg
{
    using ProtoBuf;
    using System;
    using System.ComponentModel;

    [Serializable, ProtoContract(Name="UploadMediaRequest")]
    public class UploadMediaRequest : IExtensible
    {
        private uint _AudioBitRate = 0;
        private uint _AudioChannelNum = 0;
        private uint _AudioFormat = 0;
        private uint _AudioSamplingRate = 0;
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinString_t _ClientMediaId;
        private SKBuiltinBuffer_t _Data;
        private uint _DataLen;
        private SKBuiltinString_t _DataMD5;
        private uint _MediaType;
        private uint _StartPos;
        private uint _TotalLen;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(12, IsRequired=false, Name="AudioBitRate", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AudioBitRate
        {
            get
            {
                return this._AudioBitRate;
            }
            set
            {
                this._AudioBitRate = value;
            }
        }

        [ProtoMember(11, IsRequired=false, Name="AudioChannelNum", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AudioChannelNum
        {
            get
            {
                return this._AudioChannelNum;
            }
            set
            {
                this._AudioChannelNum = value;
            }
        }

        [ProtoMember(9, IsRequired=false, Name="AudioFormat", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AudioFormat
        {
            get
            {
                return this._AudioFormat;
            }
            set
            {
                this._AudioFormat = value;
            }
        }

        [ProtoMember(10, IsRequired=false, Name="AudioSamplingRate", DataFormat=DataFormat.TwosComplement), DefaultValue((long) 0L)]
        public uint AudioSamplingRate
        {
            get
            {
                return this._AudioSamplingRate;
            }
            set
            {
                this._AudioSamplingRate = value;
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

        [ProtoMember(2, IsRequired=true, Name="ClientMediaId", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t ClientMediaId
        {
            get
            {
                return this._ClientMediaId;
            }
            set
            {
                this._ClientMediaId = value;
            }
        }

        [ProtoMember(7, IsRequired=true, Name="Data", DataFormat=DataFormat.Default)]
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

        [ProtoMember(6, IsRequired=true, Name="DataLen", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(3, IsRequired=true, Name="DataMD5", DataFormat=DataFormat.Default)]
        public SKBuiltinString_t DataMD5
        {
            get
            {
                return this._DataMD5;
            }
            set
            {
                this._DataMD5 = value;
            }
        }

        [ProtoMember(8, IsRequired=true, Name="MediaType", DataFormat=DataFormat.TwosComplement)]
        public uint MediaType
        {
            get
            {
                return this._MediaType;
            }
            set
            {
                this._MediaType = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="StartPos", DataFormat=DataFormat.TwosComplement)]
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
    }
}

