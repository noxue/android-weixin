namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="VoiceAttr")]
    public class VoiceAttr : IExtensible
    {
        private int _BitsPerSample;
        private int _EncodeType;
        private int _FileType;
        private int _SampleRate;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(4, IsRequired=true, Name="BitsPerSample", DataFormat=DataFormat.TwosComplement)]
        public int BitsPerSample
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

        [ProtoMember(2, IsRequired=true, Name="EncodeType", DataFormat=DataFormat.TwosComplement)]
        public int EncodeType
        {
            get
            {
                return this._EncodeType;
            }
            set
            {
                this._EncodeType = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="FileType", DataFormat=DataFormat.TwosComplement)]
        public int FileType
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

        [ProtoMember(3, IsRequired=true, Name="SampleRate", DataFormat=DataFormat.TwosComplement)]
        public int SampleRate
        {
            get
            {
                return this._SampleRate;
            }
            set
            {
                this._SampleRate = value;
            }
        }
    }
}

