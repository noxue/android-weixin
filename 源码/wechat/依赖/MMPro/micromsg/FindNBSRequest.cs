namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="FindNBSRequest")]
    public class FindNBSRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _NBSType;
        private SKBuiltinBuffer_t _PageBuff;
        private PositionInfo _UserPos;
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

        [ProtoMember(2, IsRequired=true, Name="NBSType", DataFormat=DataFormat.TwosComplement)]
        public uint NBSType
        {
            get
            {
                return this._NBSType;
            }
            set
            {
                this._NBSType = value;
            }
        }

        [ProtoMember(4, IsRequired=true, Name="PageBuff", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t PageBuff
        {
            get
            {
                return this._PageBuff;
            }
            set
            {
                this._PageBuff = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="UserPos", DataFormat=DataFormat.Default)]
        public PositionInfo UserPos
        {
            get
            {
                return this._UserPos;
            }
            set
            {
                this._UserPos = value;
            }
        }
    }
}

