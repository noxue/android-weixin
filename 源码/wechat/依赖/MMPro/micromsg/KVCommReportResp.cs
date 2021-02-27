namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="KVCommReportResp")]
    public class KVCommReportResp : IExtensible
    {
        private micromsg.BaseResponse _BaseResponse;
        private SKBuiltinBuffer_t _KVResponBuffer;
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

        [ProtoMember(2, IsRequired=true, Name="KVResponBuffer", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t KVResponBuffer
        {
            get
            {
                return this._KVResponBuffer;
            }
            set
            {
                this._KVResponBuffer = value;
            }
        }
    }
}

