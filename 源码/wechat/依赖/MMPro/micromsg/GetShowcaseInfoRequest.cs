namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="GetShowcaseInfoRequest")]
    public class GetShowcaseInfoRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private uint _NBSId;
        private SKBuiltinBuffer_t _PageBuff;
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

        [ProtoMember(2, IsRequired=true, Name="NBSId", DataFormat=DataFormat.TwosComplement)]
        public uint NBSId
        {
            get
            {
                return this._NBSId;
            }
            set
            {
                this._NBSId = value;
            }
        }

        [ProtoMember(3, IsRequired=true, Name="PageBuff", DataFormat=DataFormat.Default)]
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
    }
}

