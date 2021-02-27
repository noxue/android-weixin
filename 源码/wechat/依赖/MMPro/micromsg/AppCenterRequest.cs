namespace micromsg
{
    using ProtoBuf;
    using System;

    [Serializable, ProtoContract(Name="AppCenterRequest")]
    public class AppCenterRequest : IExtensible
    {
        private micromsg.BaseRequest _BaseRequest;
        private SKBuiltinBuffer_t _ReqBuf;
        private uint _Type;
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

        [ProtoMember(3, IsRequired=true, Name="ReqBuf", DataFormat=DataFormat.Default)]
        public SKBuiltinBuffer_t ReqBuf
        {
            get
            {
                return this._ReqBuf;
            }
            set
            {
                this._ReqBuf = value;
            }
        }

        [ProtoMember(2, IsRequired=true, Name="Type", DataFormat=DataFormat.TwosComplement)]
        public uint Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

