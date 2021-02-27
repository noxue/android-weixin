namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="HWTimeInfo")]
    public class HWTimeInfo : IExtensible
    {
        private int _DecodeTimeScale;
        private long _DecodeTimeValue;
        private int _DurTimeScale;
        private long _DurTimeValue;
        private int _PresentTimeScale;
        private long _PresentTimeValue;
        private IExtension extensionObject;

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }

        [ProtoMember(6, IsRequired=true, Name="DecodeTimeScale", DataFormat=DataFormat.TwosComplement)]
        public int DecodeTimeScale
        {
            get
            {
                return this._DecodeTimeScale;
            }
            set
            {
                this._DecodeTimeScale = value;
            }
        }

        [ProtoMember(5, IsRequired=true, Name="DecodeTimeValue", DataFormat=DataFormat.TwosComplement)]
        public long DecodeTimeValue
        {
            get
            {
                return this._DecodeTimeValue;
            }
            set
            {
                this._DecodeTimeValue = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="DurTimeScale", DataFormat=DataFormat.TwosComplement)]
        public int DurTimeScale
        {
            get
            {
                return this._DurTimeScale;
            }
            set
            {
                this._DurTimeScale = value;
            }
        }

        [ProtoMember(1, IsRequired=true, Name="DurTimeValue", DataFormat=DataFormat.TwosComplement)]
        public long DurTimeValue
        {
            get
            {
                return this._DurTimeValue;
            }
            set
            {
                this._DurTimeValue = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="PresentTimeScale", DataFormat=DataFormat.TwosComplement)]
        public int PresentTimeScale
        {
            get
            {
                return this._PresentTimeScale;
            }
            set
            {
                this._PresentTimeScale = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="PresentTimeValue", DataFormat=DataFormat.TwosComplement)]
        public long PresentTimeValue
        {
            get
            {
                return this._PresentTimeValue;
            }
            set
            {
                this._PresentTimeValue = value;
            }
        }
    }
}

