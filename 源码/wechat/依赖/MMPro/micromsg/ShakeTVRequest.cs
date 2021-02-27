namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="ShakeTVRequest")]
    public class ShakeTVRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _Data;
        private uint _DataId;
        private uint _EndFlag;
        private uint _NetType;
        private uint _SdkVersion;
        private float _VoiceLen;
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

        [ProtoMember(2, IsRequired=true, Name="Data", DataFormat=DataFormat.Default)]
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

        [ProtoMember(3, IsRequired=true, Name="DataId", DataFormat=DataFormat.TwosComplement)]
        public uint DataId
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

        [ProtoMember(4, IsRequired=true, Name="EndFlag", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(6, IsRequired=true, Name="NetType", DataFormat=DataFormat.TwosComplement)]
        public uint NetType
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

        [ProtoMember(7, IsRequired=true, Name="SdkVersion", DataFormat=DataFormat.TwosComplement)]
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

        [ProtoMember(5, IsRequired=true, Name="VoiceLen", DataFormat=DataFormat.FixedSize)]
        public float VoiceLen
        {
            get
            {
                return this._VoiceLen;
            }
            set
            {
                this._VoiceLen = value;
            }
        }
    }
}

